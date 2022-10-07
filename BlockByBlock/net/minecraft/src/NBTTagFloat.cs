using BlockByBlock.helpers;
using BlockByBlock.java_extensions;

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
			data = f2;
		}
        
		internal override void write(BinaryWriter dataOutput1)
		{
			dataOutput1.WriteBigEndian(data);
		}
        
		internal override void load(BinaryReader dataInput1)
		{
			data = dataInput1.ReadSingleBigEndian();
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
            return base.GetHashCode() ^ JTypes.FloatToRawIntBits(data);
		}
	}
}