using BlockByBlock.java_extensions;
using net.minecraft.client.entity;
using System;

namespace net.minecraft.src
{

    public class BlockIce : BlockBreakable
	{
		public BlockIce(int i1, int i2) : base(i1, i2, Material.ice, false)
		{
			this.slipperiness = 0.98F;
			this.TickRandomly = true;
		}

		public override int RenderBlockPass
		{
			get
			{
				return 1;
			}
		}

		public override bool shouldSideBeRendered(IBlockAccess iBlockAccess1, int i2, int i3, int i4, int i5)
		{
			return base.shouldSideBeRendered(iBlockAccess1, i2, i3, i4, 1 - i5);
		}

		public override void harvestBlock(World world1, EntityPlayer entityPlayer2, int i3, int i4, int i5, int i6)
		{
			base.harvestBlock(world1, entityPlayer2, i3, i4, i5, i6);
			Material material7 = world1.getBlockMaterial(i3, i4 - 1, i5);
			if (material7.blocksMovement() || material7.Liquid)
			{
				world1.setBlockWithNotify(i3, i4, i5, Block.waterMoving.blockID);
			}

		}

		public override int quantityDropped(RandomExtended random1)
		{
			return 0;
		}

		public override void updateTick(World world1, int i2, int i3, int i4, RandomExtended random5)
		{
			if (world1.getSavedLightValue(EnumSkyBlock.Block, i2, i3, i4) > 11 - Block.lightOpacity[this.blockID])
			{
				this.dropBlockAsItem(world1, i2, i3, i4, world1.getBlockMetadata(i2, i3, i4), 0);
				world1.setBlockWithNotify(i2, i3, i4, Block.waterStill.blockID);
			}

		}

		public override int MobilityFlag
		{
			get
			{
				return 0;
			}
		}

		protected internal override ItemStack createStackedBlock(int i1)
		{
			return null;
		}
	}

}