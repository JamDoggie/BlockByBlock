namespace net.minecraft.src
{

	public class Packet50PreChunk : Packet
	{
		public int xPosition;
		public int yPosition;
		public bool mode;

		public Packet50PreChunk()
		{
			this.isChunkDataPacket = false;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.xPosition = dataInputStream1.readInt();
			this.yPosition = dataInputStream1.readInt();
			this.mode = dataInputStream1.read() != 0;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.xPosition);
			dataOutputStream1.writeInt(this.yPosition);
			dataOutputStream1.write(this.mode ? 1 : 0);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handlePreChunk(this);
		}

		public override int PacketSize
		{
			get
			{
				return 9;
			}
		}
	}

}