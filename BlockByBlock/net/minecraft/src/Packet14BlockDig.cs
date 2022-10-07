using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet14BlockDig : Packet
	{
		public int xPosition;
		public int yPosition;
		public int zPosition;
		public int face;
		public int status;

		public Packet14BlockDig()
		{
		}

		public Packet14BlockDig(int i1, int i2, int i3, int i4, int i5)
		{
			status = i1;
			xPosition = i2;
			yPosition = i3;
			zPosition = i4;
			face = i5;
		}
        
		public override void readPacketData(BinaryReader dataInputStream1)
		{
			status = dataInputStream1.ReadSByte();
			xPosition = dataInputStream1.ReadInt32BigEndian();
			yPosition = dataInputStream1.ReadSByte();
			zPosition = dataInputStream1.ReadInt32BigEndian();
			face = dataInputStream1.ReadSByte();
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write((sbyte)status);
			dataOutputStream1.WriteBigEndian(xPosition);
			dataOutputStream1.Write((sbyte)yPosition);
			dataOutputStream1.WriteBigEndian(zPosition);
			dataOutputStream1.Write((sbyte)face);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleBlockDig(this);
		}

		public override int PacketSize
		{
			get
			{
				return 11;
			}
		}
	}

}