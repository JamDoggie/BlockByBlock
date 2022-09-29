namespace net.minecraft.src
{

	public class Packet54PlayNoteBlock : Packet
	{
		public int xLocation;
		public int yLocation;
		public int zLocation;
		public int instrumentType;
		public int pitch;

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			xLocation = dataInputStream1.ReadInt32();
			yLocation = dataInputStream1.ReadInt16();
			zLocation = dataInputStream1.ReadInt32();
			instrumentType = dataInputStream1.ReadSByte();
			pitch = dataInputStream1.ReadSByte();
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write(xLocation);
			dataOutputStream1.Write((short)yLocation);
			dataOutputStream1.Write(zLocation);
			dataOutputStream1.Write((sbyte)instrumentType);
			dataOutputStream1.Write((sbyte)pitch);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handlePlayNoteBlock(this);
		}

		public override int PacketSize
		{
			get
			{
				return 12;
			}
		}
	}

}