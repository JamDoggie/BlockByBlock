using BlockByBlock.net.minecraft.render;
using javax.swing.text;
using net.minecraft.client.entity;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Security.Policy;

namespace net.minecraft.client.world.render
{
    public unsafe partial class ChunkMeshAllocator
    {
        public UnsafeByteBuffer Buffer { get; set; }
        public Dictionary<int, BufferSegment> DataAllocations = new(); // Id, Offset
        public LinkedList<BufferSegment> bufferSegments = new();

        private int maxAllocationOffset = 0;
        private int currentIdNum = 0;

        // GL
        public VertexBuffer WorldBuffer;
        internal int PositionsSSBO;

        public ChunkMeshAllocator()
        {
            Buffer = new(DefaultBufferVerticeSize);

            int[] buffers = { 0, 0 }; // 0 = world VBO, 1 = positions SSBO
            GL.CreateBuffers(2, buffers);
            
            int worldVBO = buffers[0];
            WorldBuffer = new(DefaultBufferVerticeSize, 0, worldVBO, 7, true, true, true, false);
            GL.NamedBufferData(worldVBO, Buffer.Size, Buffer.Handle, BufferUsageHint.DynamicDraw);
            
            PositionsSSBO = buffers[1];

            GL.BindBufferBase(BufferRangeTarget.ShaderStorageBuffer, 5, PositionsSSBO);
        }
        
        public int AllocateData(Span<byte> data, WorldRenderer renderer, int pass)
        {
            int id;

            if (maxAllocationOffset == 0)
            {
                bufferSegments.AddFirst(new BufferSegment(0, data.Length, null, null));
                id = PlaceData(0, data, bufferSegments.First, renderer, pass);

                maxAllocationOffset += data.Length;
                return id;
            }

            // Find the smallest free segment that can fit the data.
            LinkedListNode<BufferSegment>? smallestSegment = null;

            LinkedListNode<BufferSegment>? node = bufferSegments.First;
            for (int i = 0; i < bufferSegments.Count; i++)
            {
                if (node == null)
                    break;

                BufferSegment currentSegment = node.ValueRef;

                if (currentSegment.IsFree && currentSegment.Size >= data.Length)
                {
                    if (smallestSegment == null)
                    {
                        smallestSegment = node;
                    }
                    else if (currentSegment.Size < smallestSegment?.ValueRef.Size)
                    {
                        smallestSegment = node;
                    }
                }

                node = node?.Next;
            }
            
            if (smallestSegment != null)
            {
                // We found a good free segment that can fit the data.
                id = PlaceData(smallestSegment!.Value.Offset, data, smallestSegment, renderer, pass);
                return id;
            }
            
            // We didn't find a free segment that can fit the data, so we'll have to allocate a new one.
            LinkedListNode<BufferSegment> lastSegment = bufferSegments.AddLast(new BufferSegment(maxAllocationOffset, data.Length, null, null));
            id = PlaceData(maxAllocationOffset, data, lastSegment, renderer, pass);
            maxAllocationOffset += data.Length;
            return id;
        }

        public bool FreeData(int id)
        {
            if (!DataAllocations.ContainsKey(id))
                return false;

            BufferSegment allocatedSegment = DataAllocations[id];

            LinkedListNode<BufferSegment>? allocatedSegmentNode = null;
            LinkedListNode<BufferSegment>? nodeIter = bufferSegments.First;
            for (int i = 0; i < bufferSegments.Count; i++)
            {
                if (nodeIter == null)
                    break;

                if (nodeIter.ValueRef.DataHandle == allocatedSegment.DataHandle)
                {
                    allocatedSegmentNode = nodeIter;
                    allocatedSegmentNode.ValueRef.DataHandle = null;
                    break;
                }

                nodeIter = nodeIter?.Next;
            }

            if (allocatedSegment.Offset + allocatedSegment.Size == maxAllocationOffset && bufferSegments.Last?.ValueRef.DataHandle == allocatedSegment.DataHandle)
            {
                // The segment is at the end of the buffer, so we can shrink the allocation space & remove the free segment.
                maxAllocationOffset -= allocatedSegment.Size;
                
                if (allocatedSegmentNode != null)
                    bufferSegments.Remove(allocatedSegmentNode);
            }
            else
            {
                MergeAdjacentFreeSegments(allocatedSegmentNode);
            }

            return DataAllocations.Remove(id);
        }

        public void FrameUpdate()
        {
#if DEBUGTOOLS
            if (MinecraftApplet.mcWindow.KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.LeftBracket))
            {
                Console.WriteLine($"Actual allocated buffer size: {Buffer.Size}, amount of buffer used: {maxAllocationOffset}");
            }
#endif
        }

