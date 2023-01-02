using System;
using System.Collections;
using BlockByBlock.java_extensions;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class BlockBrewingStand : BlockContainer
	{
		private RandomExtended rand = new RandomExtended();

		public BlockBrewingStand(int i1) : base(i1, Material.iron)
		{
			this.blockIndexInTexture = 157;
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
				return 25;
			}
		}

		public override TileEntity BlockEntity
		{
			get
			{
				return new TileEntityBrewingStand();
			}
		}

		public override bool renderAsNormalBlock()
		{
			return false;
		}

		public override void getCollidingBoundingBoxes(World world1, int i2, int i3, int i4, AxisAlignedBB axisAlignedBB5, ArrayList arrayList6)
		{
			this.setBlockBounds(0.4375F, 0.0F, 0.4375F, 0.5625F, 0.875F, 0.5625F);
			base.getCollidingBoundingBoxes(world1, i2, i3, i4, axisAlignedBB5, arrayList6);
			this.setBlockBoundsForItemRender();
			base.getCollidingBoundingBoxes(world1, i2, i3, i4, axisAlignedBB5, arrayList6);
		}

		public override void setBlockBoundsForItemRender()
		{
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.125F, 1.0F);
		}

		public override bool blockActivated(World world1, int i2, int i3, int i4, EntityPlayer entityPlayer5)
		{
			if (world1.isRemote)
			{
				return true;
			}
			else
			{
				TileEntityBrewingStand tileEntityBrewingStand6 = (TileEntityBrewingStand)world1.getBlockTileEntity(i2, i3, i4);
				if (tileEntityBrewingStand6 != null)
				{
					entityPlayer5.displayGUIBrewingStand(tileEntityBrewingStand6);
				}

				return true;
			}
		}

		public override void randomDisplayTick(World world1, int i2, int i3, int i4, RandomExtended random5)
		{
			double d6 = (double)((float)i2 + 0.4F + random5.NextSingle() * 0.2F);
			double d8 = (double)((float)i3 + 0.7F + random5.NextSingle() * 0.3F);
			double d10 = (double)((float)i4 + 0.4F + random5.NextSingle() * 0.2F);
			world1.spawnParticle("smoke", d6, d8, d10, 0.0D, 0.0D, 0.0D);
		}

		public override void onBlockRemoval(World world1, int i2, int i3, int i4)
		{
			TileEntity tileEntity5 = world1.getBlockTileEntity(i2, i3, i4);
			if (tileEntity5 != null && tileEntity5 is TileEntityBrewingStand)
			{
				TileEntityBrewingStand tileEntityBrewingStand6 = (TileEntityBrewingStand)tileEntity5;

				for (int i7 = 0; i7 < tileEntityBrewingStand6.SizeInventory; ++i7)
				{
					ItemStack itemStack8 = tileEntityBrewingStand6.getStackInSlot(i7);
					if (itemStack8 != null)
					{
						float f9 = rand.NextSingle() * 0.8F + 0.1F;
						float f10 = rand.NextSingle() * 0.8F + 0.1F;
						float f11 = rand.NextSingle() * 0.8F + 0.1F;

						while (itemStack8.stackSize > 0)
						{
							int i12 = this.rand.Next(21) + 10;
							if (i12 > itemStack8.stackSize)
							{
								i12 = itemStack8.stackSize;
							}

							itemStack8.stackSize -= i12;
							EntityItem entityItem13 = new EntityItem(world1, (double)((float)i2 + f9), (double)((float)i3 + f10), (double)((float)i4 + f11), new ItemStack(itemStack8.itemID, i12, itemStack8.ItemDamage));
							float f14 = 0.05F;
							entityItem13.motionX = rand.NextGaussian() * f14;
							entityItem13.motionY = rand.NextGaussian() * f14 + 0.2F;
							entityItem13.motionZ = rand.NextGaussian() * f14;
							world1.spawnEntityInWorld(entityItem13);
						}
					}
				}
			}

			base.onBlockRemoval(world1, i2, i3, i4);
		}

		public override int idDropped(int i1, RandomExtended random2, int i3)
		{
			return Item.brewingStand.shiftedIndex;
		}
	}

}