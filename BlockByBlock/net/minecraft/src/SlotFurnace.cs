using System;
using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class SlotFurnace : Slot
	{
		private EntityPlayer thePlayer;
		private int field_48437_f;

		public SlotFurnace(EntityPlayer entityPlayer1, IInventory iInventory2, int i3, int i4, int i5) : base(iInventory2, i3, i4, i5)
		{
			this.thePlayer = entityPlayer1;
		}

		public override bool isItemValid(ItemStack itemStack1)
		{
			return false;
		}

		public override ItemStack decrStackSize(int i1)
		{
			if (this.HasStack)
			{
				this.field_48437_f += Math.Min(i1, this.Stack.stackSize);
			}

			return base.decrStackSize(i1);
		}

		public override void onPickupFromSlot(ItemStack itemStack1)
		{
			this.func_48434_c(itemStack1);
			base.onPickupFromSlot(itemStack1);
		}

		protected internal override void func_48435_a(ItemStack itemStack1, int i2)
		{
			this.field_48437_f += i2;
			this.func_48434_c(itemStack1);
		}

		protected internal override void func_48434_c(ItemStack itemStack1)
		{
			itemStack1.onCrafting(this.thePlayer.worldObj, this.thePlayer, this.field_48437_f);
			this.field_48437_f = 0;
			if (itemStack1.itemID == Item.ingotIron.shiftedIndex)
			{
				this.thePlayer.addStat(AchievementList.acquireIron, 1);
			}

			if (itemStack1.itemID == Item.fishCooked.shiftedIndex)
			{
				this.thePlayer.addStat(AchievementList.cookFish, 1);
			}

		}
	}

}