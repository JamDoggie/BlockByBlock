namespace net.minecraft.src
{

	public class Packet25EntityPainting : Packet
	{
		public int entityId;
		public int xPosition;
		public int yPosition;
		public int zPosition;
		public int direction;
		public string title;

		public Packet25EntityPainting()
		{
		}

		public Packet25EntityPainting(EntityPainting entityPainting1)
		{
			entityId = entityPainting1.entityId;
			xPosition = entityPainting1.xPosition;
			yPosition = entityPainting1.yPosition;
			zPosition = entityPainting1.zPosition;
			direction = entityPainting1.direction;
			title = entityPainting1.art.title;
		}
        
		public override void readPacketData(BinaryReader dataInputStream1)
		{
			entityId = dataInputStream1.ReadInt32();
			title = readString(dataInputStream1, EnumArt.maxArtTitleLength);
			xPosition = dataInputStream1.ReadInt32();
			yPosition = dataInputStream1.ReadInt32();
			zPosition = dataInputStream1.ReadInt32();
			direction = dataInputStream1.ReadInt32();
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write(entityId);
			writeString(title, dataOutputStream1);
			dataOutputStream1.Write(xPosition);
			dataOutputStream1.Write(yPosition);
			dataOutputStream1.Write(zPosition);
			dataOutputStream1.Write(direction);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleEntityPainting(this);
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