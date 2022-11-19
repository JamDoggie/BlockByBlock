using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet102WindowClick : Packet
	{
		public int window_Id;
		public int inventorySlot;
		public int mouseClick;
		public short action;
		public ItemStack itemStack;
		public bool holdingShift;

		public Packet102WindowClick()
		{
		}

		public Packet102WindowClick(int i1, int i2, int i3, bool z4, ItemStack itemStack5, short s6)
		{
			window_Id = i1;
			inventorySlot = i2;
			mouseClick = i3;
			itemStack = itemStack5;
			action = s6;
			holdingShift = z4;
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleWindowClick(this);
		}
        
		public override void readPacketData(BinaryReader dataInputStream1)
		{
			window_Id = dataInputStream1.ReadSByte();
			inventorySlot = dataInputStream1.ReadInt16BigEndian();
			mouseClick = dataInputStream1.ReadSByte();
			action = dataInputStream1.ReadInt16BigEndian();
			holdingShift = dataInputStream1.ReadBoolean();
			itemStack = this.ReadItemStack(dataInputStream1);
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write((sbyte)window_Id);
			dataOutputStream1.WriteBigEndian((short)inventorySlot);
			dataOutputStream1.Write((sbyte)mouseClick);
			dataOutputStream1.WriteBigEndian(action);
			dataOutputStream1.Write(holdingShift);
			this.writeItemStack(this.itemStack, dataOutputStream1);
		}

		public override int PacketSize
		{
			get
			{
				return 11;
			}
		}
	}

}