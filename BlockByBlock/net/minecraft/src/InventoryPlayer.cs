using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class InventoryPlayer : IInventory
	{
		public ItemStack[] mainInventory = new ItemStack[36];
		public ItemStack[] armorInventory = new ItemStack[4];
		public int currentItem = 0;
		public EntityPlayer player;
		private ItemStack itemStack;
		public bool inventoryChanged = false;

		public InventoryPlayer(EntityPlayer entityPlayer1)
		{
			this.player = entityPlayer1;
		}

		public virtual ItemStack CurrentItem
		{
			get
			{
				return this.currentItem < 9 && this.currentItem >= 0 ? this.mainInventory[this.currentItem] : null;
			}
		}

		private int getInventorySlotContainItem(int i1)
		{
			for (int i2 = 0; i2 < this.mainInventory.Length; ++i2)
			{
				if (this.mainInventory[i2] != null && this.mainInventory[i2].itemID == i1)
				{
					return i2;
				}
			}

			return -1;
		}

		private int getInventorySlotContainItemAndDamage(int i1, int i2)
		{
			for (int i3 = 0; i3 < this.mainInventory.Length; ++i3)
			{
				if (this.mainInventory[i3] != null && this.mainInventory[i3].itemID == i1 && this.mainInventory[i3].ItemDamage == i2)
				{
					return i3;
				}
			}

			return -1;
		}

		private int storeItemStack(ItemStack itemStack1)
		{
			for (int i2 = 0; i2 < this.mainInventory.Length; ++i2)
			{
				if (this.mainInventory[i2] != null && this.mainInventory[i2].itemID == itemStack1.itemID && this.mainInventory[i2].Stackable && this.mainInventory[i2].stackSize < this.mainInventory[i2].MaxStackSize && this.mainInventory[i2].stackSize < this.InventoryStackLimit && (!this.mainInventory[i2].HasSubtypes || this.mainInventory[i2].ItemDamage == itemStack1.ItemDamage) && net.minecraft.src.ItemStack.func_46154_a(this.mainInventory[i2], itemStack1))
				{
					return i2;
				}
			}

			return -1;
		}

		private int FirstEmptyStack
		{
			get
			{
				for (int i1 = 0; i1 < this.mainInventory.Length; ++i1)
				{
					if (this.mainInventory[i1] == null)
					{
						return i1;
					}
				}
    
				return -1;
			}
		}

		public virtual void setCurrentItem(int i1, int i2, bool z3, bool z4)
		{
			bool z5 = true;
			int i7;
			if (z3)
			{
				i7 = this.getInventorySlotContainItemAndDamage(i1, i2);
			}
			else
			{
				i7 = this.getInventorySlotContainItem(i1);
			}

			if (i7 >= 0 && i7 < 9)
			{
				this.currentItem = i7;
			}
			else
			{
				if (z4 && i1 > 0)
				{
					int i6 = this.FirstEmptyStack;
					if (i6 >= 0 && i6 < 9)
					{
						this.currentItem = i6;
					}

					this.func_52006_a(Item.itemsList[i1], i2);
				}

			}
		}

		public virtual void changeCurrentItem(int i1)
		{
			if (i1 > 0)
			{
				i1 = 1;
			}

			if (i1 < 0)
			{
				i1 = -1;
			}

			for (this.currentItem -= i1; this.currentItem < 0; this.currentItem += 9)
			{
			}

			while (this.currentItem >= 9)
			{
				this.currentItem -= 9;
			}

		}

		public virtual void func_52006_a(Item item1, int i2)
		{
			if (item1 != null)
			{
				int i3 = this.getInventorySlotContainItemAndDamage(item1.shiftedIndex, i2);
				if (i3 >= 0)
				{
					this.mainInventory[i3] = this.mainInventory[this.currentItem];
				}

				this.mainInventory[this.currentItem] = new ItemStack(Item.itemsList[item1.shiftedIndex], 1, i2);
			}

		}

		private int storePartialItemStack(ItemStack itemStack1)
		{
			int i2 = itemStack1.itemID;
			int i3 = itemStack1.stackSize;
			int i4;
			if (itemStack1.MaxStackSize == 1)
			{
				i4 = this.FirstEmptyStack;
				if (i4 < 0)
				{
					return i3;
				}
				else
				{
					if (this.mainInventory[i4] == null)
					{
						this.mainInventory[i4] = net.minecraft.src.ItemStack.copyItemStack(itemStack1);
					}

					return 0;
				}
			}
			else
			{
				i4 = this.storeItemStack(itemStack1);
				if (i4 < 0)
				{
					i4 = this.FirstEmptyStack;
				}

				if (i4 < 0)
				{
					return i3;
				}
				else
				{
					if (this.mainInventory[i4] == null)
					{
						this.mainInventory[i4] = new ItemStack(i2, 0, itemStack1.ItemDamage);
						if (itemStack1.hasTagCompound())
						{
							this.mainInventory[i4].TagCompound = (NBTTagCompound)itemStack1.TagCompound.copy();
						}
					}

					int i5 = i3;
					if (i3 > this.mainInventory[i4].MaxStackSize - this.mainInventory[i4].stackSize)
					{
						i5 = this.mainInventory[i4].MaxStackSize - this.mainInventory[i4].stackSize;
					}

					if (i5 > this.InventoryStackLimit - this.mainInventory[i4].stackSize)
					{
						i5 = this.InventoryStackLimit - this.mainInventory[i4].stackSize;
					}

					if (i5 == 0)
					{
						return i3;
					}
					else
					{
						i3 -= i5;
						this.mainInventory[i4].stackSize += i5;
						this.mainInventory[i4].animationsToGo = 5;
						return i3;
					}
				}
			}
		}

		public virtual void decrementAnimations()
		{
			for (int i1 = 0; i1 < this.mainInventory.Length; ++i1)
			{
				if (this.mainInventory[i1] != null)
				{
					this.mainInventory[i1].updateAnimation(this.player.worldObj, this.player, i1, this.currentItem == i1);
				}
			}

		}

		public virtual bool consumeInventoryItem(int i1)
		{
			int i2 = this.getInventorySlotContainItem(i1);
			if (i2 < 0)
			{
				return false;
			}
			else
			{
				if (--this.mainInventory[i2].stackSize <= 0)
				{
					this.mainInventory[i2] = null;
				}

				return true;
			}
		}

		public virtual bool hasItem(int i1)
		{
			int i2 = this.getInventorySlotContainItem(i1);
			return i2 >= 0;
		}

		public virtual bool addItemStackToInventory(ItemStack itemStack1)
		{
			int i2;
			if (itemStack1.ItemDamaged)
			{
				i2 = this.FirstEmptyStack;
				if (i2 >= 0)
				{
					this.mainInventory[i2] = net.minecraft.src.ItemStack.copyItemStack(itemStack1);
					this.mainInventory[i2].animationsToGo = 5;
					itemStack1.stackSize = 0;
					return true;
				}
				else if (this.player.capabilities.isCreativeMode)
				{
					itemStack1.stackSize = 0;
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				do
				{
					i2 = itemStack1.stackSize;
					itemStack1.stackSize = this.storePartialItemStack(itemStack1);
				} while (itemStack1.stackSize > 0 && itemStack1.stackSize < i2);

				if (itemStack1.stackSize == i2 && this.player.capabilities.isCreativeMode)
				{
					itemStack1.stackSize = 0;
					return true;
				}
				else
				{
					return itemStack1.stackSize < i2;
				}
			}
		}

		public virtual ItemStack decrStackSize(int i1, int i2)
		{
			ItemStack[] itemStack3 = this.mainInventory;
			if (i1 >= this.mainInventory.Length)
			{
				itemStack3 = this.armorInventory;
				i1 -= this.mainInventory.Length;
			}

			if (itemStack3[i1] != null)
			{
				ItemStack itemStack4;
				if (itemStack3[i1].stackSize <= i2)
				{
					itemStack4 = itemStack3[i1];
					itemStack3[i1] = null;
					return itemStack4;
				}
				else
				{
					itemStack4 = itemStack3[i1].splitStack(i2);
					if (itemStack3[i1].stackSize == 0)
					{
						itemStack3[i1] = null;
					}

					return itemStack4;
				}
			}
			else
			{
				return null;
			}
		}

		public virtual ItemStack getStackInSlotOnClosing(int i1)
		{
			ItemStack[] itemStack2 = this.mainInventory;
			if (i1 >= this.mainInventory.Length)
			{
				itemStack2 = this.armorInventory;
				i1 -= this.mainInventory.Length;
			}

			if (itemStack2[i1] != null)
			{
				ItemStack itemStack3 = itemStack2[i1];
				itemStack2[i1] = null;
				return itemStack3;
			}
			else
			{
				return null;
			}
		}

		public virtual void setInventorySlotContents(int i1, ItemStack itemStack2)
		{
			ItemStack[] itemStack3 = this.mainInventory;
			if (i1 >= itemStack3.Length)
			{
				i1 -= itemStack3.Length;
				itemStack3 = this.armorInventory;
			}

			itemStack3[i1] = itemStack2;
		}

		public virtual float getStrVsBlock(Block block1)
		{
			float f2 = 1.0F;
			if (this.mainInventory[this.currentItem] != null)
			{
				f2 *= this.mainInventory[this.currentItem].getStrVsBlock(block1);
			}

			return f2;
		}

		public virtual NBTTagList writeToNBT(NBTTagList nBTTagList1)
		{
			int i2;
			NBTTagCompound nBTTagCompound3;
			for (i2 = 0; i2 < this.mainInventory.Length; ++i2)
			{
				if (this.mainInventory[i2] != null)
				{
					nBTTagCompound3 = new NBTTagCompound();
					nBTTagCompound3.setByte("Slot", (sbyte)i2);
					this.mainInventory[i2].writeToNBT(nBTTagCompound3);
					nBTTagList1.appendTag(nBTTagCompound3);
				}
			}

			for (i2 = 0; i2 < this.armorInventory.Length; ++i2)
			{
				if (this.armorInventory[i2] != null)
				{
					nBTTagCompound3 = new NBTTagCompound();
					nBTTagCompound3.setByte("Slot", (sbyte)(i2 + 100));
					this.armorInventory[i2].writeToNBT(nBTTagCompound3);
					nBTTagList1.appendTag(nBTTagCompound3);
				}
			}

			return nBTTagList1;
		}

		public virtual void readFromNBT(NBTTagList nBTTagList1)
		{
			this.mainInventory = new ItemStack[36];
			this.armorInventory = new ItemStack[4];

			for (int i2 = 0; i2 < nBTTagList1.tagCount(); ++i2)
			{
				NBTTagCompound nBTTagCompound3 = (NBTTagCompound)nBTTagList1.tagAt(i2);
				int i4 = nBTTagCompound3.getByte("Slot") & 255;
				ItemStack itemStack5 = net.minecraft.src.ItemStack.loadItemStackFromNBT(nBTTagCompound3);
				if (itemStack5 != null)
				{
					if (i4 >= 0 && i4 < this.mainInventory.Length)
					{
						this.mainInventory[i4] = itemStack5;
					}

					if (i4 >= 100 && i4 < this.armorInventory.Length + 100)
					{
						this.armorInventory[i4 - 100] = itemStack5;
					}
				}
			}

		}

		public virtual int SizeInventory
		{
			get
			{
				return this.mainInventory.Length + 4;
			}
		}

		public virtual ItemStack getStackInSlot(int i1)
		{
			ItemStack[] itemStack2 = this.mainInventory;
			if (i1 >= itemStack2.Length)
			{
				i1 -= itemStack2.Length;
				itemStack2 = this.armorInventory;
			}

			return itemStack2[i1];
		}

		public virtual string InvName
		{
			get
			{
				return "container.inventory";
			}
		}

		public virtual int InventoryStackLimit
		{
			get
			{
				return 64;
			}
		}

		public virtual int getDamageVsEntity(Entity entity1)
		{
			ItemStack itemStack2 = this.getStackInSlot(this.currentItem);
			return itemStack2 != null ? itemStack2.getDamageVsEntity(entity1) : 1;
		}

		public virtual bool canHarvestBlock(Block block1)
		{
			if (block1.blockMaterial.Harvestable)
			{
				return true;
			}
			else
			{
				ItemStack itemStack2 = this.getStackInSlot(this.currentItem);
				return itemStack2 != null ? itemStack2.canHarvestBlock(block1) : false;
			}
		}

		public virtual ItemStack armorItemInSlot(int i1)
		{
			return this.armorInventory[i1];
		}

		public virtual int TotalArmorValue
		{
			get
			{
				int i1 = 0;
    
				for (int i2 = 0; i2 < this.armorInventory.Length; ++i2)
				{
					if (this.armorInventory[i2] != null && this.armorInventory[i2].Item is ItemArmor)
					{
						int i3 = ((ItemArmor)this.armorInventory[i2].Item).damageReduceAmount;
						i1 += i3;
					}
				}
    
				return i1;
			}
		}

		public virtual void damageArmor(int i1)
		{
			i1 /= 4;
			if (i1 < 1)
			{
				i1 = 1;
			}

			for (int i2 = 0; i2 < this.armorInventory.Length; ++i2)
			{
				if (this.armorInventory[i2] != null && this.armorInventory[i2].Item is ItemArmor)
				{
					this.armorInventory[i2].damageItem(i1, this.player);
					if (this.armorInventory[i2].stackSize == 0)
					{
						this.armorInventory[i2].onItemDestroyedByUse(this.player);
						this.armorInventory[i2] = null;
					}
				}
			}

		}

		public virtual void dropAllItems()
		{
			int i1;
			for (i1 = 0; i1 < this.mainInventory.Length; ++i1)
			{
				if (this.mainInventory[i1] != null)
				{
					this.player.dropPlayerItemWithRandomChoice(this.mainInventory[i1], true);
					this.mainInventory[i1] = null;
				}
			}

			for (i1 = 0; i1 < this.armorInventory.Length; ++i1)
			{
				if (this.armorInventory[i1] != null)
				{
					this.player.dropPlayerItemWithRandomChoice(this.armorInventory[i1], true);
					this.armorInventory[i1] = null;
				}
			}

		}

		public virtual void onInventoryChanged()
		{
			this.inventoryChanged = true;
		}

		public virtual ItemStack ItemStack
		{
			set
			{
				this.itemStack = value;
				this.player.onItemStackChanged(value);
			}
			get
			{
				return this.itemStack;
			}
		}


		public virtual bool isUseableByPlayer(EntityPlayer entityPlayer1)
		{
			return this.player.isDead ? false : entityPlayer1.getDistanceSqToEntity(this.player) <= 64.0D;
		}

		public virtual bool hasItemStack(ItemStack itemStack1)
		{
			int i2;
			for (i2 = 0; i2 < this.armorInventory.Length; ++i2)
			{
				if (this.armorInventory[i2] != null && this.armorInventory[i2].isStackEqual(itemStack1))
				{
					return true;
				}
			}

			for (i2 = 0; i2 < this.mainInventory.Length; ++i2)
			{
				if (this.mainInventory[i2] != null && this.mainInventory[i2].isStackEqual(itemStack1))
				{
					return true;
				}
			}

			return false;
		}

		public virtual void openChest()
		{
		}

		public virtual void closeChest()
		{
		}

		public virtual void copyInventory(InventoryPlayer inventoryPlayer1)
		{
			int i2;
			for (i2 = 0; i2 < this.mainInventory.Length; ++i2)
			{
				this.mainInventory[i2] = net.minecraft.src.ItemStack.copyItemStack(inventoryPlayer1.mainInventory[i2]);
			}

			for (i2 = 0; i2 < this.armorInventory.Length; ++i2)
			{
				this.armorInventory[i2] = net.minecraft.src.ItemStack.copyItemStack(inventoryPlayer1.armorInventory[i2]);
			}

		}
	}

}