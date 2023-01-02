using System;
using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntityBlaze : EntityMob
    {
        private float heightOffset = 0.5F;
        private int heightOffsetUpdateTime;
        private int field_40152_d;

        public EntityBlaze(World world1) : base(world1)
        {
            texture = "/mob/fire.png";
            isImmuneToFire = true;
            attackStrength = 6;
            experienceValue = 10;
        }

        public override int MaxHealth
        {
            get
            {
                return 20;
            }
        }

        protected internal override void entityInit()
        {
            base.entityInit();
            dataWatcher.addObject(16, new sbyte?(0));
        }

        protected internal override string LivingSound
        {
            get
            {
                return "mob.blaze.breathe";
            }
        }

        protected internal override string HurtSound
        {
            get
            {
                return "mob.blaze.hit";
            }
        }

        protected internal override string DeathSound
        {
            get
            {
                return "mob.blaze.death";
            }
        }

        public override bool attackEntityFrom(DamageSource damageSource1, int i2)
        {
            return base.attackEntityFrom(damageSource1, i2);
        }

        public override void onDeath(DamageSource damageSource1)
        {
            base.onDeath(damageSource1);
        }

        public override int getBrightnessForRender(float f1)
        {
            return 15728880;
        }

        public override float getBrightness(float f1)
        {
            return 1.0F;
        }

        public override void onLivingUpdate()
        {
            if (!worldObj.isRemote)
            {
                if (Wet)
                {
                    attackEntityFrom(DamageSource.drown, 1);
                }

                --heightOffsetUpdateTime;
                if (heightOffsetUpdateTime <= 0)
                {
                    heightOffsetUpdateTime = 100;
                    heightOffset = 0.5F + (float)rand.NextGaussian() * 3.0F;
                }

                if (EntityToAttack != null && EntityToAttack.posY + (double)EntityToAttack.EyeHeight > posY + (double)EyeHeight + heightOffset)
                {
                    motionY += ((double)0.3F - motionY) * (double)0.3F;
                }
            }

            if (rand.Next(24) == 0)
            {
                worldObj.playSoundEffect(posX + 0.5D, posY + 0.5D, posZ + 0.5D, "fire.fire", 1.0F + rand.NextSingle(), rand.NextSingle() * 0.7F + 0.3F);
            }

            if (!onGround && motionY < 0.0D)
            {
                motionY *= 0.6D;
            }

            for (int i1 = 0; i1 < 2; ++i1)
            {
                worldObj.spawnParticle("largesmoke", posX + (rand.NextDouble() - 0.5D) * width, posY + rand.NextDouble() * height, posZ + (rand.NextDouble() - 0.5D) * width, 0.0D, 0.0D, 0.0D);
            }

            base.onLivingUpdate();
        }

        protected internal override void attackEntity(Entity entity1, float f2)
        {
            if (attackTime <= 0 && f2 < 2.0F && entity1.boundingBox.maxY > boundingBox.minY && entity1.boundingBox.minY < boundingBox.maxY)
            {
                attackTime = 20;
                attackEntityAsMob(entity1);
            }
            else if (f2 < 30.0F)
            {
                double d3 = entity1.posX - posX;
                double d5 = entity1.boundingBox.minY + (double)(entity1.height / 2.0F) - (posY + (double)(height / 2.0F));
                double d7 = entity1.posZ - posZ;
                if (attackTime == 0)
                {
                    ++field_40152_d;
                    if (field_40152_d == 1)
                    {
                        attackTime = 60;
                        func_40150_a(true);
                    }
                    else if (field_40152_d <= 4)
                    {
                        attackTime = 6;
                    }
                    else
                    {
                        attackTime = 100;
                        field_40152_d = 0;
                        func_40150_a(false);
                    }

                    if (field_40152_d > 1)
                    {
                        float f9 = MathHelper.sqrt_float(f2) * 0.5F;
                        worldObj.playAuxSFXAtEntity(null, 1009, (int)posX, (int)posY, (int)posZ, 0);

                        for (int i10 = 0; i10 < 1; ++i10)
                        {
                            EntitySmallFireball entitySmallFireball11 = new EntitySmallFireball(worldObj, this, d3 + rand.NextGaussian() * (double)f9, d5, d7 + rand.NextGaussian() * (double)f9);
                            entitySmallFireball11.posY = posY + (double)(height / 2.0F) + 0.5D;
                            worldObj.spawnEntityInWorld(entitySmallFireball11);
                        }
                    }
                }

                rotationYaw = (float)(Math.Atan2(d7, d3) * 180.0D / (double)(float)Math.PI) - 90.0F;
                hasAttacked = true;
            }

        }

        protected internal override void fall(float f1)
        {
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            base.writeEntityToNBT(nBTTagCompound1);
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            base.readEntityFromNBT(nBTTagCompound1);
        }

        protected internal override int DropItemId
        {
            get
            {
                return Item.blazeRod.shiftedIndex;
            }
        }

        public override bool Burning
        {
            get
            {
                return func_40151_ac();
            }
        }

        protected internal override void dropFewItems(bool z1, int i2)
        {
            if (z1)
            {
                int i3 = rand.Next(2 + i2);

                for (int i4 = 0; i4 < i3; ++i4)
                {
                    dropItem(Item.blazeRod.shiftedIndex, 1);
                }
            }

        }

        public virtual bool func_40151_ac()
        {
            return (dataWatcher.getWatchableObjectByte(16) & 1) != 0;
        }

        public virtual void func_40150_a(bool z1)
        {
            sbyte b2 = dataWatcher.getWatchableObjectByte(16);
            if (z1)
            {
                b2 = (sbyte)(b2 | 1);
            }
            else
            {
                b2 &= -2;
            }

            dataWatcher.updateObject(16, b2);
        }

        protected internal override bool ValidLightLevel
        {
            get
            {
                return true;
            }
        }
    }

}