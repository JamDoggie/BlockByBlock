namespace net.minecraft.src
{

	public class Packet7UseEntity : Packet
	{
		public int playerEntityId;
		public int targetEntity;
		public int isLeftClick;

		public Packet7UseEntity()
		{
		}

		public Packet7UseEntity(int i1, int i2, int i3)
		{
			this.playerEntityId = i1;
			this.targetEntity = i2;
			this.isLeftClick = i3;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.playerEntityId = dataInputStream1.readInt();
			this.targetEntity = dataInputStream1.readInt();
			this.isLeftClick = dataInputStream1.readByte();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.playerEntityId);
			dataOutputStream1.writeInt(this.targetEntity);
			dataOutputStream1.writeByte(this.isLeftClick);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleUseEntity(this);
		}

		public override int PacketSize
		{
			get
			{
				return 9;
			}
		}
	}

}