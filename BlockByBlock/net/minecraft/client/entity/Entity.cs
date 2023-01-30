using System;
using BlockByBlock.java_extensions;
using net.minecraft.src;

namespace net.minecraft.client.entity
{

    public abstract class Entity
    {
        private static int nextEntityID = 0;
        public int entityId = nextEntityID++;
        public double renderDistanceWeight = 1.0D;
        public bool preventEntitySpawning = false;
        public Entity riddenByEntity;
        public Entity ridingEntity;
        public World worldObj;
        public double prevPosX;
        public double prevPosY;
        public double prevPosZ;
        public double posX;
        public double posY;
        public double posZ;
        public double motionX;
        public double motionY;
        public double motionZ;
        public float rotationYaw;
        public float rotationPitch;
        public float prevRotationYaw;
        public float prevRotationPitch;
        public readonly AxisAlignedBB boundingBox = AxisAlignedBB.getBoundingBox(0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D);
        public bool onGround = false;
        public bool isCollidedHorizontally;
        public bool isCollidedVertically;
        public bool isCollided = false;
        public bool velocityChanged = false;
        protected internal bool isInWeb;
        public bool field_9293_aM = true;
        public bool isDead = false;
        public float yOffset = 0.0F;
        public float width = 0.6F;
        public float height = 1.8F;
        public float prevDistanceWalkedModified = 0.0F;
        public float distanceWalkedModified = 0.0F;
        public float fallDistance = 0.0F;
        private int nextStepDistance = 1;
        public double lastTickPosX;
        public double lastTickPosY;
        public double lastTickPosZ;
        public float ySize = 0.0F;
        public float stepHeight = 0.0F;
        public bool noClip = false;
        public float entityCollisionReduction = 0.0F;
        protected internal RandomExtended rand = new RandomExtended();
        public int ticksExisted = 0;
        public int fireResistance = 1;
        private int fire = 0;
        protected internal bool inWater = false;
        public int heartsLife = 0;
        private bool firstUpdate = true;
        public string skinUrl;
        public string cloakUrl;
        protected internal bool isImmuneToFire = false;
        protected internal DataWatcher dataWatcher = new DataWatcher();
        private double entityRiderPitchDelta;
        private double entityRiderYawDelta;
        public bool addedToChunk = false;
        public int chunkCoordX;
        public int chunkCoordY;
        public int chunkCoordZ;
        public int serverPosX;
        public int serverPosY;
        public int serverPosZ;
        public bool ignoreFrustumCheck;
        public bool isAirBorne;

        public Entity(World world1)
        {
            worldObj = world1;
            SetPosition(0.0D, 0.0D, 0.0D);
            dataWatcher.addObject(0, (sbyte)0);
            dataWatcher.addObject(1, (short)300);
            entityInit();
        }

        protected internal abstract void entityInit();

        public virtual DataWatcher DataWatcher
        {
            get
            {
                return dataWatcher;
            }
        }

        public override bool Equals(object object1)
        {
            return object1 is Entity ? ((Entity)object1).entityId == entityId : false;
        }

        public override int GetHashCode()
        {
            return entityId;
        }

        public virtual void preparePlayerToSpawn()
        {
            if (worldObj != null)
            {
                while (posY > 0.0D)
                {
                    SetPosition(posX, posY, posZ);
                    if (worldObj.getCollidingBoundingBoxes(this, boundingBox).Count == 0)
                    {
                        break;
                    }

                    ++posY;
                }

                motionX = motionY = motionZ = 0.0D;
                rotationPitch = 0.0F;
            }
        }

        public virtual void setDead()
        {
            isDead = true;
        }

        protected internal virtual void SetSize(float f1, float f2)
        {
            width = f1;
            height = f2;
        }

        protected internal virtual void setRotation(float f1, float f2)
        {
            rotationYaw = f1 % 360.0F;
            rotationPitch = f2 % 360.0F;
        }

        public virtual void SetPosition(double d1, double d3, double d5)
        {
            posX = d1;
            posY = d3;
            posZ = d5;
            float f7 = width / 2.0F;
            float f8 = height;
            boundingBox.setBounds(d1 - (double)f7, d3 - yOffset + ySize, d5 - (double)f7, d1 + (double)f7, d3 - yOffset + ySize + (double)f8, d5 + (double)f7);
        }

        public virtual void setAngles(float f1, float f2)
        {
            float f3 = rotationPitch;
            float f4 = rotationYaw;
            rotationYaw = (float)(rotationYaw + (double)f1 * 0.15D);
            rotationPitch = (float)(rotationPitch - (double)f2 * 0.15D);
            if (rotationPitch < -90.0F)
            {
                rotationPitch = -90.0F;
            }

            if (rotationPitch > 90.0F)
            {
                rotationPitch = 90.0F;
            }

            prevRotationPitch += rotationPitch - f3;
            prevRotationYaw += rotationYaw - f4;
        }

        public virtual void onUpdate()
        {
            onEntityUpdate();
        }

