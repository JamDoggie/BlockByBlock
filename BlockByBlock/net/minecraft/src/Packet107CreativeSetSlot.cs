namespace net.minecraft.src
{

	public class Packet107CreativeSetSlot : Packet
	{
		public int slot;
		public ItemStack itemStack;

		public Packet107CreativeSetSlot()
		{
		}

		public Packet107CreativeSetSlot(int i1, ItemStack itemStack2)
		{
			this.slot = i1;
			this.itemStack = itemStack2;
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleCreativeSetSlot(this);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.slot = dataInputStream1.readShort();
			this.itemStack = this.readItemStack(dataInputStream1);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeShort(this.slot);
			this.writeItemStack(this.itemStack, dataOutputStream1);
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