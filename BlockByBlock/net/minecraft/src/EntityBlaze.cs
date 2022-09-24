using System;

namespace net.minecraft.src
{
	public class EntityBlaze : EntityMob
	{
		private float heightOffset = 0.5F;
		private int heightOffsetUpdateTime;
		private int field_40152_d;

		public EntityBlaze(World world1) : base(world1)
		{
			this.texture = "/mob/fire.png";
			this.isImmuneToFire = true;
			this.attackStrength = 6;
			this.experienceValue = 10;
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
			this.dataWatcher.addObject(16, new sbyte?((sbyte)0));
		}

		protected internal override string LivingSound
		{
			get
			{
				return "mob.blaze.breathe";
			}
		}

		protected internal override string HurtSound
		{
			get
			{
				return "mob.blaze.hit";
			}
		}

		protected internal override string DeathSound
		{
			get
			{
				return "mob.blaze.death";
			}
		}

		public override bool attackEntityFrom(DamageSource damageSource1, int i2)
		{
			return base.attackEntityFrom(damageSource1, i2);
		}

		public override void onDeath(DamageSource damageSource1)
		{
			base.onDeath(damageSource1);
		}

		public override int getBrightnessForRender(float f1)
		{
			return 15728880;
		}

		public override float getBrightness(float f1)
		{
			return 1.0F;
		}

		public override void onLivingUpdate()
		{
			if (!this.worldObj.isRemote)
			{
				if (this.Wet)
				{
					this.attackEntityFrom(DamageSource.drown, 1);
				}

				--this.heightOffsetUpdateTime;
				if (this.heightOffsetUpdateTime <= 0)
				{
					this.heightOffsetUpdateTime = 100;
					this.heightOffset = 0.5F + (float)this.rand.nextGaussian() * 3.0F;
				}

				if (this.EntityToAttack != null && this.EntityToAttack.posY + (double)this.EntityToAttack.EyeHeight > this.posY + (double)this.EyeHeight + (double)this.heightOffset)
				{
					this.motionY += ((double)0.3F - this.motionY) * (double)0.3F;
				}
			}

			if (this.rand.Next(24) == 0)
			{
				this.worldObj.playSoundEffect(this.posX + 0.5D, this.posY + 0.5D, this.posZ + 0.5D, "fire.fire", 1.0F + this.rand.nextFloat(), this.rand.nextFloat() * 0.7F + 0.3F);
			}

			if (!this.onGround && this.motionY < 0.0D)
			{
				this.motionY *= 0.6D;
			}

			for (int i1 = 0; i1 < 2; ++i1)
			{
				this.worldObj.spawnParticle("largesmoke", this.posX + (this.rand.NextDouble() - 0.5D) * (double)this.width, this.posY + this.rand.NextDouble() * (double)this.height, this.posZ + (this.rand.NextDouble() - 0.5D) * (double)this.width, 0.0D, 0.0D, 0.0D);
			}

			base.onLivingUpdate();
		}

		protected internal override void attackEntity(Entity entity1, float f2)
		{
			if (this.attackTime <= 0 && f2 < 2.0F && entity1.boundingBox.maxY > this.boundingBox.minY && entity1.boundingBox.minY < this.boundingBox.maxY)
			{
				this.attackTime = 20;
				this.attackEntityAsMob(entity1);
			}
			else if (f2 < 30.0F)
			{
				double d3 = entity1.posX - this.posX;
				double d5 = entity1.boundingBox.minY + (double)(entity1.height / 2.0F) - (this.posY + (double)(this.height / 2.0F));
				double d7 = entity1.posZ - this.posZ;
				if (this.attackTime == 0)
				{
					++this.field_40152_d;
					if (this.field_40152_d == 1)
					{
						this.attackTime = 60;
						this.func_40150_a(true);
					}
					else if (this.field_40152_d <= 4)
					{
						this.attackTime = 6;
					}
					else
					{
						this.attackTime = 100;
						this.field_40152_d = 0;
						this.func_40150_a(false);
					}

					if (this.field_40152_d > 1)
					{
						float f9 = MathHelper.sqrt_float(f2) * 0.5F;
						this.worldObj.playAuxSFXAtEntity((EntityPlayer)null, 1009, (int)this.posX, (int)this.posY, (int)this.posZ, 0);

						for (int i10 = 0; i10 < 1; ++i10)
						{
							EntitySmallFireball entitySmallFireball11 = new EntitySmallFireball(this.worldObj, this, d3 + this.rand.nextGaussian() * (double)f9, d5, d7 + this.rand.nextGaussian() * (double)f9);
							entitySmallFireball11.posY = this.posY + (double)(this.height / 2.0F) + 0.5D;
							this.worldObj.spawnEntityInWorld(entitySmallFireball11);
						}
					}
				}

				this.rotationYaw = (float)(Math.Atan2(d7, d3) * 180.0D / (double)(float)Math.PI) - 90.0F;
				this.hasAttacked = true;
			}

		}

		protected internal override void fall(float f1)
		{
		}

		public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
		{
			base.writeEntityToNBT(nBTTagCompound1);
		}

		public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
		{
			base.readEntityFromNBT(nBTTagCompound1);
		}

		protected internal override int DropItemId
		{
			get
			{
				return Item.blazeRod.shiftedIndex;
			}
		}

		public override bool Burning
		{
			get
			{
				return this.func_40151_ac();
			}
		}

		protected internal override void dropFewItems(bool z1, int i2)
		{
			if (z1)
			{
				int i3 = this.rand.Next(2 + i2);

				for (int i4 = 0; i4 < i3; ++i4)
				{
					this.dropItem(Item.blazeRod.shiftedIndex, 1);
				}
			}

		}

		public virtual bool func_40151_ac()
		{
			return (this.dataWatcher.getWatchableObjectByte(16) & 1) != 0;
		}

		public virtual void func_40150_a(bool z1)
		{
			sbyte b2 = this.dataWatcher.getWatchableObjectByte(16);
			if (z1)
			{
				b2 = (sbyte)(b2 | 1);
			}
			else
			{
				b2 &= -2;
			}

			this.dataWatcher.updateObject(16, b2);
		}

		protected internal override bool ValidLightLevel
		{
			get
			{
				return true;
			}
		}
	}

}