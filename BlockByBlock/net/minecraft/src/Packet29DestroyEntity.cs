namespace net.minecraft.src
{

	public class Packet29DestroyEntity : Packet
	{
		public int entityId;
        
		public override void readPacketData(BinaryReader dataInputStream1)
		{
			entityId = dataInputStream1.ReadInt32();
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write(entityId);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleDestroyEntity(this);
		}

		public override int PacketSize
		{
			get
			{
				return 4;
			}
		}
	}

}