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
			sfxID = dataInputStream1.ReadInt32();
			posX = dataInputStream1.ReadInt32();
			posY = dataInputStream1.ReadSByte() & 255;
			posZ = dataInputStream1.ReadInt32();
			auxData = dataInputStream1.ReadInt32();
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write(sfxID);
			dataOutputStream1.Write(posX);
			dataOutputStream1.Write((byte)(posY & 255));
			dataOutputStream1.Write(posZ);
			dataOutputStream1.Write(auxData);
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