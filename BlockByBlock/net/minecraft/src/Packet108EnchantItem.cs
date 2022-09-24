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

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.windowId = dataInputStream1.readByte();
			this.enchantment = dataInputStream1.readByte();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeByte(this.windowId);
			dataOutputStream1.writeByte(this.enchantment);
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