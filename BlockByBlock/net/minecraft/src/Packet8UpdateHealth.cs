namespace net.minecraft.src
{

	public class Packet8UpdateHealth : Packet
	{
		public int healthMP;
		public int food;
		public float foodSaturation;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.healthMP = dataInputStream1.readShort();
			this.food = dataInputStream1.readShort();
			this.foodSaturation = dataInputStream1.readFloat();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeShort(this.healthMP);
			dataOutputStream1.writeShort(this.food);
			dataOutputStream1.writeFloat(this.foodSaturation);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleUpdateHealth(this);
		}

		public override int PacketSize
		{
			get
			{
				return 8;
			}
		}
	}

}