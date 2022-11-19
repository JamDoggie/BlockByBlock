using BlockByBlock.java_extensions;
using System;

namespace net.minecraft.src
{

	public class BlockFire : Block
	{
		private int[] chanceToEncourageFire = new int[256];
		private int[] abilityToCatchFire = new int[256];

		protected internal BlockFire(int i1, int i2) : base(i1, i2, Material.fire)
		{
			this.TickRandomly = true;
		}

		protected internal override void initializeBlock()
		{
			this.setBurnRate(Block.planks.blockID, 5, 20);
			this.setBurnRate(Block.fence.blockID, 5, 20);
			this.setBurnRate(Block.stairCompactPlanks.blockID, 5, 20);
			this.setBurnRate(Block.wood.blockID, 5, 5);
			this.setBurnRate(Block.leaves.blockID, 30, 60);
			this.setBurnRate(Block.bookShelf.blockID, 30, 20);
			this.setBurnRate(Block.tnt.blockID, 15, 100);
			this.setBurnRate(Block.tallGrass.blockID, 60, 100);
			this.setBurnRate(Block.cloth.blockID, 30, 60);
			this.setBurnRate(Block.vine.blockID, 15, 100);
		}

		private void setBurnRate(int i1, int i2, int i3)
		{
			this.chanceToEncourageFire[i1] = i2;
			this.abilityToCatchFire[i1] = i3;
		}

		public override AxisAlignedBB getCollisionBoundingBoxFromPool(World world1, int i2, int i3, int i4)
		{
			return null;
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
				return 3;
			}
		}
        
		public override int quantityDropped(RandomExtended random1)
		{
			return 0;
		}

		public override int tickRate()
		{
			return 30;
		}

		public override void updateTick(World world1, int i2, int i3, int i4, RandomExtended random5)
		{
			bool z6 = world1.getBlockId(i2, i3 - 1, i4) == Block.netherrack.blockID;
			if (world1.worldProvider is WorldProviderEnd && world1.getBlockId(i2, i3 - 1, i4) == Block.bedrock.blockID)
			{
				z6 = true;
			}

			if (!this.canPlaceBlockAt(world1, i2, i3, i4))
			{
				world1.setBlockWithNotify(i2, i3, i4, 0);
			}

			if (z6 || !world1.Raining || !world1.canLightningStrikeAt(i2, i3, i4) && !world1.canLightningStrikeAt(i2 - 1, i3, i4) && !world1.canLightningStrikeAt(i2 + 1, i3, i4) && !world1.canLightningStrikeAt(i2, i3, i4 - 1) && !world1.canLightningStrikeAt(i2, i3, i4 + 1))
			{
				int i7 = world1.getBlockMetadata(i2, i3, i4);
				if (i7 < 15)
				{
					world1.setBlockMetadata(i2, i3, i4, i7 + random5.Next(3) / 2);
				}

				world1.scheduleBlockUpdate(i2, i3, i4, this.blockID, this.tickRate() + random5.Next(10));
				if (!z6 && !this.canNeighborBurn(world1, i2, i3, i4))
				{
					if (!world1.isBlockNormalCube(i2, i3 - 1, i4) || i7 > 3)
					{
						world1.setBlockWithNotify(i2, i3, i4, 0);
					}

				}
				else if (!z6 && !this.canBlockCatchFire(world1, i2, i3 - 1, i4) && i7 == 15 && random5.Next(4) == 0)
				{
					world1.setBlockWithNotify(i2, i3, i4, 0);
				}
				else
				{
					bool z8 = world1.isBlockHighHumidity(i2, i3, i4);
					sbyte b9 = 0;
					if (z8)
					{
						b9 = -50;
					}

					this.tryToCatchBlockOnFire(world1, i2 + 1, i3, i4, 300 + b9, random5, i7);
					this.tryToCatchBlockOnFire(world1, i2 - 1, i3, i4, 300 + b9, random5, i7);
					this.tryToCatchBlockOnFire(world1, i2, i3 - 1, i4, 250 + b9, random5, i7);
					this.tryToCatchBlockOnFire(world1, i2, i3 + 1, i4, 250 + b9, random5, i7);
					this.tryToCatchBlockOnFire(world1, i2, i3, i4 - 1, 300 + b9, random5, i7);
					this.tryToCatchBlockOnFire(world1, i2, i3, i4 + 1, 300 + b9, random5, i7);

					for (int i10 = i2 - 1; i10 <= i2 + 1; ++i10)
					{
						for (int i11 = i4 - 1; i11 <= i4 + 1; ++i11)
						{
							for (int i12 = i3 - 1; i12 <= i3 + 4; ++i12)
							{
								if (i10 != i2 || i12 != i3 || i11 != i4)
								{
									int i13 = 100;
									if (i12 > i3 + 1)
									{
										i13 += (i12 - (i3 + 1)) * 100;
									}

									int i14 = this.getChanceOfNeighborsEncouragingFire(world1, i10, i12, i11);
									if (i14 > 0)
									{
										int i15 = (i14 + 40) / (i7 + 30);
										if (z8)
										{
											i15 /= 2;
										}

										if (i15 > 0 && random5.Next(i13) <= i15 && (!world1.Raining || !world1.canLightningStrikeAt(i10, i12, i11)) && !world1.canLightningStrikeAt(i10 - 1, i12, i4) && !world1.canLightningStrikeAt(i10 + 1, i12, i11) && !world1.canLightningStrikeAt(i10, i12, i11 - 1) && !world1.canLightningStrikeAt(i10, i12, i11 + 1))
										{
											int i16 = i7 + random5.Next(5) / 4;
											if (i16 > 15)
											{
												i16 = 15;
											}

											world1.setBlockAndMetadataWithNotify(i10, i12, i11, this.blockID, i16);
										}
									}
								}
							}
						}
					}

				}
			}
			else
			{
				world1.setBlockWithNotify(i2, i3, i4, 0);
			}
		}

