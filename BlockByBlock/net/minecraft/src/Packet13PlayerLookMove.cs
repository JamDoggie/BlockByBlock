using BlockByBlock.java_extensions;

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
			xPosition = dataInputStream1.ReadDoubleBigEndian();
			yPosition = dataInputStream1.ReadDoubleBigEndian();
			stance = dataInputStream1.ReadDoubleBigEndian();
			zPosition = dataInputStream1.ReadDoubleBigEndian();
			yaw = dataInputStream1.ReadSingleBigEndian();
			pitch = dataInputStream1.ReadSingleBigEndian();
			base.readPacketData(dataInputStream1);
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(xPosition);
			dataOutputStream1.WriteBigEndian(yPosition);
			dataOutputStream1.WriteBigEndian(stance);
			dataOutputStream1.WriteBigEndian(zPosition);
			dataOutputStream1.WriteBigEndian(yaw);
			dataOutputStream1.WriteBigEndian(pitch);
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