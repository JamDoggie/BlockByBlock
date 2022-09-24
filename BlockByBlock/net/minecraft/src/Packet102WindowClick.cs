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
			this.window_Id = i1;
			this.inventorySlot = i2;
			this.mouseClick = i3;
			this.itemStack = itemStack5;
			this.action = s6;
			this.holdingShift = z4;
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleWindowClick(this);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.window_Id = dataInputStream1.readByte();
			this.inventorySlot = dataInputStream1.readShort();
			this.mouseClick = dataInputStream1.readByte();
			this.action = dataInputStream1.readShort();
			this.holdingShift = dataInputStream1.readBoolean();
			this.itemStack = this.readItemStack(dataInputStream1);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeByte(this.window_Id);
			dataOutputStream1.writeShort(this.inventorySlot);
			dataOutputStream1.writeByte(this.mouseClick);
			dataOutputStream1.writeShort(this.action);
			dataOutputStream1.writeBoolean(this.holdingShift);
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