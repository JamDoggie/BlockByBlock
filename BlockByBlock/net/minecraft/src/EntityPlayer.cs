using System;

namespace net.minecraft.src
{

	public abstract class EntityPlayer : EntityLiving
	{
		private bool InstanceFieldsInitialized = false;

		private void InitializeInstanceFields()
		{
			inventory = new InventoryPlayer(this);
		}

		public InventoryPlayer inventory;
		public Container inventorySlots;
		public Container craftingInventory;
		protected internal FoodStats foodStats = new FoodStats();
		protected internal int flyToggleTimer = 0;
		public sbyte field_9371_f = 0;
		public int score = 0;
		public float prevCameraYaw;
		public float cameraYaw;
		public bool isSwinging = false;
		public int swingProgressInt = 0;
		public string username;
		public int dimension;
		public string playerCloakUrl;
		public int xpCooldown = 0;
		public double field_20066_r;
		public double field_20065_s;
		public double field_20064_t;
		public double field_20063_u;
		public double field_20062_v;
		public double field_20061_w;
		protected internal bool sleeping;
		public ChunkCoordinates playerLocation;
		private int sleepTimer;
		public float field_22063_x;
		public float field_22062_y;
		public float field_22061_z;
		private ChunkCoordinates spawnChunk;
		private ChunkCoordinates startMinecartRidingCoordinate;
		public int timeUntilPortal = 20;
		protected internal bool inPortal = false;
		public float timeInPortal;
		public float prevTimeInPortal;
		public PlayerCapabilities capabilities = new PlayerCapabilities();
		public int experienceLevel;
		public int experienceTotal;
		public float experience;
		private ItemStack itemInUse;
		private int itemInUseCount;
		protected internal float speedOnGround = 0.1F;
		protected internal float speedInAir = 0.02F;
		public EntityFishHook fishEntity = null;

		public EntityPlayer(World world1) : base(world1)
		{
			if (!InstanceFieldsInitialized)
			{
				InitializeInstanceFields();
				InstanceFieldsInitialized = true;
			}
			this.inventorySlots = new ContainerPlayer(this.inventory, !world1.isRemote);
			this.craftingInventory = this.inventorySlots;
			this.yOffset = 1.62F;
			ChunkCoordinates chunkCoordinates2 = world1.SpawnPoint;
			this.setLocationAndAngles((double)chunkCoordinates2.posX + 0.5D, (double)(chunkCoordinates2.posY + 1), (double)chunkCoordinates2.posZ + 0.5D, 0.0F, 0.0F);
			this.entityType = "humanoid";
			this.field_9353_B = 180.0F;
			this.fireResistance = 20;
			this.texture = "/mob/char.png";
		}

		public override int MaxHealth
		{
			get
			{
				return 20;
			}
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(16, (sbyte)0);
			this.dataWatcher.addObject(17, (sbyte)0);
		}

		public virtual ItemStack ItemInUse
		{
			get
			{
				return this.itemInUse;
			}
		}

		public virtual int ItemInUseCount
		{
			get
			{
				return this.itemInUseCount;
			}
		}

		public virtual bool UsingItem
		{
			get
			{
				return this.itemInUse != null;
			}
		}

		public virtual int ItemInUseDuration
		{
			get
			{
				return this.UsingItem ? this.itemInUse.MaxItemUseDuration - this.itemInUseCount : 0;
			}
		}

		public virtual void stopUsingItem()
		{
			if (this.itemInUse != null)
			{
				this.itemInUse.onPlayerStoppedUsing(this.worldObj, this, this.itemInUseCount);
			}

			this.clearItemInUse();
		}

		public virtual void clearItemInUse()
		{
			this.itemInUse = null;
			this.itemInUseCount = 0;
			if (!this.worldObj.isRemote)
			{
				this.Eating = false;
			}

		}

		public override bool Blocking
		{
			get
			{
				return this.UsingItem && Item.itemsList[this.itemInUse.itemID].getItemUseAction(this.itemInUse) == EnumAction.block;
			}
		}

		public override void onUpdate()
		{
			if (this.itemInUse != null)
			{
				ItemStack itemStack1 = this.inventory.CurrentItem;
				if (itemStack1 != this.itemInUse)
				{
					this.clearItemInUse();
				}
				else
				{
					if (this.itemInUseCount <= 25 && this.itemInUseCount % 4 == 0)
					{
						this.updateItemUse(itemStack1, 5);
					}

					if (--this.itemInUseCount == 0 && !this.worldObj.isRemote)
					{
						this.onItemUseFinish();
					}
				}
			}

			if (this.xpCooldown > 0)
			{
				--this.xpCooldown;
			}

			if (this.PlayerSleeping)
			{
				++this.sleepTimer;
				if (this.sleepTimer > 100)
				{
					this.sleepTimer = 100;
				}

				if (!this.worldObj.isRemote)
				{
					if (!this.InBed)
					{
						this.wakeUpPlayer(true, true, false);
					}
					else if (this.worldObj.Daytime)
					{
						this.wakeUpPlayer(false, true, true);
					}
				}
			}
			else if (this.sleepTimer > 0)
			{
				++this.sleepTimer;
				if (this.sleepTimer >= 110)
				{
					this.sleepTimer = 0;
				}
			}

			base.onUpdate();
			if (!this.worldObj.isRemote && this.craftingInventory != null && !this.craftingInventory.canInteractWith(this))
			{
				this.closeScreen();
				this.craftingInventory = this.inventorySlots;
			}

			if (this.capabilities.isFlying)
			{
				for (int i9 = 0; i9 < 8; ++i9)
				{
				}
			}

			if (this.Burning && this.capabilities.disableDamage)
			{
				this.extinguish();
			}

			this.field_20066_r = this.field_20063_u;
			this.field_20065_s = this.field_20062_v;
			this.field_20064_t = this.field_20061_w;
			double d10 = this.posX - this.field_20063_u;
			double d3 = this.posY - this.field_20062_v;
			double d5 = this.posZ - this.field_20061_w;
			double d7 = 10.0D;
			if (d10 > d7)
			{
				this.field_20066_r = this.field_20063_u = this.posX;
			}

			if (d5 > d7)
			{
				this.field_20064_t = this.field_20061_w = this.posZ;
			}

			if (d3 > d7)
			{
				this.field_20065_s = this.field_20062_v = this.posY;
			}

			if (d10 < -d7)
			{
				this.field_20066_r = this.field_20063_u = this.posX;
			}

			if (d5 < -d7)
			{
				this.field_20064_t = this.field_20061_w = this.posZ;
			}

			if (d3 < -d7)
			{
				this.field_20065_s = this.field_20062_v = this.posY;
			}

			this.field_20063_u += d10 * 0.25D;
			this.field_20061_w += d5 * 0.25D;
			this.field_20062_v += d3 * 0.25D;
			this.addStat(StatList.minutesPlayedStat, 1);
			if (this.ridingEntity == null)
			{
				this.startMinecartRidingCoordinate = null;
			}

			if (!this.worldObj.isRemote)
			{
				this.foodStats.onUpdate(this);
			}

		}

