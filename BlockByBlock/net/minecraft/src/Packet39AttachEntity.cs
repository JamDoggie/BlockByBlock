namespace net.minecraft.src
{

	public class Packet39AttachEntity : Packet
	{
		public int entityId;
		public int vehicleEntityId;

		public override int PacketSize
		{
			get
			{
				return 8;
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.entityId = dataInputStream1.readInt();
			this.vehicleEntityId = dataInputStream1.readInt();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.entityId);
			dataOutputStream1.writeInt(this.vehicleEntityId);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleAttachEntity(this);
		}
	}

}