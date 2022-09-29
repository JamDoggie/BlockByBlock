namespace net.minecraft.src
{

	public class Packet43Experience : Packet
	{
		public float experience;
		public int experienceTotal;
		public int experienceLevel;

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			experience = dataInputStream1.ReadSingle();
			experienceLevel = dataInputStream1.ReadInt16();
			experienceTotal = dataInputStream1.ReadInt16();
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write(experience);
			dataOutputStream1.Write((short)experienceLevel);
			dataOutputStream1.Write((short)experienceTotal);
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