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

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			this.windowId = dataInputStream1.ReadByte() & 255;
			this.inventoryType = dataInputStream1.ReadByte() & 255;
			this.windowTitle = readString(dataInputStream1, 32);
			this.slotsCount = dataInputStream1.ReadByte() & 255;
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write((byte)(this.windowId & 255));
			dataOutputStream1.Write((byte)(this.inventoryType & 255));
			writeString(this.windowTitle, dataOutputStream1);
			dataOutputStream1.Write((byte)(this.slotsCount & 255));
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