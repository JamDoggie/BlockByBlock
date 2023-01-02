using BlockByBlock.java_extensions;
using net.minecraft.client.entity;
using System;

namespace net.minecraft.src
{

    public class BlockCactus : Block
	{
		protected internal BlockCactus(int i1, int i2) : base(i1, i2, Material.cactus)
		{
			this.TickRandomly = true;
		}

		public override void updateTick(World world1, int i2, int i3, int i4, RandomExtended random5)
		{
			if (world1.isAirBlock(i2, i3 + 1, i4))
			{
				int i6;
				for (i6 = 1; world1.getBlockId(i2, i3 - i6, i4) == this.blockID; ++i6)
				{
				}

				if (i6 < 3)
				{
					int i7 = world1.getBlockMetadata(i2, i3, i4);
					if (i7 == 15)
					{
						world1.setBlockWithNotify(i2, i3 + 1, i4, this.blockID);
						world1.setBlockMetadataWithNotify(i2, i3, i4, 0);
					}
					else
					{
						world1.setBlockMetadataWithNotify(i2, i3, i4, i7 + 1);
					}
				}
			}

		}

		public override AxisAlignedBB getCollisionBoundingBoxFromPool(World world1, int i2, int i3, int i4)
		{
			float f5 = 0.0625F;
			return AxisAlignedBB.getBoundingBoxFromPool((double)((float)i2 + f5), (double)i3, (double)((float)i4 + f5), (double)((float)(i2 + 1) - f5), (double)((float)(i3 + 1) - f5), (double)((float)(i4 + 1) - f5));
		}

		public override AxisAlignedBB getSelectedBoundingBoxFromPool(World world1, int i2, int i3, int i4)
		{
			float f5 = 0.0625F;
			return AxisAlignedBB.getBoundingBoxFromPool((double)((float)i2 + f5), (double)i3, (double)((float)i4 + f5), (double)((float)(i2 + 1) - f5), (double)(i3 + 1), (double)((float)(i4 + 1) - f5));
		}

		public override int getBlockTextureFromSide(int i1)
		{
			return i1 == 1 ? this.blockIndexInTexture - 1 : (i1 == 0 ? this.blockIndexInTexture + 1 : this.blockIndexInTexture);
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

		public override int RenderType
		{
			get
			{
				return 13;
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
			if (world1.getBlockMaterial(i2 - 1, i3, i4).Solid)
			{
				return false;
			}
			else if (world1.getBlockMaterial(i2 + 1, i3, i4).Solid)
			{
				return false;
			}
			else if (world1.getBlockMaterial(i2, i3, i4 - 1).Solid)
			{
				return false;
			}
			else if (world1.getBlockMaterial(i2, i3, i4 + 1).Solid)
			{
				return false;
			}
			else
			{
				int i5 = world1.getBlockId(i2, i3 - 1, i4);
				return i5 == cactus.blockID || i5 == sand.blockID;
			}
		}

		public override void onEntityCollidedWithBlock(World world1, int i2, int i3, int i4, Entity entity5)
		{
			entity5.attackEntityFrom(DamageSource.cactus, 1);
		}
	}

}