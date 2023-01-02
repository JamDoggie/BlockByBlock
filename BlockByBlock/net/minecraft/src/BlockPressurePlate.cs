using BlockByBlock.java_extensions;
using net.minecraft.client.entity;
using System;

namespace net.minecraft.src
{

    public class BlockPressurePlate : Block
	{
		private EnumMobType triggerMobType;

		protected internal BlockPressurePlate(int i1, int i2, EnumMobType enumMobType3, Material material4) : base(i1, i2, material4)
		{
			this.triggerMobType = enumMobType3;
			this.TickRandomly = true;
			float f5 = 0.0625F;
			this.setBlockBounds(f5, 0.0F, f5, 1.0F - f5, 0.03125F, 1.0F - f5);
		}

		public override int tickRate()
		{
			return 20;
		}

		public override AxisAlignedBB getCollisionBoundingBoxFromPool(World world1, int i2, int i3, int i4)
		{
			return null;
		}

		public override bool OpaqueCube
		{
			get
			{
				return false;
			}
		}

		public override bool renderAsNormalBlock()
		{
			return false;
		}

		public override bool getBlocksMovement(IBlockAccess iBlockAccess1, int i2, int i3, int i4)
		{
			return true;
		}

		public override bool canPlaceBlockAt(World world1, int i2, int i3, int i4)
		{
			return world1.isBlockNormalCube(i2, i3 - 1, i4) || world1.getBlockId(i2, i3 - 1, i4) == Block.fence.blockID;
		}

		public override void onBlockAdded(World world1, int i2, int i3, int i4)
		{
		}

		public override void onNeighborBlockChange(World world1, int i2, int i3, int i4, int i5)
		{
			bool z6 = false;
			if (!world1.isBlockNormalCube(i2, i3 - 1, i4) && world1.getBlockId(i2, i3 - 1, i4) != Block.fence.blockID)
			{
				z6 = true;
			}

			if (z6)
			{
				this.dropBlockAsItem(world1, i2, i3, i4, world1.getBlockMetadata(i2, i3, i4), 0);
				world1.setBlockWithNotify(i2, i3, i4, 0);
			}

		}

		public override void updateTick(World world1, int i2, int i3, int i4, RandomExtended random5)
		{
			if (!world1.isRemote)
			{
				if (world1.getBlockMetadata(i2, i3, i4) != 0)
				{
					this.setStateIfMobInteractsWithPlate(world1, i2, i3, i4);
				}
			}
		}

		public override void onEntityCollidedWithBlock(World world1, int i2, int i3, int i4, Entity entity5)
		{
			if (!world1.isRemote)
			{
				if (world1.getBlockMetadata(i2, i3, i4) != 1)
				{
					this.setStateIfMobInteractsWithPlate(world1, i2, i3, i4);
				}
			}
		}

		private void setStateIfMobInteractsWithPlate(World world1, int i2, int i3, int i4)
		{
			bool z5 = world1.getBlockMetadata(i2, i3, i4) == 1;
			bool z6 = false;
			float f7 = 0.125F;
			System.Collections.IList list8 = null;
			if (this.triggerMobType == EnumMobType.everything)
			{
				list8 = world1.getEntitiesWithinAABBExcludingEntity((Entity)null, AxisAlignedBB.getBoundingBoxFromPool((double)((float)i2 + f7), (double)i3, (double)((float)i4 + f7), (double)((float)(i2 + 1) - f7), (double)i3 + 0.25D, (double)((float)(i4 + 1) - f7)));
			}

			if (this.triggerMobType == EnumMobType.mobs)
			{
				list8 = world1.getEntitiesWithinAABB(typeof(EntityLiving), AxisAlignedBB.getBoundingBoxFromPool((double)((float)i2 + f7), (double)i3, (double)((float)i4 + f7), (double)((float)(i2 + 1) - f7), (double)i3 + 0.25D, (double)((float)(i4 + 1) - f7)));
			}

			if (this.triggerMobType == EnumMobType.players)
			{
				list8 = world1.getEntitiesWithinAABB(typeof(EntityPlayer), AxisAlignedBB.getBoundingBoxFromPool((double)((float)i2 + f7), (double)i3, (double)((float)i4 + f7), (double)((float)(i2 + 1) - f7), (double)i3 + 0.25D, (double)((float)(i4 + 1) - f7)));
			}

			if (list8.Count > 0)
			{
				z6 = true;
			}

			if (z6 && !z5)
			{
				world1.setBlockMetadataWithNotify(i2, i3, i4, 1);
				world1.notifyBlocksOfNeighborChange(i2, i3, i4, this.blockID);
				world1.notifyBlocksOfNeighborChange(i2, i3 - 1, i4, this.blockID);
				world1.markBlocksDirty(i2, i3, i4, i2, i3, i4);
				world1.playSoundEffect((double)i2 + 0.5D, (double)i3 + 0.1D, (double)i4 + 0.5D, "random.click", 0.3F, 0.6F);
			}

			if (!z6 && z5)
			{
				world1.setBlockMetadataWithNotify(i2, i3, i4, 0);
				world1.notifyBlocksOfNeighborChange(i2, i3, i4, this.blockID);
				world1.notifyBlocksOfNeighborChange(i2, i3 - 1, i4, this.blockID);
				world1.markBlocksDirty(i2, i3, i4, i2, i3, i4);
				world1.playSoundEffect((double)i2 + 0.5D, (double)i3 + 0.1D, (double)i4 + 0.5D, "random.click", 0.3F, 0.5F);
			}

			if (z6)
			{
				world1.scheduleBlockUpdate(i2, i3, i4, this.blockID, this.tickRate());
			}

		}

		public override void onBlockRemoval(World world1, int i2, int i3, int i4)
		{
			int i5 = world1.getBlockMetadata(i2, i3, i4);
			if (i5 > 0)
			{
				world1.notifyBlocksOfNeighborChange(i2, i3, i4, this.blockID);
				world1.notifyBlocksOfNeighborChange(i2, i3 - 1, i4, this.blockID);
			}

			base.onBlockRemoval(world1, i2, i3, i4);
		}

		public override void setBlockBoundsBasedOnState(IBlockAccess iBlockAccess1, int i2, int i3, int i4)
		{
			bool z5 = iBlockAccess1.getBlockMetadata(i2, i3, i4) == 1;
			float f6 = 0.0625F;
			if (z5)
			{
				this.setBlockBounds(f6, 0.0F, f6, 1.0F - f6, 0.03125F, 1.0F - f6);
			}
			else
			{
				this.setBlockBounds(f6, 0.0F, f6, 1.0F - f6, 0.0625F, 1.0F - f6);
			}

		}

		public override bool isPoweringTo(IBlockAccess iBlockAccess1, int i2, int i3, int i4, int i5)
		{
			return iBlockAccess1.getBlockMetadata(i2, i3, i4) > 0;
		}

		public override bool isIndirectlyPoweringTo(World world1, int i2, int i3, int i4, int i5)
		{
			return world1.getBlockMetadata(i2, i3, i4) == 0 ? false : i5 == 1;
		}

		public override bool canProvidePower()
		{
			return true;
		}

		public override void setBlockBoundsForItemRender()
		{
			float f1 = 0.5F;
			float f2 = 0.125F;
			float f3 = 0.5F;
			this.setBlockBounds(0.5F - f1, 0.5F - f2, 0.5F - f3, 0.5F + f1, 0.5F + f2, 0.5F + f3);
		}

		public override int MobilityFlag
		{
			get
			{
				return 1;
			}
		}
	}

}