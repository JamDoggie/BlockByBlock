using System;

namespace net.minecraft.src
{

	using Minecraft = net.minecraft.client.Minecraft;

	using GL11 = org.lwjgl.opengl.GL11;

	public class TexturePackDefault : TexturePackBase
	{
		private int texturePackName = -1;
		private BufferedImage texturePackThumbnail;

		public TexturePackDefault()
		{
			this.texturePackFileName = "Default";
			this.firstDescriptionLine = "The default look of Minecraft";

			try
			{
				this.texturePackThumbnail = ImageIO.read(typeof(TexturePackDefault).getResource("/pack.png"));
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
				GL11.glBindTexture(GL11.GL_TEXTURE_2D, minecraft1.renderEngine.getTexture("/gui/unknown_pack.png"));
			}

		}
	}

}