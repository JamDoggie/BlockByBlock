using System;

namespace net.minecraft.src
{
	public class EntityTNTPrimed : Entity
	{
		public int fuse;

		public EntityTNTPrimed(World world1) : base(world1)
		{
			this.fuse = 0;
			this.preventEntitySpawning = true;
			this.setSize(0.98F, 0.98F);
			this.yOffset = this.height / 2.0F;
		}

		public EntityTNTPrimed(World world1, double d2, double d4, double d6) : this(world1)
		{
			this.setPosition(d2, d4, d6);
			float f8 = (float)(MathHelper.NextDouble * (double)(float)Math.PI * 2.0D);
			this.motionX = (double)(-((float)Math.Sin((double)f8)) * 0.02F);
			this.motionY = (double)0.2F;
			this.motionZ = (double)(-((float)Math.Cos((double)f8)) * 0.02F);
			this.fuse = 80;
			this.prevPosX = d2;
			this.prevPosY = d4;
			this.prevPosZ = d6;
		}

		protected internal override void entityInit()
		{
		}

		protected internal override bool canTriggerWalking()
		{
			return false;
		}

		public override bool canBeCollidedWith()
		{
			return !this.isDead;
		}

		public override void onUpdate()
		{
			this.prevPosX = this.posX;
			this.prevPosY = this.posY;
			this.prevPosZ = this.posZ;
			this.motionY -= (double)0.04F;
			this.moveEntity(this.motionX, this.motionY, this.motionZ);
			this.motionX *= (double)0.98F;
			this.motionY *= (double)0.98F;
			this.motionZ *= (double)0.98F;
			if (this.onGround)
			{
				this.motionX *= (double)0.7F;
				this.motionZ *= (double)0.7F;
				this.motionY *= -0.5D;
			}

			if (this.fuse-- <= 0)
			{
				if (!this.worldObj.isRemote)
				{
					this.setDead();
					this.explode();
				}
				else
				{
					this.setDead();
				}
			}
			else
			{
				this.worldObj.spawnParticle("smoke", this.posX, this.posY + 0.5D, this.posZ, 0.0D, 0.0D, 0.0D);
			}

		}

		private void explode()
		{
			float f1 = 4.0F;
			this.worldObj.createExplosion((Entity)null, this.posX, this.posY, this.posZ, f1);
		}

		protected internal override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
		{
			nBTTagCompound1.setByte("Fuse", (sbyte)this.fuse);
		}

		protected internal override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
		{
			this.fuse = nBTTagCompound1.getByte("Fuse");
		}

		public override float ShadowSize
		{
			get
			{
				return 0.0F;
			}
		}
	}

}