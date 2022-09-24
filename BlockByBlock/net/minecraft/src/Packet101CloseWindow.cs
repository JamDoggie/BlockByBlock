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

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.windowId = dataInputStream1.readByte();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeByte(this.windowId);
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