        public void ResizeBuffer(int newSize)
        {
            Console.WriteLine("Buffer resize!");
            
            UnsafeByteBuffer newBuffer = new(newSize);
            Buffer.CopyBytesTo(newBuffer);

            Buffer = newBuffer;

            ReallocateEntireBuffer();
        }
        
        private int PlaceData(int offset, Span<byte> inData, LinkedListNode<BufferSegment>? existingFreeSpace, WorldRenderer? renderer, int pass)
        {
            Profiler.startSection("placeData");
            // Check if the data would exceed the current buffer size, and expand the buffer if so.
            if (offset + inData.Length > Buffer.Size)
                ResizeBuffer(Math.Max(Buffer.Size + BufferIncreaseStep, offset + inData.Length));
            
            for (int i = 0; i < inData.Length; i++)
            {
                Buffer[offset + i] = inData[i];
            }

            int id = currentIdNum;
            BufferSegment segment = new(offset, inData.Length, id, renderer, pass);
            
            // If we allocated in a free segment, split or remove the free segment depending on if we fully fill it or not.
            if (existingFreeSpace != null)
            {
                BufferSegment freeSegment = existingFreeSpace.ValueRef;

                if (freeSegment.Size == inData.Length)
                {
                    existingFreeSpace.Value = segment;
                }
                else
                {
                    freeSegment = new(freeSegment.Offset + inData.Length, freeSegment.Size - inData.Length, null, null);

                    existingFreeSpace.Value = segment;
                    bufferSegments.AddAfter(existingFreeSpace, freeSegment);
                }
            }

            DataAllocations.Add(id, segment);

            UpdateGLArrayForSegment(segment);
            UpdateVertexCount();

            unchecked // Allow an overflow in case this number somehow reaches the max.
            {
                currentIdNum++;
            }
            
            Profiler.endSection();
            return id;
        }

        /// <summary>
        /// Takes in the linked list node for a free segment, and merges it with any directly adjacent free segments.
        /// </summary>
        /// <param name="freeSegment"></param>
        private void MergeAdjacentFreeSegments(LinkedListNode<BufferSegment> freeSegment)
        {
            // Merge the given free segment with neighboring free segments.
            LinkedListNode<BufferSegment>? leftSegment = freeSegment.Previous;
            LinkedListNode<BufferSegment>? rightSegment = freeSegment.Next;

            if (leftSegment != null && leftSegment.ValueRef.IsFree)
            {
                leftSegment.ValueRef.Size += freeSegment.ValueRef.Size;
                bufferSegments.Remove(freeSegment);

                freeSegment = leftSegment;
            }
            
            if (rightSegment != null && rightSegment.ValueRef.IsFree)
            {
                freeSegment.ValueRef.Size += rightSegment.ValueRef.Size;
                bufferSegments.Remove(rightSegment);
            }
        }

        private void UpdateVertexCount()
        {
            int byteCount = 0;

            foreach (BufferSegment segment in DataAllocations.Values)
            {
                byteCount += segment.Size;
            }

            WorldBuffer.SizeInBytes = byteCount;
            WorldBuffer.VertexCount = byteCount / Tessellator.VertexSize;
        }

        private void ReallocateEntireBuffer()
        {
            // Reallocate the entire buffer.
            // This is done when the buffer is resized, or when the buffer is cleared.
            // This is done because the buffer is a single array, and we can't just resize it.
            // We have to copy the data to a new array, and then reallocate the GL buffer.
            // This is a very expensive operation, so it should be avoided as much as possible.
            int worldVBO = WorldBuffer.GLHandle;
            int vertices = WorldBuffer.VertexCount;
            WorldBuffer = new(Buffer.Size, vertices, worldVBO, 7, true, true, true, false);
            GL.NamedBufferData(worldVBO, Buffer.Size, IntPtr.Zero, BufferUsageHint.DynamicDraw);
            GL.NamedBufferSubData(worldVBO, 0, maxAllocationOffset, (nint)Buffer.Handle);
        }

        private void UpdateGLArrayForSegment(BufferSegment segment)
        {
            GL.NamedBufferSubData(WorldBuffer.GLHandle, segment.Offset, segment.Size, (nint)Buffer.Handle + segment.Offset);
        }
    }

    public struct BufferSegment
    {
        public bool IsFree => DataHandle == null;

        public int Offset;
        public int Size;
        public int? DataHandle; // Will be null if this segment represents free space.
        public WeakReference<WorldRenderer>? RendererRef;
        public int? RenderPass;

        public BufferSegment(int offset, int size, int? handle, WorldRenderer? renderer, int? renderPass = null)
        {
            Offset = offset;
            Size = size;
            DataHandle = handle;

            if (renderer != null)
                RendererRef = new(renderer);

            RenderPass = renderPass;
        }
    }
}
