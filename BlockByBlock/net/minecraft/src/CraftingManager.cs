using System;
using System.Collections;
using System.Linq;

namespace net.minecraft.src
{

	public class CraftingManager
	{
		private static readonly CraftingManager instance = new CraftingManager();
		private List<IRecipe> recipes = new();

		public static CraftingManager Instance
		{
			get
			{
				return instance;
			}
		}

		private CraftingManager()
		{
			(new RecipesTools()).addRecipes(this);
			(new RecipesWeapons()).addRecipes(this);
			(new RecipesIngots()).addRecipes(this);
			(new RecipesFood()).addRecipes(this);
			(new RecipesCrafting()).addRecipes(this);
			(new RecipesArmor()).addRecipes(this);
			(new RecipesDyes()).addRecipes(this);
			addRecipe(new ItemStack(Item.paper, 3), new object[]{"###", '#', Item.reed});
			addRecipe(new ItemStack(Item.book, 1), new object[]{"#", "#", "#", '#', Item.paper});
			addRecipe(new ItemStack(Block.fence, 2), new object[]{"###", "###", '#', Item.stick});
			addRecipe(new ItemStack(Block.netherFence, 6), new object[]{"###", "###", '#', Block.netherBrick});
			addRecipe(new ItemStack(Block.fenceGate, 1), new object[]{"#W#", "#W#", '#', Item.stick, 'W', Block.planks});
			addRecipe(new ItemStack(Block.jukebox, 1), new object[]{"###", "#X#", "###", '#', Block.planks, 'X', Item.diamond});
			addRecipe(new ItemStack(Block.music, 1), new object[]{"###", "#X#", "###", '#', Block.planks, 'X', Item.redstone});
			addRecipe(new ItemStack(Block.bookShelf, 1), new object[]{"###", "XXX", "###", '#', Block.planks, 'X', Item.book});
			addRecipe(new ItemStack(Block.blockSnow, 1), new object[]{"##", "##", '#', Item.snowball});
			addRecipe(new ItemStack(Block.blockClay, 1), new object[]{"##", "##", '#', Item.clay});
			addRecipe(new ItemStack(Block.brick, 1), new object[]{"##", "##", '#', Item.brick});
			addRecipe(new ItemStack(Block.glowStone, 1), new object[]{"##", "##", '#', Item.lightStoneDust});
			addRecipe(new ItemStack(Block.cloth, 1), new object[]{"##", "##", '#', Item.silk});
			addRecipe(new ItemStack(Block.tnt, 1), new object[]{"X#X", "#X#", "X#X", 'X', Item.gunpowder, '#', Block.sand});
			addRecipe(new ItemStack(Block.stairSingle, 6, 3), new object[]{"###", '#', Block.cobblestone});
			addRecipe(new ItemStack(Block.stairSingle, 6, 0), new object[]{"###", '#', Block.stone});
			addRecipe(new ItemStack(Block.stairSingle, 6, 1), new object[]{"###", '#', Block.sandStone});
			addRecipe(new ItemStack(Block.stairSingle, 6, 2), new object[]{"###", '#', Block.planks});
			addRecipe(new ItemStack(Block.stairSingle, 6, 4), new object[]{"###", '#', Block.brick});
			addRecipe(new ItemStack(Block.stairSingle, 6, 5), new object[]{"###", '#', Block.stoneBrick});
			addRecipe(new ItemStack(Block.ladder, 3), new object[]{"# #", "###", "# #", '#', Item.stick});
			addRecipe(new ItemStack(Item.doorWood, 1), new object[]{"##", "##", "##", '#', Block.planks});
			addRecipe(new ItemStack(Block.trapdoor, 2), new object[]{"###", "###", '#', Block.planks});
			addRecipe(new ItemStack(Item.doorSteel, 1), new object[]{"##", "##", "##", '#', Item.ingotIron});
			addRecipe(new ItemStack(Item.sign, 1), new object[]{"###", "###", " X ", '#', Block.planks, 'X', Item.stick});
			addRecipe(new ItemStack(Item.cake, 1), new object[]{"AAA", "BEB", "CCC", 'A', Item.bucketMilk, 'B', Item.sugar, 'C', Item.wheat, 'E', Item.egg});
			addRecipe(new ItemStack(Item.sugar, 1), new object[]{"#", '#', Item.reed});
			addRecipe(new ItemStack(Block.planks, 4, 0), new object[]{"#", '#', new ItemStack(Block.wood, 1, 0)});
			addRecipe(new ItemStack(Block.planks, 4, 1), new object[]{"#", '#', new ItemStack(Block.wood, 1, 1)});
			addRecipe(new ItemStack(Block.planks, 4, 2), new object[]{"#", '#', new ItemStack(Block.wood, 1, 2)});
			addRecipe(new ItemStack(Block.planks, 4, 3), new object[]{"#", '#', new ItemStack(Block.wood, 1, 3)});
			addRecipe(new ItemStack(Item.stick, 4), new object[]{"#", "#", '#', Block.planks});
			addRecipe(new ItemStack(Block.torchWood, 4), new object[]{"X", "#", 'X', Item.coal, '#', Item.stick});
			addRecipe(new ItemStack(Block.torchWood, 4), new object[]{"X", "#", 'X', new ItemStack(Item.coal, 1, 1), '#', Item.stick});
			addRecipe(new ItemStack(Item.bowlEmpty, 4), new object[]{"# #", " # ", '#', Block.planks});
			addRecipe(new ItemStack(Item.glassBottle, 3), new object[]{"# #", " # ", '#', Block.glass});
			addRecipe(new ItemStack(Block.rail, 16), new object[]{"X X", "X#X", "X X", 'X', Item.ingotIron, '#', Item.stick});
			addRecipe(new ItemStack(Block.railPowered, 6), new object[]{"X X", "X#X", "XRX", 'X', Item.ingotGold, 'R', Item.redstone, '#', Item.stick});
			addRecipe(new ItemStack(Block.railDetector, 6), new object[]{"X X", "X#X", "XRX", 'X', Item.ingotIron, 'R', Item.redstone, '#', Block.pressurePlateStone});
			addRecipe(new ItemStack(Item.minecartEmpty, 1), new object[]{"# #", "###", '#', Item.ingotIron});
			addRecipe(new ItemStack(Item.cauldron, 1), new object[]{"# #", "# #", "###", '#', Item.ingotIron});
			addRecipe(new ItemStack(Item.brewingStand, 1), new object[]{" B ", "###", '#', Block.cobblestone, 'B', Item.blazeRod});
			addRecipe(new ItemStack(Block.pumpkinLantern, 1), new object[]{"A", "B", 'A', Block.pumpkin, 'B', Block.torchWood});
			addRecipe(new ItemStack(Item.minecartCrate, 1), new object[]{"A", "B", 'A', Block.chest, 'B', Item.minecartEmpty});
			addRecipe(new ItemStack(Item.minecartPowered, 1), new object[]{"A", "B", 'A', Block.stoneOvenIdle, 'B', Item.minecartEmpty});
			addRecipe(new ItemStack(Item.boat, 1), new object[]{"# #", "###", '#', Block.planks});
			addRecipe(new ItemStack(Item.bucketEmpty, 1), new object[]{"# #", " # ", '#', Item.ingotIron});
			addRecipe(new ItemStack(Item.flintAndSteel, 1), new object[]{"A ", " B", 'A', Item.ingotIron, 'B', Item.flint});
			addRecipe(new ItemStack(Item.bread, 1), new object[]{"###", '#', Item.wheat});
			addRecipe(new ItemStack(Block.stairCompactPlanks, 4), new object[]{"#  ", "## ", "###", '#', Block.planks});
			addRecipe(new ItemStack(Item.fishingRod, 1), new object[]{"  #", " #X", "# X", '#', Item.stick, 'X', Item.silk});
			addRecipe(new ItemStack(Block.stairCompactCobblestone, 4), new object[]{"#  ", "## ", "###", '#', Block.cobblestone});
			addRecipe(new ItemStack(Block.stairsBrick, 4), new object[]{"#  ", "## ", "###", '#', Block.brick});
			addRecipe(new ItemStack(Block.stairsStoneBrickSmooth, 4), new object[]{"#  ", "## ", "###", '#', Block.stoneBrick});
			addRecipe(new ItemStack(Block.stairsNetherBrick, 4), new object[]{"#  ", "## ", "###", '#', Block.netherBrick});
			addRecipe(new ItemStack(Item.painting, 1), new object[]{"###", "#X#", "###", '#', Item.stick, 'X', Block.cloth});
			addRecipe(new ItemStack(Item.appleGold, 1), new object[]{"###", "#X#", "###", '#', Item.goldNugget, 'X', Item.appleRed});
			addRecipe(new ItemStack(Block.lever, 1), new object[]{"X", "#", '#', Block.cobblestone, 'X', Item.stick});
			addRecipe(new ItemStack(Block.torchRedstoneActive, 1), new object[]{"X", "#", '#', Item.stick, 'X', Item.redstone});
			addRecipe(new ItemStack(Item.redstoneRepeater, 1), new object[]{"#X#", "III", '#', Block.torchRedstoneActive, 'X', Item.redstone, 'I', Block.stone});
			addRecipe(new ItemStack(Item.pocketSundial, 1), new object[]{" # ", "#X#", " # ", '#', Item.ingotGold, 'X', Item.redstone});
			addRecipe(new ItemStack(Item.compass, 1), new object[]{" # ", "#X#", " # ", '#', Item.ingotIron, 'X', Item.redstone});
			addRecipe(new ItemStack(Item.map, 1), new object[]{"###", "#X#", "###", '#', Item.paper, 'X', Item.compass});
			addRecipe(new ItemStack(Block.button, 1), new object[]{"#", "#", '#', Block.stone});
			addRecipe(new ItemStack(Block.pressurePlateStone, 1), new object[]{"##", '#', Block.stone});
			addRecipe(new ItemStack(Block.pressurePlatePlanks, 1), new object[]{"##", '#', Block.planks});
			addRecipe(new ItemStack(Block.dispenser, 1), new object[]{"###", "#X#", "#R#", '#', Block.cobblestone, 'X', Item.bow, 'R', Item.redstone});
			addRecipe(new ItemStack(Block.pistonBase, 1), new object[]{"TTT", "#X#", "#R#", '#', Block.cobblestone, 'X', Item.ingotIron, 'R', Item.redstone, 'T', Block.planks});
			addRecipe(new ItemStack(Block.pistonStickyBase, 1), new object[]{"S", "P", 'S', Item.slimeBall, 'P', Block.pistonBase});
			addRecipe(new ItemStack(Item.bed, 1), new object[]{"###", "XXX", '#', Block.cloth, 'X', Block.planks});
			addRecipe(new ItemStack(Block.enchantmentTable, 1), new object[]{" B ", "D#D", "###", '#', Block.obsidian, 'B', Item.book, 'D', Item.diamond});
			addShapelessRecipe(new ItemStack(Item.eyeOfEnder, 1), new object[]{Item.enderPearl, Item.blazePowder});
			addShapelessRecipe(new ItemStack(Item.fireballCharge, 3), new object[]{Item.gunpowder, Item.blazePowder, Item.coal});
			addShapelessRecipe(new ItemStack(Item.fireballCharge, 3), new object[]{Item.gunpowder, Item.blazePowder, new ItemStack(Item.coal, 1, 1)});
			recipes = recipes.OrderBy(recipe => recipe, new RecipeSorter(this)).ToList();
			Console.WriteLine(this.recipes.Count + " recipes");
		}

