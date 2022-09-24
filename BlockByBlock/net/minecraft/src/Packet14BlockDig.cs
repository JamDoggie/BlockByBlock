namespace net.minecraft.src
{

	public class Packet14BlockDig : Packet
	{
		public int xPosition;
		public int yPosition;
		public int zPosition;
		public int face;
		public int status;

		public Packet14BlockDig()
		{
		}

		public Packet14BlockDig(int i1, int i2, int i3, int i4, int i5)
		{
			this.status = i1;
			this.xPosition = i2;
			this.yPosition = i3;
			this.zPosition = i4;
			this.face = i5;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.status = dataInputStream1.read();
			this.xPosition = dataInputStream1.readInt();
			this.yPosition = dataInputStream1.read();
			this.zPosition = dataInputStream1.readInt();
			this.face = dataInputStream1.read();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.write(this.status);
			dataOutputStream1.writeInt(this.xPosition);
			dataOutputStream1.write(this.yPosition);
			dataOutputStream1.writeInt(this.zPosition);
			dataOutputStream1.write(this.face);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleBlockDig(this);
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