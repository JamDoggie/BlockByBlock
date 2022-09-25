using System;

namespace net.minecraft.src
{

	public class EntityBoat : Entity
	{
		private int boatPosRotationIncrements;
		private double boatX;
		private double boatY;
		private double boatZ;
		private double boatYaw;
		private double boatPitch;
		private double velocityX;
		private double velocityY;
		private double velocityZ;

		public EntityBoat(World world1) : base(world1)
		{
			this.preventEntitySpawning = true;
			this.setSize(1.5F, 0.6F);
			this.yOffset = this.height / 2.0F;
		}

		protected internal override bool canTriggerWalking()
		{
			return false;
		}

		protected internal override void entityInit()
		{
			this.dataWatcher.addObject(17, new int?(0));
			this.dataWatcher.addObject(18, new int?(1));
			this.dataWatcher.addObject(19, new int?(0));
		}

		public override AxisAlignedBB getCollisionBox(Entity entity1)
		{
			return entity1.boundingBox;
		}

		public override AxisAlignedBB BoundingBox
		{
			get
			{
				return this.boundingBox;
			}
		}

		public override bool canBePushed()
		{
			return true;
		}

		public EntityBoat(World world1, double d2, double d4, double d6) : this(world1)
		{
			this.setPosition(d2, d4 + (double)this.yOffset, d6);
			this.motionX = 0.0D;
			this.motionY = 0.0D;
			this.motionZ = 0.0D;
			this.prevPosX = d2;
			this.prevPosY = d4;
			this.prevPosZ = d6;
		}

		public override double MountedYOffset
		{
			get
			{
				return (double)this.height * 0.0D - (double)0.3F;
			}
		}

		public override bool attackEntityFrom(DamageSource damageSource1, int i2)
		{
			if (!this.worldObj.isRemote && !this.isDead)
			{
				this.ForwardDirection = -this.ForwardDirection;
				this.TimeSinceHit = 10;
				this.DamageTaken = this.DamageTaken + i2 * 10;
				this.setBeenAttacked();
				if (this.DamageTaken > 40)
				{
					if (this.riddenByEntity != null)
					{
						this.riddenByEntity.mountEntity(this);
					}

					int i3;
					for (i3 = 0; i3 < 3; ++i3)
					{
						this.dropItemWithOffset(Block.planks.blockID, 1, 0.0F);
					}

					for (i3 = 0; i3 < 2; ++i3)
					{
						this.dropItemWithOffset(Item.stick.shiftedIndex, 1, 0.0F);
					}

					this.setDead();
				}

				return true;
			}
			else
			{
				return true;
			}
		}

		public override void performHurtAnimation()
		{
			this.ForwardDirection = -this.ForwardDirection;
			this.TimeSinceHit = 10;
			this.DamageTaken = this.DamageTaken * 11;
		}

		public override bool canBeCollidedWith()
		{
			return !this.isDead;
		}

		public override void setPositionAndRotation2(double d1, double d3, double d5, float f7, float f8, int i9)
		{
			this.boatX = d1;
			this.boatY = d3;
			this.boatZ = d5;
			this.boatYaw = (double)f7;
			this.boatPitch = (double)f8;
			this.boatPosRotationIncrements = i9 + 4;
			this.motionX = this.velocityX;
			this.motionY = this.velocityY;
			this.motionZ = this.velocityZ;
		}

		public override void setVelocity(double d1, double d3, double d5)
		{
			this.velocityX = this.motionX = d1;
			this.velocityY = this.motionY = d3;
			this.velocityZ = this.motionZ = d5;
		}

