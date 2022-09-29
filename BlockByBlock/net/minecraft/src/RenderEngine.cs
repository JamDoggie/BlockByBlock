using System;
using System.Collections;
using System.IO;

namespace net.minecraft.src
{

	using GL11 = org.lwjgl.opengl.GL11;

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
		private BufferedImage missingTextureImage = new BufferedImage(64, 64, 2);
		private int field_48512_n = 16;

		public RenderEngine(TexturePackList texturePackList1, GameSettings gameSettings2)
		{
			this.texturePack = texturePackList1;
			this.options = gameSettings2;
			Graphics graphics3 = this.missingTextureImage.getGraphics();
			graphics3.setColor(Color.WHITE);
			graphics3.fillRect(0, 0, 64, 64);
			graphics3.setColor(Color.BLACK);
			graphics3.drawString("missingtex", 1, 10);
			graphics3.dispose();
		}

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

		private int[] getImageContentsAndAllocate(BufferedImage bufferedImage1)
		{
			int i2 = bufferedImage1.getWidth();
			int i3 = bufferedImage1.getHeight();
			int[] i4 = new int[i2 * i3];
			bufferedImage1.getRGB(0, 0, i2, i3, i4, 0, i2);
			return i4;
		}

		private int[] getImageContents(BufferedImage bufferedImage1, int[] i2)
		{
			int i3 = bufferedImage1.getWidth();
			int i4 = bufferedImage1.getHeight();
			bufferedImage1.getRGB(0, 0, i3, i4, i2, 0, i3);
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

		private BufferedImage unwrapImageByColumns(BufferedImage bufferedImage1)
		{
			int i2 = bufferedImage1.getWidth() / 16;
			BufferedImage bufferedImage3 = new BufferedImage(16, bufferedImage1.getHeight() * i2, 2);
			Graphics graphics4 = bufferedImage3.getGraphics();

			for (int i5 = 0; i5 < i2; ++i5)
			{
				graphics4.drawImage(bufferedImage1, -i5 * 16, i5 * bufferedImage1.getHeight(), (ImageObserver)null);
			}

			graphics4.dispose();
			return bufferedImage3;
		}

		public virtual int allocateAndSetupTexture(BufferedImage bufferedImage1)
		{
			this.singleIntBuffer.clear();
			GLAllocation.generateTextureNames(this.singleIntBuffer);
			int i2 = this.singleIntBuffer.get(0);
			this.setupTexture(bufferedImage1, i2);
			this.textureNameToImageMap.addKey(i2, bufferedImage1);
			return i2;
		}

		public virtual void setupTexture(BufferedImage bufferedImage1, int i2)
		{
			GL11.glBindTexture(GL11.GL_TEXTURE_2D, i2);
			if (useMipmaps)
			{
				GL11.glTexParameteri(GL11.GL_TEXTURE_2D, GL11.GL_TEXTURE_MIN_FILTER, GL11.GL_NEAREST_MIPMAP_LINEAR);
				GL11.glTexParameteri(GL11.GL_TEXTURE_2D, GL11.GL_TEXTURE_MAG_FILTER, GL11.GL_NEAREST);
			}
			else
			{
				GL11.glTexParameteri(GL11.GL_TEXTURE_2D, GL11.GL_TEXTURE_MIN_FILTER, GL11.GL_NEAREST);
				GL11.glTexParameteri(GL11.GL_TEXTURE_2D, GL11.GL_TEXTURE_MAG_FILTER, GL11.GL_NEAREST);
			}

			if (this.blurTexture)
			{
				GL11.glTexParameteri(GL11.GL_TEXTURE_2D, GL11.GL_TEXTURE_MIN_FILTER, GL11.GL_LINEAR);
				GL11.glTexParameteri(GL11.GL_TEXTURE_2D, GL11.GL_TEXTURE_MAG_FILTER, GL11.GL_LINEAR);
			}

			if (this.clampTexture)
			{
				GL11.glTexParameteri(GL11.GL_TEXTURE_2D, GL11.GL_TEXTURE_WRAP_S, GL11.GL_CLAMP);
				GL11.glTexParameteri(GL11.GL_TEXTURE_2D, GL11.GL_TEXTURE_WRAP_T, GL11.GL_CLAMP);
			}
			else
			{
				GL11.glTexParameteri(GL11.GL_TEXTURE_2D, GL11.GL_TEXTURE_WRAP_S, GL11.GL_REPEAT);
				GL11.glTexParameteri(GL11.GL_TEXTURE_2D, GL11.GL_TEXTURE_WRAP_T, GL11.GL_REPEAT);
			}

			int i3 = bufferedImage1.getWidth();
			int i4 = bufferedImage1.getHeight();
			int[] i5 = new int[i3 * i4];
			sbyte[] b6 = new sbyte[i3 * i4 * 4];
			bufferedImage1.getRGB(0, 0, i3, i4, i5, 0, i3);

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

				b6[i7 * 4 + 0] = (sbyte)i9;
				b6[i7 * 4 + 1] = (sbyte)i10;
				b6[i7 * 4 + 2] = (sbyte)i11;
				b6[i7 * 4 + 3] = (sbyte)i8;
			}

			imageData.clear();
			imageData.Put(b6);
			imageData.position(0).limit(b6.Length);
			GL11.glTexImage2D(GL11.GL_TEXTURE_2D, 0, GL11.GL_RGBA, i3, i4, 0, GL11.GL_RGBA, GL11.GL_UNSIGNED_BYTE, this.imageData);
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

					GL11.glTexImage2D(GL11.GL_TEXTURE_2D, i7, GL11.GL_RGBA, i9, i10, 0, GL11.GL_RGBA, GL11.GL_UNSIGNED_BYTE, this.imageData);
				}
			}

		}

		public virtual void createTextureFromBytes(int[] i1, int i2, int i3, int i4)
		{
			GL11.glBindTexture(GL11.GL_TEXTURE_2D, i4);
			if (useMipmaps)
			{
				GL11.glTexParameteri(GL11.GL_TEXTURE_2D, GL11.GL_TEXTURE_MIN_FILTER, GL11.GL_NEAREST_MIPMAP_LINEAR);
				GL11.glTexParameteri(GL11.GL_TEXTURE_2D, GL11.GL_TEXTURE_MAG_FILTER, GL11.GL_NEAREST);
			}
			else
			{
				GL11.glTexParameteri(GL11.GL_TEXTURE_2D, GL11.GL_TEXTURE_MIN_FILTER, GL11.GL_NEAREST);
				GL11.glTexParameteri(GL11.GL_TEXTURE_2D, GL11.GL_TEXTURE_MAG_FILTER, GL11.GL_NEAREST);
			}

			if (this.blurTexture)
			{
				GL11.glTexParameteri(GL11.GL_TEXTURE_2D, GL11.GL_TEXTURE_MIN_FILTER, GL11.GL_LINEAR);
				GL11.glTexParameteri(GL11.GL_TEXTURE_2D, GL11.GL_TEXTURE_MAG_FILTER, GL11.GL_LINEAR);
			}

			if (this.clampTexture)
			{
				GL11.glTexParameteri(GL11.GL_TEXTURE_2D, GL11.GL_TEXTURE_WRAP_S, GL11.GL_CLAMP);
				GL11.glTexParameteri(GL11.GL_TEXTURE_2D, GL11.GL_TEXTURE_WRAP_T, GL11.GL_CLAMP);
			}
			else
			{
				GL11.glTexParameteri(GL11.GL_TEXTURE_2D, GL11.GL_TEXTURE_WRAP_S, GL11.GL_REPEAT);
				GL11.glTexParameteri(GL11.GL_TEXTURE_2D, GL11.GL_TEXTURE_WRAP_T, GL11.GL_REPEAT);
			}

			sbyte[] b5 = new sbyte[i2 * i3 * 4];

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

				b5[i6 * 4 + 0] = (sbyte)i8;
				b5[i6 * 4 + 1] = (sbyte)i9;
				b5[i6 * 4 + 2] = (sbyte)i10;
				b5[i6 * 4 + 3] = (sbyte)i7;
			}

			this.imageData.clear();
			this.imageData.Put(b5);
			this.imageData.position(0).limit(b5.Length);
			GL11.glTexSubImage2D(GL11.GL_TEXTURE_2D, 0, 0, 0, i2, i3, GL11.GL_RGBA, GL11.GL_UNSIGNED_BYTE, this.imageData);
		}

		public virtual void deleteTexture(int i1)
		{
			this.textureNameToImageMap.removeObject(i1);
			this.singleIntBuffer.clear();
			this.singleIntBuffer.putInt(i1);
			this.singleIntBuffer.flip();
			GL11.glDeleteTextures(this.singleIntBuffer);
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
				this.imageData.clear();
				this.imageData.Put(textureFX3.imageData);
				this.imageData.position(0).limit(textureFX3.imageData.Length);
				if (textureFX3.iconIndex != i1)
				{
					textureFX3.bindImage(this);
					i1 = textureFX3.iconIndex;
				}

				for (int i4 = 0; i4 < textureFX3.tileSize; ++i4)
				{
					for (int i5 = 0; i5 < textureFX3.tileSize; ++i5)
					{
						GL11.glTexSubImage2D(GL11.GL_TEXTURE_2D, 0, textureFX3.iconIndex % 16 * 16 + i4 * 16, textureFX3.iconIndex / 16 * 16 + i5 * 16, 16, 16, GL11.GL_RGBA, GL11.GL_UNSIGNED_BYTE, this.imageData);
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

			BufferedImage bufferedImage4;
			while (iterator2.MoveNext())
			{
				int i3 = ((int?)iterator2.Current).Value;
				bufferedImage4 = (BufferedImage)this.textureNameToImageMap.lookup(i3);
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
        
		private BufferedImage readTextureImage(Stream inputStream1)
		{
			BufferedImage bufferedImage2 = ImageIO.read(inputStream1);
			inputStream1.Close();
			return bufferedImage2;
		}

		public virtual void bindTexture(int i1)
		{
			if (i1 >= 0)
			{
				GL11.glBindTexture(GL11.GL_TEXTURE_2D, i1);
			}
		}
	}

}