        public virtual void onEntityUpdate()
        {
            Profiler.startSection("entityBaseTick");
            if (ridingEntity != null && ridingEntity.isDead)
            {
                ridingEntity = null;
            }

            ++ticksExisted;
            prevDistanceWalkedModified = distanceWalkedModified;
            prevPosX = posX;
            prevPosY = posY;
            prevPosZ = posZ;
            prevRotationPitch = rotationPitch;
            prevRotationYaw = rotationYaw;
            int i3;
            if (Sprinting && !InWater)
            {
                int i1 = MathHelper.floor_double(posX);
                int i2 = MathHelper.floor_double(posY - (double)0.2F - yOffset);
                i3 = MathHelper.floor_double(posZ);
                int i4 = worldObj.getBlockId(i1, i2, i3);
                if (i4 > 0)
                {
                    worldObj.spawnParticle("tilecrack_" + i4, posX + ((double)rand.NextSingle() - 0.5D) * width, boundingBox.minY + 0.1D, posZ + ((double)rand.NextSingle() - 0.5D) * width, -motionX * 4.0D, 1.5D, -motionZ * 4.0D);
                }
            }

            if (handleWaterMovement())
            {
                if (!inWater && !firstUpdate)
                {
                    float f6 = MathHelper.sqrt_double(motionX * motionX * (double)0.2F + motionY * motionY + motionZ * motionZ * (double)0.2F) * 0.2F;
                    if (f6 > 1.0F)
                    {
                        f6 = 1.0F;
                    }

                    worldObj.playSoundAtEntity(this, "random.splash", f6, 1.0F + (rand.NextSingle() - rand.NextSingle()) * 0.4F);
                    float f7 = MathHelper.floor_double(boundingBox.minY);

                    float f5;
                    float f8;
                    for (i3 = 0; i3 < 1.0F + width * 20.0F; ++i3)
                    {
                        f8 = (rand.NextSingle() * 2.0F - 1.0F) * width;
                        f5 = (rand.NextSingle() * 2.0F - 1.0F) * width;
                        worldObj.spawnParticle("bubble", posX + (double)f8, (double)(f7 + 1.0F), posZ + (double)f5, motionX, motionY - (double)(rand.NextSingle() * 0.2F), motionZ);
                    }

                    for (i3 = 0; i3 < 1.0F + width * 20.0F; ++i3)
                    {
                        f8 = (rand.NextSingle() * 2.0F - 1.0F) * width;
                        f5 = (rand.NextSingle() * 2.0F - 1.0F) * width;
                        worldObj.spawnParticle("splash", posX + (double)f8, (double)(f7 + 1.0F), posZ + (double)f5, motionX, motionY, motionZ);
                    }
                }

                fallDistance = 0.0F;
                inWater = true;
                fire = 0;
            }
            else
            {
                inWater = false;
            }

            if (worldObj.isRemote)
            {
                fire = 0;
            }
            else if (fire > 0)
            {
                if (isImmuneToFire)
                {
                    fire -= 4;
                    if (fire < 0)
                    {
                        fire = 0;
                    }
                }
                else
                {
                    if (fire % 20 == 0)
                    {
                        attackEntityFrom(DamageSource.onFire, 1);
                    }

                    --fire;
                }
            }

            if (handleLavaMovement())
            {
                setOnFireFromLava();
                fallDistance *= 0.5F;
            }

            if (posY < -64.0D)
            {
                kill();
            }

            if (!worldObj.isRemote)
            {
                setFlag(0, fire > 0);
                setFlag(2, ridingEntity != null);
            }

            firstUpdate = false;
            Profiler.endSection();
        }

        protected internal virtual void setOnFireFromLava()
        {
            if (!isImmuneToFire)
            {
                attackEntityFrom(DamageSource.lava, 4);
                Fire = 15;
            }

        }

        public virtual int Fire
        {
            set
            {
                int i2 = value * 20;
                if (fire < i2)
                {
                    fire = i2;
                }

            }
        }

        public virtual void extinguish()
        {
            fire = 0;
        }

        protected internal virtual void kill()
        {
            setDead();
        }

        public virtual bool isOffsetPositionInLiquid(double d1, double d3, double d5)
        {
            AxisAlignedBB axisAlignedBB7 = boundingBox.getOffsetBoundingBox(d1, d3, d5);
            System.Collections.IList list8 = worldObj.getCollidingBoundingBoxes(this, axisAlignedBB7);
            return list8.Count > 0 ? false : !worldObj.isAnyLiquid(axisAlignedBB7);
        }

