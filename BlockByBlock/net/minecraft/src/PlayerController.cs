namespace net.minecraft.src
{
    using net.minecraft.client.entity;
    using Minecraft = net.minecraft.client.Minecraft;

    public abstract class PlayerController
	{
		protected internal readonly Minecraft mc;
		public bool isInTestMode = false;

		public PlayerController(Minecraft minecraft1)
		{
			this.mc = minecraft1;
		}

		public virtual void onWorldChange(World world1)
		{
		}

		public abstract void clickBlock(int i1, int i2, int i3, int i4);

		public virtual bool onPlayerDestroyBlock(int i1, int i2, int i3, int i4)
		{
			World world5 = this.mc.theWorld;
			Block block6 = Block.blocksList[world5.getBlockId(i1, i2, i3)];
			if (block6 == null)
			{
				return false;
			}
			else
			{
				world5.playAuxSFX(2001, i1, i2, i3, block6.blockID + (world5.getBlockMetadata(i1, i2, i3) << 12));
				int i7 = world5.getBlockMetadata(i1, i2, i3);
				bool z8 = world5.setBlockWithNotify(i1, i2, i3, 0);
				if (z8)
				{
					block6.onBlockDestroyedByPlayer(world5, i1, i2, i3, i7);
				}

				return z8;
			}
		}

		public abstract void onPlayerDamageBlock(int i1, int i2, int i3, int i4);

		public abstract void resetBlockRemoving();

		public virtual float PartialTime
		{
			set
			{
			}
		}

		public abstract float BlockReachDistance {get;}

		public virtual bool sendUseItem(EntityPlayer entityPlayer1, World world2, ItemStack itemStack3)
		{
			int i4 = itemStack3.stackSize;
			ItemStack itemStack5 = itemStack3.useItemRightClick(world2, entityPlayer1);
			if (itemStack5 != itemStack3 || itemStack5 != null && itemStack5.stackSize != i4)
			{
				entityPlayer1.inventory.mainInventory[entityPlayer1.inventory.currentItem] = itemStack5;
				if (itemStack5.stackSize == 0)
				{
					entityPlayer1.inventory.mainInventory[entityPlayer1.inventory.currentItem] = null;
				}

				return true;
			}
			else
			{
				return false;
			}
		}

		public virtual void flipPlayer(EntityPlayer entityPlayer1)
		{
		}

		public virtual void updateController()
		{
		}

		public abstract bool shouldDrawHUD();

		public virtual void func_6473_b(EntityPlayer entityPlayer1)
		{
			PlayerControllerCreative.disableAbilities(entityPlayer1);
		}

		public abstract bool onPlayerRightClick(EntityPlayer entityPlayer1, World world2, ItemStack itemStack3, int i4, int i5, int i6, int i7);

		public virtual EntityPlayer createPlayer(World world1)
		{
			return new EntityPlayerSP(this.mc, world1, this.mc.session, world1.worldProvider.worldType);
		}

		public virtual void interactWithEntity(EntityPlayer entityPlayer1, Entity entity2)
		{
			entityPlayer1.useCurrentItemOnEntity(entity2);
		}

		public virtual void attackEntity(EntityPlayer entityPlayer1, Entity entity2)
		{
			entityPlayer1.attackTargetEntityWithCurrentItem(entity2);
		}

		public virtual ItemStack windowClick(int i1, int i2, int i3, bool z4, EntityPlayer entityPlayer5)
		{
			return entityPlayer5.craftingInventory.slotClick(i2, i3, z4, entityPlayer5);
		}

		public virtual void func_20086_a(int i1, EntityPlayer entityPlayer2)
		{
			entityPlayer2.craftingInventory.onCraftGuiClosed(entityPlayer2);
			entityPlayer2.craftingInventory = entityPlayer2.inventorySlots;
		}

		public virtual void func_40593_a(int i1, int i2)
		{
		}

		/// <summary>
		/// TODO: check that this name is accurate. Changing this to true seems to make the player camera turn like the title screen panorama, even in game.
		/// </summary>
		/// <returns>false, always.</returns>
		public virtual bool IsPanoramaCamera()
		{
			return false;
		}

		public virtual void onStoppedUsingItem(EntityPlayer entityPlayer1)
		{
			entityPlayer1.stopUsingItem();
		}

		/// <summary>
		/// Mystery constant. This returns false.
		/// </summary>
		/// <returns>false, always.</returns>
		public virtual bool func_35642_f()
		{
			return false;
		}

		public virtual bool NotCreative
		{
			get
			{
				return true;
			}
		}

		public virtual bool InCreativeMode
		{
			get
			{
				return false;
			}
		}

		public virtual bool extendedReach()
		{
			return false;
		}

		public virtual void sendSlotPacket(ItemStack itemStack1, int i2)
		{
		}

		public virtual void func_35639_a(ItemStack itemStack1)
		{
		}
	}

}