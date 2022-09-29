namespace net.minecraft.src
{

	public class Packet132TileEntityData : Packet
	{
		public int xPosition;
		public int yPosition;
		public int zPosition;
		public int actionType;
		public int customParam1;
		public int customParam2;
		public int customParam3;

		public Packet132TileEntityData()
		{
			isChunkDataPacket = true;
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			xPosition = dataInputStream1.ReadInt32();
			yPosition = dataInputStream1.ReadInt16();
			zPosition = dataInputStream1.ReadInt32();
			actionType = dataInputStream1.ReadSByte();
			customParam1 = dataInputStream1.ReadInt32();
			customParam2 = dataInputStream1.ReadInt32();
			customParam3 = dataInputStream1.ReadInt32();
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write(xPosition);
			dataOutputStream1.Write((short)yPosition);
			dataOutputStream1.Write(zPosition);
			dataOutputStream1.Write((sbyte)actionType);
			dataOutputStream1.Write(customParam1);
			dataOutputStream1.Write(customParam2);
			dataOutputStream1.Write(customParam3);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleTileEntityData(this);
		}

		public override int PacketSize
		{
			get
			{
				return 25;
			}
		}
	}

}