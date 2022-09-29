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
			entityId = dataInputStream1.ReadInt32();
			type = dataInputStream1.ReadSByte();
			xPosition = dataInputStream1.ReadInt32();
			yPosition = dataInputStream1.ReadInt32();
			zPosition = dataInputStream1.ReadInt32();
			throwerEntityId = dataInputStream1.ReadInt32();
			if (throwerEntityId > 0)
			{
				speedX = dataInputStream1.ReadInt16();
				speedY = dataInputStream1.ReadInt16();
				speedZ = dataInputStream1.ReadInt16();
			}

		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write(entityId);
			dataOutputStream1.Write((sbyte)type);
			dataOutputStream1.Write(xPosition);
			dataOutputStream1.Write(yPosition);
			dataOutputStream1.Write(zPosition);
			dataOutputStream1.Write(throwerEntityId);
			if (throwerEntityId > 0)
			{
				dataOutputStream1.Write((short)speedX);
				dataOutputStream1.Write((short)speedY);
				dataOutputStream1.Write((short)speedZ);
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