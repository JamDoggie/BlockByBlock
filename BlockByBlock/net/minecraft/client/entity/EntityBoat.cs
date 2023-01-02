using System;
using net.minecraft.src;

namespace net.minecraft.client.entity
{

    public class EntityBoat : Entity
    {
        private int boatPosRotationIncrements;
        private double boatX;
        private double boatY;
        private double boatZ;
        private double boatYaw;
        private double boatPitch;
        private double velocityX;
        private double velocityY;
        private double velocityZ;

        public EntityBoat(World world1) : base(world1)
        {
            preventEntitySpawning = true;
            SetSize(1.5F, 0.6F);
            yOffset = height / 2.0F;
        }

        protected internal override bool canTriggerWalking()
        {
            return false;
        }

        protected internal override void entityInit()
        {
            dataWatcher.addObject(17, new int?(0));
            dataWatcher.addObject(18, new int?(1));
            dataWatcher.addObject(19, new int?(0));
        }

        public override AxisAlignedBB getCollisionBox(Entity entity1)
        {
            return entity1.boundingBox;
        }

        public override AxisAlignedBB BoundingBox
        {
            get
            {
                return boundingBox;
            }
        }

        public override bool canBePushed()
        {
            return true;
        }

        public EntityBoat(World world1, double d2, double d4, double d6) : this(world1)
        {
            SetPosition(d2, d4 + yOffset, d6);
            motionX = 0.0D;
            motionY = 0.0D;
            motionZ = 0.0D;
            prevPosX = d2;
            prevPosY = d4;
            prevPosZ = d6;
        }

        public override double MountedYOffset
        {
            get
            {
                return height * 0.0D - (double)0.3F;
            }
        }

        public override bool attackEntityFrom(DamageSource damageSource1, int i2)
        {
            if (!worldObj.isRemote && !isDead)
            {
                ForwardDirection = -ForwardDirection;
                TimeSinceHit = 10;
                DamageTaken = DamageTaken + i2 * 10;
                setBeenAttacked();
                if (DamageTaken > 40)
                {
                    if (riddenByEntity != null)
                    {
                        riddenByEntity.mountEntity(this);
                    }

                    int i3;
                    for (i3 = 0; i3 < 3; ++i3)
                    {
                        dropItemWithOffset(Block.planks.blockID, 1, 0.0F);
                    }

                    for (i3 = 0; i3 < 2; ++i3)
                    {
                        dropItemWithOffset(Item.stick.shiftedIndex, 1, 0.0F);
                    }

                    setDead();
                }

                return true;
            }
            else
            {
                return true;
            }
        }

        public override void performHurtAnimation()
        {
            ForwardDirection = -ForwardDirection;
            TimeSinceHit = 10;
            DamageTaken = DamageTaken * 11;
        }

        public override bool canBeCollidedWith()
        {
            return !isDead;
        }

        public override void setPositionAndRotation2(double d1, double d3, double d5, float f7, float f8, int i9)
        {
            boatX = d1;
            boatY = d3;
            boatZ = d5;
            boatYaw = f7;
            boatPitch = f8;
            boatPosRotationIncrements = i9 + 4;
            motionX = velocityX;
            motionY = velocityY;
            motionZ = velocityZ;
        }

        public override void setVelocity(double d1, double d3, double d5)
        {
            velocityX = motionX = d1;
            velocityY = motionY = d3;
            velocityZ = motionZ = d5;
        }

