namespace net.minecraft.src
{

	public class Packet42RemoveEntityEffect : Packet
	{
		public int entityId;
		public sbyte effectId;
        
		public override void readPacketData(BinaryReader dataInputStream1)
		{
			entityId = dataInputStream1.ReadInt32();
			effectId = dataInputStream1.ReadSByte();
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write(entityId);
			dataOutputStream1.Write(effectId);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleRemoveEntityEffect(this);
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