using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;
using OpenTK.Compute.OpenCL;

namespace net.minecraft.src
{

	public class TextureManager
	{
		public static bool useMipmaps = true;
		private Hashtable textureMap = new Hashtable();
		private Hashtable textureContentsMap = new Hashtable();
		private IntHashMap textureNameToImageMap = new IntHashMap();
		private ByteBuffer singleIntBuffer = GLAllocation.createDirectIntBuffer(1);
		//private ByteBuffer imageData = GLAllocation.createDirectByteBuffer(16777216);
		private System.Collections.IList textureList = new ArrayList();
		private System.Collections.IDictionary urlToImageDataMap = new Hashtable();
		private GameSettings options;
		public bool clampTexture = false;
		public bool blurTexture = false;
		private TexturePackList texturePack;
        private Image<Bgra32> missingTextureImage = new(64, 64, Color.Black);
        private int field_48512_n = 16;

		public TextureManager(TexturePackList texturePackList1, GameSettings gameSettings2)
		{
			this.texturePack = texturePackList1;
			this.options = gameSettings2;

			missingTextureImage.Mutate(x => x.Fill(Color.Black, new RectangleF(0, 0, 32, 32))
											.Fill(Color.Black, new RectangleF(32, 32, 32, 32))
											.Fill(Rgba32.ParseHex("#ff007f"), new RectangleF(32, 0, 32, 32))
											.Fill(Rgba32.ParseHex("#ff007f"), new RectangleF(0, 32, 32, 32)));
		}

		private static int[] texFilterLinear = new int[] { (int)TextureMinFilter.Linear };
		private static int[] texWrapClamp = new int[] { (int)TextureWrapMode.Clamp };
		private static int[] texWrapRepeat = new int[] { (int)TextureWrapMode.Repeat };

		public static int[] TextureFilterLinear => texFilterLinear;

		public static int[] TextureWrapClamp => texWrapClamp;

		public static int[] TextureWrapRepeat => texWrapRepeat;

		public virtual int[] getTextureContents(string string1)
		{
			TexturePackBase texturePackBase2 = this.texturePack.selectedTexturePack;
			int[] i3 = (int[])this.textureContentsMap[string1];
			if (i3 != null)
			{
				return i3;
			}
			else
			{
				try
				{
					object object6 = null;
					if (string1.StartsWith("##", StringComparison.Ordinal))
					{
						i3 = this.getImageContentsAndAllocate(this.unwrapImageByColumns(this.readTextureImage(texturePackBase2.getResourceAsStream(string1.Substring(2)))));
					}
					else if (string1.StartsWith("%clamp%", StringComparison.Ordinal))
					{
						this.clampTexture = true;
						i3 = this.getImageContentsAndAllocate(this.readTextureImage(texturePackBase2.getResourceAsStream(string1.Substring(7))));
						this.clampTexture = false;
					}
					else if (string1.StartsWith("%blur%", StringComparison.Ordinal))
					{
						this.blurTexture = true;
						this.clampTexture = true;
						i3 = this.getImageContentsAndAllocate(this.readTextureImage(texturePackBase2.getResourceAsStream(string1.Substring(6))));
						this.clampTexture = false;
						this.blurTexture = false;
					}
					else
					{
						Stream inputStream7 = texturePackBase2.getResourceAsStream(string1);
						if (inputStream7 == null)
						{
							i3 = this.getImageContentsAndAllocate(this.missingTextureImage);
						}
						else
						{
							i3 = this.getImageContentsAndAllocate(this.readTextureImage(inputStream7));
						}
					}

					this.textureContentsMap[string1] = i3;
					return i3;
				}
				catch (IOException iOException5)
				{
					Console.WriteLine(iOException5.ToString());
					Console.Write(iOException5.StackTrace);
					int[] i4 = this.getImageContentsAndAllocate(this.missingTextureImage);
					this.textureContentsMap[string1] = i4;
					return i4;
				}
			}
		}

