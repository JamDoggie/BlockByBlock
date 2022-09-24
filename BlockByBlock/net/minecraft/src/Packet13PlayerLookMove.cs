namespace net.minecraft.src
{

	public class Packet13PlayerLookMove : Packet10Flying
	{
		public Packet13PlayerLookMove()
		{
			this.rotating = true;
			this.moving = true;
		}

		public Packet13PlayerLookMove(double d1, double d3, double d5, double d7, float f9, float f10, bool z11)
		{
			this.xPosition = d1;
			this.yPosition = d3;
			this.stance = d5;
			this.zPosition = d7;
			this.yaw = f9;
			this.pitch = f10;
			this.onGround = z11;
			this.rotating = true;
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
			this.yaw = dataInputStream1.readFloat();
			this.pitch = dataInputStream1.readFloat();
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
			dataOutputStream1.writeFloat(this.yaw);
			dataOutputStream1.writeFloat(this.pitch);
			base.writePacketData(dataOutputStream1);
		}

		public override int PacketSize
		{
			get
			{
				return 41;
			}
		}
	}

}