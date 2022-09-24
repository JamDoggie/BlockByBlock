namespace net.minecraft.src
{

	public class Packet24MobSpawn : Packet
	{
		public int entityId;
		public int type;
		public int xPosition;
		public int yPosition;
		public int zPosition;
		public sbyte yaw;
		public sbyte pitch;
		public sbyte field_48169_h;
		private DataWatcher metaData;
		private System.Collections.IList receivedMetadata;

		public Packet24MobSpawn()
		{
		}

		public Packet24MobSpawn(EntityLiving entityLiving1)
		{
			this.entityId = entityLiving1.entityId;
			this.type = (sbyte)EntityList.getEntityID(entityLiving1);
			this.xPosition = MathHelper.floor_double(entityLiving1.posX * 32.0D);
			this.yPosition = MathHelper.floor_double(entityLiving1.posY * 32.0D);
			this.zPosition = MathHelper.floor_double(entityLiving1.posZ * 32.0D);
			this.yaw = (sbyte)((int)(entityLiving1.rotationYaw * 256.0F / 360.0F));
			this.pitch = (sbyte)((int)(entityLiving1.rotationPitch * 256.0F / 360.0F));
			this.field_48169_h = (sbyte)((int)(entityLiving1.rotationYawHead * 256.0F / 360.0F));
			this.metaData = entityLiving1.DataWatcher;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.entityId = dataInputStream1.readInt();
			this.type = dataInputStream1.readByte() & 255;
			this.xPosition = dataInputStream1.readInt();
			this.yPosition = dataInputStream1.readInt();
			this.zPosition = dataInputStream1.readInt();
			this.yaw = dataInputStream1.readByte();
			this.pitch = dataInputStream1.readByte();
			this.field_48169_h = dataInputStream1.readByte();
			this.receivedMetadata = DataWatcher.readWatchableObjects(dataInputStream1);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.entityId);
			dataOutputStream1.writeByte(this.type & 255);
			dataOutputStream1.writeInt(this.xPosition);
			dataOutputStream1.writeInt(this.yPosition);
			dataOutputStream1.writeInt(this.zPosition);
			dataOutputStream1.writeByte(this.yaw);
			dataOutputStream1.writeByte(this.pitch);
			dataOutputStream1.writeByte(this.field_48169_h);
			this.metaData.writeWatchableObjects(dataOutputStream1);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleMobSpawn(this);
		}

		public override int PacketSize
		{
			get
			{
				return 20;
			}
		}

		public virtual System.Collections.IList Metadata
		{
			get
			{
				return this.receivedMetadata;
			}
		}
	}

}