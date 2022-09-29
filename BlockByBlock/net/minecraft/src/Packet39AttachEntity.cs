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
			entityId = dataInputStream1.ReadInt32();
			vehicleEntityId = dataInputStream1.ReadInt32();
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write(entityId);
			dataOutputStream1.Write(vehicleEntityId);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleAttachEntity(this);
		}
	}

}