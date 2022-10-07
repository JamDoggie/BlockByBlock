using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet12PlayerLook : Packet10Flying
	{
		public Packet12PlayerLook()
		{
			rotating = true;
		}

		public Packet12PlayerLook(float f1, float f2, bool z3)
		{
			yaw = f1;
			pitch = f2;
			onGround = z3;
			rotating = true;
		}
        
		public override void readPacketData(BinaryReader dataInputStream1)
		{
			yaw = dataInputStream1.ReadSingleBigEndian();
			pitch = dataInputStream1.ReadSingleBigEndian();
			base.readPacketData(dataInputStream1);
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(yaw);
			dataOutputStream1.WriteBigEndian(pitch);
			base.writePacketData(dataOutputStream1);
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