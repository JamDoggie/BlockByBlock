using BlockByBlock.java_extensions;
using net.minecraft.client.entity;
using System;

namespace net.minecraft.src
{

    public class BlockTallGrass : BlockFlower
	{
		protected internal BlockTallGrass(int i1, int i2) : base(i1, i2, Material.vine)
		{
			float f3 = 0.4F;
			this.setBlockBounds(0.5F - f3, 0.0F, 0.5F - f3, 0.5F + f3, 0.8F, 0.5F + f3);
		}

		public override int getBlockTextureFromSideAndMetadata(int i1, int i2)
		{
			return i2 == 1 ? this.blockIndexInTexture : (i2 == 2 ? this.blockIndexInTexture + 16 + 1 : (i2 == 0 ? this.blockIndexInTexture + 16 : this.blockIndexInTexture));
		}

		public override int BlockColor
		{
			get
			{
				double d1 = 0.5D;
				double d3 = 1.0D;
				return ColorizerGrass.getGrassColor(d1, d3);
			}
		}

		public override int getRenderColor(int i1)
		{
			return i1 == 0 ? 0xFFFFFF : ColorizerFoliage.FoliageColorBasic;
		}

		public override int colorMultiplier(IBlockAccess iBlockAccess1, int i2, int i3, int i4)
		{
			int i5 = iBlockAccess1.getBlockMetadata(i2, i3, i4);
			return i5 == 0 ? 0xFFFFFF : iBlockAccess1.getBiomeGenForCoords(i2, i4).BiomeGrassColor;
		}

		public override int idDropped(int i1, RandomExtended random2, int i3)
		{
			return random2.Next(8) == 0 ? Item.seeds.shiftedIndex : -1;
		}

		public override int quantityDroppedWithBonus(int i1, RandomExtended random2)
		{
			return 1 + random2.Next(i1 * 2 + 1);
		}

		public override void harvestBlock(World world1, EntityPlayer entityPlayer2, int i3, int i4, int i5, int i6)
		{
			if (!world1.isRemote && entityPlayer2.CurrentEquippedItem != null && entityPlayer2.CurrentEquippedItem.itemID == Item.shears.shiftedIndex)
			{
				entityPlayer2.addStat(StatList.mineBlockStatArray[this.blockID], 1);
				this.dropBlockAsItem_do(world1, i3, i4, i5, new ItemStack(Block.tallGrass, 1, i6));
			}
			else
			{
				base.harvestBlock(world1, entityPlayer2, i3, i4, i5, i6);
			}

		}
	}

}