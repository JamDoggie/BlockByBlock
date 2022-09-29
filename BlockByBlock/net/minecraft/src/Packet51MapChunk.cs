using ICSharpCode.SharpZipLib.Zip.Compression;

namespace net.minecraft.src
{

	public class Packet51MapChunk : Packet
	{
		public int xCh;
		public int zCh;
		public int yChMin;
		public int yChMax;
		public byte[] chunkData;
		public bool includeInitialize;
		private int tempLength;
		private int field_48178_h;
		private static byte[] temp = new byte[0];

		public Packet51MapChunk()
		{
			isChunkDataPacket = true;
		}
        
		public override void readPacketData(BinaryReader dataInputStream1)
		{
			xCh = dataInputStream1.ReadInt32();
			zCh = dataInputStream1.ReadInt32();
			includeInitialize = dataInputStream1.ReadBoolean();
			yChMin = dataInputStream1.ReadUInt16();
			yChMax = dataInputStream1.ReadUInt16();
			tempLength = dataInputStream1.ReadInt32();
			field_48178_h = dataInputStream1.ReadInt32();
			if (temp.Length < tempLength)
			{
				temp = new byte[tempLength];
			}

			dataInputStream1.Read(temp, 0, tempLength);
			int i2 = 0;

			int i3;
			for (i3 = 0; i3 < 16; ++i3)
			{
				i2 += yChMin >> i3 & 1;
			}

			i3 = 12288 * i2;
			if (includeInitialize)
			{
				i3 += 256;
			}

			chunkData = new byte[i3];
			Inflater inflater4 = new Inflater();
			inflater4.SetInput(temp, 0, tempLength);

			try
			{
				inflater4.Inflate(chunkData);
			}
			catch (FormatException)
			{
				throw new IOException("Bad compressed data format");
			}
			finally
			{
				inflater4.Reset();
			}

		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write(xCh);
			dataOutputStream1.Write(zCh);
			dataOutputStream1.Write(includeInitialize);
			dataOutputStream1.Write(unchecked((ushort)(yChMin & 65535)));
			dataOutputStream1.Write(unchecked((ushort)(yChMax & 65535)));
			dataOutputStream1.Write(tempLength);
			dataOutputStream1.Write(field_48178_h);
			dataOutputStream1.Write(chunkData, 0, tempLength);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.func_48487_a(this);
		}

		public override int PacketSize
		{
			get
			{
				return 17 + tempLength;
			}
		}
	}

}