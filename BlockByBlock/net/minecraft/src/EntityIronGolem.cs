using System;

namespace net.minecraft.src
{
	public class EntityIronGolem : EntityGolem
	{
		private int field_48119_b = 0;
		internal Village villageObj = null;
		private int field_48120_c;
		private int field_48118_d;

		public EntityIronGolem(World world1) : base(world1)
		{
			this.texture = "/mob/villager_golem.png";
			this.setSize(1.4F, 2.9F);
			this.Navigator.func_48664_a(true);
			this.tasks.addTask(1, new EntityAIAttackOnCollide(this, 0.25F, true));
			this.tasks.addTask(2, new EntityAIMoveTowardsTarget(this, 0.22F, 32.0F));
			this.tasks.addTask(3, new EntityAIMoveThroughVillage(this, 0.16F, true));
			this.tasks.addTask(4, new EntityAIMoveTwardsRestriction(this, 0.16F));
			this.tasks.addTask(5, new EntityAILookAtVillager(this));
			this.tasks.addTask(6, new EntityAIWander(this, 0.16F));
			this.tasks.addTask(7, new EntityAIWatchClosest(this, typeof(EntityPlayer), 6.0F));
			this.tasks.addTask(8, new EntityAILookIdle(this));
			this.targetTasks.addTask(1, new EntityAIDefendVillage(this));
			this.targetTasks.addTask(2, new EntityAIHurtByTarget(this, false));
			this.targetTasks.addTask(3, new EntityAINearestAttackableTarget(this, typeof(EntityMob), 16.0F, 0, false, true));
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(16, (sbyte)0);
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
			if (--this.field_48119_b <= 0)
			{
				this.field_48119_b = 70 + this.rand.Next(50);
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

		public override int MaxHealth
		{
			get
			{
				return 100;
			}
		}

		protected internal override int decreaseAirSupply(int i1)
		{
			return i1;
		}

		public override void onLivingUpdate()
		{
			base.onLivingUpdate();
			if (this.field_48120_c > 0)
			{
				--this.field_48120_c;
			}

			if (this.field_48118_d > 0)
			{
				--this.field_48118_d;
			}

			if (this.motionX * this.motionX + this.motionZ * this.motionZ > 2.500000277905201E-7D && this.rand.Next(5) == 0)
			{
				int i1 = MathHelper.floor_double(this.posX);
				int i2 = MathHelper.floor_double(this.posY - (double)0.2F - (double)this.yOffset);
				int i3 = MathHelper.floor_double(this.posZ);
				int i4 = this.worldObj.getBlockId(i1, i2, i3);
				if (i4 > 0)
				{
					this.worldObj.spawnParticle("tilecrack_" + i4, this.posX + ((double)this.rand.NextSingle() - 0.5D) * (double)this.width, this.boundingBox.minY + 0.1D, this.posZ + ((double)this.rand.NextSingle() - 0.5D) * (double)this.width, 4.0D * ((double)this.rand.NextSingle() - 0.5D), 0.5D, ((double)this.rand.NextSingle() - 0.5D) * 4.0D);
				}
			}

		}

		public override bool func_48100_a(Type class1)
		{
			return this.func_48112_E_() && class1.IsAssignableFrom(typeof(EntityPlayer)) ? false : base.func_48100_a(class1);
		}

		public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
		{
			base.writeEntityToNBT(nBTTagCompound1);
			nBTTagCompound1.setBoolean("PlayerCreated", this.func_48112_E_());
		}

		public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
		{
			base.readEntityFromNBT(nBTTagCompound1);
			this.func_48115_b(nBTTagCompound1.getBoolean("PlayerCreated"));
		}

		public override bool attackEntityAsMob(Entity entity1)
		{
			this.field_48120_c = 10;
			this.worldObj.setEntityState(this, (sbyte)4);
			bool z2 = entity1.attackEntityFrom(DamageSource.causeMobDamage(this), 7 + this.rand.Next(15));
			if (z2)
			{
				entity1.motionY += (double)0.4F;
			}

			this.worldObj.playSoundAtEntity(this, "mob.irongolem.throw", 1.0F, 1.0F);
			return z2;
		}

		public override void handleHealthUpdate(sbyte b1)
		{
			if (b1 == 4)
			{
				this.field_48120_c = 10;
				this.worldObj.playSoundAtEntity(this, "mob.irongolem.throw", 1.0F, 1.0F);
			}
			else if (b1 == 11)
			{
				this.field_48118_d = 400;
			}
			else
			{
				base.handleHealthUpdate(b1);
			}

		}

		public virtual Village Village
		{
			get
			{
				return this.villageObj;
			}
		}

		public virtual int func_48114_ab()
		{
			return this.field_48120_c;
		}

		public virtual void func_48116_a(bool z1)
		{
			this.field_48118_d = z1 ? 400 : 0;
			this.worldObj.setEntityState(this, (sbyte)11);
		}

		protected internal override string LivingSound
		{
			get
			{
				return "none";
			}
		}

		protected internal override string HurtSound
		{
			get
			{
				return "mob.irongolem.hit";
			}
		}

		protected internal override string DeathSound
		{
			get
			{
				return "mob.irongolem.death";
			}
		}

		protected internal override void playStepSound(int i1, int i2, int i3, int i4)
		{
			this.worldObj.playSoundAtEntity(this, "mob.irongolem.walk", 1.0F, 1.0F);
		}

		protected internal override void dropFewItems(bool z1, int i2)
		{
			int i3 = this.rand.Next(3);

			int i4;
			for (i4 = 0; i4 < i3; ++i4)
			{
				this.dropItem(Block.plantRed.blockID, 1);
			}

			i4 = 3 + this.rand.Next(3);

			for (int i5 = 0; i5 < i4; ++i5)
			{
				this.dropItem(Item.ingotIron.shiftedIndex, 1);
			}

		}

		public virtual int func_48117_D_()
		{
			return this.field_48118_d;
		}

		public virtual bool func_48112_E_()
		{
			return (this.dataWatcher.getWatchableObjectByte(16) & 1) != 0;
		}

		public virtual void func_48115_b(bool z1)
		{
			sbyte b2 = this.dataWatcher.getWatchableObjectByte(16);
			if (z1)
			{
				this.dataWatcher.updateObject(16, (sbyte)(b2 | 1));
			}
			else
			{
				this.dataWatcher.updateObject(16, (sbyte)(b2 & -2));
			}

		}
	}

}