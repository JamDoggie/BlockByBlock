namespace net.minecraft.src
{

	public class Packet105UpdateProgressbar : Packet
	{
		public int windowId;
		public int progressBar;
		public int progressBarValue;

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleUpdateProgressbar(this);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.windowId = dataInputStream1.readByte();
			this.progressBar = dataInputStream1.readShort();
			this.progressBarValue = dataInputStream1.readShort();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeByte(this.windowId);
			dataOutputStream1.writeShort(this.progressBar);
			dataOutputStream1.writeShort(this.progressBarValue);
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