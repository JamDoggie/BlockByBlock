using BlockByBlock.java_extensions;
using ICSharpCode.SharpZipLib.Zip.Compression;

namespace net.minecraft.src
{

	public class Packet51MapChunk : Packet
	{
		public int xCh;
		public int zCh;
		public int yChMin;
		public int yChMax;
		public byte[] chunkData { get; set; }
		public bool includeInitialize;
		private int tempLength;
		private int unused;
		private static byte[] temp = new byte[0];

		private Inflater inflater4 = new Inflater();

		public Packet51MapChunk()
		{
			isChunkDataPacket = true;
		}
        
		public override void readPacketData(BinaryReader dataInputStream1)
		{
			xCh = dataInputStream1.ReadInt32BigEndian();
			zCh = dataInputStream1.ReadInt32BigEndian();
			includeInitialize = dataInputStream1.ReadBoolean();
			yChMin = dataInputStream1.ReadUInt16BigEndian();
			yChMax = dataInputStream1.ReadUInt16BigEndian();
			tempLength = dataInputStream1.ReadInt32BigEndian();
			unused = dataInputStream1.ReadInt32BigEndian();
			if (temp.Length < tempLength)
			{
				temp = new byte[tempLength];
			}
			
			int bytesRead = 0;

            while (bytesRead < tempLength)
            {
                bytesRead += dataInputStream1.Read(temp, bytesRead, tempLength - bytesRead);
            }

			if (bytesRead != tempLength)
			{
                Console.WriteLine("Chunk Packet " + xCh + "," + zCh + " truncated: expected " + tempLength + " bytes, read " + bytesRead); 
				
            }

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
			dataOutputStream1.WriteBigEndian(xCh);
			dataOutputStream1.WriteBigEndian(zCh);
			dataOutputStream1.Write(includeInitialize);
			dataOutputStream1.WriteBigEndian(checked((ushort)(yChMin & 65535)));
			dataOutputStream1.WriteBigEndian(checked((ushort)(yChMax & 65535)));
			dataOutputStream1.WriteBigEndian(tempLength);
			dataOutputStream1.WriteBigEndian(unused);
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