        public virtual void moveEntity(double d1, double d3, double d5)
        {
            if (noClip)
            {
                boundingBox.offset(d1, d3, d5);
                posX = (boundingBox.minX + boundingBox.maxX) / 2.0D;
                posY = boundingBox.minY + yOffset - ySize;
                posZ = (boundingBox.minZ + boundingBox.maxZ) / 2.0D;
            }
            else
            {
                Profiler.startSection("move");
                ySize *= 0.4F;
                double d7 = posX;
                double d9 = posZ;
                if (isInWeb)
                {
                    isInWeb = false;
                    d1 *= 0.25D;
                    d3 *= (double)0.05F;
                    d5 *= 0.25D;
                    motionX = 0.0D;
                    motionY = 0.0D;
                    motionZ = 0.0D;
                }

                double d11 = d1;
                double d13 = d3;
                double d15 = d5;
                AxisAlignedBB axisAlignedBB17 = boundingBox.copy();
                bool z18 = onGround && Sneaking && this is EntityPlayer;
                if (z18)
                {
                    double d19;
                    for (d19 = 0.05D; d1 != 0.0D && worldObj.getCollidingBoundingBoxes(this, boundingBox.getOffsetBoundingBox(d1, -1.0D, 0.0D)).Count == 0; d11 = d1)
                    {
                        if (d1 < d19 && d1 >= -d19)
                        {
                            d1 = 0.0D;
                        }
                        else if (d1 > 0.0D)
                        {
                            d1 -= d19;
                        }
                        else
                        {
                            d1 += d19;
                        }
                    }

                    for (; d5 != 0.0D && worldObj.getCollidingBoundingBoxes(this, boundingBox.getOffsetBoundingBox(0.0D, -1.0D, d5)).Count == 0; d15 = d5)
                    {
                        if (d5 < d19 && d5 >= -d19)
                        {
                            d5 = 0.0D;
                        }
                        else if (d5 > 0.0D)
                        {
                            d5 -= d19;
                        }
                        else
                        {
                            d5 += d19;
                        }
                    }

                    while (d1 != 0.0D && d5 != 0.0D && worldObj.getCollidingBoundingBoxes(this, boundingBox.getOffsetBoundingBox(d1, -1.0D, d5)).Count == 0)
                    {
                        if (d1 < d19 && d1 >= -d19)
                        {
                            d1 = 0.0D;
                        }
                        else if (d1 > 0.0D)
                        {
                            d1 -= d19;
                        }
                        else
                        {
                            d1 += d19;
                        }

                        if (d5 < d19 && d5 >= -d19)
                        {
                            d5 = 0.0D;
                        }
                        else if (d5 > 0.0D)
                        {
                            d5 -= d19;
                        }
                        else
                        {
                            d5 += d19;
                        }

                        d11 = d1;
                        d15 = d5;
                    }
                }

                System.Collections.IList list35 = worldObj.getCollidingBoundingBoxes(this, boundingBox.addCoord(d1, d3, d5));

                for (int i20 = 0; i20 < list35.Count; ++i20)
                {
                    d3 = ((AxisAlignedBB)list35[i20]).calculateYOffset(boundingBox, d3);
                }

                boundingBox.offset(0.0D, d3, 0.0D);
                if (!field_9293_aM && d13 != d3)
                {
                    d5 = 0.0D;
                    d3 = 0.0D;
                    d1 = 0.0D;
                }

                bool z36 = onGround || d13 != d3 && d13 < 0.0D;

                int i21;
                for (i21 = 0; i21 < list35.Count; ++i21)
                {
                    d1 = ((AxisAlignedBB)list35[i21]).calculateXOffset(boundingBox, d1);
                }

                boundingBox.offset(d1, 0.0D, 0.0D);
                if (!field_9293_aM && d11 != d1)
                {
                    d5 = 0.0D;
                    d3 = 0.0D;
                    d1 = 0.0D;
                }

                for (i21 = 0; i21 < list35.Count; ++i21)
                {
                    d5 = ((AxisAlignedBB)list35[i21]).calculateZOffset(boundingBox, d5);
                }

                boundingBox.offset(0.0D, 0.0D, d5);
                if (!field_9293_aM && d15 != d5)
                {
                    d5 = 0.0D;
                    d3 = 0.0D;
                    d1 = 0.0D;
                }

                double d23;
                int i28;
                double d37;
                if (stepHeight > 0.0F && z36 && (z18 || ySize < 0.05F) && (d11 != d1 || d15 != d5))
                {
                    d37 = d1;
                    d23 = d3;
                    double d25 = d5;
                    d1 = d11;
                    d3 = stepHeight;
                    d5 = d15;
                    AxisAlignedBB axisAlignedBB27 = boundingBox.copy();
                    boundingBox.BB = axisAlignedBB17;
                    list35 = worldObj.getCollidingBoundingBoxes(this, boundingBox.addCoord(d11, d3, d15));

                    for (i28 = 0; i28 < list35.Count; ++i28)
                    {
                        d3 = ((AxisAlignedBB)list35[i28]).calculateYOffset(boundingBox, d3);
                    }

                    boundingBox.offset(0.0D, d3, 0.0D);
                    if (!field_9293_aM && d13 != d3)
                    {
                        d5 = 0.0D;
                        d3 = 0.0D;
                        d1 = 0.0D;
                    }

                    for (i28 = 0; i28 < list35.Count; ++i28)
                    {
                        d1 = ((AxisAlignedBB)list35[i28]).calculateXOffset(boundingBox, d1);
                    }

                    boundingBox.offset(d1, 0.0D, 0.0D);
                    if (!field_9293_aM && d11 != d1)
                    {
                        d5 = 0.0D;
                        d3 = 0.0D;
                        d1 = 0.0D;
                    }

                    for (i28 = 0; i28 < list35.Count; ++i28)
                    {
                        d5 = ((AxisAlignedBB)list35[i28]).calculateZOffset(boundingBox, d5);
                    }

                    boundingBox.offset(0.0D, 0.0D, d5);
                    if (!field_9293_aM && d15 != d5)
                    {
                        d5 = 0.0D;
                        d3 = 0.0D;
                        d1 = 0.0D;
                    }

                    if (!field_9293_aM && d13 != d3)
                    {
                        d5 = 0.0D;
                        d3 = 0.0D;
                        d1 = 0.0D;
                    }
                    else
                    {
                        d3 = (double)-stepHeight;

                        for (i28 = 0; i28 < list35.Count; ++i28)
                        {
                            d3 = ((AxisAlignedBB)list35[i28]).calculateYOffset(boundingBox, d3);
                        }

                        boundingBox.offset(0.0D, d3, 0.0D);
                    }

                    if (d37 * d37 + d25 * d25 >= d1 * d1 + d5 * d5)
                    {
                        d1 = d37;
                        d3 = d23;
                        d5 = d25;
                        boundingBox.BB = axisAlignedBB27;
                    }
                    else
                    {
                        double d40 = boundingBox.minY - (int)boundingBox.minY;
                        if (d40 > 0.0D)
                        {
                            ySize = (float)(ySize + d40 + 0.01D);
                        }
                    }
                }

                Profiler.endSection();
                Profiler.startSection("rest");
                posX = (boundingBox.minX + boundingBox.maxX) / 2.0D;
                posY = boundingBox.minY + yOffset - ySize;
                posZ = (boundingBox.minZ + boundingBox.maxZ) / 2.0D;
                isCollidedHorizontally = d11 != d1 || d15 != d5;
                isCollidedVertically = d13 != d3;
                onGround = d13 != d3 && d13 < 0.0D;
                isCollided = isCollidedHorizontally || isCollidedVertically;
                updateFallState(d3, onGround);
                if (d11 != d1)
                {
                    motionX = 0.0D;
                }

                if (d13 != d3)
                {
                    motionY = 0.0D;
                }

                if (d15 != d5)
                {
                    motionZ = 0.0D;
                }

                d37 = posX - d7;
                d23 = posZ - d9;
                int i26;
                int i38;
                int i39;
                if (canTriggerWalking() && !z18 && ridingEntity == null)
                {
                    distanceWalkedModified = (float)(distanceWalkedModified + (double)MathHelper.sqrt_double(d37 * d37 + d23 * d23) * 0.6D);
                    i38 = MathHelper.floor_double(posX);
                    i26 = MathHelper.floor_double(posY - (double)0.2F - yOffset);
                    i39 = MathHelper.floor_double(posZ);
                    i28 = worldObj.getBlockId(i38, i26, i39);
                    if (i28 == 0 && worldObj.getBlockId(i38, i26 - 1, i39) == Block.fence.blockID)
                    {
                        i28 = worldObj.getBlockId(i38, i26 - 1, i39);
                    }

                    if (distanceWalkedModified > nextStepDistance && i28 > 0)
                    {
                        nextStepDistance = (int)distanceWalkedModified + 1;
                        playStepSound(i38, i26, i39, i28);
                        Block.blocksList[i28].onEntityWalking(worldObj, i38, i26, i39, this);
                    }
                }

                i38 = MathHelper.floor_double(boundingBox.minX + 0.001D);
                i26 = MathHelper.floor_double(boundingBox.minY + 0.001D);
                i39 = MathHelper.floor_double(boundingBox.minZ + 0.001D);
                i28 = MathHelper.floor_double(boundingBox.maxX - 0.001D);
                int i29 = MathHelper.floor_double(boundingBox.maxY - 0.001D);
                int i30 = MathHelper.floor_double(boundingBox.maxZ - 0.001D);
                if (worldObj.checkChunksExist(i38, i26, i39, i28, i29, i30))
                {
                    for (int i31 = i38; i31 <= i28; ++i31)
                    {
                        for (int i32 = i26; i32 <= i29; ++i32)
                        {
                            for (int i33 = i39; i33 <= i30; ++i33)
                            {
                                int i34 = worldObj.getBlockId(i31, i32, i33);
                                if (i34 > 0)
                                {
                                    Block.blocksList[i34].onEntityCollidedWithBlock(worldObj, i31, i32, i33, this);
                                }
                            }
                        }
                    }
                }

                bool z41 = Wet;
                if (worldObj.isBoundingBoxBurning(boundingBox.contract(0.001D, 0.001D, 0.001D)))
                {
                    dealFireDamage(1);
                    if (!z41)
                    {
                        ++fire;
                        if (fire == 0)
                        {
                            Fire = 8;
                        }
                    }
                }
                else if (fire <= 0)
                {
                    fire = -fireResistance;
                }

                if (z41 && fire > 0)
                {
                    worldObj.playSoundAtEntity(this, "random.fizz", 0.7F, 1.6F + (rand.NextSingle() - rand.NextSingle()) * 0.4F);
                    fire = -fireResistance;
                }

                Profiler.endSection();
            }
        }

