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
			xPosition = dataInputStream1.ReadInt32();
			zPosition = dataInputStream1.ReadInt32();
			size = dataInputStream1.ReadInt16() & 65535;
			int i2 = dataInputStream1.ReadInt32();
			if (i2 > 0)
			{
				metadataArray = new byte[i2];
				dataInputStream1.Read(metadataArray);
			}

		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write(xPosition);
			dataOutputStream1.Write(zPosition);
			dataOutputStream1.Write((short)size);
			if (metadataArray != null)
			{
				dataOutputStream1.Write(metadataArray.Length);
				dataOutputStream1.Write(metadataArray);
			}
			else
			{
				dataOutputStream1.Write(0);
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