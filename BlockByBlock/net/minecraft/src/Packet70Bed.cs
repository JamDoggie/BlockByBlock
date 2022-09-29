namespace net.minecraft.src
{

	public class Packet70Bed : Packet
	{
		public static readonly string[] bedChat = new string[]{"tile.bed.notValid", null, null, "gameMode.changed"};
		public int bedState;
		public int gameMode;

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			bedState = dataInputStream1.ReadSByte();
			gameMode = dataInputStream1.ReadSByte();
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write((sbyte)bedState);
			dataOutputStream1.Write((sbyte)gameMode);
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