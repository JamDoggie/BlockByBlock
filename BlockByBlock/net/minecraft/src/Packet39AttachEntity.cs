using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet39AttachEntity : Packet
	{
		public int entityId;
		public int vehicleEntityId;

		public override int PacketSize
		{
			get
			{
				return 8;
			}
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			entityId = dataInputStream1.ReadInt32BigEndian();
			vehicleEntityId = dataInputStream1.ReadInt32BigEndian();
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(entityId);
			dataOutputStream1.WriteBigEndian(vehicleEntityId);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleAttachEntity(this);
		}
	}

}