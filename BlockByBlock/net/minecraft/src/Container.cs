using System.Collections;
using System.Collections.Generic;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public abstract class Container
	{
		public System.Collections.IList inventoryItemStacks = new ArrayList();
		public System.Collections.IList inventorySlots = new ArrayList();
		public int windowId = 0;
		private short transactionID = 0;
		protected internal System.Collections.IList crafters = new ArrayList();
		private ISet<object> field_20918_b = new HashSet<object>();

		protected internal virtual void addSlot(Slot slot1)
		{
			slot1.slotNumber = this.inventorySlots.Count;
			this.inventorySlots.Add(slot1);
			this.inventoryItemStacks.Add((object)null);
		}

		public virtual void updateCraftingResults()
		{
			for (int i1 = 0; i1 < this.inventorySlots.Count; ++i1)
			{
				ItemStack itemStack2 = ((Slot)this.inventorySlots[i1]).Stack;
				ItemStack itemStack3 = (ItemStack)this.inventoryItemStacks[i1];
				if (!ItemStack.areItemStacksEqual(itemStack3, itemStack2))
				{
					itemStack3 = itemStack2 == null ? null : itemStack2.copy();
					this.inventoryItemStacks[i1] = itemStack3;

					for (int i4 = 0; i4 < this.crafters.Count; ++i4)
					{
						((ICrafting)this.crafters[i4]).updateCraftingInventorySlot(this, i1, itemStack3);
					}
				}
			}

		}

		public virtual bool enchantItem(EntityPlayer entityPlayer1, int i2)
		{
			return false;
		}

		public virtual Slot getSlot(int i1)
		{
			return (Slot)this.inventorySlots[i1];
		}

		public virtual ItemStack transferStackInSlot(int i1)
		{
			Slot slot2 = (Slot)this.inventorySlots[i1];
			return slot2 != null ? slot2.Stack : null;
		}

		public virtual ItemStack slotClick(int i1, int i2, bool z3, EntityPlayer entityPlayer4)
		{
			ItemStack itemStack5 = null;
			if (i2 > 1)
			{
				return null;
			}
			else
			{
				if (i2 == 0 || i2 == 1)
				{
					InventoryPlayer inventoryPlayer6 = entityPlayer4.inventory;
					if (i1 == -999)
					{
						if (inventoryPlayer6.ItemStack != null && i1 == -999)
						{
							if (i2 == 0)
							{
								entityPlayer4.dropPlayerItem(inventoryPlayer6.ItemStack);
								inventoryPlayer6.ItemStack = (ItemStack)null;
							}

							if (i2 == 1)
							{
								entityPlayer4.dropPlayerItem(inventoryPlayer6.ItemStack.splitStack(1));
								if (inventoryPlayer6.ItemStack.stackSize == 0)
								{
									inventoryPlayer6.ItemStack = (ItemStack)null;
								}
							}
						}
					}
					else if (z3)
					{
						ItemStack itemStack7 = this.transferStackInSlot(i1);
						if (itemStack7 != null)
						{
							int i8 = itemStack7.itemID;
							itemStack5 = itemStack7.copy();
							Slot slot9 = (Slot)this.inventorySlots[i1];
							if (slot9 != null && slot9.Stack != null && slot9.Stack.itemID == i8)
							{
								this.retrySlotClick(i1, i2, z3, entityPlayer4);
							}
						}
					}
					else
					{
						if (i1 < 0)
						{
							return null;
						}

						Slot slot12 = (Slot)this.inventorySlots[i1];
						if (slot12 != null)
						{
							slot12.onSlotChanged();
							ItemStack itemStack13 = slot12.Stack;
							ItemStack itemStack14 = inventoryPlayer6.ItemStack;
							if (itemStack13 != null)
							{
								itemStack5 = itemStack13.copy();
							}

							int i10;
							if (itemStack13 == null)
							{
								if (itemStack14 != null && slot12.isItemValid(itemStack14))
								{
									i10 = i2 == 0 ? itemStack14.stackSize : 1;
									if (i10 > slot12.SlotStackLimit)
									{
										i10 = slot12.SlotStackLimit;
									}

									slot12.putStack(itemStack14.splitStack(i10));
									if (itemStack14.stackSize == 0)
									{
										inventoryPlayer6.ItemStack = (ItemStack)null;
									}
								}
							}
							else if (itemStack14 == null)
							{
								i10 = i2 == 0 ? itemStack13.stackSize : (itemStack13.stackSize + 1) / 2;
								ItemStack itemStack11 = slot12.decrStackSize(i10);
								inventoryPlayer6.ItemStack = itemStack11;
								if (itemStack13.stackSize == 0)
								{
									slot12.putStack((ItemStack)null);
								}

								slot12.onPickupFromSlot(inventoryPlayer6.ItemStack);
							}
							else if (slot12.isItemValid(itemStack14))
							{
								if (itemStack13.itemID == itemStack14.itemID && (!itemStack13.HasSubtypes || itemStack13.ItemDamage == itemStack14.ItemDamage) && ItemStack.func_46154_a(itemStack13, itemStack14))
								{
									i10 = i2 == 0 ? itemStack14.stackSize : 1;
									if (i10 > slot12.SlotStackLimit - itemStack13.stackSize)
									{
										i10 = slot12.SlotStackLimit - itemStack13.stackSize;
									}

									if (i10 > itemStack14.MaxStackSize - itemStack13.stackSize)
									{
										i10 = itemStack14.MaxStackSize - itemStack13.stackSize;
									}

									itemStack14.splitStack(i10);
									if (itemStack14.stackSize == 0)
									{
										inventoryPlayer6.ItemStack = (ItemStack)null;
									}

									itemStack13.stackSize += i10;
								}
								else if (itemStack14.stackSize <= slot12.SlotStackLimit)
								{
									slot12.putStack(itemStack14);
									inventoryPlayer6.ItemStack = itemStack13;
								}
							}
							else if (itemStack13.itemID == itemStack14.itemID && itemStack14.MaxStackSize > 1 && (!itemStack13.HasSubtypes || itemStack13.ItemDamage == itemStack14.ItemDamage) && ItemStack.func_46154_a(itemStack13, itemStack14))
							{
								i10 = itemStack13.stackSize;
								if (i10 > 0 && i10 + itemStack14.stackSize <= itemStack14.MaxStackSize)
								{
									itemStack14.stackSize += i10;
									itemStack13 = slot12.decrStackSize(i10);
									if (itemStack13.stackSize == 0)
									{
										slot12.putStack((ItemStack)null);
									}

									slot12.onPickupFromSlot(inventoryPlayer6.ItemStack);
								}
							}
						}
					}
				}

				return itemStack5;
			}
		}

		protected internal virtual void retrySlotClick(int i1, int i2, bool z3, EntityPlayer entityPlayer4)
		{
			this.slotClick(i1, i2, z3, entityPlayer4);
		}

		public virtual void onCraftGuiClosed(EntityPlayer entityPlayer1)
		{
			InventoryPlayer inventoryPlayer2 = entityPlayer1.inventory;
			if (inventoryPlayer2.ItemStack != null)
			{
				entityPlayer1.dropPlayerItem(inventoryPlayer2.ItemStack);
				inventoryPlayer2.ItemStack = (ItemStack)null;
			}

		}

		public virtual void onCraftMatrixChanged(IInventory iInventory1)
		{
			this.updateCraftingResults();
		}

		public virtual void putStackInSlot(int i1, ItemStack itemStack2)
		{
			this.getSlot(i1).putStack(itemStack2);
		}

		public virtual void putStacksInSlots(ItemStack[] itemStack1)
		{
			for (int i2 = 0; i2 < itemStack1.Length; ++i2)
			{
				this.getSlot(i2).putStack(itemStack1[i2]);
			}

		}

		public virtual void updateProgressBar(int i1, int i2)
		{
		}

		public virtual short getNextTransactionID(InventoryPlayer inventoryPlayer1)
		{
			++this.transactionID;
			return this.transactionID;
		}

		public virtual void func_20113_a(short s1)
		{
		}

		public virtual void func_20110_b(short s1)
		{
		}

		public abstract bool canInteractWith(EntityPlayer entityPlayer1);

		protected internal virtual bool mergeItemStack(ItemStack itemStack1, int i2, int i3, bool z4)
		{
			bool z5 = false;
			int i6 = i2;
			if (z4)
			{
				i6 = i3 - 1;
			}

			Slot slot7;
			ItemStack itemStack8;
			if (itemStack1.Stackable)
			{
				while (itemStack1.stackSize > 0 && (!z4 && i6 < i3 || z4 && i6 >= i2))
				{
					slot7 = (Slot)this.inventorySlots[i6];
					itemStack8 = slot7.Stack;
					if (itemStack8 != null && itemStack8.itemID == itemStack1.itemID && (!itemStack1.HasSubtypes || itemStack1.ItemDamage == itemStack8.ItemDamage) && ItemStack.func_46154_a(itemStack1, itemStack8))
					{
						int i9 = itemStack8.stackSize + itemStack1.stackSize;
						if (i9 <= itemStack1.MaxStackSize)
						{
							itemStack1.stackSize = 0;
							itemStack8.stackSize = i9;
							slot7.onSlotChanged();
							z5 = true;
						}
						else if (itemStack8.stackSize < itemStack1.MaxStackSize)
						{
							itemStack1.stackSize -= itemStack1.MaxStackSize - itemStack8.stackSize;
							itemStack8.stackSize = itemStack1.MaxStackSize;
							slot7.onSlotChanged();
							z5 = true;
						}
					}

					if (z4)
					{
						--i6;
					}
					else
					{
						++i6;
					}
				}
			}

			if (itemStack1.stackSize > 0)
			{
				if (z4)
				{
					i6 = i3 - 1;
				}
				else
				{
					i6 = i2;
				}

				while (!z4 && i6 < i3 || z4 && i6 >= i2)
				{
					slot7 = (Slot)this.inventorySlots[i6];
					itemStack8 = slot7.Stack;
					if (itemStack8 == null)
					{
						slot7.putStack(itemStack1.copy());
						slot7.onSlotChanged();
						itemStack1.stackSize = 0;
						z5 = true;
						break;
					}

					if (z4)
					{
						--i6;
					}
					else
					{
						++i6;
					}
				}
			}

			return z5;
		}
	}

}