        public override void onUpdate()
        {
            base.onUpdate();
            if (TimeSinceHit > 0)
            {
                TimeSinceHit = TimeSinceHit - 1;
            }

            if (DamageTaken > 0)
            {
                DamageTaken = DamageTaken - 1;
            }

            prevPosX = posX;
            prevPosY = posY;
            prevPosZ = posZ;
            sbyte b1 = 5;
            double d2 = 0.0D;

            for (int i4 = 0; i4 < b1; ++i4)
            {
                double d5 = boundingBox.minY + (boundingBox.maxY - boundingBox.minY) * (i4 + 0) / b1 - 0.125D;
                double d7 = boundingBox.minY + (boundingBox.maxY - boundingBox.minY) * (i4 + 1) / b1 - 0.125D;
                AxisAlignedBB axisAlignedBB9 = AxisAlignedBB.getBoundingBoxFromPool(boundingBox.minX, d5, boundingBox.minZ, boundingBox.maxX, d7, boundingBox.maxZ);
                if (worldObj.isAABBInMaterial(axisAlignedBB9, Material.water))
                {
                    d2 += 1.0D / b1;
                }
            }

            double d21 = Math.Sqrt(motionX * motionX + motionZ * motionZ);
            double d6;
            double d8;
            if (d21 > 0.15D)
            {
                d6 = Math.Cos(rotationYaw * Math.PI / 180.0D);
                d8 = Math.Sin(rotationYaw * Math.PI / 180.0D);

                for (int i10 = 0; i10 < 1.0D + d21 * 60.0D; ++i10)
                {
                    double d11 = (double)(rand.NextSingle() * 2.0F - 1.0F);
                    double d13 = (rand.Next(2) * 2 - 1) * 0.7D;
                    double d15;
                    double d17;
                    if (rand.NextBool())
                    {
                        d15 = posX - d6 * d11 * 0.8D + d8 * d13;
                        d17 = posZ - d8 * d11 * 0.8D - d6 * d13;
                        worldObj.spawnParticle("splash", d15, posY - 0.125D, d17, motionX, motionY, motionZ);
                    }
                    else
                    {
                        d15 = posX + d6 + d8 * d11 * 0.7D;
                        d17 = posZ + d8 - d6 * d11 * 0.7D;
                        worldObj.spawnParticle("splash", d15, posY - 0.125D, d17, motionX, motionY, motionZ);
                    }
                }
            }

            double d12;
            double d23;
            if (worldObj.isRemote)
            {
                if (boatPosRotationIncrements > 0)
                {
                    d6 = posX + (boatX - posX) / boatPosRotationIncrements;
                    d8 = posY + (boatY - posY) / boatPosRotationIncrements;
                    d23 = posZ + (boatZ - posZ) / boatPosRotationIncrements;

                    for (d12 = boatYaw - rotationYaw; d12 < -180.0D; d12 += 360.0D)
                    {
                    }

                    while (d12 >= 180.0D)
                    {
                        d12 -= 360.0D;
                    }

                    rotationYaw = (float)(rotationYaw + d12 / boatPosRotationIncrements);
                    rotationPitch = (float)(rotationPitch + (boatPitch - rotationPitch) / boatPosRotationIncrements);
                    --boatPosRotationIncrements;
                    SetPosition(d6, d8, d23);
                    setRotation(rotationYaw, rotationPitch);
                }
                else
                {
                    d6 = posX + motionX;
                    d8 = posY + motionY;
                    d23 = posZ + motionZ;
                    SetPosition(d6, d8, d23);
                    if (onGround)
                    {
                        motionX *= 0.5D;
                        motionY *= 0.5D;
                        motionZ *= 0.5D;
                    }

                    motionX *= 0.99F;
                    motionY *= 0.95F;
                    motionZ *= 0.99F;
                }

            }
            else
            {
                if (d2 < 1.0D)
                {
                    d6 = d2 * 2.0D - 1.0D;
                    motionY += (double)0.04F * d6;
                }
                else
                {
                    if (motionY < 0.0D)
                    {
                        motionY /= 2.0D;
                    }

                    motionY += 0.007000000216066837D;
                }

                if (riddenByEntity != null)
                {
                    motionX += riddenByEntity.motionX * 0.2D;
                    motionZ += riddenByEntity.motionZ * 0.2D;
                }

                d6 = 0.4D;
                if (motionX < -d6)
                {
                    motionX = -d6;
                }

                if (motionX > d6)
                {
                    motionX = d6;
                }

                if (motionZ < -d6)
                {
                    motionZ = -d6;
                }

                if (motionZ > d6)
                {
                    motionZ = d6;
                }

                if (onGround)
                {
                    motionX *= 0.5D;
                    motionY *= 0.5D;
                    motionZ *= 0.5D;
                }

                moveEntity(motionX, motionY, motionZ);
                if (isCollidedHorizontally && d21 > 0.2D)
                {
                    if (!worldObj.isRemote)
                    {
                        setDead();

                        int i22;
                        for (i22 = 0; i22 < 3; ++i22)
                        {
                            dropItemWithOffset(Block.planks.blockID, 1, 0.0F);
                        }

                        for (i22 = 0; i22 < 2; ++i22)
                        {
                            dropItemWithOffset(Item.stick.shiftedIndex, 1, 0.0F);
                        }
                    }
                }
                else
                {
                    motionX *= 0.99F;
                    motionY *= 0.95F;
                    motionZ *= 0.99F;
                }

                rotationPitch = 0.0F;
                d8 = rotationYaw;
                d23 = prevPosX - posX;
                d12 = prevPosZ - posZ;
                if (d23 * d23 + d12 * d12 > 0.001D)
                {
                    d8 = (double)(float)(Math.Atan2(d12, d23) * 180.0D / Math.PI);
                }

                double d14;
                for (d14 = d8 - rotationYaw; d14 >= 180.0D; d14 -= 360.0D)
                {
                }

                while (d14 < -180.0D)
                {
                    d14 += 360.0D;
                }

                if (d14 > 20.0D)
                {
                    d14 = 20.0D;
                }

                if (d14 < -20.0D)
                {
                    d14 = -20.0D;
                }

                rotationYaw = (float)(rotationYaw + d14);
                setRotation(rotationYaw, rotationPitch);
                System.Collections.IList list16 = worldObj.getEntitiesWithinAABBExcludingEntity(this, boundingBox.expand((double)0.2F, 0.0D, (double)0.2F));
                int i24;
                if (list16 != null && list16.Count > 0)
                {
                    for (i24 = 0; i24 < list16.Count; ++i24)
                    {
                        Entity entity18 = (Entity)list16[i24];
                        if (entity18 != riddenByEntity && entity18.canBePushed() && entity18 is EntityBoat)
                        {
                            entity18.applyEntityCollision(this);
                        }
                    }
                }

                for (i24 = 0; i24 < 4; ++i24)
                {
                    int i25 = MathHelper.floor_double(posX + (i24 % 2 - 0.5D) * 0.8D);
                    int i19 = MathHelper.floor_double(posY);
                    int i20 = MathHelper.floor_double(posZ + (i24 / 2 - 0.5D) * 0.8D);
                    if (worldObj.getBlockId(i25, i19, i20) == Block.snow.blockID)
                    {
                        worldObj.setBlockWithNotify(i25, i19, i20, 0);
                    }
                }

                if (riddenByEntity != null && riddenByEntity.isDead)
                {
                    riddenByEntity = null;
                }

            }
        }

