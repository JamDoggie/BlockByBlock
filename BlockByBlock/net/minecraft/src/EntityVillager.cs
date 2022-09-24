namespace net.minecraft.src
{
	public class EntityVillager : EntityAgeable
	{
		private int randomTickDivider;
		private bool isMatingFlag;
		private bool isPlayingFlag;
		internal Village villageObj;

		public EntityVillager(World world1) : this(world1, 0)
		{
		}

		public EntityVillager(World world1, int i2) : base(world1)
		{
			this.randomTickDivider = 0;
			this.isMatingFlag = false;
			this.isPlayingFlag = false;
			this.villageObj = null;
			this.Profession = i2;
			this.texture = "/mob/villager/villager.png";
			this.moveSpeed = 0.5F;
			this.Navigator.BreakDoors = true;
			this.Navigator.func_48664_a(true);
			this.tasks.addTask(0, new EntityAISwimming(this));
			this.tasks.addTask(1, new EntityAIAvoidEntity(this, typeof(EntityZombie), 8.0F, 0.3F, 0.35F));
			this.tasks.addTask(2, new EntityAIMoveIndoors(this));
			this.tasks.addTask(3, new EntityAIRestrictOpenDoor(this));
			this.tasks.addTask(4, new EntityAIOpenDoor(this, true));
			this.tasks.addTask(5, new EntityAIMoveTwardsRestriction(this, 0.3F));
			this.tasks.addTask(6, new EntityAIVillagerMate(this));
			this.tasks.addTask(7, new EntityAIFollowGolem(this));
			this.tasks.addTask(8, new EntityAIPlay(this, 0.32F));
			this.tasks.addTask(9, new EntityAIWatchClosest2(this, typeof(EntityPlayer), 3.0F, 1.0F));
			this.tasks.addTask(9, new EntityAIWatchClosest2(this, typeof(EntityVillager), 5.0F, 0.02F));
			this.tasks.addTask(9, new EntityAIWander(this, 0.3F));
			this.tasks.addTask(10, new EntityAIWatchClosest(this, typeof(EntityLiving), 8.0F));
		}

		public override bool AIEnabled
		{
			get
			{
				return true;
			}
		}

		protected internal override void updateAITick()
		{
			if (--this.randomTickDivider <= 0)
			{
				this.worldObj.villageCollectionObj.addVillagerPosition(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY), MathHelper.floor_double(this.posZ));
				this.randomTickDivider = 70 + this.rand.Next(50);
				this.villageObj = this.worldObj.villageCollectionObj.findNearestVillage(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY), MathHelper.floor_double(this.posZ), 32);
				if (this.villageObj == null)
				{
					this.detachHome();
				}
				else
				{
					ChunkCoordinates chunkCoordinates1 = this.villageObj.Center;
					this.setHomeArea(chunkCoordinates1.posX, chunkCoordinates1.posY, chunkCoordinates1.posZ, this.villageObj.VillageRadius);
				}
			}

			base.updateAITick();
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(16, 0);
		}

		public override int MaxHealth
		{
			get
			{
				return 20;
			}
		}

		public override void onLivingUpdate()
		{
			base.onLivingUpdate();
		}

		public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
		{
			base.writeEntityToNBT(nBTTagCompound1);
			nBTTagCompound1.setInteger("Profession", this.Profession);
		}

		public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
		{
			base.readEntityFromNBT(nBTTagCompound1);
			this.Profession = nBTTagCompound1.getInteger("Profession");
		}

		public override string Texture
		{
			get
			{
				switch (this.Profession)
				{
				case 0:
					return "/mob/villager/farmer.png";
				case 1:
					return "/mob/villager/librarian.png";
				case 2:
					return "/mob/villager/priest.png";
				case 3:
					return "/mob/villager/smith.png";
				case 4:
					return "/mob/villager/butcher.png";
				default:
					return base.Texture;
				}
			}
		}

		protected internal override bool canDespawn()
		{
			return false;
		}

		protected internal override string LivingSound
		{
			get
			{
				return "mob.villager.default";
			}
		}

		protected internal override string HurtSound
		{
			get
			{
				return "mob.villager.defaulthurt";
			}
		}

		protected internal override string DeathSound
		{
			get
			{
				return "mob.villager.defaultdeath";
			}
		}

		public virtual int Profession
		{
			set
			{
				this.dataWatcher.updateObject(16, value);
			}
			get
			{
				return this.dataWatcher.getWatchableObjectInt(16);
			}
		}


		public virtual bool IsMatingFlag
		{
			get
			{
				return this.isMatingFlag;
			}
			set
			{
				this.isMatingFlag = value;
			}
		}


		public virtual bool IsPlayingFlag
		{
			set
			{
				this.isPlayingFlag = value;
			}
			get
			{
				return this.isPlayingFlag;
			}
		}


		public override EntityLiving RevengeTarget
		{
			set
			{
				base.RevengeTarget = value;
				if (this.villageObj != null && value != null)
				{
					this.villageObj.addOrRenewAgressor(value);
				}
    
			}
		}
	}

}