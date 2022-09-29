namespace net.minecraft.src
{

	public class Packet201PlayerInfo : Packet
	{
		public string playerName;
		public bool isConnected;
		public int ping;

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			playerName = readString(dataInputStream1, 16);
			isConnected = dataInputStream1.ReadByte() != 0;
			ping = dataInputStream1.ReadInt16();
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			writeString(playerName, dataOutputStream1);
			dataOutputStream1.Write(isConnected ? (byte)1 : (byte)0);
			dataOutputStream1.Write((short)ping);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handlePlayerInfo(this);
		}

		public override int PacketSize
		{
			get
			{
				return playerName.Length + 2 + 1 + 2;
			}
		}
	}

}