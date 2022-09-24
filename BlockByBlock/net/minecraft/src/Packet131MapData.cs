namespace net.minecraft.src
{

	public class Packet131MapData : Packet
	{
		public short itemID;
		public short uniqueID;
		public sbyte[] itemData;

		public Packet131MapData()
		{
			this.isChunkDataPacket = true;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.itemID = dataInputStream1.readShort();
			this.uniqueID = dataInputStream1.readShort();
			this.itemData = new sbyte[dataInputStream1.readByte() & 255];
			dataInputStream1.readFully(this.itemData);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeShort(this.itemID);
			dataOutputStream1.writeShort(this.uniqueID);
			dataOutputStream1.writeByte(this.itemData.Length);
			dataOutputStream1.write(this.itemData);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleMapData(this);
		}

		public override int PacketSize
		{
			get
			{
				return 4 + this.itemData.Length;
			}
		}
	}

}