using BlockByBlock.java_extensions;

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
			respawnDimension = i1;
			difficulty = b2;
			worldHeight = i4;
			creativeMode = i5;
			terrainType = worldType3;
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleRespawn(this);
		}
        
		public override void readPacketData(BinaryReader dataInputStream1)
		{
			respawnDimension = dataInputStream1.ReadInt32BigEndian();
			difficulty = dataInputStream1.ReadSByte();
			creativeMode = dataInputStream1.ReadSByte();
			worldHeight = dataInputStream1.ReadInt16BigEndian();
			string string2 = readString(dataInputStream1, 16);
			terrainType = WorldType.parseWorldType(string2);
			if (terrainType == null)
			{
				terrainType = WorldType.DEFAULT;
			}

		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(respawnDimension);
			dataOutputStream1.Write((sbyte)difficulty);
			dataOutputStream1.Write((sbyte)creativeMode);
			dataOutputStream1.WriteBigEndian((short)worldHeight);
			writeString(terrainType.func_48628_a(), dataOutputStream1);
		}

		public override int PacketSize
		{
			get
			{
				return 8 + terrainType.func_48628_a().Length;
			}
		}
	}

}