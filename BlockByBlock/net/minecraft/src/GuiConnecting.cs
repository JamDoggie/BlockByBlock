using System;

namespace net.minecraft.src
{
	using Minecraft = net.minecraft.client.Minecraft;

	public class GuiConnecting : GuiScreen
	{
		private NetClientHandler clientHandler;
		private bool cancelled = false;

		public GuiConnecting(Minecraft minecraft1, string string2, int i3)
		{
			Console.WriteLine("Connecting to " + string2 + ", " + i3);
			minecraft1.changeWorld1(null);
			(new ThreadConnectToServer(this, minecraft1, string2, i3)).startThread();
		}

		public override void updateScreen()
		{
			if (this.clientHandler != null)
			{
				this.clientHandler.processReadPackets();
			}

		}

		protected internal override void keyTyped(char c1, int i2)
		{
		}

		public override void initGui()
		{
			StringTranslate stringTranslate1 = StringTranslate.Instance;
			this.controlList.Clear();
			this.controlList.Add(new GuiButton(0, this.width / 2 - 100, this.height / 4 + 120 + 12, stringTranslate1.translateKey("gui.cancel")));
		}

		protected internal override void actionPerformed(GuiButton guiButton1)
		{
			if (guiButton1.id == 0)
			{
				this.cancelled = true;
				if (this.clientHandler != null)
				{
					this.clientHandler.disconnect();
				}

				this.mc.displayGuiScreen(new GuiMainMenu());
			}

		}

		public override void drawScreen(int i1, int i2, float f3)
		{
			this.drawDefaultBackground();
			StringTranslate stringTranslate4 = StringTranslate.Instance;
			if (this.clientHandler == null)
			{
				this.drawCenteredString(this.fontRenderer, stringTranslate4.translateKey("connect.connecting"), this.width / 2, this.height / 2 - 50, 0xFFFFFF);
				this.drawCenteredString(this.fontRenderer, "", this.width / 2, this.height / 2 - 10, 0xFFFFFF);
			}
			else
			{
				this.drawCenteredString(this.fontRenderer, stringTranslate4.translateKey("connect.authorizing"), this.width / 2, this.height / 2 - 50, 0xFFFFFF);
				this.drawCenteredString(this.fontRenderer, this.clientHandler.field_1209_a, this.width / 2, this.height / 2 - 10, 0xFFFFFF);
			}

			base.drawScreen(i1, i2, f3);
		}

		internal static NetClientHandler setNetClientHandler(GuiConnecting guiConnecting0, NetClientHandler netClientHandler1)
		{
			return guiConnecting0.clientHandler = netClientHandler1;
		}

		internal static bool isCancelled(GuiConnecting guiConnecting0)
		{
			return guiConnecting0.cancelled;
		}

		internal static NetClientHandler getNetClientHandler(GuiConnecting guiConnecting0)
		{
			return guiConnecting0.clientHandler;
		}
	}

}