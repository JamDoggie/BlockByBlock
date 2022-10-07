using BlockByBlock.java_extensions;

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

		internal override void write(BinaryWriter dataOutput1)
		{
			dataOutput1.WriteBigEndian(this.data);
		}

		internal override void load(BinaryReader dataInput1)
		{
			this.data = dataInput1.ReadInt32BigEndian();
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