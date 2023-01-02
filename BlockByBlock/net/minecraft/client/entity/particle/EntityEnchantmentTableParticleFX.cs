using net.minecraft.src;

namespace BlockByBlock.net.minecraft.client.entity.particle
{
    public class EntityEnchantmentTableParticleFX : ParticleEffect
    {
        private float field_40107_a;
        private double field_40109_aw;
        private double field_40108_ax;
        private double field_40106_ay;

        public EntityEnchantmentTableParticleFX(World world1, double d2, double d4, double d6, double d8, double d10, double d12) : base(world1, d2, d4, d6, d8, d10, d12)
        {
            motionX = d8;
            motionY = d10;
            motionZ = d12;
            field_40109_aw = posX = d2;
            field_40108_ax = posY = d4;
            field_40106_ay = posZ = d6;
            float f14 = rand.NextSingle() * 0.6F + 0.4F;
            field_40107_a = particleScale = rand.NextSingle() * 0.5F + 0.2F;
            particleRed = particleGreen = particleBlue = 1.0F * f14;
            particleGreen *= 0.9F;
            particleRed *= 0.9F;
            particleMaxAge = (int)(portinghelpers.MathHelper.NextDouble * 10.0D) + 30;
            noClip = true;
            ParticleTextureIndex = (int)(portinghelpers.MathHelper.NextDouble * 26.0D + 1.0D + 224.0D);
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
            f3 *= f3;
            f3 *= f3;
            return f2 * (1.0F - f3) + f3;
        }

        public override void onUpdate()
        {
            prevPosX = posX;
            prevPosY = posY;
            prevPosZ = posZ;
            float f1 = particleAge / (float)particleMaxAge;
            f1 = 1.0F - f1;
            float f2 = 1.0F - f1;
            f2 *= f2;
            f2 *= f2;
            posX = field_40109_aw + motionX * (double)f1;
            posY = field_40108_ax + motionY * (double)f1 - (double)(f2 * 1.2F);
            posZ = field_40106_ay + motionZ * (double)f1;
            if (particleAge++ >= particleMaxAge)
            {
                setDead();
            }

        }
    }

}