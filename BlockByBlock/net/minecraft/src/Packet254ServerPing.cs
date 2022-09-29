namespace net.minecraft.src
{

	public class Packet254ServerPing : Packet
	{
		public override void readPacketData(BinaryReader dataInputStream1)
		{
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleServerPing(this);
		}

		public override int PacketSize
		{
			get
			{
				return 0;
			}
		}
	}

}