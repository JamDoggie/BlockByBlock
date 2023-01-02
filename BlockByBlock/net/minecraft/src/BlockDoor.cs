using BlockByBlock.java_extensions;
using net.minecraft.client.entity;
using System;

namespace net.minecraft.src
{

    public class BlockDoor : Block
	{
		protected internal BlockDoor(int i1, Material material2) : base(i1, material2)
		{
			this.blockIndexInTexture = 97;
			if (material2 == Material.iron)
			{
				++this.blockIndexInTexture;
			}

			float f3 = 0.5F;
			float f4 = 1.0F;
			this.setBlockBounds(0.5F - f3, 0.0F, 0.5F - f3, 0.5F + f3, f4, 0.5F + f3);
		}

		public override int getBlockTexture(IBlockAccess iBlockAccess1, int i2, int i3, int i4, int i5)
		{
			if (i5 != 0 && i5 != 1)
			{
				int i6 = this.getFullMetadata(iBlockAccess1, i2, i3, i4);
				int i7 = this.blockIndexInTexture;
				if ((i6 & 8) != 0)
				{
					i7 -= 16;
				}

				int i8 = i6 & 3;
				bool z9 = (i6 & 4) != 0;
				if (!z9)
				{
					if (i8 == 0 && i5 == 5)
					{
						i7 = -i7;
					}
					else if (i8 == 1 && i5 == 3)
					{
						i7 = -i7;
					}
					else if (i8 == 2 && i5 == 4)
					{
						i7 = -i7;
					}
					else if (i8 == 3 && i5 == 2)
					{
						i7 = -i7;
					}

					if ((i6 & 16) != 0)
					{
						i7 = -i7;
					}
				}
				else if (i8 == 0 && i5 == 2)
				{
					i7 = -i7;
				}
				else if (i8 == 1 && i5 == 5)
				{
					i7 = -i7;
				}
				else if (i8 == 2 && i5 == 3)
				{
					i7 = -i7;
				}
				else if (i8 == 3 && i5 == 4)
				{
					i7 = -i7;
				}

				return i7;
			}
			else
			{
				return this.blockIndexInTexture;
			}
		}

		public override bool OpaqueCube
		{
			get
			{
				return false;
			}
		}

		public override bool getBlocksMovement(IBlockAccess iBlockAccess1, int i2, int i3, int i4)
		{
			int i5 = this.getFullMetadata(iBlockAccess1, i2, i3, i4);
			return (i5 & 4) != 0;
		}

		public override bool renderAsNormalBlock()
		{
			return false;
		}

