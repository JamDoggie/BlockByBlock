using System;

namespace net.minecraft.src
{
	public class EntityWolf : EntityTameable
	{
		private bool looksWithInterest = false;
		private float field_25048_b;
		private float field_25054_c;
		private bool isShaking;
		private bool field_25052_g;
		private float timeWolfIsShaking;
		private float prevTimeWolfIsShaking;

		public EntityWolf(World world1) : base(world1)
		{
			this.texture = "/mob/wolf.png";
			this.setSize(0.6F, 0.8F);
			this.moveSpeed = 0.3F;
			this.Navigator.func_48664_a(true);
			this.tasks.addTask(1, new EntityAISwimming(this));
			this.tasks.addTask(2, this.aiSit);
			this.tasks.addTask(3, new EntityAILeapAtTarget(this, 0.4F));
			this.tasks.addTask(4, new EntityAIAttackOnCollide(this, this.moveSpeed, true));
			this.tasks.addTask(5, new EntityAIFollowOwner(this, this.moveSpeed, 10.0F, 2.0F));
			this.tasks.addTask(6, new EntityAIMate(this, this.moveSpeed));
			this.tasks.addTask(7, new EntityAIWander(this, this.moveSpeed));
			this.tasks.addTask(8, new EntityAIBeg(this, 8.0F));
			this.tasks.addTask(9, new EntityAIWatchClosest(this, typeof(EntityPlayer), 8.0F));
			this.tasks.addTask(9, new EntityAILookIdle(this));
			this.targetTasks.addTask(1, new EntityAIOwnerHurtByTarget(this));
			this.targetTasks.addTask(2, new EntityAIOwnerHurtTarget(this));
			this.targetTasks.addTask(3, new EntityAIHurtByTarget(this, true));
			this.targetTasks.addTask(4, new EntityAITargetNonTamed(this, typeof(EntitySheep), 16.0F, 200, false));
		}

		public override bool AIEnabled
		{
			get
			{
				return true;
			}
		}

		public override EntityLiving AttackTarget
		{
			set
			{
				base.AttackTarget = value;
				if (value is EntityPlayer)
				{
					this.Angry = true;
				}
    
			}
		}

		protected internal override void updateAITick()
		{
			this.dataWatcher.updateObject(18, this.Health);
		}

		public override int MaxHealth
		{
			get
			{
				return this.Tamed ? 20 : 8;
			}
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(18, new int?(this.Health));
		}

		protected internal override bool canTriggerWalking()
		{
			return false;
		}

		public override string Texture
		{
			get
			{
				return this.Tamed ? "/mob/wolf_tame.png" : (this.Angry ? "/mob/wolf_angry.png" : base.Texture);
			}
		}

		public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
		{
			base.writeEntityToNBT(nBTTagCompound1);
			nBTTagCompound1.setBoolean("Angry", this.Angry);
		}

		public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
		{
			base.readEntityFromNBT(nBTTagCompound1);
			this.Angry = nBTTagCompound1.getBoolean("Angry");
		}

		protected internal override bool canDespawn()
		{
			return this.Angry;
		}

		protected internal override string LivingSound
		{
			get
			{
				return this.Angry ? "mob.wolf.growl" : (this.rand.Next(3) == 0 ? (this.Tamed && this.dataWatcher.getWatchableObjectInt(18) < 10 ? "mob.wolf.whine" : "mob.wolf.panting") : "mob.wolf.bark");
			}
		}

		protected internal override string HurtSound
		{
			get
			{
				return "mob.wolf.hurt";
			}
		}

		protected internal override string DeathSound
		{
			get
			{
				return "mob.wolf.death";
			}
		}

		protected internal override float SoundVolume
		{
			get
			{
				return 0.4F;
			}
		}

		protected internal override int DropItemId
		{
			get
			{
				return -1;
			}
		}

		public override void onLivingUpdate()
		{
			base.onLivingUpdate();
			if (!this.worldObj.isRemote && this.isShaking && !this.field_25052_g && !this.hasPath() && this.onGround)
			{
				this.field_25052_g = true;
				this.timeWolfIsShaking = 0.0F;
				this.prevTimeWolfIsShaking = 0.0F;
				this.worldObj.setEntityState(this, (sbyte)8);
			}

		}

