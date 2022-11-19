using BlockByBlock.java_extensions;

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
			itemSlot = dataInputStream1.ReadInt16BigEndian();
			myItemStack = this.ReadItemStack(dataInputStream1);
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write((sbyte)windowId);
			dataOutputStream1.WriteBigEndian((short)itemSlot);
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