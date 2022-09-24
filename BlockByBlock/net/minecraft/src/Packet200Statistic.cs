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

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.statisticId = dataInputStream1.readInt();
			this.amount = dataInputStream1.readByte();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.statisticId);
			dataOutputStream1.writeByte(this.amount);
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