namespace net.minecraft.src
{

	public class Packet8UpdateHealth : Packet
	{
		public int healthMP;
		public int food;
		public float foodSaturation;
        
		public override void readPacketData(BinaryReader dataInputStream1)
		{
			healthMP = dataInputStream1.ReadInt16();
			food = dataInputStream1.ReadInt16();
			foodSaturation = dataInputStream1.ReadSingle();
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write((short)healthMP);
			dataOutputStream1.Write((short)food);
			dataOutputStream1.Write(foodSaturation);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleUpdateHealth(this);
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