		private void tryToCatchBlockOnFire(World world1, int i2, int i3, int i4, int i5, RandomExtended random6, int i7)
		{
			int i8 = this.abilityToCatchFire[world1.getBlockId(i2, i3, i4)];
			if (random6.Next(i5) < i8)
			{
				bool z9 = world1.getBlockId(i2, i3, i4) == Block.tnt.blockID;
				if (random6.Next(i7 + 10) < 5 && !world1.canLightningStrikeAt(i2, i3, i4))
				{
					int i10 = i7 + random6.Next(5) / 4;
					if (i10 > 15)
					{
						i10 = 15;
					}

					world1.setBlockAndMetadataWithNotify(i2, i3, i4, this.blockID, i10);
				}
				else
				{
					world1.setBlockWithNotify(i2, i3, i4, 0);
				}

				if (z9)
				{
					Block.tnt.onBlockDestroyedByPlayer(world1, i2, i3, i4, 1);
				}
			}

		}

		private bool canNeighborBurn(World world1, int i2, int i3, int i4)
		{
			return this.canBlockCatchFire(world1, i2 + 1, i3, i4) ? true : (this.canBlockCatchFire(world1, i2 - 1, i3, i4) ? true : (this.canBlockCatchFire(world1, i2, i3 - 1, i4) ? true : (this.canBlockCatchFire(world1, i2, i3 + 1, i4) ? true : (this.canBlockCatchFire(world1, i2, i3, i4 - 1) ? true : this.canBlockCatchFire(world1, i2, i3, i4 + 1)))));
		}

		private int getChanceOfNeighborsEncouragingFire(World world1, int i2, int i3, int i4)
		{
			sbyte b5 = 0;
			if (!world1.isAirBlock(i2, i3, i4))
			{
				return 0;
			}
			else
			{
				int i6 = this.getChanceToEncourageFire(world1, i2 + 1, i3, i4, b5);
				i6 = this.getChanceToEncourageFire(world1, i2 - 1, i3, i4, i6);
				i6 = this.getChanceToEncourageFire(world1, i2, i3 - 1, i4, i6);
				i6 = this.getChanceToEncourageFire(world1, i2, i3 + 1, i4, i6);
				i6 = this.getChanceToEncourageFire(world1, i2, i3, i4 - 1, i6);
				i6 = this.getChanceToEncourageFire(world1, i2, i3, i4 + 1, i6);
				return i6;
			}
		}

		public override bool Collidable
		{
			get
			{
				return false;
			}
		}

		public virtual bool canBlockCatchFire(IBlockAccess iBlockAccess1, int i2, int i3, int i4)
		{
			return this.chanceToEncourageFire[iBlockAccess1.getBlockId(i2, i3, i4)] > 0;
		}

		public virtual int getChanceToEncourageFire(World world1, int i2, int i3, int i4, int i5)
		{
			int i6 = this.chanceToEncourageFire[world1.getBlockId(i2, i3, i4)];
			return i6 > i5 ? i6 : i5;
		}

		public override bool canPlaceBlockAt(World world1, int i2, int i3, int i4)
		{
			return world1.isBlockNormalCube(i2, i3 - 1, i4) || this.canNeighborBurn(world1, i2, i3, i4);
		}

