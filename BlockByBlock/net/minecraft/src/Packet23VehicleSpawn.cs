using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet23VehicleSpawn : Packet
	{
		public int entityId;
		public int xPosition;
		public int yPosition;
		public int zPosition;
		public int speedX;
		public int speedY;
		public int speedZ;
		public int type;
		public int throwerEntityId;

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			entityId = dataInputStream1.ReadInt32BigEndian();
			type = dataInputStream1.ReadSByte();
			xPosition = dataInputStream1.ReadInt32BigEndian();
			yPosition = dataInputStream1.ReadInt32BigEndian();
			zPosition = dataInputStream1.ReadInt32BigEndian();
			throwerEntityId = dataInputStream1.ReadInt32BigEndian();
			if (throwerEntityId > 0)
			{
				speedX = dataInputStream1.ReadInt16BigEndian();
				speedY = dataInputStream1.ReadInt16BigEndian();
				speedZ = dataInputStream1.ReadInt16BigEndian();
			}

		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(entityId);
			dataOutputStream1.Write((sbyte)type);
			dataOutputStream1.WriteBigEndian(xPosition);
			dataOutputStream1.WriteBigEndian(yPosition);
			dataOutputStream1.WriteBigEndian(zPosition);
			dataOutputStream1.WriteBigEndian(throwerEntityId);
			if (throwerEntityId > 0)
			{
				dataOutputStream1.WriteBigEndian((short)speedX);
				dataOutputStream1.WriteBigEndian((short)speedY);
				dataOutputStream1.WriteBigEndian((short)speedZ);
			}

		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleVehicleSpawn(this);
		}

		public override int PacketSize
		{
			get
			{
				return 21 + throwerEntityId > 0 ? 6 : 0;
			}
		}
	}

}