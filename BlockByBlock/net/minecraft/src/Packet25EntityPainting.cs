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
			this.entityId = entityPainting1.entityId;
			this.xPosition = entityPainting1.xPosition;
			this.yPosition = entityPainting1.yPosition;
			this.zPosition = entityPainting1.zPosition;
			this.direction = entityPainting1.direction;
			this.title = entityPainting1.art.title;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.entityId = dataInputStream1.readInt();
			this.title = readString(dataInputStream1, EnumArt.maxArtTitleLength);
			this.xPosition = dataInputStream1.readInt();
			this.yPosition = dataInputStream1.readInt();
			this.zPosition = dataInputStream1.readInt();
			this.direction = dataInputStream1.readInt();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.entityId);
			writeString(this.title, dataOutputStream1);
			dataOutputStream1.writeInt(this.xPosition);
			dataOutputStream1.writeInt(this.yPosition);
			dataOutputStream1.writeInt(this.zPosition);
			dataOutputStream1.writeInt(this.direction);
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