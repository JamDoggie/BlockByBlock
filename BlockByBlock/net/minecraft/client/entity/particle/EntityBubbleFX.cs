using net.minecraft.src;

namespace BlockByBlock.net.minecraft.client.entity.particle
{
    public class EntityBubbleFX : ParticleEffect
    {
        public EntityBubbleFX(World world1, double d2, double d4, double d6, double d8, double d10, double d12) : base(world1, d2, d4, d6, d8, d10, d12)
        {
            particleRed = 1.0F;
            particleGreen = 1.0F;
            particleBlue = 1.0F;
            ParticleTextureIndex = 32;
            SetSize(0.02F, 0.02F);
            particleScale *= rand.NextSingle() * 0.6F + 0.2F;
            motionX = d8 * (double)0.2F + (double)((float)(portinghelpers.MathHelper.NextDouble * 2.0D - 1.0D) * 0.02F);
            motionY = d10 * (double)0.2F + (double)((float)(portinghelpers.MathHelper.NextDouble * 2.0D - 1.0D) * 0.02F);
            motionZ = d12 * (double)0.2F + (double)((float)(portinghelpers.MathHelper.NextDouble * 2.0D - 1.0D) * 0.02F);
            particleMaxAge = (int)(8.0D / (portinghelpers.MathHelper.NextDouble * 0.8D + 0.2D));
        }

        public override void onUpdate()
        {
            prevPosX = posX;
            prevPosY = posY;
            prevPosZ = posZ;
            motionY += 0.002D;
            moveEntity(motionX, motionY, motionZ);
            motionX *= 0.85F;
            motionY *= 0.85F;
            motionZ *= 0.85F;
            if (worldObj.getBlockMaterial(MathHelper.floor_double(posX), MathHelper.floor_double(posY), MathHelper.floor_double(posZ)) != Material.water)
            {
                setDead();
            }

            if (particleMaxAge-- <= 0)
            {
                setDead();
            }

        }
    }

}