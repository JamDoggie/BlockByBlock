namespace net.minecraft.src
{

	public class Packet0KeepAlive : Packet
	{
		public int randomId;

		public Packet0KeepAlive()
		{
		}

		public Packet0KeepAlive(int i1)
		{
			this.randomId = i1;
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleKeepAlive(this);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.randomId = dataInputStream1.readInt();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.randomId);
		}

		public override int PacketSize
		{
			get
			{
				return 4;
			}
		}
	}

}