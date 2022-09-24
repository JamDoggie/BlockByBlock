namespace net.minecraft.src
{
	public class RecipesTools
	{
		private string[][] recipePatterns = new string[][]
		{
			new string[] {"XXX", " # ", " # "},
			new string[] {"X", "#", "#"},
			new string[] {"XX", "X#", " #"},
			new string[] {"XX", " #", " #"}
		};
		private object[][] recipeItems = new object[][]
		{
			new object[] {Block.planks, Block.cobblestone, Item.ingotIron, Item.diamond, Item.ingotGold},
			new object[] {Item.pickaxeWood, Item.pickaxeStone, Item.pickaxeSteel, Item.pickaxeDiamond, Item.pickaxeGold},
			new object[] {Item.shovelWood, Item.shovelStone, Item.shovelSteel, Item.shovelDiamond, Item.shovelGold},
			new object[] {Item.axeWood, Item.axeStone, Item.axeSteel, Item.axeDiamond, Item.axeGold},
			new object[] {Item.hoeWood, Item.hoeStone, Item.hoeSteel, Item.hoeDiamond, Item.hoeGold}
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

			craftingManager1.addRecipe(new ItemStack(Item.shears), new object[]{" #", "# ", '#', Item.ingotIron});
		}
	}

}