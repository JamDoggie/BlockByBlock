using System;
using System.Collections;
using BlockByBlock.java_extensions;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class BlockEndPortalFrame : Block
	{
		public BlockEndPortalFrame(int i1) : base(i1, 159, Material.glass)
		{
		}

		public override int getBlockTextureFromSideAndMetadata(int i1, int i2)
		{
			return i1 == 1 ? this.blockIndexInTexture - 1 : (i1 == 0 ? this.blockIndexInTexture + 16 : this.blockIndexInTexture);
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
				return 26;
			}
		}

		public override void setBlockBoundsForItemRender()
		{
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.8125F, 1.0F);
		}

		public override void getCollidingBoundingBoxes(World world1, int i2, int i3, int i4, AxisAlignedBB axisAlignedBB5, ArrayList arrayList6)
		{
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.8125F, 1.0F);
			base.getCollidingBoundingBoxes(world1, i2, i3, i4, axisAlignedBB5, arrayList6);
			int i7 = world1.getBlockMetadata(i2, i3, i4);
			if (isEnderEyeInserted(i7))
			{
				this.setBlockBounds(0.3125F, 0.8125F, 0.3125F, 0.6875F, 1.0F, 0.6875F);
				base.getCollidingBoundingBoxes(world1, i2, i3, i4, axisAlignedBB5, arrayList6);
			}

			this.setBlockBoundsForItemRender();
		}

		public static bool isEnderEyeInserted(int i0)
		{
			return (i0 & 4) != 0;
		}
        
		public override int idDropped(int i1, RandomExtended random2, int i3)
		{
			return 0;
		}

		public override void onBlockPlacedBy(World world1, int i2, int i3, int i4, EntityLiving entityLiving5)
		{
			int i6 = ((MathHelper.floor_double((double)(entityLiving5.rotationYaw * 4.0F / 360.0F) + 0.5D) & 3) + 2) % 4;
			world1.setBlockMetadataWithNotify(i2, i3, i4, i6);
		}
	}

}