		protected internal virtual void updateItemUse(ItemStack itemStack1, int i2)
		{
			if (itemStack1.ItemUseAction == EnumAction.drink)
			{
				this.worldObj.playSoundAtEntity(this, "random.drink", 0.5F, this.worldObj.rand.nextFloat() * 0.1F + 0.9F);
			}

			if (itemStack1.ItemUseAction == EnumAction.eat)
			{
				for (int i3 = 0; i3 < i2; ++i3)
				{
					Vec3D vec3D4 = Vec3D.createVector(((double)this.rand.nextFloat() - 0.5D) * 0.1D, MathHelper.NextDouble * 0.1D + 0.1D, 0.0D);
					vec3D4.rotateAroundX(-this.rotationPitch * (float)Math.PI / 180.0F);
					vec3D4.rotateAroundY(-this.rotationYaw * (float)Math.PI / 180.0F);
					Vec3D vec3D5 = Vec3D.createVector(((double)this.rand.nextFloat() - 0.5D) * 0.3D, (double)(-this.rand.nextFloat()) * 0.6D - 0.3D, 0.6D);
					vec3D5.rotateAroundX(-this.rotationPitch * (float)Math.PI / 180.0F);
					vec3D5.rotateAroundY(-this.rotationYaw * (float)Math.PI / 180.0F);
					vec3D5 = vec3D5.addVector(this.posX, this.posY + (double)this.EyeHeight, this.posZ);
					this.worldObj.spawnParticle("iconcrack_" + itemStack1.Item.shiftedIndex, vec3D5.xCoord, vec3D5.yCoord, vec3D5.zCoord, vec3D4.xCoord, vec3D4.yCoord + 0.05D, vec3D4.zCoord);
				}

				this.worldObj.playSoundAtEntity(this, "random.eat", 0.5F + 0.5F * (float)this.rand.Next(2), (this.rand.nextFloat() - this.rand.nextFloat()) * 0.2F + 1.0F);
			}

		}

		protected internal virtual void onItemUseFinish()
		{
			if (this.itemInUse != null)
			{
				this.updateItemUse(this.itemInUse, 16);
				int i1 = this.itemInUse.stackSize;
				ItemStack itemStack2 = this.itemInUse.onFoodEaten(this.worldObj, this);
				if (itemStack2 != this.itemInUse || itemStack2 != null && itemStack2.stackSize != i1)
				{
					this.inventory.mainInventory[this.inventory.currentItem] = itemStack2;
					if (itemStack2.stackSize == 0)
					{
						this.inventory.mainInventory[this.inventory.currentItem] = null;
					}
				}

				this.clearItemInUse();
			}

		}

		public override void handleHealthUpdate(sbyte b1)
		{
			if (b1 == 9)
			{
				this.onItemUseFinish();
			}
			else
			{
				base.handleHealthUpdate(b1);
			}

		}

		protected internal override bool MovementBlocked
		{
			get
			{
				return this.Health <= 0 || this.PlayerSleeping;
			}
		}

		protected internal virtual void closeScreen()
		{
			this.craftingInventory = this.inventorySlots;
		}

		public override void updateCloak()
		{
			this.playerCloakUrl = "http://s3.amazonaws.com/MinecraftCloaks/" + this.username + ".png";
			this.cloakUrl = this.playerCloakUrl;
		}

		public override void updateRidden()
		{
			double d1 = this.posX;
			double d3 = this.posY;
			double d5 = this.posZ;
			base.updateRidden();
			this.prevCameraYaw = this.cameraYaw;
			this.cameraYaw = 0.0F;
			this.addMountedMovementStat(this.posX - d1, this.posY - d3, this.posZ - d5);
		}

		public override void preparePlayerToSpawn()
		{
			this.yOffset = 1.62F;
			this.setSize(0.6F, 1.8F);
			base.preparePlayerToSpawn();
			this.EntityHealth = this.MaxHealth;
			this.deathTime = 0;
		}

		private int SwingSpeedModifier
		{
			get
			{
				return this.isPotionActive(Potion.digSpeed) ? 6 - (1 + this.getActivePotionEffect(Potion.digSpeed).Amplifier) * 1 : (this.isPotionActive(Potion.digSlowdown) ? 6 + (1 + this.getActivePotionEffect(Potion.digSlowdown).Amplifier) * 2 : 6);
			}
		}

		protected internal override void updateEntityActionState()
		{
			int i1 = this.SwingSpeedModifier;
			if (this.isSwinging)
			{
				++this.swingProgressInt;
				if (this.swingProgressInt >= i1)
				{
					this.swingProgressInt = 0;
					this.isSwinging = false;
				}
			}
			else
			{
				this.swingProgressInt = 0;
			}

			this.swingProgress = (float)this.swingProgressInt / (float)i1;
		}

