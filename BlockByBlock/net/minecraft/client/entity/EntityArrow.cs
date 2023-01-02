using System;
using net.minecraft.src;

namespace net.minecraft.client.entity
{

    public class EntityArrow : Entity
    {
        private int xTile = -1;
        private int yTile = -1;
        private int zTile = -1;
        private int inTile = 0;
        private int inData = 0;
        private bool inGround = false;
        public bool doesArrowBelongToPlayer = false;
        public int arrowShake = 0;
        public Entity shootingEntity;
        private int ticksInGround;
        private int ticksInAir = 0;
        private double damage = 2.0D;
        private int field_46027_au;
        public bool arrowCritical = false;

        public EntityArrow(World world1) : base(world1)
        {
            SetSize(0.5F, 0.5F);
        }

        public EntityArrow(World world1, double d2, double d4, double d6) : base(world1)
        {
            SetSize(0.5F, 0.5F);
            SetPosition(d2, d4, d6);
            yOffset = 0.0F;
        }

        public EntityArrow(World world1, EntityLiving entityLiving2, EntityLiving entityLiving3, float f4, float f5) : base(world1)
        {
            shootingEntity = entityLiving2;
            doesArrowBelongToPlayer = entityLiving2 is EntityPlayer;
            posY = entityLiving2.posY + (double)entityLiving2.EyeHeight - (double)0.1F;
            double d6 = entityLiving3.posX - entityLiving2.posX;
            double d8 = entityLiving3.posY + (double)entityLiving3.EyeHeight - (double)0.7F - posY;
            double d10 = entityLiving3.posZ - entityLiving2.posZ;
            double d12 = (double)MathHelper.sqrt_double(d6 * d6 + d10 * d10);
            if (d12 >= 1.0E-7D)
            {
                float f14 = (float)(Math.Atan2(d10, d6) * 180.0D / (double)(float)Math.PI) - 90.0F;
                float f15 = (float)-(Math.Atan2(d8, d12) * 180.0D / (double)(float)Math.PI);
                double d16 = d6 / d12;
                double d18 = d10 / d12;
                setLocationAndAngles(entityLiving2.posX + d16, posY, entityLiving2.posZ + d18, f14, f15);
                yOffset = 0.0F;
                float f20 = (float)d12 * 0.2F;
                setArrowHeading(d6, d8 + (double)f20, d10, f4, f5);
            }
        }

        public EntityArrow(World world1, EntityLiving entityLiving2, float f3) : base(world1)
        {
            shootingEntity = entityLiving2;
            doesArrowBelongToPlayer = entityLiving2 is EntityPlayer;
            SetSize(0.5F, 0.5F);
            setLocationAndAngles(entityLiving2.posX, entityLiving2.posY + (double)entityLiving2.EyeHeight, entityLiving2.posZ, entityLiving2.rotationYaw, entityLiving2.rotationPitch);
            posX -= MathHelper.cos(rotationYaw / 180.0F * (float)Math.PI) * 0.16F;
            posY -= 0.1F;
            posZ -= MathHelper.sin(rotationYaw / 180.0F * (float)Math.PI) * 0.16F;
            SetPosition(posX, posY, posZ);
            yOffset = 0.0F;
            motionX = -MathHelper.sin(rotationYaw / 180.0F * (float)Math.PI) * MathHelper.cos(rotationPitch / 180.0F * (float)Math.PI);
            motionZ = MathHelper.cos(rotationYaw / 180.0F * (float)Math.PI) * MathHelper.cos(rotationPitch / 180.0F * (float)Math.PI);
            motionY = -MathHelper.sin(rotationPitch / 180.0F * (float)Math.PI);
            setArrowHeading(motionX, motionY, motionZ, f3 * 1.5F, 1.0F);
        }

        protected internal override void entityInit()
        {
        }

