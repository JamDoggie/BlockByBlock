namespace net.minecraft.src
{

	public class NBTTagByte : NBTBase
	{
		public sbyte data;

		public NBTTagByte(string string1) : base(string1)
		{
		}

		public NBTTagByte(string string1, sbyte b2) : base(string1)
		{
			this.data = b2;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: void write(java.io.DataOutput dataOutput1) throws java.io.IOException
		internal override void write(DataOutput dataOutput1)
		{
			dataOutput1.writeByte(this.data);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: void load(java.io.DataInput dataInput1) throws java.io.IOException
		internal override void load(DataInput dataInput1)
		{
			this.data = dataInput1.readByte();
		}

		public override sbyte Id
		{
			get
			{
				return (sbyte)1;
			}
		}

		public override string ToString()
		{
			return "" + this.data;
		}

		public override NBTBase copy()
		{
			return new NBTTagByte(this.Name, this.data);
		}

		public override bool Equals(object object1)
		{
			if (base.Equals(object1))
			{
				NBTTagByte nBTTagByte2 = (NBTTagByte)object1;
				return this.data == nBTTagByte2.data;
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