using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet200Statistic : Packet
	{
		public int statisticId;
		public int amount;

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleStatistic(this);
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			statisticId = dataInputStream1.ReadInt32BigEndian();
			amount = dataInputStream1.ReadSByte();
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(statisticId);
			dataOutputStream1.Write((sbyte)amount);
		}

		public override int PacketSize
		{
			get
			{
				return 6;
			}
		}
	}

}