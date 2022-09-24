namespace net.minecraft.src
{

	public class Packet61DoorChange : Packet
	{
		public int sfxID;
		public int auxData;
		public int posX;
		public int posY;
		public int posZ;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.sfxID = dataInputStream1.readInt();
			this.posX = dataInputStream1.readInt();
			this.posY = dataInputStream1.readByte() & 255;
			this.posZ = dataInputStream1.readInt();
			this.auxData = dataInputStream1.readInt();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.sfxID);
			dataOutputStream1.writeInt(this.posX);
			dataOutputStream1.writeByte(this.posY & 255);
			dataOutputStream1.writeInt(this.posZ);
			dataOutputStream1.writeInt(this.auxData);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleDoorChange(this);
		}

		public override int PacketSize
		{
			get
			{
				return 20;
			}
		}
	}

}