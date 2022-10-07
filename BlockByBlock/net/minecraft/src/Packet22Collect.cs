using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet22Collect : Packet
	{
		public int collectedEntityId;
		public int collectorEntityId;
        
		public override void readPacketData(BinaryReader dataInputStream1)
		{
			collectedEntityId = dataInputStream1.ReadInt32BigEndian();
			collectorEntityId = dataInputStream1.ReadInt32BigEndian();
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(collectedEntityId);
			dataOutputStream1.WriteBigEndian(collectorEntityId);
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