		public override void onLivingUpdate()
		{
			if (this.flyToggleTimer > 0)
			{
				--this.flyToggleTimer;
			}

			if (this.worldObj.difficultySetting == 0 && this.Health < this.MaxHealth && this.ticksExisted % 20 * 12 == 0)
			{
				this.heal(1);
			}

			this.inventory.decrementAnimations();
			this.prevCameraYaw = this.cameraYaw;
			base.onLivingUpdate();
			this.landMovementFactor = this.speedOnGround;
			this.jumpMovementFactor = this.speedInAir;
			if (this.Sprinting)
			{
				this.landMovementFactor = (float)((double)this.landMovementFactor + (double)this.speedOnGround * 0.3D);
				this.jumpMovementFactor = (float)((double)this.jumpMovementFactor + (double)this.speedInAir * 0.3D);
			}

			float f1 = MathHelper.sqrt_double(this.motionX * this.motionX + this.motionZ * this.motionZ);
			float f2 = (float)Math.Atan(-this.motionY * (double)0.2F) * 15.0F;
			if (f1 > 0.1F)
			{
				f1 = 0.1F;
			}

			if (!this.onGround || this.Health <= 0)
			{
				f1 = 0.0F;
			}

			if (this.onGround || this.Health <= 0)
			{
				f2 = 0.0F;
			}

			this.cameraYaw += (f1 - this.cameraYaw) * 0.4F;
			this.cameraPitch += (f2 - this.cameraPitch) * 0.8F;
			if (this.Health > 0)
			{
				System.Collections.IList list3 = this.worldObj.getEntitiesWithinAABBExcludingEntity(this, this.boundingBox.expand(1.0D, 0.0D, 1.0D));
				if (list3 != null)
				{
					for (int i4 = 0; i4 < list3.Count; ++i4)
					{
						Entity entity5 = (Entity)list3[i4];
						if (!entity5.isDead)
						{
							this.collideWithPlayer(entity5);
						}
					}
				}
			}

		}

		private void collideWithPlayer(Entity entity1)
		{
			entity1.onCollideWithPlayer(this);
		}

		public virtual int Score
		{
			get
			{
				return this.score;
			}
		}

		public override void onDeath(DamageSource damageSource1)
		{
			base.onDeath(damageSource1);
			this.setSize(0.2F, 0.2F);
			this.setPosition(this.posX, this.posY, this.posZ);
			this.motionY = (double)0.1F;
			if (this.username.Equals("Notch"))
			{
				this.dropPlayerItemWithRandomChoice(new ItemStack(Item.appleRed, 1), true);
			}

			this.inventory.dropAllItems();
			if (damageSource1 != null)
			{
				this.motionX = (double)(-MathHelper.cos((this.attackedAtYaw + this.rotationYaw) * (float)Math.PI / 180.0F) * 0.1F);
				this.motionZ = (double)(-MathHelper.sin((this.attackedAtYaw + this.rotationYaw) * (float)Math.PI / 180.0F) * 0.1F);
			}
			else
			{
				this.motionX = this.motionZ = 0.0D;
			}

			this.yOffset = 0.1F;
			this.addStat(StatList.deathsStat, 1);
		}

		public override void addToPlayerScore(Entity entity1, int i2)
		{
			this.score += i2;
			if (entity1 is EntityPlayer)
			{
				this.addStat(StatList.playerKillsStat, 1);
			}
			else
			{
				this.addStat(StatList.mobKillsStat, 1);
			}

		}

		protected internal override int decreaseAirSupply(int i1)
		{
			int i2 = EnchantmentHelper.getRespiration(this.inventory);
			return i2 > 0 && this.rand.Next(i2 + 1) > 0 ? i1 : base.decreaseAirSupply(i1);
		}

		public virtual EntityItem dropOneItem()
		{
			return this.dropPlayerItemWithRandomChoice(this.inventory.decrStackSize(this.inventory.currentItem, 1), false);
		}

		public virtual EntityItem dropPlayerItem(ItemStack itemStack1)
		{
			return this.dropPlayerItemWithRandomChoice(itemStack1, false);
		}

		public virtual EntityItem dropPlayerItemWithRandomChoice(ItemStack itemStack1, bool z2)
		{
			if (itemStack1 == null)
			{
				return null;
			}
			else
			{
				EntityItem entityItem3 = new EntityItem(this.worldObj, this.posX, this.posY - (double)0.3F + (double)this.EyeHeight, this.posZ, itemStack1);
				entityItem3.delayBeforeCanPickup = 40;
				float f4 = 0.1F;
				float f5;
				if (z2)
				{
					f5 = this.rand.nextFloat() * 0.5F;
					float f6 = this.rand.nextFloat() * (float)Math.PI * 2.0F;
					entityItem3.motionX = (double)(-MathHelper.sin(f6) * f5);
					entityItem3.motionZ = (double)(MathHelper.cos(f6) * f5);
					entityItem3.motionY = (double)0.2F;
				}
				else
				{
					f4 = 0.3F;
					entityItem3.motionX = (double)(-MathHelper.sin(this.rotationYaw / 180.0F * (float)Math.PI) * MathHelper.cos(this.rotationPitch / 180.0F * (float)Math.PI) * f4);
					entityItem3.motionZ = (double)(MathHelper.cos(this.rotationYaw / 180.0F * (float)Math.PI) * MathHelper.cos(this.rotationPitch / 180.0F * (float)Math.PI) * f4);
					entityItem3.motionY = (double)(-MathHelper.sin(this.rotationPitch / 180.0F * (float)Math.PI) * f4 + 0.1F);
					f4 = 0.02F;
					f5 = this.rand.nextFloat() * (float)Math.PI * 2.0F;
					f4 *= this.rand.nextFloat();
					entityItem3.motionX += Math.Cos((double)f5) * (double)f4;
					entityItem3.motionY += (double)((this.rand.nextFloat() - this.rand.nextFloat()) * 0.1F);
					entityItem3.motionZ += Math.Sin((double)f5) * (double)f4;
				}

				this.joinEntityItemWithWorld(entityItem3);
				this.addStat(StatList.dropStat, 1);
				return entityItem3;
			}
		}

		protected internal virtual void joinEntityItemWithWorld(EntityItem entityItem1)
		{
			this.worldObj.spawnEntityInWorld(entityItem1);
		}

