namespace net.minecraft.src
{

	public class Packet22Collect : Packet
	{
		public int collectedEntityId;
		public int collectorEntityId;
        
		public override void readPacketData(BinaryReader dataInputStream1)
		{
			collectedEntityId = dataInputStream1.ReadInt32();
			collectorEntityId = dataInputStream1.ReadInt32();
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write(collectedEntityId);
			dataOutputStream1.Write(collectorEntityId);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleCollect(this);
		}

		public override int PacketSize
		{
			get
			{
				return 8;
			}
		}
	}

}