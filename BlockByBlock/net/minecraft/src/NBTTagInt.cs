namespace net.minecraft.src
{

	public class NBTTagInt : NBTBase
	{
		public int data;

		public NBTTagInt(string string1) : base(string1)
		{
		}

		public NBTTagInt(string string1, int i2) : base(string1)
		{
			this.data = i2;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: void write(java.io.DataOutput dataOutput1) throws java.io.IOException
		internal override void write(DataOutput dataOutput1)
		{
			dataOutput1.writeInt(this.data);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: void load(java.io.DataInput dataInput1) throws java.io.IOException
		internal override void load(DataInput dataInput1)
		{
			this.data = dataInput1.readInt();
		}

		public override sbyte Id
		{
			get
			{
				return (sbyte)3;
			}
		}

		public override string ToString()
		{
			return "" + this.data;
		}

		public override NBTBase copy()
		{
			return new NBTTagInt(this.Name, this.data);
		}

		public override bool Equals(object object1)
		{
			if (base.Equals(object1))
			{
				NBTTagInt nBTTagInt2 = (NBTTagInt)object1;
				return this.data == nBTTagInt2.data;
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.data;
		}
	}

}