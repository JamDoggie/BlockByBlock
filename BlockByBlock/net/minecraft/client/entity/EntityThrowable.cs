using System;
using net.minecraft.src;

namespace net.minecraft.client.entity
{

    public abstract class EntityThrowable : Entity
    {
        private int xTile = -1;
        private int yTile = -1;
        private int zTile = -1;
        private int inTile = 0;
        protected internal bool inGround = false;
        public int throwableShake = 0;
        protected internal EntityLiving thrower;
        private int ticksInGround;
        private int ticksInAir = 0;

        public EntityThrowable(World world1) : base(world1)
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

        public EntityThrowable(World world1, EntityLiving entityLiving2) : base(world1)
        {
            thrower = entityLiving2;
            SetSize(0.25F, 0.25F);
            setLocationAndAngles(entityLiving2.posX, entityLiving2.posY + (double)entityLiving2.EyeHeight, entityLiving2.posZ, entityLiving2.rotationYaw, entityLiving2.rotationPitch);
            posX -= MathHelper.cos(rotationYaw / 180.0F * (float)Math.PI) * 0.16F;
            posY -= 0.1F;
            posZ -= MathHelper.sin(rotationYaw / 180.0F * (float)Math.PI) * 0.16F;
            SetPosition(posX, posY, posZ);
            yOffset = 0.0F;
            float f3 = 0.4F;
            motionX = -MathHelper.sin(rotationYaw / 180.0F * (float)Math.PI) * MathHelper.cos(rotationPitch / 180.0F * (float)Math.PI) * f3;
            motionZ = MathHelper.cos(rotationYaw / 180.0F * (float)Math.PI) * MathHelper.cos(rotationPitch / 180.0F * (float)Math.PI) * f3;
            motionY = -MathHelper.sin((rotationPitch + func_40074_d()) / 180.0F * (float)Math.PI) * f3;
            setThrowableHeading(motionX, motionY, motionZ, func_40077_c(), 1.0F);
        }

        public EntityThrowable(World world1, double d2, double d4, double d6) : base(world1)
        {
            ticksInGround = 0;
            SetSize(0.25F, 0.25F);
            SetPosition(d2, d4, d6);
            yOffset = 0.0F;
        }

        protected internal virtual float func_40077_c()
        {
            return 1.5F;
        }

        protected internal virtual float func_40074_d()
        {
            return 0.0F;
        }

