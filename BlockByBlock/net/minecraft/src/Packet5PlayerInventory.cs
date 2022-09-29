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
			entityID = dataInputStream1.ReadInt32();
			slot = dataInputStream1.ReadInt16();
			itemID = dataInputStream1.ReadInt16();
			itemDamage = dataInputStream1.ReadInt16();
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write(entityID);
			dataOutputStream1.Write((short)slot);
			dataOutputStream1.Write((short)itemID);
			dataOutputStream1.Write((short)itemDamage);
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