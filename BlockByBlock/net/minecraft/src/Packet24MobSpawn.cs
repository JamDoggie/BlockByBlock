using BlockByBlock.java_extensions;
using net.minecraft.client.entity;

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
		public sbyte headYaw;
		private DataWatcher metaData;
		private System.Collections.IList receivedMetadata;

		public Packet24MobSpawn()
		{
		}

		public Packet24MobSpawn(EntityLiving entityLiving1)
		{
			entityId = entityLiving1.entityId;
			type = (sbyte)EntityList.getEntityID(entityLiving1);
			xPosition = MathHelper.floor_double(entityLiving1.posX * 32.0D);
			yPosition = MathHelper.floor_double(entityLiving1.posY * 32.0D);
			zPosition = MathHelper.floor_double(entityLiving1.posZ * 32.0D);
			yaw = (sbyte)((int)(entityLiving1.rotationYaw * 256.0F / 360.0F));
			pitch = (sbyte)((int)(entityLiving1.rotationPitch * 256.0F / 360.0F));
			headYaw = (sbyte)((int)(entityLiving1.rotationYawHead * 256.0F / 360.0F));
			metaData = entityLiving1.DataWatcher;
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			entityId = dataInputStream1.ReadInt32BigEndian();
			type = dataInputStream1.ReadSByte() & 255;
			xPosition = dataInputStream1.ReadInt32BigEndian();
			yPosition = dataInputStream1.ReadInt32BigEndian();
			zPosition = dataInputStream1.ReadInt32BigEndian();
			yaw = dataInputStream1.ReadSByte();
			pitch = dataInputStream1.ReadSByte();
			headYaw = dataInputStream1.ReadSByte();
			receivedMetadata = DataWatcher.readWatchableObjects(dataInputStream1);
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(entityId);
			dataOutputStream1.Write((byte)(type & 255));
			dataOutputStream1.WriteBigEndian(xPosition);
			dataOutputStream1.WriteBigEndian(yPosition);
			dataOutputStream1.WriteBigEndian(zPosition);
			dataOutputStream1.Write(yaw);
			dataOutputStream1.Write(pitch);
			dataOutputStream1.Write(headYaw);
			metaData.writeWatchableObjects(dataOutputStream1);
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
				return receivedMetadata;
			}
		}
	}

}