		private int[] getImageContentsAndAllocate(Image<Bgra32> bufferedImage1)
		{
			int i2 = bufferedImage1.Width;
			int i3 = bufferedImage1.Height;
			byte[] i4 = new byte[i2 * i3 * 4];
			bufferedImage1.CopyPixelDataTo(i4);
			int[] array = new int[i2 * i3];

            System.Buffer.BlockCopy(i4, 0, array, 0, i4.Length);

            return array;
		}

		private int[] getImageContents(Image<Bgra32> bufferedImage1, int[] i2)
		{
			int i3 = bufferedImage1.Width;
			int i4 = bufferedImage1.Height;
			FillIntBufferWithImage(bufferedImage1, i2);
			return i2;
		}

		public virtual int getTexture(string string1)
		{
			TexturePackBase texturePackBase2 = this.texturePack.selectedTexturePack;
			int? integer3 = (int?)this.textureMap[string1];
			if (integer3 != null)
			{
				return integer3.Value;
			}
			else
			{
				try
				{
					this.singleIntBuffer.clear().position(0);
					GLAllocation.generateTextureNames(this.singleIntBuffer);
					int i6 = this.singleIntBuffer.getInt(0);
					if (string1.StartsWith("##", StringComparison.Ordinal))
					{
						this.setupTexture(this.unwrapImageByColumns(this.readTextureImage(texturePackBase2.getResourceAsStream(string1.Substring(2)))), i6);
					}
					else if (string1.StartsWith("%clamp%", StringComparison.Ordinal))
					{
						this.clampTexture = true;
						this.setupTexture(this.readTextureImage(texturePackBase2.getResourceAsStream(string1.Substring(7))), i6);
						this.clampTexture = false;
					}
					else if (string1.StartsWith("%blur%", StringComparison.Ordinal))
					{
						this.blurTexture = true;
						this.setupTexture(this.readTextureImage(texturePackBase2.getResourceAsStream(string1.Substring(6))), i6);
						this.blurTexture = false;
					}
					else if (string1.StartsWith("%blurclamp%", StringComparison.Ordinal))
					{
						this.blurTexture = true;
						this.clampTexture = true;
						this.setupTexture(this.readTextureImage(texturePackBase2.getResourceAsStream(string1.Substring(11))), i6);
						this.blurTexture = false;
						this.clampTexture = false;
					}
					else
					{
						Stream inputStream7 = texturePackBase2.getResourceAsStream(string1);
						if (inputStream7 == null)
						{
							this.setupTexture(this.missingTextureImage, i6);
						}
						else
						{
							this.setupTexture(this.readTextureImage(inputStream7), i6);
						}
					}

					this.textureMap[string1] = i6;
					return i6;
				}
				catch (Exception exception5)
				{
					Console.WriteLine(exception5.ToString());
					Console.Write(exception5.StackTrace);
					GLAllocation.generateTextureNames(this.singleIntBuffer);
					int i4 = this.singleIntBuffer.getInt(0);
					this.setupTexture(this.missingTextureImage, i4);
					this.textureMap[string1] = i4;
					return i4;
				}
			}
		}

		private Image<Bgra32> unwrapImageByColumns(Image<Bgra32> bufferedImage1)
		{
			int i2 = bufferedImage1.Width / 16;
			Image<Bgra32> bufferedImage3 = new(16, bufferedImage1.Height * i2);
			
			for (int i5 = 0; i5 < i2; ++i5)
			{
				bufferedImage3.Mutate(x => x.DrawImage(bufferedImage1, new Point(-i5 * 16, i5 * bufferedImage1.Height), 1.0f));
			}
            
			return bufferedImage3;
		}

		public virtual int allocateAndSetupTexture(Image<Bgra32> bufferedImage1)
		{
			singleIntBuffer.clear().position(0);
			GLAllocation.generateTextureNames(singleIntBuffer);
			int i2 = singleIntBuffer.getInt(0);
			setupTexture(bufferedImage1, i2);
			textureNameToImageMap.addKey(i2, bufferedImage1);
			return i2;
		}

