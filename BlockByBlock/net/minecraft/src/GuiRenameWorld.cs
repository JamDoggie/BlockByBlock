namespace net.minecraft.src
{
	public class GuiRenameWorld : GuiScreen
	{
		private GuiScreen parentGuiScreen;
		private GuiTextField theGuiTextField;
		private readonly string worldName;

		public GuiRenameWorld(GuiScreen guiScreen1, string string2)
		{
			this.parentGuiScreen = guiScreen1;
			this.worldName = string2;
		}

		public override void updateScreen()
		{
			this.theGuiTextField.updateCursorCounter();
		}

		public override void initGui()
		{
			StringTranslate stringTranslate1 = StringTranslate.Instance;
			mc.mcApplet.EnableKeyRepeatingEvents(true);
			this.controlList.Clear();
			this.controlList.Add(new GuiButton(0, this.width / 2 - 100, this.height / 4 + 96 + 12, stringTranslate1.translateKey("selectWorld.renameButton")));
			this.controlList.Add(new GuiButton(1, this.width / 2 - 100, this.height / 4 + 120 + 12, stringTranslate1.translateKey("gui.cancel")));
			ISaveFormat iSaveFormat2 = this.mc.SaveLoader;
			WorldInfo worldInfo3 = iSaveFormat2.getWorldInfo(this.worldName);
			string string4 = worldInfo3.WorldName;
			this.theGuiTextField = new GuiTextField(this.fontRenderer, this.width / 2 - 100, 60, 200, 20);
			this.theGuiTextField.setFocused(true);
			this.theGuiTextField.Text = string4;
		}

		public override void onGuiClosed()
		{
			mc.mcApplet.EnableKeyRepeatingEvents(false);
		}

		protected internal override void actionPerformed(GuiButton guiButton1)
		{
			if (guiButton1.enabled)
			{
				if (guiButton1.id == 1)
				{
					this.mc.displayGuiScreen(this.parentGuiScreen);
				}
				else if (guiButton1.id == 0)
				{
					ISaveFormat iSaveFormat2 = this.mc.SaveLoader;
					iSaveFormat2.renameWorld(this.worldName, this.theGuiTextField.Text.Trim());
					this.mc.displayGuiScreen(this.parentGuiScreen);
				}

			}
		}

		protected internal override void keyTyped(char c1, int i2)
		{
			this.theGuiTextField.keyTyped(c1, i2);
			((GuiButton)this.controlList[0]).enabled = this.theGuiTextField.Text.Trim().Length > 0;
			if (c1 == (char)13)
			{
				this.actionPerformed((GuiButton)this.controlList[0]);
			}

		}

		protected internal override void mouseClicked(int i1, int i2, int i3)
		{
			base.mouseClicked(i1, i2, i3);
			this.theGuiTextField.mouseClicked(i1, i2, i3);
		}

		public override void drawScreen(int i1, int i2, float f3)
		{
			StringTranslate stringTranslate4 = StringTranslate.Instance;
			this.drawDefaultBackground();
			this.drawCenteredString(this.fontRenderer, stringTranslate4.translateKey("selectWorld.renameTitle"), this.width / 2, this.height / 4 - 60 + 20, 0xFFFFFF);
			this.drawString(this.fontRenderer, stringTranslate4.translateKey("selectWorld.enterName"), this.width / 2 - 100, 47, 10526880);
			this.theGuiTextField.drawTextBox();
			base.drawScreen(i1, i2, f3);
		}
	}

}