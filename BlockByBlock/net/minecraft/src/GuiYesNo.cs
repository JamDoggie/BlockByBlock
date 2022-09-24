namespace net.minecraft.src
{
	public class GuiYesNo : GuiScreen
	{
		private GuiScreen parentScreen;
		private string message1;
		private string message2;
		protected internal string buttonText1;
		protected internal string buttonText2;
		private int worldNumber;

		public GuiYesNo(GuiScreen guiScreen1, string string2, string string3, int i4)
		{
			this.parentScreen = guiScreen1;
			this.message1 = string2;
			this.message2 = string3;
			this.worldNumber = i4;
			StringTranslate stringTranslate5 = StringTranslate.Instance;
			this.buttonText1 = stringTranslate5.translateKey("gui.yes");
			this.buttonText2 = stringTranslate5.translateKey("gui.no");
		}

		public GuiYesNo(GuiScreen guiScreen1, string string2, string string3, string string4, string string5, int i6)
		{
			this.parentScreen = guiScreen1;
			this.message1 = string2;
			this.message2 = string3;
			this.buttonText1 = string4;
			this.buttonText2 = string5;
			this.worldNumber = i6;
		}

		public override void initGui()
		{
			this.controlList.Add(new GuiSmallButton(0, this.width / 2 - 155, this.height / 6 + 96, this.buttonText1));
			this.controlList.Add(new GuiSmallButton(1, this.width / 2 - 155 + 160, this.height / 6 + 96, this.buttonText2));
		}

		protected internal override void actionPerformed(GuiButton guiButton1)
		{
			this.parentScreen.confirmClicked(guiButton1.id == 0, this.worldNumber);
		}

		public override void drawScreen(int i1, int i2, float f3)
		{
			this.drawDefaultBackground();
			this.drawCenteredString(this.fontRenderer, this.message1, this.width / 2, 70, 0xFFFFFF);
			this.drawCenteredString(this.fontRenderer, this.message2, this.width / 2, 90, 0xFFFFFF);
			base.drawScreen(i1, i2, f3);
		}
	}

}