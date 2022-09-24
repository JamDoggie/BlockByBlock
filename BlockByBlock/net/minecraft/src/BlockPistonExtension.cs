using System;
using System.Collections;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class BlockPistonExtension : Block
	{
		private int headTexture = -1;

		public BlockPistonExtension(int i1, int i2) : base(i1, i2, Material.piston)
		{
			setStepSound(soundStoneFootstep);
			Hardness = 0.5F;
		}

		public virtual int HeadTexture
		{
			set
			{
				this.headTexture = value;
			}
		}

		public virtual void clearHeadTexture()
		{
			this.headTexture = -1;
		}

		public override void onBlockRemoval(World world1, int i2, int i3, int i4)
		{
			base.onBlockRemoval(world1, i2, i3, i4);
			int i5 = world1.getBlockMetadata(i2, i3, i4);
			int i6 = Facing.faceToSide[getDirectionMeta(i5)];
			i2 += Facing.offsetsXForSide[i6];
			i3 += Facing.offsetsYForSide[i6];
			i4 += Facing.offsetsZForSide[i6];
			int i7 = world1.getBlockId(i2, i3, i4);
			if (i7 == Block.pistonBase.blockID || i7 == Block.pistonStickyBase.blockID)
			{
				i5 = world1.getBlockMetadata(i2, i3, i4);
				if (BlockPistonBase.isExtended(i5))
				{
					Block.blocksList[i7].dropBlockAsItem(world1, i2, i3, i4, i5, 0);
					world1.setBlockWithNotify(i2, i3, i4, 0);
				}
			}

		}

		public override int getBlockTextureFromSideAndMetadata(int i1, int i2)
		{
			int i3 = getDirectionMeta(i2);
			return i1 == i3 ? (this.headTexture >= 0 ? this.headTexture : ((i2 & 8) != 0 ? this.blockIndexInTexture - 1 : this.blockIndexInTexture)) : (i1 == Facing.faceToSide[i3] ? 107 : 108);
		}

		public override int RenderType
		{
			get
			{
				return 17;
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

		public override bool canPlaceBlockAt(World world1, int i2, int i3, int i4)
		{
			return false;
		}

		public override bool canPlaceBlockOnSide(World world1, int i2, int i3, int i4, int i5)
		{
			return false;
		}

		public override int quantityDropped(RandomExtended random1)
		{
			return 0;
		}

		public override void getCollidingBoundingBoxes(World world1, int i2, int i3, int i4, AxisAlignedBB axisAlignedBB5, ArrayList arrayList6)
		{
			int i7 = world1.getBlockMetadata(i2, i3, i4);
			switch (getDirectionMeta(i7))
			{
			case 0:
				this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.25F, 1.0F);
				base.getCollidingBoundingBoxes(world1, i2, i3, i4, axisAlignedBB5, arrayList6);
				this.setBlockBounds(0.375F, 0.25F, 0.375F, 0.625F, 1.0F, 0.625F);
				base.getCollidingBoundingBoxes(world1, i2, i3, i4, axisAlignedBB5, arrayList6);
				break;
			case 1:
				this.setBlockBounds(0.0F, 0.75F, 0.0F, 1.0F, 1.0F, 1.0F);
				base.getCollidingBoundingBoxes(world1, i2, i3, i4, axisAlignedBB5, arrayList6);
				this.setBlockBounds(0.375F, 0.0F, 0.375F, 0.625F, 0.75F, 0.625F);
				base.getCollidingBoundingBoxes(world1, i2, i3, i4, axisAlignedBB5, arrayList6);
				break;
			case 2:
				this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 0.25F);
				base.getCollidingBoundingBoxes(world1, i2, i3, i4, axisAlignedBB5, arrayList6);
				this.setBlockBounds(0.25F, 0.375F, 0.25F, 0.75F, 0.625F, 1.0F);
				base.getCollidingBoundingBoxes(world1, i2, i3, i4, axisAlignedBB5, arrayList6);
				break;
			case 3:
				this.setBlockBounds(0.0F, 0.0F, 0.75F, 1.0F, 1.0F, 1.0F);
				base.getCollidingBoundingBoxes(world1, i2, i3, i4, axisAlignedBB5, arrayList6);
				this.setBlockBounds(0.25F, 0.375F, 0.0F, 0.75F, 0.625F, 0.75F);
				base.getCollidingBoundingBoxes(world1, i2, i3, i4, axisAlignedBB5, arrayList6);
				break;
			case 4:
				this.setBlockBounds(0.0F, 0.0F, 0.0F, 0.25F, 1.0F, 1.0F);
				base.getCollidingBoundingBoxes(world1, i2, i3, i4, axisAlignedBB5, arrayList6);
				this.setBlockBounds(0.375F, 0.25F, 0.25F, 0.625F, 0.75F, 1.0F);
				base.getCollidingBoundingBoxes(world1, i2, i3, i4, axisAlignedBB5, arrayList6);
				break;
			case 5:
				this.setBlockBounds(0.75F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
				base.getCollidingBoundingBoxes(world1, i2, i3, i4, axisAlignedBB5, arrayList6);
				this.setBlockBounds(0.0F, 0.375F, 0.25F, 0.75F, 0.625F, 0.75F);
				base.getCollidingBoundingBoxes(world1, i2, i3, i4, axisAlignedBB5, arrayList6);
			break;
			}

			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
		}

		public override void setBlockBoundsBasedOnState(IBlockAccess iBlockAccess1, int i2, int i3, int i4)
		{
			int i5 = iBlockAccess1.getBlockMetadata(i2, i3, i4);
			switch (getDirectionMeta(i5))
			{
			case 0:
				this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.25F, 1.0F);
				break;
			case 1:
				this.setBlockBounds(0.0F, 0.75F, 0.0F, 1.0F, 1.0F, 1.0F);
				break;
			case 2:
				this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 0.25F);
				break;
			case 3:
				this.setBlockBounds(0.0F, 0.0F, 0.75F, 1.0F, 1.0F, 1.0F);
				break;
			case 4:
				this.setBlockBounds(0.0F, 0.0F, 0.0F, 0.25F, 1.0F, 1.0F);
				break;
			case 5:
				this.setBlockBounds(0.75F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
			break;
			}

		}

		public override void onNeighborBlockChange(World world1, int i2, int i3, int i4, int i5)
		{
			int i6 = getDirectionMeta(world1.getBlockMetadata(i2, i3, i4));
			int i7 = world1.getBlockId(i2 - Facing.offsetsXForSide[i6], i3 - Facing.offsetsYForSide[i6], i4 - Facing.offsetsZForSide[i6]);
			if (i7 != Block.pistonBase.blockID && i7 != Block.pistonStickyBase.blockID)
			{
				world1.setBlockWithNotify(i2, i3, i4, 0);
			}
			else
			{
				Block.blocksList[i7].onNeighborBlockChange(world1, i2 - Facing.offsetsXForSide[i6], i3 - Facing.offsetsYForSide[i6], i4 - Facing.offsetsZForSide[i6], i5);
			}

		}

		public static int getDirectionMeta(int i0)
		{
			return i0 & 7;
		}
	}

}