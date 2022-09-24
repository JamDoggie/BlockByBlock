namespace net.minecraft.src
{

	public class Packet11PlayerPosition : Packet10Flying
	{
		public Packet11PlayerPosition()
		{
			this.moving = true;
		}

		public Packet11PlayerPosition(double d1, double d3, double d5, double d7, bool z9)
		{
			this.xPosition = d1;
			this.yPosition = d3;
			this.stance = d5;
			this.zPosition = d7;
			this.onGround = z9;
			this.moving = true;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.xPosition = dataInputStream1.readDouble();
			this.yPosition = dataInputStream1.readDouble();
			this.stance = dataInputStream1.readDouble();
			this.zPosition = dataInputStream1.readDouble();
			base.readPacketData(dataInputStream1);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeDouble(this.xPosition);
			dataOutputStream1.writeDouble(this.yPosition);
			dataOutputStream1.writeDouble(this.stance);
			dataOutputStream1.writeDouble(this.zPosition);
			base.writePacketData(dataOutputStream1);
		}

		public override int PacketSize
		{
			get
			{
				return 33;
			}
		}
	}

}