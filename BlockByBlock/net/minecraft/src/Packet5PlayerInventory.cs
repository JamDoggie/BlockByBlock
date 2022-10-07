using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet5PlayerInventory : Packet
	{
		public int entityID;
		public int slot;
		public int itemID;
		public int itemDamage;
        
		public override void readPacketData(BinaryReader dataInputStream1)
		{
			entityID = dataInputStream1.ReadInt32BigEndian();
			slot = dataInputStream1.ReadInt16BigEndian();
			itemID = dataInputStream1.ReadInt16BigEndian();
			itemDamage = dataInputStream1.ReadInt16BigEndian();
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(entityID);
			dataOutputStream1.WriteBigEndian((short)slot);
			dataOutputStream1.WriteBigEndian((short)itemID);
			dataOutputStream1.WriteBigEndian((short)itemDamage);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handlePlayerInventory(this);
		}

		public override int PacketSize
		{
			get
			{
				return 8;
			}
		}
	}

}