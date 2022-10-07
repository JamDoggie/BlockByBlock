using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet8UpdateHealth : Packet
	{
		public int healthMP;
		public int food;
		public float foodSaturation;
        
		public override void readPacketData(BinaryReader dataInputStream1)
		{
			healthMP = dataInputStream1.ReadInt16BigEndian();
			food = dataInputStream1.ReadInt16BigEndian();
			foodSaturation = dataInputStream1.ReadSingleBigEndian();
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian((short)healthMP);
			dataOutputStream1.WriteBigEndian((short)food);
			dataOutputStream1.WriteBigEndian(foodSaturation);
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