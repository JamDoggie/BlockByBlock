namespace net.minecraft.src
{
	public class RecipesFood
	{
		public virtual void addRecipes(CraftingManager craftingManager1)
		{
			craftingManager1.addShapelessRecipe(new ItemStack(Item.bowlSoup), new object[]{Block.mushroomBrown, Block.mushroomRed, Item.bowlEmpty});
			craftingManager1.addRecipe(new ItemStack(Item.cookie, 8), new object[]{"#X#", 'X', new ItemStack(Item.dyePowder, 1, 3), '#', Item.wheat});
			craftingManager1.addRecipe(new ItemStack(Block.melon), new object[]{"MMM", "MMM", "MMM", 'M', Item.melon});
			craftingManager1.addRecipe(new ItemStack(Item.melonSeeds), new object[]{"M", 'M', Item.melon});
			craftingManager1.addRecipe(new ItemStack(Item.pumpkinSeeds, 4), new object[]{"M", 'M', Block.pumpkin});
			craftingManager1.addShapelessRecipe(new ItemStack(Item.fermentedSpiderEye), new object[]{Item.spiderEye, Block.mushroomBrown, Item.sugar});
			craftingManager1.addShapelessRecipe(new ItemStack(Item.speckledMelon), new object[]{Item.melon, Item.goldNugget});
			craftingManager1.addShapelessRecipe(new ItemStack(Item.blazePowder, 2), new object[]{Item.blazeRod});
			craftingManager1.addShapelessRecipe(new ItemStack(Item.magmaCream), new object[]{Item.blazePowder, Item.slimeBall});
		}
	}

}