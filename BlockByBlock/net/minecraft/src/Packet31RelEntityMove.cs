namespace net.minecraft.src
{

	public class Packet31RelEntityMove : Packet30Entity
	{
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			base.readPacketData(dataInputStream1);
			this.xPosition = dataInputStream1.readByte();
			this.yPosition = dataInputStream1.readByte();
			this.zPosition = dataInputStream1.readByte();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			base.writePacketData(dataOutputStream1);
			dataOutputStream1.writeByte(this.xPosition);
			dataOutputStream1.writeByte(this.yPosition);
			dataOutputStream1.writeByte(this.zPosition);
		}

		public override int PacketSize
		{
			get
			{
				return 7;
			}
		}
	}

}