		public virtual float getCurrentPlayerStrVsBlock(Block block1)
		{
			float f2 = this.inventory.getStrVsBlock(block1);
			float f3 = f2;
			int i4 = EnchantmentHelper.getEfficiencyModifier(this.inventory);
			if (i4 > 0 && this.inventory.canHarvestBlock(block1))
			{
				f3 = f2 + (float)(i4 * i4 + 1);
			}

			if (this.isPotionActive(Potion.digSpeed))
			{
				f3 *= 1.0F + (float)(this.getActivePotionEffect(Potion.digSpeed).Amplifier + 1) * 0.2F;
			}

			if (this.isPotionActive(Potion.digSlowdown))
			{
				f3 *= 1.0F - (float)(this.getActivePotionEffect(Potion.digSlowdown).Amplifier + 1) * 0.2F;
			}

			if (this.isInsideOfMaterial(Material.water) && !EnchantmentHelper.getAquaAffinityModifier(this.inventory))
			{
				f3 /= 5.0F;
			}

			if (!this.onGround)
			{
				f3 /= 5.0F;
			}

			return f3;
		}

		public virtual bool canHarvestBlock(Block block1)
		{
			return this.inventory.canHarvestBlock(block1);
		}

		public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
		{
			base.readEntityFromNBT(nBTTagCompound1);
			NBTTagList nBTTagList2 = nBTTagCompound1.getTagList("Inventory");
			this.inventory.readFromNBT(nBTTagList2);
			this.dimension = nBTTagCompound1.getInteger("Dimension");
			this.sleeping = nBTTagCompound1.getBoolean("Sleeping");
			this.sleepTimer = nBTTagCompound1.getShort("SleepTimer");
			this.experience = nBTTagCompound1.getFloat("XpP");
			this.experienceLevel = nBTTagCompound1.getInteger("XpLevel");
			this.experienceTotal = nBTTagCompound1.getInteger("XpTotal");
			if (this.sleeping)
			{
				this.playerLocation = new ChunkCoordinates(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY), MathHelper.floor_double(this.posZ));
				this.wakeUpPlayer(true, true, false);
			}

			if (nBTTagCompound1.hasKey("SpawnX") && nBTTagCompound1.hasKey("SpawnY") && nBTTagCompound1.hasKey("SpawnZ"))
			{
				this.spawnChunk = new ChunkCoordinates(nBTTagCompound1.getInteger("SpawnX"), nBTTagCompound1.getInteger("SpawnY"), nBTTagCompound1.getInteger("SpawnZ"));
			}

			this.foodStats.readNBT(nBTTagCompound1);
			this.capabilities.readCapabilitiesFromNBT(nBTTagCompound1);
		}

		public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
		{
			base.writeEntityToNBT(nBTTagCompound1);
			nBTTagCompound1.setTag("Inventory", this.inventory.writeToNBT(new NBTTagList()));
			nBTTagCompound1.setInteger("Dimension", this.dimension);
			nBTTagCompound1.setBoolean("Sleeping", this.sleeping);
			nBTTagCompound1.setShort("SleepTimer", (short)this.sleepTimer);
			nBTTagCompound1.setFloat("XpP", this.experience);
			nBTTagCompound1.setInteger("XpLevel", this.experienceLevel);
			nBTTagCompound1.setInteger("XpTotal", this.experienceTotal);
			if (this.spawnChunk != null)
			{
				nBTTagCompound1.setInteger("SpawnX", this.spawnChunk.posX);
				nBTTagCompound1.setInteger("SpawnY", this.spawnChunk.posY);
				nBTTagCompound1.setInteger("SpawnZ", this.spawnChunk.posZ);
			}

			this.foodStats.writeNBT(nBTTagCompound1);
			this.capabilities.writeCapabilitiesToNBT(nBTTagCompound1);
		}

		public virtual void displayGUIChest(IInventory iInventory1)
		{
		}

		public virtual void displayGUIEnchantment(int i1, int i2, int i3)
		{
		}

		public virtual void displayWorkbenchGUI(int i1, int i2, int i3)
		{
		}

		public virtual void onItemPickup(Entity entity1, int i2)
		{
		}

		public override float EyeHeight
		{
			get
			{
				return 0.12F;
			}
		}

		protected internal virtual void resetHeight()
		{
			this.yOffset = 1.62F;
		}

		public override bool attackEntityFrom(DamageSource damageSource1, int i2)
		{
			if (this.capabilities.disableDamage && !damageSource1.canHarmInCreative())
			{
				return false;
			}
			else
			{
				this.entityAge = 0;
				if (this.Health <= 0)
				{
					return false;
				}
				else
				{
					if (this.PlayerSleeping && !this.worldObj.isRemote)
					{
						this.wakeUpPlayer(true, true, false);
					}

					Entity entity3 = damageSource1.Entity;
					if (entity3 is EntityMob || entity3 is EntityArrow)
					{
						if (this.worldObj.difficultySetting == 0)
						{
							i2 = 0;
						}

						if (this.worldObj.difficultySetting == 1)
						{
							i2 = i2 / 2 + 1;
						}

						if (this.worldObj.difficultySetting == 3)
						{
							i2 = i2 * 3 / 2;
						}
					}

					if (i2 == 0)
					{
						return false;
					}
					else
					{
						Entity entity4 = entity3;
						if (entity3 is EntityArrow && ((EntityArrow)entity3).shootingEntity != null)
						{
							entity4 = ((EntityArrow)entity3).shootingEntity;
						}

						if (entity4 is EntityLiving)
						{
							this.alertWolves((EntityLiving)entity4, false);
						}

						this.addStat(StatList.damageTakenStat, i2);
						return base.attackEntityFrom(damageSource1, i2);
					}
				}
			}
		}

		protected internal override int applyPotionDamageCalculations(DamageSource damageSource1, int i2)
		{
			int i3 = base.applyPotionDamageCalculations(damageSource1, i2);
			if (i3 <= 0)
			{
				return 0;
			}
			else
			{
				int i4 = EnchantmentHelper.getEnchantmentModifierDamage(this.inventory, damageSource1);
				if (i4 > 20)
				{
					i4 = 20;
				}

				if (i4 > 0 && i4 <= 20)
				{
					int i5 = 25 - i4;
					int i6 = i3 * i5 + this.carryoverDamage;
					i3 = i6 / 25;
					this.carryoverDamage = i6 % 25;
				}

				return i3;
			}
		}

