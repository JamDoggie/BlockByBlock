namespace net.minecraft.src
{
	using Minecraft = net.minecraft.client.Minecraft;

	public class EntityPlayerSP : EntityPlayer
	{
		public MovementInput movementInput;
		protected internal Minecraft mc;
		protected internal int sprintToggleTimer = 0;
		public int sprintingTicksLeft = 0;
		public float renderArmYaw;
		public float renderArmPitch;
		public float prevRenderArmYaw;
		public float prevRenderArmPitch;
		private MouseFilter field_21903_bJ = new MouseFilter();
		private MouseFilter field_21904_bK = new MouseFilter();
		private MouseFilter field_21902_bL = new MouseFilter();

		public EntityPlayerSP(Minecraft minecraft1, World world2, Session session3, int i4) : base(world2)
		{
			this.mc = minecraft1;
			this.dimension = i4;
			if (session3 != null && !string.ReferenceEquals(session3.username, null) && session3.username.Length > 0)
			{
				this.skinUrl = "http://s3.amazonaws.com/MinecraftSkins/" + session3.username + ".png";
			}

			this.username = session3.username;
		}

		public override void moveEntity(double d1, double d3, double d5)
		{
			base.moveEntity(d1, d3, d5);
		}

		public override void updateEntityActionState()
		{
			base.updateEntityActionState();
			this.moveStrafing = this.movementInput.moveStrafe;
			this.moveForward = this.movementInput.moveForward;
			this.isJumping = this.movementInput.jump;
			this.prevRenderArmYaw = this.renderArmYaw;
			this.prevRenderArmPitch = this.renderArmPitch;
			this.renderArmPitch = (float)((double)this.renderArmPitch + (double)(this.rotationPitch - this.renderArmPitch) * 0.5D);
			this.renderArmYaw = (float)((double)this.renderArmYaw + (double)(this.rotationYaw - this.renderArmYaw) * 0.5D);
		}

		protected internal override bool ClientWorld
		{
			get
			{
				return true;
			}
		}

		public override void onLivingUpdate()
		{
			if (this.sprintingTicksLeft > 0)
			{
				--this.sprintingTicksLeft;
				if (this.sprintingTicksLeft == 0)
				{
					this.Sprinting = false;
				}
			}

			if (this.sprintToggleTimer > 0)
			{
				--this.sprintToggleTimer;
			}

			if (this.mc.playerController.func_35643_e())
			{
				this.posX = this.posZ = 0.5D;
				this.posX = 0.0D;
				this.posZ = 0.0D;
				this.rotationYaw = (float)this.ticksExisted / 12.0F;
				this.rotationPitch = 10.0F;
				this.posY = 68.5D;
			}
			else
			{
				if (!this.mc.statFileWriter.hasAchievementUnlocked(AchievementList.openInventory))
				{
					this.mc.guiAchievement.queueAchievementInformation(AchievementList.openInventory);
				}

				this.prevTimeInPortal = this.timeInPortal;
				bool z1;
				if (this.inPortal)
				{
					if (!this.worldObj.isRemote && this.ridingEntity != null)
					{
						this.mountEntity((Entity)null);
					}

					if (this.mc.currentScreen != null)
					{
						this.mc.displayGuiScreen((GuiScreen)null);
					}

					if (this.timeInPortal == 0.0F)
					{
						this.mc.sndManager.playSoundFX("portal.trigger", 1.0F, this.rand.NextSingle() * 0.4F + 0.8F);
					}

					this.timeInPortal += 0.0125F;
					if (this.timeInPortal >= 1.0F)
					{
						this.timeInPortal = 1.0F;
						if (!this.worldObj.isRemote)
						{
							this.timeUntilPortal = 10;
							this.mc.sndManager.playSoundFX("portal.travel", 1.0F, this.rand.NextSingle() * 0.4F + 0.8F);
							z1 = false;
							sbyte b5;
							if (this.dimension == -1)
							{
								b5 = 0;
							}
							else
							{
								b5 = -1;
							}

							this.mc.usePortal(b5);
							this.triggerAchievement(AchievementList.portal);
						}
					}

					this.inPortal = false;
				}
				else if (this.isPotionActive(Potion.confusion) && this.getActivePotionEffect(Potion.confusion).Duration > 60)
				{
					this.timeInPortal += 0.006666667F;
					if (this.timeInPortal > 1.0F)
					{
						this.timeInPortal = 1.0F;
					}
				}
				else
				{
					if (this.timeInPortal > 0.0F)
					{
						this.timeInPortal -= 0.05F;
					}

					if (this.timeInPortal < 0.0F)
					{
						this.timeInPortal = 0.0F;
					}
				}

				if (this.timeUntilPortal > 0)
				{
					--this.timeUntilPortal;
				}

				z1 = this.movementInput.jump;
				float f2 = 0.8F;
				bool z3 = this.movementInput.moveForward >= f2;
				this.movementInput.func_52013_a();
				if (this.UsingItem)
				{
					this.movementInput.moveStrafe *= 0.2F;
					this.movementInput.moveForward *= 0.2F;
					this.sprintToggleTimer = 0;
				}

				if (this.movementInput.sneak && this.ySize < 0.2F)
				{
					this.ySize = 0.2F;
				}

				this.pushOutOfBlocks(this.posX - (double)this.width * 0.35D, this.boundingBox.minY + 0.5D, this.posZ + (double)this.width * 0.35D);
				this.pushOutOfBlocks(this.posX - (double)this.width * 0.35D, this.boundingBox.minY + 0.5D, this.posZ - (double)this.width * 0.35D);
				this.pushOutOfBlocks(this.posX + (double)this.width * 0.35D, this.boundingBox.minY + 0.5D, this.posZ - (double)this.width * 0.35D);
				this.pushOutOfBlocks(this.posX + (double)this.width * 0.35D, this.boundingBox.minY + 0.5D, this.posZ + (double)this.width * 0.35D);
				bool z4 = (float)this.FoodStats.FoodLevel > 6.0F;
				if (this.onGround && !z3 && this.movementInput.moveForward >= f2 && !this.Sprinting && z4 && !this.UsingItem && !this.isPotionActive(Potion.blindness))
				{
					if (this.sprintToggleTimer == 0)
					{
						this.sprintToggleTimer = 7;
					}
					else
					{
						this.Sprinting = true;
						this.sprintToggleTimer = 0;
					}
				}

				if (this.Sneaking)
				{
					this.sprintToggleTimer = 0;
				}

				if (this.Sprinting && (this.movementInput.moveForward < f2 || this.isCollidedHorizontally || !z4))
				{
					this.Sprinting = false;
				}

				if (this.capabilities.allowFlying && !z1 && this.movementInput.jump)
				{
					if (this.flyToggleTimer == 0)
					{
						this.flyToggleTimer = 7;
					}
					else
					{
						this.capabilities.isFlying = !this.capabilities.isFlying;
						this.func_50009_aI();
						this.flyToggleTimer = 0;
					}
				}

				if (this.capabilities.isFlying)
				{
					if (this.movementInput.sneak)
					{
						this.motionY -= 0.15D;
					}

					if (this.movementInput.jump)
					{
						this.motionY += 0.15D;
					}
				}

				base.onLivingUpdate();
				if (this.onGround && this.capabilities.isFlying)
				{
					this.capabilities.isFlying = false;
					this.func_50009_aI();
				}

			}
		}

		public override void travelToTheEnd(int i1)
		{
			if (!this.worldObj.isRemote)
			{
				if (this.dimension == 1 && i1 == 1)
				{
					this.triggerAchievement(AchievementList.theEnd2);
					this.mc.displayGuiScreen(new GuiWinGame());
				}
				else
				{
					this.triggerAchievement(AchievementList.theEnd);
					this.mc.sndManager.playSoundFX("portal.travel", 1.0F, this.rand.NextSingle() * 0.4F + 0.8F);
					this.mc.usePortal(1);
				}

			}
		}

		public virtual float FOVMultiplier
		{
			get
			{
				float f1 = 1.0F;
				if (this.capabilities.isFlying)
				{
					f1 *= 1.1F;
				}
    
				f1 *= (this.landMovementFactor * this.SpeedModifier / this.speedOnGround + 1.0F) / 2.0F;
				if (this.UsingItem && this.ItemInUse.itemID == Item.bow.shiftedIndex)
				{
					int i2 = this.ItemInUseDuration;
					float f3 = (float)i2 / 20.0F;
					if (f3 > 1.0F)
					{
						f3 = 1.0F;
					}
					else
					{
						f3 *= f3;
					}
    
					f1 *= 1.0F - f3 * 0.15F;
				}
    
				return f1;
			}
		}

		public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
		{
			base.writeEntityToNBT(nBTTagCompound1);
			nBTTagCompound1.setInteger("Score", this.score);
		}

		public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
		{
			base.readEntityFromNBT(nBTTagCompound1);
			this.score = nBTTagCompound1.getInteger("Score");
		}

		public override void closeScreen()
		{
			base.closeScreen();
			this.mc.displayGuiScreen((GuiScreen)null);
		}

		public override void displayGUIEditSign(TileEntitySign tileEntitySign1)
		{
			this.mc.displayGuiScreen(new GuiEditSign(tileEntitySign1));
		}

		public override void displayGUIChest(IInventory iInventory1)
		{
			this.mc.displayGuiScreen(new GuiChest(this.inventory, iInventory1));
		}

		public override void displayWorkbenchGUI(int i1, int i2, int i3)
		{
			this.mc.displayGuiScreen(new GuiCrafting(this.inventory, this.worldObj, i1, i2, i3));
		}

		public override void displayGUIEnchantment(int i1, int i2, int i3)
		{
			this.mc.displayGuiScreen(new GuiEnchantment(this.inventory, this.worldObj, i1, i2, i3));
		}

		public override void displayGUIFurnace(TileEntityFurnace tileEntityFurnace1)
		{
			this.mc.displayGuiScreen(new GuiFurnace(this.inventory, tileEntityFurnace1));
		}

		public override void displayGUIBrewingStand(TileEntityBrewingStand tileEntityBrewingStand1)
		{
			this.mc.displayGuiScreen(new GuiBrewingStand(this.inventory, tileEntityBrewingStand1));
		}

		public override void displayGUIDispenser(TileEntityDispenser tileEntityDispenser1)
		{
			this.mc.displayGuiScreen(new GuiDispenser(this.inventory, tileEntityDispenser1));
		}

		public override void onCriticalHit(Entity entity1)
		{
			this.mc.effectRenderer.addEffect(new EntityCrit2FX(this.mc.theWorld, entity1));
		}

		public override void onEnchantmentCritical(Entity entity1)
		{
			EntityCrit2FX entityCrit2FX2 = new EntityCrit2FX(this.mc.theWorld, entity1, "magicCrit");
			this.mc.effectRenderer.addEffect(entityCrit2FX2);
		}

		public override void onItemPickup(Entity entity1, int i2)
		{
			this.mc.effectRenderer.addEffect(new EntityPickupFX(this.mc.theWorld, entity1, this, -0.5F));
		}

		public virtual void sendChatMessage(string string1)
		{
		}

		public override bool Sneaking
		{
			get
			{
				return this.movementInput.sneak && !this.sleeping;
			}
		}

		public virtual int Health
		{
			set
			{
				int i2 = health - value;
				if (i2 <= 0)
				{
					this.EntityHealth = value;
					if (i2 < 0)
					{
						this.heartsLife = this.heartsHalvesLife / 2;
					}
				}
				else
				{
					this.naturalArmorRating = i2;
					this.EntityHealth = this.Health;
					this.heartsLife = this.heartsHalvesLife;
					this.damageEntity(DamageSource.generic, i2);
					this.hurtTime = this.maxHurtTime = 10;
				}
    
			}

			get
            {
				return health;
            }
		}

		public override void respawnPlayer()
		{
			this.mc.respawn(false, 0, false);
		}

		public override void func_6420_o()
		{
		}

		public override void addChatMessage(string string1)
		{
			this.mc.ingameGUI.addChatMessageTranslate(string1);
		}

		public override void addStat(StatBase statBase1, int i2)
		{
			if (statBase1 != null)
			{
				if (statBase1.IsAchievement)
				{
					Achievement achievement3 = (Achievement)statBase1;
					if (achievement3.parentAchievement == null || this.mc.statFileWriter.hasAchievementUnlocked(achievement3.parentAchievement))
					{
						if (!this.mc.statFileWriter.hasAchievementUnlocked(achievement3))
						{
							this.mc.guiAchievement.queueTakenAchievement(achievement3);
						}

						this.mc.statFileWriter.readStat(statBase1, i2);
					}
				}
				else
				{
					this.mc.statFileWriter.readStat(statBase1, i2);
				}

			}
		}

		private bool isBlockTranslucent(int i1, int i2, int i3)
		{
			return this.worldObj.isBlockNormalCube(i1, i2, i3);
		}

		protected internal override bool pushOutOfBlocks(double d1, double d3, double d5)
		{
			int i7 = MathHelper.floor_double(d1);
			int i8 = MathHelper.floor_double(d3);
			int i9 = MathHelper.floor_double(d5);
			double d10 = d1 - (double)i7;
			double d12 = d5 - (double)i9;
			if (this.isBlockTranslucent(i7, i8, i9) || this.isBlockTranslucent(i7, i8 + 1, i9))
			{
				bool z14 = !this.isBlockTranslucent(i7 - 1, i8, i9) && !this.isBlockTranslucent(i7 - 1, i8 + 1, i9);
				bool z15 = !this.isBlockTranslucent(i7 + 1, i8, i9) && !this.isBlockTranslucent(i7 + 1, i8 + 1, i9);
				bool z16 = !this.isBlockTranslucent(i7, i8, i9 - 1) && !this.isBlockTranslucent(i7, i8 + 1, i9 - 1);
				bool z17 = !this.isBlockTranslucent(i7, i8, i9 + 1) && !this.isBlockTranslucent(i7, i8 + 1, i9 + 1);
				sbyte b18 = -1;
				double d19 = 9999.0D;
				if (z14 && d10 < d19)
				{
					d19 = d10;
					b18 = 0;
				}

				if (z15 && 1.0D - d10 < d19)
				{
					d19 = 1.0D - d10;
					b18 = 1;
				}

				if (z16 && d12 < d19)
				{
					d19 = d12;
					b18 = 4;
				}

				if (z17 && 1.0D - d12 < d19)
				{
					d19 = 1.0D - d12;
					b18 = 5;
				}

				float f21 = 0.1F;
				if (b18 == 0)
				{
					this.motionX = (double)(-f21);
				}

				if (b18 == 1)
				{
					this.motionX = (double)f21;
				}

				if (b18 == 4)
				{
					this.motionZ = (double)(-f21);
				}

				if (b18 == 5)
				{
					this.motionZ = (double)f21;
				}
			}

			return false;
		}

		public override bool Sprinting
		{
			set
			{
				base.Sprinting = value;
				this.sprintingTicksLeft = value ? 600 : 0;
			}
		}

		public virtual void setXPStats(float f1, int i2, int i3)
		{
			this.experience = f1;
			this.experienceTotal = i2;
			this.experienceLevel = i3;
		}
	}

}