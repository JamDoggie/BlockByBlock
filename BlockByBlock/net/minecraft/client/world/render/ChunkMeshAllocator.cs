using BlockByBlock.net.minecraft.render;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.world.render
{
    public partial class ChunkMeshAllocator : IAllocator
    {
        public UnsafeByteBuffer Buffer { get; set; }
        public Dictionary<int, BufferSegment> DataAllocations = new(); // Id, Offset
        public List<BufferSegment> FreeSegments = new();
        
        private int maxAllocationOffset = 0;
        private int currentIdNum = 0;

        // GL
        public VertexBuffer WorldBuffer;

        public ChunkMeshAllocator()
        {
            Buffer = new(DefaultBufferVerticeSize);

            int[] worldVBOBuffer = { 0 };
            GL.CreateBuffers(1, worldVBOBuffer);

            if (worldVBOBuffer.Length > 0)
            {
                int worldVBO = worldVBOBuffer[0];
                WorldBuffer = new(DefaultBufferVerticeSize, 0, worldVBO, 7, true, true, true, false);
                GL.NamedBufferData(worldVBO, Buffer.Size, Buffer.Handle, BufferUsageHint.DynamicDraw);
            }
        }

        public int AllocateData(Span<byte> data)
        {
            int id;

            if (maxAllocationOffset == 0)
            {
                id = PlaceData(0, data);
                maxAllocationOffset += data.Length;
                return id;
            }
            
            // Find the smallest free segment that can fit the data.
            BufferSegment? bestFreeSegmentToFill = null;

            foreach (BufferSegment freeSegment in FreeSegments)
            {
                if (freeSegment.Size >= data.Length)
                {
                    if (bestFreeSegmentToFill == null)
                    {
                        bestFreeSegmentToFill = freeSegment;
                    }
                    else
                    {
                        if (freeSegment.Size < bestFreeSegmentToFill?.Size)
                        {
                            bestFreeSegmentToFill = freeSegment;
                        }
                    }
                }
            }

            if (bestFreeSegmentToFill != null)
            {
                // We found a good free segment that can fit the data.
                id = PlaceData(bestFreeSegmentToFill!.Value.Offset, data);
                return id;
            }
            
            // We didn't find a free segment that can fit the data, so we'll have to allocate a new one.
            id = PlaceData(maxAllocationOffset, data);
            maxAllocationOffset += data.Length;
            return id;
        }

        public bool FreeData(int id)
        {
            Console.WriteLine("Free called");

            if (!DataAllocations.ContainsKey(id))
                return false;

            BufferSegment segment = DataAllocations[id];

            FreeSegments.Add(segment);

            if (segment.Offset + segment.Size == maxAllocationOffset)
            {
                // The segment is at the end of the buffer, so we can shrink the allocation space.
                maxAllocationOffset -= segment.Size;
            }

            MergeAdjacentFreeSegments();

            return DataAllocations.Remove(id);
        }

        public void FrameUpdate()
        {
#if DEBUG
            if (MinecraftApplet.mcWindow.KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.LeftBracket))
            {
                Console.WriteLine($"Actual allocated buffer size: {Buffer.Size}, amount of buffer used: {maxAllocationOffset}");
            }
#endif

            if (FreeSegments.Count > 100)
                Defragment();
        }

        public BufferSegment? GetAllocationOffset(int id)
        {
            bool exists = DataAllocations.TryGetValue(id, out BufferSegment offset);
            
            return exists ? offset : null;
        }

        public void ResizeBuffer(int newSize)
        {
            Console.WriteLine("Buffer resize!");

            //byte[] newBuffer = new byte[newSize];
            //Array.Copy(Buffer, newBuffer, Buffer.Size);

            UnsafeByteBuffer newBuffer = new(newSize);
            Buffer.CopyTo(newBuffer);

            Buffer = newBuffer;

            ReallocateEntireBuffer();
        }

        public void Defragment()
        {
            Console.WriteLine("Defragmenting!");

            
        }

        private int PlaceData(int offset, Span<byte> inData)
        {
            Profiler.startSection("placeData");
            // Check if the data would exceed the current buffer size, and expand the buffer if so.
            if (offset + inData.Length > Buffer.Size)
                ResizeBuffer(Math.Max(Buffer.Size + BufferIncreaseStep, offset + inData.Length));

            for (int i = 0; i < inData.Length; i++)
            {
                Buffer[offset + i] = inData[i];
            }

            BufferSegment segment = new(offset, inData.Length);

            int id = currentIdNum;
            DataAllocations.Add(id, segment);

            // If we allocated in a free segment, update FreeSegments accordingly.
            if (FreeSegments.Count > 0)
            {
                for (int i = FreeSegments.Count - 1; i >= 0; i--)
                {
                    BufferSegment freeSegment = FreeSegments[i];

                    if (freeSegment.Offset == offset)
                    {
                        if (freeSegment.Size == inData.Length)
                        {
                            FreeSegments.RemoveAt(i);
                            break;
                        }
                        else
                        {
                            FreeSegments[i] = new(freeSegment.Offset + inData.Length, freeSegment.Size - inData.Length);
                            break;
                        }
                    }
                }
            }

            MergeAdjacentFreeSegments();

            UpdateGLArrayForSegment(segment);
            UpdateVertexCount();

            unchecked // Allow an overflow by design, by the time we wrap around from
                      // int.MaxValue to 0, all positive IDs should be gone.
            {
                currentIdNum++;
            }
            Profiler.endSection();
            return id;
        }

        private void MergeAdjacentFreeSegments()
        {
            // Remove any free segments that border eachother, and merge them into one.
            for (int i = 0; i < FreeSegments.Count; i++)
            {
                BufferSegment segment = FreeSegments[i];

                for (int j = 0; j < FreeSegments.Count; j++)
                {
                    if (i == j)
                        continue;

                    BufferSegment otherSegment = FreeSegments[j];

                    if (segment.Offset + segment.Size == otherSegment.Offset)
                    {
                        FreeSegments[i] = new(segment.Offset, segment.Size + otherSegment.Size);
                        FreeSegments.RemoveAt(j);
                        break;
                    }
                    else if (otherSegment.Offset + otherSegment.Size == segment.Offset)
                    {
                        FreeSegments[i] = new(otherSegment.Offset, segment.Size + otherSegment.Size);
                        FreeSegments.RemoveAt(j);
                        break;
                    }
                }
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
            //Defragment();

            // Reallocate the entire buffer.
            // This is done when the buffer is resized, or when the buffer is cleared.
            // This is done because the buffer is a single array, and we can't just resize it.
            // We have to copy the data to a new array, and then reallocate the GL buffer.
            // This is a very expensive operation, so it should be avoided as much as possible.
            int worldVBO = WorldBuffer.GLHandle;
            int vertices = WorldBuffer.VertexCount;
            WorldBuffer = new(Buffer.Size, vertices, worldVBO, 7, true, true, true, false);
            GL.NamedBufferData(worldVBO, Buffer.Size, IntPtr.Zero, BufferUsageHint.DynamicDraw);
            GL.NamedBufferSubData(worldVBO, 0, maxAllocationOffset, Buffer.Handle);
        }

        private void UpdateGLArrayForSegment(BufferSegment segment)
        {
            GL.NamedBufferSubData(WorldBuffer.GLHandle, segment.Offset, segment.Size, Buffer.Handle + segment.Offset);
        }
    }

    public struct BufferSegment
    {
        public int Offset;
        public int Size;

        public BufferSegment(int offset, int size)
        {
            Offset = offset;
            Size = size;
        }
    }
}