		protected internal virtual bool PVPEnabled
		{
			get
			{
				return false;
			}
		}

		protected internal virtual void alertWolves(EntityLiving entityLiving1, bool z2)
		{
			if (!(entityLiving1 is EntityCreeper) && !(entityLiving1 is EntityGhast))
			{
				if (entityLiving1 is EntityWolf)
				{
					EntityWolf entityWolf3 = (EntityWolf)entityLiving1;
					if (entityWolf3.Tamed && this.username.Equals(entityWolf3.OwnerName))
					{
						return;
					}
				}

				if (!(entityLiving1 is EntityPlayer) || this.PVPEnabled)
				{
					System.Collections.IList list7 = this.worldObj.getEntitiesWithinAABB(typeof(EntityWolf), AxisAlignedBB.getBoundingBoxFromPool(this.posX, this.posY, this.posZ, this.posX + 1.0D, this.posY + 1.0D, this.posZ + 1.0D).expand(16.0D, 4.0D, 16.0D));
					System.Collections.IEnumerator iterator4 = list7.GetEnumerator();

					while (true)
					{
						EntityWolf entityWolf6;
						do
						{
							do
							{
								do
								{
									do
									{
//JAVA TO C# CONVERTER TODO TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
										if (!iterator4.hasNext())
										{
											return;
										}

//JAVA TO C# CONVERTER TODO TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
										Entity entity5 = (Entity)iterator4.next();
										entityWolf6 = (EntityWolf)entity5;
									} while (!entityWolf6.Tamed);
								} while (entityWolf6.EntityToAttack != null);
							} while (!this.username.Equals(entityWolf6.OwnerName));
						} while (z2 && entityWolf6.Sitting);

						entityWolf6.func_48140_f(false);
						entityWolf6.Target = entityLiving1;
					}
				}
			}
		}

		protected internal override void damageArmor(int i1)
		{
			this.inventory.damageArmor(i1);
		}

		public override int TotalArmorValue
		{
			get
			{
				return this.inventory.TotalArmorValue;
			}
		}

		protected internal override void damageEntity(DamageSource damageSource1, int i2)
		{
			if (!damageSource1.Unblockable && this.Blocking)
			{
				i2 = 1 + i2 >> 1;
			}

			i2 = this.applyArmorCalculations(damageSource1, i2);
			i2 = this.applyPotionDamageCalculations(damageSource1, i2);
			this.addExhaustion(damageSource1.HungerDamage);
			this.health -= i2;
		}

		public virtual void displayGUIFurnace(TileEntityFurnace tileEntityFurnace1)
		{
		}

		public virtual void displayGUIDispenser(TileEntityDispenser tileEntityDispenser1)
		{
		}

		public virtual void displayGUIEditSign(TileEntitySign tileEntitySign1)
		{
		}

		public virtual void displayGUIBrewingStand(TileEntityBrewingStand tileEntityBrewingStand1)
		{
		}

		public virtual void useCurrentItemOnEntity(Entity entity1)
		{
			if (!entity1.interact(this))
			{
				ItemStack itemStack2 = this.CurrentEquippedItem;
				if (itemStack2 != null && entity1 is EntityLiving)
				{
					itemStack2.useItemOnEntity((EntityLiving)entity1);
					if (itemStack2.stackSize <= 0)
					{
						itemStack2.onItemDestroyedByUse(this);
						this.destroyCurrentEquippedItem();
					}
				}

			}
		}

		public virtual ItemStack CurrentEquippedItem
		{
			get
			{
				return this.inventory.CurrentItem;
			}
		}

		public virtual void destroyCurrentEquippedItem()
		{
			this.inventory.setInventorySlotContents(this.inventory.currentItem, (ItemStack)null);
		}

		public override double YOffset
		{
			get
			{
				return (double)(this.yOffset - 0.5F);
			}
		}

		public virtual void swingItem()
		{
			if (!this.isSwinging || this.swingProgressInt >= this.SwingSpeedModifier / 2 || this.swingProgressInt < 0)
			{
				this.swingProgressInt = -1;
				this.isSwinging = true;
			}

		}

		public virtual void attackTargetEntityWithCurrentItem(Entity entity1)
		{
			if (entity1.canAttackWithItem())
			{
				int i2 = this.inventory.getDamageVsEntity(entity1);
				if (this.isPotionActive(Potion.damageBoost))
				{
					i2 += 3 << this.getActivePotionEffect(Potion.damageBoost).Amplifier;
				}

				if (this.isPotionActive(Potion.weakness))
				{
					i2 -= 2 << this.getActivePotionEffect(Potion.weakness).Amplifier;
				}

				int i3 = 0;
				int i4 = 0;
				if (entity1 is EntityLiving)
				{
					i4 = EnchantmentHelper.getEnchantmentModifierLiving(this.inventory, (EntityLiving)entity1);
					i3 += EnchantmentHelper.getKnockbackModifier(this.inventory, (EntityLiving)entity1);
				}

				if (this.Sprinting)
				{
					++i3;
				}

				if (i2 > 0 || i4 > 0)
				{
					bool z5 = this.fallDistance > 0.0F && !this.onGround && !this.OnLadder && !this.InWater && !this.isPotionActive(Potion.blindness) && this.ridingEntity == null && entity1 is EntityLiving;
					if (z5)
					{
						i2 += this.rand.Next(i2 / 2 + 2);
					}

					i2 += i4;
					bool z6 = entity1.attackEntityFrom(DamageSource.causePlayerDamage(this), i2);
					if (z6)
					{
						if (i3 > 0)
						{
							entity1.addVelocity((double)(-MathHelper.sin(this.rotationYaw * (float)Math.PI / 180.0F) * (float)i3 * 0.5F), 0.1D, (double)(MathHelper.cos(this.rotationYaw * (float)Math.PI / 180.0F) * (float)i3 * 0.5F));
							this.motionX *= 0.6D;
							this.motionZ *= 0.6D;
							this.Sprinting = false;
						}

						if (z5)
						{
							this.onCriticalHit(entity1);
						}

						if (i4 > 0)
						{
							this.onEnchantmentCritical(entity1);
						}

						if (i2 >= 18)
						{
							this.triggerAchievement(AchievementList.overkill);
						}

						this.setLastAttackingEntity(entity1);
					}

					ItemStack itemStack7 = this.CurrentEquippedItem;
					if (itemStack7 != null && entity1 is EntityLiving)
					{
						itemStack7.hitEntity((EntityLiving)entity1, this);
						if (itemStack7.stackSize <= 0)
						{
							itemStack7.onItemDestroyedByUse(this);
							this.destroyCurrentEquippedItem();
						}
					}

					if (entity1 is EntityLiving)
					{
						if (entity1.EntityAlive)
						{
							this.alertWolves((EntityLiving)entity1, true);
						}

						this.addStat(StatList.damageDealtStat, i2);
						int i8 = EnchantmentHelper.getFireAspectModifier(this.inventory, (EntityLiving)entity1);
						if (i8 > 0)
						{
							entity1.Fire = i8 * 4;
						}
					}

					this.addExhaustion(0.3F);
				}

			}
		}

