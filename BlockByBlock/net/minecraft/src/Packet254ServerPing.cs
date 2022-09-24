namespace net.minecraft.src
{

	public class Packet254ServerPing : Packet
	{
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleServerPing(this);
		}

		public override int PacketSize
		{
			get
			{
				return 0;
			}
		}
	}

}