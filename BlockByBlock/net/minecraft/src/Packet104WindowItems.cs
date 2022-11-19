using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet104WindowItems : Packet
	{
		public int windowId;
		public ItemStack[] itemStack;

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			windowId = dataInputStream1.ReadSByte();
			short s2 = dataInputStream1.ReadInt16BigEndian();
			itemStack = new ItemStack[s2];

			for (int i3 = 0; i3 < s2; ++i3)
			{
				itemStack[i3] = ReadItemStack(dataInputStream1);
			}

		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write((sbyte)windowId);
			dataOutputStream1.WriteBigEndian((short)itemStack.Length);

			for (int i2 = 0; i2 < itemStack.Length; ++i2)
			{
				writeItemStack(itemStack[i2], dataOutputStream1);
			}

		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleWindowItems(this);
		}

		public override int PacketSize
		{
			get
			{
				return 3 + itemStack.Length * 5;
			}
		}
	}

}