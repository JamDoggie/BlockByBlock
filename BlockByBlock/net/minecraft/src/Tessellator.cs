namespace net.minecraft.src
{

	using ARBVertexBufferObject = org.lwjgl.opengl.ARBVertexBufferObject;
	using GL11 = org.lwjgl.opengl.GL11;
	using GLContext = org.lwjgl.opengl.GLContext;
	using GL15 = org.lwjgl.opengl.GL15;
    using BlockByBlock.helpers;

    // PORTING TODO: OpenGL code, and lots of it.

    public class Tessellator
	{
		private static bool convertQuadsToTriangles = false;
		private static bool tryVBO = false;
		private ByteBuffer byteBuffer;
		private IntBuffer intBuffer;
		private FloatBuffer floatBuffer;
		private ShortBuffer shortBuffer;
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
		public static readonly Tessellator instance = new Tessellator(2097152);
		private bool isDrawing = false;
		private bool useVBO = false;
		private IntBuffer vertexBuffers;
		private int vboIndex = 0;
		private int vboCount = 10;
		private int bufferSize;

		private Tessellator(int i1)
		{
			bufferSize = i1;
			byteBuffer = GLAllocation.createDirectByteBuffer(i1 * 4);
			intBuffer = byteBuffer.asIntBuffer();
			floatBuffer = byteBuffer.asFloatBuffer();
			shortBuffer = byteBuffer.asShortBuffer();
			rawBuffer = new int[i1];
			useVBO = tryVBO && GLContext.getCapabilities().GL_ARB_vertex_buffer_object;
			if (useVBO)
			{
				vertexBuffers = GLAllocation.createDirectIntBuffer(vboCount);
				ARBVertexBufferObject.glGenBuffersARB(vertexBuffers);
			}

		}

		public virtual int draw()
		{
			if (!isDrawing)
			{
				throw new System.InvalidOperationException("Not tesselating!");
			}
			else
			{
				isDrawing = false;
				if (vertexCount > 0)
				{
					intBuffer.clear();
					intBuffer.Put(rawBuffer, rawBufferIndex);
					byteBuffer.position(0);
					byteBuffer.limit(rawBufferIndex * 4);
					if (useVBO)
					{
						vboIndex = (vboIndex + 1) % vboCount;
						ARBVertexBufferObject.glBindBufferARB(GL15.GL_ARRAY_BUFFER, vertexBuffers.get(vboIndex));
						ARBVertexBufferObject.glBufferDataARB(GL15.GL_ARRAY_BUFFER, byteBuffer, GL15.GL_STREAM_DRAW);
					}

					if (hasTexture)
					{
						if (useVBO)
						{
							GL11.glTexCoordPointer(2, GL11.GL_FLOAT, 32, 12L);
						}
						else
						{
							floatBuffer.position(3);
							GL11.glTexCoordPointer(2, 32, floatBuffer);
						}

						GL11.glEnableClientState(GL11.GL_TEXTURE_COORD_ARRAY);
					}

					if (hasBrightness)
					{
						OpenGlHelper.ClientActiveTexture = OpenGlHelper.lightmapTexUnit;
						if (useVBO)
						{
							GL11.glTexCoordPointer(2, GL11.GL_SHORT, 32, 28L);
						}
						else
						{
							shortBuffer.position(14);
							GL11.glTexCoordPointer(2, 32, shortBuffer);
						}

						GL11.glEnableClientState(GL11.GL_TEXTURE_COORD_ARRAY);
						OpenGlHelper.ClientActiveTexture = OpenGlHelper.defaultTexUnit;
					}

					if (hasColor)
					{
						if (useVBO)
						{
							GL11.glColorPointer(4, GL11.GL_UNSIGNED_BYTE, 32, 20L);
						}
						else
						{
							byteBuffer.position(20);
							GL11.glColorPointer(4, true, 32, byteBuffer);
						}

						GL11.glEnableClientState(GL11.GL_COLOR_ARRAY);
					}

					if (hasNormals)
					{
						if (useVBO)
						{
							GL11.glNormalPointer(GL11.GL_UNSIGNED_BYTE, 32, 24L);
						}
						else
						{
							byteBuffer.position(24);
							GL11.glNormalPointer(32, byteBuffer);
						}

						GL11.glEnableClientState(GL11.GL_NORMAL_ARRAY);
					}

					if (useVBO)
					{
						GL11.glVertexPointer(3, GL11.GL_FLOAT, 32, 0L);
					}
					else
					{
						floatBuffer.position(0);
						GL11.glVertexPointer(3, 32, floatBuffer);
					}

					GL11.glEnableClientState(GL11.GL_VERTEX_ARRAY);
					if (drawMode == 7 && convertQuadsToTriangles)
					{
						GL11.glDrawArrays(GL11.GL_TRIANGLES, GL11.GL_POINTS, vertexCount);
					}
					else
					{
						GL11.glDrawArrays(drawMode, GL11.GL_POINTS, vertexCount);
					}

					GL11.glDisableClientState(GL11.GL_VERTEX_ARRAY);
					if (hasTexture)
					{
						GL11.glDisableClientState(GL11.GL_TEXTURE_COORD_ARRAY);
					}

					if (hasBrightness)
					{
						OpenGlHelper.ClientActiveTexture = OpenGlHelper.lightmapTexUnit;
						GL11.glDisableClientState(GL11.GL_TEXTURE_COORD_ARRAY);
						OpenGlHelper.ClientActiveTexture = OpenGlHelper.defaultTexUnit;
					}

					if (hasColor)
					{
						GL11.glDisableClientState(GL11.GL_COLOR_ARRAY);
					}

					if (hasNormals)
					{
						GL11.glDisableClientState(GL11.GL_NORMAL_ARRAY);
					}
				}

				int i1 = rawBufferIndex * 4;
				reset();
				return i1;
			}
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

		public virtual void startDrawing(int i1)
		{
			if (isDrawing)
			{
				throw new System.InvalidOperationException("Already tesselating!");
			}
			else
			{
				isDrawing = true;
				reset();
				drawMode = i1;
				hasNormals = false;
				hasColor = false;
				hasTexture = false;
				hasBrightness = false;
				isColorDisabled = false;
			}
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

					if (hasColor)
					{
						rawBuffer[rawBufferIndex + 5] = rawBuffer[rawBufferIndex - i8 + 5];
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

			if (hasColor)
			{
				rawBuffer[rawBufferIndex + 5] = color;
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