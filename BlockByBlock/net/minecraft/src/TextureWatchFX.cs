using BlockByBlock;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;

namespace net.minecraft.src
{

	using Minecraft = net.minecraft.client.Minecraft;

	public class TextureWatchFX : TextureFX
	{
		private Minecraft mc;
		private int[] watchIconImageData = new int[256];
		private int[] dialImageData = new int[256];
		private double field_4222_j;
		private double field_4221_k;

		public TextureWatchFX(Minecraft minecraft1) : base(Item.pocketSundial.getIconFromDamage(0))
		{
			this.mc = minecraft1;
			this.tileImage = 1;

			try
			{
				Image<Bgra32> bufferedImage2 = Image.Load<Bgra32>(GameEnv.GetResourceAsStream("/gui/items.png"));
				int i3 = this.iconIndex % 16 * 16;
				int i4 = this.iconIndex / 16 * 16;
				TextureManager.FillIntBufferWithImage(bufferedImage2, watchIconImageData, i3, i4, 16, 16);
				bufferedImage2 = Image.Load<Bgra32>(GameEnv.GetResourceAsStream("/misc/dial.png"));
				TextureManager.FillIntBufferWithImage(bufferedImage2, dialImageData);
			}
			catch (IOException iOException5)
			{
				Console.WriteLine(iOException5.ToString());
				Console.Write(iOException5.StackTrace);
			}

		}

		public override void onTick()
		{
			double d1 = 0.0D;
			if (this.mc.theWorld != null && this.mc.thePlayer != null)
			{
				float f3 = this.mc.theWorld.getCelestialAngle(1.0F);
				d1 = (double)(-f3 * (float)Math.PI * 2.0F);
				if (!this.mc.theWorld.worldProvider.func_48217_e())
				{
					d1 = portinghelpers.MathHelper.NextDouble * (double)(float)Math.PI * 2.0D;
				}
			}

			double d22;
			for (d22 = d1 - this.field_4222_j; d22 < -3.141592653589793D; d22 += Math.PI * 2D)
			{
			}

			while (d22 >= Math.PI)
			{
				d22 -= Math.PI * 2D;
			}

			if (d22 < -1.0D)
			{
				d22 = -1.0D;
			}

			if (d22 > 1.0D)
			{
				d22 = 1.0D;
			}

			this.field_4221_k += d22 * 0.1D;
			this.field_4221_k *= 0.8D;
			this.field_4222_j += this.field_4221_k;
			double d5 = Math.Sin(this.field_4222_j);
			double d7 = Math.Cos(this.field_4222_j);

			for (int i9 = 0; i9 < 256; ++i9)
			{
				int i10 = this.watchIconImageData[i9] >> 24 & 255;
				int i11 = this.watchIconImageData[i9] >> 16 & 255;
				int i12 = this.watchIconImageData[i9] >> 8 & 255;
				int i13 = this.watchIconImageData[i9] >> 0 & 255;
				if (i11 == i13 && i12 == 0 && i13 > 0)
				{
					double d14 = -((double)(i9 % 16) / 15.0D - 0.5D);
					double d16 = (double)(i9 / 16) / 15.0D - 0.5D;
					int i18 = i11;
					int i19 = (int)((d14 * d7 + d16 * d5 + 0.5D) * 16.0D);
					int i20 = (int)((d16 * d7 - d14 * d5 + 0.5D) * 16.0D);
					int i21 = (i19 & 15) + (i20 & 15) * 16;
					i10 = this.dialImageData[i21] >> 24 & 255;
					i11 = (this.dialImageData[i21] >> 16 & 255) * i11 / 255;
					i12 = (this.dialImageData[i21] >> 8 & 255) * i18 / 255;
					i13 = (this.dialImageData[i21] >> 0 & 255) * i18 / 255;
				}

				this.imageData[i9 * 4 + 0] = (byte)(i11 & 255);
				this.imageData[i9 * 4 + 1] = (byte)(i12 & 255);
				this.imageData[i9 * 4 + 2] = (byte)(i13 & 255);
				this.imageData[i9 * 4 + 3] = (byte)(i10 & 255);
			}

		}
	}

}