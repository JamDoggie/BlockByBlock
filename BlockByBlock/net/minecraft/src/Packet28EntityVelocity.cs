namespace net.minecraft.src
{

	public class Packet28EntityVelocity : Packet
	{
		public int entityId;
		public int motionX;
		public int motionY;
		public int motionZ;

		public Packet28EntityVelocity()
		{
		}

		public Packet28EntityVelocity(Entity entity1) : this(entity1.entityId, entity1.motionX, entity1.motionY, entity1.motionZ)
		{
		}

		public Packet28EntityVelocity(int i1, double d2, double d4, double d6)
		{
			this.entityId = i1;
			double d8 = 3.9D;
			if (d2 < -d8)
			{
				d2 = -d8;
			}

			if (d4 < -d8)
			{
				d4 = -d8;
			}

			if (d6 < -d8)
			{
				d6 = -d8;
			}

			if (d2 > d8)
			{
				d2 = d8;
			}

			if (d4 > d8)
			{
				d4 = d8;
			}

			if (d6 > d8)
			{
				d6 = d8;
			}

			this.motionX = (int)(d2 * 8000.0D);
			this.motionY = (int)(d4 * 8000.0D);
			this.motionZ = (int)(d6 * 8000.0D);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.entityId = dataInputStream1.readInt();
			this.motionX = dataInputStream1.readShort();
			this.motionY = dataInputStream1.readShort();
			this.motionZ = dataInputStream1.readShort();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.entityId);
			dataOutputStream1.writeShort(this.motionX);
			dataOutputStream1.writeShort(this.motionY);
			dataOutputStream1.writeShort(this.motionZ);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleEntityVelocity(this);
		}

		public override int PacketSize
		{
			get
			{
				return 10;
			}
		}
	}

}