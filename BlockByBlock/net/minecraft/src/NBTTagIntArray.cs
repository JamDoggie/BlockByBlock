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

		internal override void write(BinaryWriter dataOutput1)
		{
			dataOutput1.Write(this.field_48181_a.Length);

			for (int i2 = 0; i2 < this.field_48181_a.Length; ++i2)
			{
				dataOutput1.Write(this.field_48181_a[i2]);
			}

		}

		internal override void load(BinaryReader dataInput1)
		{
			int i2 = dataInput1.ReadInt32();
			this.field_48181_a = new int[i2];

			for (int i3 = 0; i3 < i2; ++i3)
			{
				this.field_48181_a[i3] = dataInput1.ReadInt32();
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
			return base.GetHashCode() ^ field_48181_a.GetHashCode();
		}
	}

}