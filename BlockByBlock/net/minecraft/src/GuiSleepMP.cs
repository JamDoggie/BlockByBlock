using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class GuiSleepMP : GuiChat
	{
		public override void initGui()
		{
			base.initGui();
			StringTranslate stringTranslate1 = StringTranslate.Instance;
			this.controlList.Add(new GuiButton(1, this.width / 2 - 100, this.height - 40, stringTranslate1.translateKey("multiplayer.stopSleeping")));
		}

		protected internal override void keyTyped(char c1, int i2)
		{
			if (i2 == 1)
			{
				this.wakeEntity();
			}
			else if (i2 == 28)
			{
				string string3 = this.field_50064_a.Text.Trim();
				if (string3.Length > 0)
				{
					this.mc.thePlayer.sendChatMessage(string3);
				}

				this.field_50064_a.Text = "";
				this.mc.ingameGUI.func_50014_d();
			}
			else
			{
				base.keyTyped(c1, i2);
			}

		}

		protected internal override void actionPerformed(GuiButton guiButton1)
		{
			if (guiButton1.id == 1)
			{
				this.wakeEntity();
			}
			else
			{
				base.actionPerformed(guiButton1);
			}

		}

		private void wakeEntity()
		{
			if (this.mc.thePlayer is EntityClientPlayerMP)
			{
				NetClientHandler netClientHandler1 = ((EntityClientPlayerMP)this.mc.thePlayer).sendQueue;
				netClientHandler1.addToSendQueue(new Packet19EntityAction(this.mc.thePlayer, 3));
			}

		}
	}

}