        public virtual void setThrowableHeading(double d1, double d3, double d5, float f7, float f8)
        {
            float f9 = MathHelper.sqrt_double(d1 * d1 + d3 * d3 + d5 * d5);
            d1 /= (double)f9;
            d3 /= (double)f9;
            d5 /= (double)f9;
            d1 += rand.NextGaussian() * (double)0.0075F * (double)f8;
            d3 += rand.NextGaussian() * (double)0.0075F * (double)f8;
            d5 += rand.NextGaussian() * (double)0.0075F * (double)f8;
            d1 *= (double)f7;
            d3 *= (double)f7;
            d5 *= (double)f7;
            motionX = d1;
            motionY = d3;
            motionZ = d5;
            float f10 = MathHelper.sqrt_double(d1 * d1 + d5 * d5);
            prevRotationYaw = rotationYaw = (float)(Math.Atan2(d1, d5) * 180.0D / (double)(float)Math.PI);
            prevRotationPitch = rotationPitch = (float)(Math.Atan2(d3, (double)f10) * 180.0D / (double)(float)Math.PI);
            ticksInGround = 0;
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
            if (throwableShake > 0)
            {
                --throwableShake;
            }

            if (inGround)
            {
                int i1 = worldObj.getBlockId(xTile, yTile, zTile);
                if (i1 == inTile)
                {
                    ++ticksInGround;
                    if (ticksInGround == 1200)
                    {
                        setDead();
                    }

                    return;
                }

                inGround = false;
                motionX *= rand.NextSingle() * 0.2F;
                motionY *= rand.NextSingle() * 0.2F;
                motionZ *= rand.NextSingle() * 0.2F;
                ticksInGround = 0;
                ticksInAir = 0;
            }
            else
            {
                ++ticksInAir;
            }

            Vec3D vec3D15 = Vec3D.createVector(posX, posY, posZ);
            Vec3D vec3D2 = Vec3D.createVector(posX + motionX, posY + motionY, posZ + motionZ);
            MovingObjectPosition movingObjectPosition3 = worldObj.rayTraceBlocks(vec3D15, vec3D2);
            vec3D15 = Vec3D.createVector(posX, posY, posZ);
            vec3D2 = Vec3D.createVector(posX + motionX, posY + motionY, posZ + motionZ);
            if (movingObjectPosition3 != null)
            {
                vec3D2 = Vec3D.createVector(movingObjectPosition3.hitVec.xCoord, movingObjectPosition3.hitVec.yCoord, movingObjectPosition3.hitVec.zCoord);
            }

            if (!worldObj.isRemote)
            {
                Entity entity4 = null;
                System.Collections.IList list5 = worldObj.getEntitiesWithinAABBExcludingEntity(this, boundingBox.addCoord(motionX, motionY, motionZ).expand(1.0D, 1.0D, 1.0D));
                double d6 = 0.0D;

                for (int i8 = 0; i8 < list5.Count; ++i8)
                {
                    Entity entity9 = (Entity)list5[i8];
                    if (entity9.canBeCollidedWith() && (entity9 != thrower || ticksInAir >= 5))
                    {
                        float f10 = 0.3F;
                        AxisAlignedBB axisAlignedBB11 = entity9.boundingBox.expand((double)f10, (double)f10, (double)f10);
                        MovingObjectPosition movingObjectPosition12 = axisAlignedBB11.calculateIntercept(vec3D15, vec3D2);
                        if (movingObjectPosition12 != null)
                        {
                            double d13 = vec3D15.distanceTo(movingObjectPosition12.hitVec);
                            if (d13 < d6 || d6 == 0.0D)
                            {
                                entity4 = entity9;
                                d6 = d13;
                            }
                        }
                    }
                }

                if (entity4 != null)
                {
                    movingObjectPosition3 = new MovingObjectPosition(entity4);
                }
            }

            if (movingObjectPosition3 != null)
            {
                onImpact(movingObjectPosition3);
            }

            posX += motionX;
            posY += motionY;
            posZ += motionZ;
            float f16 = MathHelper.sqrt_double(motionX * motionX + motionZ * motionZ);
            rotationYaw = (float)(Math.Atan2(motionX, motionZ) * 180.0D / (double)(float)Math.PI);

            for (rotationPitch = (float)(Math.Atan2(motionY, (double)f16) * 180.0D / (double)(float)Math.PI); rotationPitch - prevRotationPitch < -180.0F; prevRotationPitch -= 360.0F)
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
            float f17 = 0.99F;
            float f18 = func_40075_e();
            if (InWater)
            {
                for (int i7 = 0; i7 < 4; ++i7)
                {
                    float f19 = 0.25F;
                    worldObj.spawnParticle("bubble", posX - motionX * (double)f19, posY - motionY * (double)f19, posZ - motionZ * (double)f19, motionX, motionY, motionZ);
                }

                f17 = 0.8F;
            }

            motionX *= f17;
            motionY *= f17;
            motionZ *= f17;
            motionY -= f18;
            SetPosition(posX, posY, posZ);
        }

        protected internal virtual float func_40075_e()
        {
            return 0.03F;
        }

        protected internal abstract void onImpact(MovingObjectPosition movingObjectPosition1);

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            nBTTagCompound1.setShort("xTile", (short)xTile);
            nBTTagCompound1.setShort("yTile", (short)yTile);
            nBTTagCompound1.setShort("zTile", (short)zTile);
            nBTTagCompound1.setByte("inTile", (sbyte)inTile);
            nBTTagCompound1.setByte("shake", (sbyte)throwableShake);
            nBTTagCompound1.setByte("inGround", (sbyte)(inGround ? 1 : 0));
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            xTile = nBTTagCompound1.getShort("xTile");
            yTile = nBTTagCompound1.getShort("yTile");
            zTile = nBTTagCompound1.getShort("zTile");
            inTile = nBTTagCompound1.getByte("inTile") & 255;
            throwableShake = nBTTagCompound1.getByte("shake") & 255;
            inGround = nBTTagCompound1.getByte("inGround") == 1;
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
    }

}