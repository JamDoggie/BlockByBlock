using System;
using BlockByBlock.java_extensions;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class BlockDetectorRail : BlockRail
	{
		public BlockDetectorRail(int i1, int i2) : base(i1, i2, true)
		{
			this.TickRandomly = true;
		}

		public override int tickRate()
		{
			return 20;
		}

		public override bool canProvidePower()
		{
			return true;
		}

		public override void onEntityCollidedWithBlock(World world1, int i2, int i3, int i4, Entity entity5)
		{
			if (!world1.isRemote)
			{
				int i6 = world1.getBlockMetadata(i2, i3, i4);
				if ((i6 & 8) == 0)
				{
					this.setStateIfMinecartInteractsWithRail(world1, i2, i3, i4, i6);
				}
			}
		}

		public override void updateTick(World world1, int i2, int i3, int i4, RandomExtended random5)
		{
			if (!world1.isRemote)
			{
				int i6 = world1.getBlockMetadata(i2, i3, i4);
				if ((i6 & 8) != 0)
				{
					this.setStateIfMinecartInteractsWithRail(world1, i2, i3, i4, i6);
				}
			}
		}

		public override bool isPoweringTo(IBlockAccess iBlockAccess1, int i2, int i3, int i4, int i5)
		{
			return (iBlockAccess1.getBlockMetadata(i2, i3, i4) & 8) != 0;
		}

		public override bool isIndirectlyPoweringTo(World world1, int i2, int i3, int i4, int i5)
		{
			return (world1.getBlockMetadata(i2, i3, i4) & 8) == 0 ? false : i5 == 1;
		}

		private void setStateIfMinecartInteractsWithRail(World world1, int i2, int i3, int i4, int i5)
		{
			bool z6 = (i5 & 8) != 0;
			bool z7 = false;
			float f8 = 0.125F;
			System.Collections.IList list9 = world1.getEntitiesWithinAABB(typeof(EntityMinecart), AxisAlignedBB.getBoundingBoxFromPool((double)((float)i2 + f8), (double)i3, (double)((float)i4 + f8), (double)((float)(i2 + 1) - f8), (double)((float)(i3 + 1) - f8), (double)((float)(i4 + 1) - f8)));
			if (list9.Count > 0)
			{
				z7 = true;
			}

			if (z7 && !z6)
			{
				world1.setBlockMetadataWithNotify(i2, i3, i4, i5 | 8);
				world1.notifyBlocksOfNeighborChange(i2, i3, i4, this.blockID);
				world1.notifyBlocksOfNeighborChange(i2, i3 - 1, i4, this.blockID);
				world1.markBlocksDirty(i2, i3, i4, i2, i3, i4);
			}

			if (!z7 && z6)
			{
				world1.setBlockMetadataWithNotify(i2, i3, i4, i5 & 7);
				world1.notifyBlocksOfNeighborChange(i2, i3, i4, this.blockID);
				world1.notifyBlocksOfNeighborChange(i2, i3 - 1, i4, this.blockID);
				world1.markBlocksDirty(i2, i3, i4, i2, i3, i4);
			}

			if (z7)
			{
				world1.scheduleBlockUpdate(i2, i3, i4, this.blockID, this.tickRate());
			}

		}
	}

}