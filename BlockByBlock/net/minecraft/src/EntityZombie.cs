namespace net.minecraft.src
{
	public class EntityZombie : EntityMob
	{
		public EntityZombie(World world1) : base(world1)
		{
			this.texture = "/mob/zombie.png";
			this.moveSpeed = 0.23F;
			this.attackStrength = 4;
			this.Navigator.BreakDoors = true;
			this.tasks.addTask(0, new EntityAISwimming(this));
			this.tasks.addTask(1, new EntityAIBreakDoor(this));
			this.tasks.addTask(2, new EntityAIAttackOnCollide(this, typeof(EntityPlayer), this.moveSpeed, false));
			this.tasks.addTask(3, new EntityAIAttackOnCollide(this, typeof(EntityVillager), this.moveSpeed, true));
			this.tasks.addTask(4, new EntityAIMoveTwardsRestriction(this, this.moveSpeed));
			this.tasks.addTask(5, new EntityAIMoveThroughVillage(this, this.moveSpeed, false));
			this.tasks.addTask(6, new EntityAIWander(this, this.moveSpeed));
			this.tasks.addTask(7, new EntityAIWatchClosest(this, typeof(EntityPlayer), 8.0F));
			this.tasks.addTask(7, new EntityAILookIdle(this));
			this.targetTasks.addTask(1, new EntityAIHurtByTarget(this, false));
			this.targetTasks.addTask(2, new EntityAINearestAttackableTarget(this, typeof(EntityPlayer), 16.0F, 0, true));
			this.targetTasks.addTask(2, new EntityAINearestAttackableTarget(this, typeof(EntityVillager), 16.0F, 0, false));
		}

		public override int MaxHealth
		{
			get
			{
				return 20;
			}
		}

		public override int TotalArmorValue
		{
			get
			{
				return 2;
			}
		}

		protected internal override bool AIEnabled
		{
			get
			{
				return true;
			}
		}

		public override void onLivingUpdate()
		{
			if (this.worldObj.Daytime && !this.worldObj.isRemote)
			{
				float f1 = this.getBrightness(1.0F);
				if (f1 > 0.5F && this.worldObj.canBlockSeeTheSky(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY), MathHelper.floor_double(this.posZ)) && this.rand.nextFloat() * 30.0F < (f1 - 0.4F) * 2.0F)
				{
					this.Fire = 8;
				}
			}

			base.onLivingUpdate();
		}

		protected internal override string LivingSound
		{
			get
			{
				return "mob.zombie";
			}
		}

		protected internal override string HurtSound
		{
			get
			{
				return "mob.zombiehurt";
			}
		}

		protected internal override string DeathSound
		{
			get
			{
				return "mob.zombiedeath";
			}
		}

		protected internal override int DropItemId
		{
			get
			{
				return Item.rottenFlesh.shiftedIndex;
			}
		}

		public override EnumCreatureAttribute CreatureAttribute
		{
			get
			{
				return EnumCreatureAttribute.UNDEAD;
			}
		}

		protected internal override void dropRareDrop(int i1)
		{
			switch (this.rand.Next(4))
			{
			case 0:
				this.dropItem(Item.swordSteel.shiftedIndex, 1);
				break;
			case 1:
				this.dropItem(Item.helmetSteel.shiftedIndex, 1);
				break;
			case 2:
				this.dropItem(Item.ingotIron.shiftedIndex, 1);
				break;
			case 3:
				this.dropItem(Item.shovelSteel.shiftedIndex, 1);
			break;
			}

		}
	}

}