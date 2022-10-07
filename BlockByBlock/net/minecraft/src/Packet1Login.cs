using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet1Login : Packet
	{
		public int protocolVersion;
		public string username;
		public WorldType terrainType;
		public int serverMode;
		public int field_48170_e;
		public sbyte difficultySetting;
		public byte worldHeight;
		public byte maxPlayers;

		public Packet1Login()
		{
		}

		public Packet1Login(string string1, int i2)
		{
			username = string1;
			protocolVersion = i2;
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			protocolVersion = dataInputStream1.ReadInt32BigEndian();
			username = readString(dataInputStream1, 16);
			string string2 = readString(dataInputStream1, 16);
			terrainType = WorldType.parseWorldType(string2);
			if (terrainType == null)
			{
				terrainType = WorldType.DEFAULT;
			}

			serverMode = dataInputStream1.ReadInt32BigEndian();
			field_48170_e = dataInputStream1.ReadInt32BigEndian();
			difficultySetting = dataInputStream1.ReadSByte();
			worldHeight = dataInputStream1.ReadByte();
			maxPlayers = dataInputStream1.ReadByte();
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(protocolVersion);
			writeString(username, dataOutputStream1);
			if (terrainType == null)
			{
				writeString("", dataOutputStream1);
			}
			else
			{
				writeString(terrainType.func_48628_a(), dataOutputStream1);
			}

			dataOutputStream1.WriteBigEndian(serverMode);
			dataOutputStream1.WriteBigEndian(field_48170_e);
			dataOutputStream1.Write(difficultySetting);
			dataOutputStream1.Write(worldHeight);
			dataOutputStream1.Write(maxPlayers);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleLogin(this);
		}

		public override int PacketSize
		{
			get
			{
				int i1 = 0;
				if (terrainType != null)
				{
					i1 = terrainType.func_48628_a().Length;
				}
    
				return 4 + username.Length + 4 + 7 + 7 + i1;
			}
		}
	}

}