namespace net.minecraft.src
{

	public class Packet250CustomPayload : Packet
	{
		public string channel;
		public int length;
		public sbyte[] data;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.channel = readString(dataInputStream1, 16);
			this.length = dataInputStream1.readShort();
			if (this.length > 0 && this.length < 32767)
			{
				this.data = new sbyte[this.length];
				dataInputStream1.readFully(this.data);
			}

		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			writeString(this.channel, dataOutputStream1);
			dataOutputStream1.writeShort((short)this.length);
			if (this.data != null)
			{
				dataOutputStream1.write(this.data);
			}

		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleCustomPayload(this);
		}

		public override int PacketSize
		{
			get
			{
				return 2 + this.channel.Length * 2 + 2 + this.length;
			}
		}
	}

}