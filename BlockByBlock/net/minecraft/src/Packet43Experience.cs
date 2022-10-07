using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet43Experience : Packet
	{
		public float experience;
		public int experienceTotal;
		public int experienceLevel;

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			experience = dataInputStream1.ReadSingleBigEndian();
			experienceLevel = dataInputStream1.ReadInt16BigEndian();
			experienceTotal = dataInputStream1.ReadInt16BigEndian();
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(experience);
			dataOutputStream1.WriteBigEndian((short)experienceLevel);
			dataOutputStream1.WriteBigEndian((short)experienceTotal);
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