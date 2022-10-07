using System;
using BlockByBlock;
using OpenTK.Graphics.OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace net.minecraft.src
{

	using Minecraft = net.minecraft.client.Minecraft;

	public class TexturePackDefault : TexturePackBase
	{
		private int texturePackName = -1;
		private Image<Bgra32> texturePackThumbnail;

		public TexturePackDefault()
		{
			this.texturePackFileName = "Default";
			this.firstDescriptionLine = "The default look of Minecraft";

			try
			{
				this.texturePackThumbnail = Image.Load<Bgra32>(GameEnv.GetResourceAsStream("/pack.png"));
			}
			catch (IOException iOException2)
			{
				Console.WriteLine(iOException2.ToString());
				Console.Write(iOException2.StackTrace);
			}

		}

		public override void unbindThumbnailTexture(Minecraft minecraft1)
		{
			if (this.texturePackThumbnail != null)
			{
				minecraft1.renderEngine.deleteTexture(this.texturePackName);
			}

		}

		public override void bindThumbnailTexture(Minecraft minecraft1)
		{
			if (this.texturePackThumbnail != null && this.texturePackName < 0)
			{
				this.texturePackName = minecraft1.renderEngine.allocateAndSetupTexture(this.texturePackThumbnail);
			}

			if (this.texturePackThumbnail != null)
			{
				minecraft1.renderEngine.bindTexture(this.texturePackName);
			}
			else
			{
				GL.BindTexture(TextureTarget.Texture2D, minecraft1.renderEngine.getTexture("/gui/unknown_pack.png"));
			}

		}
	}

}