namespace net.minecraft.src
{

	public class Packet202PlayerAbilities : Packet
	{
		public bool field_50072_a = false;
		public bool field_50070_b = false;
		public bool field_50071_c = false;
		public bool field_50069_d = false;

		public Packet202PlayerAbilities()
		{
		}

		public Packet202PlayerAbilities(PlayerCapabilities playerCapabilities1)
		{
			field_50072_a = playerCapabilities1.disableDamage;
			field_50070_b = playerCapabilities1.isFlying;
			field_50071_c = playerCapabilities1.allowFlying;
			field_50069_d = playerCapabilities1.isCreativeMode;
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			field_50072_a = dataInputStream1.ReadBoolean();
			field_50070_b = dataInputStream1.ReadBoolean();
			field_50071_c = dataInputStream1.ReadBoolean();
			field_50069_d = dataInputStream1.ReadBoolean();
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write(field_50072_a);
			dataOutputStream1.Write(field_50070_b);
			dataOutputStream1.Write(field_50071_c);
			dataOutputStream1.Write(field_50069_d);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.func_50100_a(this);
		}

		public override int PacketSize
		{
			get
			{
				return 1;
			}
		}
	}

}