		public override void onUpdate()
		{
			base.onUpdate();
			if (this.TimeSinceHit > 0)
			{
				this.TimeSinceHit = this.TimeSinceHit - 1;
			}

			if (this.DamageTaken > 0)
			{
				this.DamageTaken = this.DamageTaken - 1;
			}

			this.prevPosX = this.posX;
			this.prevPosY = this.posY;
			this.prevPosZ = this.posZ;
			sbyte b1 = 5;
			double d2 = 0.0D;

			for (int i4 = 0; i4 < b1; ++i4)
			{
				double d5 = this.boundingBox.minY + (this.boundingBox.maxY - this.boundingBox.minY) * (double)(i4 + 0) / (double)b1 - 0.125D;
				double d7 = this.boundingBox.minY + (this.boundingBox.maxY - this.boundingBox.minY) * (double)(i4 + 1) / (double)b1 - 0.125D;
				AxisAlignedBB axisAlignedBB9 = AxisAlignedBB.getBoundingBoxFromPool(this.boundingBox.minX, d5, this.boundingBox.minZ, this.boundingBox.maxX, d7, this.boundingBox.maxZ);
				if (this.worldObj.isAABBInMaterial(axisAlignedBB9, Material.water))
				{
					d2 += 1.0D / (double)b1;
				}
			}

			double d21 = Math.Sqrt(this.motionX * this.motionX + this.motionZ * this.motionZ);
			double d6;
			double d8;
			if (d21 > 0.15D)
			{
				d6 = Math.Cos((double)this.rotationYaw * Math.PI / 180.0D);
				d8 = Math.Sin((double)this.rotationYaw * Math.PI / 180.0D);

				for (int i10 = 0; (double)i10 < 1.0D + d21 * 60.0D; ++i10)
				{
					double d11 = (double)(this.rand.NextSingle() * 2.0F - 1.0F);
					double d13 = (double)(this.rand.Next(2) * 2 - 1) * 0.7D;
					double d15;
					double d17;
					if (this.rand.NextBool())
					{
						d15 = this.posX - d6 * d11 * 0.8D + d8 * d13;
						d17 = this.posZ - d8 * d11 * 0.8D - d6 * d13;
						this.worldObj.spawnParticle("splash", d15, this.posY - 0.125D, d17, this.motionX, this.motionY, this.motionZ);
					}
					else
					{
						d15 = this.posX + d6 + d8 * d11 * 0.7D;
						d17 = this.posZ + d8 - d6 * d11 * 0.7D;
						this.worldObj.spawnParticle("splash", d15, this.posY - 0.125D, d17, this.motionX, this.motionY, this.motionZ);
					}
				}
			}

			double d12;
			double d23;
			if (this.worldObj.isRemote)
			{
				if (this.boatPosRotationIncrements > 0)
				{
					d6 = this.posX + (this.boatX - this.posX) / (double)this.boatPosRotationIncrements;
					d8 = this.posY + (this.boatY - this.posY) / (double)this.boatPosRotationIncrements;
					d23 = this.posZ + (this.boatZ - this.posZ) / (double)this.boatPosRotationIncrements;

					for (d12 = this.boatYaw - (double)this.rotationYaw; d12 < -180.0D; d12 += 360.0D)
					{
					}

					while (d12 >= 180.0D)
					{
						d12 -= 360.0D;
					}

					this.rotationYaw = (float)((double)this.rotationYaw + d12 / (double)this.boatPosRotationIncrements);
					this.rotationPitch = (float)((double)this.rotationPitch + (this.boatPitch - (double)this.rotationPitch) / (double)this.boatPosRotationIncrements);
					--this.boatPosRotationIncrements;
					this.setPosition(d6, d8, d23);
					this.setRotation(this.rotationYaw, this.rotationPitch);
				}
				else
				{
					d6 = this.posX + this.motionX;
					d8 = this.posY + this.motionY;
					d23 = this.posZ + this.motionZ;
					this.setPosition(d6, d8, d23);
					if (this.onGround)
					{
						this.motionX *= 0.5D;
						this.motionY *= 0.5D;
						this.motionZ *= 0.5D;
					}

					this.motionX *= (double)0.99F;
					this.motionY *= (double)0.95F;
					this.motionZ *= (double)0.99F;
				}

			}
			else
			{
				if (d2 < 1.0D)
				{
					d6 = d2 * 2.0D - 1.0D;
					this.motionY += (double)0.04F * d6;
				}
				else
				{
					if (this.motionY < 0.0D)
					{
						this.motionY /= 2.0D;
					}

					this.motionY += 0.007000000216066837D;
				}

				if (this.riddenByEntity != null)
				{
					this.motionX += this.riddenByEntity.motionX * 0.2D;
					this.motionZ += this.riddenByEntity.motionZ * 0.2D;
				}

				d6 = 0.4D;
				if (this.motionX < -d6)
				{
					this.motionX = -d6;
				}

				if (this.motionX > d6)
				{
					this.motionX = d6;
				}

				if (this.motionZ < -d6)
				{
					this.motionZ = -d6;
				}

				if (this.motionZ > d6)
				{
					this.motionZ = d6;
				}

				if (this.onGround)
				{
					this.motionX *= 0.5D;
					this.motionY *= 0.5D;
					this.motionZ *= 0.5D;
				}

				this.moveEntity(this.motionX, this.motionY, this.motionZ);
				if (this.isCollidedHorizontally && d21 > 0.2D)
				{
					if (!this.worldObj.isRemote)
					{
						this.setDead();

						int i22;
						for (i22 = 0; i22 < 3; ++i22)
						{
							this.dropItemWithOffset(Block.planks.blockID, 1, 0.0F);
						}

						for (i22 = 0; i22 < 2; ++i22)
						{
							this.dropItemWithOffset(Item.stick.shiftedIndex, 1, 0.0F);
						}
					}
				}
				else
				{
					this.motionX *= (double)0.99F;
					this.motionY *= (double)0.95F;
					this.motionZ *= (double)0.99F;
				}

				this.rotationPitch = 0.0F;
				d8 = (double)this.rotationYaw;
				d23 = this.prevPosX - this.posX;
				d12 = this.prevPosZ - this.posZ;
				if (d23 * d23 + d12 * d12 > 0.001D)
				{
					d8 = (double)((float)(Math.Atan2(d12, d23) * 180.0D / Math.PI));
				}

				double d14;
				for (d14 = d8 - (double)this.rotationYaw; d14 >= 180.0D; d14 -= 360.0D)
				{
				}

				while (d14 < -180.0D)
				{
					d14 += 360.0D;
				}

				if (d14 > 20.0D)
				{
					d14 = 20.0D;
				}

				if (d14 < -20.0D)
				{
					d14 = -20.0D;
				}

				this.rotationYaw = (float)((double)this.rotationYaw + d14);
				this.setRotation(this.rotationYaw, this.rotationPitch);
				System.Collections.IList list16 = this.worldObj.getEntitiesWithinAABBExcludingEntity(this, this.boundingBox.expand((double)0.2F, 0.0D, (double)0.2F));
				int i24;
				if (list16 != null && list16.Count > 0)
				{
					for (i24 = 0; i24 < list16.Count; ++i24)
					{
						Entity entity18 = (Entity)list16[i24];
						if (entity18 != this.riddenByEntity && entity18.canBePushed() && entity18 is EntityBoat)
						{
							entity18.applyEntityCollision(this);
						}
					}
				}

				for (i24 = 0; i24 < 4; ++i24)
				{
					int i25 = MathHelper.floor_double(this.posX + ((double)(i24 % 2) - 0.5D) * 0.8D);
					int i19 = MathHelper.floor_double(this.posY);
					int i20 = MathHelper.floor_double(this.posZ + ((double)(i24 / 2) - 0.5D) * 0.8D);
					if (this.worldObj.getBlockId(i25, i19, i20) == Block.snow.blockID)
					{
						this.worldObj.setBlockWithNotify(i25, i19, i20, 0);
					}
				}

				if (this.riddenByEntity != null && this.riddenByEntity.isDead)
				{
					this.riddenByEntity = null;
				}

			}
		}

