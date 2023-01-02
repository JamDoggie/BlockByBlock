namespace net.minecraft.src
{
    using net.minecraft.client.entity;
    using Minecraft = net.minecraft.client.Minecraft;

    public class PlayerControllerMP : PlayerController
	{
		private int currentBlockX = -1;
		private int currentBlockY = -1;
		private int currentblockZ = -1;
		private float curBlockDamageMP = 0.0F;
		private float prevBlockDamageMP = 0.0F;
		private float stepSoundTickCounter = 0.0F;
		private int blockHitDelay = 0;
		private bool isHittingBlock = false;
		private bool creativeMode;
		private NetClientHandler netClientHandler;
		private int currentPlayerItem = 0;

		public PlayerControllerMP(Minecraft minecraft1, NetClientHandler netClientHandler2) : base(minecraft1)
		{
			this.netClientHandler = netClientHandler2;
		}

		public virtual bool Creative
		{
			set
			{
				this.creativeMode = value;
				if (this.creativeMode)
				{
					PlayerControllerCreative.enableAbilities(this.mc.thePlayer);
				}
				else
				{
					PlayerControllerCreative.disableAbilities(this.mc.thePlayer);
				}
    
			}
		}

		public override void flipPlayer(EntityPlayer entityPlayer1)
		{
			entityPlayer1.rotationYaw = -180.0F;
		}

		public override bool shouldDrawHUD()
		{
			return !this.creativeMode;
		}

		public override bool onPlayerDestroyBlock(int i1, int i2, int i3, int i4)
		{
			if (this.creativeMode)
			{
				return base.onPlayerDestroyBlock(i1, i2, i3, i4);
			}
			else
			{
				int i5 = this.mc.theWorld.getBlockId(i1, i2, i3);
				bool z6 = base.onPlayerDestroyBlock(i1, i2, i3, i4);
				ItemStack itemStack7 = this.mc.thePlayer.CurrentEquippedItem;
				if (itemStack7 != null)
				{
					itemStack7.onDestroyBlock(i5, i1, i2, i3, this.mc.thePlayer);
					if (itemStack7.stackSize == 0)
					{
						itemStack7.onItemDestroyedByUse(this.mc.thePlayer);
						this.mc.thePlayer.destroyCurrentEquippedItem();
					}
				}

				return z6;
			}
		}

		public override void clickBlock(int i1, int i2, int i3, int i4)
		{
			if (this.creativeMode)
			{
				this.netClientHandler.addToSendQueue(new Packet14BlockDig(0, i1, i2, i3, i4));
				PlayerControllerCreative.clickBlockCreative(this.mc, this, i1, i2, i3, i4);
				this.blockHitDelay = 5;
			}
			else if (!this.isHittingBlock || i1 != this.currentBlockX || i2 != this.currentBlockY || i3 != this.currentblockZ)
			{
				this.netClientHandler.addToSendQueue(new Packet14BlockDig(0, i1, i2, i3, i4));
				int i5 = this.mc.theWorld.getBlockId(i1, i2, i3);
				if (i5 > 0 && this.curBlockDamageMP == 0.0F)
				{
					Block.blocksList[i5].onBlockClicked(this.mc.theWorld, i1, i2, i3, this.mc.thePlayer);
				}

				if (i5 > 0 && Block.blocksList[i5].blockStrength(this.mc.thePlayer) >= 1.0F)
				{
					this.onPlayerDestroyBlock(i1, i2, i3, i4);
				}
				else
				{
					this.isHittingBlock = true;
					this.currentBlockX = i1;
					this.currentBlockY = i2;
					this.currentblockZ = i3;
					this.curBlockDamageMP = 0.0F;
					this.prevBlockDamageMP = 0.0F;
					this.stepSoundTickCounter = 0.0F;
				}
			}

		}

		public override void resetBlockRemoving()
		{
			this.curBlockDamageMP = 0.0F;
			this.isHittingBlock = false;
		}

		public override void onPlayerDamageBlock(int i1, int i2, int i3, int i4)
		{
			this.syncCurrentPlayItem();
			if (this.blockHitDelay > 0)
			{
				--this.blockHitDelay;
			}
			else if (this.creativeMode)
			{
				this.blockHitDelay = 5;
				this.netClientHandler.addToSendQueue(new Packet14BlockDig(0, i1, i2, i3, i4));
				PlayerControllerCreative.clickBlockCreative(this.mc, this, i1, i2, i3, i4);
			}
			else
			{
				if (i1 == this.currentBlockX && i2 == this.currentBlockY && i3 == this.currentblockZ)
				{
					int i5 = this.mc.theWorld.getBlockId(i1, i2, i3);
					if (i5 == 0)
					{
						this.isHittingBlock = false;
						return;
					}

					Block block6 = Block.blocksList[i5];
					this.curBlockDamageMP += block6.blockStrength(this.mc.thePlayer);
					if (this.stepSoundTickCounter % 4.0F == 0.0F && block6 != null)
					{
						this.mc.sndManager.playSound(block6.stepSound.StepSoundName, (float)i1 + 0.5F, (float)i2 + 0.5F, (float)i3 + 0.5F, (block6.stepSound.Volume + 1.0F) / 8.0F, block6.stepSound.Pitch * 0.5F);
					}

					++this.stepSoundTickCounter;
					if (this.curBlockDamageMP >= 1.0F)
					{
						this.isHittingBlock = false;
						this.netClientHandler.addToSendQueue(new Packet14BlockDig(2, i1, i2, i3, i4));
						this.onPlayerDestroyBlock(i1, i2, i3, i4);
						this.curBlockDamageMP = 0.0F;
						this.prevBlockDamageMP = 0.0F;
						this.stepSoundTickCounter = 0.0F;
						this.blockHitDelay = 5;
					}
				}
				else
				{
					this.clickBlock(i1, i2, i3, i4);
				}

			}
		}

		public override float PartialTime
		{
			set
			{
				if (this.curBlockDamageMP <= 0.0F)
				{
					this.mc.ingameGUI.damageGuiPartialTime = 0.0F;
					this.mc.renderGlobal.damagePartialTime = 0.0F;
				}
				else
				{
					float f2 = this.prevBlockDamageMP + (this.curBlockDamageMP - this.prevBlockDamageMP) * value;
					this.mc.ingameGUI.damageGuiPartialTime = f2;
					this.mc.renderGlobal.damagePartialTime = f2;
				}
    
			}
		}

		public override float BlockReachDistance
		{
			get
			{
				return this.creativeMode ? 5.0F : 4.5F;
			}
		}

		public override void onWorldChange(World world1)
		{
			base.onWorldChange(world1);
		}

		public override void updateController()
		{
			this.syncCurrentPlayItem();
			this.prevBlockDamageMP = this.curBlockDamageMP;
			this.mc.sndManager.playRandomMusicIfReady();
		}

		private void syncCurrentPlayItem()
		{
			int i1 = this.mc.thePlayer.inventory.currentItem;
			if (i1 != this.currentPlayerItem)
			{
				this.currentPlayerItem = i1;
				this.netClientHandler.addToSendQueue(new Packet16BlockItemSwitch(this.currentPlayerItem));
			}

		}

		public override bool onPlayerRightClick(EntityPlayer entityPlayer1, World world2, ItemStack itemStack3, int i4, int i5, int i6, int i7)
		{
			this.syncCurrentPlayItem();
			this.netClientHandler.addToSendQueue(new Packet15Place(i4, i5, i6, i7, entityPlayer1.inventory.CurrentItem));
			int i8 = world2.getBlockId(i4, i5, i6);
			if (i8 > 0 && Block.blocksList[i8].blockActivated(world2, i4, i5, i6, entityPlayer1))
			{
				return true;
			}
			else if (itemStack3 == null)
			{
				return false;
			}
			else if (this.creativeMode)
			{
				int i9 = itemStack3.ItemDamage;
				int i10 = itemStack3.stackSize;
				bool z11 = itemStack3.useItem(entityPlayer1, world2, i4, i5, i6, i7);
				itemStack3.ItemDamage = i9;
				itemStack3.stackSize = i10;
				return z11;
			}
			else
			{
				return itemStack3.useItem(entityPlayer1, world2, i4, i5, i6, i7);
			}
		}

		public override bool sendUseItem(EntityPlayer entityPlayer1, World world2, ItemStack itemStack3)
		{
			this.syncCurrentPlayItem();
			this.netClientHandler.addToSendQueue(new Packet15Place(-1, -1, -1, 255, entityPlayer1.inventory.CurrentItem));
			bool z4 = base.sendUseItem(entityPlayer1, world2, itemStack3);
			return z4;
		}

		public override EntityPlayer createPlayer(World world1)
		{
			return new EntityClientPlayerMP(this.mc, world1, this.mc.session, this.netClientHandler);
		}

		public override void attackEntity(EntityPlayer entityPlayer1, Entity entity2)
		{
			this.syncCurrentPlayItem();
			this.netClientHandler.addToSendQueue(new Packet7UseEntity(entityPlayer1.entityId, entity2.entityId, 1));
			entityPlayer1.attackTargetEntityWithCurrentItem(entity2);
		}

		public override void interactWithEntity(EntityPlayer entityPlayer1, Entity entity2)
		{
			this.syncCurrentPlayItem();
			this.netClientHandler.addToSendQueue(new Packet7UseEntity(entityPlayer1.entityId, entity2.entityId, 0));
			entityPlayer1.useCurrentItemOnEntity(entity2);
		}

		public override ItemStack windowClick(int i1, int i2, int i3, bool z4, EntityPlayer entityPlayer5)
		{
			short s6 = entityPlayer5.craftingInventory.getNextTransactionID(entityPlayer5.inventory);
			ItemStack itemStack7 = base.windowClick(i1, i2, i3, z4, entityPlayer5);
			this.netClientHandler.addToSendQueue(new Packet102WindowClick(i1, i2, i3, z4, itemStack7, s6));
			return itemStack7;
		}

		public override void func_40593_a(int i1, int i2)
		{
			this.netClientHandler.addToSendQueue(new Packet108EnchantItem(i1, i2));
		}

		public override void sendSlotPacket(ItemStack itemStack1, int i2)
		{
			if (this.creativeMode)
			{
				this.netClientHandler.addToSendQueue(new Packet107CreativeSetSlot(i2, itemStack1));
			}

		}

		public override void func_35639_a(ItemStack itemStack1)
		{
			if (this.creativeMode && itemStack1 != null)
			{
				this.netClientHandler.addToSendQueue(new Packet107CreativeSetSlot(-1, itemStack1));
			}

		}

		public override void func_20086_a(int i1, EntityPlayer entityPlayer2)
		{
			if (i1 != -9999)
			{
				;
			}
		}

		public override void onStoppedUsingItem(EntityPlayer entityPlayer1)
		{
			this.syncCurrentPlayItem();
			this.netClientHandler.addToSendQueue(new Packet14BlockDig(5, 0, 0, 0, 255));
			base.onStoppedUsingItem(entityPlayer1);
		}

		public override bool func_35642_f()
		{
			return true;
		}

		public override bool NotCreative
		{
			get
			{
				return !this.creativeMode;
			}
		}

		public override bool InCreativeMode
		{
			get
			{
				return this.creativeMode;
			}
		}

		public override bool extendedReach()
		{
			return this.creativeMode;
		}
	}

}