		internal virtual void addRecipe(ItemStack itemStack1, params object[] object2)
		{
			string string3 = "";
			int i4 = 0;
			int i5 = 0;
			int i6 = 0;
			if (object2[i4] is string[])
			{
				string[] string11 = (string[])((string[])object2[i4++]);

				for (int i8 = 0; i8 < string11.Length; ++i8)
				{
					string string9 = string11[i8];
					++i6;
					i5 = string9.Length;
					string3 = string3 + string9;
				}
			}
			else
			{
				while (object2[i4] is string)
				{
					string string7 = (string)object2[i4++];
					++i6;
					i5 = string7.Length;
					string3 = string3 + string7;
				}
			}

			Hashtable hashMap12;
			for (hashMap12 = new Hashtable(); i4 < object2.Length; i4 += 2)
			{
				char? character13 = (char?)object2[i4];
				ItemStack itemStack15 = null;
				if (object2[i4 + 1] is Item)
				{
					itemStack15 = new ItemStack((Item)object2[i4 + 1]);
				}
				else if (object2[i4 + 1] is Block)
				{
					itemStack15 = new ItemStack((Block)object2[i4 + 1], 1, -1);
				}
				else if (object2[i4 + 1] is ItemStack)
				{
					itemStack15 = (ItemStack)object2[i4 + 1];
				}

				hashMap12[character13] = itemStack15;
			}

			ItemStack[] itemStack14 = new ItemStack[i5 * i6];

			for (int i16 = 0; i16 < i5 * i6; ++i16)
			{
				char c10 = string3[i16];
				if (hashMap12.ContainsKey(c10))
				{
					itemStack14[i16] = ((ItemStack)hashMap12[c10]).copy();
				}
				else
				{
					itemStack14[i16] = null;
				}
			}

			this.recipes.Add(new ShapedRecipes(i5, i6, itemStack14, itemStack1));
		}

