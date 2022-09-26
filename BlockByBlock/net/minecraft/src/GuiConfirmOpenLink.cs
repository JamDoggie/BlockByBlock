namespace net.minecraft.src
{
	public abstract class GuiConfirmOpenLink : GuiYesNo
	{
		private string field_50054_a;
		private string field_50053_b;

		public GuiConfirmOpenLink(GuiScreen guiScreen1, string string2, int i3) : base(guiScreen1, StringTranslate.Instance.translateKey("chat.link.confirm"), string2, i3)
		{
			StringTranslate stringTranslate4 = StringTranslate.Instance;
			this.buttonText1 = stringTranslate4.translateKey("gui.yes");
			this.buttonText2 = stringTranslate4.translateKey("gui.no");
			this.field_50053_b = stringTranslate4.translateKey("chat.copy");
			this.field_50054_a = stringTranslate4.translateKey("chat.link.warning");
		}

		public override void initGui()
		{
			this.controlList.Add(new GuiButton(0, this.width / 3 - 83 + 0, this.height / 6 + 96, 100, 20, this.buttonText1));
			this.controlList.Add(new GuiButton(2, this.width / 3 - 83 + 105, this.height / 6 + 96, 100, 20, this.field_50053_b));
			this.controlList.Add(new GuiButton(1, this.width / 3 - 83 + 210, this.height / 6 + 96, 100, 20, this.buttonText2));
		}

		protected internal override void actionPerformed(GuiButton guiButton1)
		{
			if (guiButton1.id == 2)
			{
				this.copyLink();
				base.actionPerformed((GuiButton)this.controlList[1]);
			}
			else
			{
				base.actionPerformed(guiButton1);
			}

		}

		public abstract void copyLink();

		public override void drawScreen(int i1, int i2, float f3)
		{
			base.drawScreen(i1, i2, f3);
			this.drawCenteredString(this.fontRenderer, this.field_50054_a, this.width / 2, 110, 16764108);
		}
	}

}