		public virtual void onCriticalHit(Entity entity1)
		{
		}

		public virtual void onEnchantmentCritical(Entity entity1)
		{
		}

		public virtual void respawnPlayer()
		{
		}

		public abstract void func_6420_o();

		public virtual void onItemStackChanged(ItemStack itemStack1)
		{
		}

		public override void setDead()
		{
			base.setDead();
			this.inventorySlots.onCraftGuiClosed(this);
			if (this.craftingInventory != null)
			{
				this.craftingInventory.onCraftGuiClosed(this);
			}

		}

		public override bool EntityInsideOpaqueBlock
		{
			get
			{
				return !this.sleeping && base.EntityInsideOpaqueBlock;
			}
		}

		public virtual EnumStatus sleepInBedAt(int i1, int i2, int i3)
		{
			if (!this.worldObj.isRemote)
			{
				if (this.PlayerSleeping || !this.EntityAlive)
				{
					return EnumStatus.OTHER_PROBLEM;
				}

				if (!this.worldObj.worldProvider.func_48217_e())
				{
					return EnumStatus.NOT_POSSIBLE_HERE;
				}

				if (this.worldObj.Daytime)
				{
					return EnumStatus.NOT_POSSIBLE_NOW;
				}

				if (Math.Abs(this.posX - (double)i1) > 3.0D || Math.Abs(this.posY - (double)i2) > 2.0D || Math.Abs(this.posZ - (double)i3) > 3.0D)
				{
					return EnumStatus.TOO_FAR_AWAY;
				}

				double d4 = 8.0D;
				double d6 = 5.0D;
				System.Collections.IList list8 = this.worldObj.getEntitiesWithinAABB(typeof(EntityMob), AxisAlignedBB.getBoundingBoxFromPool((double)i1 - d4, (double)i2 - d6, (double)i3 - d4, (double)i1 + d4, (double)i2 + d6, (double)i3 + d4));
				if (list8.Count > 0)
				{
					return EnumStatus.NOT_SAFE;
				}
			}

			this.setSize(0.2F, 0.2F);
			this.yOffset = 0.2F;
			if (this.worldObj.blockExists(i1, i2, i3))
			{
				int i9 = this.worldObj.getBlockMetadata(i1, i2, i3);
				int i5 = BlockBed.getDirection(i9);
				float f10 = 0.5F;
				float f7 = 0.5F;
				switch (i5)
				{
				case 0:
					f7 = 0.9F;
					break;
				case 1:
					f10 = 0.1F;
					break;
				case 2:
					f7 = 0.1F;
					break;
				case 3:
					f10 = 0.9F;
				break;
				}

				this.func_22052_e(i5);
				this.setPosition((double)((float)i1 + f10), (double)((float)i2 + 0.9375F), (double)((float)i3 + f7));
			}
			else
			{
				this.setPosition((double)((float)i1 + 0.5F), (double)((float)i2 + 0.9375F), (double)((float)i3 + 0.5F));
			}

			this.sleeping = true;
			this.sleepTimer = 0;
			this.playerLocation = new ChunkCoordinates(i1, i2, i3);
			this.motionX = this.motionZ = this.motionY = 0.0D;
			if (!this.worldObj.isRemote)
			{
				this.worldObj.updateAllPlayersSleepingFlag();
			}

			return EnumStatus.OK;
		}

		private void func_22052_e(int i1)
		{
			this.field_22063_x = 0.0F;
			this.field_22061_z = 0.0F;
			switch (i1)
			{
			case 0:
				this.field_22061_z = -1.8F;
				break;
			case 1:
				this.field_22063_x = 1.8F;
				break;
			case 2:
				this.field_22061_z = 1.8F;
				break;
			case 3:
				this.field_22063_x = -1.8F;
			break;
			}

		}

		public virtual void wakeUpPlayer(bool z1, bool z2, bool z3)
		{
			this.setSize(0.6F, 1.8F);
			this.resetHeight();
			ChunkCoordinates chunkCoordinates4 = this.playerLocation;
			ChunkCoordinates chunkCoordinates5 = this.playerLocation;
			if (chunkCoordinates4 != null && this.worldObj.getBlockId(chunkCoordinates4.posX, chunkCoordinates4.posY, chunkCoordinates4.posZ) == Block.bed.blockID)
			{
				BlockBed.setBedOccupied(this.worldObj, chunkCoordinates4.posX, chunkCoordinates4.posY, chunkCoordinates4.posZ, false);
				chunkCoordinates5 = BlockBed.getNearestEmptyChunkCoordinates(this.worldObj, chunkCoordinates4.posX, chunkCoordinates4.posY, chunkCoordinates4.posZ, 0);
				if (chunkCoordinates5 == null)
				{
					chunkCoordinates5 = new ChunkCoordinates(chunkCoordinates4.posX, chunkCoordinates4.posY + 1, chunkCoordinates4.posZ);
				}

				this.setPosition((double)((float)chunkCoordinates5.posX + 0.5F), (double)((float)chunkCoordinates5.posY + this.yOffset + 0.1F), (double)((float)chunkCoordinates5.posZ + 0.5F));
			}

			this.sleeping = false;
			if (!this.worldObj.isRemote && z2)
			{
				this.worldObj.updateAllPlayersSleepingFlag();
			}

			if (z1)
			{
				this.sleepTimer = 0;
			}
			else
			{
				this.sleepTimer = 100;
			}

			if (z3)
			{
				this.SpawnChunk = this.playerLocation;
			}

		}

