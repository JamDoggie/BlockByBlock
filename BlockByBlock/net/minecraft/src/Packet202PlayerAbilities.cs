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
			this.field_50072_a = playerCapabilities1.disableDamage;
			this.field_50070_b = playerCapabilities1.isFlying;
			this.field_50071_c = playerCapabilities1.allowFlying;
			this.field_50069_d = playerCapabilities1.isCreativeMode;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.field_50072_a = dataInputStream1.readBoolean();
			this.field_50070_b = dataInputStream1.readBoolean();
			this.field_50071_c = dataInputStream1.readBoolean();
			this.field_50069_d = dataInputStream1.readBoolean();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeBoolean(this.field_50072_a);
			dataOutputStream1.writeBoolean(this.field_50070_b);
			dataOutputStream1.writeBoolean(this.field_50071_c);
			dataOutputStream1.writeBoolean(this.field_50069_d);
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