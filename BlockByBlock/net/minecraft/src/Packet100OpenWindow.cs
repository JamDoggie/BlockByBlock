namespace net.minecraft.src
{

	public class Packet100OpenWindow : Packet
	{
		public int windowId;
		public int inventoryType;
		public string windowTitle;
		public int slotsCount;

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleOpenWindow(this);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.windowId = dataInputStream1.readByte() & 255;
			this.inventoryType = dataInputStream1.readByte() & 255;
			this.windowTitle = readString(dataInputStream1, 32);
			this.slotsCount = dataInputStream1.readByte() & 255;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeByte(this.windowId & 255);
			dataOutputStream1.writeByte(this.inventoryType & 255);
			writeString(this.windowTitle, dataOutputStream1);
			dataOutputStream1.writeByte(this.slotsCount & 255);
		}

		public override int PacketSize
		{
			get
			{
				return 3 + this.windowTitle.Length;
			}
		}
	}

}