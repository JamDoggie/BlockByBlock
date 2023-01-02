using BlockByBlock.java_extensions;
using net.minecraft.client.entity;
using System;
using System.Collections;

namespace net.minecraft.src
{

    public class BlockEndPortal : BlockContainer
	{
		public static bool bossDefeated = false;

		protected internal BlockEndPortal(int i1, Material material2) : base(i1, 0, material2)
		{
			this.setLightValue(1.0F);
		}

		public override TileEntity BlockEntity
		{
			get
			{
				return new TileEntityEndPortal();
			}
		}

		public override void setBlockBoundsBasedOnState(IBlockAccess iBlockAccess1, int i2, int i3, int i4)
		{
			float f5 = 0.0625F;
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, f5, 1.0F);
		}

		public override bool shouldSideBeRendered(IBlockAccess iBlockAccess1, int i2, int i3, int i4, int i5)
		{
			return i5 != 0 ? false : base.shouldSideBeRendered(iBlockAccess1, i2, i3, i4, i5);
		}

		public override void getCollidingBoundingBoxes(World world1, int i2, int i3, int i4, AxisAlignedBB axisAlignedBB5, ArrayList arrayList6)
		{
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
        
		public override int quantityDropped(RandomExtended random1)
		{
			return 0;
		}

		public override void onEntityCollidedWithBlock(World world1, int i2, int i3, int i4, Entity entity5)
		{
			if (entity5.ridingEntity == null && entity5.riddenByEntity == null && entity5 is EntityPlayer && !world1.isRemote)
			{
				((EntityPlayer)entity5).travelToTheEnd(1);
			}

		}

		public override void randomDisplayTick(World world1, int i2, int i3, int i4, RandomExtended random5)
		{
			double d6 = (double)((float)i2 + random5.NextSingle());
			double d8 = (double)((float)i3 + 0.8F);
			double d10 = (double)((float)i4 + random5.NextSingle());
			double d12 = 0.0D;
			double d14 = 0.0D;
			double d16 = 0.0D;
			world1.spawnParticle("smoke", d6, d8, d10, d12, d14, d16);
		}

		public override int RenderType
		{
			get
			{
				return -1;
			}
		}

		public override void onBlockAdded(World world1, int i2, int i3, int i4)
		{
			if (!bossDefeated)
			{
				if (world1.worldProvider.worldType != 0)
				{
					world1.setBlockWithNotify(i2, i3, i4, 0);
				}
			}
		}
	}

}