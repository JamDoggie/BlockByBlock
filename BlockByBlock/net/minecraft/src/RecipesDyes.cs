namespace net.minecraft.src
{
	public class RecipesDyes
	{
		public virtual void addRecipes(CraftingManager craftingManager1)
		{
			for (int i2 = 0; i2 < 16; ++i2)
			{
				craftingManager1.addShapelessRecipe(new ItemStack(Block.cloth, 1, BlockCloth.getDyeFromBlock(i2)), new object[]
				{
					new ItemStack(Item.dyePowder, 1, i2),
					new ItemStack(Item.itemsList[Block.cloth.blockID], 1, 0)
				});
			}

			craftingManager1.addShapelessRecipe(new ItemStack(Item.dyePowder, 2, 11), new object[]{Block.plantYellow});
			craftingManager1.addShapelessRecipe(new ItemStack(Item.dyePowder, 2, 1), new object[]{Block.plantRed});
			craftingManager1.addShapelessRecipe(new ItemStack(Item.dyePowder, 3, 15), new object[]{Item.bone});
			craftingManager1.addShapelessRecipe(new ItemStack(Item.dyePowder, 2, 9), new object[]
			{
				new ItemStack(Item.dyePowder, 1, 1),
				new ItemStack(Item.dyePowder, 1, 15)
			});
			craftingManager1.addShapelessRecipe(new ItemStack(Item.dyePowder, 2, 14), new object[]
			{
				new ItemStack(Item.dyePowder, 1, 1),
				new ItemStack(Item.dyePowder, 1, 11)
			});
			craftingManager1.addShapelessRecipe(new ItemStack(Item.dyePowder, 2, 10), new object[]
			{
				new ItemStack(Item.dyePowder, 1, 2),
				new ItemStack(Item.dyePowder, 1, 15)
			});
			craftingManager1.addShapelessRecipe(new ItemStack(Item.dyePowder, 2, 8), new object[]
			{
				new ItemStack(Item.dyePowder, 1, 0),
				new ItemStack(Item.dyePowder, 1, 15)
			});
			craftingManager1.addShapelessRecipe(new ItemStack(Item.dyePowder, 2, 7), new object[]
			{
				new ItemStack(Item.dyePowder, 1, 8),
				new ItemStack(Item.dyePowder, 1, 15)
			});
			craftingManager1.addShapelessRecipe(new ItemStack(Item.dyePowder, 3, 7), new object[]
			{
				new ItemStack(Item.dyePowder, 1, 0),
				new ItemStack(Item.dyePowder, 1, 15),
				new ItemStack(Item.dyePowder, 1, 15)
			});
			craftingManager1.addShapelessRecipe(new ItemStack(Item.dyePowder, 2, 12), new object[]
			{
				new ItemStack(Item.dyePowder, 1, 4),
				new ItemStack(Item.dyePowder, 1, 15)
			});
			craftingManager1.addShapelessRecipe(new ItemStack(Item.dyePowder, 2, 6), new object[]
			{
				new ItemStack(Item.dyePowder, 1, 4),
				new ItemStack(Item.dyePowder, 1, 2)
			});
			craftingManager1.addShapelessRecipe(new ItemStack(Item.dyePowder, 2, 5), new object[]
			{
				new ItemStack(Item.dyePowder, 1, 4),
				new ItemStack(Item.dyePowder, 1, 1)
			});
			craftingManager1.addShapelessRecipe(new ItemStack(Item.dyePowder, 2, 13), new object[]
			{
				new ItemStack(Item.dyePowder, 1, 5),
				new ItemStack(Item.dyePowder, 1, 9)
			});
			craftingManager1.addShapelessRecipe(new ItemStack(Item.dyePowder, 3, 13), new object[]
			{
				new ItemStack(Item.dyePowder, 1, 4),
				new ItemStack(Item.dyePowder, 1, 1),
				new ItemStack(Item.dyePowder, 1, 9)
			});
			craftingManager1.addShapelessRecipe(new ItemStack(Item.dyePowder, 4, 13), new object[]
			{
				new ItemStack(Item.dyePowder, 1, 4),
				new ItemStack(Item.dyePowder, 1, 1),
				new ItemStack(Item.dyePowder, 1, 1),
				new ItemStack(Item.dyePowder, 1, 15)
			});
		}
	}

}