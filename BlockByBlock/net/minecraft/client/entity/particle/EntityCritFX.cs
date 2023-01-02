using net.minecraft.src;

namespace BlockByBlock.net.minecraft.client.entity.particle
{
    public class EntityCritFX : ParticleEffect
    {
        internal float field_35137_a;

        public EntityCritFX(World world1, double d2, double d4, double d6, double d8, double d10, double d12) : this(world1, d2, d4, d6, d8, d10, d12, 1.0F)
        {
        }

        public EntityCritFX(World world1, double d2, double d4, double d6, double d8, double d10, double d12, float f14) : base(world1, d2, d4, d6, 0.0D, 0.0D, 0.0D)
        {
            motionX *= 0.1F;
            motionY *= 0.1F;
            motionZ *= 0.1F;
            motionX += d8 * 0.4D;
            motionY += d10 * 0.4D;
            motionZ += d12 * 0.4D;
            particleRed = particleGreen = particleBlue = (float)(portinghelpers.MathHelper.NextDouble * (double)0.3F + (double)0.6F);
            particleScale *= 0.75F;
            particleScale *= f14;
            field_35137_a = particleScale;
            particleMaxAge = (int)(6.0D / (portinghelpers.MathHelper.NextDouble * 0.8D + 0.6D));
            particleMaxAge = (int)(particleMaxAge * f14);
            noClip = false;
            ParticleTextureIndex = 65;
            onUpdate();
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

            particleScale = field_35137_a * f8;
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

            moveEntity(motionX, motionY, motionZ);
            particleGreen = (float)(particleGreen * 0.96D);
            particleBlue = (float)(particleBlue * 0.9D);
            motionX *= 0.7F;
            motionY *= 0.7F;
            motionZ *= 0.7F;
            motionY -= 0.02F;
            if (onGround)
            {
                motionX *= 0.7F;
                motionZ *= 0.7F;
            }

        }
    }

}