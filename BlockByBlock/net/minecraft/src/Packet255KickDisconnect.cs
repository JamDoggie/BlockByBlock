namespace net.minecraft.src
{

	public class Packet255KickDisconnect : Packet
	{
		public string reason;

		public Packet255KickDisconnect()
		{
		}

		public Packet255KickDisconnect(string string1)
		{
			reason = string1;
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			reason = readString(dataInputStream1, 256);
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			writeString(reason, dataOutputStream1);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleKickDisconnect(this);
		}

		public override int PacketSize
		{
			get
			{
				return reason.Length;
			}
		}
	}

}