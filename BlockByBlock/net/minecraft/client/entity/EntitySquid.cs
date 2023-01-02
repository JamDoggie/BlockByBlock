using System;
using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntitySquid : EntityWaterMob
    {
        public float field_21089_a = 0.0F;
        public float field_21088_b = 0.0F;
        public float field_21087_c = 0.0F;
        public float field_21086_f = 0.0F;
        public float field_21085_g = 0.0F;
        public float field_21084_h = 0.0F;
        public float tentacleAngle = 0.0F;
        public float lastTentacleAngle = 0.0F;
        private float randomMotionSpeed = 0.0F;
        private float field_21080_l = 0.0F;
        private float field_21079_m = 0.0F;
        private float randomMotionVecX = 0.0F;
        private float randomMotionVecY = 0.0F;
        private float randomMotionVecZ = 0.0F;

        public EntitySquid(World world1) : base(world1)
        {
            texture = "/mob/squid.png";
            SetSize(0.95F, 0.95F);
            field_21080_l = 1.0F / (rand.NextSingle() + 1.0F) * 0.2F;
        }

        public override int MaxHealth
        {
            get
            {
                return 10;
            }
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            base.writeEntityToNBT(nBTTagCompound1);
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            base.readEntityFromNBT(nBTTagCompound1);
        }

        protected internal override string LivingSound
        {
            get
            {
                return null;
            }
        }

        protected internal override string HurtSound
        {
            get
            {
                return null;
            }
        }

        protected internal override string DeathSound
        {
            get
            {
                return null;
            }
        }

        protected internal override float SoundVolume
        {
            get
            {
                return 0.4F;
            }
        }

        protected internal override int DropItemId
        {
            get
            {
                return 0;
            }
        }

        protected internal override void dropFewItems(bool z1, int i2)
        {
            int i3 = rand.Next(3 + i2) + 1;

            for (int i4 = 0; i4 < i3; ++i4)
            {
                entityDropItem(new ItemStack(Item.dyePowder, 1, 0), 0.0F);
            }

        }

        public override bool interact(EntityPlayer entityPlayer1)
        {
            return base.interact(entityPlayer1);
        }

        public override bool InWater
        {
            get
            {
                return worldObj.handleMaterialAcceleration(boundingBox.expand(0.0D, -0.6000000238418579D, 0.0D), Material.water, this);
            }
        }

        public override void onLivingUpdate()
        {
            base.onLivingUpdate();
            field_21088_b = field_21089_a;
            field_21086_f = field_21087_c;
            field_21084_h = field_21085_g;
            lastTentacleAngle = tentacleAngle;
            field_21085_g += field_21080_l;
            if (field_21085_g > 6.2831855F)
            {
                field_21085_g -= 6.2831855F;
                if (rand.Next(10) == 0)
                {
                    field_21080_l = 1.0F / (rand.NextSingle() + 1.0F) * 0.2F;
                }
            }

            if (InWater)
            {
                float f1;
                if (field_21085_g < (float)Math.PI)
                {
                    f1 = field_21085_g / (float)Math.PI;
                    tentacleAngle = MathHelper.sin(f1 * f1 * (float)Math.PI) * (float)Math.PI * 0.25F;
                    if ((double)f1 > 0.75D)
                    {
                        randomMotionSpeed = 1.0F;
                        field_21079_m = 1.0F;
                    }
                    else
                    {
                        field_21079_m *= 0.8F;
                    }
                }
                else
                {
                    tentacleAngle = 0.0F;
                    randomMotionSpeed *= 0.9F;
                    field_21079_m *= 0.99F;
                }

                if (!worldObj.isRemote)
                {
                    motionX = randomMotionVecX * randomMotionSpeed;
                    motionY = randomMotionVecY * randomMotionSpeed;
                    motionZ = randomMotionVecZ * randomMotionSpeed;
                }

                f1 = MathHelper.sqrt_double(motionX * motionX + motionZ * motionZ);
                renderYawOffset += (-(float)Math.Atan2(motionX, motionZ) * 180.0F / (float)Math.PI - renderYawOffset) * 0.1F;
                rotationYaw = renderYawOffset;
                field_21087_c += (float)Math.PI * field_21079_m * 1.5F;
                field_21089_a += (-(float)Math.Atan2((double)f1, motionY) * 180.0F / (float)Math.PI - field_21089_a) * 0.1F;
            }
            else
            {
                tentacleAngle = MathHelper.abs(MathHelper.sin(field_21085_g)) * (float)Math.PI * 0.25F;
                if (!worldObj.isRemote)
                {
                    motionX = 0.0D;
                    motionY -= 0.08D;
                    motionY *= 0.98F;
                    motionZ = 0.0D;
                }

                field_21089_a = (float)(field_21089_a + (double)(-90.0F - field_21089_a) * 0.02D);
            }

        }

        public override void moveEntityWithHeading(float f1, float f2)
        {
            moveEntity(motionX, motionY, motionZ);
        }

        public override void updateEntityActionState()
        {
            ++entityAge;
            if (entityAge > 100)
            {
                randomMotionVecX = randomMotionVecY = randomMotionVecZ = 0.0F;
            }
            else if (rand.Next(50) == 0 || !inWater || randomMotionVecX == 0.0F && randomMotionVecY == 0.0F && randomMotionVecZ == 0.0F)
            {
                float f1 = rand.NextSingle() * (float)Math.PI * 2.0F;
                randomMotionVecX = MathHelper.cos(f1) * 0.2F;
                randomMotionVecY = -0.1F + rand.NextSingle() * 0.2F;
                randomMotionVecZ = MathHelper.sin(f1) * 0.2F;
            }

            despawnEntity();
        }

        public override bool CanSpawnHere
        {
            get
            {
                return posY > 45.0D && posY < 63.0D && base.CanSpawnHere;
            }
        }
    }

}