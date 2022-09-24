using System;

namespace net.minecraft.src
{

	public class NBTTagIntArray : NBTBase
	{
		public int[] field_48181_a;

		public NBTTagIntArray(string string1) : base(string1)
		{
		}

		public NBTTagIntArray(string string1, int[] i2) : base(string1)
		{
			this.field_48181_a = i2;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: void write(java.io.DataOutput dataOutput1) throws java.io.IOException
		internal override void write(DataOutput dataOutput1)
		{
			dataOutput1.writeInt(this.field_48181_a.Length);

			for (int i2 = 0; i2 < this.field_48181_a.Length; ++i2)
			{
				dataOutput1.writeInt(this.field_48181_a[i2]);
			}

		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: void load(java.io.DataInput dataInput1) throws java.io.IOException
		internal override void load(DataInput dataInput1)
		{
			int i2 = dataInput1.readInt();
			this.field_48181_a = new int[i2];

			for (int i3 = 0; i3 < i2; ++i3)
			{
				this.field_48181_a[i3] = dataInput1.readInt();
			}

		}

		public override sbyte Id
		{
			get
			{
				return (sbyte)11;
			}
		}

		public override string ToString()
		{
			return "[" + this.field_48181_a.Length + " bytes]";
		}

		public override NBTBase copy()
		{
			int[] i1 = new int[this.field_48181_a.Length];
			Array.Copy(this.field_48181_a, 0, i1, 0, this.field_48181_a.Length);
			return new NBTTagIntArray(this.Name, i1);
		}

		public override bool Equals(object object1)
		{
			if (!base.Equals(object1))
			{
				return false;
			}
			else
			{
				NBTTagIntArray nBTTagIntArray2 = (NBTTagIntArray)object1;
				return this.field_48181_a == null && nBTTagIntArray2.field_48181_a == null || this.field_48181_a != null && this.field_48181_a.Equals(nBTTagIntArray2.field_48181_a);
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ Arrays.hashCode(this.field_48181_a);
		}
	}

}