using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public abstract class EntityMob : EntityCreature, IMob
    {
        protected internal int attackStrength = 2;

        public EntityMob(World world1) : base(world1)
        {
            experienceValue = 5;
        }

        public override void onLivingUpdate()
        {
            float f1 = getBrightness(1.0F);
            if (f1 > 0.5F)
            {
                entityAge += 2;
            }

            base.onLivingUpdate();
        }

        public override void onUpdate()
        {
            base.onUpdate();
            if (!worldObj.isRemote && worldObj.difficultySetting == 0)
            {
                setDead();
            }

        }

        protected internal override Entity findPlayerToAttack()
        {
            EntityPlayer entityPlayer1 = worldObj.getClosestVulnerablePlayerToEntity(this, 16.0D);
            return entityPlayer1 != null && canEntityBeSeen(entityPlayer1) ? entityPlayer1 : null;
        }

        public override bool attackEntityFrom(DamageSource damageSource1, int i2)
        {
            if (base.attackEntityFrom(damageSource1, i2))
            {
                Entity entity3 = damageSource1.Entity;
                if (riddenByEntity != entity3 && ridingEntity != entity3)
                {
                    if (entity3 != this)
                    {
                        entityToAttack = entity3;
                    }

                    return true;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public override bool attackEntityAsMob(Entity entity1)
        {
            int i2 = attackStrength;
            if (isPotionActive(Potion.damageBoost))
            {
                i2 += 3 << getActivePotionEffect(Potion.damageBoost).Amplifier;
            }

            if (isPotionActive(Potion.weakness))
            {
                i2 -= 2 << getActivePotionEffect(Potion.weakness).Amplifier;
            }

            return entity1.attackEntityFrom(DamageSource.causeMobDamage(this), i2);
        }

        protected internal override void attackEntity(Entity entity1, float f2)
        {
            if (attackTime <= 0 && f2 < 2.0F && entity1.boundingBox.maxY > boundingBox.minY && entity1.boundingBox.minY < boundingBox.maxY)
            {
                attackTime = 20;
                attackEntityAsMob(entity1);
            }

        }

        public override float getBlockPathWeight(int i1, int i2, int i3)
        {
            return 0.5F - worldObj.getLightBrightness(i1, i2, i3);
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            base.writeEntityToNBT(nBTTagCompound1);
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            base.readEntityFromNBT(nBTTagCompound1);
        }

        protected internal virtual bool ValidLightLevel
        {
            get
            {
                int i1 = MathHelper.floor_double(posX);
                int i2 = MathHelper.floor_double(boundingBox.minY);
                int i3 = MathHelper.floor_double(posZ);
                if (worldObj.getSavedLightValue(EnumSkyBlock.Sky, i1, i2, i3) > rand.Next(32))
                {
                    return false;
                }
                else
                {
                    int i4 = worldObj.getBlockLightValue(i1, i2, i3);
                    if (worldObj.Thundering)
                    {
                        int i5 = worldObj.skylightSubtracted;
                        worldObj.skylightSubtracted = 10;
                        i4 = worldObj.getBlockLightValue(i1, i2, i3);
                        worldObj.skylightSubtracted = i5;
                    }

                    return i4 <= rand.Next(8);
                }
            }
        }

        public override bool CanSpawnHere
        {
            get
            {
                return ValidLightLevel && base.CanSpawnHere;
            }
        }
    }

}