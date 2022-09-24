namespace net.minecraft.src
{

	public class Packet22Collect : Packet
	{
		public int collectedEntityId;
		public int collectorEntityId;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.collectedEntityId = dataInputStream1.readInt();
			this.collectorEntityId = dataInputStream1.readInt();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.collectedEntityId);
			dataOutputStream1.writeInt(this.collectorEntityId);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleCollect(this);
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