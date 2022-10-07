using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet105UpdateProgressbar : Packet
	{
		public int windowId;
		public int progressBar;
		public int progressBarValue;

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleUpdateProgressbar(this);
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			windowId = dataInputStream1.ReadSByte();
			progressBar = dataInputStream1.ReadInt16BigEndian();
			progressBarValue = dataInputStream1.ReadInt16BigEndian();
		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write((sbyte)windowId);
			dataOutputStream1.WriteBigEndian((short)progressBar);
			dataOutputStream1.WriteBigEndian((short)progressBarValue);
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