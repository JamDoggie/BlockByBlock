namespace net.minecraft.src
{

	public class Packet50PreChunk : Packet
	{
		public int xPosition;
		public int yPosition;
		public bool mode;

		public Packet50PreChunk()
		{
			this.isChunkDataPacket = false;
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			xPosition = dataInputStream1.ReadInt32();
			yPosition = dataInputStream1.ReadInt32();
			mode = dataInputStream1.ReadSByte() != 0;
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write(xPosition);
			dataOutputStream1.Write(yPosition);
			dataOutputStream1.Write(mode ? (sbyte)1 : (sbyte)0);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handlePreChunk(this);
		}

		public override int PacketSize
		{
			get
			{
				return 9;
			}
		}
	}

}