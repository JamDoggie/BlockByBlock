using System.Collections;

namespace net.minecraft.src
{

	public class FurnaceRecipes
	{
		private static readonly FurnaceRecipes smeltingBase = new FurnaceRecipes();
		private System.Collections.IDictionary smeltingList = new Hashtable();

		public static FurnaceRecipes smelting()
		{
			return smeltingBase;
		}

		private FurnaceRecipes()
		{
			this.addSmelting(Block.oreIron.blockID, new ItemStack(Item.ingotIron));
			this.addSmelting(Block.oreGold.blockID, new ItemStack(Item.ingotGold));
			this.addSmelting(Block.oreDiamond.blockID, new ItemStack(Item.diamond));
			this.addSmelting(Block.sand.blockID, new ItemStack(Block.glass));
			this.addSmelting(Item.porkRaw.shiftedIndex, new ItemStack(Item.porkCooked));
			this.addSmelting(Item.beefRaw.shiftedIndex, new ItemStack(Item.beefCooked));
			this.addSmelting(Item.chickenRaw.shiftedIndex, new ItemStack(Item.chickenCooked));
			this.addSmelting(Item.fishRaw.shiftedIndex, new ItemStack(Item.fishCooked));
			this.addSmelting(Block.cobblestone.blockID, new ItemStack(Block.stone));
			this.addSmelting(Item.clay.shiftedIndex, new ItemStack(Item.brick));
			this.addSmelting(Block.cactus.blockID, new ItemStack(Item.dyePowder, 1, 2));
			this.addSmelting(Block.wood.blockID, new ItemStack(Item.coal, 1, 1));
			this.addSmelting(Block.oreCoal.blockID, new ItemStack(Item.coal));
			this.addSmelting(Block.oreRedstone.blockID, new ItemStack(Item.redstone));
			this.addSmelting(Block.oreLapis.blockID, new ItemStack(Item.dyePowder, 1, 4));
		}

		public virtual void addSmelting(int i1, ItemStack itemStack2)
		{
			this.smeltingList[i1] = itemStack2;
		}

		public virtual ItemStack getSmeltingResult(int i1)
		{
			return (ItemStack)this.smeltingList[i1];
		}

		public virtual System.Collections.IDictionary SmeltingList
		{
			get
			{
				return this.smeltingList;
			}
		}
	}

}