		private bool InBed
		{
			get
			{
				return this.worldObj.getBlockId(this.playerLocation.posX, this.playerLocation.posY, this.playerLocation.posZ) == Block.bed.blockID;
			}
		}

		public static ChunkCoordinates verifyRespawnCoordinates(World world0, ChunkCoordinates chunkCoordinates1)
		{
			IChunkProvider iChunkProvider2 = world0.ChunkProvider;
			iChunkProvider2.loadChunk(chunkCoordinates1.posX - 3 >> 4, chunkCoordinates1.posZ - 3 >> 4);
			iChunkProvider2.loadChunk(chunkCoordinates1.posX + 3 >> 4, chunkCoordinates1.posZ - 3 >> 4);
			iChunkProvider2.loadChunk(chunkCoordinates1.posX - 3 >> 4, chunkCoordinates1.posZ + 3 >> 4);
			iChunkProvider2.loadChunk(chunkCoordinates1.posX + 3 >> 4, chunkCoordinates1.posZ + 3 >> 4);
			if (world0.getBlockId(chunkCoordinates1.posX, chunkCoordinates1.posY, chunkCoordinates1.posZ) != Block.bed.blockID)
			{
				return null;
			}
			else
			{
				ChunkCoordinates chunkCoordinates3 = BlockBed.getNearestEmptyChunkCoordinates(world0, chunkCoordinates1.posX, chunkCoordinates1.posY, chunkCoordinates1.posZ, 0);
				return chunkCoordinates3;
			}
		}

		public virtual float BedOrientationInDegrees
		{
			get
			{
				if (this.playerLocation != null)
				{
					int i1 = this.worldObj.getBlockMetadata(this.playerLocation.posX, this.playerLocation.posY, this.playerLocation.posZ);
					int i2 = BlockBed.getDirection(i1);
					switch (i2)
					{
					case 0:
						return 90.0F;
					case 1:
						return 0.0F;
					case 2:
						return 270.0F;
					case 3:
						return 180.0F;
					}
				}
    
				return 0.0F;
			}
		}

		public override bool PlayerSleeping
		{
			get
			{
				return this.sleeping;
			}
		}

		public virtual bool PlayerFullyAsleep
		{
			get
			{
				return this.sleeping && this.sleepTimer >= 100;
			}
		}

		public virtual int SleepTimer
		{
			get
			{
				return this.sleepTimer;
			}
		}

		public virtual void addChatMessage(string string1)
		{
		}

		public virtual ChunkCoordinates SpawnChunk
		{
			get
			{
				return this.spawnChunk;
			}
			set
			{
				if (value != null)
				{
					this.spawnChunk = new ChunkCoordinates(value);
				}
				else
				{
					this.spawnChunk = null;
				}
    
			}
		}


		public virtual void triggerAchievement(StatBase statBase1)
		{
			this.addStat(statBase1, 1);
		}

		public virtual void addStat(StatBase statBase1, int i2)
		{
		}

		protected internal override void jump()
		{
			base.jump();
			this.addStat(StatList.jumpStat, 1);
			if (this.Sprinting)
			{
				this.addExhaustion(0.8F);
			}
			else
			{
				this.addExhaustion(0.2F);
			}

		}

		public override void moveEntityWithHeading(float f1, float f2)
		{
			double d3 = this.posX;
			double d5 = this.posY;
			double d7 = this.posZ;
			if (this.capabilities.isFlying)
			{
				double d9 = this.motionY;
				float f11 = this.jumpMovementFactor;
				this.jumpMovementFactor = 0.05F;
				base.moveEntityWithHeading(f1, f2);
				this.motionY = d9 * 0.6D;
				this.jumpMovementFactor = f11;
			}
			else
			{
				base.moveEntityWithHeading(f1, f2);
			}

			this.addMovementStat(this.posX - d3, this.posY - d5, this.posZ - d7);
		}

		public virtual void addMovementStat(double d1, double d3, double d5)
		{
			if (this.ridingEntity == null)
			{
				int i7;
				if (this.isInsideOfMaterial(Material.water))
				{
					i7 = (int)Math.Round(MathHelper.sqrt_double(d1 * d1 + d3 * d3 + d5 * d5) * 100.0F, MidpointRounding.AwayFromZero);
					if (i7 > 0)
					{
						this.addStat(StatList.distanceDoveStat, i7);
						this.addExhaustion(0.015F * (float)i7 * 0.01F);
					}
				}
				else if (this.InWater)
				{
					i7 = (int)Math.Round(MathHelper.sqrt_double(d1 * d1 + d5 * d5) * 100.0F, MidpointRounding.AwayFromZero);
					if (i7 > 0)
					{
						this.addStat(StatList.distanceSwumStat, i7);
						this.addExhaustion(0.015F * (float)i7 * 0.01F);
					}
				}
				else if (this.OnLadder)
				{
					if (d3 > 0.0D)
					{
						this.addStat(StatList.distanceClimbedStat, (int)(long)Math.Round(d3 * 100.0D, MidpointRounding.AwayFromZero));
					}
				}
				else if (this.onGround)
				{
					i7 = (int)Math.Round(MathHelper.sqrt_double(d1 * d1 + d5 * d5) * 100.0F, MidpointRounding.AwayFromZero);
					if (i7 > 0)
					{
						this.addStat(StatList.distanceWalkedStat, i7);
						if (this.Sprinting)
						{
							this.addExhaustion(0.099999994F * (float)i7 * 0.01F);
						}
						else
						{
							this.addExhaustion(0.01F * (float)i7 * 0.01F);
						}
					}
				}
				else
				{
					i7 = (int)Math.Round(MathHelper.sqrt_double(d1 * d1 + d5 * d5) * 100.0F, MidpointRounding.AwayFromZero);
					if (i7 > 25)
					{
						this.addStat(StatList.distanceFlownStat, i7);
					}
				}

			}
		}

