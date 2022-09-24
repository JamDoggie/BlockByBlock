using System;
using System.Threading;

namespace net.minecraft.client
{

	using CanvasMinecraftApplet = net.minecraft.src.CanvasMinecraftApplet;
	using MinecraftAppletImpl = net.minecraft.src.MinecraftAppletImpl;
	using Session = net.minecraft.src.Session;

	public class MinecraftApplet : Applet
	{
		private Canvas mcCanvas;
		private Minecraft mc;
		private Thread mcThread = null;

		public virtual void init()
		{
			this.mcCanvas = new CanvasMinecraftApplet(this);
			bool z1 = false;
			if (this.getParameter("fullscreen") != null)
			{
				z1 = this.getParameter("fullscreen").equalsIgnoreCase("true");
			}

			this.mc = new MinecraftAppletImpl(this, this, this.mcCanvas, this, this.getWidth(), this.getHeight(), z1);
			this.mc.minecraftUri = this.getDocumentBase().getHost();
			if (this.getDocumentBase().getPort() > 0)
			{
				this.mc.minecraftUri = this.mc.minecraftUri + ":" + this.getDocumentBase().getPort();
			}

			if (this.getParameter("username") != null && this.getParameter("sessionid") != null)
			{
				this.mc.session = new Session(this.getParameter("username"), this.getParameter("sessionid"));
				Console.WriteLine("Setting user: " + this.mc.session.username + ", " + this.mc.session.sessionId);
				if (this.getParameter("mppass") != null)
				{
					this.mc.session.mpPassParameter = this.getParameter("mppass");
				}
			}
			else
			{
				this.mc.session = new Session("Player", "");
			}

			if (this.getParameter("server") != null && this.getParameter("port") != null)
			{
				this.mc.setServer(this.getParameter("server"), int.Parse(this.getParameter("port")));
			}

			this.mc.hideQuitButton = true;
			if ("true".Equals(this.getParameter("stand-alone")))
			{
				this.mc.hideQuitButton = false;
			}

			this.setLayout(new BorderLayout());
			this.add(this.mcCanvas, "Center");
			this.mcCanvas.setFocusable(true);
			this.validate();
		}

		public virtual void startMainThread()
		{
			if (this.mcThread == null)
			{
				this.mcThread = new Thread(this.mc, "Minecraft main thread");
				this.mcThread.Start();
			}
		}

		public virtual void start()
		{
			if (this.mc != null)
			{
				this.mc.isGamePaused = false;
			}

		}

		public virtual void stop()
		{
			if (this.mc != null)
			{
				this.mc.isGamePaused = true;
			}

		}

		public virtual void destroy()
		{
			this.shutdown();
		}

		public virtual void shutdown()
		{
			if (this.mcThread != null)
			{
				this.mc.shutdown();

				try
				{
					this.mcThread.Join(10000L);
				}
				catch (InterruptedException)
				{
					try
					{
						this.mc.shutdownMinecraftApplet();
					}
					catch (Exception exception3)
					{
						Console.WriteLine(exception3.ToString());
						Console.Write(exception3.StackTrace);
					}
				}

				this.mcThread = null;
			}
		}

		public virtual void clearApplet()
		{
			this.mcCanvas = null;
			this.mc = null;
			this.mcThread = null;

			try
			{
				this.removeAll();
				this.validate();
			}
			catch (Exception)
			{
			}

		}
	}

}