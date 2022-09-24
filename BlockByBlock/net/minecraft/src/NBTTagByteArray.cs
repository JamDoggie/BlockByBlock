using System;
using System.Linq;

namespace net.minecraft.src
{

	public class NBTTagByteArray : NBTBase
	{
		public sbyte[] byteArray;

		public NBTTagByteArray(string string1) : base(string1)
		{
		}

		public NBTTagByteArray(string string1, sbyte[] b2) : base(string1)
		{
			this.byteArray = b2;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: void write(java.io.DataOutput dataOutput1) throws java.io.IOException
		internal override void write(DataOutput dataOutput1)
		{
			dataOutput1.writeInt(this.byteArray.Length);
			dataOutput1.write(this.byteArray);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: void load(java.io.DataInput dataInput1) throws java.io.IOException
		internal override void load(DataInput dataInput1)
		{
			int i2 = dataInput1.readInt();
			this.byteArray = new sbyte[i2];
			dataInput1.readFully(this.byteArray);
		}

		public override sbyte Id
		{
			get
			{
				return (sbyte)7;
			}
		}

		public override string ToString()
		{
			return "[" + this.byteArray.Length + " bytes]";
		}

		public override NBTBase copy()
		{
			sbyte[] b1 = new sbyte[this.byteArray.Length];
			Array.Copy(this.byteArray, 0, b1, 0, this.byteArray.Length);
			return new NBTTagByteArray(this.Name, b1);
		}

		public override bool Equals(object object1)
		{
			return base.Equals(object1) ? this.byteArray.SequenceEqual(((NBTTagByteArray)object1).byteArray) : false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ Arrays.hashCode(this.byteArray);
		}
	}

}