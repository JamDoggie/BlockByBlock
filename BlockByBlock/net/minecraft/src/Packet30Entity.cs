using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet30Entity : Packet
	{
		public int entityId;
		public sbyte xPosition;
		public sbyte yPosition;
		public sbyte zPosition;
		public sbyte yaw;
		public sbyte pitch;
		public bool rotating = false;
        
		public override void readPacketData(BinaryReader dataInputStream1)
		{
			entityId = dataInputStream1.ReadInt32BigEndian();
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(entityId);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleEntity(this);
		}

		public override int PacketSize
		{
			get
			{
				return 4;
			}
		}
	}

}