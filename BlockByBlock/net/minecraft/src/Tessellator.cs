#pragma warning disable CA2014

namespace net.minecraft.src
{
    using BlockByBlock.helpers;
	using BlockByBlock.net.minecraft.render;
	using BlockByBlock.sound;
	using com.sun.org.apache.xerces.@internal.impl.dv.xs;
	using com.sun.tools.corba.se.logutil;
	using java.lang;
	using javax.swing;
	using net.minecraft.client;
	using net.minecraft.client.entity;
	using net.minecraft.client.entity.render;
	using net.minecraft.client.world.render;
	using OpenTK.Graphics.OpenGL;
	using OpenTK.Mathematics;
	using System.Runtime.InteropServices;

    public class Tessellator
	{
        public bool CurrentlyBuildingVBO { get; set; } = false;
        public int VAO;
        
        public static int VertexSize = 32;
        public static readonly Tessellator instance = new(2097152);

        private ByteBuffer byteBuffer;
		private int[] rawBuffer;
		private int vertexCount = 0;
		private double textureU;
		private double textureV;
		private int brightness;
		private int color;
		private bool hasColor = false;
		private bool hasTexture = false;
		private bool hasBrightness = false;
		private bool hasNormals = false;
		private int rawBufferIndex = 0;
		private int addedVertices = 0;
		private bool isColorDisabled = false;
		private int drawMode;
		private double xOffset;
		private double yOffset;
		private double zOffset;
		private int normal;
		
		private bool isDrawing = false;
		private int[] vertexBuffers;
		private int vboIndex = 0;
		private int vboCount = 10;
		private int bufferSize;
		private IntPtr bufferPointer;
		private int previousWorldRendererBufferSize = -1;
		private int renderingTerrainUniform = -1;

		#region Unsafe terrain optimization things
		private UnsafeIntBuffer? chunkPositionsBuffer;
        private UnsafeIntBuffer? chunkMeshOffsets;
        private UnsafeIntBuffer? chunkMeshLengths;
        #endregion

        private Tessellator(int bufSize)
		{
			bufferSize = bufSize;
			byteBuffer = GLAllocation.createDirectByteBuffer(bufSize * 4);
            
			byte[] underlyingBuffer = byteBuffer.GetUnderlyingBuffer();
			bufferPointer = Marshal.AllocHGlobal(underlyingBuffer.Length);
			rawBuffer = new int[bufSize];

			// VBOs
			vertexBuffers = new int[vboCount];
			GL.CreateBuffers(vertexBuffers.Length, vertexBuffers);
        }

        ~Tessellator()
        {
            Marshal.FreeHGlobal(bufferPointer);
        }

		private bool hasCopiedBuffer = false;
		private Random testRand = new();
        
        internal void Init()
		{
            int[] vao = new int[1];
            GL.CreateVertexArrays(1, vao);

            VAO = vao[0];

            SetupVertexArray();

            renderingTerrainUniform = GL.GetUniformLocation(Minecraft.renderPipeline.GLProgram, "RenderingTerrain");
        }

		internal unsafe void SetupVertexArray()
		{
			// Main vertex array
            GL.EnableVertexArrayAttrib(VAO, 0);
            GL.EnableVertexArrayAttrib(VAO, 1);
            GL.EnableVertexArrayAttrib(VAO, 2);
            GL.EnableVertexArrayAttrib(VAO, 3);
            GL.EnableVertexArrayAttrib(VAO, 4);

            GL.VertexArrayAttribFormat(VAO, 0, 3, VertexAttribType.Float, false, 0);
			GL.VertexArrayAttribFormat(VAO, 1, 2, VertexAttribType.Float, false, 12);
			GL.VertexArrayAttribFormat(VAO, 2, 4, VertexAttribType.UnsignedByte, false, 20);
			GL.VertexArrayAttribFormat(VAO, 3, 4, VertexAttribType.Byte, false, 24);
			GL.VertexArrayAttribFormat(VAO, 4, 2, VertexAttribType.Short, false, 28);
			
            GL.VertexArrayAttribBinding(VAO, 0, 0);
            GL.VertexArrayAttribBinding(VAO, 1, 0);
            GL.VertexArrayAttribBinding(VAO, 2, 0);
            GL.VertexArrayAttribBinding(VAO, 3, 0);
            GL.VertexArrayAttribBinding(VAO, 4, 0);

			GL.BindVertexArray(VAO);

            // Vertex
            /*GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, VertexSize, 0);
            
            // Texture Coords
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, VertexSize, 12);
            
            // Color
            GL.EnableVertexAttribArray(2);
            GL.VertexAttribPointer(2, 4, VertexAttribPointerType.UnsignedByte, false, VertexSize, 20);
            
            // Normal
            GL.EnableVertexAttribArray(3);
            GL.VertexAttribPointer(3, 4, VertexAttribPointerType.Byte, false, VertexSize, 24);
            
            // Brightness
            GL.EnableVertexAttribArray(4);
            GL.VertexAttribPointer(4, 2, VertexAttribPointerType.Short, false, VertexSize, 28);*/
        }

