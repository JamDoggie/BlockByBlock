namespace net.minecraft.src
{

	public class Packet6SpawnPosition : Packet
	{
		public int xPosition;
		public int yPosition;
		public int zPosition;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.xPosition = dataInputStream1.readInt();
			this.yPosition = dataInputStream1.readInt();
			this.zPosition = dataInputStream1.readInt();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.xPosition);
			dataOutputStream1.writeInt(this.yPosition);
			dataOutputStream1.writeInt(this.zPosition);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleSpawnPosition(this);
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