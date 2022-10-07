using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet106Transaction : Packet
	{
		public int windowId;
		public short shortWindowId;
		public bool accepted;

		public Packet106Transaction()
		{
		}

		public Packet106Transaction(int i1, short s2, bool z3)
		{
			windowId = i1;
			shortWindowId = s2;
			accepted = z3;
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleTransaction(this);
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			windowId = dataInputStream1.ReadSByte();
			shortWindowId = dataInputStream1.ReadInt16BigEndian();
			accepted = dataInputStream1.ReadByte() != 0;
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write((sbyte)windowId);
			dataOutputStream1.WriteBigEndian(shortWindowId);
			dataOutputStream1.Write(accepted ? (byte)1 : (byte)0);
		}

		public override int PacketSize
		{
			get
			{
				return 4;
			}
		}
	}

}