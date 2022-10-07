using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet38EntityStatus : Packet
	{
		public int entityId;
		public sbyte entityStatus;

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			entityId = dataInputStream1.ReadInt32BigEndian();
			entityStatus = dataInputStream1.ReadSByte();
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(entityId);
			dataOutputStream1.Write(entityStatus);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleEntityStatus(this);
		}

		public override int PacketSize
		{
			get
			{
				return 5;
			}
		}
	}

}