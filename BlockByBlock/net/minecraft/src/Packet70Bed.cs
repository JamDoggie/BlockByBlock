namespace net.minecraft.src
{

	public class Packet70Bed : Packet
	{
		public static readonly string[] bedChat = new string[]{"tile.bed.notValid", null, null, "gameMode.changed"};
		public int bedState;
		public int gameMode;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.bedState = dataInputStream1.readByte();
			this.gameMode = dataInputStream1.readByte();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeByte(this.bedState);
			dataOutputStream1.writeByte(this.gameMode);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleBed(this);
		}

		public override int PacketSize
		{
			get
			{
				return 2;
			}
		}
	}

}