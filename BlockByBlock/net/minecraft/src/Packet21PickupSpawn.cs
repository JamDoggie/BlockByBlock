namespace net.minecraft.src
{

	public class Packet21PickupSpawn : Packet
	{
		public int entityId;
		public int xPosition;
		public int yPosition;
		public int zPosition;
		public sbyte rotation;
		public sbyte pitch;
		public sbyte roll;
		public int itemID;
		public int count;
		public int itemDamage;

		public Packet21PickupSpawn()
		{
		}

		public Packet21PickupSpawn(EntityItem entityItem1)
		{
			this.entityId = entityItem1.entityId;
			this.itemID = entityItem1.item.itemID;
			this.count = entityItem1.item.stackSize;
			this.itemDamage = entityItem1.item.ItemDamage;
			this.xPosition = MathHelper.floor_double(entityItem1.posX * 32.0D);
			this.yPosition = MathHelper.floor_double(entityItem1.posY * 32.0D);
			this.zPosition = MathHelper.floor_double(entityItem1.posZ * 32.0D);
			this.rotation = (sbyte)((int)(entityItem1.motionX * 128.0D));
			this.pitch = (sbyte)((int)(entityItem1.motionY * 128.0D));
			this.roll = (sbyte)((int)(entityItem1.motionZ * 128.0D));
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.entityId = dataInputStream1.readInt();
			this.itemID = dataInputStream1.readShort();
			this.count = dataInputStream1.readByte();
			this.itemDamage = dataInputStream1.readShort();
			this.xPosition = dataInputStream1.readInt();
			this.yPosition = dataInputStream1.readInt();
			this.zPosition = dataInputStream1.readInt();
			this.rotation = dataInputStream1.readByte();
			this.pitch = dataInputStream1.readByte();
			this.roll = dataInputStream1.readByte();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.entityId);
			dataOutputStream1.writeShort(this.itemID);
			dataOutputStream1.writeByte(this.count);
			dataOutputStream1.writeShort(this.itemDamage);
			dataOutputStream1.writeInt(this.xPosition);
			dataOutputStream1.writeInt(this.yPosition);
			dataOutputStream1.writeInt(this.zPosition);
			dataOutputStream1.writeByte(this.rotation);
			dataOutputStream1.writeByte(this.pitch);
			dataOutputStream1.writeByte(this.roll);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handlePickupSpawn(this);
		}

		public override int PacketSize
		{
			get
			{
				return 24;
			}
		}
	}

}