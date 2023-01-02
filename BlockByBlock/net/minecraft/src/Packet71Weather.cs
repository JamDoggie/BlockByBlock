using BlockByBlock.java_extensions;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class Packet71Weather : Packet
	{
		public int entityID;
		public int posX;
		public int posY;
		public int posZ;
		public int isLightningBolt;

		public Packet71Weather()
		{
		}

		public Packet71Weather(Entity entity1)
		{
			entityID = entity1.entityId;
			posX = MathHelper.floor_double(entity1.posX * 32.0D);
			posY = MathHelper.floor_double(entity1.posY * 32.0D);
			posZ = MathHelper.floor_double(entity1.posZ * 32.0D);
			if (entity1 is EntityLightningBolt)
			{
				isLightningBolt = 1;
			}

		}
        
		public override void readPacketData(BinaryReader dataInputStream1)
		{
			entityID = dataInputStream1.ReadInt32BigEndian();
			isLightningBolt = dataInputStream1.ReadSByte();
			posX = dataInputStream1.ReadInt32BigEndian();
			posY = dataInputStream1.ReadInt32BigEndian();
			posZ = dataInputStream1.ReadInt32BigEndian();
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(entityID);
			dataOutputStream1.Write((sbyte)isLightningBolt);
			dataOutputStream1.WriteBigEndian(posX);
			dataOutputStream1.WriteBigEndian(posY);
			dataOutputStream1.WriteBigEndian(posZ);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleWeather(this);
		}

		public override int PacketSize
		{
			get
			{
				return 17;
			}
		}
	}

}