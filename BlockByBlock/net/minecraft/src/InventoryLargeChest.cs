using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class InventoryLargeChest : IInventory
	{
		private string name;
		private IInventory upperChest;
		private IInventory lowerChest;

		public InventoryLargeChest(string string1, IInventory iInventory2, IInventory iInventory3)
		{
			this.name = string1;
			if (iInventory2 == null)
			{
				iInventory2 = iInventory3;
			}

			if (iInventory3 == null)
			{
				iInventory3 = iInventory2;
			}

			this.upperChest = iInventory2;
			this.lowerChest = iInventory3;
		}

		public virtual int SizeInventory
		{
			get
			{
				return this.upperChest.SizeInventory + this.lowerChest.SizeInventory;
			}
		}

		public virtual string InvName
		{
			get
			{
				return this.name;
			}
		}

		public virtual ItemStack getStackInSlot(int i1)
		{
			return i1 >= this.upperChest.SizeInventory ? this.lowerChest.getStackInSlot(i1 - this.upperChest.SizeInventory) : this.upperChest.getStackInSlot(i1);
		}

		public virtual ItemStack decrStackSize(int i1, int i2)
		{
			return i1 >= this.upperChest.SizeInventory ? this.lowerChest.decrStackSize(i1 - this.upperChest.SizeInventory, i2) : this.upperChest.decrStackSize(i1, i2);
		}

		public virtual ItemStack getStackInSlotOnClosing(int i1)
		{
			return i1 >= this.upperChest.SizeInventory ? this.lowerChest.getStackInSlotOnClosing(i1 - this.upperChest.SizeInventory) : this.upperChest.getStackInSlotOnClosing(i1);
		}

		public virtual void setInventorySlotContents(int i1, ItemStack itemStack2)
		{
			if (i1 >= this.upperChest.SizeInventory)
			{
				this.lowerChest.setInventorySlotContents(i1 - this.upperChest.SizeInventory, itemStack2);
			}
			else
			{
				this.upperChest.setInventorySlotContents(i1, itemStack2);
			}

		}

		public virtual int InventoryStackLimit
		{
			get
			{
				return this.upperChest.InventoryStackLimit;
			}
		}

		public virtual void onInventoryChanged()
		{
			this.upperChest.onInventoryChanged();
			this.lowerChest.onInventoryChanged();
		}

		public virtual bool isUseableByPlayer(EntityPlayer entityPlayer1)
		{
			return this.upperChest.isUseableByPlayer(entityPlayer1) && this.lowerChest.isUseableByPlayer(entityPlayer1);
		}

		public virtual void openChest()
		{
			this.upperChest.openChest();
			this.lowerChest.openChest();
		}

		public virtual void closeChest()
		{
			this.upperChest.closeChest();
			this.lowerChest.closeChest();
		}
	}

}