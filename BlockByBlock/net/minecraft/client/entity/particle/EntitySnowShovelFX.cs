using net.minecraft.src;

namespace BlockByBlock.net.minecraft.client.entity.particle
{
    public class EntitySnowShovelFX : ParticleEffect
    {
        internal float snowDigParticleScale;

        public EntitySnowShovelFX(World world1, double d2, double d4, double d6, double d8, double d10, double d12) : this(world1, d2, d4, d6, d8, d10, d12, 1.0F)
        {
        }

        public EntitySnowShovelFX(World world1, double d2, double d4, double d6, double d8, double d10, double d12, float f14) : base(world1, d2, d4, d6, d8, d10, d12)
        {
            motionX *= 0.1F;
            motionY *= 0.1F;
            motionZ *= 0.1F;
            motionX += d8;
            motionY += d10;
            motionZ += d12;
            particleRed = particleGreen = particleBlue = 1.0F - (float)(portinghelpers.MathHelper.NextDouble * (double)0.3F);
            particleScale *= 0.75F;
            particleScale *= f14;
            snowDigParticleScale = particleScale;
            particleMaxAge = (int)(8.0D / (portinghelpers.MathHelper.NextDouble * 0.8D + 0.2D));
            particleMaxAge = (int)(particleMaxAge * f14);
            noClip = false;
        }

        public override void renderParticle(Tessellator tessellator1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
            float f8 = (particleAge + f2) / particleMaxAge * 32.0F;
            if (f8 < 0.0F)
            {
                f8 = 0.0F;
            }

            if (f8 > 1.0F)
            {
                f8 = 1.0F;
            }

            particleScale = snowDigParticleScale * f8;
            base.renderParticle(tessellator1, f2, f3, f4, f5, f6, f7);
        }

        public override void onUpdate()
        {
            prevPosX = posX;
            prevPosY = posY;
            prevPosZ = posZ;
            if (particleAge++ >= particleMaxAge)
            {
                setDead();
            }

            ParticleTextureIndex = 7 - particleAge * 8 / particleMaxAge;
            motionY -= 0.03D;
            moveEntity(motionX, motionY, motionZ);
            motionX *= 0.99F;
            motionY *= 0.99F;
            motionZ *= 0.99F;
            if (onGround)
            {
                motionX *= 0.7F;
                motionZ *= 0.7F;
            }

        }
    }

}