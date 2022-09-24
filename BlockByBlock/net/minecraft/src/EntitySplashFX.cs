namespace net.minecraft.src
{
	public class EntitySplashFX : EntityRainFX
	{
		public EntitySplashFX(World world1, double d2, double d4, double d6, double d8, double d10, double d12) : base(world1, d2, d4, d6)
		{
			this.particleGravity = 0.04F;
			this.ParticleTextureIndex = this.ParticleTextureIndex + 1;
			if (d10 == 0.0D && (d8 != 0.0D || d12 != 0.0D))
			{
				this.motionX = d8;
				this.motionY = d10 + 0.1D;
				this.motionZ = d12;
			}

		}
	}

}