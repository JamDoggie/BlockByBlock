namespace net.minecraft.src
{
	public class ItemLeaves : ItemBlock
	{
		public ItemLeaves(int i1) : base(i1)
		{
			setMaxDamage(0);
			setHasSubtypes(true);
		}

		public override int getMetadata(int i1)
		{
			return i1 | 4;
		}

		public override int getIconFromDamage(int i1)
		{
			return Block.leaves.getBlockTextureFromSideAndMetadata(0, i1);
		}

		public override int getColorFromDamage(int i1, int i2)
		{
			return (i1 & 1) == 1 ? ColorizerFoliage.FoliageColorPine : ((i1 & 2) == 2 ? ColorizerFoliage.FoliageColorBirch : ColorizerFoliage.FoliageColorBasic);
		}
	}

}