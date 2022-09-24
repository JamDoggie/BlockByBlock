namespace net.minecraft.src
{

	public class Packet16BlockItemSwitch : Packet
	{
		public int id;

		public Packet16BlockItemSwitch()
		{
		}

		public Packet16BlockItemSwitch(int i1)
		{
			this.id = i1;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.id = dataInputStream1.readShort();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeShort(this.id);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleBlockItemSwitch(this);
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