using System.Threading;

namespace net.minecraft.src
{
	using Minecraft = net.minecraft.client.Minecraft;

	public class ThreadClientSleep : Thread
	{
		internal readonly Minecraft mc;

		public ThreadClientSleep(Minecraft minecraft1, string string2) : base(string2)
		{
			this.mc = minecraft1;
			this.setDaemon(true);
			this.Start();
		}

		public virtual void run()
		{
			while (this.mc.running)
			{
				try
				{
					Thread.Sleep(2147483647L);
				}
				catch (InterruptedException)
				{
				}
			}

		}
	}

}