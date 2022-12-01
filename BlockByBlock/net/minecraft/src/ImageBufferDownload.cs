using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace net.minecraft.src
{

	public class ImageBufferDownload : ImageBuffer
	{
		private int[] imageData;
		private int imageWidth;
		private int imageHeight;

		public virtual Image<Bgra32> parseUserSkin(Image<Bgra32> bufferedImage1)
		{
			if (bufferedImage1 == null)
			{
				return null;
			}
			else
			{
				this.imageWidth = 64;
				this.imageHeight = 32;
				Image<Bgra32> bufferedImage2 = new Image<Bgra32>(this.imageWidth, this.imageHeight);
				bufferedImage2.Mutate(x => x.DrawImage(bufferedImage1, 1.0f));
				TextureManager.FillIntBufferWithImage(bufferedImage2, imageData);
				this.func_884_b(0, 0, 32, 16);
				this.func_885_a(32, 0, 64, 32);
				this.func_884_b(0, 16, 64, 32);
				bool z4 = false;

				int i5;
				int i6;
				int i7;
				for (i5 = 32; i5 < 64; ++i5)
				{
					for (i6 = 0; i6 < 16; ++i6)
					{
						i7 = this.imageData[i5 + i6 * 64];
						if ((i7 >> 24 & 255) < 128)
						{
							z4 = true;
						}
					}
				}

				if (!z4)
				{
					for (i5 = 32; i5 < 64; ++i5)
					{
						for (i6 = 0; i6 < 16; ++i6)
						{
							i7 = this.imageData[i5 + i6 * 64];
							if ((i7 >> 24 & 255) < 128)
							{
								z4 = true;
							}
						}
					}
				}

				return bufferedImage2;
			}
		}

		private void func_885_a(int i1, int i2, int i3, int i4)
		{
			if (!this.func_886_c(i1, i2, i3, i4))
			{
				for (int i5 = i1; i5 < i3; ++i5)
				{
					for (int i6 = i2; i6 < i4; ++i6)
					{
						this.imageData[i5 + i6 * this.imageWidth] &= 0xFFFFFF;
					}
				}

			}
		}

		private void func_884_b(int i1, int i2, int i3, int i4)
		{
			for (int i5 = i1; i5 < i3; ++i5)
			{
				for (int i6 = i2; i6 < i4; ++i6)
				{
					this.imageData[i5 + i6 * this.imageWidth] |= unchecked((int)0xFF000000);
				}
			}

		}

		private bool func_886_c(int i1, int i2, int i3, int i4)
		{
			for (int i5 = i1; i5 < i3; ++i5)
			{
				for (int i6 = i2; i6 < i4; ++i6)
				{
					int i7 = this.imageData[i5 + i6 * this.imageWidth];
					if ((i7 >> 24 & 255) < 128)
					{
						return true;
					}
				}
			}

			return false;
		}
	}

}