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
			this.windowId = i1;
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleCloseWindow(this);
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			this.windowId = dataInputStream1.ReadByte();
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write((byte)windowId);
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