		public override void updateRiderPosition()
		{
			if (this.riddenByEntity != null)
			{
				double d1 = Math.Cos((double)this.rotationYaw * Math.PI / 180.0D) * 0.4D;
				double d3 = Math.Sin((double)this.rotationYaw * Math.PI / 180.0D) * 0.4D;
				this.riddenByEntity.setPosition(this.posX + d1, this.posY + this.MountedYOffset + this.riddenByEntity.YOffset, this.posZ + d3);
			}
		}

		public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
		{
		}

		public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
		{
		}

		public override float ShadowSize
		{
			get
			{
				return 0.0F;
			}
		}

		public override bool interact(EntityPlayer entityPlayer1)
		{
			if (this.riddenByEntity != null && this.riddenByEntity is EntityPlayer && this.riddenByEntity != entityPlayer1)
			{
				return true;
			}
			else
			{
				if (!this.worldObj.isRemote)
				{
					entityPlayer1.mountEntity(this);
				}

				return true;
			}
		}

		public virtual int DamageTaken
		{
			set
			{
				this.dataWatcher.updateObject(19, value);
			}
			get
			{
				return this.dataWatcher.getWatchableObjectInt(19);
			}
		}


		public virtual int TimeSinceHit
		{
			set
			{
				this.dataWatcher.updateObject(17, value);
			}
			get
			{
				return this.dataWatcher.getWatchableObjectInt(17);
			}
		}


		public virtual int ForwardDirection
		{
			set
			{
				this.dataWatcher.updateObject(18, value);
			}
			get
			{
				return this.dataWatcher.getWatchableObjectInt(18);
			}
		}

	}

}