namespace net.minecraft.src
{

	public class Packet103SetSlot : Packet
	{
		public int windowId;
		public int itemSlot;
		public ItemStack myItemStack;

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleSetSlot(this);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.windowId = dataInputStream1.readByte();
			this.itemSlot = dataInputStream1.readShort();
			this.myItemStack = this.readItemStack(dataInputStream1);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeByte(this.windowId);
			dataOutputStream1.writeShort(this.itemSlot);
			this.writeItemStack(this.myItemStack, dataOutputStream1);
		}

		public override int PacketSize
		{
			get
			{
				return 8;
			}
		}
	}

}