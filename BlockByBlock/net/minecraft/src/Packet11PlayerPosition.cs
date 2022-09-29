namespace net.minecraft.src
{

	public class Packet11PlayerPosition : Packet10Flying
	{
		public Packet11PlayerPosition()
		{
			moving = true;
		}

		public Packet11PlayerPosition(double d1, double d3, double d5, double d7, bool z9)
		{
			xPosition = d1;
			yPosition = d3;
			stance = d5;
			zPosition = d7;
			onGround = z9;
			moving = true;
		}
        
		public override void readPacketData(BinaryReader dataInputStream1)
		{
			xPosition = dataInputStream1.ReadDouble();
			yPosition = dataInputStream1.ReadDouble();
			stance = dataInputStream1.ReadDouble();
			zPosition = dataInputStream1.ReadDouble();
			base.readPacketData(dataInputStream1);
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write(xPosition);
			dataOutputStream1.Write(yPosition);
			dataOutputStream1.Write(stance);
			dataOutputStream1.Write(zPosition);
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