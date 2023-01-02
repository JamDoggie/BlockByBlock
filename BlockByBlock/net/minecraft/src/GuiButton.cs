using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{
	using Minecraft = net.minecraft.client.Minecraft;

	public class GuiButton : Gui
	{
		protected internal int width;
		protected internal int height;
		public int xPosition;
		public int yPosition;
		public string displayString;
		public int id;
		public bool enabled;
		public bool shouldDrawButton;

		public GuiButton(int i1, int i2, int i3, string string4) : this(i1, i2, i3, 200, 20, string4)
		{
		}

		public GuiButton(int id, int xPosition, int yPosition, int i4, int i5, string displayString)
		{
			this.width = 200;
			this.height = 20;
			this.enabled = true;
			this.shouldDrawButton = true;
			this.id = id;
			this.xPosition = xPosition;
			this.yPosition = yPosition;
			this.width = i4;
			this.height = i5;
			this.displayString = displayString;
		}

		protected internal virtual int getHoverState(bool z1)
		{
			sbyte b2 = 1;
			if (!this.enabled)
			{
				b2 = 0;
			}
			else if (z1)
			{
				b2 = 2;
			}

			return b2;
		}

		public virtual void drawButton(Minecraft minecraft1, int i2, int i3)
		{
			if (this.shouldDrawButton)
			{
				FontRenderer fontRenderer4 = minecraft1.fontRenderer;
				GL.BindTexture(TextureTarget.Texture2D, minecraft1.renderEngine.getTexture("/gui/gui.png"));
				Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
				bool z5 = i2 >= this.xPosition && i3 >= this.yPosition && i2 < this.xPosition + this.width && i3 < this.yPosition + this.height;
				int i6 = this.getHoverState(z5);
				this.drawTexturedModalRect(this.xPosition, this.yPosition, 0, 46 + i6 * 20, this.width / 2, this.height);
				this.drawTexturedModalRect(this.xPosition + this.width / 2, this.yPosition, 200 - this.width / 2, 46 + i6 * 20, this.width / 2, this.height);
				this.mouseDragged(minecraft1, i2, i3);
				int i7 = 14737632;
				if (!this.enabled)
				{
					i7 = -6250336;
				}
				else if (z5)
				{
					i7 = 16777120;
				}

				this.drawCenteredString(fontRenderer4, this.displayString, this.xPosition + this.width / 2, this.yPosition + (this.height - 8) / 2, i7);
			}
		}

		protected internal virtual void mouseDragged(Minecraft minecraft1, int i2, int i3)
		{
		}

		public virtual void mouseReleased(int i1, int i2)
		{
		}

		public virtual bool mousePressed(Minecraft minecraft1, int i2, int i3)
		{
			bool isEnabled = enabled;
			bool shouldDraw = shouldDrawButton;
			bool x1 = i2 >= this.xPosition;
            bool x2 = i2 < this.xPosition + this.width;
            bool y1 = i3 >= this.yPosition;
            bool y2 = i3 < this.yPosition + this.height;
            return this.enabled && this.shouldDrawButton && i2 >= this.xPosition && i3 >= this.yPosition && i2 < this.xPosition + this.width && i3 < this.yPosition + this.height;
		}
	}

}