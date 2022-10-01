using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{

	public class RenderEngine
	{
		public static bool useMipmaps = false;
		private Hashtable textureMap = new Hashtable();
		private Hashtable textureContentsMap = new Hashtable();
		private IntHashMap textureNameToImageMap = new IntHashMap();
		private IntBuffer singleIntBuffer = GLAllocation.createDirectIntBuffer(1);
		private ByteBuffer imageData = GLAllocation.createDirectByteBuffer(16777216);
		private System.Collections.IList textureList = new ArrayList();
		private System.Collections.IDictionary urlToImageDataMap = new Hashtable();
		private GameSettings options;
		public bool clampTexture = false;
		public bool blurTexture = false;
		private TexturePackList texturePack;
        private Image<Rgba32> missingTextureImage = new(64, 64, Color.Black);
        private int field_48512_n = 16;

		public RenderEngine(TexturePackList texturePackList1, GameSettings gameSettings2)
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

		private int[] getImageContentsAndAllocate(Image<Rgba32> bufferedImage1)
		{
			int i2 = bufferedImage1.Width;
			int i3 = bufferedImage1.Height;
			byte[] i4 = new byte[i2 * i3 * 4];
			bufferedImage1.CopyPixelDataTo(i4);
			int[] array = new int[i2 * i3];

            System.Buffer.BlockCopy(i4, 0, array, 0, i4.Length);

            return array;
		}

		private int[] getImageContents(Image<Rgba32> bufferedImage1, int[] i2)
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
					this.singleIntBuffer.clear();
					GLAllocation.generateTextureNames(this.singleIntBuffer);
					int i6 = this.singleIntBuffer.get(0);
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
					int i4 = this.singleIntBuffer.get(0);
					this.setupTexture(this.missingTextureImage, i4);
					this.textureMap[string1] = i4;
					return i4;
				}
			}
		}

		private Image<Rgba32> unwrapImageByColumns(Image<Rgba32> bufferedImage1)
		{
			int i2 = bufferedImage1.Width / 16;
			Image<Rgba32> bufferedImage3 = new(16, bufferedImage1.Height * i2);
			
			for (int i5 = 0; i5 < i2; ++i5)
			{
				bufferedImage3.Mutate(x => x.DrawImage(bufferedImage1, new Point(-i5 * 16, i5 * bufferedImage1.Height), 1.0f));
			}
            
			return bufferedImage3;
		}

		public virtual int allocateAndSetupTexture(Image<Rgba32> bufferedImage1)
		{
			singleIntBuffer.clear();
			GLAllocation.generateTextureNames(singleIntBuffer);
			int i2 = singleIntBuffer.get(0);
			setupTexture(bufferedImage1, i2);
			textureNameToImageMap.addKey(i2, bufferedImage1);
			return i2;
		}

		public virtual void setupTexture(Image<Rgba32> bufferedImage1, int i2)
		{
			GL.BindTexture(TextureTarget.Texture2D, i2);
			if (useMipmaps)
			{
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, new int[] { (int)TextureMinFilter.NearestMipmapLinear });
				GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, new int[] { (int)TextureMagFilter.Nearest });
			}
			else
			{
				GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, new int[] { (int)TextureMinFilter.Nearest });
				GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, new int[] { (int)TextureMagFilter.Nearest });
			}

			if (blurTexture)
			{
				GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, new int[] { (int)TextureMinFilter.Linear });
				GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, new int[] { (int)TextureMagFilter.Linear });
			}

			if (clampTexture)
			{
				GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, new int[] { (int)TextureWrapMode.Clamp });
				GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, new int[] { (int)TextureWrapMode.Clamp });
			}
			else
			{
				GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, new int[] { (int)TextureWrapMode.Repeat });
				GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, new int[] { (int)TextureWrapMode.Repeat });
			}

			int i3 = bufferedImage1.Width;
			int i4 = bufferedImage1.Height;
			int[] i5 = new int[i3 * i4];
			byte[] b6 = new byte[i3 * i4 * 4];
			FillIntBufferWithImage(bufferedImage1, i5);

			int i7;
			int i8;
			int i9;
			int i10;
			int i11;
			int i12;
			int i13;
			int i14;
			for (i7 = 0; i7 < i5.Length; ++i7)
			{
				i8 = i5[i7] >> 24 & 255;
				i9 = i5[i7] >> 16 & 255;
				i10 = i5[i7] >> 8 & 255;
				i11 = i5[i7] & 255;
				if (this.options != null && this.options.anaglyph)
				{
					i12 = (i9 * 30 + i10 * 59 + i11 * 11) / 100;
					i13 = (i9 * 30 + i10 * 70) / 100;
					i14 = (i9 * 30 + i11 * 70) / 100;
					i9 = i12;
					i10 = i13;
					i11 = i14;
				}

				b6[i7 * 4 + 0] = (byte)(i9 & 255);
				b6[i7 * 4 + 1] = (byte)(i10 & 255);
				b6[i7 * 4 + 2] = (byte)(i11 & 255);
				b6[i7 * 4 + 3] = (byte)(i8 & 255);
			}

			imageData.clear();
			imageData.Put(b6, 0, b6.Length);
			imageData.position(0).limit(b6.Length);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, i3, i4, 0, PixelFormat.Rgba, PixelType.UnsignedByte, b6);
            if (useMipmaps)
			{
				for (i7 = 1; i7 <= 4; ++i7)
				{
					i8 = i3 >> i7 - 1;
					i9 = i3 >> i7;
					i10 = i4 >> i7;

					for (i11 = 0; i11 < i9; ++i11)
					{
						for (i12 = 0; i12 < i10; ++i12)
						{
							i13 = this.imageData.getInt((i11 * 2 + 0 + (i12 * 2 + 0) * i8) * 4);
							i14 = this.imageData.getInt((i11 * 2 + 1 + (i12 * 2 + 0) * i8) * 4);
							int i15 = this.imageData.getInt((i11 * 2 + 1 + (i12 * 2 + 1) * i8) * 4);
							int i16 = this.imageData.getInt((i11 * 2 + 0 + (i12 * 2 + 1) * i8) * 4);
							int i17 = this.alphaBlend(this.alphaBlend(i13, i14), this.alphaBlend(i15, i16));
							this.imageData.putInt((i11 + i12 * i9) * 4, i17);
						}
					}

					byte[] buff = new byte[imageData.getLimit()];
					imageData.get(buff, 0, buff.Length);

					GL.TexImage2D(TextureTarget.Texture2D, i7, PixelInternalFormat.Rgba, i9, i10, 0, PixelFormat.Rgba, PixelType.UnsignedByte, buff);
                }
			}

		}

		public virtual void createTextureFromBytes(int[] i1, int i2, int i3, int i4)
		{
            GL.BindTexture(TextureTarget.Texture2D, i4);
            if (useMipmaps)
			{
				GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, new int[] { (int)TextureMinFilter.NearestMipmapLinear });
				GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, new int[] { (int)TextureMagFilter.Nearest });
			}
			else
			{
				GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, new int[] { (int)TextureMinFilter.Nearest });
				GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, new int[] { (int)TextureMagFilter.Nearest });
			}

			if (blurTexture)
			{
				GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, new int[] { (int)TextureMinFilter.Linear });
				GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, new int[] { (int)TextureMagFilter.Linear });
			}

			if (clampTexture)
			{
				GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, new int[] { (int)TextureWrapMode.Clamp });
				GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, new int[] { (int)TextureWrapMode.Clamp });
			}
			else
			{
				GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, new int[] { (int)TextureWrapMode.Repeat });
				GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, new int[] { (int)TextureWrapMode.Repeat });
			}

			byte[] b5 = new byte[i2 * i3 * 4];

			for (int i6 = 0; i6 < i1.Length; ++i6)
			{
				int i7 = i1[i6] >> 24 & 255;
				int i8 = i1[i6] >> 16 & 255;
				int i9 = i1[i6] >> 8 & 255;
				int i10 = i1[i6] & 255;
				if (this.options != null && this.options.anaglyph)
				{
					int i11 = (i8 * 30 + i9 * 59 + i10 * 11) / 100;
					int i12 = (i8 * 30 + i9 * 70) / 100;
					int i13 = (i8 * 30 + i10 * 70) / 100;
					i8 = i11;
					i9 = i12;
					i10 = i13;
				}

				b5[i6 * 4 + 0] = (byte)(i8 & 255);
				b5[i6 * 4 + 1] = (byte)(i9 & 255);
				b5[i6 * 4 + 2] = (byte)(i10 & 255);
				b5[i6 * 4 + 3] = (byte)(i7 & 255);
			}

			this.imageData.clear();
			this.imageData.Put(b5, 0, b5.Length);
			this.imageData.position(0).limit(b5.Length);
            GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, i2, i3, PixelFormat.Rgba, PixelType.UnsignedByte, b5);
        }

		int[] singleIntCache = new int[1];

		public virtual void deleteTexture(int i1)
		{
			textureNameToImageMap.removeObject(i1);
			singleIntBuffer.clear();
			singleIntBuffer.putInt(i1); // PORTING TODO: this intbuffer is redundant because no methods take in ByteBuffers anyway since we're in C# land.
			singleIntBuffer.flip();
            singleIntCache[0] = i1;
			GL.DeleteTextures(1, singleIntCache);
		}

		public virtual int getTextureForDownloadableImage(string string1, string string2)
		{
			ThreadDownloadImageData threadDownloadImageData3 = (ThreadDownloadImageData)this.urlToImageDataMap[string1];
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
				textureFX3.anaglyphEnabled = this.options.anaglyph;
				textureFX3.onTick();
				imageData.clear();
				imageData.Put(textureFX3.imageData, 0, textureFX3.imageData.Length);
				imageData.position(0).limit(textureFX3.imageData.Length);
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

		private int alphaBlend(int i1, int i2)
		{
			int i3 = (int)((i1 & 0xFF000000) >> 24 & 255);
			int i4 = (int)((i2 & 0xFF000000) >> 24 & 255);
			short s5 = 255;
			short s15;
			short s16;
			if (i3 + i4 < 255)
			{
				s5 = 0;
				s15 = 1;
				s16 = 1;
			}
			else if (i3 > i4)
			{
				s15 = 255;
				s16 = 1;
			}
			else
			{
				s15 = 1;
				s16 = 255;
			}

			int i6 = (i1 >> 16 & 255) * s15;
			int i7 = (i1 >> 8 & 255) * s15;
			int i8 = (i1 & 255) * s15;
			int i9 = (i2 >> 16 & 255) * s16;
			int i10 = (i2 >> 8 & 255) * s16;
			int i11 = (i2 & 255) * s16;
			int i12 = (i6 + i9) / (s15 + s16);
			int i13 = (i7 + i10) / (s15 + s16);
			int i14 = (i8 + i11) / (s15 + s16);
			return s5 << 24 | i12 << 16 | i13 << 8 | i14;
		}

		public virtual void refreshTextures()
		{
			TexturePackBase texturePackBase1 = this.texturePack.selectedTexturePack;
			System.Collections.IEnumerator iterator2 = this.textureNameToImageMap.KeySet.GetEnumerator();

			Image<Rgba32> bufferedImage4;
			while (iterator2.MoveNext())
			{
				int i3 = ((int?)iterator2.Current).Value;
				bufferedImage4 = (Image<Rgba32>)this.textureNameToImageMap.lookup(i3);
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
        
		private Image<Rgba32> readTextureImage(Stream inputStream1)
		{
			Image<Rgba32> bufferedImage2 = Image.Load<Rgba32>(inputStream1);
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

		public static void FillIntBufferWithImage(Image<Rgba32> img, int[] buffer)
        {
			if (img == null || buffer == null)
				return;

			for (int x = 0; x < img.Width; x++)
			{
				for (int y = 0; y < img.Height; y++)
				{
					Rgba32 color = img[x, y];

					buffer[x + y * img.Width] = new IntByteUnion() { byte0 = color.R, byte1 = color.G, byte2 = color.B, byte3 = color.A }.integer;
				}
			}
		}
	}

	[StructLayout(LayoutKind.Explicit)]
	struct IntByteUnion
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
}