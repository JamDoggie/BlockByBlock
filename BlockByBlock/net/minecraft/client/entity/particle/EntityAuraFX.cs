using net.minecraft.src;

namespace BlockByBlock.net.minecraft.client.entity.particle
{
    public class EntityAuraFX : ParticleEffect
    {
        public EntityAuraFX(World world1, double d2, double d4, double d6, double d8, double d10, double d12) : base(world1, d2, d4, d6, d8, d10, d12)
        {
            float f14 = rand.NextSingle() * 0.1F + 0.2F;
            particleRed = f14;
            particleGreen = f14;
            particleBlue = f14;
            ParticleTextureIndex = 0;
            SetSize(0.02F, 0.02F);
            particleScale *= rand.NextSingle() * 0.6F + 0.5F;
            motionX *= 0.02F;
            motionY *= 0.02F;
            motionZ *= 0.02F;
            particleMaxAge = (int)(20.0D / (portinghelpers.MathHelper.NextDouble * 0.8D + 0.2D));
            noClip = true;
        }

        public override void onUpdate()
        {
            prevPosX = posX;
            prevPosY = posY;
            prevPosZ = posZ;
            moveEntity(motionX, motionY, motionZ);
            motionX *= 0.99D;
            motionY *= 0.99D;
            motionZ *= 0.99D;
            if (particleMaxAge-- <= 0)
            {
                setDead();
            }

        }
    }

}