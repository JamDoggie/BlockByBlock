namespace net.minecraft.src
{

	public class NBTTagEnd : NBTBase
	{
		public NBTTagEnd() : base((string)null)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: void load(java.io.DataInput dataInput1) throws java.io.IOException
		internal override void load(DataInput dataInput1)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: void write(java.io.DataOutput dataOutput1) throws java.io.IOException
		internal override void write(DataOutput dataOutput1)
		{
		}

		public override sbyte Id
		{
			get
			{
				return (sbyte)0;
			}
		}

		public override string ToString()
		{
			return "END";
		}

		public override NBTBase copy()
		{
			return new NBTTagEnd();
		}

		public override bool Equals(object object1)
		{
			return base.Equals(object1);
		}
	}

}