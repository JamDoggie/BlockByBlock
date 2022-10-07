using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet130UpdateSign : Packet
	{
		public int xPosition;
		public int yPosition;
		public int zPosition;
		public string[] signLines;

		public Packet130UpdateSign()
		{
			this.isChunkDataPacket = true;
		}

		public Packet130UpdateSign(int i1, int i2, int i3, string[] string4)
		{
			isChunkDataPacket = true;
			xPosition = i1;
			yPosition = i2;
			zPosition = i3;
			signLines = string4;
		}
        
		public override void readPacketData(BinaryReader dataInputStream1)
		{
			xPosition = dataInputStream1.ReadInt32BigEndian();
			yPosition = dataInputStream1.ReadInt16BigEndian();
			zPosition = dataInputStream1.ReadInt32BigEndian();
			signLines = new string[4];

			for (int i2 = 0; i2 < 4; ++i2)
			{
				signLines[i2] = readString(dataInputStream1, 15);
			}

		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(xPosition);
			dataOutputStream1.WriteBigEndian((short)yPosition);
			dataOutputStream1.WriteBigEndian(zPosition);

			for (int i2 = 0; i2 < 4; ++i2)
			{
				writeString(signLines[i2], dataOutputStream1);
			}

		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleUpdateSign(this);
		}

		public override int PacketSize
		{
			get
			{
				int i1 = 0;
    
				for (int i2 = 0; i2 < 4; ++i2)
				{
					i1 += this.signLines[i2].Length;
				}
    
				return i1;
			}
		}
	}

}