        protected internal virtual void playStepSound(int i1, int i2, int i3, int i4)
        {
            StepSound stepSound5 = Block.blocksList[i4].stepSound;
            if (worldObj.getBlockId(i1, i2 + 1, i3) == Block.snow.blockID)
            {
                stepSound5 = Block.snow.stepSound;
                worldObj.playSoundAtEntity(this, stepSound5.StepSoundName, stepSound5.Volume * 0.15F, stepSound5.Pitch);
            }
            else if (!Block.blocksList[i4].blockMaterial.Liquid)
            {
                worldObj.playSoundAtEntity(this, stepSound5.StepSoundName, stepSound5.Volume * 0.15F, stepSound5.Pitch);
            }

        }

        protected internal virtual bool canTriggerWalking()
        {
            return true;
        }

        protected internal virtual void updateFallState(double d1, bool z3)
        {
            if (z3)
            {
                if (fallDistance > 0.0F)
                {
                    if (this is EntityLiving)
                    {
                        int i4 = MathHelper.floor_double(posX);
                        int i5 = MathHelper.floor_double(posY - (double)0.2F - yOffset);
                        int i6 = MathHelper.floor_double(posZ);
                        int i7 = worldObj.getBlockId(i4, i5, i6);
                        if (i7 == 0 && worldObj.getBlockId(i4, i5 - 1, i6) == Block.fence.blockID)
                        {
                            i7 = worldObj.getBlockId(i4, i5 - 1, i6);
                        }

                        if (i7 > 0)
                        {
                            Block.blocksList[i7].onFallenUpon(worldObj, i4, i5, i6, this, fallDistance);
                        }
                    }

                    fall(fallDistance);
                    fallDistance = 0.0F;
                }
            }
            else if (d1 < 0.0D)
            {
                fallDistance = (float)(fallDistance - d1);
            }

        }

        public virtual AxisAlignedBB BoundingBox
        {
            get
            {
                return null;
            }
        }

        protected internal virtual void dealFireDamage(int i1)
        {
            if (!isImmuneToFire)
            {
                attackEntityFrom(DamageSource.inFire, i1);
            }

        }

        public bool ImmuneToFire
        {
            get
            {
                return isImmuneToFire;
            }
        }

        protected internal virtual void fall(float f1)
        {
            if (riddenByEntity != null)
            {
                riddenByEntity.fall(f1);
            }

        }

        public virtual bool Wet
        {
            get
            {
                return inWater || worldObj.canLightningStrikeAt(MathHelper.floor_double(posX), MathHelper.floor_double(posY), MathHelper.floor_double(posZ));
            }
        }

