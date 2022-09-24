namespace net.minecraft.src
{
	public class PathPoint
	{
		public readonly int xCoord;
		public readonly int yCoord;
		public readonly int zCoord;
		private readonly int hash;
		internal int index = -1;
		internal float totalPathDistance;
		internal float distanceToNext;
		internal float distanceToTarget;
		internal PathPoint previous;
		public bool isFirst = false;

		public PathPoint(int i1, int i2, int i3)
		{
			this.xCoord = i1;
			this.yCoord = i2;
			this.zCoord = i3;
			this.hash = makeHash(i1, i2, i3);
		}

		public static int makeHash(int i0, int i1, int i2)
		{
			return i1 & 255 | (i0 & 32767) << 8 | (i2 & 32767) << 24 | (i0 < 0 ? int.MinValue : 0) | (i2 < 0 ? 32768 : 0);
		}

		public virtual float distanceTo(PathPoint pathPoint1)
		{
			float f2 = (float)(pathPoint1.xCoord - this.xCoord);
			float f3 = (float)(pathPoint1.yCoord - this.yCoord);
			float f4 = (float)(pathPoint1.zCoord - this.zCoord);
			return MathHelper.sqrt_float(f2 * f2 + f3 * f3 + f4 * f4);
		}

		public override bool Equals(object object1)
		{
			if (!(object1 is PathPoint))
			{
				return false;
			}
			else
			{
				PathPoint pathPoint2 = (PathPoint)object1;
				return this.hash == pathPoint2.hash && this.xCoord == pathPoint2.xCoord && this.yCoord == pathPoint2.yCoord && this.zCoord == pathPoint2.zCoord;
			}
		}

		public override int GetHashCode()
		{
			return this.hash;
		}

		public virtual bool Assigned
		{
			get
			{
				return this.index >= 0;
			}
		}

		public override string ToString()
		{
			return this.xCoord + ", " + this.yCoord + ", " + this.zCoord;
		}
	}

}