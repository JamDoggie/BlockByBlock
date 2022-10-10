using System;
using System.Threading;

namespace net.minecraft.src
{

	internal class NetworkWriterThread : NetworkThread
	{

		internal NetworkWriterThread(NetworkManager networkManager1, CancellationTokenSource source, string string2) : base(networkManager1, source)
		{
			
		}

		protected override void runThreadLoop()
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
					if (!NetworkManager.getIsRunning(this.netManager))
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
							//NetworkManager.getOutputStream(netManager).Flush();
						}
					}
					catch (IOException iOException18)
					{
						if (!NetworkManager.getIsTerminating(this.netManager))
						{
							NetworkManager.sendError(this.netManager, iOException18);
						}

						Console.WriteLine(iOException18.ToString());
						Console.Write(iOException18.StackTrace);
					}

					if (tokenSource.Token.IsCancellationRequested)
						break;

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