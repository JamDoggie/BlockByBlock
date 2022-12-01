namespace net.minecraft.src
{
    using BlockByBlock.helpers;
	using BlockByBlock.net.minecraft.render;
	using net.minecraft.client;
    using OpenTK.Graphics.OpenGL;
	using OpenTK.Mathematics;
	using System.Runtime.InteropServices;

    public class Tessellator
	{
		private static bool convertQuadsToTriangles { get; set; } = false;
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
		private int VAO;

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

		public unsafe virtual int draw()
		{
			if (CurrentlyBuildingVBO)
			{
				throw new InvalidOperationException("Cannot draw while builing a VBO!");
			}

			if (!isDrawing)
			{
				throw new System.InvalidOperationException("Not tesselating!");
			}
			else
			{
				isDrawing = false;
				if (vertexCount > 0)
				{
                    byteBuffer.clear();
                    byteBuffer.Put(rawBuffer, rawBufferIndex);
                    byteBuffer.position(0);
                    byteBuffer.limit(rawBufferIndex * 4);

                    vboIndex = (vboIndex + 1) % vboCount;
                    GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffers[vboIndex]);
                    GL.BufferData(BufferTarget.ArrayBuffer, (int)byteBuffer.getLimit(), byteBuffer.GetUnderlyingBuffer(), BufferUsageHint.StreamDraw);

					setupVertexArrays(hasColor, hasTexture, hasBrightness, hasNormals);

                    if (drawMode == 7 && convertQuadsToTriangles)
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

		public virtual void drawVBO(VertexBuffer vbo)
		{
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo.GLHandle);

			setupVertexArrays(vbo.HasColor, vbo.HasTexture, vbo.HasBrightness, vbo.HasNormal);

            if (drawMode == 7 && convertQuadsToTriangles)
            {
                GL.DrawArrays(PrimitiveType.Triangles, 0, vbo.VertexCount);
            }
            else
            {
                GL.DrawArrays((PrimitiveType)drawMode, 0, vbo.VertexCount);
            }
        }

        private void setupVertexArrays(bool hasColor, bool hasTexture, bool hasBrightness, bool hasNormals)
        {
            // Vertex
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 32, 0);
            GL.EnableVertexAttribArray(0);

            // Texture Coords
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 32, 12);
            GL.EnableVertexAttribArray(1);

            // Color
            GL.VertexAttribPointer(2, 1, VertexAttribPointerType.Float, false, 32, 20);
            GL.EnableVertexAttribArray(2);

            // Normal
            GL.VertexAttribPointer(3, 1, VertexAttribPointerType.Float, false, 32, 24);
            GL.EnableVertexAttribArray(3);

            // Brightness
            GL.VertexAttribPointer(4, 1, VertexAttribPointerType.Float, false, 32, 28);
            GL.EnableVertexAttribArray(4);
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
				throw new System.InvalidOperationException("Already tesselating!");
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
            
			VertexBuffer vbo = new(rawBufferIndex * 4, vertexCount, bufferHandle, hasBrightness, hasColor, hasTexture, hasNormals);

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

		public virtual void addVertexWithUV(double d1, double d3, double d5, double d7, double d9)
		{
			setTextureUV(d7, d9);
			addVertex(d1, d3, d5);
		}

		public virtual void addVertex(double d1, double d3, double d5)
		{
			++addedVertices;
			if (drawMode == 7 && convertQuadsToTriangles && addedVertices % 4 == 0)
			{
				for (int i7 = 0; i7 < 2; ++i7)
				{
					int i8 = 8 * (3 - i7);
					if (hasTexture)
					{
						rawBuffer[rawBufferIndex + 3] = rawBuffer[rawBufferIndex - i8 + 3];
						rawBuffer[rawBufferIndex + 4] = rawBuffer[rawBufferIndex - i8 + 4];
					}

					if (hasBrightness)
					{
						rawBuffer[rawBufferIndex + 7] = rawBuffer[rawBufferIndex - i8 + 7];
					}
					else
                    {
                        rawBuffer[rawBufferIndex + 7] = 0x20;
                    }

                    if (hasColor)
					{
						rawBuffer[rawBufferIndex + 5] = rawBuffer[rawBufferIndex - i8 + 5];
					}
					else
					{
                        rawBuffer[rawBufferIndex + 5] = 0xFFFFFF;
                    }

					rawBuffer[rawBufferIndex + 0] = rawBuffer[rawBufferIndex - i8 + 0];
					rawBuffer[rawBufferIndex + 1] = rawBuffer[rawBufferIndex - i8 + 1];
					rawBuffer[rawBufferIndex + 2] = rawBuffer[rawBufferIndex - i8 + 2];
					++vertexCount;
					rawBufferIndex += 8;
				}
			}

			if (hasTexture)
			{
				rawBuffer[rawBufferIndex + 3] = JTypes.FloatToRawIntBits((float)textureU);
				rawBuffer[rawBufferIndex + 4] = JTypes.FloatToRawIntBits((float)textureV);
			}

			if (hasBrightness)
			{
				rawBuffer[rawBufferIndex + 7] = brightness;
			}
            else
            {
                rawBuffer[rawBufferIndex + 7] = 0x20;
            }

            if (hasColor)
			{
				rawBuffer[rawBufferIndex + 5] = color;
			}
            else
            {
                rawBuffer[rawBufferIndex + 5] = 0xFFFFFF;
            }

            if (hasNormals)
			{
				rawBuffer[rawBufferIndex + 6] = normal;
			}

			rawBuffer[rawBufferIndex + 0] = JTypes.FloatToRawIntBits((float)(d1 + xOffset));
			rawBuffer[rawBufferIndex + 1] = JTypes.FloatToRawIntBits((float)(d3 + yOffset));
			rawBuffer[rawBufferIndex + 2] = JTypes.FloatToRawIntBits((float)(d5 + zOffset));
			rawBufferIndex += 8;
			++vertexCount;
			if (vertexCount % 4 == 0 && rawBufferIndex >= bufferSize - 32)
			{
				draw();
				isDrawing = true;
			}

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

		public virtual void setNormal(float f1, float f2, float f3)
		{
			hasNormals = true;
			sbyte b4 = (sbyte)((int)(f1 * 127.0F));
			sbyte b5 = (sbyte)((int)(f2 * 127.0F));
			sbyte b6 = (sbyte)((int)(f3 * 127.0F));
			normal = b4 & 255 | (b5 & 255) << 8 | (b6 & 255) << 16;
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

}