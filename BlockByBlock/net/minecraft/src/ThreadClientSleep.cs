using System.Threading;

namespace net.minecraft.src
{
	using Minecraft = net.minecraft.client.Minecraft;

	public class ThreadClientSleep // wtf is this class?
		// PORTING TODO: I don't think this class is actually necessary, I think it's a workaround for a Java issue. Check back on this.
	{
		internal readonly Minecraft mc;

		private Thread thread;

		public ThreadClientSleep(Minecraft minecraft1, string string2)
		{
			this.mc = minecraft1;
			thread = new Thread(() => run());
			thread.IsBackground = true;
			Start();
		}

		public virtual void Start()
        {
			thread.Start();
        }

		public virtual void run()
		{
			while (mc.running)
			{
				try
				{
					Thread.Sleep(2147483647);
				}
				catch (ThreadInterruptedException)
				{
				}
			}

		}
	}

}