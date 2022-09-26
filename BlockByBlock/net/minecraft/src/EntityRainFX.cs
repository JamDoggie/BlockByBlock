namespace net.minecraft.src
{
	public class EntityRainFX : EntityFX
	{
		public EntityRainFX(World world1, double d2, double d4, double d6) : base(world1, d2, d4, d6, 0.0D, 0.0D, 0.0D)
		{
			this.motionX *= (double)0.3F;
			this.motionY = (double)((float)portinghelpers.MathHelper.NextDouble * 0.2F + 0.1F);
			this.motionZ *= (double)0.3F;
			this.particleRed = 1.0F;
			this.particleGreen = 1.0F;
			this.particleBlue = 1.0F;
			this.ParticleTextureIndex = 19 + this.rand.Next(4);
			this.setSize(0.01F, 0.01F);
			this.particleGravity = 0.06F;
			this.particleMaxAge = (int)(8.0D / (portinghelpers.MathHelper.NextDouble * 0.8D + 0.2D));
		}

		public override void onUpdate()
		{
			this.prevPosX = this.posX;
			this.prevPosY = this.posY;
			this.prevPosZ = this.posZ;
			this.motionY -= (double)this.particleGravity;
			this.moveEntity(this.motionX, this.motionY, this.motionZ);
			this.motionX *= (double)0.98F;
			this.motionY *= (double)0.98F;
			this.motionZ *= (double)0.98F;
			if (this.particleMaxAge-- <= 0)
			{
				this.setDead();
			}

			if (this.onGround)
			{
				if (portinghelpers.MathHelper.NextDouble < 0.5D)
				{
					this.setDead();
				}

				this.motionX *= (double)0.7F;
				this.motionZ *= (double)0.7F;
			}

			Material material1 = this.worldObj.getBlockMaterial(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY), MathHelper.floor_double(this.posZ));
			if (material1.Liquid || material1.Solid)
			{
				double d2 = (double)((float)(MathHelper.floor_double(this.posY) + 1) - BlockFluid.getFluidHeightPercent(this.worldObj.getBlockMetadata(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY), MathHelper.floor_double(this.posZ))));
				if (this.posY < d2)
				{
					this.setDead();
				}
			}

		}
	}

}