		public unsafe virtual int DrawImmediate()
		{
			if (CurrentlyBuildingVBO)
			{
				throw new InvalidOperationException("Cannot draw while building a VBO!");
			}

			if (!isDrawing)
			{
				throw new InvalidOperationException("Not tesselating!");
			}
			else
			{
				isDrawing = false;
				if (vertexCount > 0)
				{
                    uploadMatrixStacks();
                    
                    byteBuffer.clear();
                    byteBuffer.Put(rawBuffer, rawBufferIndex);
                    byteBuffer.position(0);
                    byteBuffer.limit(rawBufferIndex * 4);
                    
                    vboIndex = (vboIndex + 1) % vboCount;

                    SetupVertexArrays(vertexBuffers[vboIndex]);
                    GL.NamedBufferData(vertexBuffers[vboIndex], (int)byteBuffer.getLimit(), byteBuffer.GetUnderlyingBuffer(), BufferUsageHint.StreamDraw);
                    
                    if (drawMode == 7) // We convert quads to tris
                    {
                        GL.DrawArrays(PrimitiveType.Triangles, 0, vertexCount);
                    }
                    else
                    {
                        GL.DrawArrays((PrimitiveType)drawMode, 0, vertexCount);
                    }
                }

				int i1 = rawBufferIndex * 4;
				reset();
				return i1;
			}
		}

		private string vertexBufferProfilerSection = "vertexArrays";

		public virtual void Draw(VertexBuffer vbo)
		{
            uploadMatrixStacks();
            
			SetupVertexArrays(vbo.GLHandle);

			if (!vbo.HasBrightness)
			{
                Minecraft.renderPipeline.SetState(RenderState.OverrideBrightnessState, true);
				Minecraft.renderPipeline.SetBrightnessOverrideCoords(Minecraft.renderPipeline.LightmapCoords.X, Minecraft.renderPipeline.LightmapCoords.Y);
            }
				
            if (vbo.DrawMode == 7) // Any vertices that are given as quads are automatically converted to tris
								   // beforehand because GL_QUADS (draw mode 7) has been obsolete for quite some time.
            {
                GL.DrawArrays(PrimitiveType.Triangles, 0, vbo.VertexCount);
            }
            else
            {
                GL.DrawArrays((PrimitiveType)vbo.DrawMode, 0, vbo.VertexCount);
            }

            if (!vbo.HasBrightness)
                Minecraft.renderPipeline.SetState(RenderState.OverrideBrightnessState, false);
        }
		
		public virtual void TessellateOcclusionQuery(int vbo)
		{
            uploadMatrixStacks();

			SetupVertexArrays(vbo);

            GL.DrawArrays(PrimitiveType.Triangles, 0, vbo);
        }

