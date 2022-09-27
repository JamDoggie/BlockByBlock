using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class NBTTagString : NBTBase
	{
		public string data;

		public NBTTagString(string string1) : base(string1)
		{
		}

		public NBTTagString(string string1, string string2) : base(string1)
		{
			this.data = string2;
			if (string.ReferenceEquals(string2, null))
			{
				throw new System.ArgumentException("Empty string not allowed");
			}
		}
        
		internal override void write(BinaryWriter dataOutput1)
		{
			dataOutput1.WriteUTF(data);
		}
        
		internal override void load(BinaryReader dataInput1)
		{
			this.data = dataInput1.ReadUTF();
		}

		public override sbyte Id
		{
			get
			{
				return (sbyte)8;
			}
		}

		public override string ToString()
		{
			return "" + this.data;
		}

		public override NBTBase copy()
		{
			return new NBTTagString(this.Name, this.data);
		}

		public override bool Equals(object object1)
		{
			if (!base.Equals(object1))
			{
				return false;
			}
			else
			{
				NBTTagString nBTTagString2 = (NBTTagString)object1;
				return string.ReferenceEquals(this.data, null) && string.ReferenceEquals(nBTTagString2.data, null) || !string.ReferenceEquals(this.data, null) && this.data.Equals(nBTTagString2.data);
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.data.GetHashCode();
		}
	}

}