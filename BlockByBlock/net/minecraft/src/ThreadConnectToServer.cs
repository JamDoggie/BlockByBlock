using System;
using System.Net.Sockets;
using System.Threading;

namespace net.minecraft.src
{

	using Minecraft = net.minecraft.client.Minecraft;

	internal class ThreadConnectToServer
	{
		internal readonly Minecraft mc;
		internal readonly string ip;
		internal readonly int port;
		internal readonly GuiConnecting connectingGui;

		public Thread thread;

		internal ThreadConnectToServer(GuiConnecting guiConnecting1, Minecraft minecraft2, string string3, int i4)
		{
			this.connectingGui = guiConnecting1;
			this.mc = minecraft2;
			this.ip = string3;
			this.port = i4;

			thread = new Thread(() => run());
		}

		public virtual void startThread()
        {
			thread.Start();
        }
        
		protected virtual void run()
		{
			try
			{
				GuiConnecting.setNetClientHandler(this.connectingGui, new NetClientHandler(this.mc, this.ip, this.port));
				if (GuiConnecting.isCancelled(this.connectingGui))
				{
					return;
				}

				GuiConnecting.getNetClientHandler(this.connectingGui).addToSendQueue(new Packet2Handshake(this.mc.session.username, this.ip, this.port));
			}
			catch (SocketException connectException3)
			{
				if (GuiConnecting.isCancelled(this.connectingGui))
				{
					return;
				}

				this.mc.displayGuiScreen(new GuiDisconnected("connect.failed", "disconnect.genericReason", new object[]{connectException3.Message}));
			}
			catch (Exception exception4)
			{
				if (GuiConnecting.isCancelled(this.connectingGui))
				{
					return;
				}

				Console.WriteLine(exception4.ToString());
				Console.Write(exception4.StackTrace);
				this.mc.displayGuiScreen(new GuiDisconnected("connect.failed", "disconnect.genericReason", new object[]{exception4.ToString()}));
			}

		}
	}

}