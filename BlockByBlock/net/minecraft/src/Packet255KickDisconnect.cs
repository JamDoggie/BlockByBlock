namespace net.minecraft.src
{

	public class Packet255KickDisconnect : Packet
	{
		public string reason;

		public Packet255KickDisconnect()
		{
		}

		public Packet255KickDisconnect(string string1)
		{
			this.reason = string1;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.reason = readString(dataInputStream1, 256);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			writeString(this.reason, dataOutputStream1);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleKickDisconnect(this);
		}

		public override int PacketSize
		{
			get
			{
				return this.reason.Length;
			}
		}
	}

}