namespace net.minecraft.src
{
	public interface IRecipe
	{
		bool matches(InventoryCrafting inventoryCrafting1);

		ItemStack getCraftingResult(InventoryCrafting inventoryCrafting1);

		int RecipeSize {get;}

		ItemStack RecipeOutput {get;}
	}

}