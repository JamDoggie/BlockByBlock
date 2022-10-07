using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet41EntityEffect : Packet
	{
		public int entityId;
		public sbyte effectId;
		public sbyte effectAmp;
		public short duration;

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			entityId = dataInputStream1.ReadInt32BigEndian();
			effectId = dataInputStream1.ReadSByte();
			effectAmp = dataInputStream1.ReadSByte();
			duration = dataInputStream1.ReadInt16BigEndian();
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(entityId);
			dataOutputStream1.Write(effectId);
			dataOutputStream1.Write(effectAmp);
			dataOutputStream1.WriteBigEndian(duration);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleEntityEffect(this);
		}

		public override int PacketSize
		{
			get
			{
				return 8;
			}
		}
	}

}