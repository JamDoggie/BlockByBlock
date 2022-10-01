using BlockByBlock.java_extensions;
using System;
using System.Collections;

namespace net.minecraft.src
{

	public class BlockRedstoneTorch : BlockTorch
	{
		private bool torchActive = false;
		private static System.Collections.IList torchUpdates = new ArrayList();

		public override int getBlockTextureFromSideAndMetadata(int i1, int i2)
		{
			return i1 == 1 ? Block.redstoneWire.getBlockTextureFromSideAndMetadata(i1, i2) : base.getBlockTextureFromSideAndMetadata(i1, i2);
		}

		private bool checkForBurnout(World world1, int i2, int i3, int i4, bool z5)
		{
			if (z5)
			{
				torchUpdates.Add(new RedstoneUpdateInfo(i2, i3, i4, world1.WorldTime));
			}

			int i6 = 0;

			for (int i7 = 0; i7 < torchUpdates.Count; ++i7)
			{
				RedstoneUpdateInfo redstoneUpdateInfo8 = (RedstoneUpdateInfo)torchUpdates[i7];
				if (redstoneUpdateInfo8.x == i2 && redstoneUpdateInfo8.y == i3 && redstoneUpdateInfo8.z == i4)
				{
					++i6;
					if (i6 >= 8)
					{
						return true;
					}
				}
			}

			return false;
		}

		protected internal BlockRedstoneTorch(int i1, int i2, bool z3) : base(i1, i2)
		{
			this.torchActive = z3;
			this.TickRandomly = true;
		}

		public override int tickRate()
		{
			return 2;
		}

		public override void onBlockAdded(World world1, int i2, int i3, int i4)
		{
			if (world1.getBlockMetadata(i2, i3, i4) == 0)
			{
				base.onBlockAdded(world1, i2, i3, i4);
			}

			if (this.torchActive)
			{
				world1.notifyBlocksOfNeighborChange(i2, i3 - 1, i4, this.blockID);
				world1.notifyBlocksOfNeighborChange(i2, i3 + 1, i4, this.blockID);
				world1.notifyBlocksOfNeighborChange(i2 - 1, i3, i4, this.blockID);
				world1.notifyBlocksOfNeighborChange(i2 + 1, i3, i4, this.blockID);
				world1.notifyBlocksOfNeighborChange(i2, i3, i4 - 1, this.blockID);
				world1.notifyBlocksOfNeighborChange(i2, i3, i4 + 1, this.blockID);
			}

		}

		public override void onBlockRemoval(World world1, int i2, int i3, int i4)
		{
			if (this.torchActive)
			{
				world1.notifyBlocksOfNeighborChange(i2, i3 - 1, i4, this.blockID);
				world1.notifyBlocksOfNeighborChange(i2, i3 + 1, i4, this.blockID);
				world1.notifyBlocksOfNeighborChange(i2 - 1, i3, i4, this.blockID);
				world1.notifyBlocksOfNeighborChange(i2 + 1, i3, i4, this.blockID);
				world1.notifyBlocksOfNeighborChange(i2, i3, i4 - 1, this.blockID);
				world1.notifyBlocksOfNeighborChange(i2, i3, i4 + 1, this.blockID);
			}

		}

		public override bool isPoweringTo(IBlockAccess iBlockAccess1, int i2, int i3, int i4, int i5)
		{
			if (!this.torchActive)
			{
				return false;
			}
			else
			{
				int i6 = iBlockAccess1.getBlockMetadata(i2, i3, i4);
				return i6 == 5 && i5 == 1 ? false : (i6 == 3 && i5 == 3 ? false : (i6 == 4 && i5 == 2 ? false : (i6 == 1 && i5 == 5 ? false : i6 != 2 || i5 != 4)));
			}
		}

		private bool isIndirectlyPowered(World world1, int i2, int i3, int i4)
		{
			int i5 = world1.getBlockMetadata(i2, i3, i4);
			return i5 == 5 && world1.isBlockIndirectlyProvidingPowerTo(i2, i3 - 1, i4, 0) ? true : (i5 == 3 && world1.isBlockIndirectlyProvidingPowerTo(i2, i3, i4 - 1, 2) ? true : (i5 == 4 && world1.isBlockIndirectlyProvidingPowerTo(i2, i3, i4 + 1, 3) ? true : (i5 == 1 && world1.isBlockIndirectlyProvidingPowerTo(i2 - 1, i3, i4, 4) ? true : i5 == 2 && world1.isBlockIndirectlyProvidingPowerTo(i2 + 1, i3, i4, 5))));
		}

