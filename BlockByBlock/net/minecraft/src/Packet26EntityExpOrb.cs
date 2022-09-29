namespace net.minecraft.src
{

	public class Packet26EntityExpOrb : Packet
	{
		public int entityId;
		public int posX;
		public int posY;
		public int posZ;
		public int xpValue;

		public Packet26EntityExpOrb()
		{
		}

		public Packet26EntityExpOrb(EntityXPOrb entityXPOrb1)
		{
			entityId = entityXPOrb1.entityId;
			posX = MathHelper.floor_double(entityXPOrb1.posX * 32.0D);
			posY = MathHelper.floor_double(entityXPOrb1.posY * 32.0D);
			posZ = MathHelper.floor_double(entityXPOrb1.posZ * 32.0D);
			xpValue = entityXPOrb1.XpValue;
		}
        
		public override void readPacketData(BinaryReader dataInputStream1)
		{
			entityId = dataInputStream1.ReadInt32();
			posX = dataInputStream1.ReadInt32();
			posY = dataInputStream1.ReadInt32();
			posZ = dataInputStream1.ReadInt32();
			xpValue = dataInputStream1.ReadInt16();
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write(entityId);
			dataOutputStream1.Write(posX);
			dataOutputStream1.Write(posY);
			dataOutputStream1.Write(posZ);
			dataOutputStream1.Write((short)xpValue);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleEntityExpOrb(this);
		}

		public override int PacketSize
		{
			get
			{
				return 18;
			}
		}
	}

}