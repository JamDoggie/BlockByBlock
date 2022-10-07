using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet10Flying : Packet
	{
		public double xPosition;
		public double yPosition;
		public double zPosition;
		public double stance;
		public float yaw;
		public float pitch;
		public bool onGround;
		public bool moving;
		public bool rotating;

		public Packet10Flying()
		{
		}

		public Packet10Flying(bool z1)
		{
			onGround = z1;
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleFlying(this);
		}
        
		public override void readPacketData(BinaryReader dataInputStream1)
		{
			onGround = dataInputStream1.ReadSByte() != 0;
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write(onGround ? (sbyte)1 : (sbyte)0);
		}

		public override int PacketSize
		{
			get
			{
				return 1;
			}
		}
	}

}