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

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			windowId = dataInputStream1.ReadSByte();
			itemSlot = dataInputStream1.ReadInt16();
			myItemStack = this.readItemStack(dataInputStream1);
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write((sbyte)windowId);
			dataOutputStream1.Write((short)itemSlot);
			writeItemStack(myItemStack, dataOutputStream1);
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