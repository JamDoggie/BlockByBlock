namespace net.minecraft.src
{

	public class Packet19EntityAction : Packet
	{
		public int entityId;
		public int state;

		public Packet19EntityAction()
		{
		}

		public Packet19EntityAction(Entity entity1, int i2)
		{
			this.entityId = entity1.entityId;
			this.state = i2;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.entityId = dataInputStream1.readInt();
			this.state = dataInputStream1.readByte();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.entityId);
			dataOutputStream1.writeByte(this.state);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleEntityAction(this);
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