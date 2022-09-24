namespace net.minecraft.src
{

	public class Packet43Experience : Packet
	{
		public float experience;
		public int experienceTotal;
		public int experienceLevel;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.experience = dataInputStream1.readFloat();
			this.experienceLevel = dataInputStream1.readShort();
			this.experienceTotal = dataInputStream1.readShort();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeFloat(this.experience);
			dataOutputStream1.writeShort(this.experienceLevel);
			dataOutputStream1.writeShort(this.experienceTotal);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleExperience(this);
		}

		public override int PacketSize
		{
			get
			{
				return 4;
			}
		}
	}

}