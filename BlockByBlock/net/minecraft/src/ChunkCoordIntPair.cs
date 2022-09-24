namespace net.minecraft.src
{
	public class ChunkCoordIntPair
	{
		public readonly int chunkXPos;
		public readonly int chunkZPos;

		public ChunkCoordIntPair(int i1, int i2)
		{
			this.chunkXPos = i1;
			this.chunkZPos = i2;
		}

		public static long chunkXZ2Int(int i0, int i1)
		{
			long j2 = (long)i0;
			long j4 = (long)i1;
			return j2 & 4294967295L | (j4 & 4294967295L) << 32;
		}

		public override int GetHashCode()
		{
			long j1 = chunkXZ2Int(this.chunkXPos, this.chunkZPos);
			int i3 = (int)j1;
			int i4 = (int)(j1 >> 32);
			return i3 ^ i4;
		}

		public override bool Equals(object object1)
		{
			ChunkCoordIntPair chunkCoordIntPair2 = (ChunkCoordIntPair)object1;
			return chunkCoordIntPair2.chunkXPos == this.chunkXPos && chunkCoordIntPair2.chunkZPos == this.chunkZPos;
		}

		public virtual int CenterXPos
		{
			get
			{
				return (this.chunkXPos << 4) + 8;
			}
		}

		public virtual int CenterZPos
		{
			get
			{
				return (this.chunkZPos << 4) + 8;
			}
		}

		public virtual ChunkPosition getChunkPosition(int i1)
		{
			return new ChunkPosition(this.CenterXPos, i1, this.CenterZPos);
		}

		public override string ToString()
		{
			return "[" + this.chunkXPos + ", " + this.chunkZPos + "]";
		}
	}

}