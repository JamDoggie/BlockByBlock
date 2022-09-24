using System.Collections;

namespace net.minecraft.src
{

	public class Vec3D
	{
		private static System.Collections.IList vectorList = new ArrayList();
		private static int nextVector = 0;
		public double xCoord;
		public double yCoord;
		public double zCoord;

		public static Vec3D createVectorHelper(double d0, double d2, double d4)
		{
			return new Vec3D(d0, d2, d4);
		}

		public static void clearVectorList()
		{
			vectorList.Clear();
			nextVector = 0;
		}

		public static void initialize()
		{
			nextVector = 0;
		}

		public static Vec3D createVector(double d0, double d2, double d4)
		{
			if (nextVector >= vectorList.Count)
			{
				vectorList.Add(createVectorHelper(0.0D, 0.0D, 0.0D));
			}

			return ((Vec3D)vectorList[nextVector++]).setComponents(d0, d2, d4);
		}

		private Vec3D(double d1, double d3, double d5)
		{
			if (d1 == -0.0D)
			{
				d1 = 0.0D;
			}

			if (d3 == -0.0D)
			{
				d3 = 0.0D;
			}

			if (d5 == -0.0D)
			{
				d5 = 0.0D;
			}

			this.xCoord = d1;
			this.yCoord = d3;
			this.zCoord = d5;
		}

		private Vec3D setComponents(double d1, double d3, double d5)
		{
			this.xCoord = d1;
			this.yCoord = d3;
			this.zCoord = d5;
			return this;
		}

		public virtual Vec3D subtract(Vec3D vec3D1)
		{
			return createVector(vec3D1.xCoord - this.xCoord, vec3D1.yCoord - this.yCoord, vec3D1.zCoord - this.zCoord);
		}

		public virtual Vec3D normalize()
		{
			double d1 = (double)MathHelper.sqrt_double(this.xCoord * this.xCoord + this.yCoord * this.yCoord + this.zCoord * this.zCoord);
			return d1 < 1.0E-4D ? createVector(0.0D, 0.0D, 0.0D) : createVector(this.xCoord / d1, this.yCoord / d1, this.zCoord / d1);
		}

		public virtual double dotProduct(Vec3D vec3D1)
		{
			return this.xCoord * vec3D1.xCoord + this.yCoord * vec3D1.yCoord + this.zCoord * vec3D1.zCoord;
		}

		public virtual Vec3D crossProduct(Vec3D vec3D1)
		{
			return createVector(this.yCoord * vec3D1.zCoord - this.zCoord * vec3D1.yCoord, this.zCoord * vec3D1.xCoord - this.xCoord * vec3D1.zCoord, this.xCoord * vec3D1.yCoord - this.yCoord * vec3D1.xCoord);
		}

		public virtual Vec3D addVector(double d1, double d3, double d5)
		{
			return createVector(this.xCoord + d1, this.yCoord + d3, this.zCoord + d5);
		}

		public virtual double distanceTo(Vec3D vec3D1)
		{
			double d2 = vec3D1.xCoord - this.xCoord;
			double d4 = vec3D1.yCoord - this.yCoord;
			double d6 = vec3D1.zCoord - this.zCoord;
			return (double)MathHelper.sqrt_double(d2 * d2 + d4 * d4 + d6 * d6);
		}

		public virtual double squareDistanceTo(Vec3D vec3D1)
		{
			double d2 = vec3D1.xCoord - this.xCoord;
			double d4 = vec3D1.yCoord - this.yCoord;
			double d6 = vec3D1.zCoord - this.zCoord;
			return d2 * d2 + d4 * d4 + d6 * d6;
		}

		public virtual double squareDistanceTo(double d1, double d3, double d5)
		{
			double d7 = d1 - this.xCoord;
			double d9 = d3 - this.yCoord;
			double d11 = d5 - this.zCoord;
			return d7 * d7 + d9 * d9 + d11 * d11;
		}

		public virtual double lengthVector()
		{
			return (double)MathHelper.sqrt_double(this.xCoord * this.xCoord + this.yCoord * this.yCoord + this.zCoord * this.zCoord);
		}

		public virtual Vec3D getIntermediateWithXValue(Vec3D vec3D1, double d2)
		{
			double d4 = vec3D1.xCoord - this.xCoord;
			double d6 = vec3D1.yCoord - this.yCoord;
			double d8 = vec3D1.zCoord - this.zCoord;
			if (d4 * d4 < 1.0000000116860974E-7D)
			{
				return null;
			}
			else
			{
				double d10 = (d2 - this.xCoord) / d4;
				return d10 >= 0.0D && d10 <= 1.0D ? createVector(this.xCoord + d4 * d10, this.yCoord + d6 * d10, this.zCoord + d8 * d10) : null;
			}
		}

		public virtual Vec3D getIntermediateWithYValue(Vec3D vec3D1, double d2)
		{
			double d4 = vec3D1.xCoord - this.xCoord;
			double d6 = vec3D1.yCoord - this.yCoord;
			double d8 = vec3D1.zCoord - this.zCoord;
			if (d6 * d6 < 1.0000000116860974E-7D)
			{
				return null;
			}
			else
			{
				double d10 = (d2 - this.yCoord) / d6;
				return d10 >= 0.0D && d10 <= 1.0D ? createVector(this.xCoord + d4 * d10, this.yCoord + d6 * d10, this.zCoord + d8 * d10) : null;
			}
		}

		public virtual Vec3D getIntermediateWithZValue(Vec3D vec3D1, double d2)
		{
			double d4 = vec3D1.xCoord - this.xCoord;
			double d6 = vec3D1.yCoord - this.yCoord;
			double d8 = vec3D1.zCoord - this.zCoord;
			if (d8 * d8 < 1.0000000116860974E-7D)
			{
				return null;
			}
			else
			{
				double d10 = (d2 - this.zCoord) / d8;
				return d10 >= 0.0D && d10 <= 1.0D ? createVector(this.xCoord + d4 * d10, this.yCoord + d6 * d10, this.zCoord + d8 * d10) : null;
			}
		}

		public override string ToString()
		{
			return "(" + this.xCoord + ", " + this.yCoord + ", " + this.zCoord + ")";
		}

		public virtual void rotateAroundX(float f1)
		{
			float f2 = MathHelper.cos(f1);
			float f3 = MathHelper.sin(f1);
			double d4 = this.xCoord;
			double d6 = this.yCoord * (double)f2 + this.zCoord * (double)f3;
			double d8 = this.zCoord * (double)f2 - this.yCoord * (double)f3;
			this.xCoord = d4;
			this.yCoord = d6;
			this.zCoord = d8;
		}

		public virtual void rotateAroundY(float f1)
		{
			float f2 = MathHelper.cos(f1);
			float f3 = MathHelper.sin(f1);
			double d4 = this.xCoord * (double)f2 + this.zCoord * (double)f3;
			double d6 = this.yCoord;
			double d8 = this.zCoord * (double)f2 - this.xCoord * (double)f3;
			this.xCoord = d4;
			this.yCoord = d6;
			this.zCoord = d8;
		}
	}

}