using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public interface IInventory
	{
		int SizeInventory {get;}

		ItemStack getStackInSlot(int i1);

		ItemStack decrStackSize(int i1, int i2);

		ItemStack getStackInSlotOnClosing(int i1);

		void setInventorySlotContents(int i1, ItemStack itemStack2);

		string InvName {get;}

		int InventoryStackLimit {get;}

		void onInventoryChanged();

		bool isUseableByPlayer(EntityPlayer entityPlayer1);

		void openChest();

		void closeChest();
	}

}