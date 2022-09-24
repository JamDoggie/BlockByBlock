namespace net.minecraft.src
{

	public class NBTTagShort : NBTBase
	{
		public short data;

		public NBTTagShort(string string1) : base(string1)
		{
		}

		public NBTTagShort(string string1, short s2) : base(string1)
		{
			this.data = s2;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: void write(java.io.DataOutput dataOutput1) throws java.io.IOException
		internal override void write(DataOutput dataOutput1)
		{
			dataOutput1.writeShort(this.data);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: void load(java.io.DataInput dataInput1) throws java.io.IOException
		internal override void load(DataInput dataInput1)
		{
			this.data = dataInput1.readShort();
		}

		public override sbyte Id
		{
			get
			{
				return (sbyte)2;
			}
		}

		public override string ToString()
		{
			return "" + this.data;
		}

		public override NBTBase copy()
		{
			return new NBTTagShort(this.Name, this.data);
		}

		public override bool Equals(object object1)
		{
			if (base.Equals(object1))
			{
				NBTTagShort nBTTagShort2 = (NBTTagShort)object1;
				return this.data == nBTTagShort2.data;
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