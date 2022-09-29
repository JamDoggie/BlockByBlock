namespace net.minecraft.src
{

	public class Packet101CloseWindow : Packet
	{
		public int windowId;

		public Packet101CloseWindow()
		{
		}

		public Packet101CloseWindow(int i1)
		{
			windowId = i1;
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleCloseWindow(this);
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			windowId = dataInputStream1.ReadSByte();
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write((sbyte)windowId);
		}

		public override int PacketSize
		{
			get
			{
				return 1;
			}
		}
	}

}