using net.minecraft.src;

namespace BlockByBlock.net.minecraft.client.entity.particle
{
    public class EntityLavaFX : ParticleEffect
    {
        private float lavaParticleScale;

        public EntityLavaFX(World world1, double d2, double d4, double d6) : base(world1, d2, d4, d6, 0.0D, 0.0D, 0.0D)
        {
            motionX *= 0.8F;
            motionY *= 0.8F;
            motionZ *= 0.8F;
            motionY = rand.NextSingle() * 0.4F + 0.05F;
            particleRed = particleGreen = particleBlue = 1.0F;
            particleScale *= rand.NextSingle() * 2.0F + 0.2F;
            lavaParticleScale = particleScale;
            particleMaxAge = (int)(16.0D / (portinghelpers.MathHelper.NextDouble * 0.8D + 0.2D));
            noClip = false;
            ParticleTextureIndex = 49;
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
            short s4 = 240;
            int i5 = i3 >> 16 & 255;
            return s4 | i5 << 16;
        }

        public override float getBrightness(float f1)
        {
            return 1.0F;
        }

        public override void renderParticle(Tessellator tessellator1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
            float f8 = (particleAge + f2) / particleMaxAge;
            particleScale = lavaParticleScale * (1.0F - f8 * f8);
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

            float f1 = particleAge / (float)particleMaxAge;
            if (rand.NextSingle() > f1)
            {
                worldObj.spawnParticle("smoke", posX, posY, posZ, motionX, motionY, motionZ);
            }

            motionY -= 0.03D;
            moveEntity(motionX, motionY, motionZ);
            motionX *= 0.999F;
            motionY *= 0.999F;
            motionZ *= 0.999F;
            if (onGround)
            {
                motionX *= 0.7F;
                motionZ *= 0.7F;
            }

        }
    }

}