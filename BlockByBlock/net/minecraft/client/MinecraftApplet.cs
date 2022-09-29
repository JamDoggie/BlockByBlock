using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Desktop;
using System;
using System.ComponentModel;
using System.Threading;

namespace net.minecraft.client
{

	using CanvasMinecraftApplet = net.minecraft.src.CanvasMinecraftApplet;
	using MinecraftAppletImpl = net.minecraft.src.MinecraftAppletImpl;
	using Session = net.minecraft.src.Session;
    
    public class MinecraftApplet : NativeWindow
    {
		private Minecraft mc;
		private Thread mcThread = null;

		protected NativeWindowSettings windowSettings { get; set; }

        public MinecraftApplet(NativeWindowSettings settings) : base(settings)
        {
			windowSettings = settings;
        }

		public virtual void init()
		{
            // PORTING TODO: Launch parameters
			bool fullscreen = false;
			
			mc = new MinecraftAppletImpl(this, this, this, Size.X, Size.Y, fullscreen);
			mc.minecraftUri = "www.minecraft.net";

			mc.session = new Session("Yammy", "");

			// PORTING TODO: may need to set mc.SetServer (?) I don't think so, but check what this does for sure.
		
		}

		public virtual void startMainThread()
		{
			if (mcThread == null)
			{
				mcThread = new Thread(() => mc.run());
				mcThread.Name = "Minecraft main thread";
				mcThread.Start();
			}
		}

        protected override void OnClosing(CancelEventArgs e)
        {
			mc.shutdown();

			try
			{
				mcThread.Join();
			}
			catch (ThreadInterruptedException ex)
			{
				Console.WriteLine(ex.ToString());
				Console.Write(ex.StackTrace);
			}
            
			base.OnClosing(e);

			Environment.Exit(0);
		}

        public virtual void start()
		{
			if (mc != null)
			{
				mc.isGamePaused = false;
			}

		}

		public virtual void stop()
		{
			if (mc != null)
			{
				mc.isGamePaused = true;
			}

		}

		public virtual void destroy()
		{
			shutdown();
		}

		public virtual void shutdown()
		{
			if (mcThread != null)
			{
				mc.shutdown();

				try
				{
					mcThread.Join(10000);
				}
				catch (ThreadInterruptedException)
				{
					try
					{
						mc.shutdownMinecraftApplet();
					}
					catch (Exception exception3)
					{
						Console.WriteLine(exception3.ToString());
						Console.Write(exception3.StackTrace);
					}
				}

				mcThread = null;
			}
		}

		public virtual void clearApplet()
		{
			// PORTING TODO: add any stuff for disposing this window here.
		}
	}
}