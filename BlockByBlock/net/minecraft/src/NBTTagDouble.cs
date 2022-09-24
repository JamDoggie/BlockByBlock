namespace net.minecraft.src
{

	public class NBTTagDouble : NBTBase
	{
		public double data;

		public NBTTagDouble(string string1) : base(string1)
		{
		}

		public NBTTagDouble(string string1, double d2) : base(string1)
		{
			this.data = d2;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: void write(java.io.DataOutput dataOutput1) throws java.io.IOException
		internal override void write(DataOutput dataOutput1)
		{
			dataOutput1.writeDouble(this.data);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: void load(java.io.DataInput dataInput1) throws java.io.IOException
		internal override void load(DataInput dataInput1)
		{
			this.data = dataInput1.readDouble();
		}

		public override sbyte Id
		{
			get
			{
				return (sbyte)6;
			}
		}

		public override string ToString()
		{
			return "" + this.data;
		}

		public override NBTBase copy()
		{
			return new NBTTagDouble(this.Name, this.data);
		}

		public override bool Equals(object object1)
		{
			if (base.Equals(object1))
			{
				NBTTagDouble nBTTagDouble2 = (NBTTagDouble)object1;
				return this.data == nBTTagDouble2.data;
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			long j1 = System.BitConverter.DoubleToInt64Bits(this.data);
			return base.GetHashCode() ^ (int)(j1 ^ (long)((ulong)j1 >> 32));
		}
	}

}