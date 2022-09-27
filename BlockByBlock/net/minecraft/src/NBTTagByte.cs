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
        
		internal override void write(BinaryWriter dataOutput1)
		{
			dataOutput1.Write(this.data);
		}
        
		internal override void load(BinaryReader dataInput1)
		{
			this.data = dataInput1.ReadSByte();
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