        public virtual void setArrowHeading(double d1, double d3, double d5, float f7, float f8)
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
                prevRotationPitch = rotationPitch;
                prevRotationYaw = rotationYaw;
                setLocationAndAngles(posX, posY, posZ, rotationYaw, rotationPitch);
                ticksInGround = 0;
            }

        }

        public override void onUpdate()
        {
            base.onUpdate();
            if (prevRotationPitch == 0.0F && prevRotationYaw == 0.0F)
            {
                float f1 = MathHelper.sqrt_double(motionX * motionX + motionZ * motionZ);
                prevRotationYaw = rotationYaw = (float)(Math.Atan2(motionX, motionZ) * 180.0D / (double)(float)Math.PI);
                prevRotationPitch = rotationPitch = (float)(Math.Atan2(motionY, (double)f1) * 180.0D / (double)(float)Math.PI);
            }

            int i15 = worldObj.getBlockId(xTile, yTile, zTile);
            if (i15 > 0)
            {
                Block.blocksList[i15].setBlockBoundsBasedOnState(worldObj, xTile, yTile, zTile);
                AxisAlignedBB axisAlignedBB2 = Block.blocksList[i15].getCollisionBoundingBoxFromPool(worldObj, xTile, yTile, zTile);
                if (axisAlignedBB2 != null && axisAlignedBB2.isVecInside(Vec3D.createVector(posX, posY, posZ)))
                {
                    inGround = true;
                }
            }

            if (arrowShake > 0)
            {
                --arrowShake;
            }

            if (inGround)
            {
                i15 = worldObj.getBlockId(xTile, yTile, zTile);
                int i18 = worldObj.getBlockMetadata(xTile, yTile, zTile);
                if (i15 == inTile && i18 == inData)
                {
                    ++ticksInGround;
                    if (ticksInGround == 1200)
                    {
                        setDead();
                    }

                }
                else
                {
                    inGround = false;
                    motionX *= rand.NextSingle() * 0.2F;
                    motionY *= rand.NextSingle() * 0.2F;
                    motionZ *= rand.NextSingle() * 0.2F;
                    ticksInGround = 0;
                    ticksInAir = 0;
                }
            }
            else
            {
                ++ticksInAir;
                Vec3D vec3D16 = Vec3D.createVector(posX, posY, posZ);
                Vec3D vec3D17 = Vec3D.createVector(posX + motionX, posY + motionY, posZ + motionZ);
                MovingObjectPosition movingObjectPosition3 = worldObj.rayTraceBlocks_do_do(vec3D16, vec3D17, false, true);
                vec3D16 = Vec3D.createVector(posX, posY, posZ);
                vec3D17 = Vec3D.createVector(posX + motionX, posY + motionY, posZ + motionZ);
                if (movingObjectPosition3 != null)
                {
                    vec3D17 = Vec3D.createVector(movingObjectPosition3.hitVec.xCoord, movingObjectPosition3.hitVec.yCoord, movingObjectPosition3.hitVec.zCoord);
                }

                Entity entity4 = null;
                System.Collections.IList list5 = worldObj.getEntitiesWithinAABBExcludingEntity(this, boundingBox.addCoord(motionX, motionY, motionZ).expand(1.0D, 1.0D, 1.0D));
                double d6 = 0.0D;

                int i8;
                float f10;
                for (i8 = 0; i8 < list5.Count; ++i8)
                {
                    Entity entity9 = (Entity)list5[i8];
                    if (entity9.canBeCollidedWith() && (entity9 != shootingEntity || ticksInAir >= 5))
                    {
                        f10 = 0.3F;
                        AxisAlignedBB axisAlignedBB11 = entity9.boundingBox.expand((double)f10, (double)f10, (double)f10);
                        MovingObjectPosition movingObjectPosition12 = axisAlignedBB11.calculateIntercept(vec3D16, vec3D17);
                        if (movingObjectPosition12 != null)
                        {
                            double d13 = vec3D16.distanceTo(movingObjectPosition12.hitVec);
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

                float f19;
                if (movingObjectPosition3 != null)
                {
                    if (movingObjectPosition3.entityHit != null)
                    {
                        f19 = MathHelper.sqrt_double(motionX * motionX + motionY * motionY + motionZ * motionZ);
                        int i20 = (int)Math.Ceiling((double)f19 * damage);
                        if (arrowCritical)
                        {
                            i20 += rand.Next(i20 / 2 + 2);
                        }

                        DamageSource damageSource21 = null;
                        if (shootingEntity == null)
                        {
                            damageSource21 = DamageSource.causeArrowDamage(this, this);
                        }
                        else
                        {
                            damageSource21 = DamageSource.causeArrowDamage(this, shootingEntity);
                        }

                        if (Burning)
                        {
                            movingObjectPosition3.entityHit.Fire = 5;
                        }

                        if (movingObjectPosition3.entityHit.attackEntityFrom(damageSource21, i20))
                        {
                            if (movingObjectPosition3.entityHit is EntityLiving)
                            {
                                ++((EntityLiving)movingObjectPosition3.entityHit).arrowHitTempCounter;
                                if (field_46027_au > 0)
                                {
                                    float f23 = MathHelper.sqrt_double(motionX * motionX + motionZ * motionZ);
                                    if (f23 > 0.0F)
                                    {
                                        movingObjectPosition3.entityHit.addVelocity(motionX * field_46027_au * (double)0.6F / (double)f23, 0.1D, motionZ * field_46027_au * (double)0.6F / (double)f23);
                                    }
                                }
                            }

                            worldObj.playSoundAtEntity(this, "random.bowhit", 1.0F, 1.2F / (rand.NextSingle() * 0.2F + 0.9F));
                            setDead();
                        }
                        else
                        {
                            motionX *= -0.10000000149011612D;
                            motionY *= -0.10000000149011612D;
                            motionZ *= -0.10000000149011612D;
                            rotationYaw += 180.0F;
                            prevRotationYaw += 180.0F;
                            ticksInAir = 0;
                        }
                    }
                    else
                    {
                        xTile = movingObjectPosition3.blockX;
                        yTile = movingObjectPosition3.blockY;
                        zTile = movingObjectPosition3.blockZ;
                        inTile = worldObj.getBlockId(xTile, yTile, zTile);
                        inData = worldObj.getBlockMetadata(xTile, yTile, zTile);
                        motionX = (float)(movingObjectPosition3.hitVec.xCoord - posX);
                        motionY = (float)(movingObjectPosition3.hitVec.yCoord - posY);
                        motionZ = (float)(movingObjectPosition3.hitVec.zCoord - posZ);
                        f19 = MathHelper.sqrt_double(motionX * motionX + motionY * motionY + motionZ * motionZ);
                        posX -= motionX / (double)f19 * (double)0.05F;
                        posY -= motionY / (double)f19 * (double)0.05F;
                        posZ -= motionZ / (double)f19 * (double)0.05F;
                        worldObj.playSoundAtEntity(this, "random.bowhit", 1.0F, 1.2F / (rand.NextSingle() * 0.2F + 0.9F));
                        inGround = true;
                        arrowShake = 7;
                        arrowCritical = false;
                    }
                }

                if (arrowCritical)
                {
                    for (i8 = 0; i8 < 4; ++i8)
                    {
                        worldObj.spawnParticle("crit", posX + motionX * i8 / 4.0D, posY + motionY * i8 / 4.0D, posZ + motionZ * i8 / 4.0D, -motionX, -motionY + 0.2D, -motionZ);
                    }
                }

                posX += motionX;
                posY += motionY;
                posZ += motionZ;
                f19 = MathHelper.sqrt_double(motionX * motionX + motionZ * motionZ);
                rotationYaw = (float)(Math.Atan2(motionX, motionZ) * 180.0D / (double)(float)Math.PI);

                for (rotationPitch = (float)(Math.Atan2(motionY, (double)f19) * 180.0D / (double)(float)Math.PI); rotationPitch - prevRotationPitch < -180.0F; prevRotationPitch -= 360.0F)
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
                float f22 = 0.99F;
                f10 = 0.05F;
                if (InWater)
                {
                    for (int i24 = 0; i24 < 4; ++i24)
                    {
                        float f25 = 0.25F;
                        worldObj.spawnParticle("bubble", posX - motionX * (double)f25, posY - motionY * (double)f25, posZ - motionZ * (double)f25, motionX, motionY, motionZ);
                    }

                    f22 = 0.8F;
                }

                motionX *= f22;
                motionY *= f22;
                motionZ *= f22;
                motionY -= f10;
                SetPosition(posX, posY, posZ);
            }
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            nBTTagCompound1.setShort("xTile", (short)xTile);
            nBTTagCompound1.setShort("yTile", (short)yTile);
            nBTTagCompound1.setShort("zTile", (short)zTile);
            nBTTagCompound1.setByte("inTile", (sbyte)inTile);
            nBTTagCompound1.setByte("inData", (sbyte)inData);
            nBTTagCompound1.setByte("shake", (sbyte)arrowShake);
            nBTTagCompound1.setByte("inGround", (sbyte)(inGround ? 1 : 0));
            nBTTagCompound1.setBoolean("player", doesArrowBelongToPlayer);
            nBTTagCompound1.setDouble("damage", damage);
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            xTile = nBTTagCompound1.getShort("xTile");
            yTile = nBTTagCompound1.getShort("yTile");
            zTile = nBTTagCompound1.getShort("zTile");
            inTile = nBTTagCompound1.getByte("inTile") & 255;
            inData = nBTTagCompound1.getByte("inData") & 255;
            arrowShake = nBTTagCompound1.getByte("shake") & 255;
            inGround = nBTTagCompound1.getByte("inGround") == 1;
            doesArrowBelongToPlayer = nBTTagCompound1.getBoolean("player");
            if (nBTTagCompound1.hasKey("damage"))
            {
                damage = nBTTagCompound1.getDouble("damage");
            }

        }

        public override void onCollideWithPlayer(EntityPlayer entityPlayer1)
        {
            if (!worldObj.isRemote)
            {
                if (inGround && doesArrowBelongToPlayer && arrowShake <= 0 && entityPlayer1.inventory.addItemStackToInventory(new ItemStack(Item.arrow, 1)))
                {
                    worldObj.playSoundAtEntity(this, "random.pop", 0.2F, ((rand.NextSingle() - rand.NextSingle()) * 0.7F + 1.0F) * 2.0F);
                    entityPlayer1.onItemPickup(this, 1);
                    setDead();
                }

            }
        }

        public override float ShadowSize
        {
            get
            {
                return 0.0F;
            }
        }

        public virtual double Damage
        {
            set
            {
                damage = value;
            }
            get
            {
                return damage;
            }
        }


        public virtual void func_46023_b(int i1)
        {
            field_46027_au = i1;
        }

        public override bool canAttackWithItem()
        {
            return false;
        }
    }

}