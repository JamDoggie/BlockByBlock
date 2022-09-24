namespace net.minecraft.src
{

	public class NBTTagLong : NBTBase
	{
		public long data;

		public NBTTagLong(string string1) : base(string1)
		{
		}

		public NBTTagLong(string string1, long j2) : base(string1)
		{
			this.data = j2;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: void write(java.io.DataOutput dataOutput1) throws java.io.IOException
		internal override void write(DataOutput dataOutput1)
		{
			dataOutput1.writeLong(this.data);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: void load(java.io.DataInput dataInput1) throws java.io.IOException
		internal override void load(DataInput dataInput1)
		{
			this.data = dataInput1.readLong();
		}

		public override sbyte Id
		{
			get
			{
				return (sbyte)4;
			}
		}

		public override string ToString()
		{
			return "" + this.data;
		}

		public override NBTBase copy()
		{
			return new NBTTagLong(this.Name, this.data);
		}

		public override bool Equals(object object1)
		{
			if (base.Equals(object1))
			{
				NBTTagLong nBTTagLong2 = (NBTTagLong)object1;
				return this.data == nBTTagLong2.data;
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ (int)(this.data ^ (long)((ulong)this.data >> 32));
		}
	}

}