		public override void updateTick(World world1, int i2, int i3, int i4, RandomExtended random5)
		{
			bool z6 = this.isIndirectlyPowered(world1, i2, i3, i4);

			while (torchUpdates.Count > 0 && world1.WorldTime - ((RedstoneUpdateInfo)torchUpdates[0]).updateTime > 60L)
			{
				torchUpdates.RemoveAt(0);
			}

			if (this.torchActive)
			{
				if (z6)
				{
					world1.setBlockAndMetadataWithNotify(i2, i3, i4, Block.torchRedstoneIdle.blockID, world1.getBlockMetadata(i2, i3, i4));
					if (this.checkForBurnout(world1, i2, i3, i4, true))
					{
						world1.playSoundEffect((double)((float)i2 + 0.5F), (double)((float)i3 + 0.5F), (double)((float)i4 + 0.5F), "random.fizz", 0.5F, 2.6F + (world1.rand.NextSingle() - world1.rand.NextSingle()) * 0.8F);

						for (int i7 = 0; i7 < 5; ++i7)
						{
							double d8 = (double)i2 + random5.NextDouble() * 0.6D + 0.2D;
							double d10 = (double)i3 + random5.NextDouble() * 0.6D + 0.2D;
							double d12 = (double)i4 + random5.NextDouble() * 0.6D + 0.2D;
							world1.spawnParticle("smoke", d8, d10, d12, 0.0D, 0.0D, 0.0D);
						}
					}
				}
			}
			else if (!z6 && !this.checkForBurnout(world1, i2, i3, i4, false))
			{
				world1.setBlockAndMetadataWithNotify(i2, i3, i4, Block.torchRedstoneActive.blockID, world1.getBlockMetadata(i2, i3, i4));
			}

		}

		public override void onNeighborBlockChange(World world1, int i2, int i3, int i4, int i5)
		{
			base.onNeighborBlockChange(world1, i2, i3, i4, i5);
			world1.scheduleBlockUpdate(i2, i3, i4, this.blockID, this.tickRate());
		}

		public override bool isIndirectlyPoweringTo(World world1, int i2, int i3, int i4, int i5)
		{
			return i5 == 0 ? this.isPoweringTo(world1, i2, i3, i4, i5) : false;
		}

		public override int idDropped(int i1, RandomExtended random2, int i3)
		{
			return Block.torchRedstoneActive.blockID;
		}

		public override bool canProvidePower()
		{
			return true;
		}

		public override void randomDisplayTick(World world1, int i2, int i3, int i4, RandomExtended random5)
		{
			if (this.torchActive)
			{
				int i6 = world1.getBlockMetadata(i2, i3, i4);
				double d7 = (double)((float)i2 + 0.5F) + (double)(random5.NextSingle() - 0.5F) * 0.2D;
				double d9 = (double)((float)i3 + 0.7F) + (double)(random5.NextSingle() - 0.5F) * 0.2D;
				double d11 = (double)((float)i4 + 0.5F) + (double)(random5.NextSingle() - 0.5F) * 0.2D;
				double d13 = (double)0.22F;
				double d15 = (double)0.27F;
				if (i6 == 1)
				{
					world1.spawnParticle("reddust", d7 - d15, d9 + d13, d11, 0.0D, 0.0D, 0.0D);
				}
				else if (i6 == 2)
				{
					world1.spawnParticle("reddust", d7 + d15, d9 + d13, d11, 0.0D, 0.0D, 0.0D);
				}
				else if (i6 == 3)
				{
					world1.spawnParticle("reddust", d7, d9 + d13, d11 - d15, 0.0D, 0.0D, 0.0D);
				}
				else if (i6 == 4)
				{
					world1.spawnParticle("reddust", d7, d9 + d13, d11 + d15, 0.0D, 0.0D, 0.0D);
				}
				else
				{
					world1.spawnParticle("reddust", d7, d9, d11, 0.0D, 0.0D, 0.0D);
				}

			}
		}
	}

}