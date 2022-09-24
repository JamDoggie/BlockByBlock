namespace net.minecraft.src
{

	public class Packet5PlayerInventory : Packet
	{
		public int entityID;
		public int slot;
		public int itemID;
		public int itemDamage;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.entityID = dataInputStream1.readInt();
			this.slot = dataInputStream1.readShort();
			this.itemID = dataInputStream1.readShort();
			this.itemDamage = dataInputStream1.readShort();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.entityID);
			dataOutputStream1.writeShort(this.slot);
			dataOutputStream1.writeShort(this.itemID);
			dataOutputStream1.writeShort(this.itemDamage);
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