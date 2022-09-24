using System.Collections;

namespace net.minecraft.src
{

	public class ShapelessRecipes : IRecipe
	{
		private readonly ItemStack recipeOutput;
		private readonly System.Collections.IList recipeItems;

		public ShapelessRecipes(ItemStack itemStack1, System.Collections.IList list2)
		{
			this.recipeOutput = itemStack1;
			this.recipeItems = list2;
		}

		public virtual ItemStack RecipeOutput
		{
			get
			{
				return this.recipeOutput;
			}
		}

		public virtual bool matches(InventoryCrafting inventoryCrafting1)
		{
			ArrayList arrayList2 = new ArrayList(this.recipeItems);

			for (int i3 = 0; i3 < 3; ++i3)
			{
				for (int i4 = 0; i4 < 3; ++i4)
				{
					ItemStack itemStack5 = inventoryCrafting1.getStackInRowAndColumn(i4, i3);
					if (itemStack5 != null)
					{
						bool z6 = false;
						System.Collections.IEnumerator iterator7 = arrayList2.GetEnumerator();

						while (iterator7.MoveNext())
						{
							ItemStack itemStack8 = (ItemStack)iterator7.Current;
							if (itemStack5.itemID == itemStack8.itemID && (itemStack8.ItemDamage == -1 || itemStack5.ItemDamage == itemStack8.ItemDamage))
							{
								z6 = true;
								arrayList2.Remove(itemStack8);
								break;
							}
						}

						if (!z6)
						{
							return false;
						}
					}
				}
			}

			return arrayList2.Count == 0;
		}

		public virtual ItemStack getCraftingResult(InventoryCrafting inventoryCrafting1)
		{
			return this.recipeOutput.copy();
		}

		public virtual int RecipeSize
		{
			get
			{
				return this.recipeItems.Count;
			}
		}
	}

}