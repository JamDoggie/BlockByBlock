using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet61DoorChange : Packet
	{
		public int sfxID;
		public int auxData;
		public int posX;
		public int posY;
		public int posZ;

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			sfxID = dataInputStream1.ReadInt32BigEndian();
			posX = dataInputStream1.ReadInt32BigEndian();
			posY = dataInputStream1.ReadSByte() & 255;
			posZ = dataInputStream1.ReadInt32BigEndian();
			auxData = dataInputStream1.ReadInt32BigEndian();
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(sfxID);
			dataOutputStream1.WriteBigEndian(posX);
			dataOutputStream1.Write((byte)(posY & 255));
			dataOutputStream1.WriteBigEndian(posZ);
			dataOutputStream1.WriteBigEndian(auxData);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleDoorChange(this);
		}

		public override int PacketSize
		{
			get
			{
				return 20;
			}
		}
	}

}