using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet6SpawnPosition : Packet
	{
		public int xPosition;
		public int yPosition;
		public int zPosition;
        
		public override void readPacketData(BinaryReader dataInputStream1)
		{
			xPosition = dataInputStream1.ReadInt32BigEndian();
			yPosition = dataInputStream1.ReadInt32BigEndian();
			zPosition = dataInputStream1.ReadInt32BigEndian();
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(xPosition);
			dataOutputStream1.WriteBigEndian(yPosition);
			dataOutputStream1.WriteBigEndian(zPosition);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleSpawnPosition(this);
		}

		public override int PacketSize
		{
			get
			{
				return 12;
			}
		}
	}

}