using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public abstract class Packet
	{
		public static IntHashMap packetIdToClassMap = new IntHashMap();
		private static System.Collections.IDictionary packetClassToIdMap = new Hashtable();
		private static ISet<object> ClientPacketIdList = new HashSet<object>();
		private static ISet<object> ServerPacketIdList = new HashSet<object>();
		public readonly long creationTimeMillis = DateTimeHelper.CurrentUnixTimeMillis();
		public static long networkTotalReadPackets;
		public static long networkTotalReadBytes;
		public static long networkTotalWrittenPackets;
		public static long networkTotalWrittenBytes;
		public bool isChunkDataPacket = false;

		internal static void AddIdClassMapping(int i0, bool z1, bool z2, Type class3)
		{
			if (packetIdToClassMap.containsItem(i0))
			{
				throw new ArgumentException("Duplicate packet id:" + i0);
			}
			else if (packetClassToIdMap.Contains(class3))
			{
				throw new ArgumentException("Duplicate packet class:" + class3);
			}
			else
			{
				packetIdToClassMap.addKey(i0, class3);
				packetClassToIdMap[class3] = i0;
				if (z1)
				{
					ClientPacketIdList.Add(i0);
				}

				if (z2)
				{
					ServerPacketIdList.Add(i0);
				}

			}
		}

		public static Packet GetNewPacket(int i0)
		{
			try
			{
				Type class1 = (Type)packetIdToClassMap.lookup(i0);
				return class1 == null ? null : (Packet)System.Activator.CreateInstance(class1);
			}
			catch (Exception exception2)
			{
				Console.WriteLine(exception2.ToString());
				Console.Write(exception2.StackTrace);
				Console.WriteLine("Skipping packet with id " + i0);
				return null;
			}
		}

		public int PacketId
		{
			get
			{
				return ((int?)packetClassToIdMap[this.GetType()]).Value;
			}
		}

		public static Packet? ReadPacket(BinaryReader stream, bool isServer)
		{
			Packet? packet;

			int packetId;
			try
			{
				packetId = stream.ReadByte();
				if (packetId == -1)
				{
					return null;
				}

				if (isServer && !ServerPacketIdList.Contains(packetId) || !isServer && !ClientPacketIdList.Contains(packetId))
				{
					throw new IOException("Bad packet id " + packetId);
				}

				packet = GetNewPacket(packetId);
				if (packet == null)
				{
					throw new IOException("Bad packet id " + packetId);
				}

				packet.readPacketData(stream);
				++networkTotalReadPackets;
				networkTotalReadBytes += packet.PacketSize;
			}
			catch (EndOfStreamException)
			{
				Console.WriteLine("Reached end of stream");
				return null;
			}

			PacketCount.countPacket(packetId, packet.PacketSize);
			++networkTotalReadPackets;
			networkTotalReadBytes += packet.PacketSize;
			return packet;
		}
        
		public static void writePacket(Packet packet0, BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write((byte)packet0.PacketId);
			packet0.writePacketData(dataOutputStream1);
			++networkTotalWrittenPackets;
			networkTotalWrittenBytes += packet0.PacketSize;
		}

		public static void writeString(string string0, BinaryWriter dataOutputStream1)
		{
			if (string0.Length > 32767)
			{
				throw new IOException("String too big");
			}
			else
			{
				dataOutputStream1.WriteBigEndian((short)string0.Length);
				dataOutputStream1.WriteBigEndian(string0.ToArray());
			}
		}
        
		public static string readString(BinaryReader dataInputStream0, int i1)
		{
			short s2 = dataInputStream0.ReadInt16BigEndian();
			if (s2 > i1)
			{
				throw new IOException("Received string length longer than maximum allowed (" + s2 + " > " + i1 + ")");
			}
			else if (s2 < 0)
			{
				throw new IOException("Received string length is less than zero! Weird string!");
			}
			else
			{
				char[] chars = dataInputStream0.ReadCharsBigEndian(s2);
                byte[] bytes = new byte[chars.Length * sizeof(char)];
				Buffer.BlockCopy(chars, 0, bytes, 0, bytes.Length);

				return Encoding.Unicode.GetString(bytes);
			}
		}

		public abstract void readPacketData(BinaryReader dataInputStream1);
        
		public abstract void writePacketData(BinaryWriter dataOutputStream1);

		public abstract void processPacket(NetHandler netHandler1);

		public abstract int PacketSize {get;}
        
		protected internal virtual ItemStack? ReadItemStack(BinaryReader dataInputStream1)
		{
			ItemStack itemStack2 = null;
			short s3 = dataInputStream1.ReadInt16BigEndian();
			if (s3 >= 0)
			{
				sbyte b4 = dataInputStream1.ReadSByte();
				short s5 = dataInputStream1.ReadInt16BigEndian();
				itemStack2 = new ItemStack(s3, b4, s5);
				if (Item.itemsList[s3].Damageable || Item.itemsList[s3].func_46056_k())
				{
					itemStack2.stackTagCompound = this.readNBTTagCompound(dataInputStream1);
				}
			}

			return itemStack2;
		}
        
		protected internal virtual void writeItemStack(ItemStack itemStack1, BinaryWriter dataOutputStream2)
		{
			if (itemStack1 == null)
			{
				dataOutputStream2.Write((short)-1);
			}
			else
			{
				dataOutputStream2.WriteBigEndian((short)itemStack1.itemID);
				dataOutputStream2.Write((sbyte)itemStack1.stackSize);
				dataOutputStream2.WriteBigEndian((short)itemStack1.ItemDamage);
				if (itemStack1.Item.Damageable || itemStack1.Item.func_46056_k())
				{
					this.writeNBTTagCompound(itemStack1.stackTagCompound, dataOutputStream2);
				}
			}

		}
        
		protected internal virtual NBTTagCompound readNBTTagCompound(BinaryReader dataInputStream1)
		{
			short s2 = dataInputStream1.ReadInt16BigEndian();
			if (s2 < 0)
			{
				return null;
			}
			else
			{
				byte[] b3 = new byte[s2];
				dataInputStream1.Read(b3);
				return CompressedStreamTools.decompress(b3);
			}
		}
        
		protected internal virtual void writeNBTTagCompound(NBTTagCompound nBTTagCompound1, BinaryWriter dataOutputStream2)
		{
			if (nBTTagCompound1 == null)
			{
				dataOutputStream2.WriteBigEndian((short)-1);
			}
			else
			{
				byte[] b3 = CompressedStreamTools.compress(nBTTagCompound1);
				dataOutputStream2.WriteBigEndian((short)b3.Length);
				dataOutputStream2.Write(b3);
			}

		}

		static Packet()
		{
			AddIdClassMapping(0, true, true, typeof(Packet0KeepAlive));
			AddIdClassMapping(1, true, true, typeof(Packet1Login));
			AddIdClassMapping(2, true, true, typeof(Packet2Handshake));
			AddIdClassMapping(3, true, true, typeof(Packet3Chat));
			AddIdClassMapping(4, true, false, typeof(Packet4UpdateTime));
			AddIdClassMapping(5, true, false, typeof(Packet5PlayerInventory));
			AddIdClassMapping(6, true, false, typeof(Packet6SpawnPosition));
			AddIdClassMapping(7, false, true, typeof(Packet7UseEntity));
			AddIdClassMapping(8, true, false, typeof(Packet8UpdateHealth));
			AddIdClassMapping(9, true, true, typeof(Packet9Respawn));
			AddIdClassMapping(10, true, true, typeof(Packet10Flying));
			AddIdClassMapping(11, true, true, typeof(Packet11PlayerPosition));
			AddIdClassMapping(12, true, true, typeof(Packet12PlayerLook));
			AddIdClassMapping(13, true, true, typeof(Packet13PlayerLookMove));
			AddIdClassMapping(14, false, true, typeof(Packet14BlockDig));
			AddIdClassMapping(15, false, true, typeof(Packet15Place));
			AddIdClassMapping(16, false, true, typeof(Packet16BlockItemSwitch));
			AddIdClassMapping(17, true, false, typeof(Packet17Sleep));
			AddIdClassMapping(18, true, true, typeof(Packet18Animation));
			AddIdClassMapping(19, false, true, typeof(Packet19EntityAction));
			AddIdClassMapping(20, true, false, typeof(Packet20NamedEntitySpawn));
			AddIdClassMapping(21, true, false, typeof(Packet21PickupSpawn));
			AddIdClassMapping(22, true, false, typeof(Packet22Collect));
			AddIdClassMapping(23, true, false, typeof(Packet23VehicleSpawn));
			AddIdClassMapping(24, true, false, typeof(Packet24MobSpawn));
			AddIdClassMapping(25, true, false, typeof(Packet25EntityPainting));
			AddIdClassMapping(26, true, false, typeof(Packet26EntityExpOrb));
			AddIdClassMapping(28, true, false, typeof(Packet28EntityVelocity));
			AddIdClassMapping(29, true, false, typeof(Packet29DestroyEntity));
			AddIdClassMapping(30, true, false, typeof(Packet30Entity));
			AddIdClassMapping(31, true, false, typeof(Packet31RelEntityMove));
			AddIdClassMapping(32, true, false, typeof(Packet32EntityLook));
			AddIdClassMapping(33, true, false, typeof(Packet33RelEntityMoveLook));
			AddIdClassMapping(34, true, false, typeof(Packet34EntityTeleport));
			AddIdClassMapping(35, true, false, typeof(Packet35EntityHeadRotation));
			AddIdClassMapping(38, true, false, typeof(Packet38EntityStatus));
			AddIdClassMapping(39, true, false, typeof(Packet39AttachEntity));
			AddIdClassMapping(40, true, false, typeof(Packet40EntityMetadata));
			AddIdClassMapping(41, true, false, typeof(Packet41EntityEffect));
			AddIdClassMapping(42, true, false, typeof(Packet42RemoveEntityEffect));
			AddIdClassMapping(43, true, false, typeof(Packet43Experience));
			AddIdClassMapping(50, true, false, typeof(Packet50PreChunk));
			AddIdClassMapping(51, true, false, typeof(Packet51MapChunk));
			AddIdClassMapping(52, true, false, typeof(Packet52MultiBlockChange));
			AddIdClassMapping(53, true, false, typeof(Packet53BlockChange));
			AddIdClassMapping(54, true, false, typeof(Packet54PlayNoteBlock));
			AddIdClassMapping(60, true, false, typeof(Packet60Explosion));
			AddIdClassMapping(61, true, false, typeof(Packet61DoorChange));
			AddIdClassMapping(70, true, false, typeof(Packet70Bed));
			AddIdClassMapping(71, true, false, typeof(Packet71Weather));
			AddIdClassMapping(100, true, false, typeof(Packet100OpenWindow));
			AddIdClassMapping(101, true, true, typeof(Packet101CloseWindow));
			AddIdClassMapping(102, false, true, typeof(Packet102WindowClick));
			AddIdClassMapping(103, true, false, typeof(Packet103SetSlot));
			AddIdClassMapping(104, true, false, typeof(Packet104WindowItems));
			AddIdClassMapping(105, true, false, typeof(Packet105UpdateProgressbar));
			AddIdClassMapping(106, true, true, typeof(Packet106Transaction));
			AddIdClassMapping(107, true, true, typeof(Packet107CreativeSetSlot));
			AddIdClassMapping(108, false, true, typeof(Packet108EnchantItem));
			AddIdClassMapping(130, true, true, typeof(Packet130UpdateSign));
			AddIdClassMapping(131, true, false, typeof(Packet131MapData));
			AddIdClassMapping(132, true, false, typeof(Packet132TileEntityData));
			AddIdClassMapping(200, true, false, typeof(Packet200Statistic));
			AddIdClassMapping(201, true, false, typeof(Packet201PlayerInfo));
			AddIdClassMapping(202, true, true, typeof(Packet202PlayerAbilities));
			AddIdClassMapping(250, true, true, typeof(Packet250CustomPayload));
			AddIdClassMapping(254, false, true, typeof(Packet254ServerPing));
			AddIdClassMapping(255, true, true, typeof(Packet255KickDisconnect));
		}
	}

}