namespace net.minecraft.src
{
	public class ItemPickaxe : ItemTool
	{
		private static Block[] blocksEffectiveAgainst = new Block[]{Block.cobblestone, Block.stairDouble, Block.stairSingle, Block.stone, Block.sandStone, Block.cobblestoneMossy, Block.oreIron, Block.blockSteel, Block.oreCoal, Block.blockGold, Block.oreGold, Block.oreDiamond, Block.blockDiamond, Block.ice, Block.netherrack, Block.oreLapis, Block.blockLapis, Block.oreRedstone, Block.oreRedstoneGlowing, Block.rail, Block.railDetector, Block.railPowered};

		protected internal ItemPickaxe(int i1, EnumToolMaterial enumToolMaterial2) : base(i1, 2, enumToolMaterial2, blocksEffectiveAgainst)
		{
		}

		public override bool canHarvestBlock(Block block1)
		{
			return block1 == Block.obsidian ? this.toolMaterial.HarvestLevel == 3 : (block1 != Block.blockDiamond && block1 != Block.oreDiamond ? (block1 != Block.blockGold && block1 != Block.oreGold ? (block1 != Block.blockSteel && block1 != Block.oreIron ? (block1 != Block.blockLapis && block1 != Block.oreLapis ? (block1 != Block.oreRedstone && block1 != Block.oreRedstoneGlowing ? (block1.blockMaterial == Material.rock ? true : block1.blockMaterial == Material.iron) : this.toolMaterial.HarvestLevel >= 2) : this.toolMaterial.HarvestLevel >= 1) : this.toolMaterial.HarvestLevel >= 1) : this.toolMaterial.HarvestLevel >= 2) : this.toolMaterial.HarvestLevel >= 2);
		}

		public override float getStrVsBlock(ItemStack itemStack1, Block block2)
		{
			return block2 == null || block2.blockMaterial != Material.iron && block2.blockMaterial != Material.rock ? base.getStrVsBlock(itemStack1, block2) : this.efficiencyOnProperMaterial;
		}
	}

}