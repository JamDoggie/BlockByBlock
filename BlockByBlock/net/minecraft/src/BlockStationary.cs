using BlockByBlock.java_extensions;
using System;

namespace net.minecraft.src
{

	public class BlockStationary : BlockFluid
	{
		protected internal BlockStationary(int i1, Material material2) : base(i1, material2)
		{
			this.TickRandomly = false;
			if (material2 == Material.lava)
			{
				this.TickRandomly = true;
			}

		}

		public override bool getBlocksMovement(IBlockAccess iBlockAccess1, int i2, int i3, int i4)
		{
			return this.blockMaterial != Material.lava;
		}

		public override void onNeighborBlockChange(World world1, int i2, int i3, int i4, int i5)
		{
			base.onNeighborBlockChange(world1, i2, i3, i4, i5);
			if (world1.getBlockId(i2, i3, i4) == this.blockID)
			{
				this.setNotStationary(world1, i2, i3, i4);
			}

		}

		private void setNotStationary(World world1, int i2, int i3, int i4)
		{
			int i5 = world1.getBlockMetadata(i2, i3, i4);
			world1.editingBlocks = true;
			world1.setBlockAndMetadata(i2, i3, i4, this.blockID - 1, i5);
			world1.markBlocksDirty(i2, i3, i4, i2, i3, i4);
			world1.scheduleBlockUpdate(i2, i3, i4, this.blockID - 1, this.tickRate());
			world1.editingBlocks = false;
		}

		public override void updateTick(World world1, int i2, int i3, int i4, RandomExtended random5)
		{
			if (this.blockMaterial == Material.lava)
			{
				int i6 = random5.Next(3);
				int i7 = 0;

				while (true)
				{
					int i8;
					if (i7 >= i6)
					{
						if (i6 == 0)
						{
							i7 = i2;
							i8 = i4;

							for (int i9 = 0; i9 < 3; ++i9)
							{
								i2 = i7 + random5.Next(3) - 1;
								i4 = i8 + random5.Next(3) - 1;
								if (world1.isAirBlock(i2, i3 + 1, i4) && this.isFlammable(world1, i2, i3, i4))
								{
									world1.setBlockWithNotify(i2, i3 + 1, i4, Block.fire.blockID);
								}
							}
						}
						break;
					}

					i2 += random5.Next(3) - 1;
					++i3;
					i4 += random5.Next(3) - 1;
					i8 = world1.getBlockId(i2, i3, i4);
					if (i8 == 0)
					{
						if (this.isFlammable(world1, i2 - 1, i3, i4) || this.isFlammable(world1, i2 + 1, i3, i4) || this.isFlammable(world1, i2, i3, i4 - 1) || this.isFlammable(world1, i2, i3, i4 + 1) || this.isFlammable(world1, i2, i3 - 1, i4) || this.isFlammable(world1, i2, i3 + 1, i4))
						{
							world1.setBlockWithNotify(i2, i3, i4, Block.fire.blockID);
							return;
						}
					}
					else if (Block.blocksList[i8].blockMaterial.blocksMovement())
					{
						return;
					}

					++i7;
				}
			}

		}

		private bool isFlammable(World world1, int i2, int i3, int i4)
		{
			return world1.getBlockMaterial(i2, i3, i4).CanBurn;
		}
	}

}