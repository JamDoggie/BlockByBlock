using System;
using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntityEnderEye : Entity
    {
        public int field_40096_a = 0;
        private double field_40094_b;
        private double field_40095_c;
        private double field_40091_d;
        private int despawnTimer;
        private bool shatterOrDrop;

        public EntityEnderEye(World world1) : base(world1)
        {
            SetSize(0.25F, 0.25F);
        }

        protected internal override void entityInit()
        {
        }

        public override bool isInRangeToRenderDist(double d1)
        {
            double d3 = boundingBox.AverageEdgeLength * 4.0D;
            d3 *= 64.0D;
            return d1 < d3 * d3;
        }

        public EntityEnderEye(World world1, double d2, double d4, double d6) : base(world1)
        {
            despawnTimer = 0;
            SetSize(0.25F, 0.25F);
            SetPosition(d2, d4, d6);
            yOffset = 0.0F;
        }

        public virtual void func_40090_a(double d1, int i3, double d4)
        {
            double d6 = d1 - posX;
            double d8 = d4 - posZ;
            float f10 = MathHelper.sqrt_double(d6 * d6 + d8 * d8);
            if (f10 > 12.0F)
            {
                field_40094_b = posX + d6 / (double)f10 * 12.0D;
                field_40091_d = posZ + d8 / (double)f10 * 12.0D;
                field_40095_c = posY + 8.0D;
            }
            else
            {
                field_40094_b = d1;
                field_40095_c = i3;
                field_40091_d = d4;
            }

            despawnTimer = 0;
            shatterOrDrop = rand.Next(5) > 0;
        }

        public override void setVelocity(double d1, double d3, double d5)
        {
            motionX = d1;
            motionY = d3;
            motionZ = d5;
            if (prevRotationPitch == 0.0F && prevRotationYaw == 0.0F)
            {
                float f7 = MathHelper.sqrt_double(d1 * d1 + d5 * d5);
                prevRotationYaw = rotationYaw = (float)(Math.Atan2(d1, d5) * 180.0D / (double)(float)Math.PI);
                prevRotationPitch = rotationPitch = (float)(Math.Atan2(d3, (double)f7) * 180.0D / (double)(float)Math.PI);
            }

        }

        public override void onUpdate()
        {
            lastTickPosX = posX;
            lastTickPosY = posY;
            lastTickPosZ = posZ;
            base.onUpdate();
            posX += motionX;
            posY += motionY;
            posZ += motionZ;
            float f1 = MathHelper.sqrt_double(motionX * motionX + motionZ * motionZ);
            rotationYaw = (float)(Math.Atan2(motionX, motionZ) * 180.0D / (double)(float)Math.PI);

            for (rotationPitch = (float)(Math.Atan2(motionY, (double)f1) * 180.0D / (double)(float)Math.PI); rotationPitch - prevRotationPitch < -180.0F; prevRotationPitch -= 360.0F)
            {
            }

            while (rotationPitch - prevRotationPitch >= 180.0F)
            {
                prevRotationPitch += 360.0F;
            }

            while (rotationYaw - prevRotationYaw < -180.0F)
            {
                prevRotationYaw -= 360.0F;
            }

            while (rotationYaw - prevRotationYaw >= 180.0F)
            {
                prevRotationYaw += 360.0F;
            }

            rotationPitch = prevRotationPitch + (rotationPitch - prevRotationPitch) * 0.2F;
            rotationYaw = prevRotationYaw + (rotationYaw - prevRotationYaw) * 0.2F;
            if (!worldObj.isRemote)
            {
                double d2 = field_40094_b - posX;
                double d4 = field_40091_d - posZ;
                float f6 = (float)Math.Sqrt(d2 * d2 + d4 * d4);
                float f7 = (float)Math.Atan2(d4, d2);
                double d8 = (double)f1 + (double)(f6 - f1) * 0.0025D;
                if (f6 < 1.0F)
                {
                    d8 *= 0.8D;
                    motionY *= 0.8D;
                }

                motionX = Math.Cos((double)f7) * d8;
                motionZ = Math.Sin((double)f7) * d8;
                if (posY < field_40095_c)
                {
                    motionY += (1.0D - motionY) * 0.014999999664723873D;
                }
                else
                {
                    motionY += (-1.0D - motionY) * 0.014999999664723873D;
                }
            }

            float f10 = 0.25F;
            if (InWater)
            {
                for (int i3 = 0; i3 < 4; ++i3)
                {
                    worldObj.spawnParticle("bubble", posX - motionX * (double)f10, posY - motionY * (double)f10, posZ - motionZ * (double)f10, motionX, motionY, motionZ);
                }
            }
            else
            {
                worldObj.spawnParticle("portal", posX - motionX * (double)f10 + rand.NextDouble() * 0.6D - 0.3D, posY - motionY * (double)f10 - 0.5D, posZ - motionZ * (double)f10 + rand.NextDouble() * 0.6D - 0.3D, motionX, motionY, motionZ);
            }

            if (!worldObj.isRemote)
            {
                SetPosition(posX, posY, posZ);
                ++despawnTimer;
                if (despawnTimer > 80 && !worldObj.isRemote)
                {
                    setDead();
                    if (shatterOrDrop)
                    {
                        worldObj.spawnEntityInWorld(new EntityItem(worldObj, posX, posY, posZ, new ItemStack(Item.eyeOfEnder)));
                    }
                    else
                    {
                        worldObj.playAuxSFX(2003, (int)(long)Math.Round(posX, MidpointRounding.AwayFromZero), (int)(long)Math.Round(posY, MidpointRounding.AwayFromZero), (int)(long)Math.Round(posZ, MidpointRounding.AwayFromZero), 0);
                    }
                }
            }

        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
        }

        public override void onCollideWithPlayer(EntityPlayer entityPlayer1)
        {
        }

        public override float ShadowSize
        {
            get
            {
                return 0.0F;
            }
        }

        public override float getBrightness(float f1)
        {
            return 1.0F;
        }

        public override int getBrightnessForRender(float f1)
        {
            return 15728880;
        }

        public override bool canAttackWithItem()
        {
            return false;
        }
    }

}