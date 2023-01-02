using net.minecraft.src;

namespace BlockByBlock.net.minecraft.client.entity.particle
{
    public class EntityExplodeFX : ParticleEffect
    {
        public EntityExplodeFX(World world1, double d2, double d4, double d6, double d8, double d10, double d12) : base(world1, d2, d4, d6, d8, d10, d12)
        {
            motionX = d8 + (double)((float)(portinghelpers.MathHelper.NextDouble * 2.0D - 1.0D) * 0.05F);
            motionY = d10 + (double)((float)(portinghelpers.MathHelper.NextDouble * 2.0D - 1.0D) * 0.05F);
            motionZ = d12 + (double)((float)(portinghelpers.MathHelper.NextDouble * 2.0D - 1.0D) * 0.05F);
            particleRed = particleGreen = particleBlue = rand.NextSingle() * 0.3F + 0.7F;
            particleScale = rand.NextSingle() * rand.NextSingle() * 6.0F + 1.0F;
            particleMaxAge = (int)(16.0D / ((double)rand.NextSingle() * 0.8D + 0.2D)) + 2;
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
            motionX *= 0.9F;
            motionY *= 0.9F;
            motionZ *= 0.9F;
            if (onGround)
            {
                motionX *= 0.7F;
                motionZ *= 0.7F;
            }

        }
    }

}