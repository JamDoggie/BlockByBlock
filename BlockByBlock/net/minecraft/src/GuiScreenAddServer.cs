namespace net.minecraft.src
{

	public class GuiScreenAddServer : GuiScreen
	{
		private GuiScreen parentGui;
		private GuiTextField serverAddress;
		private GuiTextField serverName;
		private ServerNBTStorage serverNBTStorage;

		public GuiScreenAddServer(GuiScreen guiScreen1, ServerNBTStorage serverNBTStorage2)
		{
			this.parentGui = guiScreen1;
			this.serverNBTStorage = serverNBTStorage2;
		}

		public override void updateScreen()
		{
			this.serverName.updateCursorCounter();
			this.serverAddress.updateCursorCounter();
		}

		public override void initGui()
		{
			StringTranslate stringTranslate1 = StringTranslate.Instance;
			mc.mcApplet.EnableKeyRepeatingEvents(true);
			this.controlList.Clear();
			this.controlList.Add(new GuiButton(0, this.width / 2 - 100, this.height / 4 + 96 + 12, stringTranslate1.translateKey("addServer.add")));
			this.controlList.Add(new GuiButton(1, this.width / 2 - 100, this.height / 4 + 120 + 12, stringTranslate1.translateKey("gui.cancel")));
			this.serverName = new GuiTextField(this.fontRenderer, this.width / 2 - 100, 76, 200, 20);
			this.serverName.setFocused(true);
			this.serverName.Text = this.serverNBTStorage.name;
			this.serverAddress = new GuiTextField(this.fontRenderer, this.width / 2 - 100, 116, 200, 20);
			this.serverAddress.MaxStringLength = 128;
			this.serverAddress.Text = this.serverNBTStorage.host;
			((GuiButton)this.controlList[0]).enabled = this.serverAddress.Text.Length > 0 && this.serverAddress.Text.Split(":", true).Length > 0 && this.serverName.Text.Length > 0;
		}

		public override void onGuiClosed()
		{
			mc.mcApplet.EnableKeyRepeatingEvents(true);
		}

		protected internal override void actionPerformed(GuiButton guiButton1)
		{
			if (guiButton1.enabled)
			{
				if (guiButton1.id == 1)
				{
					this.parentGui.confirmClicked(false, 0);
				}
				else if (guiButton1.id == 0)
				{
					this.serverNBTStorage.name = this.serverName.Text;
					this.serverNBTStorage.host = this.serverAddress.Text;
					this.parentGui.confirmClicked(true, 0);
				}

			}
		}

		protected internal override void keyTyped(char c1, int i2)
		{
			this.serverName.keyTyped(c1, i2);
			this.serverAddress.keyTyped(c1, i2);
			if (c1 == (char)9)
			{
				if (this.serverName.func_50025_j())
				{
					this.serverName.setFocused(false);
					this.serverAddress.setFocused(true);
				}
				else
				{
					this.serverName.setFocused(true);
					this.serverAddress.setFocused(false);
				}
			}

			if (c1 == (char)13)
			{
				this.actionPerformed((GuiButton)this.controlList[0]);
			}

			((GuiButton)this.controlList[0]).enabled = this.serverAddress.Text.Length > 0 && this.serverAddress.Text.Split(":", true).Length > 0 && this.serverName.Text.Length > 0;
			if (((GuiButton)this.controlList[0]).enabled)
			{
				string string3 = this.serverAddress.Text.Trim();
				string[] string4 = string3.Split(":", true);
				if (string4.Length > 2)
				{
					((GuiButton)this.controlList[0]).enabled = false;
				}
			}

		}

		protected internal override void mouseClicked(int i1, int i2, int i3)
		{
			base.mouseClicked(i1, i2, i3);
			this.serverAddress.mouseClicked(i1, i2, i3);
			this.serverName.mouseClicked(i1, i2, i3);
		}

		public override void drawScreen(int i1, int i2, float f3)
		{
			StringTranslate stringTranslate4 = StringTranslate.Instance;
			this.drawDefaultBackground();
			this.drawCenteredString(this.fontRenderer, stringTranslate4.translateKey("addServer.title"), this.width / 2, this.height / 4 - 60 + 20, 0xFFFFFF);
			this.drawString(this.fontRenderer, stringTranslate4.translateKey("addServer.enterName"), this.width / 2 - 100, 63, 10526880);
			this.drawString(this.fontRenderer, stringTranslate4.translateKey("addServer.enterIp"), this.width / 2 - 100, 104, 10526880);
			this.serverName.drawTextBox();
			this.serverAddress.drawTextBox();
			base.drawScreen(i1, i2, f3);
		}
	}

}