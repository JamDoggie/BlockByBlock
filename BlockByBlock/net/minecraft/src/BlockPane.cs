using System;
using System.Collections;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class BlockPane : Block
	{
		private int sideTextureIndex;
		private readonly bool canDropItself;

		protected internal BlockPane(int i1, int i2, int i3, Material material4, bool z5) : base(i1, i2, material4)
		{
			this.sideTextureIndex = i3;
			this.canDropItself = z5;
		}

		public override int idDropped(int i1, RandomExtended random2, int i3)
		{
			return !this.canDropItself ? 0 : base.idDropped(i1, random2, i3);
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
				return 18;
			}
		}

		public override bool shouldSideBeRendered(IBlockAccess iBlockAccess1, int i2, int i3, int i4, int i5)
		{
			int i6 = iBlockAccess1.getBlockId(i2, i3, i4);
			return i6 == this.blockID ? false : base.shouldSideBeRendered(iBlockAccess1, i2, i3, i4, i5);
		}

		public override void getCollidingBoundingBoxes(World world1, int i2, int i3, int i4, AxisAlignedBB axisAlignedBB5, ArrayList arrayList6)
		{
			bool z7 = this.canThisPaneConnectToThisBlockID(world1.getBlockId(i2, i3, i4 - 1));
			bool z8 = this.canThisPaneConnectToThisBlockID(world1.getBlockId(i2, i3, i4 + 1));
			bool z9 = this.canThisPaneConnectToThisBlockID(world1.getBlockId(i2 - 1, i3, i4));
			bool z10 = this.canThisPaneConnectToThisBlockID(world1.getBlockId(i2 + 1, i3, i4));
			if (z9 && z10 || !z9 && !z10 && !z7 && !z8)
			{
				this.setBlockBounds(0.0F, 0.0F, 0.4375F, 1.0F, 1.0F, 0.5625F);
				base.getCollidingBoundingBoxes(world1, i2, i3, i4, axisAlignedBB5, arrayList6);
			}
			else if (z9 && !z10)
			{
				this.setBlockBounds(0.0F, 0.0F, 0.4375F, 0.5F, 1.0F, 0.5625F);
				base.getCollidingBoundingBoxes(world1, i2, i3, i4, axisAlignedBB5, arrayList6);
			}
			else if (!z9 && z10)
			{
				this.setBlockBounds(0.5F, 0.0F, 0.4375F, 1.0F, 1.0F, 0.5625F);
				base.getCollidingBoundingBoxes(world1, i2, i3, i4, axisAlignedBB5, arrayList6);
			}

			if ((!z7 || !z8) && (z9 || z10 || z7 || z8))
			{
				if (z7 && !z8)
				{
					this.setBlockBounds(0.4375F, 0.0F, 0.0F, 0.5625F, 1.0F, 0.5F);
					base.getCollidingBoundingBoxes(world1, i2, i3, i4, axisAlignedBB5, arrayList6);
				}
				else if (!z7 && z8)
				{
					this.setBlockBounds(0.4375F, 0.0F, 0.5F, 0.5625F, 1.0F, 1.0F);
					base.getCollidingBoundingBoxes(world1, i2, i3, i4, axisAlignedBB5, arrayList6);
				}
			}
			else
			{
				this.setBlockBounds(0.4375F, 0.0F, 0.0F, 0.5625F, 1.0F, 1.0F);
				base.getCollidingBoundingBoxes(world1, i2, i3, i4, axisAlignedBB5, arrayList6);
			}

		}

		public override void setBlockBoundsForItemRender()
		{
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
		}

		public override void setBlockBoundsBasedOnState(IBlockAccess iBlockAccess1, int i2, int i3, int i4)
		{
			float f5 = 0.4375F;
			float f6 = 0.5625F;
			float f7 = 0.4375F;
			float f8 = 0.5625F;
			bool z9 = this.canThisPaneConnectToThisBlockID(iBlockAccess1.getBlockId(i2, i3, i4 - 1));
			bool z10 = this.canThisPaneConnectToThisBlockID(iBlockAccess1.getBlockId(i2, i3, i4 + 1));
			bool z11 = this.canThisPaneConnectToThisBlockID(iBlockAccess1.getBlockId(i2 - 1, i3, i4));
			bool z12 = this.canThisPaneConnectToThisBlockID(iBlockAccess1.getBlockId(i2 + 1, i3, i4));
			if (z11 && z12 || !z11 && !z12 && !z9 && !z10)
			{
				f5 = 0.0F;
				f6 = 1.0F;
			}
			else if (z11 && !z12)
			{
				f5 = 0.0F;
			}
			else if (!z11 && z12)
			{
				f6 = 1.0F;
			}

			if ((!z9 || !z10) && (z11 || z12 || z9 || z10))
			{
				if (z9 && !z10)
				{
					f7 = 0.0F;
				}
				else if (!z9 && z10)
				{
					f8 = 1.0F;
				}
			}
			else
			{
				f7 = 0.0F;
				f8 = 1.0F;
			}

			this.setBlockBounds(f5, 0.0F, f7, f6, 1.0F, f8);
		}

		public virtual int SideTextureIndex
		{
			get
			{
				return this.sideTextureIndex;
			}
		}

		public bool canThisPaneConnectToThisBlockID(int i1)
		{
			return Block.opaqueCubeLookup[i1] || i1 == this.blockID || i1 == Block.glass.blockID;
		}
	}

}