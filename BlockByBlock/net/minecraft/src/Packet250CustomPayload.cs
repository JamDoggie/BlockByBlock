using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet250CustomPayload : Packet
	{
		public string channel;
		public int length;
		public byte[] data;

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			channel = readString(dataInputStream1, 16);
			length = dataInputStream1.ReadInt16BigEndian();
			if (length > 0 && length < 32767)
			{
				data = new byte[length];
				dataInputStream1.Read(data);
			}

		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			writeString(channel, dataOutputStream1);
			dataOutputStream1.WriteBigEndian((short)length);
			if (data != null)
			{
				dataOutputStream1.Write(data);
			}

		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleCustomPayload(this);
		}

		public override int PacketSize
		{
			get
			{
				return 2 + channel.Length * 2 + 2 + length;
			}
		}
	}

}