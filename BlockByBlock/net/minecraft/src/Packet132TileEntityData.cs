namespace net.minecraft.src
{

	public class Packet132TileEntityData : Packet
	{
		public int xPosition;
		public int yPosition;
		public int zPosition;
		public int actionType;
		public int customParam1;
		public int customParam2;
		public int customParam3;

		public Packet132TileEntityData()
		{
			this.isChunkDataPacket = true;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.xPosition = dataInputStream1.readInt();
			this.yPosition = dataInputStream1.readShort();
			this.zPosition = dataInputStream1.readInt();
			this.actionType = dataInputStream1.readByte();
			this.customParam1 = dataInputStream1.readInt();
			this.customParam2 = dataInputStream1.readInt();
			this.customParam3 = dataInputStream1.readInt();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.xPosition);
			dataOutputStream1.writeShort(this.yPosition);
			dataOutputStream1.writeInt(this.zPosition);
			dataOutputStream1.writeByte((sbyte)this.actionType);
			dataOutputStream1.writeInt(this.customParam1);
			dataOutputStream1.writeInt(this.customParam2);
			dataOutputStream1.writeInt(this.customParam3);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleTileEntityData(this);
		}

		public override int PacketSize
		{
			get
			{
				return 25;
			}
		}
	}

}