		private void addMountedMovementStat(double d1, double d3, double d5)
		{
			if (this.ridingEntity != null)
			{
				int i7 = (int)Math.Round(MathHelper.sqrt_double(d1 * d1 + d3 * d3 + d5 * d5) * 100.0F, MidpointRounding.AwayFromZero);
				if (i7 > 0)
				{
					if (this.ridingEntity is EntityMinecart)
					{
						this.addStat(StatList.distanceByMinecartStat, i7);
						if (this.startMinecartRidingCoordinate == null)
						{
							this.startMinecartRidingCoordinate = new ChunkCoordinates(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY), MathHelper.floor_double(this.posZ));
						}
						else if (this.startMinecartRidingCoordinate.getEuclideanDistanceTo(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY), MathHelper.floor_double(this.posZ)) >= 1000.0D)
						{
							this.addStat(AchievementList.onARail, 1);
						}
					}
					else if (this.ridingEntity is EntityBoat)
					{
						this.addStat(StatList.distanceByBoatStat, i7);
					}
					else if (this.ridingEntity is EntityPig)
					{
						this.addStat(StatList.distanceByPigStat, i7);
					}
				}
			}

		}

		protected internal override void fall(float f1)
		{
			if (!this.capabilities.allowFlying)
			{
				if (f1 >= 2.0F)
				{
					this.addStat(StatList.distanceFallenStat, (int)(long)Math.Round((double)f1 * 100.0D, MidpointRounding.AwayFromZero));
				}

				base.fall(f1);
			}
		}

		public override void onKillEntity(EntityLiving entityLiving1)
		{
			if (entityLiving1 is EntityMob)
			{
				this.triggerAchievement(AchievementList.killEnemy);
			}

		}

		public override int getItemIcon(ItemStack itemStack1, int i2)
		{
			int i3 = base.getItemIcon(itemStack1, i2);
			if (itemStack1.itemID == Item.fishingRod.shiftedIndex && this.fishEntity != null)
			{
				i3 = itemStack1.IconIndex + 16;
			}
			else
			{
				if (itemStack1.Item.func_46058_c())
				{
					return itemStack1.Item.func_46057_a(itemStack1.ItemDamage, i2);
				}

				if (this.itemInUse != null && itemStack1.itemID == Item.bow.shiftedIndex)
				{
					int i4 = itemStack1.MaxItemUseDuration - this.itemInUseCount;
					if (i4 >= 18)
					{
						return 133;
					}

					if (i4 > 13)
					{
						return 117;
					}

					if (i4 > 0)
					{
						return 101;
					}
				}
			}

			return i3;
		}

		public override void setInPortal()
		{
			if (this.timeUntilPortal > 0)
			{
				this.timeUntilPortal = 10;
			}
			else
			{
				this.inPortal = true;
			}
		}

		public virtual void addExperience(int i1)
		{
			this.score += i1;
			int i2 = int.MaxValue - this.experienceTotal;
			if (i1 > i2)
			{
				i1 = i2;
			}

			this.experience += (float)i1 / (float)this.xpBarCap();

			for (this.experienceTotal += i1; this.experience >= 1.0F; this.experience /= (float)this.xpBarCap())
			{
				this.experience = (this.experience - 1.0F) * (float)this.xpBarCap();
				this.increaseLevel();
			}

		}

		public virtual void removeExperience(int i1)
		{
			this.experienceLevel -= i1;
			if (this.experienceLevel < 0)
			{
				this.experienceLevel = 0;
			}

		}

		public virtual int xpBarCap()
		{
			return 7 + (this.experienceLevel * 7 >> 1);
		}

		private void increaseLevel()
		{
			++this.experienceLevel;
		}

		public virtual void addExhaustion(float f1)
		{
			if (!this.capabilities.disableDamage)
			{
				if (!this.worldObj.isRemote)
				{
					this.foodStats.addExhaustion(f1);
				}

			}
		}

		public virtual FoodStats FoodStats
		{
			get
			{
				return this.foodStats;
			}
		}

		public virtual bool canEat(bool z1)
		{
			return (z1 || this.foodStats.needFood()) && !this.capabilities.disableDamage;
		}

		public virtual bool shouldHeal()
		{
			return this.Health > 0 && this.Health < this.MaxHealth;
		}

		public virtual void setItemInUse(ItemStack itemStack1, int i2)
		{
			if (itemStack1 != this.itemInUse)
			{
				this.itemInUse = itemStack1;
				this.itemInUseCount = i2;
				if (!this.worldObj.isRemote)
				{
					this.Eating = true;
				}

			}
		}

		public virtual bool canPlayerEdit(int i1, int i2, int i3)
		{
			return true;
		}

		protected internal override int getExperiencePoints(EntityPlayer entityPlayer1)
		{
			int i2 = this.experienceLevel * 7;
			return i2 > 100 ? 100 : i2;
		}

		protected internal override bool Player
		{
			get
			{
				return true;
			}
		}

		public virtual void travelToTheEnd(int i1)
		{
		}

		public virtual void copyPlayer(EntityPlayer entityPlayer1)
		{
			this.inventory.copyInventory(entityPlayer1.inventory);
			this.health = entityPlayer1.health;
			this.foodStats = entityPlayer1.foodStats;
			this.experienceLevel = entityPlayer1.experienceLevel;
			this.experienceTotal = entityPlayer1.experienceTotal;
			this.experience = entityPlayer1.experience;
			this.score = entityPlayer1.score;
		}

		protected internal override bool canTriggerWalking()
		{
			return !this.capabilities.isFlying;
		}

		public virtual void func_50009_aI()
		{
		}
	}

}