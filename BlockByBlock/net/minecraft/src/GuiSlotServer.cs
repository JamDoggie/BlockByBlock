using net.minecraft.client;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{

	internal class GuiSlotServer : GuiSlot
	{
		internal readonly GuiMultiplayer parentGui;

		public GuiSlotServer(GuiMultiplayer guiMultiplayer1) : base(guiMultiplayer1.mc, guiMultiplayer1.width, guiMultiplayer1.height, 32, guiMultiplayer1.height - 64, 36)
		{
			this.parentGui = guiMultiplayer1;
		}

		protected internal override int Size
		{
			get
			{
				return GuiMultiplayer.getServerList(this.parentGui).Count;
			}
		}

		protected internal override void elementClicked(int i1, bool z2)
		{
			GuiMultiplayer.setSelectedServer(this.parentGui, i1);
			bool z3 = GuiMultiplayer.getSelectedServer(this.parentGui) >= 0 && GuiMultiplayer.getSelectedServer(this.parentGui) < this.Size;
			GuiMultiplayer.getButtonSelect(this.parentGui).enabled = z3;
			GuiMultiplayer.getButtonEdit(this.parentGui).enabled = z3;
			GuiMultiplayer.getButtonDelete(this.parentGui).enabled = z3;
			if (z2 && z3)
			{
				GuiMultiplayer.joinServer(this.parentGui, i1);
			}

		}

		protected internal override bool isSelected(int i1)
		{
			return i1 == GuiMultiplayer.getSelectedServer(this.parentGui);
		}

		protected internal override int ContentHeight
		{
			get
			{
				return GuiMultiplayer.getServerList(this.parentGui).Count * 36;
			}
		}

		protected internal override void drawBackground()
		{
			this.parentGui.drawDefaultBackground();
		}

		protected internal override void drawSlot(int i1, int i2, int i3, int i4, Tessellator tessellator5)
		{
			ServerNBTStorage serverNBTStorage6 = (ServerNBTStorage)GuiMultiplayer.getServerList(this.parentGui)[i1];
			lock (GuiMultiplayer.Lock)
			{
				if (GuiMultiplayer.ThreadsPending < 5 && !serverNBTStorage6.polled)
				{
					serverNBTStorage6.polled = true;
					serverNBTStorage6.lag = -2L;
					serverNBTStorage6.motd = "";
					serverNBTStorage6.playerCount = "";
					GuiMultiplayer.incrementThreadsPending();
					(new ThreadPollServers(this, serverNBTStorage6)).startThread();
				}
			}

			this.parentGui.drawString(this.parentGui.fontRenderer, serverNBTStorage6.name, i2 + 2, i3 + 1, 0xFFFFFF);
			this.parentGui.drawString(this.parentGui.fontRenderer, serverNBTStorage6.motd, i2 + 2, i3 + 12, 8421504);
			this.parentGui.drawString(this.parentGui.fontRenderer, serverNBTStorage6.playerCount, i2 + 215 - this.parentGui.fontRenderer.getStringWidth(serverNBTStorage6.playerCount), i3 + 12, 8421504);
			this.parentGui.drawString(this.parentGui.fontRenderer, serverNBTStorage6.host, i2 + 2, i3 + 12 + 11, 3158064);
            Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
			this.parentGui.mc.renderEngine.bindTexture(this.parentGui.mc.renderEngine.getTexture("/gui/icons.png"));
			string string9 = "";
			sbyte b7;
			int i8;
			if (serverNBTStorage6.polled && serverNBTStorage6.lag != -2L)
			{
				b7 = 0;
				bool z12 = false;
				if (serverNBTStorage6.lag < 0L)
				{
					i8 = 5;
				}
				else if (serverNBTStorage6.lag < 150L)
				{
					i8 = 0;
				}
				else if (serverNBTStorage6.lag < 300L)
				{
					i8 = 1;
				}
				else if (serverNBTStorage6.lag < 600L)
				{
					i8 = 2;
				}
				else if (serverNBTStorage6.lag < 1000L)
				{
					i8 = 3;
				}
				else
				{
					i8 = 4;
				}

				if (serverNBTStorage6.lag < 0L)
				{
					string9 = "(no connection)";
				}
				else
				{
					string9 = serverNBTStorage6.lag + "ms";
				}
			}
			else
			{
				b7 = 1;
				i8 = (int)(DateTimeHelper.CurrentUnixTimeMillis() / 100L + (long)(i1 * 2) & 7L);
				if (i8 > 4)
				{
					i8 = 8 - i8;
				}

				string9 = "Polling..";
			}

			this.parentGui.drawTexturedModalRect(i2 + 205, i3, 0 + b7 * 10, 176 + i8 * 8, 10, 8);
			sbyte b10 = 4;
			if (this.mouseX >= i2 + 205 - b10 && this.mouseY >= i3 - b10 && this.mouseX <= i2 + 205 + 10 + b10 && this.mouseY <= i3 + 8 + b10)
			{
				GuiMultiplayer.setTooltipText(this.parentGui, string9);
			}

		}
	}

}