namespace net.minecraft.src
{

	public class Packet33RelEntityMoveLook : Packet30Entity
	{
		public Packet33RelEntityMoveLook()
		{
			rotating = true;
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			base.readPacketData(dataInputStream1);
			xPosition = dataInputStream1.ReadSByte();
			yPosition = dataInputStream1.ReadSByte();
			zPosition = dataInputStream1.ReadSByte();
			yaw = dataInputStream1.ReadSByte();
			pitch = dataInputStream1.ReadSByte();
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			base.writePacketData(dataOutputStream1);
			dataOutputStream1.Write(xPosition);
			dataOutputStream1.Write(yPosition);
			dataOutputStream1.Write(zPosition);
			dataOutputStream1.Write(yaw);
			dataOutputStream1.Write(pitch);
		}

		public override int PacketSize
		{
			get
			{
				return 9;
			}
		}
	}

}