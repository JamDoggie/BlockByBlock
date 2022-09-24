namespace net.minecraft.src
{

	public class Packet4UpdateTime : Packet
	{
		public long time;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.time = dataInputStream1.readLong();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeLong(this.time);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleUpdateTime(this);
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