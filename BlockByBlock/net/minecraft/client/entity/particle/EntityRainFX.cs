using net.minecraft.src;

namespace BlockByBlock.net.minecraft.client.entity.particle
{
    public class EntityRainFX : ParticleEffect
    {
        public EntityRainFX(World world1, double d2, double d4, double d6) : base(world1, d2, d4, d6, 0.0D, 0.0D, 0.0D)
        {
            motionX *= 0.3F;
            motionY = (float)portinghelpers.MathHelper.NextDouble * 0.2F + 0.1F;
            motionZ *= 0.3F;
            particleRed = 1.0F;
            particleGreen = 1.0F;
            particleBlue = 1.0F;
            ParticleTextureIndex = 19 + rand.Next(4);
            SetSize(0.01F, 0.01F);
            particleGravity = 0.06F;
            particleMaxAge = (int)(8.0D / (portinghelpers.MathHelper.NextDouble * 0.8D + 0.2D));
        }

        public override void onUpdate()
        {
            prevPosX = posX;
            prevPosY = posY;
            prevPosZ = posZ;
            motionY -= particleGravity;
            moveEntity(motionX, motionY, motionZ);
            motionX *= 0.98F;
            motionY *= 0.98F;
            motionZ *= 0.98F;
            if (particleMaxAge-- <= 0)
            {
                setDead();
            }

            if (onGround)
            {
                if (portinghelpers.MathHelper.NextDouble < 0.5D)
                {
                    setDead();
                }

                motionX *= 0.7F;
                motionZ *= 0.7F;
            }

            Material material1 = worldObj.getBlockMaterial(MathHelper.floor_double(posX), MathHelper.floor_double(posY), MathHelper.floor_double(posZ));
            if (material1.Liquid || material1.Solid)
            {
                double d2 = (double)(MathHelper.floor_double(posY) + 1 - BlockFluid.getFluidHeightPercent(worldObj.getBlockMetadata(MathHelper.floor_double(posX), MathHelper.floor_double(posY), MathHelper.floor_double(posZ))));
                if (posY < d2)
                {
                    setDead();
                }
            }

        }
    }

}