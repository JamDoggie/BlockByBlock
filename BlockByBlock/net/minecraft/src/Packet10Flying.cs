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
			this.onGround = z1;
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleFlying(this);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.onGround = dataInputStream1.read() != 0;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.write(this.onGround ? 1 : 0);
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