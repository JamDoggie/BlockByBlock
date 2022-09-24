namespace net.minecraft.src
{

	public class Packet130UpdateSign : Packet
	{
		public int xPosition;
		public int yPosition;
		public int zPosition;
		public string[] signLines;

		public Packet130UpdateSign()
		{
			this.isChunkDataPacket = true;
		}

		public Packet130UpdateSign(int i1, int i2, int i3, string[] string4)
		{
			this.isChunkDataPacket = true;
			this.xPosition = i1;
			this.yPosition = i2;
			this.zPosition = i3;
			this.signLines = string4;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.xPosition = dataInputStream1.readInt();
			this.yPosition = dataInputStream1.readShort();
			this.zPosition = dataInputStream1.readInt();
			this.signLines = new string[4];

			for (int i2 = 0; i2 < 4; ++i2)
			{
				this.signLines[i2] = readString(dataInputStream1, 15);
			}

		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.xPosition);
			dataOutputStream1.writeShort(this.yPosition);
			dataOutputStream1.writeInt(this.zPosition);

			for (int i2 = 0; i2 < 4; ++i2)
			{
				writeString(this.signLines[i2], dataOutputStream1);
			}

		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleUpdateSign(this);
		}

		public override int PacketSize
		{
			get
			{
				int i1 = 0;
    
				for (int i2 = 0; i2 < 4; ++i2)
				{
					i1 += this.signLines[i2].Length;
				}
    
				return i1;
			}
		}
	}

}