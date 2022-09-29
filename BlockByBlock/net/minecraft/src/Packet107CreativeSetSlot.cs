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
			slot = i1;
			itemStack = itemStack2;
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleCreativeSetSlot(this);
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			slot = dataInputStream1.ReadInt16();
			itemStack = readItemStack(dataInputStream1);
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write((short)slot);
			writeItemStack(itemStack, dataOutputStream1);
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