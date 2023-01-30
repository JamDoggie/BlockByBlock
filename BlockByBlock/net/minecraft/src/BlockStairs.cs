using System;
using System.Collections;
using BlockByBlock.java_extensions;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class BlockStairs : Block
	{
		private Block modelBlock;

		protected internal BlockStairs(int i1, Block block2) : base(i1, block2.blockIndexInTexture, block2.blockMaterial)
		{
			modelBlock = block2;
			Hardness = block2.blockHardness;
			setResistance(block2.blockResistance / 3.0F);
			setStepSound(block2.stepSound);
			setLightOpacity(255);
		}

		public override void setBlockBoundsBasedOnState(IBlockAccess iBlockAccess1, int i2, int i3, int i4)
		{
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
		}

		public override AxisAlignedBB getCollisionBoundingBoxFromPool(World world1, int i2, int i3, int i4)
		{
			return base.getCollisionBoundingBoxFromPool(world1, i2, i3, i4);
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
				return 10;
			}
		}

		public override bool shouldSideBeRendered(IBlockAccess iBlockAccess1, int i2, int i3, int i4, int i5)
		{
			return base.shouldSideBeRendered(iBlockAccess1, i2, i3, i4, i5);
		}

		public override void getCollidingBoundingBoxes(World world1, int i2, int i3, int i4, AxisAlignedBB axisAlignedBB5, ArrayList arrayList6)
		{
			int i7 = world1.getBlockMetadata(i2, i3, i4);
			int i8 = i7 & 3;
			float f9 = 0.0F;
			float f10 = 0.5F;
			float f11 = 0.5F;
			float f12 = 1.0F;
			if ((i7 & 4) != 0)
			{
				f9 = 0.5F;
				f10 = 1.0F;
				f11 = 0.0F;
				f12 = 0.5F;
			}

			this.setBlockBounds(0.0F, f9, 0.0F, 1.0F, f10, 1.0F);
			base.getCollidingBoundingBoxes(world1, i2, i3, i4, axisAlignedBB5, arrayList6);
			if (i8 == 0)
			{
				this.setBlockBounds(0.5F, f11, 0.0F, 1.0F, f12, 1.0F);
				base.getCollidingBoundingBoxes(world1, i2, i3, i4, axisAlignedBB5, arrayList6);
			}
			else if (i8 == 1)
			{
				this.setBlockBounds(0.0F, f11, 0.0F, 0.5F, f12, 1.0F);
				base.getCollidingBoundingBoxes(world1, i2, i3, i4, axisAlignedBB5, arrayList6);
			}
			else if (i8 == 2)
			{
				this.setBlockBounds(0.0F, f11, 0.5F, 1.0F, f12, 1.0F);
				base.getCollidingBoundingBoxes(world1, i2, i3, i4, axisAlignedBB5, arrayList6);
			}
			else if (i8 == 3)
			{
				this.setBlockBounds(0.0F, f11, 0.0F, 1.0F, f12, 0.5F);
				base.getCollidingBoundingBoxes(world1, i2, i3, i4, axisAlignedBB5, arrayList6);
			}

			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
		}

		public override void randomDisplayTick(World world1, int i2, int i3, int i4, RandomExtended random5)
		{
			this.modelBlock.randomDisplayTick(world1, i2, i3, i4, random5);
		}

		public override void onBlockClicked(World world1, int i2, int i3, int i4, EntityPlayer entityPlayer5)
		{
			this.modelBlock.onBlockClicked(world1, i2, i3, i4, entityPlayer5);
		}

		public override void onBlockDestroyedByPlayer(World world1, int i2, int i3, int i4, int i5)
		{
			this.modelBlock.onBlockDestroyedByPlayer(world1, i2, i3, i4, i5);
		}

		public override int GetMixedBrightnessForBlock(IBlockAccess iBlockAccess1, int i2, int i3, int i4)
		{
			return this.modelBlock.GetMixedBrightnessForBlock(iBlockAccess1, i2, i3, i4);
		}

		public override float getBlockBrightness(IBlockAccess iBlockAccess1, int i2, int i3, int i4)
		{
			return this.modelBlock.getBlockBrightness(iBlockAccess1, i2, i3, i4);
		}

		public override float getExplosionResistance(Entity entity1)
		{
			return this.modelBlock.getExplosionResistance(entity1);
		}

		public override int RenderBlockPass
		{
			get
			{
				return this.modelBlock.RenderBlockPass;
			}
		}

		public override int getBlockTextureFromSideAndMetadata(int i1, int i2)
		{
			return this.modelBlock.getBlockTextureFromSideAndMetadata(i1, 0);
		}

		public override int getBlockTextureFromSide(int i1)
		{
			return this.modelBlock.getBlockTextureFromSideAndMetadata(i1, 0);
		}

		public override int tickRate()
		{
			return this.modelBlock.tickRate();
		}

		public override AxisAlignedBB getSelectedBoundingBoxFromPool(World world1, int i2, int i3, int i4)
		{
			return this.modelBlock.getSelectedBoundingBoxFromPool(world1, i2, i3, i4);
		}

		public override void velocityToAddToEntity(World world1, int i2, int i3, int i4, Entity entity5, Vec3D vec3D6)
		{
			this.modelBlock.velocityToAddToEntity(world1, i2, i3, i4, entity5, vec3D6);
		}

		public override bool Collidable
		{
			get
			{
				return this.modelBlock.Collidable;
			}
		}

		public override bool canCollideCheck(int i1, bool z2)
		{
			return this.modelBlock.canCollideCheck(i1, z2);
		}

		public override bool canPlaceBlockAt(World world1, int i2, int i3, int i4)
		{
			return this.modelBlock.canPlaceBlockAt(world1, i2, i3, i4);
		}

		public override void onBlockAdded(World world1, int i2, int i3, int i4)
		{
			this.onNeighborBlockChange(world1, i2, i3, i4, 0);
			this.modelBlock.onBlockAdded(world1, i2, i3, i4);
		}

		public override void onBlockRemoval(World world1, int i2, int i3, int i4)
		{
			this.modelBlock.onBlockRemoval(world1, i2, i3, i4);
		}

		public override void onEntityWalking(World world1, int i2, int i3, int i4, Entity entity5)
		{
			this.modelBlock.onEntityWalking(world1, i2, i3, i4, entity5);
		}

		public override void updateTick(World world1, int i2, int i3, int i4, RandomExtended random5)
		{
			this.modelBlock.updateTick(world1, i2, i3, i4, random5);
		}

		public override bool blockActivated(World world1, int i2, int i3, int i4, EntityPlayer entityPlayer5)
		{
			return this.modelBlock.blockActivated(world1, i2, i3, i4, entityPlayer5);
		}

		public override void onBlockDestroyedByExplosion(World world1, int i2, int i3, int i4)
		{
			this.modelBlock.onBlockDestroyedByExplosion(world1, i2, i3, i4);
		}

		public override void onBlockPlacedBy(World world1, int i2, int i3, int i4, EntityLiving entityLiving5)
		{
			int i6 = MathHelper.floor_double((double)(entityLiving5.rotationYaw * 4.0F / 360.0F) + 0.5D) & 3;
			int i7 = world1.getBlockMetadata(i2, i3, i4) & 4;
			if (i6 == 0)
			{
				world1.setBlockMetadataWithNotify(i2, i3, i4, 2 | i7);
			}

			if (i6 == 1)
			{
				world1.setBlockMetadataWithNotify(i2, i3, i4, 1 | i7);
			}

			if (i6 == 2)
			{
				world1.setBlockMetadataWithNotify(i2, i3, i4, 3 | i7);
			}

			if (i6 == 3)
			{
				world1.setBlockMetadataWithNotify(i2, i3, i4, 0 | i7);
			}

		}

		public override void onBlockPlaced(World world1, int i2, int i3, int i4, int i5)
		{
			if (i5 == 0)
			{
				int i6 = world1.getBlockMetadata(i2, i3, i4);
				world1.setBlockMetadataWithNotify(i2, i3, i4, i6 | 4);
			}

		}
	}

}