namespace net.minecraft.src
{

	public class Packet41EntityEffect : Packet
	{
		public int entityId;
		public sbyte effectId;
		public sbyte effectAmp;
		public short duration;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.entityId = dataInputStream1.readInt();
			this.effectId = dataInputStream1.readByte();
			this.effectAmp = dataInputStream1.readByte();
			this.duration = dataInputStream1.readShort();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.entityId);
			dataOutputStream1.writeByte(this.effectId);
			dataOutputStream1.writeByte(this.effectAmp);
			dataOutputStream1.writeShort(this.duration);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleEntityEffect(this);
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