using System;
using BlockByBlock.java_extensions;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class BlockRedstoneRepeater : BlockDirectional
	{
		public static readonly double[] repeaterTorchOffset = new double[]{-0.0625D, 0.0625D, 0.1875D, 0.3125D};
		private static readonly int[] repeaterState = new int[]{1, 2, 3, 4};
		private readonly bool isRepeaterPowered;

		protected internal BlockRedstoneRepeater(int i1, bool z2) : base(i1, 6, Material.circuits)
		{
			this.isRepeaterPowered = z2;
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.125F, 1.0F);
		}

		public override bool renderAsNormalBlock()
		{
			return false;
		}

		public override bool canPlaceBlockAt(World world1, int i2, int i3, int i4)
		{
			return !world1.isBlockNormalCube(i2, i3 - 1, i4) ? false : base.canPlaceBlockAt(world1, i2, i3, i4);
		}

		public override bool canBlockStay(World world1, int i2, int i3, int i4)
		{
			return !world1.isBlockNormalCube(i2, i3 - 1, i4) ? false : base.canBlockStay(world1, i2, i3, i4);
		}

		public override void updateTick(World world1, int i2, int i3, int i4, RandomExtended random5)
		{
			int i6 = world1.getBlockMetadata(i2, i3, i4);
			bool z7 = this.ignoreTick(world1, i2, i3, i4, i6);
			if (this.isRepeaterPowered && !z7)
			{
				world1.setBlockAndMetadataWithNotify(i2, i3, i4, Block.redstoneRepeaterIdle.blockID, i6);
			}
			else if (!this.isRepeaterPowered)
			{
				world1.setBlockAndMetadataWithNotify(i2, i3, i4, Block.redstoneRepeaterActive.blockID, i6);
				if (!z7)
				{
					int i8 = (i6 & 12) >> 2;
					world1.scheduleBlockUpdate(i2, i3, i4, Block.redstoneRepeaterActive.blockID, repeaterState[i8] * 2);
				}
			}

		}

		public override int getBlockTextureFromSideAndMetadata(int i1, int i2)
		{
			return i1 == 0 ? (this.isRepeaterPowered ? 99 : 115) : (i1 == 1 ? (this.isRepeaterPowered ? 147 : 131) : 5);
		}

		public override bool shouldSideBeRendered(IBlockAccess iBlockAccess1, int i2, int i3, int i4, int i5)
		{
			return i5 != 0 && i5 != 1;
		}

		public override int RenderType
		{
			get
			{
				return 15;
			}
		}

		public override int getBlockTextureFromSide(int i1)
		{
			return this.getBlockTextureFromSideAndMetadata(i1, 0);
		}

		public override bool isIndirectlyPoweringTo(World world1, int i2, int i3, int i4, int i5)
		{
			return this.isPoweringTo(world1, i2, i3, i4, i5);
		}

		public override bool isPoweringTo(IBlockAccess iBlockAccess1, int i2, int i3, int i4, int i5)
		{
			if (!this.isRepeaterPowered)
			{
				return false;
			}
			else
			{
				int i6 = getDirection(iBlockAccess1.getBlockMetadata(i2, i3, i4));
				return i6 == 0 && i5 == 3 ? true : (i6 == 1 && i5 == 4 ? true : (i6 == 2 && i5 == 2 ? true : i6 == 3 && i5 == 5));
			}
		}

		public override void onNeighborBlockChange(World world1, int i2, int i3, int i4, int i5)
		{
			if (!this.canBlockStay(world1, i2, i3, i4))
			{
				this.dropBlockAsItem(world1, i2, i3, i4, world1.getBlockMetadata(i2, i3, i4), 0);
				world1.setBlockWithNotify(i2, i3, i4, 0);
				world1.notifyBlocksOfNeighborChange(i2 + 1, i3, i4, this.blockID);
				world1.notifyBlocksOfNeighborChange(i2 - 1, i3, i4, this.blockID);
				world1.notifyBlocksOfNeighborChange(i2, i3, i4 + 1, this.blockID);
				world1.notifyBlocksOfNeighborChange(i2, i3, i4 - 1, this.blockID);
				world1.notifyBlocksOfNeighborChange(i2, i3 - 1, i4, this.blockID);
				world1.notifyBlocksOfNeighborChange(i2, i3 + 1, i4, this.blockID);
			}
			else
			{
				int i6 = world1.getBlockMetadata(i2, i3, i4);
				bool z7 = this.ignoreTick(world1, i2, i3, i4, i6);
				int i8 = (i6 & 12) >> 2;
				if (this.isRepeaterPowered && !z7)
				{
					world1.scheduleBlockUpdate(i2, i3, i4, this.blockID, repeaterState[i8] * 2);
				}
				else if (!this.isRepeaterPowered && z7)
				{
					world1.scheduleBlockUpdate(i2, i3, i4, this.blockID, repeaterState[i8] * 2);
				}

			}
		}

		private bool ignoreTick(World world1, int i2, int i3, int i4, int i5)
		{
			int i6 = getDirection(i5);
			switch (i6)
			{
			case 0:
				return world1.isBlockIndirectlyProvidingPowerTo(i2, i3, i4 + 1, 3) || world1.getBlockId(i2, i3, i4 + 1) == Block.redstoneWire.blockID && world1.getBlockMetadata(i2, i3, i4 + 1) > 0;
			case 1:
				return world1.isBlockIndirectlyProvidingPowerTo(i2 - 1, i3, i4, 4) || world1.getBlockId(i2 - 1, i3, i4) == Block.redstoneWire.blockID && world1.getBlockMetadata(i2 - 1, i3, i4) > 0;
			case 2:
				return world1.isBlockIndirectlyProvidingPowerTo(i2, i3, i4 - 1, 2) || world1.getBlockId(i2, i3, i4 - 1) == Block.redstoneWire.blockID && world1.getBlockMetadata(i2, i3, i4 - 1) > 0;
			case 3:
				return world1.isBlockIndirectlyProvidingPowerTo(i2 + 1, i3, i4, 5) || world1.getBlockId(i2 + 1, i3, i4) == Block.redstoneWire.blockID && world1.getBlockMetadata(i2 + 1, i3, i4) > 0;
			default:
				return false;
			}
		}

		public override bool blockActivated(World world1, int i2, int i3, int i4, EntityPlayer entityPlayer5)
		{
			int i6 = world1.getBlockMetadata(i2, i3, i4);
			int i7 = (i6 & 12) >> 2;
			i7 = i7 + 1 << 2 & 12;
			world1.setBlockMetadataWithNotify(i2, i3, i4, i7 | i6 & 3);
			return true;
		}

		public override bool canProvidePower()
		{
			return true;
		}

		public override void onBlockPlacedBy(World world1, int i2, int i3, int i4, EntityLiving entityLiving5)
		{
			int i6 = ((MathHelper.floor_double((double)(entityLiving5.rotationYaw * 4.0F / 360.0F) + 0.5D) & 3) + 2) % 4;
			world1.setBlockMetadataWithNotify(i2, i3, i4, i6);
			bool z7 = this.ignoreTick(world1, i2, i3, i4, i6);
			if (z7)
			{
				world1.scheduleBlockUpdate(i2, i3, i4, this.blockID, 1);
			}

		}

		public override void onBlockAdded(World world1, int i2, int i3, int i4)
		{
			world1.notifyBlocksOfNeighborChange(i2 + 1, i3, i4, this.blockID);
			world1.notifyBlocksOfNeighborChange(i2 - 1, i3, i4, this.blockID);
			world1.notifyBlocksOfNeighborChange(i2, i3, i4 + 1, this.blockID);
			world1.notifyBlocksOfNeighborChange(i2, i3, i4 - 1, this.blockID);
			world1.notifyBlocksOfNeighborChange(i2, i3 - 1, i4, this.blockID);
			world1.notifyBlocksOfNeighborChange(i2, i3 + 1, i4, this.blockID);
		}

		public override void onBlockDestroyedByPlayer(World world1, int i2, int i3, int i4, int i5)
		{
			if (this.isRepeaterPowered)
			{
				world1.notifyBlocksOfNeighborChange(i2 + 1, i3, i4, this.blockID);
				world1.notifyBlocksOfNeighborChange(i2 - 1, i3, i4, this.blockID);
				world1.notifyBlocksOfNeighborChange(i2, i3, i4 + 1, this.blockID);
				world1.notifyBlocksOfNeighborChange(i2, i3, i4 - 1, this.blockID);
				world1.notifyBlocksOfNeighborChange(i2, i3 - 1, i4, this.blockID);
				world1.notifyBlocksOfNeighborChange(i2, i3 + 1, i4, this.blockID);
			}

			base.onBlockDestroyedByPlayer(world1, i2, i3, i4, i5);
		}

		public override bool OpaqueCube
		{
			get
			{
				return false;
			}
		}

		public override int idDropped(int i1, RandomExtended random2, int i3)
		{
			return Item.redstoneRepeater.shiftedIndex;
		}

		public override void randomDisplayTick(World world1, int i2, int i3, int i4, RandomExtended random5)
		{
			if (this.isRepeaterPowered)
			{
				int i6 = world1.getBlockMetadata(i2, i3, i4);
				int i7 = getDirection(i6);
				double d8 = (double)((float)i2 + 0.5F) + (double)(random5.NextSingle() - 0.5F) * 0.2D;
				double d10 = (double)((float)i3 + 0.4F) + (double)(random5.NextSingle() - 0.5F) * 0.2D;
				double d12 = (double)((float)i4 + 0.5F) + (double)(random5.NextSingle() - 0.5F) * 0.2D;
				double d14 = 0.0D;
				double d16 = 0.0D;
				if (random5.Next(2) == 0)
				{
					switch (i7)
					{
					case 0:
						d16 = -0.3125D;
						break;
					case 1:
						d14 = 0.3125D;
						break;
					case 2:
						d16 = 0.3125D;
						break;
					case 3:
						d14 = -0.3125D;
					break;
					}
				}
				else
				{
					int i18 = (i6 & 12) >> 2;
					switch (i7)
					{
					case 0:
						d16 = repeaterTorchOffset[i18];
						break;
					case 1:
						d14 = -repeaterTorchOffset[i18];
						break;
					case 2:
						d16 = -repeaterTorchOffset[i18];
						break;
					case 3:
						d14 = repeaterTorchOffset[i18];
					break;
					}
				}

				world1.spawnParticle("reddust", d8 + d14, d10, d12 + d16, 0.0D, 0.0D, 0.0D);
			}
		}
	}

}