namespace net.minecraft.src
{

	public class Packet13PlayerLookMove : Packet10Flying
	{
		public Packet13PlayerLookMove()
		{
			rotating = true;
			moving = true;
		}

		public Packet13PlayerLookMove(double d1, double d3, double d5, double d7, float f9, float f10, bool z11)
		{
			xPosition = d1;
			yPosition = d3;
			stance = d5;
			zPosition = d7;
			yaw = f9;
			pitch = f10;
			onGround = z11;
			rotating = true;
			moving = true;
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			xPosition = dataInputStream1.ReadDouble();
			yPosition = dataInputStream1.ReadDouble();
			stance = dataInputStream1.ReadDouble();
			zPosition = dataInputStream1.ReadDouble();
			yaw = dataInputStream1.ReadSingle();
			pitch = dataInputStream1.ReadSingle();
			base.readPacketData(dataInputStream1);
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write(xPosition);
			dataOutputStream1.Write(yPosition);
			dataOutputStream1.Write(stance);
			dataOutputStream1.Write(zPosition);
			dataOutputStream1.Write(yaw);
			dataOutputStream1.Write(pitch);
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