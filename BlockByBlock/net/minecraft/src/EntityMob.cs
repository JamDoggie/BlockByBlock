namespace net.minecraft.src
{
	public abstract class EntityMob : EntityCreature, IMob
	{
		protected internal int attackStrength = 2;

		public EntityMob(World world1) : base(world1)
		{
			this.experienceValue = 5;
		}

		public override void onLivingUpdate()
		{
			float f1 = this.getBrightness(1.0F);
			if (f1 > 0.5F)
			{
				this.entityAge += 2;
			}

			base.onLivingUpdate();
		}

		public override void onUpdate()
		{
			base.onUpdate();
			if (!this.worldObj.isRemote && this.worldObj.difficultySetting == 0)
			{
				this.setDead();
			}

		}

		protected internal override Entity findPlayerToAttack()
		{
			EntityPlayer entityPlayer1 = this.worldObj.getClosestVulnerablePlayerToEntity(this, 16.0D);
			return entityPlayer1 != null && this.canEntityBeSeen(entityPlayer1) ? entityPlayer1 : null;
		}

		public override bool attackEntityFrom(DamageSource damageSource1, int i2)
		{
			if (base.attackEntityFrom(damageSource1, i2))
			{
				Entity entity3 = damageSource1.Entity;
				if (this.riddenByEntity != entity3 && this.ridingEntity != entity3)
				{
					if (entity3 != this)
					{
						this.entityToAttack = entity3;
					}

					return true;
				}
				else
				{
					return true;
				}
			}
			else
			{
				return false;
			}
		}

		public override bool attackEntityAsMob(Entity entity1)
		{
			int i2 = this.attackStrength;
			if (this.isPotionActive(Potion.damageBoost))
			{
				i2 += 3 << this.getActivePotionEffect(Potion.damageBoost).Amplifier;
			}

			if (this.isPotionActive(Potion.weakness))
			{
				i2 -= 2 << this.getActivePotionEffect(Potion.weakness).Amplifier;
			}

			return entity1.attackEntityFrom(DamageSource.causeMobDamage(this), i2);
		}

		protected internal override void attackEntity(Entity entity1, float f2)
		{
			if (this.attackTime <= 0 && f2 < 2.0F && entity1.boundingBox.maxY > this.boundingBox.minY && entity1.boundingBox.minY < this.boundingBox.maxY)
			{
				this.attackTime = 20;
				this.attackEntityAsMob(entity1);
			}

		}

		public override float getBlockPathWeight(int i1, int i2, int i3)
		{
			return 0.5F - this.worldObj.getLightBrightness(i1, i2, i3);
		}

		public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
		{
			base.writeEntityToNBT(nBTTagCompound1);
		}

		public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
		{
			base.readEntityFromNBT(nBTTagCompound1);
		}

		protected internal virtual bool ValidLightLevel
		{
			get
			{
				int i1 = MathHelper.floor_double(this.posX);
				int i2 = MathHelper.floor_double(this.boundingBox.minY);
				int i3 = MathHelper.floor_double(this.posZ);
				if (this.worldObj.getSavedLightValue(EnumSkyBlock.Sky, i1, i2, i3) > this.rand.Next(32))
				{
					return false;
				}
				else
				{
					int i4 = this.worldObj.getBlockLightValue(i1, i2, i3);
					if (this.worldObj.Thundering)
					{
						int i5 = this.worldObj.skylightSubtracted;
						this.worldObj.skylightSubtracted = 10;
						i4 = this.worldObj.getBlockLightValue(i1, i2, i3);
						this.worldObj.skylightSubtracted = i5;
					}
    
					return i4 <= this.rand.Next(8);
				}
			}
		}

		public override bool CanSpawnHere
		{
			get
			{
				return this.ValidLightLevel && base.CanSpawnHere;
			}
		}
	}

}