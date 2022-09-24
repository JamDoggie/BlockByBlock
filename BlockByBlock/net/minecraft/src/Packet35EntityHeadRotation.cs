namespace net.minecraft.src
{

	public class Packet35EntityHeadRotation : Packet
	{
		public int entityId;
		public sbyte headRotationYaw;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.entityId = dataInputStream1.readInt();
			this.headRotationYaw = dataInputStream1.readByte();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.entityId);
			dataOutputStream1.writeByte(this.headRotationYaw);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleEntityHeadRotation(this);
		}

		public override int PacketSize
		{
			get
			{
				return 5;
			}
		}
	}

}