using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet4UpdateTime : Packet
	{
		public long time;

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			this.time = dataInputStream1.ReadInt64BigEndian();
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(time);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleUpdateTime(this);
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