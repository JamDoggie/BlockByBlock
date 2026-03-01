namespace net.minecraft.src
{
	public class EntityPig : EntityAnimal
	{
		public EntityPig(World world1) : base(world1)
		{
			this.texture = "/mob/pig.png";
			this.setSize(0.9F, 0.9F);
			this.Navigator.func_48664_a(true);
			float f2 = 0.25F;
			this.tasks.addTask(0, new EntityAISwimming(this));
			this.tasks.addTask(1, new EntityAIPanic(this, 0.38F));
			this.tasks.addTask(2, new EntityAIMate(this, f2));
			this.tasks.addTask(3, new EntityAITempt(this, 0.25F, Item.wheat.shiftedIndex, false));
			this.tasks.addTask(4, new EntityAIFollowParent(this, 0.28F));
			this.tasks.addTask(5, new EntityAIWander(this, f2));
			this.tasks.addTask(6, new EntityAIWatchClosest(this, typeof(EntityPlayer), 6.0F));
			this.tasks.addTask(7, new EntityAILookIdle(this));
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
				return 10;
			}
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(16, (sbyte)0);
		}

		public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
		{
			base.writeEntityToNBT(nBTTagCompound1);
			nBTTagCompound1.setBoolean("Saddle", this.Saddled);
		}

		public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
		{
			base.readEntityFromNBT(nBTTagCompound1);
			this.Saddled = nBTTagCompound1.getBoolean("Saddle");
		}

		protected internal override string LivingSound
		{
			get
			{
				return "mob.pig";
			}
		}

		protected internal override string HurtSound
		{
			get
			{
				return "mob.pig";
			}
		}

		protected internal override string DeathSound
		{
			get
			{
				return "mob.pigdeath";
			}
		}

		public override bool interact(EntityPlayer entityPlayer1)
		{
			if (base.interact(entityPlayer1))
			{
				return true;
			}
			else if (!this.Saddled || this.worldObj.isRemote || this.riddenByEntity != null && this.riddenByEntity != entityPlayer1)
			{
				return false;
			}
			else
			{
				entityPlayer1.mountEntity(this);
				return true;
			}
		}

		protected internal override int DropItemId
		{
			get
			{
				return this.Burning ? Item.porkCooked.shiftedIndex : Item.porkRaw.shiftedIndex;
			}
		}

		public virtual bool Saddled
		{
			get
			{
				return (this.dataWatcher.getWatchableObjectByte(16) & 1) != 0;
			}
			set
			{
				if (value)
				{
					this.dataWatcher.updateObject(16, (sbyte)1);
				}
				else
				{
					this.dataWatcher.updateObject(16, (sbyte)0);
				}
    
			}
		}


		public override void onStruckByLightning(EntityLightningBolt entityLightningBolt1)
		{
			if (!this.worldObj.isRemote)
			{
				EntityPigZombie entityPigZombie2 = new EntityPigZombie(this.worldObj);
				entityPigZombie2.setLocationAndAngles(this.posX, this.posY, this.posZ, this.rotationYaw, this.rotationPitch);
				this.worldObj.spawnEntityInWorld(entityPigZombie2);
				this.setDead();
			}
		}

		protected internal override void fall(float f1)
		{
			base.fall(f1);
			if (f1 > 5.0F && this.riddenByEntity is EntityPlayer)
			{
				((EntityPlayer)this.riddenByEntity).triggerAchievement(AchievementList.flyPig);
			}

		}

		public override EntityAnimal spawnBabyAnimal(EntityAnimal entityAnimal1)
		{
			return new EntityPig(this.worldObj);
		}
	}

}