namespace net.minecraft.src
{

	public class Packet34EntityTeleport : Packet
	{
		public int entityId;
		public int xPosition;
		public int yPosition;
		public int zPosition;
		public sbyte yaw;
		public sbyte pitch;

		public Packet34EntityTeleport()
		{
		}

		public Packet34EntityTeleport(Entity entity1)
		{
			this.entityId = entity1.entityId;
			this.xPosition = MathHelper.floor_double(entity1.posX * 32.0D);
			this.yPosition = MathHelper.floor_double(entity1.posY * 32.0D);
			this.zPosition = MathHelper.floor_double(entity1.posZ * 32.0D);
			this.yaw = (sbyte)((int)(entity1.rotationYaw * 256.0F / 360.0F));
			this.pitch = (sbyte)((int)(entity1.rotationPitch * 256.0F / 360.0F));
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.entityId = dataInputStream1.readInt();
			this.xPosition = dataInputStream1.readInt();
			this.yPosition = dataInputStream1.readInt();
			this.zPosition = dataInputStream1.readInt();
			this.yaw = (sbyte)dataInputStream1.read();
			this.pitch = (sbyte)dataInputStream1.read();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.entityId);
			dataOutputStream1.writeInt(this.xPosition);
			dataOutputStream1.writeInt(this.yPosition);
			dataOutputStream1.writeInt(this.zPosition);
			dataOutputStream1.write(this.yaw);
			dataOutputStream1.write(this.pitch);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleEntityTeleport(this);
		}

		public override int PacketSize
		{
			get
			{
				return 34;
			}
		}
	}

}