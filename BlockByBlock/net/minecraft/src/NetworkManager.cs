using System;
using System.Collections;
using System.Threading;

namespace net.minecraft.src
{

	public class NetworkManager
	{
		public static readonly object threadSyncObject = new object();
		public static int numReadThreads;
		public static int numWriteThreads;
		private object sendQueueLock = new object();
		private Socket networkSocket;
		private readonly SocketAddress remoteSocketAddress;
		private DataInputStream socketInputStream;
		private DataOutputStream socketOutputStream;
//JAVA TO C# CONVERTER NOTE: Field name conflicts with a method name of the current type:
		private bool isRunning_Conflict = true;
		private System.Collections.IList readPackets = Collections.synchronizedList(new ArrayList());
		private System.Collections.IList dataPackets = Collections.synchronizedList(new ArrayList());
		private System.Collections.IList chunkDataPackets = Collections.synchronizedList(new ArrayList());
		private NetHandler netHandler;
//JAVA TO C# CONVERTER NOTE: Field name conflicts with a method name of the current type:
		private bool isServerTerminating_Conflict = false;
		private Thread writeThread;
		private Thread readThread;
//JAVA TO C# CONVERTER NOTE: Field name conflicts with a method name of the current type:
		private bool isTerminating_Conflict = false;
		private string terminationReason = "";
		private object[] field_20101_t;
		private int timeSinceLastRead = 0;
		private int sendQueueByteLength = 0;
		public static int[] field_28145_d = new int[256];
		public static int[] field_28144_e = new int[256];
		public int chunkDataSendCounter = 0;
		private int field_20100_w = 50;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public NetworkManager(java.net.Socket socket1, String string2, NetHandler netHandler3) throws java.io.IOException
		public NetworkManager(Socket socket1, string string2, NetHandler netHandler3)
		{
			this.networkSocket = socket1;
			this.remoteSocketAddress = socket1.getRemoteSocketAddress();
			this.netHandler = netHandler3;

			try
			{
				socket1.setSoTimeout(30000);
				socket1.setTrafficClass(24);
			}
			catch (SocketException socketException5)
			{
				Console.Error.WriteLine(socketException5.Message);
			}

			this.socketInputStream = new DataInputStream(socket1.getInputStream());
			this.socketOutputStream = new DataOutputStream(new BufferedOutputStream(socket1.getOutputStream(), 5120));
			this.readThread = new NetworkReaderThread(this, string2 + " read thread");
			this.writeThread = new NetworkWriterThread(this, string2 + " write thread");
			this.readThread.Start();
			this.writeThread.Start();
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

					Packet.writePacket(packet2, this.socketOutputStream);
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

					Packet.writePacket(packet2, this.socketOutputStream);
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
				if (!this.isTerminating_Conflict)
				{
					this.onNetworkError(exception8);
				}

				return false;
			}
		}

		public virtual void wakeThreads()
		{
			this.readThread.Interrupt();
			this.writeThread.Interrupt();
		}

		private bool readPacket()
		{
			bool z1 = false;

			try
			{
				Packet packet2 = Packet.readPacket(this.socketInputStream, this.netHandler.ServerHandler);
				if (packet2 != null)
				{
					int[] i10000 = field_28145_d;
					int i10001 = packet2.PacketId;
					i10000[i10001] += packet2.PacketSize + 1;
					if (!this.isServerTerminating_Conflict)
					{
						this.readPackets.Add(packet2);
					}

					z1 = true;
				}
				else
				{
					this.networkShutdown("disconnect.endOfStream", new object[0]);
				}

				return z1;
			}
			catch (Exception exception3)
			{
				if (!this.isTerminating_Conflict)
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
			this.networkShutdown("disconnect.genericReason", new object[]{"Internal exception: " + exception1.ToString()});
		}

		public virtual void networkShutdown(string string1, params object[] object2)
		{
			if (this.isRunning_Conflict)
			{
				this.isTerminating_Conflict = true;
				this.terminationReason = string1;
				this.field_20101_t = object2;
				(new NetworkMasterThread(this)).Start();
				this.isRunning_Conflict = false;

				try
				{
					this.socketInputStream.close();
					this.socketInputStream = null;
				}
				catch (Exception)
				{
				}

				try
				{
					this.socketOutputStream.close();
					this.socketOutputStream = null;
				}
				catch (Exception)
				{
				}

				try
				{
					this.networkSocket.close();
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
				Packet packet2 = (Packet)this.readPackets.RemoveAndReturn(0);
				packet2.processPacket(this.netHandler);
			}

			this.wakeThreads();
			if (this.isTerminating_Conflict && this.readPackets.Count == 0)
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
				this.readThread.Interrupt();
				(new ThreadMonitorConnection(this)).Start();
			}
		}

		internal static bool isRunning(NetworkManager networkManager0)
		{
			return networkManager0.isRunning_Conflict;
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

		internal static DataOutputStream getOutputStream(NetworkManager networkManager0)
		{
			return networkManager0.socketOutputStream;
		}

		internal static bool isTerminating(NetworkManager networkManager0)
		{
			return networkManager0.isTerminating_Conflict;
		}

		internal static void sendError(NetworkManager networkManager0, Exception exception1)
		{
			networkManager0.onNetworkError(exception1);
		}

		internal static Thread getReadThread(NetworkManager networkManager0)
		{
			return networkManager0.readThread;
		}

		internal static Thread getWriteThread(NetworkManager networkManager0)
		{
			return networkManager0.writeThread;
		}
	}

}