        public virtual bool InWater
        {
            get
            {
                return inWater;
            }
        }

        public virtual bool handleWaterMovement()
        {
            return worldObj.handleMaterialAcceleration(boundingBox.expand(0.0D, -0.4000000059604645D, 0.0D).contract(0.001D, 0.001D, 0.001D), Material.water, this);
        }

        public virtual bool isInsideOfMaterial(Material material1)
        {
            double d2 = posY + (double)EyeHeight;
            int i4 = MathHelper.floor_double(posX);
            int i5 = MathHelper.floor_float(MathHelper.floor_double(d2));
            int i6 = MathHelper.floor_double(posZ);
            int i7 = worldObj.getBlockId(i4, i5, i6);
            if (i7 != 0 && Block.blocksList[i7].blockMaterial == material1)
            {
                float f8 = BlockFluid.getFluidHeightPercent(worldObj.getBlockMetadata(i4, i5, i6)) - 0.11111111F;
                float f9 = i5 + 1 - f8;
                return d2 < (double)f9;
            }
            else
            {
                return false;
            }
        }

        public virtual float EyeHeight
        {
            get
            {
                return 0.0F;
            }
        }

        public virtual bool handleLavaMovement()
        {
            return worldObj.isMaterialInBB(boundingBox.expand(-0.10000000149011612D, -0.4000000059604645D, -0.10000000149011612D), Material.lava);
        }

        public virtual void moveFlying(float f1, float f2, float f3)
        {
            float f4 = MathHelper.sqrt_float(f1 * f1 + f2 * f2);
            if (f4 >= 0.01F)
            {
                if (f4 < 1.0F)
                {
                    f4 = 1.0F;
                }

                f4 = f3 / f4;
                f1 *= f4;
                f2 *= f4;
                float f5 = MathHelper.sin(rotationYaw * (float)Math.PI / 180.0F);
                float f6 = MathHelper.cos(rotationYaw * (float)Math.PI / 180.0F);
                motionX += f1 * f6 - f2 * f5;
                motionZ += f2 * f6 + f1 * f5;
            }
        }

        public virtual int getBrightnessForRender(float f1)
        {
            int i2 = MathHelper.floor_double(posX);
            int i3 = MathHelper.floor_double(posZ);
            if (worldObj.blockExists(i2, 0, i3))
            {
                double d4 = (boundingBox.maxY - boundingBox.minY) * 0.66D;
                int i6 = MathHelper.floor_double(posY - yOffset + d4);
                return worldObj.GetLightBrightnessForSkyBlocks(i2, i6, i3, 0);
            }
            else
            {
                return 0;
            }
        }

        public virtual float getBrightness(float f1)
        {
            int i2 = MathHelper.floor_double(posX);
            int i3 = MathHelper.floor_double(posZ);
            if (worldObj.blockExists(i2, 0, i3))
            {
                double d4 = (boundingBox.maxY - boundingBox.minY) * 0.66D;
                int i6 = MathHelper.floor_double(posY - yOffset + d4);
                return worldObj.getLightBrightness(i2, i6, i3);
            }
            else
            {
                return 0.0F;
            }
        }

        public virtual World World
        {
            set
            {
                worldObj = value;
            }
        }

        public virtual void setPositionAndRotation(double d1, double d3, double d5, float f7, float f8)
        {
            prevPosX = posX = d1;
            prevPosY = posY = d3;
            prevPosZ = posZ = d5;
            prevRotationYaw = rotationYaw = f7;
            prevRotationPitch = rotationPitch = f8;
            ySize = 0.0F;
            double d9 = (double)(prevRotationYaw - f7);
            if (d9 < -180.0D)
            {
                prevRotationYaw += 360.0F;
            }

            if (d9 >= 180.0D)
            {
                prevRotationYaw -= 360.0F;
            }

            SetPosition(posX, posY, posZ);
            setRotation(f7, f8);
        }

        public virtual void setLocationAndAngles(double d1, double d3, double d5, float f7, float f8)
        {
            lastTickPosX = prevPosX = posX = d1;
            lastTickPosY = prevPosY = posY = d3 + yOffset;
            lastTickPosZ = prevPosZ = posZ = d5;
            rotationYaw = f7;
            rotationPitch = f8;
            SetPosition(posX, posY, posZ);
        }

        public virtual float getDistanceToEntity(Entity entity1)
        {
            float f2 = (float)(posX - entity1.posX);
            float f3 = (float)(posY - entity1.posY);
            float f4 = (float)(posZ - entity1.posZ);
            return MathHelper.sqrt_float(f2 * f2 + f3 * f3 + f4 * f4);
        }

        public virtual double getDistanceSq(double d1, double d3, double d5)
        {
            double d7 = posX - d1;
            double d9 = posY - d3;
            double d11 = posZ - d5;
            return d7 * d7 + d9 * d9 + d11 * d11;
        }

        public virtual double getDistance(double d1, double d3, double d5)
        {
            double d7 = posX - d1;
            double d9 = posY - d3;
            double d11 = posZ - d5;
            return (double)MathHelper.sqrt_double(d7 * d7 + d9 * d9 + d11 * d11);
        }

        public virtual double getDistanceSqToEntity(Entity entity1)
        {
            double d2 = posX - entity1.posX;
            double d4 = posY - entity1.posY;
            double d6 = posZ - entity1.posZ;
            return d2 * d2 + d4 * d4 + d6 * d6;
        }

        public virtual void onCollideWithPlayer(EntityPlayer entityPlayer1)
        {
        }

