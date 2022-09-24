namespace net.minecraft.src
{

	public class Packet53BlockChange : Packet
	{
		public int xPosition;
		public int yPosition;
		public int zPosition;
		public int type;
		public int metadata;

		public Packet53BlockChange()
		{
			this.isChunkDataPacket = true;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.xPosition = dataInputStream1.readInt();
			this.yPosition = dataInputStream1.read();
			this.zPosition = dataInputStream1.readInt();
			this.type = dataInputStream1.read();
			this.metadata = dataInputStream1.read();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.xPosition);
			dataOutputStream1.write(this.yPosition);
			dataOutputStream1.writeInt(this.zPosition);
			dataOutputStream1.write(this.type);
			dataOutputStream1.write(this.metadata);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleBlockChange(this);
		}

		public override int PacketSize
		{
			get
			{
				return 11;
			}
		}
	}

}