		public virtual void setupTexture(Image<Bgra32> bufferedImage1, int i2)
		{
			GL.BindTexture(TextureTarget.Texture2D, i2);
			if (useMipmaps)
			{
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, new int[] { (int)TextureMinFilter.NearestMipmapLinear });
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, new int[] { (int)TextureMagFilter.Nearest });

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinLod, 0);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLod, 4);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureLodBias, 0.0f);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBaseLevel, 0);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, 4);
            }
			else
			{
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, new int[] { (int)TextureMinFilter.Nearest });
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, new int[] { (int)TextureMagFilter.Nearest });
			}

			if (blurTexture)
			{
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, new int[] { (int)TextureMinFilter.Linear });
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, new int[] { (int)TextureMagFilter.Linear });
			}

			if (clampTexture)
			{
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, new int[] { (int)TextureWrapMode.Clamp });
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, new int[] { (int)TextureWrapMode.Clamp });
			}
			else
			{
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, new int[] { (int)TextureWrapMode.Repeat });
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, new int[] { (int)TextureWrapMode.Repeat });
			}

			int width = bufferedImage1.Width;
			int height = bufferedImage1.Height;

            // If width /= height, then we need to resize the image to a square
            if (width != height)
            {
                int size = Math.Max(width, height);
                bufferedImage1.Mutate(x => x.Resize(size, size, new SixLabors.ImageSharp.Processing.Processors.Transforms.NearestNeighborResampler()));

                width = size;
                height = size;
            }

            int[] imageDataInts = new int[width * height];
			byte[] imageDataBytes = new byte[width * height * sizeof(int)];
			FillIntBufferWithImage(bufferedImage1, imageDataInts);

			int i8;
			int i9;
			int i10;
			int x;
			int y;
			int i13;
			int i14;
			for (int i = 0; i < imageDataInts.Length; ++i)
			{
				i8 = imageDataInts[i] >> 24 & 255;
				i9 = imageDataInts[i] >> 16 & 255;
				i10 = imageDataInts[i] >> 8 & 255;
				x = imageDataInts[i] & 255;

				byte colorR = (byte)(i9 & 255);
                byte colorG = (byte)(i10 & 255);
                byte colorB = (byte)(x & 255);

                byte alpha = (byte)(i8 & 255);
                float alphaFloat = alpha / 255.0f;
                
                colorR = (byte)(colorR);
                colorG = (byte)(colorG);
                colorB = (byte)(colorB);

                imageDataBytes[i * sizeof(int) + 0] = colorR;
				imageDataBytes[i * sizeof(int) + 1] = colorG;
				imageDataBytes[i * sizeof(int) + 2] = colorB;
                imageDataBytes[i * sizeof(int) + 3] = alpha;
			}

            // Apply a solidify filter around fully transparent edges to prevent black fringing around the mipmaps.
            byte[] solidifiedImageBytes = new byte[imageDataBytes.Length];
            Array.Copy(imageDataBytes, solidifiedImageBytes, imageDataBytes.Length);

            for (int i = 0; i < imageDataBytes.Length / sizeof(int); i++)
            {
                byte colorR = imageDataBytes[i * sizeof(int) + 0];
                byte colorG = imageDataBytes[i * sizeof(int) + 1];
                byte colorB = imageDataBytes[i * sizeof(int) + 2];
                byte alpha = imageDataBytes[i * sizeof(int) + 3];

                int currentPixelX = i % width;
                int currentPixelY = i / width;


                if (alpha > 0)
                {
                    // Up pixel
                    if (currentPixelY - 1 >= 0)
                    {
                        int pixelIndex = (currentPixelY - 1) * width + currentPixelX;

                        byte alpha2 = imageDataBytes[pixelIndex * sizeof(int) + 3];

                        if (alpha2 == 0)
                        {
                            solidifiedImageBytes[pixelIndex * sizeof(int) + 0] = colorR;
                            solidifiedImageBytes[pixelIndex * sizeof(int) + 1] = colorG;
                            solidifiedImageBytes[pixelIndex * sizeof(int) + 2] = colorB;
                        }
                    }

                    // Down pixel
                    if (currentPixelY + 1 < height)
                    {
                        int pixelIndex = (currentPixelY + 1) * width + currentPixelX;

                        byte alpha2 = imageDataBytes[pixelIndex * sizeof(int) + 3];

                        if (alpha2 == 0)
                        {
                            solidifiedImageBytes[pixelIndex * sizeof(int) + 0] = colorR;
                            solidifiedImageBytes[pixelIndex * sizeof(int) + 1] = colorG;
                            solidifiedImageBytes[pixelIndex * sizeof(int) + 2] = colorB;
                        }
                    }

                    // Left pixel
                    if (currentPixelX - 1 >= 0)
                    {
                        int pixelIndex = currentPixelY * width + (currentPixelX - 1);

                        byte alpha2 = imageDataBytes[pixelIndex * sizeof(int) + 3];

                        if (alpha2 == 0)
                        {
                            solidifiedImageBytes[pixelIndex * sizeof(int) + 0] = colorR;
                            solidifiedImageBytes[pixelIndex * sizeof(int) + 1] = colorG;
                            solidifiedImageBytes[pixelIndex * sizeof(int) + 2] = colorB;
                        }
                    }

                    // Right pixel
                    if (currentPixelX + 1 < width)
                    {
                        int pixelIndex = currentPixelY * width + (currentPixelX + 1);

                        byte alpha2 = imageDataBytes[pixelIndex * sizeof(int) + 3];

                        if (alpha2 == 0)
                        {
                            solidifiedImageBytes[pixelIndex * sizeof(int) + 0] = colorR;
                            solidifiedImageBytes[pixelIndex * sizeof(int) + 1] = colorG;
                            solidifiedImageBytes[pixelIndex * sizeof(int) + 2] = colorB;
                        }
                    }
                }
            }

            imageDataBytes = solidifiedImageBytes;

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, imageDataBytes);

			if (useMipmaps)
			{
                // Generate, allocate and upload our own mipmaps.
                int level = 0;

				int width2 = width;
				int height2 = height;

				// Get the average color of the image in case we need it to fill in areas around the alpha.
                int avgTexColorR = 0;
                int avgTexColorG = 0;
                int avgTexColorB = 0;

                int numColors = 0;

                for (int imageByteIndex = 0; imageByteIndex < imageDataBytes.Length / sizeof(int); imageByteIndex++)
                {
                    int pixelAlpha = imageDataBytes[imageByteIndex * sizeof(int) + 3];

                    if (pixelAlpha > 0)
                    {
                        avgTexColorR += imageDataBytes[imageByteIndex * sizeof(int) + 0];
                        avgTexColorG += imageDataBytes[imageByteIndex * sizeof(int) + 1];
                        avgTexColorB += imageDataBytes[imageByteIndex * sizeof(int) + 2];

                        numColors++;
                    }
                }

                if (numColors > 0)
                {
                    avgTexColorR /= numColors;
                    avgTexColorG /= numColors;
                    avgTexColorB /= numColors;
                }

                while (width2 > 1 || height2 > 1)
				{
					int newWidth = Math.Max(width2 / 2, 1);
					int newHeight = Math.Max(height2 / 2, 1);

					byte[] newImageDataBytes = new byte[newWidth * newHeight * sizeof(int)];
                    
                    for (int y2 = 0; y2 < newHeight; y2++)
					{
						for (int x2 = 0; x2 < newWidth; x2++)
						{
                            int ii1 = (2 * x2 + 0 + (2 * y2 + 0) * width2) * sizeof(int);
                            int ii2 = (2 * x2 + 1 + (2 * y2 + 0) * width2) * sizeof(int);
                            int ii3 = (2 * x2 + 1 + (2 * y2 + 1) * width2) * sizeof(int);
                            int ii4 = (2 * x2 + 0 + (2 * y2 + 1) * width2) * sizeof(int);

                            byte r1 = imageDataBytes[ii1 + 0];
                            byte g1 = imageDataBytes[ii1 + 1];
                            byte b1 = imageDataBytes[ii1 + 2];
                            byte a1 = imageDataBytes[ii1 + 3];

                            byte r2 = imageDataBytes[ii2 + 0];
                            byte g2 = imageDataBytes[ii2 + 1];
                            byte b2 = imageDataBytes[ii2 + 2];
                            byte a2 = imageDataBytes[ii2 + 3];

							if (a2 == 0)
							{
								r2 = r1;
                                g2 = g1;
                                b2 = b1;
                            }

                            byte r3 = imageDataBytes[ii3 + 0];
                            byte g3 = imageDataBytes[ii3 + 1];
                            byte b3 = imageDataBytes[ii3 + 2];
                            byte a3 = imageDataBytes[ii3 + 3];

							if (a3 == 0)
							{
								r3 = r2;
								g3 = g2;
								b3 = b2;
							}

							byte r4 = imageDataBytes[ii4 + 0];
                            byte g4 = imageDataBytes[ii4 + 1];
                            byte b4 = imageDataBytes[ii4 + 2];
                            byte a4 = imageDataBytes[ii4 + 3];

							if (a4 == 0)
							{
                                r4 = r3;
                                g4 = g3;
                                b4 = b3;
                            }

                            byte r = (byte)((r1 + r2 + r3 + r4) / 4);
                            byte g = (byte)((g1 + g2 + g3 + g4) / 4);
                            byte b = (byte)((b1 + b2 + b3 + b4) / 4);
                            byte a = (byte)((a1 + a2 + a3 + a4) / 4);

                            int i5 = (x2 + y2 * newWidth) * sizeof(int);

                            newImageDataBytes[i5 + 0] = r;
                            newImageDataBytes[i5 + 1] = g;
                            newImageDataBytes[i5 + 2] = b;
                            newImageDataBytes[i5 + 3] = a;
                        }
					}

                    GL.TexImage2D(TextureTarget.Texture2D, ++level, PixelInternalFormat.Rgba, newWidth, newHeight, 0, PixelFormat.Rgba, PixelType.UnsignedByte, newImageDataBytes);

                    width2 = newWidth;
                    height2 = newHeight;

                    imageDataBytes = newImageDataBytes;
                }
			}
        }

		byte[] rawByteImageData;

        public virtual void createTextureFromBytes(int[] i1, int i2, int i3, int i4)
		{
			Profiler.startSection("opengl_attributes");
            GL.BindTexture(TextureTarget.Texture2D, i4);

            if (useMipmaps)
			{
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, new int[] { (int)TextureMinFilter.NearestMipmapLinear });
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, new int[] { (int)TextureMagFilter.Nearest });

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBaseLevel, 0);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, 4);
            }
			else
			{
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, new int[] { (int)TextureMinFilter.Nearest });
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, new int[] { (int)TextureMagFilter.Nearest });
			}

			if (blurTexture)
			{
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, new int[] { (int)TextureMinFilter.Linear });
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, new int[] { (int)TextureMagFilter.Linear });
			}

			if (clampTexture)
			{
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, new int[] { (int)TextureWrapMode.Clamp });
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, new int[] { (int)TextureWrapMode.Clamp });
			}
			else
			{
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, new int[] { (int)TextureWrapMode.Repeat });
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, new int[] { (int)TextureWrapMode.Repeat });
			}
			Profiler.endStartSection("populate_arrays");
            
            if (rawByteImageData == null || rawByteImageData.Length < i2 * i3 * sizeof(int))
			{
                rawByteImageData = new byte[i2 * i3 * 4];
            }

			Array.Clear(rawByteImageData);
			

			for (int i6 = 0; i6 < i1.Length; ++i6)
			{
				int i7 = i1[i6] >> 24 & 255;
				int i8 = i1[i6] >> 16 & 255;
				int i9 = i1[i6] >> 8 & 255;
				int i10 = i1[i6] & 255;

				rawByteImageData[i6 * 4 + 0] = (byte)(i8 & 255);
				rawByteImageData[i6 * 4 + 1] = (byte)(i9 & 255);
				rawByteImageData[i6 * 4 + 2] = (byte)(i10 & 255);
				rawByteImageData[i6 * 4 + 3] = (byte)(i7 & 255);
			}
			Profiler.endStartSection("upload_texture");
            GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, i2, i3, PixelFormat.Rgba, PixelType.UnsignedInt8888Reversed, rawByteImageData);
			Profiler.endSection();
			
        }

		int[] singleIntCache = new int[1];

		public virtual void deleteTexture(int i1)
		{
			textureNameToImageMap.removeObject(i1);
			singleIntBuffer.clear().position(0);
			singleIntBuffer.putInt(i1);
			singleIntBuffer.flip();
            singleIntCache[0] = i1;
			GL.DeleteTextures(1, singleIntCache);
		}

		public virtual int getTextureForDownloadableImage(string string1, string string2)
		{
			ThreadDownloadImageData? threadDownloadImageData3 = null;
            
			if (string1 != null && urlToImageDataMap.Contains(string1))
				threadDownloadImageData3 = (ThreadDownloadImageData?)this.urlToImageDataMap[string1];

			if (threadDownloadImageData3 != null && threadDownloadImageData3.image != null && !threadDownloadImageData3.textureSetupComplete)
			{
				if (threadDownloadImageData3.textureName < 0)
				{
					threadDownloadImageData3.textureName = this.allocateAndSetupTexture(threadDownloadImageData3.image);
				}
				else
				{
					this.setupTexture(threadDownloadImageData3.image, threadDownloadImageData3.textureName);
				}

				threadDownloadImageData3.textureSetupComplete = true;
			}

			return threadDownloadImageData3 != null && threadDownloadImageData3.textureName >= 0 ? threadDownloadImageData3.textureName : (string.ReferenceEquals(string2, null) ? -1 : this.getTexture(string2));
		}

		public virtual ThreadDownloadImageData obtainImageData(string string1, ImageBuffer imageBuffer2)
		{
			ThreadDownloadImageData threadDownloadImageData3 = (ThreadDownloadImageData)this.urlToImageDataMap[string1];
			if (threadDownloadImageData3 == null)
			{
				this.urlToImageDataMap[string1] = new ThreadDownloadImageData(string1, imageBuffer2);
			}
			else
			{
				++threadDownloadImageData3.referenceCount;
			}

			return threadDownloadImageData3;
		}

		public virtual void releaseImageData(string string1)
		{
			ThreadDownloadImageData threadDownloadImageData2 = (ThreadDownloadImageData)this.urlToImageDataMap[string1];
			if (threadDownloadImageData2 != null)
			{
				--threadDownloadImageData2.referenceCount;
				if (threadDownloadImageData2.referenceCount == 0)
				{
					if (threadDownloadImageData2.textureName >= 0)
					{
						this.deleteTexture(threadDownloadImageData2.textureName);
					}

					this.urlToImageDataMap.Remove(string1);
				}
			}

		}

		public virtual void registerTextureFX(TextureFX textureFX1)
		{
			this.textureList.Add(textureFX1);
			textureFX1.onTick();
		}

		public virtual void updateDynamicTextures()
		{
			int i1 = -1;

			for (int i2 = 0; i2 < this.textureList.Count; ++i2)
			{
				TextureFX textureFX3 = (TextureFX)this.textureList[i2];
				textureFX3.onTick();
				if (textureFX3.iconIndex != i1)
				{
					textureFX3.bindImage(this);
					i1 = textureFX3.iconIndex;
				}

				for (int i4 = 0; i4 < textureFX3.tileSize; ++i4)
				{
					for (int i5 = 0; i5 < textureFX3.tileSize; ++i5)
					{
						GL.TexSubImage2D(TextureTarget.Texture2D, 0, textureFX3.iconIndex % 16 * 16 + i4 * 16, textureFX3.iconIndex / 16 * 16 + i5 * 16, 16, 16, PixelFormat.Rgba, PixelType.UnsignedByte, textureFX3.imageData);
					}
				}
			}
		}

		private int alphaBlend(int color1, int color2)
		{
            IntByteUnion color1Union = new IntByteUnion() { integer = color1 };
            IntByteUnion color2Union = new IntByteUnion() { integer = color2 };

            byte alpha1 = color1Union.byte3;
            byte alpha2 = color2Union.byte3;

            if (alpha1 == 0)
            {
                return color2;
            }
            else if (alpha2 == 0)
            {
                return color1;
            }
            else
            {
                byte alpha3 = (byte)(alpha1 + alpha2 - alpha1 * alpha2 / 255);
                byte red3 = (byte)((color1Union.byte0 * alpha1 + color2Union.byte0 * alpha2 * (255 - alpha1) / 255) / alpha3);
                byte green3 = (byte)((color1Union.byte1 * alpha1 + color2Union.byte1 * alpha2 * (255 - alpha1) / 255) / alpha3);
                byte blue3 = (byte)((color1Union.byte2 * alpha1 + color2Union.byte2 * alpha2 * (255 - alpha1) / 255) / alpha3);
                return (alpha3 << 24) + (red3 << 16) + (green3 << 8) + blue3;
            }
        }

		public virtual void refreshTextures()
		{
			TexturePackBase texturePackBase1 = this.texturePack.selectedTexturePack;
			System.Collections.IEnumerator iterator2 = this.textureNameToImageMap.KeySet.GetEnumerator();

			Image<Bgra32> bufferedImage4;
			while (iterator2.MoveNext())
			{
				int i3 = ((int?)iterator2.Current).Value;
				bufferedImage4 = (Image<Bgra32>)this.textureNameToImageMap.lookup(i3);
				this.setupTexture(bufferedImage4, i3);
			}

			ThreadDownloadImageData threadDownloadImageData8;
			for (iterator2 = this.urlToImageDataMap.Values.GetEnumerator(); iterator2.MoveNext(); threadDownloadImageData8.textureSetupComplete = false)
			{
				threadDownloadImageData8 = (ThreadDownloadImageData)iterator2.Current;
			}

			iterator2 = this.textureMap.Keys.GetEnumerator();

			string string9;
			while (iterator2.MoveNext())
			{
				string9 = (string)iterator2.Current;

				try
				{
					if (string9.StartsWith("##", StringComparison.Ordinal))
					{
						bufferedImage4 = this.unwrapImageByColumns(this.readTextureImage(texturePackBase1.getResourceAsStream(string9.Substring(2))));
					}
					else if (string9.StartsWith("%clamp%", StringComparison.Ordinal))
					{
						this.clampTexture = true;
						bufferedImage4 = this.readTextureImage(texturePackBase1.getResourceAsStream(string9.Substring(7)));
					}
					else if (string9.StartsWith("%blur%", StringComparison.Ordinal))
					{
						this.blurTexture = true;
						bufferedImage4 = this.readTextureImage(texturePackBase1.getResourceAsStream(string9.Substring(6)));
					}
					else if (string9.StartsWith("%blurclamp%", StringComparison.Ordinal))
					{
						this.blurTexture = true;
						this.clampTexture = true;
						bufferedImage4 = this.readTextureImage(texturePackBase1.getResourceAsStream(string9.Substring(11)));
					}
					else
					{
						bufferedImage4 = this.readTextureImage(texturePackBase1.getResourceAsStream(string9));
					}

					int i5 = ((int?)this.textureMap[string9]).Value;
					this.setupTexture(bufferedImage4, i5);
					this.blurTexture = false;
					this.clampTexture = false;
				}
				catch (IOException iOException7)
				{
					Console.WriteLine(iOException7.ToString());
					Console.Write(iOException7.StackTrace);
				}
			}

			iterator2 = this.textureContentsMap.Keys.GetEnumerator();

			while (iterator2.MoveNext())
			{
				string9 = (string)iterator2.Current;

				try
				{
					if (string9.StartsWith("##", StringComparison.Ordinal))
					{
						bufferedImage4 = this.unwrapImageByColumns(this.readTextureImage(texturePackBase1.getResourceAsStream(string9.Substring(2))));
					}
					else if (string9.StartsWith("%clamp%", StringComparison.Ordinal))
					{
						this.clampTexture = true;
						bufferedImage4 = this.readTextureImage(texturePackBase1.getResourceAsStream(string9.Substring(7)));
					}
					else if (string9.StartsWith("%blur%", StringComparison.Ordinal))
					{
						this.blurTexture = true;
						bufferedImage4 = this.readTextureImage(texturePackBase1.getResourceAsStream(string9.Substring(6)));
					}
					else
					{
						bufferedImage4 = this.readTextureImage(texturePackBase1.getResourceAsStream(string9));
					}

					this.getImageContents(bufferedImage4, (int[])this.textureContentsMap[string9]);
					this.blurTexture = false;
					this.clampTexture = false;
				}
				catch (IOException iOException6)
				{
					Console.WriteLine(iOException6.ToString());
					Console.Write(iOException6.StackTrace);
				}
			}

		}
        
		private Image<Bgra32> readTextureImage(Stream inputStream1)
		{
			Image<Bgra32> bufferedImage2 = Image.Load<Bgra32>(inputStream1);
			inputStream1.Close();
			return bufferedImage2;
		}

		public virtual void bindTexture(int i1)
		{
			if (i1 >= 0)
			{
				GL.BindTexture(TextureTarget.Texture2D, i1);
			}
		}

		public static void FillIntBufferWithImage(Image<Bgra32> img, int[] buffer)
        {
			if (img == null || buffer == null)
				return;

			for (int x = 0; x < img.Width; x++)
			{
				for (int y = 0; y < img.Height; y++)
				{
					Bgra32 color = img[x, y];

					buffer[x + y * img.Width] = new IntByteUnion() { byte0 = color.B, byte1 = color.G, byte2 = color.R, byte3 = color.A }.integer;
				}
			}
		}

		public static void FillIntBufferWithImage(Image<Bgra32> img, int[] buffer, int srcX, int srcY, int srcWidth, int srcHeight)
		{
			if (img == null || buffer == null)
				return;

			int iter = 0;
            for (int y = srcY; y < srcY + srcHeight; y++)
            {
                for (int x = srcX; x < srcX + srcWidth; x++)
				{
					Bgra32 color = img[x, y];

					buffer[iter] = new IntByteUnion() { byte0 = color.B, byte1 = color.G, byte2 = color.R, byte3 = color.A }.integer;
					iter++;
				}
			}
		}
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct IntByteUnion
    {
		[FieldOffset(0)]
		public byte byte0;
		[FieldOffset(1)]
		public byte byte1;
		[FieldOffset(2)]
		public byte byte2;
		[FieldOffset(3)]
		public byte byte3;

		[FieldOffset(0)]
		public int integer;
	}

    [StructLayout(LayoutKind.Explicit)]
    public struct IntSByteUnion
    {
        [FieldOffset(0)]
        public sbyte byte0;
        [FieldOffset(1)]
        public sbyte byte1;
        [FieldOffset(2)]
        public sbyte byte2;
        [FieldOffset(3)]
        public sbyte byte3;

        [FieldOffset(0)]
        public int integer;
    }
}