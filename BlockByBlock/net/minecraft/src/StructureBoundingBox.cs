using System;

namespace net.minecraft.src
{
	public class StructureBoundingBox
	{
		public int minX;
		public int minY;
		public int minZ;
		public int maxX;
		public int maxY;
		public int maxZ;

		public StructureBoundingBox()
		{
		}

		public static StructureBoundingBox NewBoundingBox
		{
			get
			{
				return new StructureBoundingBox(int.MaxValue, int.MaxValue, int.MaxValue, int.MinValue, int.MinValue, int.MinValue);
			}
		}

		public static StructureBoundingBox getComponentToAddBoundingBox(int i0, int i1, int i2, int i3, int i4, int i5, int i6, int i7, int i8, int i9)
		{
			switch (i9)
			{
			case 0:
				return new StructureBoundingBox(i0 + i3, i1 + i4, i2 + i5, i0 + i6 - 1 + i3, i1 + i7 - 1 + i4, i2 + i8 - 1 + i5);
			case 1:
				return new StructureBoundingBox(i0 - i8 + 1 + i5, i1 + i4, i2 + i3, i0 + i5, i1 + i7 - 1 + i4, i2 + i6 - 1 + i3);
			case 2:
				return new StructureBoundingBox(i0 + i3, i1 + i4, i2 - i8 + 1 + i5, i0 + i6 - 1 + i3, i1 + i7 - 1 + i4, i2 + i5);
			case 3:
				return new StructureBoundingBox(i0 + i5, i1 + i4, i2 + i3, i0 + i8 - 1 + i5, i1 + i7 - 1 + i4, i2 + i6 - 1 + i3);
			default:
				return new StructureBoundingBox(i0 + i3, i1 + i4, i2 + i5, i0 + i6 - 1 + i3, i1 + i7 - 1 + i4, i2 + i8 - 1 + i5);
			}
		}

		public StructureBoundingBox(StructureBoundingBox structureBoundingBox1)
		{
			this.minX = structureBoundingBox1.minX;
			this.minY = structureBoundingBox1.minY;
			this.minZ = structureBoundingBox1.minZ;
			this.maxX = structureBoundingBox1.maxX;
			this.maxY = structureBoundingBox1.maxY;
			this.maxZ = structureBoundingBox1.maxZ;
		}

		public StructureBoundingBox(int i1, int i2, int i3, int i4, int i5, int i6)
		{
			this.minX = i1;
			this.minY = i2;
			this.minZ = i3;
			this.maxX = i4;
			this.maxY = i5;
			this.maxZ = i6;
		}

		public StructureBoundingBox(int i1, int i2, int i3, int i4)
		{
			this.minX = i1;
			this.minZ = i2;
			this.maxX = i3;
			this.maxZ = i4;
			this.minY = 1;
			this.maxY = 512;
		}

		public virtual bool intersectsWith(StructureBoundingBox structureBoundingBox1)
		{
			return this.maxX >= structureBoundingBox1.minX && this.minX <= structureBoundingBox1.maxX && this.maxZ >= structureBoundingBox1.minZ && this.minZ <= structureBoundingBox1.maxZ && this.maxY >= structureBoundingBox1.minY && this.minY <= structureBoundingBox1.maxY;
		}

		public virtual bool intersectsWith(int i1, int i2, int i3, int i4)
		{
			return this.maxX >= i1 && this.minX <= i3 && this.maxZ >= i2 && this.minZ <= i4;
		}

		public virtual void expandTo(StructureBoundingBox structureBoundingBox1)
		{
			this.minX = Math.Min(this.minX, structureBoundingBox1.minX);
			this.minY = Math.Min(this.minY, structureBoundingBox1.minY);
			this.minZ = Math.Min(this.minZ, structureBoundingBox1.minZ);
			this.maxX = Math.Max(this.maxX, structureBoundingBox1.maxX);
			this.maxY = Math.Max(this.maxY, structureBoundingBox1.maxY);
			this.maxZ = Math.Max(this.maxZ, structureBoundingBox1.maxZ);
		}

		public virtual void offset(int i1, int i2, int i3)
		{
			this.minX += i1;
			this.minY += i2;
			this.minZ += i3;
			this.maxX += i1;
			this.maxY += i2;
			this.maxZ += i3;
		}

		public virtual bool isVecInside(int i1, int i2, int i3)
		{
			return i1 >= this.minX && i1 <= this.maxX && i3 >= this.minZ && i3 <= this.maxZ && i2 >= this.minY && i2 <= this.maxY;
		}

		public virtual int XSize
		{
			get
			{
				return this.maxX - this.minX + 1;
			}
		}

		public virtual int YSize
		{
			get
			{
				return this.maxY - this.minY + 1;
			}
		}

		public virtual int ZSize
		{
			get
			{
				return this.maxZ - this.minZ + 1;
			}
		}

		public virtual int CenterX
		{
			get
			{
				return this.minX + (this.maxX - this.minX + 1) / 2;
			}
		}

		public virtual int CenterY
		{
			get
			{
				return this.minY + (this.maxY - this.minY + 1) / 2;
			}
		}

		public virtual int CenterZ
		{
			get
			{
				return this.minZ + (this.maxZ - this.minZ + 1) / 2;
			}
		}

		public override string ToString()
		{
			return "(" + this.minX + ", " + this.minY + ", " + this.minZ + "; " + this.maxX + ", " + this.maxY + ", " + this.maxZ + ")";
		}
	}

}