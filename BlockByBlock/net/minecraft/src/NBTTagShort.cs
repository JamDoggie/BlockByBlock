using BlockByBlock.java_extensions;

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
        
		internal override void write(BinaryWriter dataOutput1)
		{
			dataOutput1.WriteBigEndian(data);
		}

		internal override void load(BinaryReader dataInput1)
		{
			data = dataInput1.ReadInt16BigEndian();
		}

		public override sbyte Id
		{
			get
			{
				return 2;
			}
		}

		public override string ToString()
		{
			return "" + data;
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