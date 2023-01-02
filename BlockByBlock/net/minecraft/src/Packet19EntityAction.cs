using BlockByBlock.java_extensions;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class Packet19EntityAction : Packet
	{
		public int entityId;
		public int state;

		public Packet19EntityAction()
		{
		}

		public Packet19EntityAction(Entity entity1, int i2)
		{
			entityId = entity1.entityId;
			state = i2;
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			entityId = dataInputStream1.ReadInt32BigEndian();
			state = dataInputStream1.ReadSByte();
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(entityId);
			dataOutputStream1.Write((sbyte)state);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleEntityAction(this);
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