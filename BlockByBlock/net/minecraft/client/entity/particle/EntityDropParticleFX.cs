using net.minecraft.src;

namespace BlockByBlock.net.minecraft.client.entity.particle
{
    public class EntityDropParticleFX : ParticleEffect
    {
        private Material materialType;
        private int bobTimer;

        public EntityDropParticleFX(World world1, double d2, double d4, double d6, Material material8) : base(world1, d2, d4, d6, 0.0D, 0.0D, 0.0D)
        {
            motionX = motionY = motionZ = 0.0D;
            if (material8 == Material.water)
            {
                particleRed = 0.0F;
                particleGreen = 0.0F;
                particleBlue = 1.0F;
            }
            else
            {
                particleRed = 1.0F;
                particleGreen = 0.0F;
                particleBlue = 0.0F;
            }

            ParticleTextureIndex = 113;
            SetSize(0.01F, 0.01F);
            particleGravity = 0.06F;
            materialType = material8;
            bobTimer = 40;
            particleMaxAge = (int)(64.0D / (portinghelpers.MathHelper.NextDouble * 0.8D + 0.2D));
            motionX = motionY = motionZ = 0.0D;
        }

        public override int getBrightnessForRender(float f1)
        {
            return materialType == Material.water ? base.getBrightnessForRender(f1) : 257;
        }

        public override float getBrightness(float f1)
        {
            return materialType == Material.water ? base.getBrightness(f1) : 1.0F;
        }

        public override void onUpdate()
        {
            prevPosX = posX;
            prevPosY = posY;
            prevPosZ = posZ;
            if (materialType == Material.water)
            {
                particleRed = 0.2F;
                particleGreen = 0.3F;
                particleBlue = 1.0F;
            }
            else
            {
                particleRed = 1.0F;
                particleGreen = 16.0F / (40 - bobTimer + 16);
                particleBlue = 4.0F / (40 - bobTimer + 8);
            }

            motionY -= particleGravity;
            if (bobTimer-- > 0)
            {
                motionX *= 0.02D;
                motionY *= 0.02D;
                motionZ *= 0.02D;
                ParticleTextureIndex = 113;
            }
            else
            {
                ParticleTextureIndex = 112;
            }

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
                if (materialType == Material.water)
                {
                    setDead();
                    worldObj.spawnParticle("splash", posX, posY, posZ, 0.0D, 0.0D, 0.0D);
                }
                else
                {
                    ParticleTextureIndex = 114;
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