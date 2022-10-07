using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet53BlockChange : Packet
	{
		public int xPosition;
		public int yPosition;
		public int zPosition;
		public int type;
		public int metadata;

		public Packet53BlockChange()
		{
			isChunkDataPacket = true;
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			xPosition = dataInputStream1.ReadInt32BigEndian();
			yPosition = dataInputStream1.ReadSByte();
			zPosition = dataInputStream1.ReadInt32BigEndian();
			type = dataInputStream1.ReadSByte();
			metadata = dataInputStream1.ReadSByte();
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(xPosition);
			dataOutputStream1.Write((sbyte)yPosition);
			dataOutputStream1.WriteBigEndian(zPosition);
			dataOutputStream1.Write((sbyte)type);
			dataOutputStream1.Write((sbyte)metadata);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleBlockChange(this);
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