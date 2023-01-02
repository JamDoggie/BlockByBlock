namespace net.minecraft.src
{
    using net.minecraft.client.entity;
    using Minecraft = net.minecraft.client.Minecraft;

    public class PlayerControllerCreative : PlayerController
	{
		private int field_35647_c;

		public PlayerControllerCreative(Minecraft minecraft1) : base(minecraft1)
		{
			this.isInTestMode = true;
		}

		public static void enableAbilities(EntityPlayer entityPlayer0)
		{
			entityPlayer0.capabilities.allowFlying = true;
			entityPlayer0.capabilities.isCreativeMode = true;
			entityPlayer0.capabilities.disableDamage = true;
		}

		public static void disableAbilities(EntityPlayer entityPlayer0)
		{
			entityPlayer0.capabilities.allowFlying = false;
			entityPlayer0.capabilities.isFlying = false;
			entityPlayer0.capabilities.isCreativeMode = false;
			entityPlayer0.capabilities.disableDamage = false;
		}

		public override void func_6473_b(EntityPlayer entityPlayer1)
		{
			enableAbilities(entityPlayer1);

			for (int i2 = 0; i2 < 9; ++i2)
			{
				if (entityPlayer1.inventory.mainInventory[i2] == null)
				{
					entityPlayer1.inventory.mainInventory[i2] = new ItemStack((Block)Session.registeredBlocksList[i2]);
				}
			}

		}

		public static void clickBlockCreative(Minecraft minecraft0, PlayerController playerController1, int i2, int i3, int i4, int i5)
		{
			if (!minecraft0.theWorld.func_48457_a(minecraft0.thePlayer, i2, i3, i4, i5))
			{
				playerController1.onPlayerDestroyBlock(i2, i3, i4, i5);
			}

		}

		public override bool onPlayerRightClick(EntityPlayer entityPlayer1, World world2, ItemStack itemStack3, int i4, int i5, int i6, int i7)
		{
			int i8 = world2.getBlockId(i4, i5, i6);
			if (i8 > 0 && Block.blocksList[i8].blockActivated(world2, i4, i5, i6, entityPlayer1))
			{
				return true;
			}
			else if (itemStack3 == null)
			{
				return false;
			}
			else
			{
				int i9 = itemStack3.ItemDamage;
				int i10 = itemStack3.stackSize;
				bool z11 = itemStack3.useItem(entityPlayer1, world2, i4, i5, i6, i7);
				itemStack3.ItemDamage = i9;
				itemStack3.stackSize = i10;
				return z11;
			}
		}

		public override void clickBlock(int i1, int i2, int i3, int i4)
		{
			clickBlockCreative(this.mc, this, i1, i2, i3, i4);
			this.field_35647_c = 5;
		}

		public override void onPlayerDamageBlock(int i1, int i2, int i3, int i4)
		{
			--this.field_35647_c;
			if (this.field_35647_c <= 0)
			{
				this.field_35647_c = 5;
				clickBlockCreative(this.mc, this, i1, i2, i3, i4);
			}

		}

		public override void resetBlockRemoving()
		{
		}

		public override bool shouldDrawHUD()
		{
			return false;
		}

		public override void onWorldChange(World world1)
		{
			base.onWorldChange(world1);
		}

		public override float BlockReachDistance
		{
			get
			{
				return 5.0F;
			}
		}

		public override bool NotCreative
		{
			get
			{
				return false;
			}
		}

		public override bool InCreativeMode
		{
			get
			{
				return true;
			}
		}

		public override bool extendedReach()
		{
			return true;
		}
	}

}