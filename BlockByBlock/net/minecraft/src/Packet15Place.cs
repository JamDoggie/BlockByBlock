using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class Packet15Place : Packet
	{
		public int xPosition;
		public int yPosition;
		public int zPosition;
		public int direction;
		public ItemStack itemStack;

		public Packet15Place()
		{
		}

		public Packet15Place(int i1, int i2, int i3, int i4, ItemStack itemStack5)
		{
			xPosition = i1;
			yPosition = i2;
			zPosition = i3;
			direction = i4;
			itemStack = itemStack5;
		}

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			xPosition = dataInputStream1.ReadInt32BigEndian();
			yPosition = dataInputStream1.ReadByte();
			zPosition = dataInputStream1.ReadInt32BigEndian();
			direction = dataInputStream1.ReadSByte();
			itemStack = ReadItemStack(dataInputStream1);
		}
        
		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(xPosition);
			dataOutputStream1.Write((byte)yPosition);
			dataOutputStream1.WriteBigEndian(zPosition);
			dataOutputStream1.Write((sbyte)direction);
			writeItemStack(itemStack, dataOutputStream1);
		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handlePlace(this);
		}

		public override int PacketSize
		{
			get
			{
				return 15;
			}
		}
	}

}