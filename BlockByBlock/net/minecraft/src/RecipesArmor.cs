namespace net.minecraft.src
{
	public class RecipesArmor
	{
		private string[][] recipePatterns = new string[][]
		{
			new string[] {"XXX", "X X"},
			new string[] {"X X", "XXX", "XXX"},
			new string[] {"XXX", "X X", "X X"},
			new string[] {"X X", "X X"}
		};
		private object[][] recipeItems = new object[][]
		{
			new object[] {Item.leather, Block.fire, Item.ingotIron, Item.diamond, Item.ingotGold},
			new object[] {Item.helmetLeather, Item.helmetChain, Item.helmetSteel, Item.helmetDiamond, Item.helmetGold},
			new object[] {Item.plateLeather, Item.plateChain, Item.plateSteel, Item.plateDiamond, Item.plateGold},
			new object[] {Item.legsLeather, Item.legsChain, Item.legsSteel, Item.legsDiamond, Item.legsGold},
			new object[] {Item.bootsLeather, Item.bootsChain, Item.bootsSteel, Item.bootsDiamond, Item.bootsGold}
		};

		public virtual void addRecipes(CraftingManager craftingManager1)
		{
			for (int i2 = 0; i2 < this.recipeItems[0].Length; ++i2)
			{
				object object3 = this.recipeItems[0][i2];

				for (int i4 = 0; i4 < this.recipeItems.Length - 1; ++i4)
				{
					Item item5 = (Item)this.recipeItems[i4 + 1][i2];
					craftingManager1.addRecipe(new ItemStack(item5), new object[]{this.recipePatterns[i4], 'X', object3});
				}
			}

		}
	}

}