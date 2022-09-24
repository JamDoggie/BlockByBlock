namespace net.minecraft.src
{

	public class Packet26EntityExpOrb : Packet
	{
		public int entityId;
		public int posX;
		public int posY;
		public int posZ;
		public int xpValue;

		public Packet26EntityExpOrb()
		{
		}

		public Packet26EntityExpOrb(EntityXPOrb entityXPOrb1)
		{
			this.entityId = entityXPOrb1.entityId;
			this.posX = MathHelper.floor_double(entityXPOrb1.posX * 32.0D);
			this.posY = MathHelper.floor_double(entityXPOrb1.posY * 32.0D);
			this.posZ = MathHelper.floor_double(entityXPOrb1.posZ * 32.0D);
			this.xpValue = entityXPOrb1.XpValue;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.entityId = dataInputStream1.readInt();
			this.posX = dataInputStream1.readInt();
			this.posY = dataInputStream1.readInt();
			this.posZ = dataInputStream1.readInt();
			this.xpValue = dataInputStream1.readShort();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.entityId);
			dataOutputStream1.writeInt(this.posX);
			dataOutputStream1.writeInt(this.posY);
			dataOutputStream1.writeInt(this.posZ);
			dataOutputStream1.writeShort(this.xpValue);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleEntityExpOrb(this);
		}

		public override int PacketSize
		{
			get
			{
				return 18;
			}
		}
	}

}