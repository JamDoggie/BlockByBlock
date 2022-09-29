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
			itemID = dataInputStream1.ReadInt16();
			uniqueID = dataInputStream1.ReadInt16();
			itemData = new byte[dataInputStream1.ReadByte() & 255];
            dataInputStream1.Read(itemData, 0, itemData.Length);
        }

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.Write(itemID);
			dataOutputStream1.Write(uniqueID);
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