using net.minecraft.src;

namespace BlockByBlock.net.minecraft.client.entity.particle
{
    public class EntityNoteFX : ParticleEffect
    {
        internal float noteParticleScale;

        public EntityNoteFX(World world1, double d2, double d4, double d6, double d8, double d10, double d12) : this(world1, d2, d4, d6, d8, d10, d12, 2.0F)
        {
        }

        public EntityNoteFX(World world1, double d2, double d4, double d6, double d8, double d10, double d12, float f14) : base(world1, d2, d4, d6, 0.0D, 0.0D, 0.0D)
        {
            motionX *= 0.01F;
            motionY *= 0.01F;
            motionZ *= 0.01F;
            motionY += 0.2D;
            particleRed = MathHelper.sin(((float)d8 + 0.0F) * (float)Math.PI * 2.0F) * 0.65F + 0.35F;
            particleGreen = MathHelper.sin(((float)d8 + 0.33333334F) * (float)Math.PI * 2.0F) * 0.65F + 0.35F;
            particleBlue = MathHelper.sin(((float)d8 + 0.6666667F) * (float)Math.PI * 2.0F) * 0.65F + 0.35F;
            particleScale *= 0.75F;
            particleScale *= f14;
            noteParticleScale = particleScale;
            particleMaxAge = 6;
            noClip = false;
            ParticleTextureIndex = 64;
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

            particleScale = noteParticleScale * f8;
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
            if (posY == prevPosY)
            {
                motionX *= 1.1D;
                motionZ *= 1.1D;
            }

            motionX *= 0.66F;
            motionY *= 0.66F;
            motionZ *= 0.66F;
            if (onGround)
            {
                motionX *= 0.7F;
                motionZ *= 0.7F;
            }

        }
    }

}