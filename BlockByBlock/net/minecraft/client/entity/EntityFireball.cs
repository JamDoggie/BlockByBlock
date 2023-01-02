using System;
using net.minecraft.src;

namespace net.minecraft.client.entity
{

    public class EntityFireball : Entity
    {
        private int xTile = -1;
        private int yTile = -1;
        private int zTile = -1;
        private int inTile = 0;
        private bool inGround = false;
        public EntityLiving shootingEntity;
        private int ticksAlive;
        private int ticksInAir = 0;
        public double accelerationX;
        public double accelerationY;
        public double accelerationZ;

        public EntityFireball(World world1) : base(world1)
        {
            SetSize(1.0F, 1.0F);
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

        public EntityFireball(World world1, double d2, double d4, double d6, double d8, double d10, double d12) : base(world1)
        {
            SetSize(1.0F, 1.0F);
            setLocationAndAngles(d2, d4, d6, rotationYaw, rotationPitch);
            SetPosition(d2, d4, d6);
            double d14 = (double)MathHelper.sqrt_double(d8 * d8 + d10 * d10 + d12 * d12);
            accelerationX = d8 / d14 * 0.1D;
            accelerationY = d10 / d14 * 0.1D;
            accelerationZ = d12 / d14 * 0.1D;
        }

        public EntityFireball(World world1, EntityLiving entityLiving2, double d3, double d5, double d7) : base(world1)
        {
            shootingEntity = entityLiving2;
            SetSize(1.0F, 1.0F);
            setLocationAndAngles(entityLiving2.posX, entityLiving2.posY, entityLiving2.posZ, entityLiving2.rotationYaw, entityLiving2.rotationPitch);
            SetPosition(posX, posY, posZ);
            yOffset = 0.0F;
            motionX = motionY = motionZ = 0.0D;
            d3 += rand.NextGaussian() * 0.4D;
            d5 += rand.NextGaussian() * 0.4D;
            d7 += rand.NextGaussian() * 0.4D;
            double d9 = (double)MathHelper.sqrt_double(d3 * d3 + d5 * d5 + d7 * d7);
            accelerationX = d3 / d9 * 0.1D;
            accelerationY = d5 / d9 * 0.1D;
            accelerationZ = d7 / d9 * 0.1D;
        }

        public override void onUpdate()
        {
            if (worldObj.isRemote || (shootingEntity == null || !shootingEntity.isDead) && worldObj.blockExists((int)posX, (int)posY, (int)posZ))
            {
                base.onUpdate();
                Fire = 1;
                if (inGround)
                {
                    int i1 = worldObj.getBlockId(xTile, yTile, zTile);
                    if (i1 == inTile)
                    {
                        ++ticksAlive;
                        if (ticksAlive == 600)
                        {
                            setDead();
                        }

                        return;
                    }

                    inGround = false;
                    motionX *= rand.NextSingle() * 0.2F;
                    motionY *= rand.NextSingle() * 0.2F;
                    motionZ *= rand.NextSingle() * 0.2F;
                    ticksAlive = 0;
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

                Entity entity4 = null;
                System.Collections.IList list5 = worldObj.getEntitiesWithinAABBExcludingEntity(this, boundingBox.addCoord(motionX, motionY, motionZ).expand(1.0D, 1.0D, 1.0D));
                double d6 = 0.0D;

                for (int i8 = 0; i8 < list5.Count; ++i8)
                {
                    Entity entity9 = (Entity)list5[i8];
                    if (entity9.canBeCollidedWith() && (!entity9.isEntityEqual(shootingEntity) || ticksInAir >= 25))
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

                if (movingObjectPosition3 != null)
                {
                    func_40071_a(movingObjectPosition3);
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
                float f17 = 0.95F;
                if (InWater)
                {
                    for (int i18 = 0; i18 < 4; ++i18)
                    {
                        float f19 = 0.25F;
                        worldObj.spawnParticle("bubble", posX - motionX * (double)f19, posY - motionY * (double)f19, posZ - motionZ * (double)f19, motionX, motionY, motionZ);
                    }

                    f17 = 0.8F;
                }

                motionX += accelerationX;
                motionY += accelerationY;
                motionZ += accelerationZ;
                motionX *= f17;
                motionY *= f17;
                motionZ *= f17;
                worldObj.spawnParticle("smoke", posX, posY + 0.5D, posZ, 0.0D, 0.0D, 0.0D);
                SetPosition(posX, posY, posZ);
            }
            else
            {
                setDead();
            }
        }

        protected internal virtual void func_40071_a(MovingObjectPosition movingObjectPosition1)
        {
            if (!worldObj.isRemote)
            {
                if (movingObjectPosition1.entityHit != null && movingObjectPosition1.entityHit.attackEntityFrom(DamageSource.causeFireballDamage(this, shootingEntity), 4))
                {
                    ;
                }

                worldObj.newExplosion(null, posX, posY, posZ, 1.0F, true);
                setDead();
            }

        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            nBTTagCompound1.setShort("xTile", (short)xTile);
            nBTTagCompound1.setShort("yTile", (short)yTile);
            nBTTagCompound1.setShort("zTile", (short)zTile);
            nBTTagCompound1.setByte("inTile", (sbyte)inTile);
            nBTTagCompound1.setByte("inGround", (sbyte)(inGround ? 1 : 0));
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            xTile = nBTTagCompound1.getShort("xTile");
            yTile = nBTTagCompound1.getShort("yTile");
            zTile = nBTTagCompound1.getShort("zTile");
            inTile = nBTTagCompound1.getByte("inTile") & 255;
            inGround = nBTTagCompound1.getByte("inGround") == 1;
        }

        public override bool canBeCollidedWith()
        {
            return true;
        }

        public override float CollisionBorderSize
        {
            get
            {
                return 1.0F;
            }
        }

        public override bool attackEntityFrom(DamageSource damageSource1, int i2)
        {
            setBeenAttacked();
            if (damageSource1.Entity != null)
            {
                Vec3D vec3D3 = damageSource1.Entity.LookVec;
                if (vec3D3 != null)
                {
                    motionX = vec3D3.xCoord;
                    motionY = vec3D3.yCoord;
                    motionZ = vec3D3.zCoord;
                    accelerationX = motionX * 0.1D;
                    accelerationY = motionY * 0.1D;
                    accelerationZ = motionZ * 0.1D;
                }

                if (damageSource1.Entity is EntityLiving)
                {
                    shootingEntity = (EntityLiving)damageSource1.Entity;
                }

                return true;
            }
            else
            {
                return false;
            }
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
    }

}