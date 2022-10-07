using BlockByBlock.java_extensions;
using System.Buffers.Binary;

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

		internal override void write(BinaryWriter dataOutput1)
		{
			dataOutput1.WriteBigEndian(data);
		}
        
		internal override void load(BinaryReader dataInput1)
		{
			this.data = dataInput1.ReadDoubleBigEndian();
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