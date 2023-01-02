using BlockByBlock.java_extensions;
using net.minecraft.client.entity;

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
			entityId = entityItem1.entityId;
			itemID = entityItem1.item.itemID;
			count = entityItem1.item.stackSize;
			itemDamage = entityItem1.item.ItemDamage;
			xPosition = MathHelper.floor_double(entityItem1.posX * 32.0D);
			yPosition = MathHelper.floor_double(entityItem1.posY * 32.0D);
			zPosition = MathHelper.floor_double(entityItem1.posZ * 32.0D);
			rotation = (sbyte)((int)(entityItem1.motionX * 128.0D));
			pitch = (sbyte)((int)(entityItem1.motionY * 128.0D));
			roll = (sbyte)((int)(entityItem1.motionZ * 128.0D));
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			entityId = dataInputStream1.ReadInt32BigEndian();
			itemID = dataInputStream1.ReadInt16BigEndian();
			count = dataInputStream1.ReadSByte();
			itemDamage = dataInputStream1.ReadInt16BigEndian();
			xPosition = dataInputStream1.ReadInt32BigEndian();
			yPosition = dataInputStream1.ReadInt32BigEndian();
			zPosition = dataInputStream1.ReadInt32BigEndian();
			rotation = dataInputStream1.ReadSByte();
			pitch = dataInputStream1.ReadSByte();
			roll = dataInputStream1.ReadSByte();
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(entityId);
			dataOutputStream1.WriteBigEndian((short)itemID);
			dataOutputStream1.Write((sbyte)count);
			dataOutputStream1.WriteBigEndian((short)itemDamage);
			dataOutputStream1.WriteBigEndian(xPosition);
			dataOutputStream1.WriteBigEndian(yPosition);
			dataOutputStream1.WriteBigEndian(zPosition);
			dataOutputStream1.Write(rotation);
			dataOutputStream1.Write(pitch);
			dataOutputStream1.Write(roll);
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