        public override void updateRiderPosition()
        {
            if (riddenByEntity != null)
            {
                double d1 = Math.Cos(rotationYaw * Math.PI / 180.0D) * 0.4D;
                double d3 = Math.Sin(rotationYaw * Math.PI / 180.0D) * 0.4D;
                riddenByEntity.SetPosition(posX + d1, posY + MountedYOffset + riddenByEntity.YOffset, posZ + d3);
            }
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
        }

        public override float ShadowSize
        {
            get
            {
                return 0.0F;
            }
        }

        public override bool interact(EntityPlayer entityPlayer1)
        {
            if (riddenByEntity != null && riddenByEntity is EntityPlayer && riddenByEntity != entityPlayer1)
            {
                return true;
            }
            else
            {
                if (!worldObj.isRemote)
                {
                    entityPlayer1.mountEntity(this);
                }

                return true;
            }
        }

        public virtual int DamageTaken
        {
            set
            {
                dataWatcher.updateObject(19, value);
            }
            get
            {
                return dataWatcher.getWatchableObjectInt(19);
            }
        }


        public virtual int TimeSinceHit
        {
            set
            {
                dataWatcher.updateObject(17, value);
            }
            get
            {
                return dataWatcher.getWatchableObjectInt(17);
            }
        }


        public virtual int ForwardDirection
        {
            set
            {
                dataWatcher.updateObject(18, value);
            }
            get
            {
                return dataWatcher.getWatchableObjectInt(18);
            }
        }

    }

}