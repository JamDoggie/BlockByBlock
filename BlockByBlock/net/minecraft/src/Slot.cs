namespace net.minecraft.src
{
	public class Slot
	{
		private readonly int slotIndex;
		public readonly IInventory inventory;
		public int slotNumber;
		public int xDisplayPosition;
		public int yDisplayPosition;

		public Slot(IInventory iInventory1, int i2, int i3, int i4)
		{
			this.inventory = iInventory1;
			this.slotIndex = i2;
			this.xDisplayPosition = i3;
			this.yDisplayPosition = i4;
		}

		public virtual void func_48433_a(ItemStack itemStack1, ItemStack itemStack2)
		{
			if (itemStack1 != null && itemStack2 != null)
			{
				if (itemStack1.itemID == itemStack2.itemID)
				{
					int i3 = itemStack2.stackSize - itemStack1.stackSize;
					if (i3 > 0)
					{
						this.func_48435_a(itemStack1, i3);
					}

				}
			}
		}

		protected internal virtual void func_48435_a(ItemStack itemStack1, int i2)
		{
		}

		protected internal virtual void func_48434_c(ItemStack itemStack1)
		{
		}

		public virtual void onPickupFromSlot(ItemStack itemStack1)
		{
			this.onSlotChanged();
		}

		public virtual bool isItemValid(ItemStack itemStack1)
		{
			return true;
		}

		public virtual ItemStack Stack
		{
			get
			{
				return this.inventory.getStackInSlot(this.slotIndex);
			}
		}

		public virtual bool HasStack
		{
			get
			{
				return this.Stack != null;
			}
		}

		public virtual void putStack(ItemStack itemStack1)
		{
			this.inventory.setInventorySlotContents(this.slotIndex, itemStack1);
			this.onSlotChanged();
		}

		public virtual void onSlotChanged()
		{
			this.inventory.onInventoryChanged();
		}

		public virtual int SlotStackLimit
		{
			get
			{
				return this.inventory.InventoryStackLimit;
			}
		}

		public virtual int BackgroundIconIndex
		{
			get
			{
				return -1;
			}
		}

		public virtual ItemStack decrStackSize(int i1)
		{
			return this.inventory.decrStackSize(this.slotIndex, i1);
		}
	}

}