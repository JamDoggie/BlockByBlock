namespace net.minecraft.src
{

	public class GuiScreenServerList : GuiScreen
	{
		private static string field_52009_d = "";
		private readonly GuiScreen guiScreen;
		private readonly ServerNBTStorage serverListStorage;
		private GuiTextField serverTextField;

		public GuiScreenServerList(GuiScreen guiScreen1, ServerNBTStorage serverNBTStorage2)
		{
			this.guiScreen = guiScreen1;
			this.serverListStorage = serverNBTStorage2;
		}

		public override void updateScreen()
		{
			this.serverTextField.updateCursorCounter();
		}

		public override void initGui()
		{
			StringTranslate stringTranslate1 = StringTranslate.Instance;
			mc.mcApplet.EnableKeyRepeatingEvents(true);
			this.controlList.Clear();
			this.controlList.Add(new GuiButton(0, this.width / 2 - 100, this.height / 4 + 96 + 12, stringTranslate1.translateKey("selectServer.select")));
			this.controlList.Add(new GuiButton(1, this.width / 2 - 100, this.height / 4 + 120 + 12, stringTranslate1.translateKey("gui.cancel")));
			this.serverTextField = new GuiTextField(this.fontRenderer, this.width / 2 - 100, 116, 200, 20);
			this.serverTextField.MaxStringLength = 128;
			this.serverTextField.setFocused(true);
			this.serverTextField.Text = field_52009_d;
			((GuiButton)this.controlList[0]).enabled = this.serverTextField.Text.Length > 0 && this.serverTextField.Text.Split(":", true).Length > 0;
		}

		public override void onGuiClosed()
		{
			mc.mcApplet.EnableKeyRepeatingEvents(true);
			field_52009_d = this.serverTextField.Text;
		}

		protected internal override void actionPerformed(GuiButton guiButton1)
		{
			if (guiButton1.enabled)
			{
				if (guiButton1.id == 1)
				{
					this.guiScreen.confirmClicked(false, 0);
				}
				else if (guiButton1.id == 0)
				{
					this.serverListStorage.host = this.serverTextField.Text;
					this.guiScreen.confirmClicked(true, 0);
				}

			}
		}

		protected internal override void keyTyped(char c1, int i2)
		{
			this.serverTextField.keyTyped(c1, i2);
			if (c1 == (char)28)
			{
				this.actionPerformed((GuiButton)this.controlList[0]);
			}

			((GuiButton)this.controlList[0]).enabled = this.serverTextField.Text.Length > 0 && this.serverTextField.Text.Split(":", true).Length > 0;
		}

		protected internal override void mouseClicked(int i1, int i2, int i3)
		{
			base.mouseClicked(i1, i2, i3);
			this.serverTextField.mouseClicked(i1, i2, i3);
		}

		public override void drawScreen(int i1, int i2, float f3)
		{
			StringTranslate stringTranslate4 = StringTranslate.Instance;
			this.drawDefaultBackground();
			this.drawCenteredString(this.fontRenderer, stringTranslate4.translateKey("selectServer.direct"), this.width / 2, this.height / 4 - 60 + 20, 0xFFFFFF);
			this.drawString(this.fontRenderer, stringTranslate4.translateKey("addServer.enterIp"), this.width / 2 - 100, 100, 10526880);
			this.serverTextField.drawTextBox();
			base.drawScreen(i1, i2, f3);
		}
	}

}