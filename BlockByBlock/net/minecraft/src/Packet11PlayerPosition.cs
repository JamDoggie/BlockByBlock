using BlockByBlock.java_extensions;

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
			xPosition = dataInputStream1.ReadDoubleBigEndian();
			yPosition = dataInputStream1.ReadDoubleBigEndian();
			stance = dataInputStream1.ReadDoubleBigEndian();
			zPosition = dataInputStream1.ReadDoubleBigEndian();
			base.readPacketData(dataInputStream1);
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(xPosition);
			dataOutputStream1.WriteBigEndian(yPosition);
			dataOutputStream1.WriteBigEndian(stance);
			dataOutputStream1.WriteBigEndian(zPosition);
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