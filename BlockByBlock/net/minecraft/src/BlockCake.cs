using BlockByBlock.java_extensions;
using net.minecraft.client.entity;
using System;

namespace net.minecraft.src
{

    public class BlockCake : Block
	{
		protected internal BlockCake(int i1, int i2) : base(i1, i2, Material.cake)
		{
			this.TickRandomly = true;
		}

		public override void setBlockBoundsBasedOnState(IBlockAccess iBlockAccess1, int i2, int i3, int i4)
		{
			int i5 = iBlockAccess1.getBlockMetadata(i2, i3, i4);
			float f6 = 0.0625F;
			float f7 = (float)(1 + i5 * 2) / 16.0F;
			float f8 = 0.5F;
			this.setBlockBounds(f7, 0.0F, f6, 1.0F - f6, f8, 1.0F - f6);
		}

		public override void setBlockBoundsForItemRender()
		{
			float f1 = 0.0625F;
			float f2 = 0.5F;
			this.setBlockBounds(f1, 0.0F, f1, 1.0F - f1, f2, 1.0F - f1);
		}

		public override AxisAlignedBB getCollisionBoundingBoxFromPool(World world1, int i2, int i3, int i4)
		{
			int i5 = world1.getBlockMetadata(i2, i3, i4);
			float f6 = 0.0625F;
			float f7 = (float)(1 + i5 * 2) / 16.0F;
			float f8 = 0.5F;
			return AxisAlignedBB.getBoundingBoxFromPool((double)((float)i2 + f7), (double)i3, (double)((float)i4 + f6), (double)((float)(i2 + 1) - f6), (double)((float)i3 + f8 - f6), (double)((float)(i4 + 1) - f6));
		}

		public override AxisAlignedBB getSelectedBoundingBoxFromPool(World world1, int i2, int i3, int i4)
		{
			int i5 = world1.getBlockMetadata(i2, i3, i4);
			float f6 = 0.0625F;
			float f7 = (float)(1 + i5 * 2) / 16.0F;
			float f8 = 0.5F;
			return AxisAlignedBB.getBoundingBoxFromPool((double)((float)i2 + f7), (double)i3, (double)((float)i4 + f6), (double)((float)(i2 + 1) - f6), (double)((float)i3 + f8), (double)((float)(i4 + 1) - f6));
		}

		public override int getBlockTextureFromSideAndMetadata(int i1, int i2)
		{
			return i1 == 1 ? this.blockIndexInTexture : (i1 == 0 ? this.blockIndexInTexture + 3 : (i2 > 0 && i1 == 4 ? this.blockIndexInTexture + 2 : this.blockIndexInTexture + 1));
		}

		public override int getBlockTextureFromSide(int i1)
		{
			return i1 == 1 ? this.blockIndexInTexture : (i1 == 0 ? this.blockIndexInTexture + 3 : this.blockIndexInTexture + 1);
		}

		public override bool renderAsNormalBlock()
		{
			return false;
		}

		public override bool OpaqueCube
		{
			get
			{
				return false;
			}
		}

		public override bool blockActivated(World world1, int i2, int i3, int i4, EntityPlayer entityPlayer5)
		{
			this.eatCakeSlice(world1, i2, i3, i4, entityPlayer5);
			return true;
		}

		public override void onBlockClicked(World world1, int i2, int i3, int i4, EntityPlayer entityPlayer5)
		{
			this.eatCakeSlice(world1, i2, i3, i4, entityPlayer5);
		}

		private void eatCakeSlice(World world1, int i2, int i3, int i4, EntityPlayer entityPlayer5)
		{
			if (entityPlayer5.canEat(false))
			{
				entityPlayer5.FoodStats.addStats(2, 0.1F);
				int i6 = world1.getBlockMetadata(i2, i3, i4) + 1;
				if (i6 >= 6)
				{
					world1.setBlockWithNotify(i2, i3, i4, 0);
				}
				else
				{
					world1.setBlockMetadataWithNotify(i2, i3, i4, i6);
					world1.markBlockAsNeedsUpdate(i2, i3, i4);
				}
			}

		}

		public override bool canPlaceBlockAt(World world1, int i2, int i3, int i4)
		{
			return !base.canPlaceBlockAt(world1, i2, i3, i4) ? false : this.canBlockStay(world1, i2, i3, i4);
		}

		public override void onNeighborBlockChange(World world1, int i2, int i3, int i4, int i5)
		{
			if (!this.canBlockStay(world1, i2, i3, i4))
			{
				this.dropBlockAsItem(world1, i2, i3, i4, world1.getBlockMetadata(i2, i3, i4), 0);
				world1.setBlockWithNotify(i2, i3, i4, 0);
			}

		}

		public override bool canBlockStay(World world1, int i2, int i3, int i4)
		{
			return world1.getBlockMaterial(i2, i3 - 1, i4).Solid;
		}

		public override int quantityDropped(RandomExtended random1)
		{
			return 0;
		}

		public override int idDropped(int i1, RandomExtended random2, int i3)
		{
			return 0;
		}
	}

}