		internal virtual void addShapelessRecipe(ItemStack itemStack1, params object[] object2)
		{
			ArrayList arrayList3 = new ArrayList();
			object[] object4 = object2;
			int i5 = object2.Length;

			for (int i6 = 0; i6 < i5; ++i6)
			{
				object object7 = object4[i6];
				if (object7 is ItemStack)
				{
					arrayList3.Add(((ItemStack)object7).copy());
				}
				else if (object7 is Item)
				{
					arrayList3.Add(new ItemStack((Item)object7));
				}
				else
				{
					if (!(object7 is Block))
					{
						throw new Exception("Invalid shapeless recipy!");
					}

					arrayList3.Add(new ItemStack((Block)object7));
				}
			}

			this.recipes.Add(new ShapelessRecipes(itemStack1, arrayList3));
		}

		public virtual ItemStack findMatchingRecipe(InventoryCrafting inventoryCrafting1)
		{
			int i2 = 0;
			ItemStack itemStack3 = null;
			ItemStack itemStack4 = null;

			int i5;
			for (i5 = 0; i5 < inventoryCrafting1.SizeInventory; ++i5)
			{
				ItemStack itemStack6 = inventoryCrafting1.getStackInSlot(i5);
				if (itemStack6 != null)
				{
					if (i2 == 0)
					{
						itemStack3 = itemStack6;
					}

					if (i2 == 1)
					{
						itemStack4 = itemStack6;
					}

					++i2;
				}
			}

			if (i2 == 2 && itemStack3.itemID == itemStack4.itemID && itemStack3.stackSize == 1 && itemStack4.stackSize == 1 && Item.itemsList[itemStack3.itemID].Damageable)
			{
				Item item10 = Item.itemsList[itemStack3.itemID];
				int i12 = item10.MaxDamage - itemStack3.ItemDamageForDisplay;
				int i7 = item10.MaxDamage - itemStack4.ItemDamageForDisplay;
				int i8 = i12 + i7 + item10.MaxDamage * 10 / 100;
				int i9 = item10.MaxDamage - i8;
				if (i9 < 0)
				{
					i9 = 0;
				}

				return new ItemStack(itemStack3.itemID, 1, i9);
			}
			else
			{
				for (i5 = 0; i5 < this.recipes.Count; ++i5)
				{
					IRecipe iRecipe11 = (IRecipe)this.recipes[i5];
					if (iRecipe11.matches(inventoryCrafting1))
					{
						return iRecipe11.getCraftingResult(inventoryCrafting1);
					}
				}

				return null;
			}
		}

		public virtual System.Collections.IList RecipeList
		{
			get
			{
				return this.recipes;
			}
		}
	}

}