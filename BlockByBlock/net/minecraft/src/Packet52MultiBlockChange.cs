namespace net.minecraft.src
{

	public class Packet52MultiBlockChange : Packet
	{
		public int xPosition;
		public int zPosition;
		public sbyte[] metadataArray;
		public int size;
		private static sbyte[] field_48168_e = new sbyte[0];

		public Packet52MultiBlockChange()
		{
			this.isChunkDataPacket = true;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.xPosition = dataInputStream1.readInt();
			this.zPosition = dataInputStream1.readInt();
			this.size = dataInputStream1.readShort() & 65535;
			int i2 = dataInputStream1.readInt();
			if (i2 > 0)
			{
				this.metadataArray = new sbyte[i2];
				dataInputStream1.readFully(this.metadataArray);
			}

		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.xPosition);
			dataOutputStream1.writeInt(this.zPosition);
			dataOutputStream1.writeShort((short)this.size);
			if (this.metadataArray != null)
			{
				dataOutputStream1.writeInt(this.metadataArray.Length);
				dataOutputStream1.write(this.metadataArray);
			}
			else
			{
				dataOutputStream1.writeInt(0);
			}

		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleMultiBlockChange(this);
		}

		public override int PacketSize
		{
			get
			{
				return 10 + this.size * 4;
			}
		}
	}

}