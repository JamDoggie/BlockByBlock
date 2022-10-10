using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet131MapData : Packet
	{
		public short itemID;
		public short uniqueID;
		public byte[] itemData;

		public Packet131MapData()
		{
			isChunkDataPacket = true;
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			itemID = dataInputStream1.ReadInt16BigEndian();
			uniqueID = dataInputStream1.ReadInt16BigEndian();
			itemData = new byte[dataInputStream1.ReadByte() & 255];
            int bytesRead = dataInputStream1.Read(itemData, 0, itemData.Length);
        }

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(itemID);
			dataOutputStream1.WriteBigEndian(uniqueID);
			dataOutputStream1.Write((byte)itemData.Length);
			dataOutputStream1.Write(itemData);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleMapData(this);
		}

		public override int PacketSize
		{
			get
			{
				return 4 + this.itemData.Length;
			}
		}
	}

}