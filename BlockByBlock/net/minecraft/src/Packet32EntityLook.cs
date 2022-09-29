namespace net.minecraft.src
{

	public class Packet32EntityLook : Packet30Entity
	{
		public Packet32EntityLook()
		{
			rotating = true;
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			base.readPacketData(dataInputStream1);
			yaw = dataInputStream1.ReadSByte();
			pitch = dataInputStream1.ReadSByte();
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			base.writePacketData(dataOutputStream1);
			dataOutputStream1.Write(yaw);
			dataOutputStream1.Write(pitch);
		}

		public override int PacketSize
		{
			get
			{
				return 6;
			}
		}
	}

}