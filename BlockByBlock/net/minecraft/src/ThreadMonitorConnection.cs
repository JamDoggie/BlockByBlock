using System;
using System.Threading;

namespace net.minecraft.src
{
	internal class ThreadMonitorConnection : NetworkThread
	{
		internal readonly NetworkManager netManager;

		internal ThreadMonitorConnection(NetworkManager networkManager1, CancellationTokenSource source) : base(networkManager1, source)
		{
			netManager = networkManager1;
		}

		protected virtual void run()
		{
			try
			{
				Thread.Sleep(2000);
				if (NetworkManager.getIsRunning(netManager))
				{
					NetworkManager.getWriteThread(netManager).thread.Interrupt();
					netManager.networkShutdown("disconnect.closed", new object[0]);
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