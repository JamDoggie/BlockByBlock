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
		private static ISet<object> clientPacketIdList = new HashSet<object>();
		private static ISet<object> serverPacketIdList = new HashSet<object>();
		public readonly long creationTimeMillis = DateTimeHelper.CurrentUnixTimeMillis();
		public static long field_48158_m;
		public static long field_48156_n;
		public static long field_48157_o;
		public static long field_48155_p;
		public bool isChunkDataPacket = false;

		internal static void addIdClassMapping(int i0, bool z1, bool z2, Type class3)
		{
			if (packetIdToClassMap.containsItem(i0))
			{
				throw new System.ArgumentException("Duplicate packet id:" + i0);
			}
			else if (packetClassToIdMap.Contains(class3))
			{
				throw new System.ArgumentException("Duplicate packet class:" + class3);
			}
			else
			{
				packetIdToClassMap.addKey(i0, class3);
				packetClassToIdMap[class3] = i0;
				if (z1)
				{
					clientPacketIdList.Add(i0);
				}

				if (z2)
				{
					serverPacketIdList.Add(i0);
				}

			}
		}

		public static Packet getNewPacket(int i0)
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

		public static Packet readPacket(BinaryReader dataInputStream0, bool z1)
		{
			bool z2 = false;
			Packet packet3 = null;

			int i6;
			try
			{
				i6 = dataInputStream0.ReadByte();
				if (i6 == -1)
				{
					return null;
				}

				if (z1 && !serverPacketIdList.Contains(i6) || !z1 && !clientPacketIdList.Contains(i6))
				{
					throw new IOException("Bad packet id " + i6);
				}

				packet3 = getNewPacket(i6);
				if (packet3 == null)
				{
					throw new IOException("Bad packet id " + i6);
				}

				packet3.readPacketData(dataInputStream0);
				++field_48158_m;
				field_48156_n += (long)packet3.PacketSize;
			}
			catch (EndOfStreamException)
			{
				Console.WriteLine("Reached end of stream");
				return null;
			}

			PacketCount.countPacket(i6, (long)packet3.PacketSize);
			++field_48158_m;
			field_48156_n += (long)packet3.PacketSize;
			return packet3;
		}
        
		public static void writePacket(Packet packet0, BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write((byte)packet0.PacketId);
			packet0.writePacketData(dataOutputStream1);
			++field_48157_o;
			field_48155_p += (long)packet0.PacketSize;
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
        
		protected internal virtual ItemStack readItemStack(BinaryReader dataInputStream1)
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

		// JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
		// ORIGINAL LINE: protected void writeItemStack(ItemStack itemStack1, java.io.DataOutputStream dataOutputStream2) throws java.io.IOException
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

		// JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
		// ORIGINAL LINE: protected NBTTagCompound readNBTTagCompound(java.io.DataInputStream dataInputStream1) throws java.io.IOException
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
        
		// JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
		// ORIGINAL LINE: protected void writeNBTTagCompound(NBTTagCompound nBTTagCompound1, java.io.DataOutputStream dataOutputStream2) throws java.io.IOException
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
			addIdClassMapping(0, true, true, typeof(Packet0KeepAlive));
			addIdClassMapping(1, true, true, typeof(Packet1Login));
			addIdClassMapping(2, true, true, typeof(Packet2Handshake));
			addIdClassMapping(3, true, true, typeof(Packet3Chat));
			addIdClassMapping(4, true, false, typeof(Packet4UpdateTime));
			addIdClassMapping(5, true, false, typeof(Packet5PlayerInventory));
			addIdClassMapping(6, true, false, typeof(Packet6SpawnPosition));
			addIdClassMapping(7, false, true, typeof(Packet7UseEntity));
			addIdClassMapping(8, true, false, typeof(Packet8UpdateHealth));
			addIdClassMapping(9, true, true, typeof(Packet9Respawn));
			addIdClassMapping(10, true, true, typeof(Packet10Flying));
			addIdClassMapping(11, true, true, typeof(Packet11PlayerPosition));
			addIdClassMapping(12, true, true, typeof(Packet12PlayerLook));
			addIdClassMapping(13, true, true, typeof(Packet13PlayerLookMove));
			addIdClassMapping(14, false, true, typeof(Packet14BlockDig));
			addIdClassMapping(15, false, true, typeof(Packet15Place));
			addIdClassMapping(16, false, true, typeof(Packet16BlockItemSwitch));
			addIdClassMapping(17, true, false, typeof(Packet17Sleep));
			addIdClassMapping(18, true, true, typeof(Packet18Animation));
			addIdClassMapping(19, false, true, typeof(Packet19EntityAction));
			addIdClassMapping(20, true, false, typeof(Packet20NamedEntitySpawn));
			addIdClassMapping(21, true, false, typeof(Packet21PickupSpawn));
			addIdClassMapping(22, true, false, typeof(Packet22Collect));
			addIdClassMapping(23, true, false, typeof(Packet23VehicleSpawn));
			addIdClassMapping(24, true, false, typeof(Packet24MobSpawn));
			addIdClassMapping(25, true, false, typeof(Packet25EntityPainting));
			addIdClassMapping(26, true, false, typeof(Packet26EntityExpOrb));
			addIdClassMapping(28, true, false, typeof(Packet28EntityVelocity));
			addIdClassMapping(29, true, false, typeof(Packet29DestroyEntity));
			addIdClassMapping(30, true, false, typeof(Packet30Entity));
			addIdClassMapping(31, true, false, typeof(Packet31RelEntityMove));
			addIdClassMapping(32, true, false, typeof(Packet32EntityLook));
			addIdClassMapping(33, true, false, typeof(Packet33RelEntityMoveLook));
			addIdClassMapping(34, true, false, typeof(Packet34EntityTeleport));
			addIdClassMapping(35, true, false, typeof(Packet35EntityHeadRotation));
			addIdClassMapping(38, true, false, typeof(Packet38EntityStatus));
			addIdClassMapping(39, true, false, typeof(Packet39AttachEntity));
			addIdClassMapping(40, true, false, typeof(Packet40EntityMetadata));
			addIdClassMapping(41, true, false, typeof(Packet41EntityEffect));
			addIdClassMapping(42, true, false, typeof(Packet42RemoveEntityEffect));
			addIdClassMapping(43, true, false, typeof(Packet43Experience));
			addIdClassMapping(50, true, false, typeof(Packet50PreChunk));
			addIdClassMapping(51, true, false, typeof(Packet51MapChunk));
			addIdClassMapping(52, true, false, typeof(Packet52MultiBlockChange));
			addIdClassMapping(53, true, false, typeof(Packet53BlockChange));
			addIdClassMapping(54, true, false, typeof(Packet54PlayNoteBlock));
			addIdClassMapping(60, true, false, typeof(Packet60Explosion));
			addIdClassMapping(61, true, false, typeof(Packet61DoorChange));
			addIdClassMapping(70, true, false, typeof(Packet70Bed));
			addIdClassMapping(71, true, false, typeof(Packet71Weather));
			addIdClassMapping(100, true, false, typeof(Packet100OpenWindow));
			addIdClassMapping(101, true, true, typeof(Packet101CloseWindow));
			addIdClassMapping(102, false, true, typeof(Packet102WindowClick));
			addIdClassMapping(103, true, false, typeof(Packet103SetSlot));
			addIdClassMapping(104, true, false, typeof(Packet104WindowItems));
			addIdClassMapping(105, true, false, typeof(Packet105UpdateProgressbar));
			addIdClassMapping(106, true, true, typeof(Packet106Transaction));
			addIdClassMapping(107, true, true, typeof(Packet107CreativeSetSlot));
			addIdClassMapping(108, false, true, typeof(Packet108EnchantItem));
			addIdClassMapping(130, true, true, typeof(Packet130UpdateSign));
			addIdClassMapping(131, true, false, typeof(Packet131MapData));
			addIdClassMapping(132, true, false, typeof(Packet132TileEntityData));
			addIdClassMapping(200, true, false, typeof(Packet200Statistic));
			addIdClassMapping(201, true, false, typeof(Packet201PlayerInfo));
			addIdClassMapping(202, true, true, typeof(Packet202PlayerAbilities));
			addIdClassMapping(250, true, true, typeof(Packet250CustomPayload));
			addIdClassMapping(254, false, true, typeof(Packet254ServerPing));
			addIdClassMapping(255, true, true, typeof(Packet255KickDisconnect));
		}
	}

}