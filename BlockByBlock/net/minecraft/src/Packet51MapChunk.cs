namespace net.minecraft.src
{

	public class Packet51MapChunk : Packet
	{
		public int xCh;
		public int zCh;
		public int yChMin;
		public int yChMax;
		public sbyte[] chunkData;
		public bool includeInitialize;
		private int tempLength;
		private int field_48178_h;
		private static sbyte[] temp = new sbyte[0];

		public Packet51MapChunk()
		{
			this.isChunkDataPacket = true;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.xCh = dataInputStream1.readInt();
			this.zCh = dataInputStream1.readInt();
			this.includeInitialize = dataInputStream1.readBoolean();
			this.yChMin = dataInputStream1.readShort();
			this.yChMax = dataInputStream1.readShort();
			this.tempLength = dataInputStream1.readInt();
			this.field_48178_h = dataInputStream1.readInt();
			if (temp.Length < this.tempLength)
			{
				temp = new sbyte[this.tempLength];
			}

			dataInputStream1.readFully(temp, 0, this.tempLength);
			int i2 = 0;

			int i3;
			for (i3 = 0; i3 < 16; ++i3)
			{
				i2 += this.yChMin >> i3 & 1;
			}

			i3 = 12288 * i2;
			if (this.includeInitialize)
			{
				i3 += 256;
			}

			this.chunkData = new sbyte[i3];
			Inflater inflater4 = new Inflater();
			inflater4.setInput(temp, 0, this.tempLength);

			try
			{
				inflater4.inflate(this.chunkData);
			}
			catch (DataFormatException)
			{
				throw new IOException("Bad compressed data format");
			}
			finally
			{
				inflater4.end();
			}

		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeInt(this.xCh);
			dataOutputStream1.writeInt(this.zCh);
			dataOutputStream1.writeBoolean(this.includeInitialize);
			dataOutputStream1.writeShort(unchecked((short)(this.yChMin & 65535)));
			dataOutputStream1.writeShort(unchecked((short)(this.yChMax & 65535)));
			dataOutputStream1.writeInt(this.tempLength);
			dataOutputStream1.writeInt(this.field_48178_h);
			dataOutputStream1.write(this.chunkData, 0, this.tempLength);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.func_48487_a(this);
		}

		public override int PacketSize
		{
			get
			{
				return 17 + this.tempLength;
			}
		}
	}

}