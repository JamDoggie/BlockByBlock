namespace net.minecraft.src
{
	public class ItemAxe : ItemTool
	{
		private static Block[] blocksEffectiveAgainst = new Block[]{Block.planks, Block.bookShelf, Block.wood, Block.chest, Block.stairDouble, Block.stairSingle, Block.pumpkin, Block.pumpkinLantern};

		protected internal ItemAxe(int i1, EnumToolMaterial enumToolMaterial2) : base(i1, 3, enumToolMaterial2, blocksEffectiveAgainst)
		{
		}

		public override float getStrVsBlock(ItemStack itemStack1, Block block2)
		{
			return block2 != null && block2.blockMaterial == Material.wood ? this.efficiencyOnProperMaterial : base.getStrVsBlock(itemStack1, block2);
		}
	}

}