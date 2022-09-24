namespace net.minecraft.src
{

	public class Packet106Transaction : Packet
	{
		public int windowId;
		public short shortWindowId;
		public bool accepted;

		public Packet106Transaction()
		{
		}

		public Packet106Transaction(int i1, short s2, bool z3)
		{
			this.windowId = i1;
			this.shortWindowId = s2;
			this.accepted = z3;
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleTransaction(this);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.windowId = dataInputStream1.readByte();
			this.shortWindowId = dataInputStream1.readShort();
			this.accepted = dataInputStream1.readByte() != 0;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeByte(this.windowId);
			dataOutputStream1.writeShort(this.shortWindowId);
			dataOutputStream1.writeByte(this.accepted ? 1 : 0);
		}

		public override int PacketSize
		{
			get
			{
				return 4;
			}
		}
	}

}