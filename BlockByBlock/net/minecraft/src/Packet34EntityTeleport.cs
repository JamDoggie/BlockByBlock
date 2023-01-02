using BlockByBlock.java_extensions;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class Packet34EntityTeleport : Packet
	{
		public int entityId;
		public int xPosition;
		public int yPosition;
		public int zPosition;
		public sbyte yaw;
		public sbyte pitch;

		public Packet34EntityTeleport()
		{
		}

		public Packet34EntityTeleport(Entity entity1)
		{
			entityId = entity1.entityId;
			xPosition = MathHelper.floor_double(entity1.posX * 32.0D);
			yPosition = MathHelper.floor_double(entity1.posY * 32.0D);
			zPosition = MathHelper.floor_double(entity1.posZ * 32.0D);
			yaw = (sbyte)((int)(entity1.rotationYaw * 256.0F / 360.0F));
			pitch = (sbyte)((int)(entity1.rotationPitch * 256.0F / 360.0F));
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			entityId = dataInputStream1.ReadInt32BigEndian();
			xPosition = dataInputStream1.ReadInt32BigEndian();
			yPosition = dataInputStream1.ReadInt32BigEndian();
			zPosition = dataInputStream1.ReadInt32BigEndian();
			yaw = dataInputStream1.ReadSByte();
			pitch = dataInputStream1.ReadSByte();
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(entityId);
			dataOutputStream1.WriteBigEndian(xPosition);
			dataOutputStream1.WriteBigEndian(yPosition);
			dataOutputStream1.WriteBigEndian(zPosition);
			dataOutputStream1.Write(yaw);
			dataOutputStream1.Write(pitch);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleEntityTeleport(this);
		}

		public override int PacketSize
		{
			get
			{
				return 34;
			}
		}
	}

}