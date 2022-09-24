using System;
using System.Threading;

namespace net.minecraft.src
{
	internal class ThreadMonitorConnection : Thread
	{
		internal readonly NetworkManager netManager;

		internal ThreadMonitorConnection(NetworkManager networkManager1)
		{
			this.netManager = networkManager1;
		}

		public virtual void run()
		{
			try
			{
				Thread.Sleep(2000L);
				if (NetworkManager.isRunning(this.netManager))
				{
					NetworkManager.getWriteThread(this.netManager).Interrupt();
					this.netManager.networkShutdown("disconnect.closed", new object[0]);
				}
			}
			catch (Exception exception2)
			{
				Console.WriteLine(exception2.ToString());
				Console.Write(exception2.StackTrace);
			}

		}
	}

}