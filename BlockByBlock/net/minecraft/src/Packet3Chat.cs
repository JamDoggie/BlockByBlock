namespace net.minecraft.src
{

	public class Packet3Chat : Packet
	{
		public static int field_52010_b = 119;
		public string message;

		public Packet3Chat()
		{
		}

		public Packet3Chat(string string1)
		{
			if (string1.Length > field_52010_b)
			{
				string1 = string1.Substring(0, field_52010_b);
			}

			this.message = string1;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.message = readString(dataInputStream1, field_52010_b);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			writeString(this.message, dataOutputStream1);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleChat(this);
		}

		public override int PacketSize
		{
			get
			{
				return 2 + this.message.Length * 2;
			}
		}
	}

}