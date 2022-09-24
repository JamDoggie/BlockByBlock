namespace net.minecraft.src
{
	public class GuiErrorScreen : GuiScreen
	{
		private string message1;
		private string message2;

		public override void initGui()
		{
		}

		public override void drawScreen(int i1, int i2, float f3)
		{
			this.drawGradientRect(0, 0, this.width, this.height, -12574688, -11530224);
			this.drawCenteredString(this.fontRenderer, this.message1, this.width / 2, 90, 0xFFFFFF);
			this.drawCenteredString(this.fontRenderer, this.message2, this.width / 2, 110, 0xFFFFFF);
			base.drawScreen(i1, i2, f3);
		}

		protected internal override void keyTyped(char c1, int i2)
		{
		}
	}

}