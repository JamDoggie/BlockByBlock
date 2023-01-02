using System;
using BlockByBlock.java_extensions;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class BlockDeadBush : BlockFlower
	{
		protected internal BlockDeadBush(int i1, int i2) : base(i1, i2, Material.vine)
		{
			float f3 = 0.4F;
			setBlockBounds(0.5F - f3, 0.0F, 0.5F - f3, 0.5F + f3, 0.8F, 0.5F + f3);
		}

		protected internal override bool canThisPlantGrowOnThisBlockID(int i1)
		{
			return i1 == sand.blockID;
		}

		public override int getBlockTextureFromSideAndMetadata(int i1, int i2)
		{
			return this.blockIndexInTexture;
		}

		public override int idDropped(int i1, RandomExtended random2, int i3)
		{
			return -1;
		}

		public override void harvestBlock(World world1, EntityPlayer entityPlayer2, int i3, int i4, int i5, int i6)
		{
			if (!world1.isRemote && entityPlayer2.CurrentEquippedItem != null && entityPlayer2.CurrentEquippedItem.itemID == Item.shears.shiftedIndex)
			{
				entityPlayer2.addStat(StatList.mineBlockStatArray[blockID], 1);
				dropBlockAsItem_do(world1, i3, i4, i5, new ItemStack(deadBush, 1, i6));
			}
			else
			{
				base.harvestBlock(world1, entityPlayer2, i3, i4, i5, i6);
			}

		}
	}

}