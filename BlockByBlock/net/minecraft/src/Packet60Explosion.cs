using BlockByBlock.java_extensions;
using System.Collections.Generic;

namespace net.minecraft.src
{

	public class Packet60Explosion : Packet
	{
		public double explosionX;
		public double explosionY;
		public double explosionZ;
		public float explosionSize;
		public ISet<object> destroyedBlockPositions;

		public override void readPacketData(BinaryReader dataInputStream1)
		{
			explosionX = dataInputStream1.ReadDoubleBigEndian();
			explosionY = dataInputStream1.ReadDoubleBigEndian();
			explosionZ = dataInputStream1.ReadDoubleBigEndian();
			explosionSize = dataInputStream1.ReadSingleBigEndian();
			int i2 = dataInputStream1.ReadInt32BigEndian();
			destroyedBlockPositions = new HashSet<object>();
			int i3 = (int)explosionX;
			int i4 = (int)explosionY;
			int i5 = (int)explosionZ;

			for (int i6 = 0; i6 < i2; ++i6)
			{
				int i7 = dataInputStream1.ReadSByte() + i3;
				int i8 = dataInputStream1.ReadSByte() + i4;
				int i9 = dataInputStream1.ReadSByte() + i5;
				destroyedBlockPositions.Add(new ChunkPosition(i7, i8, i9));
			}

		}

		public override void writePacketData(BinaryWriter dataOutputStream1)
		{
			dataOutputStream1.WriteBigEndian(explosionX);
			dataOutputStream1.WriteBigEndian(explosionY);
			dataOutputStream1.WriteBigEndian(explosionZ);
			dataOutputStream1.WriteBigEndian(explosionSize);
			dataOutputStream1.WriteBigEndian(destroyedBlockPositions.Count);
			int i2 = (int)explosionX;
			int i3 = (int)explosionY;
			int i4 = (int)explosionZ;
			System.Collections.IEnumerator iterator5 = destroyedBlockPositions.GetEnumerator();

			while (iterator5.MoveNext())
			{
				ChunkPosition chunkPosition6 = (ChunkPosition)iterator5.Current;
				int i7 = chunkPosition6.x - i2;
				int i8 = chunkPosition6.y - i3;
				int i9 = chunkPosition6.z - i4;
				dataOutputStream1.Write((sbyte)i7);
				dataOutputStream1.Write((sbyte)i8);
				dataOutputStream1.Write((sbyte)i9);
			}

		}

		public override void processPacket(NetHandler netHandler1)
		{
			netHandler1.handleExplosion(this);
		}

		public override int PacketSize
		{
			get
			{
				return 32 + destroyedBlockPositions.Count * 3;
			}
		}
	}

}