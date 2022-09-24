using System;

namespace net.minecraft.src
{
	public class EntitySquid : EntityWaterMob
	{
		public float field_21089_a = 0.0F;
		public float field_21088_b = 0.0F;
		public float field_21087_c = 0.0F;
		public float field_21086_f = 0.0F;
		public float field_21085_g = 0.0F;
		public float field_21084_h = 0.0F;
		public float tentacleAngle = 0.0F;
		public float lastTentacleAngle = 0.0F;
		private float randomMotionSpeed = 0.0F;
		private float field_21080_l = 0.0F;
		private float field_21079_m = 0.0F;
		private float randomMotionVecX = 0.0F;
		private float randomMotionVecY = 0.0F;
		private float randomMotionVecZ = 0.0F;

		public EntitySquid(World world1) : base(world1)
		{
			this.texture = "/mob/squid.png";
			this.setSize(0.95F, 0.95F);
			this.field_21080_l = 1.0F / (this.rand.nextFloat() + 1.0F) * 0.2F;
		}

		public override int MaxHealth
		{
			get
			{
				return 10;
			}
		}

		public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
		{
			base.writeEntityToNBT(nBTTagCompound1);
		}

		public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
		{
			base.readEntityFromNBT(nBTTagCompound1);
		}

		protected internal override string LivingSound
		{
			get
			{
				return null;
			}
		}

		protected internal override string HurtSound
		{
			get
			{
				return null;
			}
		}

		protected internal override string DeathSound
		{
			get
			{
				return null;
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
				return 0;
			}
		}

		protected internal override void dropFewItems(bool z1, int i2)
		{
			int i3 = this.rand.Next(3 + i2) + 1;

			for (int i4 = 0; i4 < i3; ++i4)
			{
				this.entityDropItem(new ItemStack(Item.dyePowder, 1, 0), 0.0F);
			}

		}

		public override bool interact(EntityPlayer entityPlayer1)
		{
			return base.interact(entityPlayer1);
		}

		public override bool InWater
		{
			get
			{
				return this.worldObj.handleMaterialAcceleration(this.boundingBox.expand(0.0D, -0.6000000238418579D, 0.0D), Material.water, this);
			}
		}

		public override void onLivingUpdate()
		{
			base.onLivingUpdate();
			this.field_21088_b = this.field_21089_a;
			this.field_21086_f = this.field_21087_c;
			this.field_21084_h = this.field_21085_g;
			this.lastTentacleAngle = this.tentacleAngle;
			this.field_21085_g += this.field_21080_l;
			if (this.field_21085_g > 6.2831855F)
			{
				this.field_21085_g -= 6.2831855F;
				if (this.rand.Next(10) == 0)
				{
					this.field_21080_l = 1.0F / (this.rand.nextFloat() + 1.0F) * 0.2F;
				}
			}

			if (this.InWater)
			{
				float f1;
				if (this.field_21085_g < (float)Math.PI)
				{
					f1 = this.field_21085_g / (float)Math.PI;
					this.tentacleAngle = MathHelper.sin(f1 * f1 * (float)Math.PI) * (float)Math.PI * 0.25F;
					if ((double)f1 > 0.75D)
					{
						this.randomMotionSpeed = 1.0F;
						this.field_21079_m = 1.0F;
					}
					else
					{
						this.field_21079_m *= 0.8F;
					}
				}
				else
				{
					this.tentacleAngle = 0.0F;
					this.randomMotionSpeed *= 0.9F;
					this.field_21079_m *= 0.99F;
				}

				if (!this.worldObj.isRemote)
				{
					this.motionX = (double)(this.randomMotionVecX * this.randomMotionSpeed);
					this.motionY = (double)(this.randomMotionVecY * this.randomMotionSpeed);
					this.motionZ = (double)(this.randomMotionVecZ * this.randomMotionSpeed);
				}

				f1 = MathHelper.sqrt_double(this.motionX * this.motionX + this.motionZ * this.motionZ);
				this.renderYawOffset += (-((float)Math.Atan2(this.motionX, this.motionZ)) * 180.0F / (float)Math.PI - this.renderYawOffset) * 0.1F;
				this.rotationYaw = this.renderYawOffset;
				this.field_21087_c += (float)Math.PI * this.field_21079_m * 1.5F;
				this.field_21089_a += (-((float)Math.Atan2((double)f1, this.motionY)) * 180.0F / (float)Math.PI - this.field_21089_a) * 0.1F;
			}
			else
			{
				this.tentacleAngle = MathHelper.abs(MathHelper.sin(this.field_21085_g)) * (float)Math.PI * 0.25F;
				if (!this.worldObj.isRemote)
				{
					this.motionX = 0.0D;
					this.motionY -= 0.08D;
					this.motionY *= (double)0.98F;
					this.motionZ = 0.0D;
				}

				this.field_21089_a = (float)((double)this.field_21089_a + (double)(-90.0F - this.field_21089_a) * 0.02D);
			}

		}

		public override void moveEntityWithHeading(float f1, float f2)
		{
			this.moveEntity(this.motionX, this.motionY, this.motionZ);
		}

		protected internal override void updateEntityActionState()
		{
			++this.entityAge;
			if (this.entityAge > 100)
			{
				this.randomMotionVecX = this.randomMotionVecY = this.randomMotionVecZ = 0.0F;
			}
			else if (this.rand.Next(50) == 0 || !this.inWater || this.randomMotionVecX == 0.0F && this.randomMotionVecY == 0.0F && this.randomMotionVecZ == 0.0F)
			{
				float f1 = this.rand.nextFloat() * (float)Math.PI * 2.0F;
				this.randomMotionVecX = MathHelper.cos(f1) * 0.2F;
				this.randomMotionVecY = -0.1F + this.rand.nextFloat() * 0.2F;
				this.randomMotionVecZ = MathHelper.sin(f1) * 0.2F;
			}

			this.despawnEntity();
		}

		public override bool CanSpawnHere
		{
			get
			{
				return this.posY > 45.0D && this.posY < 63.0D && base.CanSpawnHere;
			}
		}
	}

}