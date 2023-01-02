using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class InventoryCraftResult : IInventory
	{
		private ItemStack[] stackResult = new ItemStack[1];

		public virtual int SizeInventory
		{
			get
			{
				return 1;
			}
		}

		public virtual ItemStack getStackInSlot(int i1)
		{
			return this.stackResult[i1];
		}

		public virtual string InvName
		{
			get
			{
				return "Result";
			}
		}

		public virtual ItemStack decrStackSize(int i1, int i2)
		{
			if (this.stackResult[i1] != null)
			{
				ItemStack itemStack3 = this.stackResult[i1];
				this.stackResult[i1] = null;
				return itemStack3;
			}
			else
			{
				return null;
			}
		}

		public virtual ItemStack getStackInSlotOnClosing(int i1)
		{
			if (this.stackResult[i1] != null)
			{
				ItemStack itemStack2 = this.stackResult[i1];
				this.stackResult[i1] = null;
				return itemStack2;
			}
			else
			{
				return null;
			}
		}

		public virtual void setInventorySlotContents(int i1, ItemStack itemStack2)
		{
			this.stackResult[i1] = itemStack2;
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