using net.minecraft.src;

namespace BlockByBlock.net.minecraft.client.entity.particle
{
    public class EntityFlameFX : ParticleEffect
    {
        private float flameScale;

        public EntityFlameFX(World world1, double d2, double d4, double d6, double d8, double d10, double d12) : base(world1, d2, d4, d6, d8, d10, d12)
        {
            motionX = motionX * (double)0.01F + d8;
            motionY = motionY * (double)0.01F + d10;
            motionZ = motionZ * (double)0.01F + d12;
            double d10000 = d2 + (double)((rand.NextSingle() - rand.NextSingle()) * 0.05F);
            d10000 = d4 + (double)((rand.NextSingle() - rand.NextSingle()) * 0.05F);
            d10000 = d6 + (double)((rand.NextSingle() - rand.NextSingle()) * 0.05F);
            flameScale = particleScale;
            particleRed = particleGreen = particleBlue = 1.0F;
            particleMaxAge = (int)(8.0D / (portinghelpers.MathHelper.NextDouble * 0.8D + 0.2D)) + 4;
            noClip = true;
            ParticleTextureIndex = 48;
        }

        public override void renderParticle(Tessellator tessellator1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
            float f8 = (particleAge + f2) / particleMaxAge;
            particleScale = flameScale * (1.0F - f8 * f8 * 0.5F);
            base.renderParticle(tessellator1, f2, f3, f4, f5, f6, f7);
        }

        public override int getBrightnessForRender(float f1)
        {
            float f2 = (particleAge + f1) / particleMaxAge;
            if (f2 < 0.0F)
            {
                f2 = 0.0F;
            }

            if (f2 > 1.0F)
            {
                f2 = 1.0F;
            }

            int i3 = base.getBrightnessForRender(f1);
            int i4 = i3 & 255;
            int i5 = i3 >> 16 & 255;
            i4 += (int)(f2 * 15.0F * 16.0F);
            if (i4 > 240)
            {
                i4 = 240;
            }

            return i4 | i5 << 16;
        }

        public override float getBrightness(float f1)
        {
            float f2 = (particleAge + f1) / particleMaxAge;
            if (f2 < 0.0F)
            {
                f2 = 0.0F;
            }

            if (f2 > 1.0F)
            {
                f2 = 1.0F;
            }

            float f3 = base.getBrightness(f1);
            return f3 * f2 + (1.0F - f2);
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