using net.minecraft.src;

namespace BlockByBlock.net.minecraft.client.entity.particle
{
    public class EntitySmokeFX : ParticleEffect
    {
        internal float smokeParticleScale;

        public EntitySmokeFX(World world1, double d2, double d4, double d6, double d8, double d10, double d12) : this(world1, d2, d4, d6, d8, d10, d12, 1.0F)
        {
        }

        public EntitySmokeFX(World world1, double d2, double d4, double d6, double d8, double d10, double d12, float f14) : base(world1, d2, d4, d6, 0.0D, 0.0D, 0.0D)
        {
            motionX *= 0.1F;
            motionY *= 0.1F;
            motionZ *= 0.1F;
            motionX += d8;
            motionY += d10;
            motionZ += d12;
            particleRed = particleGreen = particleBlue = (float)(portinghelpers.MathHelper.NextDouble * (double)0.3F);
            particleScale *= 0.75F;
            particleScale *= f14;
            smokeParticleScale = particleScale;
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

            particleScale = smokeParticleScale * f8;
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
            motionY += 0.004D;
            moveEntity(motionX, motionY, motionZ);
            if (posY == prevPosY)
            {
                motionX *= 1.1D;
                motionZ *= 1.1D;
            }

            motionX *= 0.96F;
            motionY *= 0.96F;
            motionZ *= 0.96F;
            if (onGround)
            {
                motionX *= 0.7F;
                motionZ *= 0.7F;
            }

        }
    }

}