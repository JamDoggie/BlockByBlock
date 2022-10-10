using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet52MultiBlockChange : Packet
	{
		public int xPosition;
		public int zPosition;
		public byte[] metadataArray;
		public int size;
		private static sbyte[] field_48168_e = new sbyte[0];

		public Packet52MultiBlockChange()
		{
			isChunkDataPacket = true;
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			xPosition = dataInputStream1.ReadInt32BigEndian();
			zPosition = dataInputStream1.ReadInt32BigEndian();
			size = dataInputStream1.ReadInt16BigEndian() & 65535;
			int i2 = dataInputStream1.ReadInt32BigEndian();
			if (i2 > 0)
			{
				metadataArray = new byte[i2];
				dataInputStream1.Read(metadataArray);
			}
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(xPosition);
			dataOutputStream1.WriteBigEndian(zPosition);
			dataOutputStream1.WriteBigEndian((short)size);
			if (metadataArray != null)
			{
				dataOutputStream1.WriteBigEndian(metadataArray.Length);
				dataOutputStream1.Write(metadataArray);
			}
			else
			{
				dataOutputStream1.WriteBigEndian(0);
			}

		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleMultiBlockChange(this);
		}

		public override int PacketSize
		{
			get
			{
				return 10 + size * 4;
			}
		}
	}

}