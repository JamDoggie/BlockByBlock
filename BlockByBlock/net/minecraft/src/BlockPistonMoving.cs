using System;
using BlockByBlock.java_extensions;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class BlockPistonMoving : BlockContainer
	{
		public BlockPistonMoving(int i1) : base(i1, Material.piston)
		{
			this.Hardness = -1.0F;
		}

		public override TileEntity BlockEntity
		{
			get
			{
				return null;
			}
		}

		public override void onBlockAdded(World world1, int i2, int i3, int i4)
		{
		}

		public override void onBlockRemoval(World world1, int i2, int i3, int i4)
		{
			TileEntity tileEntity5 = world1.getBlockTileEntity(i2, i3, i4);
			if (tileEntity5 != null && tileEntity5 is TileEntityPiston)
			{
				((TileEntityPiston)tileEntity5).clearPistonTileEntity();
			}
			else
			{
				base.onBlockRemoval(world1, i2, i3, i4);
			}

		}

		public override bool canPlaceBlockAt(World world1, int i2, int i3, int i4)
		{
			return false;
		}

		public override bool canPlaceBlockOnSide(World world1, int i2, int i3, int i4, int i5)
		{
			return false;
		}

		public override int RenderType
		{
			get
			{
				return -1;
			}
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

		public override bool blockActivated(World world1, int i2, int i3, int i4, EntityPlayer entityPlayer5)
		{
			if (!world1.isRemote && world1.getBlockTileEntity(i2, i3, i4) == null)
			{
				world1.setBlockWithNotify(i2, i3, i4, 0);
				return true;
			}
			else
			{
				return false;
			}
		}

		public override int idDropped(int i1, RandomExtended random2, int i3)
		{
			return 0;
		}

		public override void dropBlockAsItemWithChance(World world1, int i2, int i3, int i4, int i5, float f6, int i7)
		{
			if (!world1.isRemote)
			{
				TileEntityPiston tileEntityPiston8 = this.getTileEntityAtLocation(world1, i2, i3, i4);
				if (tileEntityPiston8 != null)
				{
					Block.blocksList[tileEntityPiston8.StoredBlockID].dropBlockAsItem(world1, i2, i3, i4, tileEntityPiston8.BlockMetadata, 0);
				}
			}
		}

		public override void onNeighborBlockChange(World world1, int i2, int i3, int i4, int i5)
		{
			if (!world1.isRemote && world1.getBlockTileEntity(i2, i3, i4) == null)
			{
				;
			}

		}

		public static TileEntity getTileEntity(int i0, int i1, int i2, bool z3, bool z4)
		{
			return new TileEntityPiston(i0, i1, i2, z3, z4);
		}

		public override AxisAlignedBB getCollisionBoundingBoxFromPool(World world1, int i2, int i3, int i4)
		{
			TileEntityPiston tileEntityPiston5 = this.getTileEntityAtLocation(world1, i2, i3, i4);
			if (tileEntityPiston5 == null)
			{
				return null;
			}
			else
			{
				float f6 = tileEntityPiston5.getProgress(0.0F);
				if (tileEntityPiston5.Extending)
				{
					f6 = 1.0F - f6;
				}

				return this.getAxisAlignedBB(world1, i2, i3, i4, tileEntityPiston5.StoredBlockID, f6, tileEntityPiston5.PistonOrientation);
			}
		}

		public override void setBlockBoundsBasedOnState(IBlockAccess iBlockAccess1, int i2, int i3, int i4)
		{
			TileEntityPiston tileEntityPiston5 = this.getTileEntityAtLocation(iBlockAccess1, i2, i3, i4);
			if (tileEntityPiston5 != null)
			{
				Block block6 = Block.blocksList[tileEntityPiston5.StoredBlockID];
				if (block6 == null || block6 == this)
				{
					return;
				}

				block6.setBlockBoundsBasedOnState(iBlockAccess1, i2, i3, i4);
				float f7 = tileEntityPiston5.getProgress(0.0F);
				if (tileEntityPiston5.Extending)
				{
					f7 = 1.0F - f7;
				}

				int i8 = tileEntityPiston5.PistonOrientation;
				this.minX = block6.minX - (double)((float)Facing.offsetsXForSide[i8] * f7);
				this.minY = block6.minY - (double)((float)Facing.offsetsYForSide[i8] * f7);
				this.minZ = block6.minZ - (double)((float)Facing.offsetsZForSide[i8] * f7);
				this.maxX = block6.maxX - (double)((float)Facing.offsetsXForSide[i8] * f7);
				this.maxY = block6.maxY - (double)((float)Facing.offsetsYForSide[i8] * f7);
				this.maxZ = block6.maxZ - (double)((float)Facing.offsetsZForSide[i8] * f7);
			}

		}

		public virtual AxisAlignedBB getAxisAlignedBB(World world1, int i2, int i3, int i4, int i5, float f6, int i7)
		{
			if (i5 != 0 && i5 != this.blockID)
			{
				AxisAlignedBB axisAlignedBB8 = Block.blocksList[i5].getCollisionBoundingBoxFromPool(world1, i2, i3, i4);
				if (axisAlignedBB8 == null)
				{
					return null;
				}
				else
				{
					if (Facing.offsetsXForSide[i7] < 0)
					{
						axisAlignedBB8.minX -= (double)((float)Facing.offsetsXForSide[i7] * f6);
					}
					else
					{
						axisAlignedBB8.maxX -= (double)((float)Facing.offsetsXForSide[i7] * f6);
					}

					if (Facing.offsetsYForSide[i7] < 0)
					{
						axisAlignedBB8.minY -= (double)((float)Facing.offsetsYForSide[i7] * f6);
					}
					else
					{
						axisAlignedBB8.maxY -= (double)((float)Facing.offsetsYForSide[i7] * f6);
					}

					if (Facing.offsetsZForSide[i7] < 0)
					{
						axisAlignedBB8.minZ -= (double)((float)Facing.offsetsZForSide[i7] * f6);
					}
					else
					{
						axisAlignedBB8.maxZ -= (double)((float)Facing.offsetsZForSide[i7] * f6);
					}

					return axisAlignedBB8;
				}
			}
			else
			{
				return null;
			}
		}

		private TileEntityPiston getTileEntityAtLocation(IBlockAccess iBlockAccess1, int i2, int i3, int i4)
		{
			TileEntity tileEntity5 = iBlockAccess1.getBlockTileEntity(i2, i3, i4);
			return tileEntity5 != null && tileEntity5 is TileEntityPiston ? (TileEntityPiston)tileEntity5 : null;
		}
	}

}