        public virtual void applyEntityCollision(Entity entity1)
        {
            if (entity1.riddenByEntity != this && entity1.ridingEntity != this)
            {
                double d2 = entity1.posX - posX;
                double d4 = entity1.posZ - posZ;
                double d6 = MathHelper.abs_max(d2, d4);
                if (d6 >= (double)0.01F)
                {
                    d6 = (double)MathHelper.sqrt_double(d6);
                    d2 /= d6;
                    d4 /= d6;
                    double d8 = 1.0D / d6;
                    if (d8 > 1.0D)
                    {
                        d8 = 1.0D;
                    }

                    d2 *= d8;
                    d4 *= d8;
                    d2 *= (double)0.05F;
                    d4 *= (double)0.05F;
                    d2 *= (double)(1.0F - entityCollisionReduction);
                    d4 *= (double)(1.0F - entityCollisionReduction);
                    addVelocity(-d2, 0.0D, -d4);
                    entity1.addVelocity(d2, 0.0D, d4);
                }

            }
        }

        public virtual void addVelocity(double d1, double d3, double d5)
        {
            motionX += d1;
            motionY += d3;
            motionZ += d5;
            isAirBorne = true;
        }

        protected internal virtual void setBeenAttacked()
        {
            velocityChanged = true;
        }

        public virtual bool attackEntityFrom(DamageSource damageSource1, int i2)
        {
            setBeenAttacked();
            return false;
        }

        public virtual bool canBeCollidedWith()
        {
            return false;
        }

        public virtual bool canBePushed()
        {
            return false;
        }

        public virtual void addToPlayerScore(Entity entity1, int i2)
        {
        }

        public virtual bool isInRangeToRenderVec3D(Vec3D vec3D1)
        {
            double d2 = posX - vec3D1.xCoord;
            double d4 = posY - vec3D1.yCoord;
            double d6 = posZ - vec3D1.zCoord;
            double d8 = d2 * d2 + d4 * d4 + d6 * d6;
            return isInRangeToRenderDist(d8);
        }

        public virtual bool isInRangeToRenderDist(double d1)
        {
            double d3 = boundingBox.AverageEdgeLength;
            d3 *= 64.0D * renderDistanceWeight;
            return d1 < d3 * d3;
        }

        public virtual string Texture
        {
            get
            {
                return null;
            }
        }

