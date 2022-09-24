using System;
using System.Threading;

namespace net.minecraft.src
{

	internal class NetworkWriterThread : Thread
	{
		internal readonly NetworkManager netManager;

		internal NetworkWriterThread(NetworkManager networkManager1, string string2) : base(string2)
		{
			this.netManager = networkManager1;
		}

		public virtual void run()
		{
			object object1 = NetworkManager.threadSyncObject;
			lock (NetworkManager.threadSyncObject)
			{
				++NetworkManager.numWriteThreads;
			}

			while (true)
			{
				bool z13 = false;

				try
				{
					z13 = true;
					if (!NetworkManager.isRunning(this.netManager))
					{
						z13 = false;
						break;
					}

					while (NetworkManager.sendNetworkPacket(this.netManager))
					{
					}

					try
					{
						if (NetworkManager.getOutputStream(this.netManager) != null)
						{
							NetworkManager.getOutputStream(this.netManager).flush();
						}
					}
					catch (IOException iOException18)
					{
						if (!NetworkManager.isTerminating(this.netManager))
						{
							NetworkManager.sendError(this.netManager, iOException18);
						}

						Console.WriteLine(iOException18.ToString());
						Console.Write(iOException18.StackTrace);
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
					if (z13)
					{
						object object5 = NetworkManager.threadSyncObject;
						lock (NetworkManager.threadSyncObject)
						{
							--NetworkManager.numWriteThreads;
						}
					}
				}
			}

			object1 = NetworkManager.threadSyncObject;
			lock (NetworkManager.threadSyncObject)
			{
				--NetworkManager.numWriteThreads;
			}
		}
	}

}