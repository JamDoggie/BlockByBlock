using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet17Sleep : Packet
	{
		public int entityID;
		public int bedX;
		public int bedY;
		public int bedZ;
		public int field_22046_e;

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			entityID = dataInputStream1.ReadInt32BigEndian();
			field_22046_e = dataInputStream1.ReadSByte();
			bedX = dataInputStream1.ReadInt32BigEndian();
			bedY = dataInputStream1.ReadSByte();
			bedZ = dataInputStream1.ReadInt32BigEndian();
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(entityID);
			dataOutputStream1.Write((sbyte)field_22046_e);
			dataOutputStream1.WriteBigEndian(bedX);
			dataOutputStream1.Write((sbyte)bedY);
			dataOutputStream1.WriteBigEndian(bedZ);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleSleep(this);
		}

		public override int PacketSize
		{
			get
			{
				return 14;
			}
		}
	}

}