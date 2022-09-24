using System;
using System.Threading;

namespace net.minecraft.src
{
	internal class NetworkMasterThread : Thread
	{
		internal readonly NetworkManager netManager;

		internal NetworkMasterThread(NetworkManager networkManager1)
		{
			this.netManager = networkManager1;
		}

		public virtual void run()
		{
			try
			{
				Thread.Sleep(5000L);
				if (NetworkManager.getReadThread(this.netManager).IsAlive)
				{
					try
					{
						NetworkManager.getReadThread(this.netManager).Abort();
					}
					catch (Exception)
					{
					}
				}

				if (NetworkManager.getWriteThread(this.netManager).IsAlive)
				{
					try
					{
						NetworkManager.getWriteThread(this.netManager).Abort();
					}
					catch (Exception)
					{
					}
				}
			}
			catch (InterruptedException interruptedException4)
			{
				Console.WriteLine(interruptedException4.ToString());
				Console.Write(interruptedException4.StackTrace);
			}

		}
	}

}