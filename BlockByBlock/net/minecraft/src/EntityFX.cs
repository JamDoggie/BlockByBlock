namespace net.minecraft.src
{
	public class EntityFX : Entity
	{
		private int particleTextureIndex;
		protected internal float particleTextureJitterX;
		protected internal float particleTextureJitterY;
		protected internal int particleAge = 0;
		protected internal int particleMaxAge = 0;
		protected internal float particleScale;
		protected internal float particleGravity;
		protected internal float particleRed;
		protected internal float particleGreen;
		protected internal float particleBlue;
		public static double interpPosX;
		public static double interpPosY;
		public static double interpPosZ;

		public EntityFX(World world1, double d2, double d4, double d6, double d8, double d10, double d12) : base(world1)
		{
			this.setSize(0.2F, 0.2F);
			this.yOffset = this.height / 2.0F;
			this.setPosition(d2, d4, d6);
			this.particleRed = this.particleGreen = this.particleBlue = 1.0F;
			this.motionX = d8 + (double)((float)(portinghelpers.MathHelper.NextDouble * 2.0D - 1.0D) * 0.4F);
			this.motionY = d10 + (double)((float)(portinghelpers.MathHelper.NextDouble * 2.0D - 1.0D) * 0.4F);
			this.motionZ = d12 + (double)((float)(portinghelpers.MathHelper.NextDouble * 2.0D - 1.0D) * 0.4F);
			float f14 = (float)(portinghelpers.MathHelper.NextDouble + portinghelpers.MathHelper.NextDouble + 1.0D) * 0.15F;
			float f15 = MathHelper.sqrt_double(this.motionX * this.motionX + this.motionY * this.motionY + this.motionZ * this.motionZ);
			this.motionX = this.motionX / (double)f15 * (double)f14 * (double)0.4F;
			this.motionY = this.motionY / (double)f15 * (double)f14 * (double)0.4F + (double)0.1F;
			this.motionZ = this.motionZ / (double)f15 * (double)f14 * (double)0.4F;
			this.particleTextureJitterX = this.rand.NextSingle() * 3.0F;
			this.particleTextureJitterY = this.rand.NextSingle() * 3.0F;
			this.particleScale = (this.rand.NextSingle() * 0.5F + 0.5F) * 2.0F;
			this.particleMaxAge = (int)(4.0F / (this.rand.NextSingle() * 0.9F + 0.1F));
			this.particleAge = 0;
		}

		public virtual EntityFX multiplyVelocity(float f1)
		{
			this.motionX *= (double)f1;
			this.motionY = (this.motionY - (double)0.1F) * (double)f1 + (double)0.1F;
			this.motionZ *= (double)f1;
			return this;
		}

		public virtual EntityFX func_405_d(float f1)
		{
			this.setSize(0.2F * f1, 0.2F * f1);
			this.particleScale *= f1;
			return this;
		}

		public virtual void func_40097_b(float f1, float f2, float f3)
		{
			this.particleRed = f1;
			this.particleGreen = f2;
			this.particleBlue = f3;
		}

		public virtual float func_40098_n()
		{
			return this.particleRed;
		}

		public virtual float func_40101_o()
		{
			return this.particleGreen;
		}

		public virtual float func_40102_p()
		{
			return this.particleBlue;
		}

		protected internal override bool canTriggerWalking()
		{
			return false;
		}

		protected internal override void entityInit()
		{
		}

		public override void onUpdate()
		{
			this.prevPosX = this.posX;
			this.prevPosY = this.posY;
			this.prevPosZ = this.posZ;
			if (this.particleAge++ >= this.particleMaxAge)
			{
				this.setDead();
			}

			this.motionY -= 0.04D * (double)this.particleGravity;
			this.moveEntity(this.motionX, this.motionY, this.motionZ);
			this.motionX *= (double)0.98F;
			this.motionY *= (double)0.98F;
			this.motionZ *= (double)0.98F;
			if (this.onGround)
			{
				this.motionX *= (double)0.7F;
				this.motionZ *= (double)0.7F;
			}

		}

		public virtual void renderParticle(Tessellator tessellator1, float f2, float f3, float f4, float f5, float f6, float f7)
		{
			float f8 = (float)(this.particleTextureIndex % 16) / 16.0F;
			float f9 = f8 + 0.0624375F;
			float f10 = (float)(this.particleTextureIndex / 16) / 16.0F;
			float f11 = f10 + 0.0624375F;
			float f12 = 0.1F * this.particleScale;
			float f13 = (float)(this.prevPosX + (this.posX - this.prevPosX) * (double)f2 - interpPosX);
			float f14 = (float)(this.prevPosY + (this.posY - this.prevPosY) * (double)f2 - interpPosY);
			float f15 = (float)(this.prevPosZ + (this.posZ - this.prevPosZ) * (double)f2 - interpPosZ);
			float f16 = 1.0F;
			tessellator1.setColorOpaque_F(this.particleRed * f16, this.particleGreen * f16, this.particleBlue * f16);
			tessellator1.addVertexWithUV((double)(f13 - f3 * f12 - f6 * f12), (double)(f14 - f4 * f12), (double)(f15 - f5 * f12 - f7 * f12), (double)f9, (double)f11);
			tessellator1.addVertexWithUV((double)(f13 - f3 * f12 + f6 * f12), (double)(f14 + f4 * f12), (double)(f15 - f5 * f12 + f7 * f12), (double)f9, (double)f10);
			tessellator1.addVertexWithUV((double)(f13 + f3 * f12 + f6 * f12), (double)(f14 + f4 * f12), (double)(f15 + f5 * f12 + f7 * f12), (double)f8, (double)f10);
			tessellator1.addVertexWithUV((double)(f13 + f3 * f12 - f6 * f12), (double)(f14 - f4 * f12), (double)(f15 + f5 * f12 - f7 * f12), (double)f8, (double)f11);
		}

		public virtual int FXLayer
		{
			get
			{
				return 0;
			}
		}

		public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
		{
		}

		public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
		{
		}

		public virtual int ParticleTextureIndex
		{
			set
			{
				this.particleTextureIndex = value;
			}
			get
			{
				return this.particleTextureIndex;
			}
		}


		public override bool canAttackWithItem()
		{
			return false;
		}
	}

}