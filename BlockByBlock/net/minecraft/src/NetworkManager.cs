using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace net.minecraft.src
{

	public class NetworkManager
	{
		public static readonly object threadSyncObject = new object();
		public static int numReadThreads;
		public static int numWriteThreads;
		private object sendQueueLock = new object();
		private Socket? networkSocket;
		private NetworkStream networkStream;
		private BinaryReader? socketInputStream;
		private BinaryWriter? socketOutputStream;
		private bool isRunning = true;
		private List<Packet> readPackets = new();
		private List<Packet> dataPackets = new();
		private List<Packet> chunkDataPackets = new();
		private NetHandler netHandler;
		private bool isServerTerminating_Conflict = false;
		private NetworkWriterThread writeThread;
		private NetworkReaderThread readThread;
		private bool isTerminating = false;
		private string terminationReason = "";
		private object[] field_20101_t;
		private int timeSinceLastRead = 0;
		private int sendQueueByteLength = 0;
		public static int[] field_28145_d = new int[256];
		public static int[] field_28144_e = new int[256];
		public int chunkDataSendCounter = 0;
		private int field_20100_w = 50;

		public NetworkManager(Socket socket1, string string2, NetHandler netHandler3)
		{
			this.networkSocket = socket1;
			this.netHandler = netHandler3;

			try
			{
				socket1.ReceiveTimeout = 30000;
			}
			catch (SocketException socketException5)
			{
				Console.Error.WriteLine(socketException5.Message);
			}

			networkStream = new NetworkStream(socket1, true);

            socketInputStream = new BinaryReader(networkStream);
            socketOutputStream = new BinaryWriter(networkStream);

            readThread = new NetworkReaderThread(this, new CancellationTokenSource(), string2 + " read thread");
			writeThread = new NetworkWriterThread(this, new CancellationTokenSource(), string2 + " write thread");
			readThread.startThread();
			writeThread.startThread();
		}

		public virtual void addToSendQueue(Packet packet1)
		{
			if (!this.isServerTerminating_Conflict)
			{
				object object2 = this.sendQueueLock;
				lock (this.sendQueueLock)
				{
					this.sendQueueByteLength += packet1.PacketSize + 1;
					if (packet1.isChunkDataPacket)
					{
						this.chunkDataPackets.Add(packet1);
					}
					else
					{
						this.dataPackets.Add(packet1);
					}

				}
			}
		}
		private bool sendPacket()
		{
			bool z1 = false;
			
			try
			{
				int[] i10000;
				int i10001;
				Packet packet2;
				object object3;
				if (this.dataPackets.Count > 0 && (this.chunkDataSendCounter == 0 || DateTimeHelper.CurrentUnixTimeMillis() - ((Packet)this.dataPackets[0]).creationTimeMillis >= (long)this.chunkDataSendCounter))
				{
					object3 = this.sendQueueLock;
					lock (this.sendQueueLock)
					{
						packet2 = (Packet)this.dataPackets.RemoveAndReturn(0);
						this.sendQueueByteLength -= packet2.PacketSize + 1;
					}

					Packet.writePacket(packet2, socketOutputStream);
					i10000 = field_28144_e;
					i10001 = packet2.PacketId;
					i10000[i10001] += packet2.PacketSize + 1;
					z1 = true;
				}

				if (this.field_20100_w-- <= 0 && this.chunkDataPackets.Count > 0 && (this.chunkDataSendCounter == 0 || DateTimeHelper.CurrentUnixTimeMillis() - ((Packet)this.chunkDataPackets[0]).creationTimeMillis >= (long)this.chunkDataSendCounter))
				{
					object3 = this.sendQueueLock;
					lock (this.sendQueueLock)
					{
						packet2 = (Packet)this.chunkDataPackets.RemoveAndReturn(0);
						this.sendQueueByteLength -= packet2.PacketSize + 1;
					}

					Packet.writePacket(packet2, socketOutputStream);
					i10000 = field_28144_e;
					i10001 = packet2.PacketId;
					i10000[i10001] += packet2.PacketSize + 1;
					this.field_20100_w = 0;
					z1 = true;
				}

				return z1;
			}
			catch (Exception exception8)
			{
				if (!this.isTerminating)
				{
					this.onNetworkError(exception8);
				}

				return false;
			}
		}

		public virtual void wakeThreads()
		{
			//this.readThread.thread.Interrupt();
			//this.writeThread.thread.Interrupt();
		}
        
		private bool readPacket()
		{
			bool z1 = false;

            try
			{


					Packet packet2 = Packet.ReadPacket(this.socketInputStream, this.netHandler.ServerHandler);
					if (packet2 != null)
					{
						int[] i10000 = field_28145_d;
						int i10001 = packet2.PacketId;
						i10000[i10001] += packet2.PacketSize + 1;
						if (!isServerTerminating_Conflict)
						{
							lock (threadSyncObject)
							{
								readPackets.Add(packet2);
							}
						}
                        
						z1 = true;
					}
					else
					{
						networkShutdown("disconnect.endOfStream", new object[0]);
					}
				
				return z1;
			}
			catch (Exception exception3)
			{


				if (!this.isTerminating)
				{
					this.onNetworkError(exception3);
				}

				return false;
			}
		}
        

		private void onNetworkError(Exception exception1)
		{
			Console.WriteLine(exception1.ToString());
			Console.Write(exception1.StackTrace);
			this.networkShutdown("disconnect.genericReason", new object[]{"Internal exception: " + exception1.Message});
		}

		public virtual void networkShutdown(string string1, params object[] object2)
		{
			if (this.isRunning)
			{
				this.isTerminating = true;
				this.terminationReason = string1;
				this.field_20101_t = object2;
				(new NetworkMasterThread(this, new CancellationTokenSource())).startThread();
				this.isRunning = false;

				try
				{
					this.socketInputStream.Dispose();
					this.socketInputStream = null;
				}
				catch (Exception)
				{
				}

				try
				{
					this.socketOutputStream.Dispose();
					this.socketOutputStream = null;
				}
				catch (Exception)
				{
				}

				try
				{
					this.networkSocket.Dispose();
					this.networkSocket = null;
				}
				catch (Exception)
				{
				}

			}
		}

		public virtual void processReadPackets()
		{
			if (this.sendQueueByteLength > 1048576)
			{
				this.networkShutdown("disconnect.overflow", new object[0]);
			}

			if (this.readPackets.Count == 0)
			{
				if (this.timeSinceLastRead++ == 1200)
				{
					this.networkShutdown("disconnect.timeout", new object[0]);
				}
			}
			else
			{
				this.timeSinceLastRead = 0;
			}

			int i1 = 1000;

			while (this.readPackets.Count > 0 && i1-- >= 0)
			{
				Packet? packet2 = (Packet?)this.readPackets.RemoveAndReturn(0);

                if (packet2 != null)
                    packet2.processPacket(this.netHandler);
			}

			this.wakeThreads();
			if (this.isTerminating && this.readPackets.Count == 0)
			{
				this.netHandler.handleErrorMessage(this.terminationReason, this.field_20101_t);
			}

		}

		public virtual void serverShutdown()
		{
			if (!this.isServerTerminating_Conflict)
			{
				this.wakeThreads();
				this.isServerTerminating_Conflict = true;
				this.readThread.thread.Interrupt();
				(new ThreadMonitorConnection(this, new CancellationTokenSource())).startThread();
			}
		}

		internal static bool getIsRunning(NetworkManager networkManager0)
		{
			return networkManager0.isRunning;
		}

		internal static bool isServerTerminating(NetworkManager networkManager0)
		{
			return networkManager0.isServerTerminating_Conflict;
		}

		internal static bool readNetworkPacket(NetworkManager networkManager0)
		{
			return networkManager0.readPacket();
		}

		internal static bool sendNetworkPacket(NetworkManager networkManager0)
		{
			return networkManager0.sendPacket();
		}

		internal static BinaryWriter? getOutputStream(NetworkManager networkManager0)
		{
			return networkManager0.socketOutputStream;
		}

		internal static NetworkStream? getNetworkStream(NetworkManager networkManager0)
		{
			return networkManager0.networkStream;
		}

		internal static bool getIsTerminating(NetworkManager networkManager0)
		{
			return networkManager0.isTerminating;
		}

		internal static void sendError(NetworkManager networkManager0, Exception exception1)
		{
			networkManager0.onNetworkError(exception1);
		}

		internal static NetworkThread getReadThread(NetworkManager networkManager0)
		{
			return networkManager0.readThread;
		}

		internal static NetworkThread getWriteThread(NetworkManager networkManager0)
		{
			return networkManager0.writeThread;
		}
	}

}