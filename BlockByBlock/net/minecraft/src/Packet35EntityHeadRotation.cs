namespace net.minecraft.src
{

	public class Packet35EntityHeadRotation : Packet
	{
		public int entityId;
		public sbyte headRotationYaw;
        
		public override void readPacketData(BinaryReader dataInputStream1)
		{
			entityId = dataInputStream1.ReadInt32();
			headRotationYaw = dataInputStream1.ReadSByte();
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write(entityId);
			dataOutputStream1.Write(headRotationYaw);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleEntityHeadRotation(this);
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