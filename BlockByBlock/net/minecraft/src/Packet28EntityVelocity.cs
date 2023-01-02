using BlockByBlock.java_extensions;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class Packet28EntityVelocity : Packet
	{
		public int entityId;
		public int motionX;
		public int motionY;
		public int motionZ;

		public Packet28EntityVelocity()
		{
		}

		public Packet28EntityVelocity(Entity entity1) : this(entity1.entityId, entity1.motionX, entity1.motionY, entity1.motionZ)
		{
		}

		public Packet28EntityVelocity(int i1, double d2, double d4, double d6)
		{
			entityId = i1;
			double d8 = 3.9D;
			if (d2 < -d8)
			{
				d2 = -d8;
			}

			if (d4 < -d8)
			{
				d4 = -d8;
			}

			if (d6 < -d8)
			{
				d6 = -d8;
			}

			if (d2 > d8)
			{
				d2 = d8;
			}

			if (d4 > d8)
			{
				d4 = d8;
			}

			if (d6 > d8)
			{
				d6 = d8;
			}

			motionX = (int)(d2 * 8000.0D);
			motionY = (int)(d4 * 8000.0D);
			motionZ = (int)(d6 * 8000.0D);
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			entityId = dataInputStream1.ReadInt32BigEndian();
			motionX = dataInputStream1.ReadInt16BigEndian();
			motionY = dataInputStream1.ReadInt16BigEndian();
			motionZ = dataInputStream1.ReadInt16BigEndian();
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(entityId);
			dataOutputStream1.WriteBigEndian((short)motionX);
			dataOutputStream1.WriteBigEndian((short)motionY);
			dataOutputStream1.WriteBigEndian((short)motionZ);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleEntityVelocity(this);
		}

		public override int PacketSize
		{
			get
			{
				return 10;
			}
		}
	}

}