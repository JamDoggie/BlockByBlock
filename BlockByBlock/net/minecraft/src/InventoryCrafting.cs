using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class InventoryCrafting : IInventory
	{
		private ItemStack[] stackList;
		private int inventoryWidth;
		private Container eventHandler;

		public InventoryCrafting(Container container1, int i2, int i3)
		{
			int i4 = i2 * i3;
			this.stackList = new ItemStack[i4];
			this.eventHandler = container1;
			this.inventoryWidth = i2;
		}

		public virtual int SizeInventory
		{
			get
			{
				return this.stackList.Length;
			}
		}

		public virtual ItemStack getStackInSlot(int i1)
		{
			return i1 >= this.SizeInventory ? null : this.stackList[i1];
		}

		public virtual ItemStack getStackInRowAndColumn(int i1, int i2)
		{
			if (i1 >= 0 && i1 < this.inventoryWidth)
			{
				int i3 = i1 + i2 * this.inventoryWidth;
				return this.getStackInSlot(i3);
			}
			else
			{
				return null;
			}
		}

		public virtual string InvName
		{
			get
			{
				return "container.crafting";
			}
		}

		public virtual ItemStack getStackInSlotOnClosing(int i1)
		{
			if (this.stackList[i1] != null)
			{
				ItemStack itemStack2 = this.stackList[i1];
				this.stackList[i1] = null;
				return itemStack2;
			}
			else
			{
				return null;
			}
		}

		public virtual ItemStack decrStackSize(int i1, int i2)
		{
			if (this.stackList[i1] != null)
			{
				ItemStack itemStack3;
				if (this.stackList[i1].stackSize <= i2)
				{
					itemStack3 = this.stackList[i1];
					this.stackList[i1] = null;
					this.eventHandler.onCraftMatrixChanged(this);
					return itemStack3;
				}
				else
				{
					itemStack3 = this.stackList[i1].splitStack(i2);
					if (this.stackList[i1].stackSize == 0)
					{
						this.stackList[i1] = null;
					}

					this.eventHandler.onCraftMatrixChanged(this);
					return itemStack3;
				}
			}
			else
			{
				return null;
			}
		}

		public virtual void setInventorySlotContents(int i1, ItemStack itemStack2)
		{
			this.stackList[i1] = itemStack2;
			this.eventHandler.onCraftMatrixChanged(this);
		}

		public virtual int InventoryStackLimit
		{
			get
			{
				return 64;
			}
		}

		public virtual void onInventoryChanged()
		{
		}

		public virtual bool isUseableByPlayer(EntityPlayer entityPlayer1)
		{
			return true;
		}

		public virtual void openChest()
		{
		}

		public virtual void closeChest()
		{
		}
	}

}