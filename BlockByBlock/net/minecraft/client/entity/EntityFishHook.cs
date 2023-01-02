using System;
using net.minecraft.src;

namespace net.minecraft.client.entity
{

    public class EntityFishHook : Entity
    {
        private int xTile;
        private int yTile;
        private int zTile;
        private int inTile;
        private bool inGround;
        public int shake;
        public EntityPlayer angler;
        private int ticksInGround;
        private int ticksInAir;
        private int ticksCatchable;
        public Entity bobber;
        private int fishPosRotationIncrements;
        private double fishX;
        private double fishY;
        private double fishZ;
        private double fishYaw;
        private double fishPitch;
        private double velocityX;
        private double velocityY;
        private double velocityZ;

        public EntityFishHook(World world1) : base(world1)
        {
            xTile = -1;
            yTile = -1;
            zTile = -1;
            inTile = 0;
            inGround = false;
            shake = 0;
            ticksInAir = 0;
            ticksCatchable = 0;
            bobber = null;
            SetSize(0.25F, 0.25F);
            ignoreFrustumCheck = true;
        }

        public EntityFishHook(World world1, double d2, double d4, double d6) : this(world1)
        {
            SetPosition(d2, d4, d6);
            ignoreFrustumCheck = true;
        }

        public EntityFishHook(World world1, EntityPlayer entityPlayer2) : base(world1)
        {
            xTile = -1;
            yTile = -1;
            zTile = -1;
            inTile = 0;
            inGround = false;
            shake = 0;
            ticksInAir = 0;
            ticksCatchable = 0;
            bobber = null;
            ignoreFrustumCheck = true;
            angler = entityPlayer2;
            angler.fishEntity = this;
            SetSize(0.25F, 0.25F);
            setLocationAndAngles(entityPlayer2.posX, entityPlayer2.posY + 1.62D - entityPlayer2.yOffset, entityPlayer2.posZ, entityPlayer2.rotationYaw, entityPlayer2.rotationPitch);
            posX -= MathHelper.cos(rotationYaw / 180.0F * (float)Math.PI) * 0.16F;
            posY -= 0.1F;
            posZ -= MathHelper.sin(rotationYaw / 180.0F * (float)Math.PI) * 0.16F;
            SetPosition(posX, posY, posZ);
            yOffset = 0.0F;
            float f3 = 0.4F;
            motionX = -MathHelper.sin(rotationYaw / 180.0F * (float)Math.PI) * MathHelper.cos(rotationPitch / 180.0F * (float)Math.PI) * f3;
            motionZ = MathHelper.cos(rotationYaw / 180.0F * (float)Math.PI) * MathHelper.cos(rotationPitch / 180.0F * (float)Math.PI) * f3;
            motionY = -MathHelper.sin(rotationPitch / 180.0F * (float)Math.PI) * f3;
            calculateVelocity(motionX, motionY, motionZ, 1.5F, 1.0F);
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

        public virtual void calculateVelocity(double d1, double d3, double d5, float f7, float f8)
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

        public override void setPositionAndRotation2(double d1, double d3, double d5, float f7, float f8, int i9)
        {
            fishX = d1;
            fishY = d3;
            fishZ = d5;
            fishYaw = f7;
            fishPitch = f8;
            fishPosRotationIncrements = i9;
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
            if (fishPosRotationIncrements > 0)
            {
                double d21 = posX + (fishX - posX) / fishPosRotationIncrements;
                double d22 = posY + (fishY - posY) / fishPosRotationIncrements;
                double d23 = posZ + (fishZ - posZ) / fishPosRotationIncrements;

                double d7;
                for (d7 = fishYaw - rotationYaw; d7 < -180.0D; d7 += 360.0D)
                {
                }

                while (d7 >= 180.0D)
                {
                    d7 -= 360.0D;
                }

                rotationYaw = (float)(rotationYaw + d7 / fishPosRotationIncrements);
                rotationPitch = (float)(rotationPitch + (fishPitch - rotationPitch) / fishPosRotationIncrements);
                --fishPosRotationIncrements;
                SetPosition(d21, d22, d23);
                setRotation(rotationYaw, rotationPitch);
            }
            else
            {
                if (!worldObj.isRemote)
                {
                    ItemStack itemStack1 = angler.CurrentEquippedItem;
                    if (angler.isDead || !angler.EntityAlive || itemStack1 == null || itemStack1.Item != Item.fishingRod || getDistanceSqToEntity(angler) > 1024.0D)
                    {
                        setDead();
                        angler.fishEntity = null;
                        return;
                    }

                    if (bobber != null)
                    {
                        if (!bobber.isDead)
                        {
                            posX = bobber.posX;
                            posY = bobber.boundingBox.minY + bobber.height * 0.8D;
                            posZ = bobber.posZ;
                            return;
                        }

                        bobber = null;
                    }
                }

                if (shake > 0)
                {
                    --shake;
                }

                if (inGround)
                {
                    int i19 = worldObj.getBlockId(xTile, yTile, zTile);
                    if (i19 == inTile)
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

                Vec3D vec3D20 = Vec3D.createVector(posX, posY, posZ);
                Vec3D vec3D2 = Vec3D.createVector(posX + motionX, posY + motionY, posZ + motionZ);
                MovingObjectPosition movingObjectPosition3 = worldObj.rayTraceBlocks(vec3D20, vec3D2);
                vec3D20 = Vec3D.createVector(posX, posY, posZ);
                vec3D2 = Vec3D.createVector(posX + motionX, posY + motionY, posZ + motionZ);
                if (movingObjectPosition3 != null)
                {
                    vec3D2 = Vec3D.createVector(movingObjectPosition3.hitVec.xCoord, movingObjectPosition3.hitVec.yCoord, movingObjectPosition3.hitVec.zCoord);
                }

                Entity entity4 = null;
                System.Collections.IList list5 = worldObj.getEntitiesWithinAABBExcludingEntity(this, boundingBox.addCoord(motionX, motionY, motionZ).expand(1.0D, 1.0D, 1.0D));
                double d6 = 0.0D;

                double d13;
                for (int i8 = 0; i8 < list5.Count; ++i8)
                {
                    Entity entity9 = (Entity)list5[i8];
                    if (entity9.canBeCollidedWith() && (entity9 != angler || ticksInAir >= 5))
                    {
                        float f10 = 0.3F;
                        AxisAlignedBB axisAlignedBB11 = entity9.boundingBox.expand((double)f10, (double)f10, (double)f10);
                        MovingObjectPosition movingObjectPosition12 = axisAlignedBB11.calculateIntercept(vec3D20, vec3D2);
                        if (movingObjectPosition12 != null)
                        {
                            d13 = vec3D20.distanceTo(movingObjectPosition12.hitVec);
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

                if (movingObjectPosition3 != null)
                {
                    if (movingObjectPosition3.entityHit != null)
                    {
                        if (movingObjectPosition3.entityHit.attackEntityFrom(DamageSource.causeThrownDamage(this, angler), 0))
                        {
                            bobber = movingObjectPosition3.entityHit;
                        }
                    }
                    else
                    {
                        inGround = true;
                    }
                }

                if (!inGround)
                {
                    moveEntity(motionX, motionY, motionZ);
                    float f24 = MathHelper.sqrt_double(motionX * motionX + motionZ * motionZ);
                    rotationYaw = (float)(Math.Atan2(motionX, motionZ) * 180.0D / (double)(float)Math.PI);

                    for (rotationPitch = (float)(Math.Atan2(motionY, (double)f24) * 180.0D / (double)(float)Math.PI); rotationPitch - prevRotationPitch < -180.0F; prevRotationPitch -= 360.0F)
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
                    float f25 = 0.92F;
                    if (onGround || isCollidedHorizontally)
                    {
                        f25 = 0.5F;
                    }

                    sbyte b26 = 5;
                    double d27 = 0.0D;

                    for (int i28 = 0; i28 < b26; ++i28)
                    {
                        double d14 = boundingBox.minY + (boundingBox.maxY - boundingBox.minY) * (i28 + 0) / b26 - 0.125D + 0.125D;
                        double d16 = boundingBox.minY + (boundingBox.maxY - boundingBox.minY) * (i28 + 1) / b26 - 0.125D + 0.125D;
                        AxisAlignedBB axisAlignedBB18 = AxisAlignedBB.getBoundingBoxFromPool(boundingBox.minX, d14, boundingBox.minZ, boundingBox.maxX, d16, boundingBox.maxZ);
                        if (worldObj.isAABBInMaterial(axisAlignedBB18, Material.water))
                        {
                            d27 += 1.0D / b26;
                        }
                    }

                    if (d27 > 0.0D)
                    {
                        if (ticksCatchable > 0)
                        {
                            --ticksCatchable;
                        }
                        else
                        {
                            short s29 = 500;
                            if (worldObj.canLightningStrikeAt(MathHelper.floor_double(posX), MathHelper.floor_double(posY) + 1, MathHelper.floor_double(posZ)))
                            {
                                s29 = 300;
                            }

                            if (rand.Next(s29) == 0)
                            {
                                ticksCatchable = rand.Next(30) + 10;
                                motionY -= 0.2F;
                                worldObj.playSoundAtEntity(this, "random.splash", 0.25F, 1.0F + (rand.NextSingle() - rand.NextSingle()) * 0.4F);
                                float f30 = MathHelper.floor_double(boundingBox.minY);

                                int i15;
                                float f17;
                                float f31;
                                for (i15 = 0; i15 < 1.0F + width * 20.0F; ++i15)
                                {
                                    f31 = (rand.NextSingle() * 2.0F - 1.0F) * width;
                                    f17 = (rand.NextSingle() * 2.0F - 1.0F) * width;
                                    worldObj.spawnParticle("bubble", posX + (double)f31, (double)(f30 + 1.0F), posZ + (double)f17, motionX, motionY - (double)(rand.NextSingle() * 0.2F), motionZ);
                                }

                                for (i15 = 0; i15 < 1.0F + width * 20.0F; ++i15)
                                {
                                    f31 = (rand.NextSingle() * 2.0F - 1.0F) * width;
                                    f17 = (rand.NextSingle() * 2.0F - 1.0F) * width;
                                    worldObj.spawnParticle("splash", posX + (double)f31, (double)(f30 + 1.0F), posZ + (double)f17, motionX, motionY, motionZ);
                                }
                            }
                        }
                    }

                    if (ticksCatchable > 0)
                    {
                        motionY -= (double)(rand.NextSingle() * rand.NextSingle() * rand.NextSingle()) * 0.2D;
                    }

                    d13 = d27 * 2.0D - 1.0D;
                    motionY += (double)0.04F * d13;
                    if (d27 > 0.0D)
                    {
                        f25 = (float)((double)f25 * 0.9D);
                        motionY *= 0.8D;
                    }

                    motionX *= f25;
                    motionY *= f25;
                    motionZ *= f25;
                    SetPosition(posX, posY, posZ);
                }
            }
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            nBTTagCompound1.setShort("xTile", (short)xTile);
            nBTTagCompound1.setShort("yTile", (short)yTile);
            nBTTagCompound1.setShort("zTile", (short)zTile);
            nBTTagCompound1.setByte("inTile", (sbyte)inTile);
            nBTTagCompound1.setByte("shake", (sbyte)shake);
            nBTTagCompound1.setByte("inGround", (sbyte)(inGround ? 1 : 0));
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            xTile = nBTTagCompound1.getShort("xTile");
            yTile = nBTTagCompound1.getShort("yTile");
            zTile = nBTTagCompound1.getShort("zTile");
            inTile = nBTTagCompound1.getByte("inTile") & 255;
            shake = nBTTagCompound1.getByte("shake") & 255;
            inGround = nBTTagCompound1.getByte("inGround") == 1;
        }

        public override float ShadowSize
        {
            get
            {
                return 0.0F;
            }
        }

        public virtual int catchFish()
        {
            sbyte b1 = 0;
            if (bobber != null)
            {
                double d2 = angler.posX - posX;
                double d4 = angler.posY - posY;
                double d6 = angler.posZ - posZ;
                double d8 = (double)MathHelper.sqrt_double(d2 * d2 + d4 * d4 + d6 * d6);
                double d10 = 0.1D;
                bobber.motionX += d2 * d10;
                bobber.motionY += d4 * d10 + (double)MathHelper.sqrt_double(d8) * 0.08D;
                bobber.motionZ += d6 * d10;
                b1 = 3;
            }
            else if (ticksCatchable > 0)
            {
                EntityItem entityItem13 = new EntityItem(worldObj, posX, posY, posZ, new ItemStack(Item.fishRaw));
                double d3 = angler.posX - posX;
                double d5 = angler.posY - posY;
                double d7 = angler.posZ - posZ;
                double d9 = (double)MathHelper.sqrt_double(d3 * d3 + d5 * d5 + d7 * d7);
                double d11 = 0.1D;
                entityItem13.motionX = d3 * d11;
                entityItem13.motionY = d5 * d11 + (double)MathHelper.sqrt_double(d9) * 0.08D;
                entityItem13.motionZ = d7 * d11;
                worldObj.spawnEntityInWorld(entityItem13);
                angler.addStat(StatList.fishCaughtStat, 1);
                b1 = 1;
            }

            if (inGround)
            {
                b1 = 2;
            }

            setDead();
            angler.fishEntity = null;
            return b1;
        }
    }

}