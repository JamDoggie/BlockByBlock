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

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void readPacketData(java.io.DataInputStream dataInputStream1) throws java.io.IOException
		public override void readPacketData(DataInputStream dataInputStream1)
		{
			this.explosionX = dataInputStream1.readDouble();
			this.explosionY = dataInputStream1.readDouble();
			this.explosionZ = dataInputStream1.readDouble();
			this.explosionSize = dataInputStream1.readFloat();
			int i2 = dataInputStream1.readInt();
			this.destroyedBlockPositions = new HashSet<object>();
			int i3 = (int)this.explosionX;
			int i4 = (int)this.explosionY;
			int i5 = (int)this.explosionZ;

			for (int i6 = 0; i6 < i2; ++i6)
			{
				int i7 = dataInputStream1.readByte() + i3;
				int i8 = dataInputStream1.readByte() + i4;
				int i9 = dataInputStream1.readByte() + i5;
				this.destroyedBlockPositions.Add(new ChunkPosition(i7, i8, i9));
			}

		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writePacketData(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public override void writePacketData(DataOutputStream dataOutputStream1)
		{
			dataOutputStream1.writeDouble(this.explosionX);
			dataOutputStream1.writeDouble(this.explosionY);
			dataOutputStream1.writeDouble(this.explosionZ);
			dataOutputStream1.writeFloat(this.explosionSize);
			dataOutputStream1.writeInt(this.destroyedBlockPositions.Count);
			int i2 = (int)this.explosionX;
			int i3 = (int)this.explosionY;
			int i4 = (int)this.explosionZ;
			System.Collections.IEnumerator iterator5 = this.destroyedBlockPositions.GetEnumerator();

			while (iterator5.MoveNext())
			{
				ChunkPosition chunkPosition6 = (ChunkPosition)iterator5.Current;
				int i7 = chunkPosition6.x - i2;
				int i8 = chunkPosition6.y - i3;
				int i9 = chunkPosition6.z - i4;
				dataOutputStream1.writeByte(i7);
				dataOutputStream1.writeByte(i8);
				dataOutputStream1.writeByte(i9);
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
				return 32 + this.destroyedBlockPositions.Count * 3;
			}
		}
	}

}