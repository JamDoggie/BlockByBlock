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
		public sbyte worldHeight;
		public sbyte maxPlayers;

		public Packet1Login()
		{
		}

		public Packet1Login(string string1, int i2)
		{
			this.username = string1;
			this.protocolVersion = i2;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.protocolVersion = dataInputStream1.readInt();
			this.username = readString(dataInputStream1, 16);
			string string2 = readString(dataInputStream1, 16);
			this.terrainType = WorldType.parseWorldType(string2);
			if (this.terrainType == null)
			{
				this.terrainType = WorldType.DEFAULT;
			}

			this.serverMode = dataInputStream1.readInt();
			this.field_48170_e = dataInputStream1.readInt();
			this.difficultySetting = dataInputStream1.readByte();
			this.worldHeight = dataInputStream1.readByte();
			this.maxPlayers = dataInputStream1.readByte();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.protocolVersion);
			writeString(this.username, dataOutputStream1);
			if (this.terrainType == null)
			{
				writeString("", dataOutputStream1);
			}
			else
			{
				writeString(this.terrainType.func_48628_a(), dataOutputStream1);
			}

			dataOutputStream1.writeInt(this.serverMode);
			dataOutputStream1.writeInt(this.field_48170_e);
			dataOutputStream1.writeByte(this.difficultySetting);
			dataOutputStream1.writeByte(this.worldHeight);
			dataOutputStream1.writeByte(this.maxPlayers);
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
				if (this.terrainType != null)
				{
					i1 = this.terrainType.func_48628_a().Length;
				}
    
				return 4 + this.username.Length + 4 + 7 + 7 + i1;
			}
		}
	}

}