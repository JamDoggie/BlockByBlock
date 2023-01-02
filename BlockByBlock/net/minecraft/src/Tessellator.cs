namespace net.minecraft.src
{
    using BlockByBlock.helpers;
	using BlockByBlock.net.minecraft.render;
	using com.sun.org.apache.xerces.@internal.impl.dv.xs;
	using java.lang;
	using javax.swing;
	using net.minecraft.client;
	using net.minecraft.client.entity.render;
	using OpenTK.Graphics.OpenGL;
	using OpenTK.Mathematics;
	using System.Runtime.InteropServices;

    public class Tessellator
	{
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
		public static readonly Tessellator instance = new(2097152);
		private bool isDrawing = false;
		private int[] vertexBuffers;
		private int vboIndex = 0;
		private int vboCount = 10;
		private int bufferSize;
		private IntPtr bufferPointer;
		public int VAO;
		private int stride = 64;

		public bool CurrentlyBuildingVBO { get; set; } = false;
        
		private Tessellator(int bufSize)
		{
			bufferSize = bufSize;
			byteBuffer = GLAllocation.createDirectByteBuffer(bufSize * 4);
            
			byte[] underlyingBuffer = byteBuffer.GetUnderlyingBuffer();
			bufferPointer = Marshal.AllocHGlobal(underlyingBuffer.Length);
			rawBuffer = new int[bufSize];

			// VBOs
			vertexBuffers = new int[vboCount];
			GL.GenBuffers(vertexBuffers.Length, vertexBuffers);

            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);
        }

        ~Tessellator()
        {
            Marshal.FreeHGlobal(bufferPointer);
        }

		private bool hasCopiedBuffer = false;
		private Random testRand = new();

		private bool test = false;
        
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
                    GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffers[vboIndex]);
                    GL.BufferData(BufferTarget.ArrayBuffer, (int)byteBuffer.getLimit(), byteBuffer.GetUnderlyingBuffer(), BufferUsageHint.StreamDraw);

					SetupVertexArrays();

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

		public virtual void Draw(VertexBuffer vbo)
		{
            uploadMatrixStacks();
            
            GL.VertexArrayVertexBuffer(VAO, 0, vbo.GLHandle, 0, stride);
            GL.VertexArrayVertexBuffer(VAO, 1, vbo.GLHandle, 12, stride);
            GL.VertexArrayVertexBuffer(VAO, 2, vbo.GLHandle, 20, stride);
            GL.VertexArrayVertexBuffer(VAO, 3, vbo.GLHandle, 36, stride);
            GL.VertexArrayVertexBuffer(VAO, 4, vbo.GLHandle, 48, stride);

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

        public void SetupVertexArrays()
        {
            // Vertex
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, stride, 0);

            // Texture Coords
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, stride, 12);

            // Color
            GL.EnableVertexAttribArray(2);
            GL.VertexAttribPointer(2, 4, VertexAttribPointerType.Float, false, stride, 20);

            // Normal
            GL.EnableVertexAttribArray(3);
            GL.VertexAttribPointer(3, 3, VertexAttribPointerType.Float, false, stride, 36);
            
            // Brightness
            GL.EnableVertexAttribArray(4);
            GL.VertexAttribPointer(4, 2, VertexAttribPointerType.Float, false, stride, 48);
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

        public virtual VertexBuffer BuildCurrentVBO()
		{
            byteBuffer.clear();
            byteBuffer.Put(rawBuffer, rawBufferIndex);
            byteBuffer.position(0);
            byteBuffer.limit(rawBufferIndex * 4);
            
            int bufferHandle = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, bufferHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, rawBufferIndex * 4, byteBuffer.GetUnderlyingBuffer(), BufferUsageHint.DynamicDraw);
            
            CurrentlyBuildingVBO = false;
            
			VertexBuffer vbo = new(rawBufferIndex * 4, vertexCount, bufferHandle, drawMode, hasBrightness, hasColor, hasTexture, hasNormals);

			reset();

            return vbo;
        }

		public virtual void setTextureUV(double d1, double d3)
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

		public virtual void setColorOpaque(int i1, int i2, int i3)
		{
			setColorRGBA(i1, i2, i3, 255);
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
			setTextureUV(uvX, uvY);
			addVertex(x, y, z);
		}
        
		public virtual void addVertex(double d1, double d3, double d5)
		{
			++addedVertices;
            
            // Convert quads to triangles
            if (drawMode == 7 && addedVertices % 4 == 0) 
			{
				for (int faceIter = 0; faceIter < 2; ++faceIter)
				{
					int i8 = 16 * (3 - faceIter);
					if (hasTexture)
					{
						rawBuffer[rawBufferIndex + 3] = rawBuffer[rawBufferIndex - i8 + 3];
						rawBuffer[rawBufferIndex + 4] = rawBuffer[rawBufferIndex - i8 + 4];
					}
                    
                    if (hasColor)
					{
						for (int i = 0; i < 4; i++)
						{
                            rawBuffer[rawBufferIndex + 5 + i] = rawBuffer[rawBufferIndex - i8 + 5 + i];
                        }
                    }
					else
					{
                        for (int i = 0; i < 4; i++)
						{
                            rawBuffer[rawBufferIndex + 5 + i] = JTypes.FloatToRawIntBits(1.0f);
                        }
                    }
                    
					if (hasNormals)
					{
                        rawBuffer[rawBufferIndex + 9] = rawBuffer[rawBufferIndex - i8 + 9];
                        rawBuffer[rawBufferIndex + 10] = rawBuffer[rawBufferIndex - i8 + 10];
                        rawBuffer[rawBufferIndex + 11] = rawBuffer[rawBufferIndex - i8 + 11];
                    }

					// Brightness
                    rawBuffer[rawBufferIndex + 12] = rawBuffer[rawBufferIndex - i8 + 12];
                    rawBuffer[rawBufferIndex + 13] = rawBuffer[rawBufferIndex - i8 + 13];

					// Vertex
                    rawBuffer[rawBufferIndex + 0] = rawBuffer[rawBufferIndex - i8 + 0];
					rawBuffer[rawBufferIndex + 1] = rawBuffer[rawBufferIndex - i8 + 1];
					rawBuffer[rawBufferIndex + 2] = rawBuffer[rawBufferIndex - i8 + 2];
					++vertexCount;
					rawBufferIndex += stride / sizeof(int);
				}
			}

			if (hasTexture)
			{
				rawBuffer[rawBufferIndex + 3] = JTypes.FloatToRawIntBits((float)textureU);
				rawBuffer[rawBufferIndex + 4] = JTypes.FloatToRawIntBits((float)textureV);
			}

            if (hasColor)
            {
                IntByteUnion intByteUnion = new() { integer = color };
                rawBuffer[rawBufferIndex + 5] = JTypes.FloatToRawIntBits(intByteUnion.byte0 / 255f);
                rawBuffer[rawBufferIndex + 6] = JTypes.FloatToRawIntBits(intByteUnion.byte1 / 255f);
                rawBuffer[rawBufferIndex + 7] = JTypes.FloatToRawIntBits(intByteUnion.byte2 / 255f);
                rawBuffer[rawBufferIndex + 8] = JTypes.FloatToRawIntBits(intByteUnion.byte3 / 255f);
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    rawBuffer[rawBufferIndex + 5 + i] = JTypes.FloatToRawIntBits(1.0f);
                }
            }

            if (hasNormals)
            {
                IntSByteUnion intByteUnion = new() { integer = normal };
                Vector3 currentNormal = new Vector3(intByteUnion.byte0 / 127f, intByteUnion.byte1 / 127f, intByteUnion.byte2 / 127f);
                
				rawBuffer[rawBufferIndex + 9] = JTypes.FloatToRawIntBits(currentNormal.X);
                rawBuffer[rawBufferIndex + 10] = JTypes.FloatToRawIntBits(currentNormal.Y);
                rawBuffer[rawBufferIndex + 11] = JTypes.FloatToRawIntBits(currentNormal.Z);
            }
			else
			{
				if (Minecraft.renderPipeline != null)
				{
                    Vector3 currentNormal = Minecraft.renderPipeline.CurrentNormal;

                    rawBuffer[rawBufferIndex + 9] = JTypes.FloatToRawIntBits(currentNormal.X);
                    rawBuffer[rawBufferIndex + 10] = JTypes.FloatToRawIntBits(currentNormal.Y);
                    rawBuffer[rawBufferIndex + 11] = JTypes.FloatToRawIntBits(currentNormal.Z);
                }
            }

            if (hasBrightness)
			{   
                float x = brightness % 65536f;
                float y = brightness / 65536f;

				x /= 16f;
				y /= 16f;

                rawBuffer[rawBufferIndex + 12] = JTypes.FloatToRawIntBits((x / (17F)) + 0.0625f);
                rawBuffer[rawBufferIndex + 13] = JTypes.FloatToRawIntBits((y / (17F)) + 0.0625f);
			}
            else
            {
				if (Minecraft.renderPipeline != null)
				{
                    rawBuffer[rawBufferIndex + 12] = JTypes.FloatToRawIntBits(Minecraft.renderPipeline.LightmapCoords.X);
                    rawBuffer[rawBufferIndex + 13] = JTypes.FloatToRawIntBits(Minecraft.renderPipeline.LightmapCoords.Y);
                }
                else
				{
                    rawBuffer[rawBufferIndex + 12] = JTypes.FloatToRawIntBits(1.0f - 0.0625f);
                    rawBuffer[rawBufferIndex + 13] = JTypes.FloatToRawIntBits(1.0f - 0.0625f);
                }
            }



            rawBuffer[rawBufferIndex + 0] = JTypes.FloatToRawIntBits((float)(d1 + xOffset));
			rawBuffer[rawBufferIndex + 1] = JTypes.FloatToRawIntBits((float)(d3 + yOffset));
			rawBuffer[rawBufferIndex + 2] = JTypes.FloatToRawIntBits((float)(d5 + zOffset));
			rawBufferIndex += stride / sizeof(int);
			++vertexCount;

			// This should never happen, and is not a good way to deal with this case anyway because now the tessellator
			// can be set to start building a VBO instead of starting to draw.
            /*
			if (vertexCount % 4 == 0 && rawBufferIndex >= bufferSize - stride)
			{
				DrawImmediate();
				isDrawing = true;
			}
			*/
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

		public virtual void setTranslation(double d1, double d3, double d5)
		{
			xOffset = d1;
			yOffset = d3;
			zOffset = d5;
		}

		public virtual void addTranslation(float f1, float f2, float f3)
		{
			xOffset += (double)f1;
			yOffset += (double)f2;
			zOffset += (double)f3;
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