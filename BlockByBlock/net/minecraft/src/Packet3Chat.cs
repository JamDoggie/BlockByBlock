namespace net.minecraft.src
{

	public class Packet3Chat : Packet
	{
		public static int maxMessageLength = 119;
		public string message;

		public Packet3Chat()
		{
		}

		public Packet3Chat(string string1)
		{
			if (string1.Length > maxMessageLength)
			{
				string1 = string1.Substring(0, maxMessageLength);
			}

			message = string1;
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			message = readString(dataInputStream1, maxMessageLength);
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			writeString(message, dataOutputStream1);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleChat(this);
		}

		public override int PacketSize
		{
			get
			{
				return 2 + message.Length * 2;
			}
		}
	}

}