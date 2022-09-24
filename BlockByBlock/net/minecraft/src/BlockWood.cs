namespace net.minecraft.src
{
	public class BlockWood : Block
	{
		public BlockWood(int i1) : base(i1, 4, Material.wood)
		{
		}

		public override int getBlockTextureFromSideAndMetadata(int i1, int i2)
		{
			switch (i2)
			{
			case 1:
				return 198;
			case 2:
				return 214;
			case 3:
				return 199;
			default:
				return 4;
			}
		}

		protected internal override int damageDropped(int i1)
		{
			return i1;
		}
	}

}