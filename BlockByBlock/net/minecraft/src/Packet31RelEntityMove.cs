namespace net.minecraft.src
{

	public class Packet31RelEntityMove : Packet30Entity
	{
		public override void readPacketData(BinaryReader dataInputStream1)
		{
			base.readPacketData(dataInputStream1);
			xPosition = dataInputStream1.ReadSByte();
			yPosition = dataInputStream1.ReadSByte();
			zPosition = dataInputStream1.ReadSByte();
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			base.writePacketData(dataOutputStream1);
			dataOutputStream1.Write(xPosition);
			dataOutputStream1.Write(yPosition);
			dataOutputStream1.Write(zPosition);
		}

		public override int PacketSize
		{
			get
			{
				return 7;
			}
		}
	}

}