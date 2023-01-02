namespace net.minecraft.src
{
    using net.minecraft.client.entity;
    using Minecraft = net.minecraft.client.Minecraft;

    public class PlayerControllerSP : PlayerController
	{
		private int curBlockX = -1;
		private int curBlockY = -1;
		private int curBlockZ = -1;
		private float curBlockDamage = 0.0F;
		private float prevBlockDamage = 0.0F;
		private float blockDestroySoundCounter = 0.0F;
		private int blockHitWait = 0;

		public PlayerControllerSP(Minecraft minecraft1) : base(minecraft1)
		{
		}

		public override void flipPlayer(EntityPlayer entityPlayer1)
		{
			entityPlayer1.rotationYaw = -180.0F;
		}

		public override bool shouldDrawHUD()
		{
			return true;
		}

		public override bool onPlayerDestroyBlock(int i1, int i2, int i3, int i4)
		{
			int i5 = this.mc.theWorld.getBlockId(i1, i2, i3);
			int i6 = this.mc.theWorld.getBlockMetadata(i1, i2, i3);
			bool z7 = base.onPlayerDestroyBlock(i1, i2, i3, i4);
			ItemStack itemStack8 = this.mc.thePlayer.CurrentEquippedItem;
			bool z9 = this.mc.thePlayer.canHarvestBlock(Block.blocksList[i5]);
			if (itemStack8 != null)
			{
				itemStack8.onDestroyBlock(i5, i1, i2, i3, this.mc.thePlayer);
				if (itemStack8.stackSize == 0)
				{
					itemStack8.onItemDestroyedByUse(this.mc.thePlayer);
					this.mc.thePlayer.destroyCurrentEquippedItem();
				}
			}

			if (z7 && z9)
			{
				Block.blocksList[i5].harvestBlock(this.mc.theWorld, this.mc.thePlayer, i1, i2, i3, i6);
			}

			return z7;
		}

		public override void clickBlock(int i1, int i2, int i3, int i4)
		{
			if (this.mc.thePlayer.canPlayerEdit(i1, i2, i3))
			{
				this.mc.theWorld.func_48457_a(this.mc.thePlayer, i1, i2, i3, i4);
				int i5 = this.mc.theWorld.getBlockId(i1, i2, i3);
				if (i5 > 0 && this.curBlockDamage == 0.0F)
				{
					Block.blocksList[i5].onBlockClicked(this.mc.theWorld, i1, i2, i3, this.mc.thePlayer);
				}

				if (i5 > 0 && Block.blocksList[i5].blockStrength(this.mc.thePlayer) >= 1.0F)
				{
					this.onPlayerDestroyBlock(i1, i2, i3, i4);
				}

			}
		}

		public override void resetBlockRemoving()
		{
			this.curBlockDamage = 0.0F;
			this.blockHitWait = 0;
		}

		public override void onPlayerDamageBlock(int i1, int i2, int i3, int i4)
		{
			if (this.blockHitWait > 0)
			{
				--this.blockHitWait;
			}
			else
			{
				if (i1 == this.curBlockX && i2 == this.curBlockY && i3 == this.curBlockZ)
				{
					int i5 = this.mc.theWorld.getBlockId(i1, i2, i3);
					if (!this.mc.thePlayer.canPlayerEdit(i1, i2, i3))
					{
						return;
					}

					if (i5 == 0)
					{
						return;
					}

					Block block6 = Block.blocksList[i5];
					this.curBlockDamage += block6.blockStrength(this.mc.thePlayer);
					if (this.blockDestroySoundCounter % 4.0F == 0.0F && block6 != null)
					{
						this.mc.sndManager.playSound(block6.stepSound.StepSoundName, (float)i1 + 0.5F, (float)i2 + 0.5F, (float)i3 + 0.5F, (block6.stepSound.Volume + 1.0F) / 8.0F, block6.stepSound.Pitch * 0.5F);
					}

					++this.blockDestroySoundCounter;
					if (this.curBlockDamage >= 1.0F)
					{
						this.onPlayerDestroyBlock(i1, i2, i3, i4);
						this.curBlockDamage = 0.0F;
						this.prevBlockDamage = 0.0F;
						this.blockDestroySoundCounter = 0.0F;
						this.blockHitWait = 5;
					}
				}
				else
				{
					this.curBlockDamage = 0.0F;
					this.prevBlockDamage = 0.0F;
					this.blockDestroySoundCounter = 0.0F;
					this.curBlockX = i1;
					this.curBlockY = i2;
					this.curBlockZ = i3;
				}

			}
		}

		public override float PartialTime
		{
			set
			{
				if (this.curBlockDamage <= 0.0F)
				{
					this.mc.ingameGUI.damageGuiPartialTime = 0.0F;
					this.mc.renderGlobal.damagePartialTime = 0.0F;
				}
				else
				{
					float f2 = this.prevBlockDamage + (this.curBlockDamage - this.prevBlockDamage) * value;
					this.mc.ingameGUI.damageGuiPartialTime = f2;
					this.mc.renderGlobal.damagePartialTime = f2;
				}
    
			}
		}

		public override float BlockReachDistance
		{
			get
			{
				return 4.0F;
			}
		}

		public override void onWorldChange(World world1)
		{
			base.onWorldChange(world1);
		}

		public override EntityPlayer createPlayer(World world1)
		{
			EntityPlayer entityPlayer2 = base.createPlayer(world1);
			return entityPlayer2;
		}

		public override void updateController()
		{
			this.prevBlockDamage = this.curBlockDamage;
			this.mc.sndManager.playRandomMusicIfReady();
		}

		public override bool onPlayerRightClick(EntityPlayer entityPlayer1, World world2, ItemStack itemStack3, int i4, int i5, int i6, int i7)
		{
			int i8 = world2.getBlockId(i4, i5, i6);
			return i8 > 0 && Block.blocksList[i8].blockActivated(world2, i4, i5, i6, entityPlayer1) ? true : (itemStack3 == null ? false : itemStack3.useItem(entityPlayer1, world2, i4, i5, i6, i7));
		}

		public override bool func_35642_f()
		{
			return true;
		}
	}

}