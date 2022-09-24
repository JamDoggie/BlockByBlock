namespace net.minecraft.src
{
	public class GuiDownloadTerrain : GuiScreen
	{
		private NetClientHandler netHandler;
		private int updateCounter = 0;

		public GuiDownloadTerrain(NetClientHandler netClientHandler1)
		{
			this.netHandler = netClientHandler1;
		}

		protected internal override void keyTyped(char c1, int i2)
		{
		}

		public override void initGui()
		{
			this.controlList.Clear();
		}

		public override void updateScreen()
		{
			++this.updateCounter;
			if (this.updateCounter % 20 == 0)
			{
				this.netHandler.addToSendQueue(new Packet0KeepAlive());
			}

			if (this.netHandler != null)
			{
				this.netHandler.processReadPackets();
			}

		}

		protected internal override void actionPerformed(GuiButton guiButton1)
		{
		}

		public override void drawScreen(int i1, int i2, float f3)
		{
			this.drawBackground(0);
			StringTranslate stringTranslate4 = StringTranslate.Instance;
			this.drawCenteredString(this.fontRenderer, stringTranslate4.translateKey("multiplayer.downloadingTerrain"), this.width / 2, this.height / 2 - 50, 0xFFFFFF);
			base.drawScreen(i1, i2, f3);
		}
	}

}