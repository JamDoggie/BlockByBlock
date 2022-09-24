namespace net.minecraft.src
{
	public class ItemSpade : ItemTool
	{
		private static Block[] blocksEffectiveAgainst = new Block[]{Block.grass, Block.dirt, Block.sand, Block.gravel, Block.snow, Block.blockSnow, Block.blockClay, Block.tilledField, Block.slowSand, Block.mycelium};

		public ItemSpade(int i1, EnumToolMaterial enumToolMaterial2) : base(i1, 1, enumToolMaterial2, blocksEffectiveAgainst)
		{
		}

		public override bool canHarvestBlock(Block block1)
		{
			return block1 == Block.snow ? true : block1 == Block.blockSnow;
		}
	}

}