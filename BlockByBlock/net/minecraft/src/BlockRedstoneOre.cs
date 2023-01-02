using System;
using BlockByBlock.java_extensions;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class BlockRedstoneOre : Block
	{
		private bool glowing;

		public BlockRedstoneOre(int i1, int i2, bool z3) : base(i1, i2, Material.rock)
		{
			if (z3)
			{
				this.TickRandomly = true;
			}

			this.glowing = z3;
		}

		public override int tickRate()
		{
			return 30;
		}

		public override void onBlockClicked(World world1, int i2, int i3, int i4, EntityPlayer entityPlayer5)
		{
			this.glow(world1, i2, i3, i4);
			base.onBlockClicked(world1, i2, i3, i4, entityPlayer5);
		}

		public override void onEntityWalking(World world1, int i2, int i3, int i4, Entity entity5)
		{
			this.glow(world1, i2, i3, i4);
			base.onEntityWalking(world1, i2, i3, i4, entity5);
		}

		public override bool blockActivated(World world1, int i2, int i3, int i4, EntityPlayer entityPlayer5)
		{
			this.glow(world1, i2, i3, i4);
			return base.blockActivated(world1, i2, i3, i4, entityPlayer5);
		}

		private void glow(World world1, int i2, int i3, int i4)
		{
			this.sparkle(world1, i2, i3, i4);
			if (this.blockID == Block.oreRedstone.blockID)
			{
				world1.setBlockWithNotify(i2, i3, i4, Block.oreRedstoneGlowing.blockID);
			}

		}

		public override void updateTick(World world1, int i2, int i3, int i4, RandomExtended random5)
		{
			if (this.blockID == Block.oreRedstoneGlowing.blockID)
			{
				world1.setBlockWithNotify(i2, i3, i4, Block.oreRedstone.blockID);
			}

		}

		public override int idDropped(int i1, RandomExtended random2, int i3)
		{
			return Item.redstone.shiftedIndex;
		}

		public override int quantityDroppedWithBonus(int i1, RandomExtended random2)
		{
			return this.quantityDropped(random2) + random2.Next(i1 + 1);
		}

		public override int quantityDropped(RandomExtended random1)
		{
			return 4 + random1.Next(2);
		}

		public override void randomDisplayTick(World world1, int i2, int i3, int i4, RandomExtended random5)
		{
			if (this.glowing)
			{
				this.sparkle(world1, i2, i3, i4);
			}

		}

		private void sparkle(World world1, int i2, int i3, int i4)
		{
			RandomExtended random5 = world1.rand;
			double d6 = 0.0625D;

			for (int i8 = 0; i8 < 6; ++i8)
			{
				double d9 = (double)((float)i2 + random5.NextSingle());
				double d11 = (double)((float)i3 + random5.NextSingle());
				double d13 = (double)((float)i4 + random5.NextSingle());
				if (i8 == 0 && !world1.isBlockOpaqueCube(i2, i3 + 1, i4))
				{
					d11 = (double)(i3 + 1) + d6;
				}

				if (i8 == 1 && !world1.isBlockOpaqueCube(i2, i3 - 1, i4))
				{
					d11 = (double)(i3 + 0) - d6;
				}

				if (i8 == 2 && !world1.isBlockOpaqueCube(i2, i3, i4 + 1))
				{
					d13 = (double)(i4 + 1) + d6;
				}

				if (i8 == 3 && !world1.isBlockOpaqueCube(i2, i3, i4 - 1))
				{
					d13 = (double)(i4 + 0) - d6;
				}

				if (i8 == 4 && !world1.isBlockOpaqueCube(i2 + 1, i3, i4))
				{
					d9 = (double)(i2 + 1) + d6;
				}

				if (i8 == 5 && !world1.isBlockOpaqueCube(i2 - 1, i3, i4))
				{
					d9 = (double)(i2 + 0) - d6;
				}

				if (d9 < (double)i2 || d9 > (double)(i2 + 1) || d11 < 0.0D || d11 > (double)(i3 + 1) || d13 < (double)i4 || d13 > (double)(i4 + 1))
				{
					world1.spawnParticle("reddust", d9, d11, d13, 0.0D, 0.0D, 0.0D);
				}
			}

		}

		protected internal override ItemStack createStackedBlock(int i1)
		{
			return new ItemStack(Block.oreRedstone);
		}
	}

}