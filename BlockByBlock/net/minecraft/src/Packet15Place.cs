namespace net.minecraft.src
{

	public class Packet15Place : Packet
	{
		public int xPosition;
		public int yPosition;
		public int zPosition;
		public int direction;
		public ItemStack itemStack;

		public Packet15Place()
		{
		}

		public Packet15Place(int i1, int i2, int i3, int i4, ItemStack itemStack5)
		{
			this.xPosition = i1;
			this.yPosition = i2;
			this.zPosition = i3;
			this.direction = i4;
			this.itemStack = itemStack5;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.xPosition = dataInputStream1.readInt();
			this.yPosition = dataInputStream1.read();
			this.zPosition = dataInputStream1.readInt();
			this.direction = dataInputStream1.read();
			this.itemStack = this.readItemStack(dataInputStream1);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.xPosition);
			dataOutputStream1.write(this.yPosition);
			dataOutputStream1.writeInt(this.zPosition);
			dataOutputStream1.write(this.direction);
			this.writeItemStack(this.itemStack, dataOutputStream1);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handlePlace(this);
		}

		public override int PacketSize
		{
			get
			{
				return 15;
			}
		}
	}

}