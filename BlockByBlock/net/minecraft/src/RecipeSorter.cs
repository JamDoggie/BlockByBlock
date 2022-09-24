namespace net.minecraft.src
{

	internal class RecipeSorter : System.Collections.Generic.IComparer<IRecipe>
	{
		internal readonly CraftingManager craftingManager;

		internal RecipeSorter(CraftingManager craftingManager1)
		{
			this.craftingManager = craftingManager1;
		}

		public virtual int compareRecipes(IRecipe iRecipe1, IRecipe iRecipe2)
		{
			return iRecipe1 is ShapelessRecipes && iRecipe2 is ShapedRecipes ? 1 : (iRecipe2 is ShapelessRecipes && iRecipe1 is ShapedRecipes ? -1 : (iRecipe2.RecipeSize < iRecipe1.RecipeSize ? -1 : (iRecipe2.RecipeSize > iRecipe1.RecipeSize ? 1 : 0)));
		}

		public virtual int Compare(IRecipe object1, IRecipe object2)
		{
			return this.compareRecipes(object1, object2);
		}
	}
}