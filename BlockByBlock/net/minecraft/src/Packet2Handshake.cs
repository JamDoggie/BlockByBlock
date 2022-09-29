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
			username = string1;
		}

		public Packet2Handshake(string string1, string string2, int i3)
		{
			username = string1 + ";" + string2 + ":" + i3;
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			username = readString(dataInputStream1, 64);
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			writeString(username, dataOutputStream1);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleHandshake(this);
		}

		public override int PacketSize
		{
			get
			{
				return 4 + username.Length + 4;
			}
		}
	}

}