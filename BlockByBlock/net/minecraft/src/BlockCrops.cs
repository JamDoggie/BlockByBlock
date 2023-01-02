using System;
using BlockByBlock.java_extensions;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class BlockCrops : BlockFlower
	{
		protected internal BlockCrops(int i1, int i2) : base(i1, i2)
		{
			this.blockIndexInTexture = i2;
			this.TickRandomly = true;
			float f3 = 0.5F;
			this.setBlockBounds(0.5F - f3, 0.0F, 0.5F - f3, 0.5F + f3, 0.25F, 0.5F + f3);
		}

		protected internal override bool canThisPlantGrowOnThisBlockID(int i1)
		{
			return i1 == Block.tilledField.blockID;
		}

		public override void updateTick(World world1, int i2, int i3, int i4, RandomExtended random5)
		{
			base.updateTick(world1, i2, i3, i4, random5);
			if (world1.getBlockLightValue(i2, i3 + 1, i4) >= 9)
			{
				int i6 = world1.getBlockMetadata(i2, i3, i4);
				if (i6 < 7)
				{
					float f7 = this.getGrowthRate(world1, i2, i3, i4);
					if (random5.Next((int)(25.0F / f7) + 1) == 0)
					{
						++i6;
						world1.setBlockMetadataWithNotify(i2, i3, i4, i6);
					}
				}
			}

		}

		public virtual void fertilize(World world1, int i2, int i3, int i4)
		{
			world1.setBlockMetadataWithNotify(i2, i3, i4, 7);
		}

		private float getGrowthRate(World world1, int i2, int i3, int i4)
		{
			float f5 = 1.0F;
			int i6 = world1.getBlockId(i2, i3, i4 - 1);
			int i7 = world1.getBlockId(i2, i3, i4 + 1);
			int i8 = world1.getBlockId(i2 - 1, i3, i4);
			int i9 = world1.getBlockId(i2 + 1, i3, i4);
			int i10 = world1.getBlockId(i2 - 1, i3, i4 - 1);
			int i11 = world1.getBlockId(i2 + 1, i3, i4 - 1);
			int i12 = world1.getBlockId(i2 + 1, i3, i4 + 1);
			int i13 = world1.getBlockId(i2 - 1, i3, i4 + 1);
			bool z14 = i8 == this.blockID || i9 == this.blockID;
			bool z15 = i6 == this.blockID || i7 == this.blockID;
			bool z16 = i10 == this.blockID || i11 == this.blockID || i12 == this.blockID || i13 == this.blockID;

			for (int i17 = i2 - 1; i17 <= i2 + 1; ++i17)
			{
				for (int i18 = i4 - 1; i18 <= i4 + 1; ++i18)
				{
					int i19 = world1.getBlockId(i17, i3 - 1, i18);
					float f20 = 0.0F;
					if (i19 == Block.tilledField.blockID)
					{
						f20 = 1.0F;
						if (world1.getBlockMetadata(i17, i3 - 1, i18) > 0)
						{
							f20 = 3.0F;
						}
					}

					if (i17 != i2 || i18 != i4)
					{
						f20 /= 4.0F;
					}

					f5 += f20;
				}
			}

			if (z16 || z14 && z15)
			{
				f5 /= 2.0F;
			}

			return f5;
		}

		public override int getBlockTextureFromSideAndMetadata(int i1, int i2)
		{
			if (i2 < 0)
			{
				i2 = 7;
			}

			return this.blockIndexInTexture + i2;
		}

		public override int RenderType
		{
			get
			{
				return 6;
			}
		}

		public override void dropBlockAsItemWithChance(World world1, int i2, int i3, int i4, int i5, float f6, int i7)
		{
			base.dropBlockAsItemWithChance(world1, i2, i3, i4, i5, f6, 0);
			if (!world1.isRemote)
			{
				int i8 = 3 + i7;

				for (int i9 = 0; i9 < i8; ++i9)
				{
					if (world1.rand.Next(15) <= i5)
					{
						float f10 = 0.7F;
						float f11 = world1.rand.NextSingle() * f10 + (1.0F - f10) * 0.5F;
						float f12 = world1.rand.NextSingle() * f10 + (1.0F - f10) * 0.5F;
						float f13 = world1.rand.NextSingle() * f10 + (1.0F - f10) * 0.5F;
						EntityItem entityItem14 = new EntityItem(world1, (double)((float)i2 + f11), (double)((float)i3 + f12), (double)((float)i4 + f13), new ItemStack(Item.seeds));
						entityItem14.delayBeforeCanPickup = 10;
						world1.spawnEntityInWorld(entityItem14);
					}
				}

			}
		}

		public override int idDropped(int i1, RandomExtended random2, int i3)
		{
			return i1 == 7 ? Item.wheat.shiftedIndex : -1;
		}

		public override int quantityDropped(RandomExtended random1)
		{
			return 1;
		}
	}

}