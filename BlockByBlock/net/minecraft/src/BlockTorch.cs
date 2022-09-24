using BlockByBlock.java_extensions;
using System;

namespace net.minecraft.src
{

	public class BlockTorch : Block
	{
		protected internal BlockTorch(int i1, int i2) : base(i1, i2, Material.circuits)
		{
			this.TickRandomly = true;
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
				return 2;
			}
		}

		private bool canPlaceTorchOn(World world1, int i2, int i3, int i4)
		{
			if (world1.isBlockNormalCubeDefault(i2, i3, i4, true))
			{
				return true;
			}
			else
			{
				int i5 = world1.getBlockId(i2, i3, i4);
				if (i5 != Block.fence.blockID && i5 != Block.netherFence.blockID && i5 != Block.glass.blockID)
				{
					if (Block.blocksList[i5] != null && Block.blocksList[i5] is BlockStairs)
					{
						int i6 = world1.getBlockMetadata(i2, i3, i4);
						if ((4 & i6) != 0)
						{
							return true;
						}
					}

					return false;
				}
				else
				{
					return true;
				}
			}
		}

		public override bool canPlaceBlockAt(World world1, int i2, int i3, int i4)
		{
			return world1.isBlockNormalCubeDefault(i2 - 1, i3, i4, true) ? true : (world1.isBlockNormalCubeDefault(i2 + 1, i3, i4, true) ? true : (world1.isBlockNormalCubeDefault(i2, i3, i4 - 1, true) ? true : (world1.isBlockNormalCubeDefault(i2, i3, i4 + 1, true) ? true : this.canPlaceTorchOn(world1, i2, i3 - 1, i4))));
		}

		public override void onBlockPlaced(World world1, int i2, int i3, int i4, int i5)
		{
			int i6 = world1.getBlockMetadata(i2, i3, i4);
			if (i5 == 1 && this.canPlaceTorchOn(world1, i2, i3 - 1, i4))
			{
				i6 = 5;
			}

			if (i5 == 2 && world1.isBlockNormalCubeDefault(i2, i3, i4 + 1, true))
			{
				i6 = 4;
			}

			if (i5 == 3 && world1.isBlockNormalCubeDefault(i2, i3, i4 - 1, true))
			{
				i6 = 3;
			}

			if (i5 == 4 && world1.isBlockNormalCubeDefault(i2 + 1, i3, i4, true))
			{
				i6 = 2;
			}

			if (i5 == 5 && world1.isBlockNormalCubeDefault(i2 - 1, i3, i4, true))
			{
				i6 = 1;
			}

			world1.setBlockMetadataWithNotify(i2, i3, i4, i6);
		}

		public override void updateTick(World world1, int i2, int i3, int i4, RandomExtended random5)
		{
			base.updateTick(world1, i2, i3, i4, random5);
			if (world1.getBlockMetadata(i2, i3, i4) == 0)
			{
				this.onBlockAdded(world1, i2, i3, i4);
			}

		}

		public override void onBlockAdded(World world1, int i2, int i3, int i4)
		{
			if (world1.isBlockNormalCubeDefault(i2 - 1, i3, i4, true))
			{
				world1.setBlockMetadataWithNotify(i2, i3, i4, 1);
			}
			else if (world1.isBlockNormalCubeDefault(i2 + 1, i3, i4, true))
			{
				world1.setBlockMetadataWithNotify(i2, i3, i4, 2);
			}
			else if (world1.isBlockNormalCubeDefault(i2, i3, i4 - 1, true))
			{
				world1.setBlockMetadataWithNotify(i2, i3, i4, 3);
			}
			else if (world1.isBlockNormalCubeDefault(i2, i3, i4 + 1, true))
			{
				world1.setBlockMetadataWithNotify(i2, i3, i4, 4);
			}
			else if (this.canPlaceTorchOn(world1, i2, i3 - 1, i4))
			{
				world1.setBlockMetadataWithNotify(i2, i3, i4, 5);
			}

			this.dropTorchIfCantStay(world1, i2, i3, i4);
		}

