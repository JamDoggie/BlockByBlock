using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class InventoryBasic : IInventory
	{
		private string inventoryTitle;
		private int slotsCount;
		private ItemStack[] inventoryContents;
		private System.Collections.IList field_20073_d;

		public InventoryBasic(string string1, int i2)
		{
			this.inventoryTitle = string1;
			this.slotsCount = i2;
			this.inventoryContents = new ItemStack[i2];
        }

		public virtual ItemStack getStackInSlot(int i1)
		{
			return this.inventoryContents[i1];
		}

		public virtual ItemStack decrStackSize(int i1, int i2)
		{
			if (this.inventoryContents[i1] != null)
			{
				ItemStack itemStack3;
				if (this.inventoryContents[i1].stackSize <= i2)
				{
					itemStack3 = this.inventoryContents[i1];
					this.inventoryContents[i1] = null;
					this.onInventoryChanged();
					return itemStack3;
				}
				else
				{
					itemStack3 = this.inventoryContents[i1].splitStack(i2);
					if (this.inventoryContents[i1].stackSize == 0)
					{
						this.inventoryContents[i1] = null;
					}

					this.onInventoryChanged();
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
			if (this.inventoryContents[i1] != null)
			{
				ItemStack itemStack2 = this.inventoryContents[i1];
				this.inventoryContents[i1] = null;
				return itemStack2;
			}
			else
			{
				return null;
			}
		}

		public virtual void setInventorySlotContents(int i1, ItemStack itemStack2)
		{
			this.inventoryContents[i1] = itemStack2;
			if (itemStack2 != null && itemStack2.stackSize > this.InventoryStackLimit)
			{
				itemStack2.stackSize = this.InventoryStackLimit;
			}

			this.onInventoryChanged();
		}

		public virtual int SizeInventory
		{
			get
			{
				return this.slotsCount;
			}
		}

		public virtual string InvName
		{
			get
			{
				return this.inventoryTitle;
			}
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
			if (this.field_20073_d != null)
			{
				for (int i1 = 0; i1 < this.field_20073_d.Count; ++i1)
				{
					((IInvBasic)this.field_20073_d[i1]).onInventoryChanged(this);
				}
			}

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