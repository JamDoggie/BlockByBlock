using System;
using System.Threading;

namespace net.minecraft.src
{
	internal class NetworkMasterThread : NetworkThread
	{ 
        
		internal NetworkMasterThread(NetworkManager networkManager1, CancellationTokenSource source) : base(networkManager1, source)
		{
		}

        protected override void runThreadLoop()
		{
			try
			{
				Thread.Sleep(5000);
				if (NetworkManager.getReadThread(this.netManager).thread.IsAlive)
				{
					try
					{
						NetworkManager.getReadThread(this.netManager).stopThread();
					}
					catch (Exception)
					{
					}
				}

				if (NetworkManager.getWriteThread(this.netManager).thread.IsAlive)
				{
					try
					{
						NetworkManager.getWriteThread(this.netManager).stopThread();
					}
					catch (Exception)
					{
					}
				}
			}
			catch (ThreadInterruptedException interruptedException4)
			{
				Console.WriteLine(interruptedException4.ToString());
				Console.Write(interruptedException4.StackTrace);
			}

		}
	}

}