using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class BlockTrapDoor : Block
	{
		protected internal BlockTrapDoor(int i1, Material material2) : base(i1, material2)
		{
			this.blockIndexInTexture = 84;
			if (material2 == Material.iron)
			{
				++this.blockIndexInTexture;
			}

			float f3 = 0.5F;
			float f4 = 1.0F;
			this.setBlockBounds(0.5F - f3, 0.0F, 0.5F - f3, 0.5F + f3, f4, 0.5F + f3);
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

		public override bool getBlocksMovement(IBlockAccess iBlockAccess1, int i2, int i3, int i4)
		{
			return !isTrapdoorOpen(iBlockAccess1.getBlockMetadata(i2, i3, i4));
		}

		public override int RenderType
		{
			get
			{
				return 0;
			}
		}

		public override AxisAlignedBB getSelectedBoundingBoxFromPool(World world1, int i2, int i3, int i4)
		{
			this.setBlockBoundsBasedOnState(world1, i2, i3, i4);
			return base.getSelectedBoundingBoxFromPool(world1, i2, i3, i4);
		}

		public override AxisAlignedBB getCollisionBoundingBoxFromPool(World world1, int i2, int i3, int i4)
		{
			this.setBlockBoundsBasedOnState(world1, i2, i3, i4);
			return base.getCollisionBoundingBoxFromPool(world1, i2, i3, i4);
		}

		public override void setBlockBoundsBasedOnState(IBlockAccess iBlockAccess1, int i2, int i3, int i4)
		{
			this.BlockBoundsForBlockRender = iBlockAccess1.getBlockMetadata(i2, i3, i4);
		}

		public override void setBlockBoundsForItemRender()
		{
			float f1 = 0.1875F;
			this.setBlockBounds(0.0F, 0.5F - f1 / 2.0F, 0.0F, 1.0F, 0.5F + f1 / 2.0F, 1.0F);
		}

		public virtual int BlockBoundsForBlockRender
		{
			set
			{
				float f2 = 0.1875F;
				this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, f2, 1.0F);
				if (isTrapdoorOpen(value))
				{
					if ((value & 3) == 0)
					{
						this.setBlockBounds(0.0F, 0.0F, 1.0F - f2, 1.0F, 1.0F, 1.0F);
					}
    
					if ((value & 3) == 1)
					{
						this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, f2);
					}
    
					if ((value & 3) == 2)
					{
						this.setBlockBounds(1.0F - f2, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
					}
    
					if ((value & 3) == 3)
					{
						this.setBlockBounds(0.0F, 0.0F, 0.0F, f2, 1.0F, 1.0F);
					}
				}
    
			}
		}

		public override void onBlockClicked(World world1, int i2, int i3, int i4, EntityPlayer entityPlayer5)
		{
			this.blockActivated(world1, i2, i3, i4, entityPlayer5);
		}

		public override bool blockActivated(World world1, int i2, int i3, int i4, EntityPlayer entityPlayer5)
		{
			if (this.blockMaterial == Material.iron)
			{
				return true;
			}
			else
			{
				int i6 = world1.getBlockMetadata(i2, i3, i4);
				world1.setBlockMetadataWithNotify(i2, i3, i4, i6 ^ 4);
				world1.playAuxSFXAtEntity(entityPlayer5, 1003, i2, i3, i4, 0);
				return true;
			}
		}

		public virtual void onPoweredBlockChange(World world1, int i2, int i3, int i4, bool z5)
		{
			int i6 = world1.getBlockMetadata(i2, i3, i4);
			bool z7 = (i6 & 4) > 0;
			if (z7 != z5)
			{
				world1.setBlockMetadataWithNotify(i2, i3, i4, i6 ^ 4);
				world1.playAuxSFXAtEntity((EntityPlayer)null, 1003, i2, i3, i4, 0);
			}
		}

		public override void onNeighborBlockChange(World world1, int i2, int i3, int i4, int i5)
		{
			if (!world1.isRemote)
			{
				int i6 = world1.getBlockMetadata(i2, i3, i4);
				int i7 = i2;
				int i8 = i4;
				if ((i6 & 3) == 0)
				{
					i8 = i4 + 1;
				}

				if ((i6 & 3) == 1)
				{
					--i8;
				}

				if ((i6 & 3) == 2)
				{
					i7 = i2 + 1;
				}

				if ((i6 & 3) == 3)
				{
					--i7;
				}

				if (!isValidSupportBlock(world1.getBlockId(i7, i3, i8)))
				{
					world1.setBlockWithNotify(i2, i3, i4, 0);
					this.dropBlockAsItem(world1, i2, i3, i4, i6, 0);
				}

				bool z9 = world1.isBlockIndirectlyGettingPowered(i2, i3, i4);
				if (z9 || i5 > 0 && Block.blocksList[i5].canProvidePower() || i5 == 0)
				{
					this.onPoweredBlockChange(world1, i2, i3, i4, z9);
				}

			}
		}

		public override MovingObjectPosition collisionRayTrace(World world1, int i2, int i3, int i4, Vec3D vec3D5, Vec3D vec3D6)
		{
			this.setBlockBoundsBasedOnState(world1, i2, i3, i4);
			return base.collisionRayTrace(world1, i2, i3, i4, vec3D5, vec3D6);
		}

		public override void onBlockPlaced(World world1, int i2, int i3, int i4, int i5)
		{
			sbyte b6 = 0;
			if (i5 == 2)
			{
				b6 = 0;
			}

			if (i5 == 3)
			{
				b6 = 1;
			}

			if (i5 == 4)
			{
				b6 = 2;
			}

			if (i5 == 5)
			{
				b6 = 3;
			}

			world1.setBlockMetadataWithNotify(i2, i3, i4, b6);
		}

		public override bool canPlaceBlockOnSide(World world1, int i2, int i3, int i4, int i5)
		{
			if (i5 == 0)
			{
				return false;
			}
			else if (i5 == 1)
			{
				return false;
			}
			else
			{
				if (i5 == 2)
				{
					++i4;
				}

				if (i5 == 3)
				{
					--i4;
				}

				if (i5 == 4)
				{
					++i2;
				}

				if (i5 == 5)
				{
					--i2;
				}

				return isValidSupportBlock(world1.getBlockId(i2, i3, i4));
			}
		}

		public static bool isTrapdoorOpen(int i0)
		{
			return (i0 & 4) != 0;
		}

		private static bool isValidSupportBlock(int i0)
		{
			if (i0 <= 0)
			{
				return false;
			}
			else
			{
				Block block1 = Block.blocksList[i0];
				return block1 != null && block1.blockMaterial.Opaque && block1.renderAsNormalBlock() || block1 == Block.glowStone;
			}
		}
	}

}