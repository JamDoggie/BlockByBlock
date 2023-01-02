using net.minecraft.src;

namespace BlockByBlock.net.minecraft.client.entity.particle
{
    public class EntityReddustFX : ParticleEffect
    {
        internal float reddustParticleScale;

        public EntityReddustFX(World world1, double d2, double d4, double d6, float f8, float f9, float f10) : this(world1, d2, d4, d6, 1.0F, f8, f9, f10)
        {
        }

        public EntityReddustFX(World world1, double d2, double d4, double d6, float f8, float f9, float f10, float f11) : base(world1, d2, d4, d6, 0.0D, 0.0D, 0.0D)
        {
            motionX *= 0.1F;
            motionY *= 0.1F;
            motionZ *= 0.1F;
            if (f9 == 0.0F)
            {
                f9 = 1.0F;
            }

            float f12 = (float)portinghelpers.MathHelper.NextDouble * 0.4F + 0.6F;
            particleRed = ((float)(portinghelpers.MathHelper.NextDouble * (double)0.2F) + 0.8F) * f9 * f12;
            particleGreen = ((float)(portinghelpers.MathHelper.NextDouble * (double)0.2F) + 0.8F) * f10 * f12;
            particleBlue = ((float)(portinghelpers.MathHelper.NextDouble * (double)0.2F) + 0.8F) * f11 * f12;
            particleScale *= 0.75F;
            particleScale *= f8;
            reddustParticleScale = particleScale;
            particleMaxAge = (int)(8.0D / (portinghelpers.MathHelper.NextDouble * 0.8D + 0.2D));
            particleMaxAge = (int)(particleMaxAge * f8);
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

            particleScale = reddustParticleScale * f8;
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