		public override void onNeighborBlockChange(World world1, int i2, int i3, int i4, int i5)
		{
			if (this.dropTorchIfCantStay(world1, i2, i3, i4))
			{
				int i6 = world1.getBlockMetadata(i2, i3, i4);
				bool z7 = false;
				if (!world1.isBlockNormalCubeDefault(i2 - 1, i3, i4, true) && i6 == 1)
				{
					z7 = true;
				}

				if (!world1.isBlockNormalCubeDefault(i2 + 1, i3, i4, true) && i6 == 2)
				{
					z7 = true;
				}

				if (!world1.isBlockNormalCubeDefault(i2, i3, i4 - 1, true) && i6 == 3)
				{
					z7 = true;
				}

				if (!world1.isBlockNormalCubeDefault(i2, i3, i4 + 1, true) && i6 == 4)
				{
					z7 = true;
				}

				if (!this.canPlaceTorchOn(world1, i2, i3 - 1, i4) && i6 == 5)
				{
					z7 = true;
				}

				if (z7)
				{
					this.dropBlockAsItem(world1, i2, i3, i4, world1.getBlockMetadata(i2, i3, i4), 0);
					world1.setBlockWithNotify(i2, i3, i4, 0);
				}
			}

		}

		private bool dropTorchIfCantStay(World world1, int i2, int i3, int i4)
		{
			if (!this.canPlaceBlockAt(world1, i2, i3, i4))
			{
				if (world1.getBlockId(i2, i3, i4) == this.blockID)
				{
					this.dropBlockAsItem(world1, i2, i3, i4, world1.getBlockMetadata(i2, i3, i4), 0);
					world1.setBlockWithNotify(i2, i3, i4, 0);
				}

				return false;
			}
			else
			{
				return true;
			}
		}

		public override MovingObjectPosition collisionRayTrace(World world1, int i2, int i3, int i4, Vec3D vec3D5, Vec3D vec3D6)
		{
			int i7 = world1.getBlockMetadata(i2, i3, i4) & 7;
			float f8 = 0.15F;
			if (i7 == 1)
			{
				this.setBlockBounds(0.0F, 0.2F, 0.5F - f8, f8 * 2.0F, 0.8F, 0.5F + f8);
			}
			else if (i7 == 2)
			{
				this.setBlockBounds(1.0F - f8 * 2.0F, 0.2F, 0.5F - f8, 1.0F, 0.8F, 0.5F + f8);
			}
			else if (i7 == 3)
			{
				this.setBlockBounds(0.5F - f8, 0.2F, 0.0F, 0.5F + f8, 0.8F, f8 * 2.0F);
			}
			else if (i7 == 4)
			{
				this.setBlockBounds(0.5F - f8, 0.2F, 1.0F - f8 * 2.0F, 0.5F + f8, 0.8F, 1.0F);
			}
			else
			{
				f8 = 0.1F;
				this.setBlockBounds(0.5F - f8, 0.0F, 0.5F - f8, 0.5F + f8, 0.6F, 0.5F + f8);
			}

			return base.collisionRayTrace(world1, i2, i3, i4, vec3D5, vec3D6);
		}

		public override void randomDisplayTick(World world1, int i2, int i3, int i4, RandomExtended random5)
		{
			int i6 = world1.getBlockMetadata(i2, i3, i4);
			double d7 = (double)((float)i2 + 0.5F);
			double d9 = (double)((float)i3 + 0.7F);
			double d11 = (double)((float)i4 + 0.5F);
			double d13 = (double)0.22F;
			double d15 = (double)0.27F;
			if (i6 == 1)
			{
				world1.spawnParticle("smoke", d7 - d15, d9 + d13, d11, 0.0D, 0.0D, 0.0D);
				world1.spawnParticle("flame", d7 - d15, d9 + d13, d11, 0.0D, 0.0D, 0.0D);
			}
			else if (i6 == 2)
			{
				world1.spawnParticle("smoke", d7 + d15, d9 + d13, d11, 0.0D, 0.0D, 0.0D);
				world1.spawnParticle("flame", d7 + d15, d9 + d13, d11, 0.0D, 0.0D, 0.0D);
			}
			else if (i6 == 3)
			{
				world1.spawnParticle("smoke", d7, d9 + d13, d11 - d15, 0.0D, 0.0D, 0.0D);
				world1.spawnParticle("flame", d7, d9 + d13, d11 - d15, 0.0D, 0.0D, 0.0D);
			}
			else if (i6 == 4)
			{
				world1.spawnParticle("smoke", d7, d9 + d13, d11 + d15, 0.0D, 0.0D, 0.0D);
				world1.spawnParticle("flame", d7, d9 + d13, d11 + d15, 0.0D, 0.0D, 0.0D);
			}
			else
			{
				world1.spawnParticle("smoke", d7, d9, d11, 0.0D, 0.0D, 0.0D);
				world1.spawnParticle("flame", d7, d9, d11, 0.0D, 0.0D, 0.0D);
			}

		}
	}

}