namespace net.minecraft.src
{
	public class EntitySuspendFX : EntityFX
	{
		public EntitySuspendFX(World world1, double d2, double d4, double d6, double d8, double d10, double d12) : base(world1, d2, d4 - 0.125D, d6, d8, d10, d12)
		{
			this.particleRed = 0.4F;
			this.particleGreen = 0.4F;
			this.particleBlue = 0.7F;
			this.ParticleTextureIndex = 0;
			this.setSize(0.01F, 0.01F);
			this.particleScale *= this.rand.NextSingle() * 0.6F + 0.2F;
			this.motionX = d8 * 0.0D;
			this.motionY = d10 * 0.0D;
			this.motionZ = d12 * 0.0D;
			this.particleMaxAge = (int)(16.0D / (portinghelpers.MathHelper.NextDouble * 0.8D + 0.2D));
		}

		public override void onUpdate()
		{
			this.prevPosX = this.posX;
			this.prevPosY = this.posY;
			this.prevPosZ = this.posZ;
			this.moveEntity(this.motionX, this.motionY, this.motionZ);
			if (this.worldObj.getBlockMaterial(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY), MathHelper.floor_double(this.posZ)) != Material.water)
			{
				this.setDead();
			}

			if (this.particleMaxAge-- <= 0)
			{
				this.setDead();
			}

		}
	}

}