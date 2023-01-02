namespace net.minecraft.src
{
    using OpenTK.Graphics.OpenGL;
    using Minecraft = net.minecraft.client.Minecraft;
	public class GuiButtonLanguage : GuiButton
	{
		public GuiButtonLanguage(int i1, int i2, int i3) : base(i1, i2, i3, 20, 20, "")
		{
		}

		public override void drawButton(Minecraft minecraft1, int i2, int i3)
		{
			if (this.shouldDrawButton)
			{
				GL.BindTexture(TextureTarget.Texture2D, minecraft1.renderEngine.getTexture("/gui/gui.png"));
				Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
				bool z4 = i2 >= this.xPosition && i3 >= this.yPosition && i2 < this.xPosition + this.width && i3 < this.yPosition + this.height;
				int i5 = 106;
				if (z4)
				{
					i5 += this.height;
				}

				this.drawTexturedModalRect(this.xPosition, this.yPosition, 0, i5, this.width, this.height);
			}
		}
	}

}