        public unsafe virtual void DrawMeshAllocator(VertexBuffer vbo, ChunkMeshAllocator meshAllocator, WorldRenderer[] sortedRenderers, int pass)
        {
			Profiler.startSection("drawMeshAllocator");
            Profiler.startSection("setStates");
			
			try
			{
                int renderersWithThisPass = 0;

                foreach (WorldRenderer renderer in sortedRenderers)
                {
                    if (renderer.skipRenderPass[pass] || !renderer.isVisible)
                        continue;

                    renderersWithThisPass++;
                }

                if (renderersWithThisPass == 0)
                    return;

                uploadMatrixStacks();

                SetupVertexArrays(vbo.GLHandle);

                if (!vbo.HasBrightness)
                { 
                    Minecraft.renderPipeline.SetState(RenderState.OverrideBrightnessState, true);
                    Minecraft.renderPipeline.SetBrightnessOverrideCoords(Minecraft.renderPipeline.LightmapCoords.X, Minecraft.renderPipeline.LightmapCoords.Y);
                }
				
                GL.Uniform1(renderingTerrainUniform, 1);

                Profiler.startSection("pass");
                Profiler.startSection("allocateBuffers");

                if (previousWorldRendererBufferSize != sortedRenderers.Length)
                {
                    chunkPositionsBuffer?.Dispose();
                    chunkMeshOffsets?.Dispose();
                    chunkMeshLengths?.Dispose();

                    chunkPositionsBuffer = new(3 * sortedRenderers.Length);
                    chunkMeshOffsets = new(sortedRenderers.Length);
                    chunkMeshLengths = new(sortedRenderers.Length);

                    GL.NamedBufferData(meshAllocator.PositionsSSBO, sortedRenderers.Length * 3 * sizeof(float), IntPtr.Zero, BufferUsageHint.StreamDraw);

                    previousWorldRendererBufferSize = sortedRenderers.Length;
                }

                if (chunkPositionsBuffer == null || chunkMeshOffsets == null || chunkMeshLengths == null)
                    throw new IllegalStateException("Buffers were never allocated. This should be impossible.");

                int chunkOffsetsCount = renderersWithThisPass * 3;

                Profiler.endStartSection("setOffsetsAndSSBOLocations");
                int iter = 0;

                for (int i = 0; i < sortedRenderers.Length; i++)
                {
                    WorldRenderer renderer = sortedRenderers[i];

                    if (renderer.skipRenderPass[pass] || !renderer.isVisible)
                        continue;

                    if (renderer.meshDataAllocations[pass] != null)
                    {
                        BufferSegment segment = meshAllocator.DataAllocations[renderer.meshDataAllocations[pass]!.Value];

                        chunkMeshOffsets[iter] = segment.Offset / VertexSize;
                        chunkMeshLengths[iter] = segment.Size / VertexSize;

                        Vector3 chunkOffset = new((float)((double)renderer.X2 - renderer.X1), (float)((double)renderer.Y2 - renderer.Y1), (float)((double)renderer.Z2 - renderer.Z1));
                        chunkPositionsBuffer[iter * 3 + 0] = JTypes.FloatToRawIntBits(chunkOffset.X);
                        chunkPositionsBuffer[iter * 3 + 1] = JTypes.FloatToRawIntBits(chunkOffset.Y);
                        chunkPositionsBuffer[iter * 3 + 2] = JTypes.FloatToRawIntBits(chunkOffset.Z);

                        iter++;
                    }
                }

                Profiler.endStartSection("allocateInBuffer");

                GL.NamedBufferSubData(meshAllocator.PositionsSSBO, 0, chunkOffsetsCount * sizeof(float), (nint)chunkPositionsBuffer.Pointer);

                Profiler.endStartSection("draw");
				

                if (vbo.DrawMode == 7) // Any vertices that are given as quads are automatically converted to tris
                                       // beforehand because GL_QUADS (draw mode 7) has been obsolete for quite some time.
                {
                    GL.MultiDrawArrays(PrimitiveType.Triangles, (int*)chunkMeshOffsets.Pointer, (int*)chunkMeshLengths.Pointer, renderersWithThisPass);
                }
                else
                {
                    GL.MultiDrawArrays((PrimitiveType)vbo.DrawMode, (int*)chunkMeshOffsets.Pointer, (int*)chunkMeshLengths.Pointer, renderersWithThisPass);
                }

                Profiler.endSection();
                Profiler.endSection();

                GL.Uniform1(renderingTerrainUniform, 0);

                if (!vbo.HasBrightness)
                    Minecraft.renderPipeline.SetState(RenderState.OverrideBrightnessState, false);
            }
            finally
			{
                Profiler.endSection();
                Profiler.endSection();
            }
        }

