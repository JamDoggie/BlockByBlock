using System;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class NBTTagIntArray : NBTBase
	{
		public int[] data;

		public NBTTagIntArray(string string1) : base(string1)
		{
		}

		public NBTTagIntArray(string string1, int[] i2) : base(string1)
		{
			this.data = i2;
		}

		internal override void write(BinaryWriter dataOutput1)
		{
			dataOutput1.WriteBigEndian(this.data.Length);

			for (int i2 = 0; i2 < this.data.Length; ++i2)
			{
				dataOutput1.WriteBigEndian(this.data[i2]);
			}

		}

		internal override void load(BinaryReader dataInput1)
		{
			int i2 = dataInput1.ReadInt32BigEndian();
			this.data = new int[i2];

			for (int i3 = 0; i3 < i2; ++i3)
			{
				this.data[i3] = dataInput1.ReadInt32BigEndian();
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
			return "[" + this.data.Length + " bytes]";
		}

		public override NBTBase copy()
		{
			int[] i1 = new int[this.data.Length];
			Array.Copy(this.data, 0, i1, 0, this.data.Length);
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
				return this.data == null && nBTTagIntArray2.data == null || this.data != null && this.data.Equals(nBTTagIntArray2.data);
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ data.GetHashCode();
		}
	}

}