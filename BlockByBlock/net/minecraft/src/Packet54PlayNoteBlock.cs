namespace net.minecraft.src
{

	public class Packet54PlayNoteBlock : Packet
	{
		public int xLocation;
		public int yLocation;
		public int zLocation;
		public int instrumentType;
		public int pitch;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.xLocation = dataInputStream1.readInt();
			this.yLocation = dataInputStream1.readShort();
			this.zLocation = dataInputStream1.readInt();
			this.instrumentType = dataInputStream1.read();
			this.pitch = dataInputStream1.read();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.xLocation);
			dataOutputStream1.writeShort(this.yLocation);
			dataOutputStream1.writeInt(this.zLocation);
			dataOutputStream1.write(this.instrumentType);
			dataOutputStream1.write(this.pitch);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handlePlayNoteBlock(this);
		}

		public override int PacketSize
		{
			get
			{
				return 12;
			}
		}
	}

}