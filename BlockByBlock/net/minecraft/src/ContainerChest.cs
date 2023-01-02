using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class ContainerChest : Container
	{
		private IInventory lowerChestInventory;
		private int numRows;

		public ContainerChest(IInventory iInventory1, IInventory iInventory2)
		{
			this.lowerChestInventory = iInventory2;
			this.numRows = iInventory2.SizeInventory / 9;
			iInventory2.openChest();
			int i3 = (this.numRows - 4) * 18;

			int i4;
			int i5;
			for (i4 = 0; i4 < this.numRows; ++i4)
			{
				for (i5 = 0; i5 < 9; ++i5)
				{
					this.addSlot(new Slot(iInventory2, i5 + i4 * 9, 8 + i5 * 18, 18 + i4 * 18));
				}
			}

			for (i4 = 0; i4 < 3; ++i4)
			{
				for (i5 = 0; i5 < 9; ++i5)
				{
					this.addSlot(new Slot(iInventory1, i5 + i4 * 9 + 9, 8 + i5 * 18, 103 + i4 * 18 + i3));
				}
			}

			for (i4 = 0; i4 < 9; ++i4)
			{
				this.addSlot(new Slot(iInventory1, i4, 8 + i4 * 18, 161 + i3));
			}

		}

		public override bool canInteractWith(EntityPlayer entityPlayer1)
		{
			return this.lowerChestInventory.isUseableByPlayer(entityPlayer1);
		}

		public override ItemStack transferStackInSlot(int i1)
		{
			ItemStack itemStack2 = null;
			Slot slot3 = (Slot)this.inventorySlots[i1];
			if (slot3 != null && slot3.HasStack)
			{
				ItemStack itemStack4 = slot3.Stack;
				itemStack2 = itemStack4.copy();
				if (i1 < this.numRows * 9)
				{
					if (!this.mergeItemStack(itemStack4, this.numRows * 9, this.inventorySlots.Count, true))
					{
						return null;
					}
				}
				else if (!this.mergeItemStack(itemStack4, 0, this.numRows * 9, false))
				{
					return null;
				}

				if (itemStack4.stackSize == 0)
				{
					slot3.putStack((ItemStack)null);
				}
				else
				{
					slot3.onSlotChanged();
				}
			}

			return itemStack2;
		}

		public override void onCraftGuiClosed(EntityPlayer entityPlayer1)
		{
			base.onCraftGuiClosed(entityPlayer1);
			this.lowerChestInventory.closeChest();
		}
	}

}