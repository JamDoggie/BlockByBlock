namespace net.minecraft.src
{
	public class EntitySpellParticleFX : EntityFX
	{
		private int field_40111_a = 128;

		public EntitySpellParticleFX(World world1, double d2, double d4, double d6, double d8, double d10, double d12) : base(world1, d2, d4, d6, d8, d10, d12)
		{
			this.motionY *= (double)0.2F;
			if (d8 == 0.0D && d12 == 0.0D)
			{
				this.motionX *= (double)0.1F;
				this.motionZ *= (double)0.1F;
			}

			this.particleScale *= 0.75F;
			this.particleMaxAge = (int)(8.0D / (MathHelper.NextDouble * 0.8D + 0.2D));
			this.noClip = false;
		}

		public override void renderParticle(Tessellator tessellator1, float f2, float f3, float f4, float f5, float f6, float f7)
		{
			float f8 = ((float)this.particleAge + f2) / (float)this.particleMaxAge * 32.0F;
			if (f8 < 0.0F)
			{
				f8 = 0.0F;
			}

			if (f8 > 1.0F)
			{
				f8 = 1.0F;
			}

			base.renderParticle(tessellator1, f2, f3, f4, f5, f6, f7);
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

			this.ParticleTextureIndex = this.field_40111_a + (7 - this.particleAge * 8 / this.particleMaxAge);
			this.motionY += 0.004D;
			this.moveEntity(this.motionX, this.motionY, this.motionZ);
			if (this.posY == this.prevPosY)
			{
				this.motionX *= 1.1D;
				this.motionZ *= 1.1D;
			}

			this.motionX *= (double)0.96F;
			this.motionY *= (double)0.96F;
			this.motionZ *= (double)0.96F;
			if (this.onGround)
			{
				this.motionX *= (double)0.7F;
				this.motionZ *= (double)0.7F;
			}

		}

		public virtual void func_40110_b(int i1)
		{
			this.field_40111_a = i1;
		}
	}

}