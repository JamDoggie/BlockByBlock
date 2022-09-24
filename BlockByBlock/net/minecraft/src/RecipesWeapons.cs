namespace net.minecraft.src
{
	public class RecipesWeapons
	{
		private string[][] recipePatterns = new string[][]
		{
			new string[] {"X", "X", "#"}
		};
		private object[][] recipeItems = new object[][]
		{
			new object[] {Block.planks, Block.cobblestone, Item.ingotIron, Item.diamond, Item.ingotGold},
			new object[] {Item.swordWood, Item.swordStone, Item.swordSteel, Item.swordDiamond, Item.swordGold}
		};

		public virtual void addRecipes(CraftingManager craftingManager1)
		{
			for (int i2 = 0; i2 < this.recipeItems[0].Length; ++i2)
			{
				object object3 = this.recipeItems[0][i2];

				for (int i4 = 0; i4 < this.recipeItems.Length - 1; ++i4)
				{
					Item item5 = (Item)this.recipeItems[i4 + 1][i2];
					craftingManager1.addRecipe(new ItemStack(item5), new object[]{this.recipePatterns[i4], '#', Item.stick, 'X', object3});
				}
			}

			craftingManager1.addRecipe(new ItemStack(Item.bow, 1), new object[]{" #X", "# X", " #X", 'X', Item.silk, '#', Item.stick});
			craftingManager1.addRecipe(new ItemStack(Item.arrow, 4), new object[]{"X", "#", "Y", 'Y', Item.feather, 'X', Item.flint, '#', Item.stick});
		}
	}

}