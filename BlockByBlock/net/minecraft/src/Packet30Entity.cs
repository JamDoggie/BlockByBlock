namespace net.minecraft.src
{

	public class Packet30Entity : Packet
	{
		public int entityId;
		public sbyte xPosition;
		public sbyte yPosition;
		public sbyte zPosition;
		public sbyte yaw;
		public sbyte pitch;
		public bool rotating = false;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.entityId = dataInputStream1.readInt();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.entityId);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleEntity(this);
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