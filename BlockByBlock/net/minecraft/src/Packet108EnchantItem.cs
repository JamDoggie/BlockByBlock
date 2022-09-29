namespace net.minecraft.src
{

	public class Packet108EnchantItem : Packet
	{
		public int windowId;
		public int enchantment;

		public Packet108EnchantItem()
		{
		}

		public Packet108EnchantItem(int i1, int i2)
		{
			this.windowId = i1;
			this.enchantment = i2;
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleEnchantItem(this);
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			windowId = dataInputStream1.ReadSByte();
			enchantment = dataInputStream1.ReadSByte();
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write((sbyte)windowId);
			dataOutputStream1.Write((sbyte)enchantment);
		}

		public override int PacketSize
		{
			get
			{
				return 2;
			}
		}
	}

}