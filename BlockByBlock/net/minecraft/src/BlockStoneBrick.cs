namespace net.minecraft.src
{
	public class BlockStoneBrick : Block
	{
		public BlockStoneBrick(int i1) : base(i1, 54, Material.rock)
		{
		}

		public override int getBlockTextureFromSideAndMetadata(int i1, int i2)
		{
			switch (i2)
			{
			case 1:
				return 100;
			case 2:
				return 101;
			case 3:
				return 213;
			default:
				return 54;
			}
		}

		protected internal override int damageDropped(int i1)
		{
			return i1;
		}
	}

}