        private void uploadMatrixStacks()
		{
			Minecraft.renderPipeline.TextureMatrix.UpdateUniform();
            Minecraft.renderPipeline.ModelMatrix.UpdateUniform();
            Minecraft.renderPipeline.ProjectionMatrix.UpdateUniform();

			int normalMatrix = Minecraft.renderPipeline.GetUniform("normalMatrix");
			Matrix4 normalMat = Minecraft.renderPipeline.ModelMatrix.GetMatrix();
            Matrix3 normalMat3 = new(normalMat);
			normalMat3.Invert();
			normalMat3.Transpose();
            GL.ProgramUniformMatrix3(Minecraft.renderPipeline.GLProgram, normalMatrix, false, ref normalMat3);
        }

        public void SetupVertexArrays(int vbo)
        {
            GL.VertexArrayVertexBuffer(VAO, 0, vbo, 0, VertexSize);
        }


        private void reset()
		{
			vertexCount = 0;
			byteBuffer.clear();
			rawBufferIndex = 0;
			addedVertices = 0;
        }

		public virtual void startDrawingQuads()
		{
			startDrawing(7);
		}

		public virtual void startDrawing(int mode)
		{
            if (isDrawing)
			{
				throw new InvalidOperationException("Already tesselating!");
			}
			else
			{
				isDrawing = true;
				reset();
				drawMode = mode;
				hasNormals = false;
				hasColor = false;
				hasTexture = false;
				hasBrightness = false;
				isColorDisabled = false;
			}
		}

        public virtual void StartBuildingVBO()
		{
			CurrentlyBuildingVBO = true;
		}

        /// <summary>
        /// Start building a VBO with a specified draw mode instead of using the current draw mode.
        /// mode 7 = Quads
        /// mode 4 = Triangles
        /// </summary>
        /// <param name="mode"></param>
        public virtual void StartBuildingVBO(int mode)
		{
            CurrentlyBuildingVBO = true;
            drawMode = mode;
        }
        
		/// <summary>
		/// If we're currently creating a VBO, this builds it.
		/// 
		/// <para>
		/// If inputVBO is null, a new VBO is created and bound. Otherwise, the VBO you passed in is bound and buffered into.
		/// </para>
		/// </summary>
		/// <param name="inputVBO"></param>
		/// <returns></returns>
        public virtual VertexBuffer BuildCurrentVBO(VertexBuffer? inputVBO = null)
		{
            byteBuffer.clear();
            byteBuffer.Put(rawBuffer, rawBufferIndex);
            byteBuffer.position(0);
            byteBuffer.limit(rawBufferIndex * 4);
            
            int bufferHandle;

            if (inputVBO == null)
                bufferHandle = GL.GenBuffer();
			else
                bufferHandle = inputVBO.Value.GLHandle;

            GL.BindBuffer(BufferTarget.ArrayBuffer, bufferHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, rawBufferIndex * 4, byteBuffer.GetUnderlyingBuffer(), BufferUsageHint.DynamicDraw);
            
            CurrentlyBuildingVBO = false;
            
			VertexBuffer vbo = new(rawBufferIndex * 4, vertexCount, bufferHandle, drawMode, hasBrightness, hasColor, hasTexture, hasNormals);

			reset();

            return vbo;
        }

        public virtual byte[] BuildCurrentVertexBuffer()
		{
            byteBuffer.clear();
            byteBuffer.Put(rawBuffer, rawBufferIndex);
            byteBuffer.position(0);
            byteBuffer.limit(rawBufferIndex * 4);

            byte[] buffer = new byte[rawBufferIndex * 4];

            for (int i = 0; i < rawBufferIndex; i++)
			{
				if (i >= byteBuffer.GetUnderlyingBuffer().Length)
					throw new IndexOutOfBoundsException("Something went wrong, tessellator buffer is too small.");

				IntByteUnion union = new() { integer = rawBuffer[i] };

                buffer[i * sizeof(int)] = union.byte0;
                buffer[i * sizeof(int) + 1] = union.byte1;
                buffer[i * sizeof(int) + 2] = union.byte2;
                buffer[i * sizeof(int) + 3] = union.byte3;
            }

            CurrentlyBuildingVBO = false;
            
            reset();

			return buffer;
        }

