using System;
using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public abstract class EntityCreature : EntityLiving
    {
        private PathEntity pathToEntity;
        protected internal Entity entityToAttack;
        protected internal bool hasAttacked = false;
        protected internal int fleeingTick = 0;

        public EntityCreature(World world1) : base(world1)
        {
        }

        protected internal virtual bool MovementCeased
        {
            get
            {
                return false;
            }
        }

        public override void updateEntityActionState()
        {
            Profiler.startSection("ai");
            if (fleeingTick > 0)
            {
                --fleeingTick;
            }

            hasAttacked = MovementCeased;
            float f1 = 16.0F;
            if (entityToAttack == null)
            {
                entityToAttack = findPlayerToAttack();
                if (entityToAttack != null)
                {
                    pathToEntity = worldObj.getPathEntityToEntity(this, entityToAttack, f1, true, false, false, true);
                }
            }
            else if (!entityToAttack.EntityAlive)
            {
                entityToAttack = null;
            }
            else
            {
                float f2 = entityToAttack.getDistanceToEntity(this);
                if (canEntityBeSeen(entityToAttack))
                {
                    attackEntity(entityToAttack, f2);
                }
                else
                {
                    attackBlockedEntity(entityToAttack, f2);
                }
            }

            Profiler.endSection();
            if (!hasAttacked && entityToAttack != null && (pathToEntity == null || rand.Next(20) == 0))
            {
                pathToEntity = worldObj.getPathEntityToEntity(this, entityToAttack, f1, true, false, false, true);
            }
            else if (!hasAttacked && (pathToEntity == null && rand.Next(180) == 0 || rand.Next(120) == 0 || fleeingTick > 0) && entityAge < 100)
            {
                updateWanderPath();
            }

            int i21 = MathHelper.floor_double(boundingBox.minY + 0.5D);
            bool z3 = InWater;
            bool z4 = handleLavaMovement();
            rotationPitch = 0.0F;
            if (pathToEntity != null && rand.Next(100) != 0)
            {
                Profiler.startSection("followpath");
                Vec3D vec3D5 = pathToEntity.getCurrentNodeVec3d(this);
                double d6 = (double)(width * 2.0F);

                while (vec3D5 != null && vec3D5.squareDistanceTo(posX, vec3D5.yCoord, posZ) < d6 * d6)
                {
                    pathToEntity.incrementPathIndex();
                    if (pathToEntity.Finished)
                    {
                        vec3D5 = null;
                        pathToEntity = null;
                    }
                    else
                    {
                        vec3D5 = pathToEntity.getCurrentNodeVec3d(this);
                    }
                }

                isJumping = false;
                if (vec3D5 != null)
                {
                    double d8 = vec3D5.xCoord - posX;
                    double d10 = vec3D5.zCoord - posZ;
                    double d12 = vec3D5.yCoord - i21;
                    float f14 = (float)(Math.Atan2(d10, d8) * 180.0D / (double)(float)Math.PI) - 90.0F;
                    float f15 = f14 - rotationYaw;

                    for (moveForward = moveSpeed; f15 < -180.0F; f15 += 360.0F)
                    {
                    }

                    while (f15 >= 180.0F)
                    {
                        f15 -= 360.0F;
                    }

                    if (f15 > 30.0F)
                    {
                        f15 = 30.0F;
                    }

                    if (f15 < -30.0F)
                    {
                        f15 = -30.0F;
                    }

                    rotationYaw += f15;
                    if (hasAttacked && entityToAttack != null)
                    {
                        double d16 = entityToAttack.posX - posX;
                        double d18 = entityToAttack.posZ - posZ;
                        float f20 = rotationYaw;
                        rotationYaw = (float)(Math.Atan2(d18, d16) * 180.0D / (double)(float)Math.PI) - 90.0F;
                        f15 = (f20 - rotationYaw + 90.0F) * (float)Math.PI / 180.0F;
                        moveStrafing = -MathHelper.sin(f15) * moveForward * 1.0F;
                        moveForward = MathHelper.cos(f15) * moveForward * 1.0F;
                    }

                    if (d12 > 0.0D)
                    {
                        isJumping = true;
                    }
                }

                if (entityToAttack != null)
                {
                    faceEntity(entityToAttack, 30.0F, 30.0F);
                }

                if (isCollidedHorizontally && !hasPath())
                {
                    isJumping = true;
                }

                if (rand.NextSingle() < 0.8F && (z3 || z4))
                {
                    isJumping = true;
                }

                Profiler.endSection();
            }
            else
            {
                base.updateEntityActionState();
                pathToEntity = null;
            }
        }

        protected internal virtual void updateWanderPath()
        {
            Profiler.startSection("stroll");
            bool z1 = false;
            int i2 = -1;
            int i3 = -1;
            int i4 = -1;
            float f5 = -99999.0F;

            for (int i6 = 0; i6 < 10; ++i6)
            {
                int i7 = MathHelper.floor_double(posX + rand.Next(13) - 6.0D);
                int i8 = MathHelper.floor_double(posY + rand.Next(7) - 3.0D);
                int i9 = MathHelper.floor_double(posZ + rand.Next(13) - 6.0D);
                float f10 = getBlockPathWeight(i7, i8, i9);
                if (f10 > f5)
                {
                    f5 = f10;
                    i2 = i7;
                    i3 = i8;
                    i4 = i9;
                    z1 = true;
                }
            }

            if (z1)
            {
                pathToEntity = worldObj.getEntityPathToXYZ(this, i2, i3, i4, 10.0F, true, false, false, true);
            }

            Profiler.endSection();
        }

        protected internal virtual void attackEntity(Entity entity1, float f2)
        {
        }

        protected internal virtual void attackBlockedEntity(Entity entity1, float f2)
        {
        }

        public virtual float getBlockPathWeight(int i1, int i2, int i3)
        {
            return 0.0F;
        }

        protected internal virtual Entity findPlayerToAttack()
        {
            return null;
        }

        public override bool CanSpawnHere
        {
            get
            {
                int i1 = MathHelper.floor_double(posX);
                int i2 = MathHelper.floor_double(boundingBox.minY);
                int i3 = MathHelper.floor_double(posZ);
                return base.CanSpawnHere && getBlockPathWeight(i1, i2, i3) >= 0.0F;
            }
        }

        public virtual bool hasPath()
        {
            return pathToEntity != null;
        }

        public virtual PathEntity PathToEntity
        {
            set
            {
                pathToEntity = value;
            }
        }

        public virtual Entity EntityToAttack
        {
            get
            {
                return entityToAttack;
            }
        }

        public virtual Entity Target
        {
            set
            {
                entityToAttack = value;
            }
        }

        protected internal override float SpeedModifier
        {
            get
            {
                if (AIEnabled)
                {
                    return 1.0F;
                }
                else
                {
                    float f1 = base.SpeedModifier;
                    if (fleeingTick > 0)
                    {
                        f1 *= 2.0F;
                    }

                    return f1;
                }
            }
        }
    }

}