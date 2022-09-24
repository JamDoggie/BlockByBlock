namespace net.minecraft.src
{
	public class RecipesIngots
	{
		private object[][] recipeItems = new object[][]
		{
			new object[] {Block.blockGold, new ItemStack(Item.ingotGold, 9)},
			new object[] {Block.blockSteel, new ItemStack(Item.ingotIron, 9)},
			new object[] {Block.blockDiamond, new ItemStack(Item.diamond, 9)},
			new object[] {Block.blockLapis, new ItemStack(Item.dyePowder, 9, 4)}
		};

		public virtual void addRecipes(CraftingManager craftingManager1)
		{
			for (int i2 = 0; i2 < this.recipeItems.Length; ++i2)
			{
				Block block3 = (Block)this.recipeItems[i2][0];
				ItemStack itemStack4 = (ItemStack)this.recipeItems[i2][1];
				craftingManager1.addRecipe(new ItemStack(block3), new object[]{"###", "###", "###", '#', itemStack4});
				craftingManager1.addRecipe(itemStack4, new object[]{"#", '#', block3});
			}

			craftingManager1.addRecipe(new ItemStack(Item.ingotGold), new object[]{"###", "###", "###", '#', Item.goldNugget});
			craftingManager1.addRecipe(new ItemStack(Item.goldNugget, 9), new object[]{"#", '#', Item.ingotGold});
		}
	}

}