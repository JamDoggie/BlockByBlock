namespace net.minecraft.src
{

	public class Packet2Handshake : Packet
	{
		public string username;

		public Packet2Handshake()
		{
		}

		public Packet2Handshake(string string1)
		{
			this.username = string1;
		}

		public Packet2Handshake(string string1, string string2, int i3)
		{
			this.username = string1 + ";" + string2 + ":" + i3;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.username = readString(dataInputStream1, 64);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			writeString(this.username, dataOutputStream1);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleHandshake(this);
		}

		public override int PacketSize
		{
			get
			{
				return 4 + this.username.Length + 4;
			}
		}
	}

}