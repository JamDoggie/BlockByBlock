using BlockByBlock.java_extensions;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class Packet18Animation : Packet
	{
		public int entityId;
		public int animate;

		public Packet18Animation()
		{
		}

		public Packet18Animation(Entity entity1, int i2)
		{
			entityId = entity1.entityId;
			animate = i2;
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			entityId = dataInputStream1.ReadInt32BigEndian();
			animate = dataInputStream1.ReadSByte();
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(entityId);
			dataOutputStream1.Write((sbyte)animate);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleAnimation(this);
		}

		public override int PacketSize
		{
			get
			{
				return 5;
			}
		}
	}

}