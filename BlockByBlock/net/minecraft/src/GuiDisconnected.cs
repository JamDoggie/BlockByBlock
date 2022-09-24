namespace net.minecraft.src
{
	public class GuiDisconnected : GuiScreen
	{
		private string errorMessage;
		private string errorDetail;

		public GuiDisconnected(string string1, string string2, params object[] object3)
		{
			StringTranslate stringTranslate4 = StringTranslate.Instance;
			this.errorMessage = stringTranslate4.translateKey(string1);
			if (object3 != null)
			{
				this.errorDetail = stringTranslate4.translateKeyFormat(string2, object3);
			}
			else
			{
				this.errorDetail = stringTranslate4.translateKey(string2);
			}

		}

		public override void updateScreen()
		{
		}

		protected internal override void keyTyped(char c1, int i2)
		{
		}

		public override void initGui()
		{
			StringTranslate stringTranslate1 = StringTranslate.Instance;
			this.controlList.Clear();
			this.controlList.Add(new GuiButton(0, this.width / 2 - 100, this.height / 4 + 120 + 12, stringTranslate1.translateKey("gui.toMenu")));
		}

		protected internal override void actionPerformed(GuiButton guiButton1)
		{
			if (guiButton1.id == 0)
			{
				this.mc.displayGuiScreen(new GuiMainMenu());
			}

		}

		public override void drawScreen(int i1, int i2, float f3)
		{
			this.drawDefaultBackground();
			this.drawCenteredString(this.fontRenderer, this.errorMessage, this.width / 2, this.height / 2 - 50, 0xFFFFFF);
			this.drawCenteredString(this.fontRenderer, this.errorDetail, this.width / 2, this.height / 2 - 10, 0xFFFFFF);
			base.drawScreen(i1, i2, f3);
		}
	}

}