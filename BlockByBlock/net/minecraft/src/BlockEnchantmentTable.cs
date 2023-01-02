using BlockByBlock.java_extensions;
using net.minecraft.client.entity;
using System;

namespace net.minecraft.src
{

    public class BlockEnchantmentTable : BlockContainer
	{
		protected internal BlockEnchantmentTable(int i1) : base(i1, 166, Material.rock)
		{
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.75F, 1.0F);
			this.setLightOpacity(0);
		}
        
		public override bool renderAsNormalBlock()
		{
			return false;
		}

		public override void randomDisplayTick(World world1, int i2, int i3, int i4, RandomExtended random5)
		{
			base.randomDisplayTick(world1, i2, i3, i4, random5);

			for (int i6 = i2 - 2; i6 <= i2 + 2; ++i6)
			{
				for (int i7 = i4 - 2; i7 <= i4 + 2; ++i7)
				{
					if (i6 > i2 - 2 && i6 < i2 + 2 && i7 == i4 - 1)
					{
						i7 = i4 + 2;
					}

					if (random5.Next(16) == 0)
					{
						for (int i8 = i3; i8 <= i3 + 1; ++i8)
						{
							if (world1.getBlockId(i6, i8, i7) == Block.bookShelf.blockID)
							{
								if (!world1.isAirBlock((i6 - i2) / 2 + i2, i8, (i7 - i4) / 2 + i4))
								{
									break;
								}

								world1.spawnParticle("enchantmenttable", (double)i2 + 0.5D, (double)i3 + 2.0D, (double)i4 + 0.5D, (double)((float)(i6 - i2) + random5.NextSingle()) - 0.5D, (double)((float)(i8 - i3) - random5.NextSingle() - 1.0F), (double)((float)(i7 - i4) + random5.NextSingle()) - 0.5D);
							}
						}
					}
				}
			}

		}

		public override bool OpaqueCube
		{
			get
			{
				return false;
			}
		}

		public override int getBlockTextureFromSideAndMetadata(int i1, int i2)
		{
			return this.getBlockTextureFromSide(i1);
		}

		public override int getBlockTextureFromSide(int i1)
		{
			return i1 == 0 ? this.blockIndexInTexture + 17 : (i1 == 1 ? this.blockIndexInTexture : this.blockIndexInTexture + 16);
		}

		public override TileEntity BlockEntity
		{
			get
			{
				return new TileEntityEnchantmentTable();
			}
		}

		public override bool blockActivated(World world1, int i2, int i3, int i4, EntityPlayer entityPlayer5)
		{
			if (world1.isRemote)
			{
				return true;
			}
			else
			{
				entityPlayer5.displayGUIEnchantment(i2, i3, i4);
				return true;
			}
		}
	}

}