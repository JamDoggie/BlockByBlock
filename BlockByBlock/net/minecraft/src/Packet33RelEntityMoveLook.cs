namespace net.minecraft.src
{

	public class Packet33RelEntityMoveLook : Packet30Entity
	{
		public Packet33RelEntityMoveLook()
		{
			this.rotating = true;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			base.readPacketData(dataInputStream1);
			this.xPosition = dataInputStream1.readByte();
			this.yPosition = dataInputStream1.readByte();
			this.zPosition = dataInputStream1.readByte();
			this.yaw = dataInputStream1.readByte();
			this.pitch = dataInputStream1.readByte();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			base.writePacketData(dataOutputStream1);
			dataOutputStream1.writeByte(this.xPosition);
			dataOutputStream1.writeByte(this.yPosition);
			dataOutputStream1.writeByte(this.zPosition);
			dataOutputStream1.writeByte(this.yaw);
			dataOutputStream1.writeByte(this.pitch);
		}

		public override int PacketSize
		{
			get
			{
				return 9;
			}
		}
	}

}