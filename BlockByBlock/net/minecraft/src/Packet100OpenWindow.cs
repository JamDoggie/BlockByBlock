using BlockByBlock.java_extensions;

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
			windowId = dataInputStream1.ReadSByte() & 255;
			inventoryType = dataInputStream1.ReadSByte() & 255;
			windowTitle = readString(dataInputStream1, 32);
			slotsCount = dataInputStream1.ReadSByte() & 255;
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write((byte)(windowId & 255));
			dataOutputStream1.Write((byte)(inventoryType & 255));
			writeString(windowTitle, dataOutputStream1);
			dataOutputStream1.Write((byte)(slotsCount & 255));
		}

		public override int PacketSize
		{
			get
			{
				return 3 + windowTitle.Length;
			}
		}
	}

}