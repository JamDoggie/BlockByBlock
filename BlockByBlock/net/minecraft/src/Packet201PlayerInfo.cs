namespace net.minecraft.src
{

	public class Packet201PlayerInfo : Packet
	{
		public string playerName;
		public bool isConnected;
		public int ping;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.playerName = readString(dataInputStream1, 16);
			this.isConnected = dataInputStream1.readByte() != 0;
			this.ping = dataInputStream1.readShort();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			writeString(this.playerName, dataOutputStream1);
			dataOutputStream1.writeByte(this.isConnected ? 1 : 0);
			dataOutputStream1.writeShort(this.ping);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handlePlayerInfo(this);
		}

		public override int PacketSize
		{
			get
			{
				return this.playerName.Length + 2 + 1 + 2;
			}
		}
	}

}