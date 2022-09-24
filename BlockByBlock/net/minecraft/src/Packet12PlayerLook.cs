namespace net.minecraft.src
{

	public class Packet12PlayerLook : Packet10Flying
	{
		public Packet12PlayerLook()
		{
			this.rotating = true;
		}

		public Packet12PlayerLook(float f1, float f2, bool z3)
		{
			this.yaw = f1;
			this.pitch = f2;
			this.onGround = z3;
			this.rotating = true;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.yaw = dataInputStream1.readFloat();
			this.pitch = dataInputStream1.readFloat();
			base.readPacketData(dataInputStream1);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeFloat(this.yaw);
			dataOutputStream1.writeFloat(this.pitch);
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