		public override void onNeighborBlockChange(World world1, int i2, int i3, int i4, int i5)
		{
			if (!world1.isBlockNormalCube(i2, i3 - 1, i4) && !this.canNeighborBurn(world1, i2, i3, i4))
			{
				world1.setBlockWithNotify(i2, i3, i4, 0);
			}
		}

		public override void onBlockAdded(World world1, int i2, int i3, int i4)
		{
			if (world1.worldProvider.worldType > 0 || world1.getBlockId(i2, i3 - 1, i4) != Block.obsidian.blockID || !Block.portal.tryToCreatePortal(world1, i2, i3, i4))
			{
				if (!world1.isBlockNormalCube(i2, i3 - 1, i4) && !this.canNeighborBurn(world1, i2, i3, i4))
				{
					world1.setBlockWithNotify(i2, i3, i4, 0);
				}
				else
				{
					world1.scheduleBlockUpdate(i2, i3, i4, this.blockID, this.tickRate() + world1.rand.Next(10));
				}
			}
		}

		public override void randomDisplayTick(World world1, int i2, int i3, int i4, RandomExtended random5)
		{
			if (random5.Next(24) == 0)
			{
				world1.playSoundEffect((double)((float)i2 + 0.5F), (double)((float)i3 + 0.5F), (double)((float)i4 + 0.5F), "fire.fire", 1.0F + random5.NextSingle(), random5.NextSingle() * 0.7F + 0.3F);
			}

			int i6;
			float f7;
			float f8;
			float f9;
			if (!world1.isBlockNormalCube(i2, i3 - 1, i4) && !Block.fire.canBlockCatchFire(world1, i2, i3 - 1, i4))
			{
				if (Block.fire.canBlockCatchFire(world1, i2 - 1, i3, i4))
				{
					for (i6 = 0; i6 < 2; ++i6)
					{
						f7 = (float)i2 + random5.NextSingle() * 0.1F;
						f8 = (float)i3 + random5.NextSingle();
						f9 = (float)i4 + random5.NextSingle();
						world1.spawnParticle("largesmoke", (double)f7, (double)f8, (double)f9, 0.0D, 0.0D, 0.0D);
					}
				}

				if (Block.fire.canBlockCatchFire(world1, i2 + 1, i3, i4))
				{
					for (i6 = 0; i6 < 2; ++i6)
					{
						f7 = (float)(i2 + 1) - random5.NextSingle() * 0.1F;
						f8 = (float)i3 + random5.NextSingle();
						f9 = (float)i4 + random5.NextSingle();
						world1.spawnParticle("largesmoke", (double)f7, (double)f8, (double)f9, 0.0D, 0.0D, 0.0D);
					}
				}

				if (Block.fire.canBlockCatchFire(world1, i2, i3, i4 - 1))
				{
					for (i6 = 0; i6 < 2; ++i6)
					{
						f7 = (float)i2 + random5.NextSingle();
						f8 = (float)i3 + random5.NextSingle();
						f9 = (float)i4 + random5.NextSingle() * 0.1F;
						world1.spawnParticle("largesmoke", (double)f7, (double)f8, (double)f9, 0.0D, 0.0D, 0.0D);
					}
				}

				if (Block.fire.canBlockCatchFire(world1, i2, i3, i4 + 1))
				{
					for (i6 = 0; i6 < 2; ++i6)
					{
						f7 = (float)i2 + random5.NextSingle();
						f8 = (float)i3 + random5.NextSingle();
						f9 = (float)(i4 + 1) - random5.NextSingle() * 0.1F;
						world1.spawnParticle("largesmoke", (double)f7, (double)f8, (double)f9, 0.0D, 0.0D, 0.0D);
					}
				}

				if (Block.fire.canBlockCatchFire(world1, i2, i3 + 1, i4))
				{
					for (i6 = 0; i6 < 2; ++i6)
					{
						f7 = (float)i2 + random5.NextSingle();
						f8 = (float)(i3 + 1) - random5.NextSingle() * 0.1F;
						f9 = (float)i4 + random5.NextSingle();
						world1.spawnParticle("largesmoke", (double)f7, (double)f8, (double)f9, 0.0D, 0.0D, 0.0D);
					}
				}
			}
			else
			{
				for (i6 = 0; i6 < 3; ++i6)
				{
					f7 = (float)i2 + random5.NextSingle();
					f8 = (float)i3 + random5.NextSingle() * 0.5F + 0.5F;
					f9 = (float)i4 + random5.NextSingle();
					world1.spawnParticle("largesmoke", (double)f7, (double)f8, (double)f9, 0.0D, 0.0D, 0.0D);
				}
			}

		}
	}

}