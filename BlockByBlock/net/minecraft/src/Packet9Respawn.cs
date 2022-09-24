namespace net.minecraft.src
{

	public class Packet9Respawn : Packet
	{
		public int respawnDimension;
		public int difficulty;
		public int worldHeight;
		public int creativeMode;
		public WorldType terrainType;

		public Packet9Respawn()
		{
		}

		public Packet9Respawn(int i1, sbyte b2, WorldType worldType3, int i4, int i5)
		{
			this.respawnDimension = i1;
			this.difficulty = b2;
			this.worldHeight = i4;
			this.creativeMode = i5;
			this.terrainType = worldType3;
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleRespawn(this);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.respawnDimension = dataInputStream1.readInt();
			this.difficulty = dataInputStream1.readByte();
			this.creativeMode = dataInputStream1.readByte();
			this.worldHeight = dataInputStream1.readShort();
			string string2 = readString(dataInputStream1, 16);
			this.terrainType = WorldType.parseWorldType(string2);
			if (this.terrainType == null)
			{
				this.terrainType = WorldType.DEFAULT;
			}

		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.respawnDimension);
			dataOutputStream1.writeByte(this.difficulty);
			dataOutputStream1.writeByte(this.creativeMode);
			dataOutputStream1.writeShort(this.worldHeight);
			writeString(this.terrainType.func_48628_a(), dataOutputStream1);
		}

		public override int PacketSize
		{
			get
			{
				return 8 + this.terrainType.func_48628_a().Length;
			}
		}
	}

}