        public virtual bool addEntityID(NBTTagCompound nBTTagCompound1)
        {
            string string2 = EntityString;
            if (!isDead && !ReferenceEquals(string2, null))
            {
                nBTTagCompound1.setString("id", string2);
                writeToNBT(nBTTagCompound1);
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void writeToNBT(NBTTagCompound nBTTagCompound1)
        {
            nBTTagCompound1.setTag("Pos", newDoubleNBTList(new double[] { posX, posY + ySize, posZ }));
            nBTTagCompound1.setTag("Motion", newDoubleNBTList(new double[] { motionX, motionY, motionZ }));
            nBTTagCompound1.setTag("Rotation", newFloatNBTList(new float[] { rotationYaw, rotationPitch }));
            nBTTagCompound1.setFloat("FallDistance", fallDistance);
            nBTTagCompound1.setShort("Fire", (short)fire);
            nBTTagCompound1.setShort("Air", (short)Air);
            nBTTagCompound1.setBoolean("OnGround", onGround);
            writeEntityToNBT(nBTTagCompound1);
        }

        public virtual void readFromNBT(NBTTagCompound nBTTagCompound1)
        {
            NBTTagList nBTTagList2 = nBTTagCompound1.getTagList("Pos");
            NBTTagList nBTTagList3 = nBTTagCompound1.getTagList("Motion");
            NBTTagList nBTTagList4 = nBTTagCompound1.getTagList("Rotation");
            motionX = ((NBTTagDouble)nBTTagList3.tagAt(0)).data;
            motionY = ((NBTTagDouble)nBTTagList3.tagAt(1)).data;
            motionZ = ((NBTTagDouble)nBTTagList3.tagAt(2)).data;
            if (Math.Abs(motionX) > 10.0D)
            {
                motionX = 0.0D;
            }

            if (Math.Abs(motionY) > 10.0D)
            {
                motionY = 0.0D;
            }

            if (Math.Abs(motionZ) > 10.0D)
            {
                motionZ = 0.0D;
            }

            prevPosX = lastTickPosX = posX = ((NBTTagDouble)nBTTagList2.tagAt(0)).data;
            prevPosY = lastTickPosY = posY = ((NBTTagDouble)nBTTagList2.tagAt(1)).data;
            prevPosZ = lastTickPosZ = posZ = ((NBTTagDouble)nBTTagList2.tagAt(2)).data;
            prevRotationYaw = rotationYaw = ((NBTTagFloat)nBTTagList4.tagAt(0)).data;
            prevRotationPitch = rotationPitch = ((NBTTagFloat)nBTTagList4.tagAt(1)).data;
            fallDistance = nBTTagCompound1.getFloat("FallDistance");
            fire = nBTTagCompound1.getShort("Fire");
            Air = nBTTagCompound1.getShort("Air");
            onGround = nBTTagCompound1.getBoolean("OnGround");
            SetPosition(posX, posY, posZ);
            setRotation(rotationYaw, rotationPitch);
            readEntityFromNBT(nBTTagCompound1);
        }

        protected internal string EntityString
        {
            get
            {
                return EntityList.getEntityString(this);
            }
        }

        public abstract void readEntityFromNBT(NBTTagCompound nBTTagCompound1);

        public abstract void writeEntityToNBT(NBTTagCompound nBTTagCompound1);

        protected internal virtual NBTTagList newDoubleNBTList(params double[] d1)
        {
            NBTTagList nBTTagList2 = new NBTTagList();
            double[] d3 = d1;
            int i4 = d1.Length;

            for (int i5 = 0; i5 < i4; ++i5)
            {
                double d6 = d3[i5];
                nBTTagList2.appendTag(new NBTTagDouble(null, d6));
            }

            return nBTTagList2;
        }

        protected internal virtual NBTTagList newFloatNBTList(params float[] f1)
        {
            NBTTagList nBTTagList2 = new NBTTagList();
            float[] f3 = f1;
            int i4 = f1.Length;

            for (int i5 = 0; i5 < i4; ++i5)
            {
                float f6 = f3[i5];
                nBTTagList2.appendTag(new NBTTagFloat(null, f6));
            }

            return nBTTagList2;
        }

        public virtual float ShadowSize
        {
            get
            {
                return height / 2.0F;
            }
        }

        public virtual EntityItem dropItem(int i1, int i2)
        {
            return dropItemWithOffset(i1, i2, 0.0F);
        }

        public virtual EntityItem dropItemWithOffset(int i1, int i2, float f3)
        {
            return entityDropItem(new ItemStack(i1, i2, 0), f3);
        }

        public virtual EntityItem entityDropItem(ItemStack itemStack1, float f2)
        {
            EntityItem entityItem3 = new EntityItem(worldObj, posX, posY + (double)f2, posZ, itemStack1);
            entityItem3.delayBeforeCanPickup = 10;
            worldObj.spawnEntityInWorld(entityItem3);
            return entityItem3;
        }

        public virtual bool EntityAlive
        {
            get
            {
                return !isDead;
            }
        }

        public virtual bool EntityInsideOpaqueBlock
        {
            get
            {
                for (int i1 = 0; i1 < 8; ++i1)
                {
                    float f2 = ((i1 >> 0) % 2 - 0.5F) * width * 0.8F;
                    float f3 = ((i1 >> 1) % 2 - 0.5F) * 0.1F;
                    float f4 = ((i1 >> 2) % 2 - 0.5F) * width * 0.8F;
                    int i5 = MathHelper.floor_double(posX + (double)f2);
                    int i6 = MathHelper.floor_double(posY + (double)EyeHeight + (double)f3);
                    int i7 = MathHelper.floor_double(posZ + (double)f4);
                    if (worldObj.isBlockNormalCube(i5, i6, i7))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public virtual bool interact(EntityPlayer entityPlayer1)
        {
            return false;
        }

        public virtual AxisAlignedBB getCollisionBox(Entity entity1)
        {
            return null;
        }

        public virtual void updateRidden()
        {
            if (ridingEntity.isDead)
            {
                ridingEntity = null;
            }
            else
            {
                motionX = 0.0D;
                motionY = 0.0D;
                motionZ = 0.0D;
                onUpdate();
                if (ridingEntity != null)
                {
                    ridingEntity.updateRiderPosition();
                    entityRiderYawDelta += ridingEntity.rotationYaw - ridingEntity.prevRotationYaw;

                    for (entityRiderPitchDelta += ridingEntity.rotationPitch - ridingEntity.prevRotationPitch; entityRiderYawDelta >= 180.0D; entityRiderYawDelta -= 360.0D)
                    {
                    }

                    while (entityRiderYawDelta < -180.0D)
                    {
                        entityRiderYawDelta += 360.0D;
                    }

                    while (entityRiderPitchDelta >= 180.0D)
                    {
                        entityRiderPitchDelta -= 360.0D;
                    }

                    while (entityRiderPitchDelta < -180.0D)
                    {
                        entityRiderPitchDelta += 360.0D;
                    }

                    double d1 = entityRiderYawDelta * 0.5D;
                    double d3 = entityRiderPitchDelta * 0.5D;
                    float f5 = 10.0F;
                    if (d1 > (double)f5)
                    {
                        d1 = (double)f5;
                    }

                    if (d1 < (double)-f5)
                    {
                        d1 = (double)-f5;
                    }

                    if (d3 > (double)f5)
                    {
                        d3 = (double)f5;
                    }

                    if (d3 < (double)-f5)
                    {
                        d3 = (double)-f5;
                    }

                    entityRiderYawDelta -= d1;
                    entityRiderPitchDelta -= d3;
                    rotationYaw = (float)(rotationYaw + d1);
                    rotationPitch = (float)(rotationPitch + d3);
                }
            }
        }

        public virtual void updateRiderPosition()
        {
            riddenByEntity.SetPosition(posX, posY + MountedYOffset + riddenByEntity.YOffset, posZ);
        }

        public virtual double YOffset
        {
            get
            {
                return yOffset;
            }
        }

        public virtual double MountedYOffset
        {
            get
            {
                return height * 0.75D;
            }
        }

        public virtual void mountEntity(Entity entity1)
        {
            entityRiderPitchDelta = 0.0D;
            entityRiderYawDelta = 0.0D;
            if (entity1 == null)
            {
                if (ridingEntity != null)
                {
                    setLocationAndAngles(ridingEntity.posX, ridingEntity.boundingBox.minY + ridingEntity.height, ridingEntity.posZ, rotationYaw, rotationPitch);
                    ridingEntity.riddenByEntity = null;
                }

                ridingEntity = null;
            }
            else if (ridingEntity == entity1)
            {
                ridingEntity.riddenByEntity = null;
                ridingEntity = null;
                setLocationAndAngles(entity1.posX, entity1.boundingBox.minY + entity1.height, entity1.posZ, rotationYaw, rotationPitch);
            }
            else
            {
                if (ridingEntity != null)
                {
                    ridingEntity.riddenByEntity = null;
                }

                if (entity1.riddenByEntity != null)
                {
                    entity1.riddenByEntity.ridingEntity = null;
                }

                ridingEntity = entity1;
                entity1.riddenByEntity = this;
            }
        }

        public virtual void setPositionAndRotation2(double d1, double d3, double d5, float f7, float f8, int i9)
        {
            SetPosition(d1, d3, d5);
            setRotation(f7, f8);
            System.Collections.IList list10 = worldObj.getCollidingBoundingBoxes(this, boundingBox.contract(8.0D / 256D, 0.0D, 8.0D / 256D));
            if (list10.Count > 0)
            {
                double d11 = 0.0D;

                for (int i13 = 0; i13 < list10.Count; ++i13)
                {
                    AxisAlignedBB axisAlignedBB14 = (AxisAlignedBB)list10[i13];
                    if (axisAlignedBB14.maxY > d11)
                    {
                        d11 = axisAlignedBB14.maxY;
                    }
                }

                d3 += d11 - boundingBox.minY;
                SetPosition(d1, d3, d5);
            }

        }

        public virtual float CollisionBorderSize
        {
            get
            {
                return 0.1F;
            }
        }

        public virtual Vec3D LookVec
        {
            get
            {
                return null;
            }
        }

        public virtual void setInPortal()
        {
        }

        public virtual void setVelocity(double d1, double d3, double d5)
        {
            motionX = d1;
            motionY = d3;
            motionZ = d5;
        }

        public virtual void handleHealthUpdate(sbyte b1)
        {
        }

        public virtual void performHurtAnimation()
        {
        }

        public virtual void updateCloak()
        {
        }

        public virtual void outfitWithItem(int i1, int i2, int i3)
        {
        }

        public virtual bool Burning
        {
            get
            {
                return fire > 0 || getFlag(0);
            }
        }

        public virtual bool Riding
        {
            get
            {
                return ridingEntity != null || getFlag(2);
            }
        }

        public virtual bool Sneaking
        {
            get
            {
                return getFlag(1);
            }
            set
            {
                setFlag(1, value);
            }
        }


        public virtual bool Sprinting
        {
            get
            {
                return getFlag(3);
            }
            set
            {
                setFlag(3, value);
            }
        }


        public virtual bool Eating
        {
            get
            {
                return getFlag(4);
            }
            set
            {
                setFlag(4, value);
            }
        }


        protected internal virtual bool getFlag(int i1)
        {
            return (dataWatcher.getWatchableObjectByte(0) & 1 << i1) != 0;
        }

        protected internal virtual void setFlag(int i1, bool z2)
        {
            sbyte b3 = dataWatcher.getWatchableObjectByte(0);
            if (z2)
            {
                dataWatcher.updateObject(0, (sbyte)(b3 | 1 << i1));
            }
            else
            {
                dataWatcher.updateObject(0, (sbyte)(b3 & ~(1 << i1)));
            }

        }

        public virtual int Air
        {
            get
            {
                return dataWatcher.getWatchableObjectShort(1);
            }
            set
            {
                dataWatcher.updateObject(1, (short)value);
            }
        }


        public virtual void onStruckByLightning(EntityLightningBolt entityLightningBolt1)
        {
            dealFireDamage(5);
            ++fire;
            if (fire == 0)
            {
                Fire = 8;
            }

        }

        public virtual void onKillEntity(EntityLiving entityLiving1)
        {
        }

        protected internal virtual bool pushOutOfBlocks(double d1, double d3, double d5)
        {
            int i7 = MathHelper.floor_double(d1);
            int i8 = MathHelper.floor_double(d3);
            int i9 = MathHelper.floor_double(d5);
            double d10 = d1 - i7;
            double d12 = d3 - i8;
            double d14 = d5 - i9;
            if (worldObj.isBlockNormalCube(i7, i8, i9))
            {
                bool z16 = !worldObj.isBlockNormalCube(i7 - 1, i8, i9);
                bool z17 = !worldObj.isBlockNormalCube(i7 + 1, i8, i9);
                bool z18 = !worldObj.isBlockNormalCube(i7, i8 - 1, i9);
                bool z19 = !worldObj.isBlockNormalCube(i7, i8 + 1, i9);
                bool z20 = !worldObj.isBlockNormalCube(i7, i8, i9 - 1);
                bool z21 = !worldObj.isBlockNormalCube(i7, i8, i9 + 1);
                sbyte b22 = -1;
                double d23 = 9999.0D;
                if (z16 && d10 < d23)
                {
                    d23 = d10;
                    b22 = 0;
                }

                if (z17 && 1.0D - d10 < d23)
                {
                    d23 = 1.0D - d10;
                    b22 = 1;
                }

                if (z18 && d12 < d23)
                {
                    d23 = d12;
                    b22 = 2;
                }

                if (z19 && 1.0D - d12 < d23)
                {
                    d23 = 1.0D - d12;
                    b22 = 3;
                }

                if (z20 && d14 < d23)
                {
                    d23 = d14;
                    b22 = 4;
                }

                if (z21 && 1.0D - d14 < d23)
                {
                    d23 = 1.0D - d14;
                    b22 = 5;
                }

                float f25 = rand.NextSingle() * 0.2F + 0.1F;
                if (b22 == 0)
                {
                    motionX = -f25;
                }

                if (b22 == 1)
                {
                    motionX = f25;
                }

                if (b22 == 2)
                {
                    motionY = -f25;
                }

                if (b22 == 3)
                {
                    motionY = f25;
                }

                if (b22 == 4)
                {
                    motionZ = -f25;
                }

                if (b22 == 5)
                {
                    motionZ = f25;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void setInWeb()
        {
            isInWeb = true;
            fallDistance = 0.0F;
        }

        public virtual Entity[] Parts
        {
            get
            {
                return null;
            }
        }

        public virtual bool isEntityEqual(Entity entity1)
        {
            return this == entity1;
        }

        public virtual void func_48079_f(float f1)
        {
        }

        public virtual bool canAttackWithItem()
        {
            return true;
        }
    }

}