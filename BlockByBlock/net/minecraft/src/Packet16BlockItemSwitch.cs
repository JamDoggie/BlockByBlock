using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet16BlockItemSwitch : Packet
	{
		public int id;

		public Packet16BlockItemSwitch()
		{
		}

		public Packet16BlockItemSwitch(int i1)
		{
			id = i1;
		}
        
		public override void readPacketData(BinaryReader dataInputStream1)
		{
			id = dataInputStream1.ReadInt16BigEndian();
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian((short)id);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleBlockItemSwitch(this);
		}

		public override int PacketSize
		{
			get
			{
				return 2;
			}
		}
	}

}