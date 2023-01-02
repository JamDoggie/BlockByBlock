using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class TileEntityFurnace : TileEntity, IInventory
	{
		private ItemStack[] furnaceItemStacks = new ItemStack[3];
		public int furnaceBurnTime = 0;
		public int currentItemBurnTime = 0;
		public int furnaceCookTime = 0;

		public virtual int SizeInventory
		{
			get
			{
				return this.furnaceItemStacks.Length;
			}
		}

		public virtual ItemStack getStackInSlot(int i1)
		{
			return this.furnaceItemStacks[i1];
		}

		public virtual ItemStack decrStackSize(int i1, int i2)
		{
			if (this.furnaceItemStacks[i1] != null)
			{
				ItemStack itemStack3;
				if (this.furnaceItemStacks[i1].stackSize <= i2)
				{
					itemStack3 = this.furnaceItemStacks[i1];
					this.furnaceItemStacks[i1] = null;
					return itemStack3;
				}
				else
				{
					itemStack3 = this.furnaceItemStacks[i1].splitStack(i2);
					if (this.furnaceItemStacks[i1].stackSize == 0)
					{
						this.furnaceItemStacks[i1] = null;
					}

					return itemStack3;
				}
			}
			else
			{
				return null;
			}
		}

		public virtual ItemStack getStackInSlotOnClosing(int i1)
		{
			if (this.furnaceItemStacks[i1] != null)
			{
				ItemStack itemStack2 = this.furnaceItemStacks[i1];
				this.furnaceItemStacks[i1] = null;
				return itemStack2;
			}
			else
			{
				return null;
			}
		}

		public virtual void setInventorySlotContents(int i1, ItemStack itemStack2)
		{
			this.furnaceItemStacks[i1] = itemStack2;
			if (itemStack2 != null && itemStack2.stackSize > this.InventoryStackLimit)
			{
				itemStack2.stackSize = this.InventoryStackLimit;
			}

		}

		public virtual string InvName
		{
			get
			{
				return "container.furnace";
			}
		}

		public override void readFromNBT(NBTTagCompound nBTTagCompound1)
		{
			base.readFromNBT(nBTTagCompound1);
			NBTTagList nBTTagList2 = nBTTagCompound1.getTagList("Items");
			this.furnaceItemStacks = new ItemStack[this.SizeInventory];

			for (int i3 = 0; i3 < nBTTagList2.tagCount(); ++i3)
			{
				NBTTagCompound nBTTagCompound4 = (NBTTagCompound)nBTTagList2.tagAt(i3);
				sbyte b5 = nBTTagCompound4.getByte("Slot");
				if (b5 >= 0 && b5 < this.furnaceItemStacks.Length)
				{
					this.furnaceItemStacks[b5] = ItemStack.loadItemStackFromNBT(nBTTagCompound4);
				}
			}

			this.furnaceBurnTime = nBTTagCompound1.getShort("BurnTime");
			this.furnaceCookTime = nBTTagCompound1.getShort("CookTime");
			this.currentItemBurnTime = getItemBurnTime(this.furnaceItemStacks[1]);
		}

		public override void writeToNBT(NBTTagCompound nBTTagCompound1)
		{
			base.writeToNBT(nBTTagCompound1);
			nBTTagCompound1.setShort("BurnTime", (short)this.furnaceBurnTime);
			nBTTagCompound1.setShort("CookTime", (short)this.furnaceCookTime);
			NBTTagList nBTTagList2 = new NBTTagList();

			for (int i3 = 0; i3 < this.furnaceItemStacks.Length; ++i3)
			{
				if (this.furnaceItemStacks[i3] != null)
				{
					NBTTagCompound nBTTagCompound4 = new NBTTagCompound();
					nBTTagCompound4.setByte("Slot", (sbyte)i3);
					this.furnaceItemStacks[i3].writeToNBT(nBTTagCompound4);
					nBTTagList2.appendTag(nBTTagCompound4);
				}
			}

			nBTTagCompound1.setTag("Items", nBTTagList2);
		}

		public virtual int InventoryStackLimit
		{
			get
			{
				return 64;
			}
		}

		public virtual int getCookProgressScaled(int i1)
		{
			return this.furnaceCookTime * i1 / 200;
		}

		public virtual int getBurnTimeRemainingScaled(int i1)
		{
			if (this.currentItemBurnTime == 0)
			{
				this.currentItemBurnTime = 200;
			}

			return this.furnaceBurnTime * i1 / this.currentItemBurnTime;
		}

		public virtual bool Burning
		{
			get
			{
				return this.furnaceBurnTime > 0;
			}
		}

		public override void updateEntity()
		{
			bool z1 = this.furnaceBurnTime > 0;
			bool z2 = false;
			if (this.furnaceBurnTime > 0)
			{
				--this.furnaceBurnTime;
			}

			if (!this.worldObj.isRemote)
			{
				if (this.furnaceBurnTime == 0 && this.canSmelt())
				{
					this.currentItemBurnTime = this.furnaceBurnTime = getItemBurnTime(this.furnaceItemStacks[1]);
					if (this.furnaceBurnTime > 0)
					{
						z2 = true;
						if (this.furnaceItemStacks[1] != null)
						{
							--this.furnaceItemStacks[1].stackSize;
							if (this.furnaceItemStacks[1].stackSize == 0)
							{
								this.furnaceItemStacks[1] = null;
							}
						}
					}
				}

				if (this.Burning && this.canSmelt())
				{
					++this.furnaceCookTime;
					if (this.furnaceCookTime == 200)
					{
						this.furnaceCookTime = 0;
						this.smeltItem();
						z2 = true;
					}
				}
				else
				{
					this.furnaceCookTime = 0;
				}

				if (z1 != this.furnaceBurnTime > 0)
				{
					z2 = true;
					BlockFurnace.updateFurnaceBlockState(this.furnaceBurnTime > 0, this.worldObj, this.xCoord, this.yCoord, this.zCoord);
				}
			}

			if (z2)
			{
				this.onInventoryChanged();
			}

		}

		private bool canSmelt()
		{
			if (this.furnaceItemStacks[0] == null)
			{
				return false;
			}
			else
			{
				ItemStack itemStack1 = FurnaceRecipes.smelting().getSmeltingResult(this.furnaceItemStacks[0].Item.shiftedIndex);
				return itemStack1 == null ? false : (this.furnaceItemStacks[2] == null ? true : (!this.furnaceItemStacks[2].isItemEqual(itemStack1) ? false : (this.furnaceItemStacks[2].stackSize < this.InventoryStackLimit && this.furnaceItemStacks[2].stackSize < this.furnaceItemStacks[2].MaxStackSize ? true : this.furnaceItemStacks[2].stackSize < itemStack1.MaxStackSize)));
			}
		}

		public virtual void smeltItem()
		{
			if (this.canSmelt())
			{
				ItemStack itemStack1 = FurnaceRecipes.smelting().getSmeltingResult(this.furnaceItemStacks[0].Item.shiftedIndex);
				if (this.furnaceItemStacks[2] == null)
				{
					this.furnaceItemStacks[2] = itemStack1.copy();
				}
				else if (this.furnaceItemStacks[2].itemID == itemStack1.itemID)
				{
					++this.furnaceItemStacks[2].stackSize;
				}

				--this.furnaceItemStacks[0].stackSize;
				if (this.furnaceItemStacks[0].stackSize <= 0)
				{
					this.furnaceItemStacks[0] = null;
				}

			}
		}

		public static int getItemBurnTime(ItemStack itemStack0)
		{
			if (itemStack0 == null)
			{
				return 0;
			}
			else
			{
				int i1 = itemStack0.Item.shiftedIndex;
				return i1 < 256 && Block.blocksList[i1].blockMaterial == Material.wood ? 300 : (i1 == Item.stick.shiftedIndex ? 100 : (i1 == Item.coal.shiftedIndex ? 1600 : (i1 == Item.bucketLava.shiftedIndex ? 20000 : (i1 == Block.sapling.blockID ? 100 : (i1 == Item.blazeRod.shiftedIndex ? 2400 : 0)))));
			}
		}

		public static bool func_52005_b(ItemStack itemStack0)
		{
			return getItemBurnTime(itemStack0) > 0;
		}

		public virtual bool isUseableByPlayer(EntityPlayer entityPlayer1)
		{
			return this.worldObj.getBlockTileEntity(this.xCoord, this.yCoord, this.zCoord) != this ? false : entityPlayer1.getDistanceSq((double)this.xCoord + 0.5D, (double)this.yCoord + 0.5D, (double)this.zCoord + 0.5D) <= 64.0D;
		}

		public virtual void openChest()
		{
		}

		public virtual void closeChest()
		{
		}
	}

}