		public virtual void SetTextureUV(double d1, double d3)
		{
			hasTexture = true;
			textureU = d1;
			textureV = d3;
		}

		public virtual int Brightness
		{
			set
			{
				hasBrightness = true;
				brightness = value;
			}
		}

		public virtual void setColorOpaque_F(float f1, float f2, float f3)
		{
			setColorOpaque((int)(f1 * 255.0F), (int)(f2 * 255.0F), (int)(f3 * 255.0F));
		}

		public virtual void setColorRGBA_F(float f1, float f2, float f3, float f4)
		{
			setColorRGBA((int)(f1 * 255.0F), (int)(f2 * 255.0F), (int)(f3 * 255.0F), (int)(f4 * 255.0F));
		}

		public virtual void setColorOpaque(int r, int g, int b)
		{
			setColorRGBA(r, g, b, 255);
		}

		public virtual void setColorRGBA(int i1, int i2, int i3, int i4)
		{
			if (!isColorDisabled)
			{
				if (i1 > 255)
				{
					i1 = 255;
				}

				if (i2 > 255)
				{
					i2 = 255;
				}

				if (i3 > 255)
				{
					i3 = 255;
				}

				if (i4 > 255)
				{
					i4 = 255;
				}

				if (i1 < 0)
				{
					i1 = 0;
				}

				if (i2 < 0)
				{
					i2 = 0;
				}

				if (i3 < 0)
				{
					i3 = 0;
				}

				if (i4 < 0)
				{
					i4 = 0;
				}

				hasColor = true;
				if (BitConverter.IsLittleEndian)
				{
					color = i4 << 24 | i3 << 16 | i2 << 8 | i1;
				}
				else
				{
					color = i1 << 24 | i2 << 16 | i3 << 8 | i4;
				}

			}
		}

		public virtual void AddVertexWithUV(double x, double y, double z, double uvX, double uvY)
		{
			SetTextureUV(uvX, uvY);
			AddVertex(x, y, z);
		}

