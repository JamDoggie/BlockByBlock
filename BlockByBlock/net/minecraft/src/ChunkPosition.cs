namespace net.minecraft.src
{
	public struct ChunkPosition
	{
		public readonly int x;
		public readonly int y;
		public readonly int z;

		public ChunkPosition(int i1, int i2, int i3)
		{
			this.x = i1;
			this.y = i2;
			this.z = i3;
		}

		public ChunkPosition(Vec3D vec3D1) : this(MathHelper.floor_double(vec3D1.xCoord), MathHelper.floor_double(vec3D1.yCoord), MathHelper.floor_double(vec3D1.zCoord))
		{
		}

		public override bool Equals(object? object1)
		{
			if (object1 is not ChunkPosition)
			{
				return false;
			}
			else
			{
				ChunkPosition chunkPosition2 = (ChunkPosition)object1;
				return chunkPosition2.x == x && chunkPosition2.y == y && chunkPosition2.z == z;
			}
		}

		public override int GetHashCode()
		{
			return x * 8976890 + y * 981131 + z;
		}
	}

}