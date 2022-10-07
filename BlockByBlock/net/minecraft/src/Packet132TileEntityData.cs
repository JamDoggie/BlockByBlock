using BlockByBlock.java_extensions;

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
			xPosition = dataInputStream1.ReadInt32BigEndian();
			yPosition = dataInputStream1.ReadInt16BigEndian();
			zPosition = dataInputStream1.ReadInt32BigEndian();
			actionType = dataInputStream1.ReadSByte();
			customParam1 = dataInputStream1.ReadInt32BigEndian();
			customParam2 = dataInputStream1.ReadInt32BigEndian();
			customParam3 = dataInputStream1.ReadInt32BigEndian();
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(xPosition);
			dataOutputStream1.WriteBigEndian((short)yPosition);
			dataOutputStream1.WriteBigEndian(zPosition);
			dataOutputStream1.Write((sbyte)actionType);
			dataOutputStream1.WriteBigEndian(customParam1);
			dataOutputStream1.WriteBigEndian(customParam2);
			dataOutputStream1.WriteBigEndian(customParam3);
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