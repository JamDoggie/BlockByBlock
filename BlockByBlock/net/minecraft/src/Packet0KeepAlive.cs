using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet0KeepAlive : Packet
	{
		public int randomId;

		public Packet0KeepAlive()
		{
		}

		public Packet0KeepAlive(int i1)
		{
			this.randomId = i1;
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleKeepAlive(this);
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			this.randomId = dataInputStream1.ReadInt32BigEndian();
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(randomId);
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