		public virtual void AddVertex(double x, double y, double z)
		{
			++addedVertices;
            
            // Convert quads to triangles
            if (drawMode == 7 && addedVertices % 4 == 0) 
			{
				for (int faceIter = 0; faceIter < 2; ++faceIter)
				{
					int i8 = 8 * (3 - faceIter);

                    // Vertex
                    rawBuffer[rawBufferIndex + 0] = rawBuffer[rawBufferIndex - i8 + 0];
                    rawBuffer[rawBufferIndex + 1] = rawBuffer[rawBufferIndex - i8 + 1];
                    rawBuffer[rawBufferIndex + 2] = rawBuffer[rawBufferIndex - i8 + 2];

                    if (hasTexture)
					{
						rawBuffer[rawBufferIndex + 3] = rawBuffer[rawBufferIndex - i8 + 3];
						rawBuffer[rawBufferIndex + 4] = rawBuffer[rawBufferIndex - i8 + 4];
					}
                    
					// Color
                    rawBuffer[rawBufferIndex + 5] = rawBuffer[rawBufferIndex - i8 + 5];
                    
                    rawBuffer[rawBufferIndex + 6] = rawBuffer[rawBufferIndex - i8 + 6];

					// Brightness
                    rawBuffer[rawBufferIndex + 7] = rawBuffer[rawBufferIndex - i8 + 7];
                    
					++vertexCount;
					rawBufferIndex += VertexSize / sizeof(int);
				}
			}

			if (hasTexture)
			{
				rawBuffer[rawBufferIndex + 3] = JTypes.FloatToRawIntBits((float)textureU);
				rawBuffer[rawBufferIndex + 4] = JTypes.FloatToRawIntBits((float)textureV);
			}

            if (hasColor)
            {
                rawBuffer[rawBufferIndex + 5] = color;
            }
            else
            {
				IntByteUnion union = new() { byte0 = 255, byte1 = 255, byte2 = 255, byte3 = 255 };
                rawBuffer[rawBufferIndex + 5] = union.integer;
            }

            if (hasNormals)
            {
				rawBuffer[rawBufferIndex + 6] = normal;
            }
			else
			{
				if (Minecraft.renderPipeline != null)
				{
                    Vector3 currentNormal = Minecraft.renderPipeline.CurrentNormal;

					int normal = 0;
                    normal |= (int)(currentNormal.X * 127f) & 0xFF;
                    normal |= ((int)(currentNormal.Y * 127f) & 0xFF) << 8;
                    normal |= ((int)(currentNormal.Z * 127f) & 0xFF) << 16;

                    rawBuffer[rawBufferIndex + 6] = normal;
                }
            }

            if (hasBrightness)
			{
				rawBuffer[rawBufferIndex + 7] = brightness;
			}
            else
            {
                short brightX = 240;
                short brightY = 240;

				if (Minecraft.renderPipeline != null)
				{
					if (Minecraft.renderPipeline.LightmapCoords.X != 0)
					{
                        
					}

					brightX = (short)(Minecraft.renderPipeline.LightmapCoords.X);
					brightY = (short)(Minecraft.renderPipeline.LightmapCoords.Y);
				}

				IntFloatShortUnion union = new() { short0 = brightX, short1 = brightY };

                int brightnessVal = union.integer;

                rawBuffer[rawBufferIndex + 7] = brightnessVal;
            }



            rawBuffer[rawBufferIndex + 0] = JTypes.FloatToRawIntBits((float)(x + xOffset));
			rawBuffer[rawBufferIndex + 1] = JTypes.FloatToRawIntBits((float)(y + yOffset));
			rawBuffer[rawBufferIndex + 2] = JTypes.FloatToRawIntBits((float)(z + zOffset));
			rawBufferIndex += VertexSize / sizeof(int);
            
			++vertexCount;
		}

		public virtual int ColorOpaque_I
		{
			set
			{
				int i2 = value >> 16 & 255;
				int i3 = value >> 8 & 255;
				int i4 = value & 255;
				setColorOpaque(i2, i3, i4);
			}
		}

		public virtual void setColorRGBA_I(int i1, int i2)
		{
			int i3 = i1 >> 16 & 255;
			int i4 = i1 >> 8 & 255;
			int i5 = i1 & 255;
			setColorRGBA(i3, i4, i5, i2);
		}

		public virtual void disableColor()
		{
			isColorDisabled = true;
		}

		public virtual void SetNormal(float f1, float f2, float f3)
		{
			Vector3 vec3 = new(f1, f2, f3);
			vec3.Normalize();

            hasNormals = true;
			sbyte b4 = (sbyte)((vec3.X * 127.0F));
			sbyte b5 = (sbyte)((vec3.Y * 127.0F));
			sbyte b6 = (sbyte)((vec3.Z * 127.0F));

            IntSByteUnion intByteUnion = new() { byte0 = b4, byte1 = b5, byte2 = b6, byte3 = 0 };

            normal = intByteUnion.integer;
		}

		public virtual void setTranslation(double x, double y, double z)
		{
			xOffset = x;
			yOffset = y;
			zOffset = z;
		}

		public virtual void addTranslation(float x, float y, float z)
		{
			xOffset += x;
			yOffset += y;
			zOffset += z;
		}

        public virtual void addTranslation(double x, double y, double z)
        {
            xOffset += x;
            yOffset += y;
            zOffset += z;
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    struct IntFloatShortUnion
	{
		[FieldOffset(0)]
		public int integer;

        [FieldOffset(0)]
        public float single;

        [FieldOffset(0)]
        public byte byte0;

        [FieldOffset(1)]
        public byte byte1;

        [FieldOffset(2)]
        public byte byte2;

        [FieldOffset(3)]
        public byte byte3;

        [FieldOffset(0)]
        public short short0;

        [FieldOffset(2)]
        public short short1;
    }
}