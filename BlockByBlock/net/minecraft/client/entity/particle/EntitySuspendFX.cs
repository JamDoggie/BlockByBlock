using net.minecraft.src;

namespace BlockByBlock.net.minecraft.client.entity.particle
{
    public class EntitySuspendFX : ParticleEffect
    {
        public EntitySuspendFX(World world1, double d2, double d4, double d6, double d8, double d10, double d12) : base(world1, d2, d4 - 0.125D, d6, d8, d10, d12)
        {
            particleRed = 0.4F;
            particleGreen = 0.4F;
            particleBlue = 0.7F;
            ParticleTextureIndex = 0;
            SetSize(0.01F, 0.01F);
            particleScale *= rand.NextSingle() * 0.6F + 0.2F;
            motionX = d8 * 0.0D;
            motionY = d10 * 0.0D;
            motionZ = d12 * 0.0D;
            particleMaxAge = (int)(16.0D / (portinghelpers.MathHelper.NextDouble * 0.8D + 0.2D));
        }

        public override void onUpdate()
        {
            prevPosX = posX;
            prevPosY = posY;
            prevPosZ = posZ;
            moveEntity(motionX, motionY, motionZ);
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