		public override int RenderType
		{
			get
			{
				return 7;
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
			this.DoorRotation = this.getFullMetadata(iBlockAccess1, i2, i3, i4);
		}

		public virtual int getDoorOrientation(IBlockAccess iBlockAccess1, int i2, int i3, int i4)
		{
			return this.getFullMetadata(iBlockAccess1, i2, i3, i4) & 3;
		}

		public virtual bool func_48213_h(IBlockAccess iBlockAccess1, int i2, int i3, int i4)
		{
			return (this.getFullMetadata(iBlockAccess1, i2, i3, i4) & 4) != 0;
		}

		private int DoorRotation
		{
			set
			{
				float f2 = 0.1875F;
				this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 2.0F, 1.0F);
				int i3 = value & 3;
				bool z4 = (value & 4) != 0;
				bool z5 = (value & 16) != 0;
				if (i3 == 0)
				{
					if (!z4)
					{
						this.setBlockBounds(0.0F, 0.0F, 0.0F, f2, 1.0F, 1.0F);
					}
					else if (!z5)
					{
						this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, f2);
					}
					else
					{
						this.setBlockBounds(0.0F, 0.0F, 1.0F - f2, 1.0F, 1.0F, 1.0F);
					}
				}
				else if (i3 == 1)
				{
					if (!z4)
					{
						this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, f2);
					}
					else if (!z5)
					{
						this.setBlockBounds(1.0F - f2, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
					}
					else
					{
						this.setBlockBounds(0.0F, 0.0F, 0.0F, f2, 1.0F, 1.0F);
					}
				}
				else if (i3 == 2)
				{
					if (!z4)
					{
						this.setBlockBounds(1.0F - f2, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
					}
					else if (!z5)
					{
						this.setBlockBounds(0.0F, 0.0F, 1.0F - f2, 1.0F, 1.0F, 1.0F);
					}
					else
					{
						this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, f2);
					}
				}
				else if (i3 == 3)
				{
					if (!z4)
					{
						this.setBlockBounds(0.0F, 0.0F, 1.0F - f2, 1.0F, 1.0F, 1.0F);
					}
					else if (!z5)
					{
						this.setBlockBounds(0.0F, 0.0F, 0.0F, f2, 1.0F, 1.0F);
					}
					else
					{
						this.setBlockBounds(1.0F - f2, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
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
				int i6 = this.getFullMetadata(world1, i2, i3, i4);
				int i7 = i6 & 7;
				i7 ^= 4;
				if ((i6 & 8) != 0)
				{
					world1.setBlockMetadataWithNotify(i2, i3 - 1, i4, i7);
					world1.markBlocksDirty(i2, i3 - 1, i4, i2, i3, i4);
				}
				else
				{
					world1.setBlockMetadataWithNotify(i2, i3, i4, i7);
					world1.markBlocksDirty(i2, i3, i4, i2, i3, i4);
				}

				world1.playAuxSFXAtEntity(entityPlayer5, 1003, i2, i3, i4, 0);
				return true;
			}
		}

		public virtual void onPoweredBlockChange(World world1, int i2, int i3, int i4, bool z5)
		{
			int i6 = this.getFullMetadata(world1, i2, i3, i4);
			bool z7 = (i6 & 4) != 0;
			if (z7 != z5)
			{
				int i8 = i6 & 7;
				i8 ^= 4;
				if ((i6 & 8) != 0)
				{
					world1.setBlockMetadataWithNotify(i2, i3 - 1, i4, i8);
					world1.markBlocksDirty(i2, i3 - 1, i4, i2, i3, i4);
				}
				else
				{
					world1.setBlockMetadataWithNotify(i2, i3, i4, i8);
					world1.markBlocksDirty(i2, i3, i4, i2, i3, i4);
				}

				world1.playAuxSFXAtEntity((EntityPlayer)null, 1003, i2, i3, i4, 0);
			}
		}

		public override void onNeighborBlockChange(World world1, int i2, int i3, int i4, int i5)
		{
			int i6 = world1.getBlockMetadata(i2, i3, i4);
			if ((i6 & 8) != 0)
			{
				if (world1.getBlockId(i2, i3 - 1, i4) != this.blockID)
				{
					world1.setBlockWithNotify(i2, i3, i4, 0);
				}

				if (i5 > 0 && i5 != this.blockID)
				{
					this.onNeighborBlockChange(world1, i2, i3 - 1, i4, i5);
				}
			}
			else
			{
				bool z7 = false;
				if (world1.getBlockId(i2, i3 + 1, i4) != this.blockID)
				{
					world1.setBlockWithNotify(i2, i3, i4, 0);
					z7 = true;
				}

				if (!world1.isBlockNormalCube(i2, i3 - 1, i4))
				{
					world1.setBlockWithNotify(i2, i3, i4, 0);
					z7 = true;
					if (world1.getBlockId(i2, i3 + 1, i4) == this.blockID)
					{
						world1.setBlockWithNotify(i2, i3 + 1, i4, 0);
					}
				}

				if (z7)
				{
					if (!world1.isRemote)
					{
						this.dropBlockAsItem(world1, i2, i3, i4, i6, 0);
					}
				}
				else
				{
					bool z8 = world1.isBlockIndirectlyGettingPowered(i2, i3, i4) || world1.isBlockIndirectlyGettingPowered(i2, i3 + 1, i4);
					if ((z8 || i5 > 0 && Block.blocksList[i5].canProvidePower() || i5 == 0) && i5 != this.blockID)
					{
						this.onPoweredBlockChange(world1, i2, i3, i4, z8);
					}
				}
			}

		}

		public override int idDropped(int i1, RandomExtended random2, int i3)
		{
			return (i1 & 8) != 0 ? 0 : (this.blockMaterial == Material.iron ? Item.doorSteel.shiftedIndex : Item.doorWood.shiftedIndex);
		}

		public override MovingObjectPosition collisionRayTrace(World world1, int i2, int i3, int i4, Vec3D vec3D5, Vec3D vec3D6)
		{
			this.setBlockBoundsBasedOnState(world1, i2, i3, i4);
			return base.collisionRayTrace(world1, i2, i3, i4, vec3D5, vec3D6);
		}

		public override bool canPlaceBlockAt(World world1, int i2, int i3, int i4)
		{
			return i3 >= 255 ? false : world1.isBlockNormalCube(i2, i3 - 1, i4) && base.canPlaceBlockAt(world1, i2, i3, i4) && base.canPlaceBlockAt(world1, i2, i3 + 1, i4);
		}

		public override int MobilityFlag
		{
			get
			{
				return 1;
			}
		}

		public virtual int getFullMetadata(IBlockAccess iBlockAccess1, int i2, int i3, int i4)
		{
			int i5 = iBlockAccess1.getBlockMetadata(i2, i3, i4);
			bool z6 = (i5 & 8) != 0;
			int i7;
			int i8;
			if (z6)
			{
				i7 = iBlockAccess1.getBlockMetadata(i2, i3 - 1, i4);
				i8 = i5;
			}
			else
			{
				i7 = i5;
				i8 = iBlockAccess1.getBlockMetadata(i2, i3 + 1, i4);
			}

			bool z9 = (i8 & 1) != 0;
			int i10 = i7 & 7 | (z6 ? 8 : 0) | (z9 ? 16 : 0);
			return i10;
		}
	}

}