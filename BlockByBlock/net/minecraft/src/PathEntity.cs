using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class PathEntity
	{
		private readonly PathPoint[] points;
		private int currentPathIndex;
		private int pathLength;

		public PathEntity(PathPoint[] pathPoint1)
		{
			this.points = pathPoint1;
			this.pathLength = pathPoint1.Length;
		}

		public virtual void incrementPathIndex()
		{
			++this.currentPathIndex;
		}

		public virtual bool Finished
		{
			get
			{
				return this.currentPathIndex >= this.pathLength;
			}
		}

		public virtual PathPoint FinalPathPoint
		{
			get
			{
				return this.pathLength > 0 ? this.points[this.pathLength - 1] : null;
			}
		}

		public virtual PathPoint getPathPointFromIndex(int i1)
		{
			return this.points[i1];
		}

		public virtual int CurrentPathLength
		{
			get
			{
				return this.pathLength;
			}
			set
			{
				this.pathLength = value;
			}
		}


		public virtual int CurrentPathIndex
		{
			get
			{
				return this.currentPathIndex;
			}
			set
			{
				this.currentPathIndex = value;
			}
		}


		public virtual Vec3D getVectorFromIndex(Entity entity1, int i2)
		{
			double d3 = (double)this.points[i2].xCoord + (double)((int)(entity1.width + 1.0F)) * 0.5D;
			double d5 = (double)this.points[i2].yCoord;
			double d7 = (double)this.points[i2].zCoord + (double)((int)(entity1.width + 1.0F)) * 0.5D;
			return Vec3D.createVector(d3, d5, d7);
		}

		public virtual Vec3D getCurrentNodeVec3d(Entity entity1)
		{
			return this.getVectorFromIndex(entity1, this.currentPathIndex);
		}

		public virtual bool func_48647_a(PathEntity pathEntity1)
		{
			if (pathEntity1 == null)
			{
				return false;
			}
			else if (pathEntity1.points.Length != this.points.Length)
			{
				return false;
			}
			else
			{
				for (int i2 = 0; i2 < this.points.Length; ++i2)
				{
					if (this.points[i2].xCoord != pathEntity1.points[i2].xCoord || this.points[i2].yCoord != pathEntity1.points[i2].yCoord || this.points[i2].zCoord != pathEntity1.points[i2].zCoord)
					{
						return false;
					}
				}

				return true;
			}
		}

		public virtual bool func_48639_a(Vec3D vec3D1)
		{
			PathPoint pathPoint2 = this.FinalPathPoint;
			return pathPoint2 == null ? false : pathPoint2.xCoord == (int)vec3D1.xCoord && pathPoint2.zCoord == (int)vec3D1.zCoord;
		}
	}

}