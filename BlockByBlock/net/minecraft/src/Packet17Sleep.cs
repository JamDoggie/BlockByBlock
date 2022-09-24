namespace net.minecraft.src
{

	public class Packet17Sleep : Packet
	{
		public int entityID;
		public int bedX;
		public int bedY;
		public int bedZ;
		public int field_22046_e;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.entityID = dataInputStream1.readInt();
			this.field_22046_e = dataInputStream1.readByte();
			this.bedX = dataInputStream1.readInt();
			this.bedY = dataInputStream1.readByte();
			this.bedZ = dataInputStream1.readInt();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.entityID);
			dataOutputStream1.writeByte(this.field_22046_e);
			dataOutputStream1.writeInt(this.bedX);
			dataOutputStream1.writeByte(this.bedY);
			dataOutputStream1.writeInt(this.bedZ);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleSleep(this);
		}

		public override int PacketSize
		{
			get
			{
				return 14;
			}
		}
	}

}