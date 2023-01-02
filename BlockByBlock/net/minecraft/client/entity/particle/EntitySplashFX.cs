using net.minecraft.src;

namespace BlockByBlock.net.minecraft.client.entity.particle
{
    public class EntitySplashFX : EntityRainFX
    {
        public EntitySplashFX(World world1, double d2, double d4, double d6, double d8, double d10, double d12) : base(world1, d2, d4, d6)
        {
            particleGravity = 0.04F;
            ParticleTextureIndex = ParticleTextureIndex + 1;
            if (d10 == 0.0D && (d8 != 0.0D || d12 != 0.0D))
            {
                motionX = d8;
                motionY = d10 + 0.1D;
                motionZ = d12;
            }

        }
    }

}