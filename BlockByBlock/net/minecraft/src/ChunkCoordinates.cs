using System;

namespace net.minecraft.src
{
	public class ChunkCoordinates : IComparable
	{
		public int posX;
		public int posY;
		public int posZ;

		public ChunkCoordinates()
		{
		}

		public ChunkCoordinates(int i1, int i2, int i3)
		{
			this.posX = i1;
			this.posY = i2;
			this.posZ = i3;
		}

		public ChunkCoordinates(ChunkCoordinates chunkCoordinates1)
		{
			this.posX = chunkCoordinates1.posX;
			this.posY = chunkCoordinates1.posY;
			this.posZ = chunkCoordinates1.posZ;
		}

		public override bool Equals(object object1)
		{
			if (!(object1 is ChunkCoordinates))
			{
				return false;
			}
			else
			{
				ChunkCoordinates chunkCoordinates2 = (ChunkCoordinates)object1;
				return this.posX == chunkCoordinates2.posX && this.posY == chunkCoordinates2.posY && this.posZ == chunkCoordinates2.posZ;
			}
		}

		public override int GetHashCode()
		{
			return this.posX + this.posZ << 8 + this.posY << 16;
		}

		public virtual int compareChunkCoordinate(ChunkCoordinates chunkCoordinates1)
		{
			return this.posY == chunkCoordinates1.posY ? (this.posZ == chunkCoordinates1.posZ ? this.posX - chunkCoordinates1.posX : this.posZ - chunkCoordinates1.posZ) : this.posY - chunkCoordinates1.posY;
		}

		public virtual void set(int i1, int i2, int i3)
		{
			this.posX = i1;
			this.posY = i2;
			this.posZ = i3;
		}

		public virtual double getEuclideanDistanceTo(int i1, int i2, int i3)
		{
			int i4 = this.posX - i1;
			int i5 = this.posY - i2;
			int i6 = this.posZ - i3;
			return Math.Sqrt((double)(i4 * i4 + i5 * i5 + i6 * i6));
		}

		public virtual float getDistanceSquared(int i1, int i2, int i3)
		{
			int i4 = this.posX - i1;
			int i5 = this.posY - i2;
			int i6 = this.posZ - i3;
			return (float)(i4 * i4 + i5 * i5 + i6 * i6);
		}

		public virtual int CompareTo(object object1)
		{
			return this.compareChunkCoordinate((ChunkCoordinates)object1);
		}
	}

}