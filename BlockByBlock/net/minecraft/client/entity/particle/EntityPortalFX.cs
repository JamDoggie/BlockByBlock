using net.minecraft.src;

namespace BlockByBlock.net.minecraft.client.entity.particle
{
    public class EntityPortalFX : ParticleEffect
    {
        private float portalParticleScale;
        private double portalPosX;
        private double portalPosY;
        private double portalPosZ;

        public EntityPortalFX(World world1, double d2, double d4, double d6, double d8, double d10, double d12) : base(world1, d2, d4, d6, d8, d10, d12)
        {
            motionX = d8;
            motionY = d10;
            motionZ = d12;
            portalPosX = posX = d2;
            portalPosY = posY = d4;
            portalPosZ = posZ = d6;
            float f14 = rand.NextSingle() * 0.6F + 0.4F;
            portalParticleScale = particleScale = rand.NextSingle() * 0.2F + 0.5F;
            particleRed = particleGreen = particleBlue = 1.0F * f14;
            particleGreen *= 0.3F;
            particleRed *= 0.9F;
            particleMaxAge = (int)(portinghelpers.MathHelper.NextDouble * 10.0D) + 40;
            noClip = true;
            ParticleTextureIndex = (int)(portinghelpers.MathHelper.NextDouble * 8.0D);
        }

        public override void renderParticle(Tessellator tessellator1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
            float f8 = (particleAge + f2) / particleMaxAge;
            f8 = 1.0F - f8;
            f8 *= f8;
            f8 = 1.0F - f8;
            particleScale = portalParticleScale * f8;
            base.renderParticle(tessellator1, f2, f3, f4, f5, f6, f7);
        }

        public override int getBrightnessForRender(float f1)
        {
            int i2 = base.getBrightnessForRender(f1);
            float f3 = particleAge / (float)particleMaxAge;
            f3 *= f3;
            f3 *= f3;
            int i4 = i2 & 255;
            int i5 = i2 >> 16 & 255;
            i5 += (int)(f3 * 15.0F * 16.0F);
            if (i5 > 240)
            {
                i5 = 240;
            }

            return i4 | i5 << 16;
        }

        public override float getBrightness(float f1)
        {
            float f2 = base.getBrightness(f1);
            float f3 = particleAge / (float)particleMaxAge;
            f3 = f3 * f3 * f3 * f3;
            return f2 * (1.0F - f3) + f3;
        }

        public override void onUpdate()
        {
            prevPosX = posX;
            prevPosY = posY;
            prevPosZ = posZ;
            float f1 = particleAge / (float)particleMaxAge;
            float f2 = f1;
            f1 = -f1 + f1 * f1 * 2.0F;
            f1 = 1.0F - f1;
            posX = portalPosX + motionX * (double)f1;
            posY = portalPosY + motionY * (double)f1 + (double)(1.0F - f2);
            posZ = portalPosZ + motionZ * (double)f1;
            if (particleAge++ >= particleMaxAge)
            {
                setDead();
            }

        }
    }

}