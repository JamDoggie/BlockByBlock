using BlockByBlock.java_extensions;
using System;

namespace net.minecraft.src
{

	public class BlockFlower : Block
	{
		protected internal BlockFlower(int i1, int i2, Material material3) : base(i1, material3)
		{
			this.blockIndexInTexture = i2;
			this.TickRandomly = true;
			float f4 = 0.2F;
			this.setBlockBounds(0.5F - f4, 0.0F, 0.5F - f4, 0.5F + f4, f4 * 3.0F, 0.5F + f4);
		}

		protected internal BlockFlower(int i1, int i2) : this(i1, i2, Material.plants)
		{
		}

		public override bool canPlaceBlockAt(World world1, int i2, int i3, int i4)
		{
			return base.canPlaceBlockAt(world1, i2, i3, i4) && this.canThisPlantGrowOnThisBlockID(world1.getBlockId(i2, i3 - 1, i4));
		}

		protected internal virtual bool canThisPlantGrowOnThisBlockID(int i1)
		{
			return i1 == Block.grass.blockID || i1 == Block.dirt.blockID || i1 == Block.tilledField.blockID;
		}

		public override void onNeighborBlockChange(World world1, int i2, int i3, int i4, int i5)
		{
			base.onNeighborBlockChange(world1, i2, i3, i4, i5);
			this.checkFlowerChange(world1, i2, i3, i4);
		}

		public override void updateTick(World world1, int i2, int i3, int i4, RandomExtended random5)
		{
			this.checkFlowerChange(world1, i2, i3, i4);
		}

		protected internal void checkFlowerChange(World world1, int i2, int i3, int i4)
		{
			if (!this.canBlockStay(world1, i2, i3, i4))
			{
				this.dropBlockAsItem(world1, i2, i3, i4, world1.getBlockMetadata(i2, i3, i4), 0);
				world1.setBlockWithNotify(i2, i3, i4, 0);
			}

		}

		public override bool canBlockStay(World world1, int i2, int i3, int i4)
		{
			return (world1.getFullBlockLightValue(i2, i3, i4) >= 8 || world1.canBlockSeeTheSky(i2, i3, i4)) && this.canThisPlantGrowOnThisBlockID(world1.getBlockId(i2, i3 - 1, i4));
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

		public override int RenderType
		{
			get
			{
				return 1;
			}
		}
	}

}