namespace net.minecraft.src
{

	public class Packet104WindowItems : Packet
	{
		public int windowId;
		public ItemStack[] itemStack;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.windowId = dataInputStream1.readByte();
			short s2 = dataInputStream1.readShort();
			this.itemStack = new ItemStack[s2];

			for (int i3 = 0; i3 < s2; ++i3)
			{
				this.itemStack[i3] = this.readItemStack(dataInputStream1);
			}

		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeByte(this.windowId);
			dataOutputStream1.writeShort(this.itemStack.Length);

			for (int i2 = 0; i2 < this.itemStack.Length; ++i2)
			{
				this.writeItemStack(this.itemStack[i2], dataOutputStream1);
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
				return 3 + this.itemStack.Length * 5;
			}
		}
	}

}