		public override void onUpdate()
		{
			base.onUpdate();
			this.field_25054_c = this.field_25048_b;
			if (this.looksWithInterest)
			{
				this.field_25048_b += (1.0F - this.field_25048_b) * 0.4F;
			}
			else
			{
				this.field_25048_b += (0.0F - this.field_25048_b) * 0.4F;
			}

			if (this.looksWithInterest)
			{
				this.numTicksToChaseTarget = 10;
			}

			if (this.Wet)
			{
				this.isShaking = true;
				this.field_25052_g = false;
				this.timeWolfIsShaking = 0.0F;
				this.prevTimeWolfIsShaking = 0.0F;
			}
			else if ((this.isShaking || this.field_25052_g) && this.field_25052_g)
			{
				if (this.timeWolfIsShaking == 0.0F)
				{
					this.worldObj.playSoundAtEntity(this, "mob.wolf.shake", this.SoundVolume, (this.rand.nextFloat() - this.rand.nextFloat()) * 0.2F + 1.0F);
				}

				this.prevTimeWolfIsShaking = this.timeWolfIsShaking;
				this.timeWolfIsShaking += 0.05F;
				if (this.prevTimeWolfIsShaking >= 2.0F)
				{
					this.isShaking = false;
					this.field_25052_g = false;
					this.prevTimeWolfIsShaking = 0.0F;
					this.timeWolfIsShaking = 0.0F;
				}

				if (this.timeWolfIsShaking > 0.4F)
				{
					float f1 = (float)this.boundingBox.minY;
					int i2 = (int)(MathHelper.sin((this.timeWolfIsShaking - 0.4F) * (float)Math.PI) * 7.0F);

					for (int i3 = 0; i3 < i2; ++i3)
					{
						float f4 = (this.rand.nextFloat() * 2.0F - 1.0F) * this.width * 0.5F;
						float f5 = (this.rand.nextFloat() * 2.0F - 1.0F) * this.width * 0.5F;
						this.worldObj.spawnParticle("splash", this.posX + (double)f4, (double)(f1 + 0.8F), this.posZ + (double)f5, this.motionX, this.motionY, this.motionZ);
					}
				}
			}

		}

		public virtual bool WolfShaking
		{
			get
			{
				return this.isShaking;
			}
		}

		public virtual float getShadingWhileShaking(float f1)
		{
			return 0.75F + (this.prevTimeWolfIsShaking + (this.timeWolfIsShaking - this.prevTimeWolfIsShaking) * f1) / 2.0F * 0.25F;
		}

		public virtual float getShakeAngle(float f1, float f2)
		{
			float f3 = (this.prevTimeWolfIsShaking + (this.timeWolfIsShaking - this.prevTimeWolfIsShaking) * f1 + f2) / 1.8F;
			if (f3 < 0.0F)
			{
				f3 = 0.0F;
			}
			else if (f3 > 1.0F)
			{
				f3 = 1.0F;
			}

			return MathHelper.sin(f3 * (float)Math.PI) * MathHelper.sin(f3 * (float)Math.PI * 11.0F) * 0.15F * (float)Math.PI;
		}

		public virtual float getInterestedAngle(float f1)
		{
			return (this.field_25054_c + (this.field_25048_b - this.field_25054_c) * f1) * 0.15F * (float)Math.PI;
		}

		public override float EyeHeight
		{
			get
			{
				return this.height * 0.8F;
			}
		}

		public override int VerticalFaceSpeed
		{
			get
			{
				return this.Sitting ? 20 : base.VerticalFaceSpeed;
			}
		}

		public override bool attackEntityFrom(DamageSource damageSource1, int i2)
		{
			Entity entity3 = damageSource1.Entity;
			this.aiSit.func_48407_a(false);
			if (entity3 != null && !(entity3 is EntityPlayer) && !(entity3 is EntityArrow))
			{
				i2 = (i2 + 1) / 2;
			}

			return base.attackEntityFrom(damageSource1, i2);
		}

		public override bool attackEntityAsMob(Entity entity1)
		{
			int i2 = this.Tamed ? 4 : 2;
			return entity1.attackEntityFrom(DamageSource.causeMobDamage(this), i2);
		}

