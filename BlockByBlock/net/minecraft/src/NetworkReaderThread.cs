using System.Threading;

namespace net.minecraft.src
{
	internal class NetworkReaderThread : NetworkThread
	{

        internal NetworkReaderThread(NetworkManager networkManager1, CancellationTokenSource tokenSource, string string2) : base(networkManager1, tokenSource)
		{
		}

        protected override void runThreadLoop()
		{
			object object1 = NetworkManager.threadSyncObject;
			lock (NetworkManager.threadSyncObject)
			{
				++NetworkManager.numReadThreads;
			}

			while (true)
			{
				bool z12 = false;

				try
				{
					z12 = true;
					if (!NetworkManager.getIsRunning(this.netManager))
					{
						z12 = false;
						break;
					}

					if (NetworkManager.isServerTerminating(this.netManager))
					{
						z12 = false;
						break;
					}

					if (tokenSource.Token.IsCancellationRequested)
						break;

					while (NetworkManager.readNetworkPacket(this.netManager))
					{
					}

					try
					{
						Thread.Sleep(2);
					}
					catch (ThreadInterruptedException)
					{
					}
				}
				finally
				{
					if (z12)
					{
						object object5 = NetworkManager.threadSyncObject;
						lock (NetworkManager.threadSyncObject)
						{
							--NetworkManager.numReadThreads;
						}
					}
				}
			}

			object1 = NetworkManager.threadSyncObject;
			lock (NetworkManager.threadSyncObject)
			{
				--NetworkManager.numReadThreads;
			}
		}
	}

}