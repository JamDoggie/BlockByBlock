namespace net.minecraft.src
{
	public class EntityCreeper : EntityMob
	{
		internal int timeSinceIgnited;
		internal int lastActiveTime;

		public EntityCreeper(World world1) : base(world1)
		{
			this.texture = "/mob/creeper.png";
			this.tasks.addTask(1, new EntityAISwimming(this));
			this.tasks.addTask(2, new EntityAICreeperSwell(this));
			this.tasks.addTask(3, new EntityAIAvoidEntity(this, typeof(EntityOcelot), 6.0F, 0.25F, 0.3F));
			this.tasks.addTask(4, new EntityAIAttackOnCollide(this, 0.25F, false));
			this.tasks.addTask(5, new EntityAIWander(this, 0.2F));
			this.tasks.addTask(6, new EntityAIWatchClosest(this, typeof(EntityPlayer), 8.0F));
			this.tasks.addTask(6, new EntityAILookIdle(this));
			this.targetTasks.addTask(1, new EntityAINearestAttackableTarget(this, typeof(EntityPlayer), 16.0F, 0, true));
			this.targetTasks.addTask(2, new EntityAIHurtByTarget(this, false));
		}

		public override bool AIEnabled
		{
			get
			{
				return true;
			}
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
			this.dataWatcher.addObject(16, (sbyte)-1);
			this.dataWatcher.addObject(17, (sbyte)0);
		}

		public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
		{
			base.writeEntityToNBT(nBTTagCompound1);
			if (this.dataWatcher.getWatchableObjectByte(17) == 1)
			{
				nBTTagCompound1.setBoolean("powered", true);
			}

		}

		public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
		{
			base.readEntityFromNBT(nBTTagCompound1);
			this.dataWatcher.updateObject(17, (sbyte)(nBTTagCompound1.getBoolean("powered") ? 1 : 0));
		}

		public override void onUpdate()
		{
			if (this.EntityAlive)
			{
				this.lastActiveTime = this.timeSinceIgnited;
				int i1 = this.CreeperState;
				if (i1 > 0 && this.timeSinceIgnited == 0)
				{
					this.worldObj.playSoundAtEntity(this, "random.fuse", 1.0F, 0.5F);
				}

				this.timeSinceIgnited += i1;
				if (this.timeSinceIgnited < 0)
				{
					this.timeSinceIgnited = 0;
				}

				if (this.timeSinceIgnited >= 30)
				{
					this.timeSinceIgnited = 30;
					if (!this.worldObj.isRemote)
					{
						if (this.Powered)
						{
							this.worldObj.createExplosion(this, this.posX, this.posY, this.posZ, 6.0F);
						}
						else
						{
							this.worldObj.createExplosion(this, this.posX, this.posY, this.posZ, 3.0F);
						}

						this.setDead();
					}
				}
			}

			base.onUpdate();
		}

		protected internal override string HurtSound
		{
			get
			{
				return "mob.creeper";
			}
		}

		protected internal override string DeathSound
		{
			get
			{
				return "mob.creeperdeath";
			}
		}

		public override void onDeath(DamageSource damageSource1)
		{
			base.onDeath(damageSource1);
			if (damageSource1.Entity is EntitySkeleton)
			{
				this.dropItem(Item.record13.shiftedIndex + this.rand.Next(10), 1);
			}

		}

		public override bool attackEntityAsMob(Entity entity1)
		{
			return true;
		}

		public virtual bool Powered
		{
			get
			{
				return this.dataWatcher.getWatchableObjectByte(17) == 1;
			}
		}

		public virtual float setCreeperFlashTime(float f1)
		{
			return ((float)this.lastActiveTime + (float)(this.timeSinceIgnited - this.lastActiveTime) * f1) / 28.0F;
		}

		protected internal override int DropItemId
		{
			get
			{
				return Item.gunpowder.shiftedIndex;
			}
		}

		public virtual int CreeperState
		{
			get
			{
				return this.dataWatcher.getWatchableObjectByte(16);
			}
			set
			{
				this.dataWatcher.updateObject(16, (sbyte)value);
			}
		}


		public override void onStruckByLightning(EntityLightningBolt entityLightningBolt1)
		{
			base.onStruckByLightning(entityLightningBolt1);
			this.dataWatcher.updateObject(17, (sbyte)1);
		}
	}

}