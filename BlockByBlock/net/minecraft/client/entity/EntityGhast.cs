using System;
using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntityGhast : EntityFlying, IMob
    {
        public int courseChangeCooldown = 0;
        public double waypointX;
        public double waypointY;
        public double waypointZ;
        private Entity targetedEntity = null;
        private int aggroCooldown = 0;
        public int prevAttackCounter = 0;
        public int attackCounter = 0;

        public EntityGhast(World world1) : base(world1)
        {
            texture = "/mob/ghast.png";
            SetSize(4.0F, 4.0F);
            isImmuneToFire = true;
            experienceValue = 5;
        }

        public override bool attackEntityFrom(DamageSource damageSource1, int i2)
        {
            if ("fireball".Equals(damageSource1.DamageType) && damageSource1.Entity is EntityPlayer)
            {
                base.attackEntityFrom(damageSource1, 1000);
                ((EntityPlayer)damageSource1.Entity).triggerAchievement(AchievementList.ghast);
                return true;
            }
            else
            {
                return base.attackEntityFrom(damageSource1, i2);
            }
        }

        protected internal override void entityInit()
        {
            base.entityInit();
            dataWatcher.addObject(16, (sbyte)0);
        }

        public override int MaxHealth
        {
            get
            {
                return 10;
            }
        }

        public override void onUpdate()
        {
            base.onUpdate();
            sbyte b1 = dataWatcher.getWatchableObjectByte(16);
            texture = b1 == 1 ? "/mob/ghast_fire.png" : "/mob/ghast.png";
        }

        public override void updateEntityActionState()
        {
            if (!worldObj.isRemote && worldObj.difficultySetting == 0)
            {
                setDead();
            }

            despawnEntity();
            prevAttackCounter = attackCounter;
            double d1 = waypointX - posX;
            double d3 = waypointY - posY;
            double d5 = waypointZ - posZ;
            double d7 = (double)MathHelper.sqrt_double(d1 * d1 + d3 * d3 + d5 * d5);
            if (d7 < 1.0D || d7 > 60.0D)
            {
                waypointX = posX + (double)((rand.NextSingle() * 2.0F - 1.0F) * 16.0F);
                waypointY = posY + (double)((rand.NextSingle() * 2.0F - 1.0F) * 16.0F);
                waypointZ = posZ + (double)((rand.NextSingle() * 2.0F - 1.0F) * 16.0F);
            }

            if (courseChangeCooldown-- <= 0)
            {
                courseChangeCooldown += rand.Next(5) + 2;
                if (isCourseTraversable(waypointX, waypointY, waypointZ, d7))
                {
                    motionX += d1 / d7 * 0.1D;
                    motionY += d3 / d7 * 0.1D;
                    motionZ += d5 / d7 * 0.1D;
                }
                else
                {
                    waypointX = posX;
                    waypointY = posY;
                    waypointZ = posZ;
                }
            }

            if (targetedEntity != null && targetedEntity.isDead)
            {
                targetedEntity = null;
            }

            if (targetedEntity == null || aggroCooldown-- <= 0)
            {
                targetedEntity = worldObj.getClosestVulnerablePlayerToEntity(this, 100.0D);
                if (targetedEntity != null)
                {
                    aggroCooldown = 20;
                }
            }

            double d9 = 64.0D;
            if (targetedEntity != null && targetedEntity.getDistanceSqToEntity(this) < d9 * d9)
            {
                double d11 = targetedEntity.posX - posX;
                double d13 = targetedEntity.boundingBox.minY + (double)(targetedEntity.height / 2.0F) - (posY + (double)(height / 2.0F));
                double d15 = targetedEntity.posZ - posZ;
                renderYawOffset = rotationYaw = -(float)Math.Atan2(d11, d15) * 180.0F / (float)Math.PI;
                if (canEntityBeSeen(targetedEntity))
                {
                    if (attackCounter == 10)
                    {
                        worldObj.playAuxSFXAtEntity(null, 1007, (int)posX, (int)posY, (int)posZ, 0);
                    }

                    ++attackCounter;
                    if (attackCounter == 20)
                    {
                        worldObj.playAuxSFXAtEntity(null, 1008, (int)posX, (int)posY, (int)posZ, 0);
                        EntityFireball entityFireball17 = new EntityFireball(worldObj, this, d11, d13, d15);
                        double d18 = 4.0D;
                        Vec3D vec3D20 = getLook(1.0F);
                        entityFireball17.posX = posX + vec3D20.xCoord * d18;
                        entityFireball17.posY = posY + (double)(height / 2.0F) + 0.5D;
                        entityFireball17.posZ = posZ + vec3D20.zCoord * d18;
                        worldObj.spawnEntityInWorld(entityFireball17);
                        attackCounter = -40;
                    }
                }
                else if (attackCounter > 0)
                {
                    --attackCounter;
                }
            }
            else
            {
                renderYawOffset = rotationYaw = -(float)Math.Atan2(motionX, motionZ) * 180.0F / (float)Math.PI;
                if (attackCounter > 0)
                {
                    --attackCounter;
                }
            }

            if (!worldObj.isRemote)
            {
                sbyte b21 = dataWatcher.getWatchableObjectByte(16);
                sbyte b12 = (sbyte)(attackCounter > 10 ? 1 : 0);
                if (b21 != b12)
                {
                    dataWatcher.updateObject(16, b12);
                }
            }

        }

        private bool isCourseTraversable(double d1, double d3, double d5, double d7)
        {
            double d9 = (waypointX - posX) / d7;
            double d11 = (waypointY - posY) / d7;
            double d13 = (waypointZ - posZ) / d7;
            AxisAlignedBB axisAlignedBB15 = boundingBox.copy();

            for (int i16 = 1; i16 < d7; ++i16)
            {
                axisAlignedBB15.offset(d9, d11, d13);
                if (worldObj.getCollidingBoundingBoxes(this, axisAlignedBB15).Count > 0)
                {
                    return false;
                }
            }

            return true;
        }

        protected internal override string LivingSound
        {
            get
            {
                return "mob.ghast.moan";
            }
        }

        protected internal override string HurtSound
        {
            get
            {
                return "mob.ghast.scream";
            }
        }

        protected internal override string DeathSound
        {
            get
            {
                return "mob.ghast.death";
            }
        }

        protected internal override int DropItemId
        {
            get
            {
                return Item.gunpowder.shiftedIndex;
            }
        }

        protected internal override void dropFewItems(bool z1, int i2)
        {
            int i3 = rand.Next(2) + rand.Next(1 + i2);

            int i4;
            for (i4 = 0; i4 < i3; ++i4)
            {
                dropItem(Item.ghastTear.shiftedIndex, 1);
            }

            i3 = rand.Next(3) + rand.Next(1 + i2);

            for (i4 = 0; i4 < i3; ++i4)
            {
                dropItem(Item.gunpowder.shiftedIndex, 1);
            }

        }

        protected internal override float SoundVolume
        {
            get
            {
                return 10.0F;
            }
        }

        public override bool CanSpawnHere
        {
            get
            {
                return rand.Next(20) == 0 && base.CanSpawnHere && worldObj.difficultySetting > 0;
            }
        }

        public override int MaxSpawnedInChunk
        {
            get
            {
                return 1;
            }
        }
    }

}