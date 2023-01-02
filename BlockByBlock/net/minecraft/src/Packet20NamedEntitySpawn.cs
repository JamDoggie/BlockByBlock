using BlockByBlock.java_extensions;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class Packet20NamedEntitySpawn : Packet
	{
		public int entityId;
		public string name;
		public int xPosition;
		public int yPosition;
		public int zPosition;
		public sbyte rotation;
		public sbyte pitch;
		public int currentItem;

		public Packet20NamedEntitySpawn()
		{
		}

		public Packet20NamedEntitySpawn(EntityPlayer entityPlayer1)
		{
			entityId = entityPlayer1.entityId;
			name = entityPlayer1.username;
			xPosition = MathHelper.floor_double(entityPlayer1.posX * 32.0D);
			yPosition = MathHelper.floor_double(entityPlayer1.posY * 32.0D);
			zPosition = MathHelper.floor_double(entityPlayer1.posZ * 32.0D);
			rotation = (sbyte)((int)(entityPlayer1.rotationYaw * 256.0F / 360.0F));
			pitch = (sbyte)((int)(entityPlayer1.rotationPitch * 256.0F / 360.0F));
			ItemStack itemStack2 = entityPlayer1.inventory.CurrentItem;
			currentItem = itemStack2 == null ? 0 : itemStack2.itemID;
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			entityId = dataInputStream1.ReadInt32BigEndian();
			name = readString(dataInputStream1, 16);
			xPosition = dataInputStream1.ReadInt32BigEndian();
			yPosition = dataInputStream1.ReadInt32BigEndian();
			zPosition = dataInputStream1.ReadInt32BigEndian();
			rotation = dataInputStream1.ReadSByte();
			pitch = dataInputStream1.ReadSByte();
			currentItem = dataInputStream1.ReadInt16BigEndian();
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(entityId);
			writeString(name, dataOutputStream1);
			dataOutputStream1.WriteBigEndian(xPosition);
			dataOutputStream1.WriteBigEndian(yPosition);
			dataOutputStream1.WriteBigEndian(zPosition);
			dataOutputStream1.Write(rotation);
			dataOutputStream1.Write(pitch);
			dataOutputStream1.WriteBigEndian((short)currentItem);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleNamedEntitySpawn(this);
		}

		public override int PacketSize
		{
			get
			{
				return 28;
			}
		}
	}

}