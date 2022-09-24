namespace net.minecraft.src
{
	public abstract class BlockDirectional : Block
	{
		protected internal BlockDirectional(int i1, int i2, Material material3) : base(i1, i2, material3)
		{
		}

		protected internal BlockDirectional(int i1, Material material2) : base(i1, material2)
		{
		}

		public static int getDirection(int i0)
		{
			return i0 & 3;
		}
	}
}