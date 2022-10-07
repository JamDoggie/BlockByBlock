namespace net.minecraft.src
{

	public class NBTTagEnd : NBTBase
	{
		public NBTTagEnd() : base((string)null)
		{
		}
        
		internal override void load(BinaryReader dataInput1)
		{
		}

		internal override void write(BinaryWriter dataOutput1)
		{
		}

		public override sbyte Id
		{
			get
			{
				return 0;
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