using BlockByBlock.java_extensions;
using net.minecraft.client.entity;
using System;

namespace net.minecraft.src
{

    public class BlockDragonEgg : Block
	{
		public BlockDragonEgg(int i1, int i2) : base(i1, i2, Material.dragonEgg)
		{
		}

		public override void onBlockAdded(World world1, int i2, int i3, int i4)
		{
			world1.scheduleBlockUpdate(i2, i3, i4, this.blockID, this.tickRate());
		}

		public override void onNeighborBlockChange(World world1, int i2, int i3, int i4, int i5)
		{
			world1.scheduleBlockUpdate(i2, i3, i4, this.blockID, this.tickRate());
		}

		public override void updateTick(World world1, int i2, int i3, int i4, RandomExtended random5)
		{
			this.fallIfPossible(world1, i2, i3, i4);
		}

		private void fallIfPossible(World world1, int i2, int i3, int i4)
		{
			if (BlockSand.canFallBelow(world1, i2, i3 - 1, i4) && i3 >= 0)
			{
				sbyte b8 = 32;
				if (!BlockSand.fallInstantly && world1.checkChunksExist(i2 - b8, i3 - b8, i4 - b8, i2 + b8, i3 + b8, i4 + b8))
				{
					EntityFallingSand entityFallingSand9 = new EntityFallingSand(world1, (double)((float)i2 + 0.5F), (double)((float)i3 + 0.5F), (double)((float)i4 + 0.5F), this.blockID);
					world1.spawnEntityInWorld(entityFallingSand9);
				}
				else
				{
					world1.setBlockWithNotify(i2, i3, i4, 0);

					while (BlockSand.canFallBelow(world1, i2, i3 - 1, i4) && i3 > 0)
					{
						--i3;
					}

					if (i3 > 0)
					{
						world1.setBlockWithNotify(i2, i3, i4, this.blockID);
					}
				}
			}

		}

		public override bool blockActivated(World world1, int i2, int i3, int i4, EntityPlayer entityPlayer5)
		{
			this.teleportNearby(world1, i2, i3, i4);
			return true;
		}

		public override void onBlockClicked(World world1, int i2, int i3, int i4, EntityPlayer entityPlayer5)
		{
			this.teleportNearby(world1, i2, i3, i4);
		}

		private void teleportNearby(World world1, int i2, int i3, int i4)
		{
			if (world1.getBlockId(i2, i3, i4) == this.blockID)
			{
				if (!world1.isRemote)
				{
					for (int i5 = 0; i5 < 1000; ++i5)
					{
						int i6 = i2 + world1.rand.Next(16) - world1.rand.Next(16);
						int i7 = i3 + world1.rand.Next(8) - world1.rand.Next(8);
						int i8 = i4 + world1.rand.Next(16) - world1.rand.Next(16);
						if (world1.getBlockId(i6, i7, i8) == 0)
						{
							world1.setBlockAndMetadataWithNotify(i6, i7, i8, this.blockID, world1.getBlockMetadata(i2, i3, i4));
							world1.setBlockWithNotify(i2, i3, i4, 0);
							short s9 = 128;

							for (int i10 = 0; i10 < s9; ++i10)
							{
								double d11 = world1.rand.NextDouble();
								float f13 = (world1.rand.NextSingle() - 0.5F) * 0.2F;
								float f14 = (world1.rand.NextSingle() - 0.5F) * 0.2F;
								float f15 = (world1.rand.NextSingle() - 0.5F) * 0.2F;
								double d16 = (double)i6 + (double)(i2 - i6) * d11 + (world1.rand.NextDouble() - 0.5D) * 1.0D + 0.5D;
								double d18 = (double)i7 + (double)(i3 - i7) * d11 + world1.rand.NextDouble() * 1.0D - 0.5D;
								double d20 = (double)i8 + (double)(i4 - i8) * d11 + (world1.rand.NextDouble() - 0.5D) * 1.0D + 0.5D;
								world1.spawnParticle("portal", d16, d18, d20, (double)f13, (double)f14, (double)f15);
							}

							return;
						}
					}

				}
			}
		}

		public override int tickRate()
		{
			return 3;
		}

		public override bool canPlaceBlockAt(World world1, int i2, int i3, int i4)
		{
			return base.canPlaceBlockAt(world1, i2, i3, i4);
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
				return 27;
			}
		}
	}

}