		public override bool interact(EntityPlayer entityPlayer1)
		{
			ItemStack itemStack2 = entityPlayer1.inventory.CurrentItem;
			if (!this.Tamed)
			{
				if (itemStack2 != null && itemStack2.itemID == Item.bone.shiftedIndex && !this.Angry)
				{
					if (!entityPlayer1.capabilities.isCreativeMode)
					{
						--itemStack2.stackSize;
					}

					if (itemStack2.stackSize <= 0)
					{
						entityPlayer1.inventory.setInventorySlotContents(entityPlayer1.inventory.currentItem, (ItemStack)null);
					}

					if (!this.worldObj.isRemote)
					{
						if (this.rand.Next(3) == 0)
						{
							this.Tamed = true;
							this.PathToEntity = (PathEntity)null;
							this.AttackTarget = (EntityLiving)null;
							this.aiSit.func_48407_a(true);
							this.EntityHealth = 20;
							this.setOwner(entityPlayer1.username);
							this.func_48142_a(true);
							this.worldObj.setEntityState(this, (sbyte)7);
						}
						else
						{
							this.func_48142_a(false);
							this.worldObj.setEntityState(this, (sbyte)6);
						}
					}

					return true;
				}
			}
			else
			{
				if (itemStack2 != null && Item.itemsList[itemStack2.itemID] is ItemFood)
				{
					ItemFood itemFood3 = (ItemFood)Item.itemsList[itemStack2.itemID];
					if (itemFood3.WolfsFavoriteMeat && this.dataWatcher.getWatchableObjectInt(18) < 20)
					{
						if (!entityPlayer1.capabilities.isCreativeMode)
						{
							--itemStack2.stackSize;
						}

						this.heal(itemFood3.HealAmount);
						if (itemStack2.stackSize <= 0)
						{
							entityPlayer1.inventory.setInventorySlotContents(entityPlayer1.inventory.currentItem, (ItemStack)null);
						}

						return true;
					}
				}

				if (entityPlayer1.username.Equals(this.OwnerName, StringComparison.OrdinalIgnoreCase) && !this.worldObj.isRemote && !this.isWheat(itemStack2))
				{
					this.aiSit.func_48407_a(!this.Sitting);
					this.isJumping = false;
					this.PathToEntity = (PathEntity)null;
				}
			}

			return base.interact(entityPlayer1);
		}

		public override void handleHealthUpdate(sbyte b1)
		{
			if (b1 == 8)
			{
				this.field_25052_g = true;
				this.timeWolfIsShaking = 0.0F;
				this.prevTimeWolfIsShaking = 0.0F;
			}
			else
			{
				base.handleHealthUpdate(b1);
			}

		}

		public virtual float TailRotation
		{
			get
			{
				return this.Angry ? 1.5393804F : (this.Tamed ? (0.55F - (float)(20 - this.dataWatcher.getWatchableObjectInt(18)) * 0.02F) * (float)Math.PI : 0.62831855F);
			}
		}

		public override bool isWheat(ItemStack itemStack1)
		{
			return itemStack1 == null ? false : (!(Item.itemsList[itemStack1.itemID] is ItemFood) ? false : ((ItemFood)Item.itemsList[itemStack1.itemID]).WolfsFavoriteMeat);
		}

		public override int MaxSpawnedInChunk
		{
			get
			{
				return 8;
			}
		}

		public virtual bool Angry
		{
			get
			{
				return (this.dataWatcher.getWatchableObjectByte(16) & 2) != 0;
			}
			set
			{
				sbyte b2 = this.dataWatcher.getWatchableObjectByte(16);
				if (value)
				{
					this.dataWatcher.updateObject(16, (sbyte)(b2 | 2));
				}
				else
				{
					this.dataWatcher.updateObject(16, (sbyte)(b2 & -3));
				}
    
			}
		}


		public override EntityAnimal spawnBabyAnimal(EntityAnimal entityAnimal1)
		{
			EntityWolf entityWolf2 = new EntityWolf(this.worldObj);
			entityWolf2.setOwner(this.OwnerName);
			entityWolf2.Tamed = true;
			return entityWolf2;
		}

		public virtual void func_48150_h(bool z1)
		{
			this.looksWithInterest = z1;
		}

		public override bool canMateWith(EntityAnimal entityAnimal1)
		{
			if (entityAnimal1 == this)
			{
				return false;
			}
			else if (!this.Tamed)
			{
				return false;
			}
			else if (!(entityAnimal1 is EntityWolf))
			{
				return false;
			}
			else
			{
				EntityWolf entityWolf2 = (EntityWolf)entityAnimal1;
				return !entityWolf2.Tamed ? false : (entityWolf2.Sitting ? false : this.InLove && entityWolf2.InLove);
			}
		}
	}

}