namespace net.minecraft.src
{
	public class EntitySkeleton : EntityMob
	{
		private static readonly ItemStack defaultHeldItem = new ItemStack(Item.bow, 1);

		public EntitySkeleton(World world1) : base(world1)
		{
			this.texture = "/mob/skeleton.png";
			this.moveSpeed = 0.25F;
			this.tasks.addTask(1, new EntityAISwimming(this));
			this.tasks.addTask(2, new EntityAIRestrictSun(this));
			this.tasks.addTask(3, new EntityAIFleeSun(this, this.moveSpeed));
			this.tasks.addTask(4, new EntityAIArrowAttack(this, this.moveSpeed, 1, 60));
			this.tasks.addTask(5, new EntityAIWander(this, this.moveSpeed));
			this.tasks.addTask(6, new EntityAIWatchClosest(this, typeof(EntityPlayer), 8.0F));
			this.tasks.addTask(6, new EntityAILookIdle(this));
			this.targetTasks.addTask(1, new EntityAIHurtByTarget(this, false));
			this.targetTasks.addTask(2, new EntityAINearestAttackableTarget(this, typeof(EntityPlayer), 16.0F, 0, true));
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

		protected internal override string LivingSound
		{
			get
			{
				return "mob.skeleton";
			}
		}

		protected internal override string HurtSound
		{
			get
			{
				return "mob.skeletonhurt";
			}
		}

		protected internal override string DeathSound
		{
			get
			{
				return "mob.skeletonhurt";
			}
		}

		public override ItemStack HeldItem
		{
			get
			{
				return defaultHeldItem;
			}
		}

		public override EnumCreatureAttribute CreatureAttribute
		{
			get
			{
				return EnumCreatureAttribute.UNDEAD;
			}
		}

		public override void onLivingUpdate()
		{
			if (this.worldObj.Daytime && !this.worldObj.isRemote)
			{
				float f1 = this.getBrightness(1.0F);
				if (f1 > 0.5F && this.worldObj.canBlockSeeTheSky(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY), MathHelper.floor_double(this.posZ)) && this.rand.NextSingle() * 30.0F < (f1 - 0.4F) * 2.0F)
				{
					this.Fire = 8;
				}
			}

			base.onLivingUpdate();
		}

		public override void onDeath(DamageSource damageSource1)
		{
			base.onDeath(damageSource1);
			if (damageSource1.SourceOfDamage is EntityArrow && damageSource1.Entity is EntityPlayer)
			{
				EntityPlayer entityPlayer2 = (EntityPlayer)damageSource1.Entity;
				double d3 = entityPlayer2.posX - this.posX;
				double d5 = entityPlayer2.posZ - this.posZ;
				if (d3 * d3 + d5 * d5 >= 2500.0D)
				{
					entityPlayer2.triggerAchievement(AchievementList.snipeSkeleton);
				}
			}

		}

		protected internal override int DropItemId
		{
			get
			{
				return Item.arrow.shiftedIndex;
			}
		}

		protected internal override void dropFewItems(bool z1, int i2)
		{
			int i3 = this.rand.Next(3 + i2);

			int i4;
			for (i4 = 0; i4 < i3; ++i4)
			{
				this.dropItem(Item.arrow.shiftedIndex, 1);
			}

			i3 = this.rand.Next(3 + i2);

			for (i4 = 0; i4 < i3; ++i4)
			{
				this.dropItem(Item.bone.shiftedIndex, 1);
			}

		}

		protected internal override void dropRareDrop(int i1)
		{
			if (i1 > 0)
			{
				ItemStack itemStack2 = new ItemStack(Item.bow);
				EnchantmentHelper.func_48441_a(this.rand, itemStack2, 5);
				this.entityDropItem(itemStack2, 0.0F);
			}
			else
			{
				this.dropItem(Item.bow.shiftedIndex, 1);
			}

		}
	}

}