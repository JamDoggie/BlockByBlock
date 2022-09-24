namespace net.minecraft.src
{
	public class RecipesCrafting
	{
		public virtual void addRecipes(CraftingManager craftingManager1)
		{
			craftingManager1.addRecipe(new ItemStack(Block.chest), new object[]{"###", "# #", "###", '#', Block.planks});
			craftingManager1.addRecipe(new ItemStack(Block.stoneOvenIdle), new object[]{"###", "# #", "###", '#', Block.cobblestone});
			craftingManager1.addRecipe(new ItemStack(Block.workbench), new object[]{"##", "##", '#', Block.planks});
			craftingManager1.addRecipe(new ItemStack(Block.sandStone), new object[]{"##", "##", '#', Block.sand});
			craftingManager1.addRecipe(new ItemStack(Block.sandStone, 4, 2), new object[]{"##", "##", '#', Block.sandStone});
			craftingManager1.addRecipe(new ItemStack(Block.sandStone, 1, 1), new object[]{"#", "#", '#', new ItemStack(Block.stairSingle, 1, 1)});
			craftingManager1.addRecipe(new ItemStack(Block.stoneBrick, 4), new object[]{"##", "##", '#', Block.stone});
			craftingManager1.addRecipe(new ItemStack(Block.fenceIron, 16), new object[]{"###", "###", '#', Item.ingotIron});
			craftingManager1.addRecipe(new ItemStack(Block.thinGlass, 16), new object[]{"###", "###", '#', Block.glass});
			craftingManager1.addRecipe(new ItemStack(Block.redstoneLampIdle, 1), new object[]{" R ", "RGR", " R ", 'R', Item.redstone, 'G', Block.glowStone});
		}
	}

}