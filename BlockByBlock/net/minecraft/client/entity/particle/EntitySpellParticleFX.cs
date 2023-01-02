using net.minecraft.src;

namespace BlockByBlock.net.minecraft.client.entity.particle
{
    public class EntitySpellParticleFX : ParticleEffect
    {
        private int field_40111_a = 128;

        public EntitySpellParticleFX(World world1, double d2, double d4, double d6, double d8, double d10, double d12) : base(world1, d2, d4, d6, d8, d10, d12)
        {
            motionY *= 0.2F;
            if (d8 == 0.0D && d12 == 0.0D)
            {
                motionX *= 0.1F;
                motionZ *= 0.1F;
            }

            particleScale *= 0.75F;
            particleMaxAge = (int)(8.0D / (portinghelpers.MathHelper.NextDouble * 0.8D + 0.2D));
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

            ParticleTextureIndex = field_40111_a + (7 - particleAge * 8 / particleMaxAge);
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

        public virtual void func_40110_b(int i1)
        {
            field_40111_a = i1;
        }
    }

}