namespace net.minecraft.src
{

	public class NBTTagFloat : NBTBase
	{
		public float data;

		public NBTTagFloat(string string1) : base(string1)
		{
		}

		public NBTTagFloat(string string1, float f2) : base(string1)
		{
			this.data = f2;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: void write(java.io.DataOutput dataOutput1) throws java.io.IOException
		internal override void write(DataOutput dataOutput1)
		{
			dataOutput1.writeFloat(this.data);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: void load(java.io.DataInput dataInput1) throws java.io.IOException
		internal override void load(DataInput dataInput1)
		{
			this.data = dataInput1.readFloat();
		}

		public override sbyte Id
		{
			get
			{
				return (sbyte)5;
			}
		}

		public override string ToString()
		{
			return "" + this.data;
		}

		public override NBTBase copy()
		{
			return new NBTTagFloat(this.Name, this.data);
		}

		public override bool Equals(object object1)
		{
			if (base.Equals(object1))
			{
				NBTTagFloat nBTTagFloat2 = (NBTTagFloat)object1;
				return this.data == nBTTagFloat2.data;
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ Float.floatToIntBits(this.data);
		}
	}

}