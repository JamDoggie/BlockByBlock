using BlockByBlock.java_extensions;
using System;

namespace net.minecraft.src
{

	public class BlockLadder : Block
	{
		protected internal BlockLadder(int i1, int i2) : base(i1, i2, Material.circuits)
		{
		}

		public override AxisAlignedBB getCollisionBoundingBoxFromPool(World world1, int i2, int i3, int i4)
		{
			int i5 = world1.getBlockMetadata(i2, i3, i4);
			float f6 = 0.125F;
			if (i5 == 2)
			{
				this.setBlockBounds(0.0F, 0.0F, 1.0F - f6, 1.0F, 1.0F, 1.0F);
			}

			if (i5 == 3)
			{
				this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, f6);
			}

			if (i5 == 4)
			{
				this.setBlockBounds(1.0F - f6, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
			}

			if (i5 == 5)
			{
				this.setBlockBounds(0.0F, 0.0F, 0.0F, f6, 1.0F, 1.0F);
			}

			return base.getCollisionBoundingBoxFromPool(world1, i2, i3, i4);
		}

		public override AxisAlignedBB getSelectedBoundingBoxFromPool(World world1, int i2, int i3, int i4)
		{
			int i5 = world1.getBlockMetadata(i2, i3, i4);
			float f6 = 0.125F;
			if (i5 == 2)
			{
				this.setBlockBounds(0.0F, 0.0F, 1.0F - f6, 1.0F, 1.0F, 1.0F);
			}

			if (i5 == 3)
			{
				this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, f6);
			}

			if (i5 == 4)
			{
				this.setBlockBounds(1.0F - f6, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
			}

			if (i5 == 5)
			{
				this.setBlockBounds(0.0F, 0.0F, 0.0F, f6, 1.0F, 1.0F);
			}

			return base.getSelectedBoundingBoxFromPool(world1, i2, i3, i4);
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
				return 8;
			}
		}

		public override bool canPlaceBlockAt(World world1, int i2, int i3, int i4)
		{
			return world1.isBlockNormalCube(i2 - 1, i3, i4) ? true : (world1.isBlockNormalCube(i2 + 1, i3, i4) ? true : (world1.isBlockNormalCube(i2, i3, i4 - 1) ? true : world1.isBlockNormalCube(i2, i3, i4 + 1)));
		}

		public override void onBlockPlaced(World world1, int i2, int i3, int i4, int i5)
		{
			int i6 = world1.getBlockMetadata(i2, i3, i4);
			if ((i6 == 0 || i5 == 2) && world1.isBlockNormalCube(i2, i3, i4 + 1))
			{
				i6 = 2;
			}

			if ((i6 == 0 || i5 == 3) && world1.isBlockNormalCube(i2, i3, i4 - 1))
			{
				i6 = 3;
			}

			if ((i6 == 0 || i5 == 4) && world1.isBlockNormalCube(i2 + 1, i3, i4))
			{
				i6 = 4;
			}

			if ((i6 == 0 || i5 == 5) && world1.isBlockNormalCube(i2 - 1, i3, i4))
			{
				i6 = 5;
			}

			world1.setBlockMetadataWithNotify(i2, i3, i4, i6);
		}

		public override void onNeighborBlockChange(World world1, int i2, int i3, int i4, int i5)
		{
			int i6 = world1.getBlockMetadata(i2, i3, i4);
			bool z7 = false;
			if (i6 == 2 && world1.isBlockNormalCube(i2, i3, i4 + 1))
			{
				z7 = true;
			}

			if (i6 == 3 && world1.isBlockNormalCube(i2, i3, i4 - 1))
			{
				z7 = true;
			}

			if (i6 == 4 && world1.isBlockNormalCube(i2 + 1, i3, i4))
			{
				z7 = true;
			}

			if (i6 == 5 && world1.isBlockNormalCube(i2 - 1, i3, i4))
			{
				z7 = true;
			}

			if (!z7)
			{
				this.dropBlockAsItem(world1, i2, i3, i4, i6, 0);
				world1.setBlockWithNotify(i2, i3, i4, 0);
			}

			base.onNeighborBlockChange(world1, i2, i3, i4, i5);
		}
        
		public override int quantityDropped(RandomExtended random1)
		{
			return 1;
		}
	}

}