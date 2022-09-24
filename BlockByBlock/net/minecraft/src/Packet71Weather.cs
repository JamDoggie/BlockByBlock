namespace net.minecraft.src
{

	public class Packet71Weather : Packet
	{
		public int entityID;
		public int posX;
		public int posY;
		public int posZ;
		public int isLightningBolt;

		public Packet71Weather()
		{
		}

		public Packet71Weather(Entity entity1)
		{
			this.entityID = entity1.entityId;
			this.posX = MathHelper.floor_double(entity1.posX * 32.0D);
			this.posY = MathHelper.floor_double(entity1.posY * 32.0D);
			this.posZ = MathHelper.floor_double(entity1.posZ * 32.0D);
			if (entity1 is EntityLightningBolt)
			{
				this.isLightningBolt = 1;
			}

		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.entityID = dataInputStream1.readInt();
			this.isLightningBolt = dataInputStream1.readByte();
			this.posX = dataInputStream1.readInt();
			this.posY = dataInputStream1.readInt();
			this.posZ = dataInputStream1.readInt();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.entityID);
			dataOutputStream1.writeByte(this.isLightningBolt);
			dataOutputStream1.writeInt(this.posX);
			dataOutputStream1.writeInt(this.posY);
			dataOutputStream1.writeInt(this.posZ);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleWeather(this);
		}

		public override int PacketSize
		{
			get
			{
				return 17;
			}
		}
	}

}