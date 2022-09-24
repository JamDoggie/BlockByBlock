using System.Threading;

namespace net.minecraft.src
{
	internal class NetworkReaderThread : Thread
	{
		internal readonly NetworkManager netManager;

		internal NetworkReaderThread(NetworkManager networkManager1, string string2) : base(string2)
		{
			this.netManager = networkManager1;
		}

		public virtual void run()
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
					if (!NetworkManager.isRunning(this.netManager))
					{
						z12 = false;
						break;
					}

					if (NetworkManager.isServerTerminating(this.netManager))
					{
						z12 = false;
						break;
					}

					while (NetworkManager.readNetworkPacket(this.netManager))
					{
					}

					try
					{
						sleep(2L);
					}
					catch (InterruptedException)
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