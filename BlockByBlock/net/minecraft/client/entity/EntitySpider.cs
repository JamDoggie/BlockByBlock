using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntitySpider : EntityMob
    {
        public EntitySpider(World world1) : base(world1)
        {
            texture = "/mob/spider.png";
            SetSize(1.4F, 0.9F);
            moveSpeed = 0.8F;
        }

        protected internal override void entityInit()
        {
            base.entityInit();
            dataWatcher.addObject(16, new sbyte?(0));
        }

        public override void onLivingUpdate()
        {
            base.onLivingUpdate();
        }

        public override void onUpdate()
        {
            base.onUpdate();
            if (!worldObj.isRemote)
            {
                func_40148_a(isCollidedHorizontally);
            }

        }

        public override int MaxHealth
        {
            get
            {
                return 16;
            }
        }

        public override double MountedYOffset
        {
            get
            {
                return height * 0.75D - 0.5D;
            }
        }

        protected internal override bool canTriggerWalking()
        {
            return false;
        }

        protected internal override Entity findPlayerToAttack()
        {
            float f1 = getBrightness(1.0F);
            if (f1 < 0.5F)
            {
                double d2 = 16.0D;
                return worldObj.getClosestVulnerablePlayerToEntity(this, d2);
            }
            else
            {
                return null;
            }
        }

        protected internal override string LivingSound
        {
            get
            {
                return "mob.spider";
            }
        }

        protected internal override string HurtSound
        {
            get
            {
                return "mob.spider";
            }
        }

        protected internal override string DeathSound
        {
            get
            {
                return "mob.spiderdeath";
            }
        }

        protected internal override void attackEntity(Entity entity1, float f2)
        {
            float f3 = getBrightness(1.0F);
            if (f3 > 0.5F && rand.Next(100) == 0)
            {
                entityToAttack = null;
            }
            else
            {
                if (f2 > 2.0F && f2 < 6.0F && rand.Next(10) == 0)
                {
                    if (onGround)
                    {
                        double d4 = entity1.posX - posX;
                        double d6 = entity1.posZ - posZ;
                        float f8 = MathHelper.sqrt_double(d4 * d4 + d6 * d6);
                        motionX = d4 / (double)f8 * 0.5D * (double)0.8F + motionX * (double)0.2F;
                        motionZ = d6 / (double)f8 * 0.5D * (double)0.8F + motionZ * (double)0.2F;
                        motionY = 0.4F;
                    }
                }
                else
                {
                    base.attackEntity(entity1, f2);
                }

            }
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
                return Item.silk.shiftedIndex;
            }
        }

        protected internal override void dropFewItems(bool z1, int i2)
        {
            base.dropFewItems(z1, i2);
            if (z1 && (rand.Next(3) == 0 || rand.Next(1 + i2) > 0))
            {
                dropItem(Item.spiderEye.shiftedIndex, 1);
            }

        }

        public override bool OnLadder
        {
            get
            {
                return func_40149_l_();
            }
        }

        public override void setInWeb()
        {
        }

        public virtual float spiderScaleAmount()
        {
            return 1.0F;
        }

        public override EnumCreatureAttribute CreatureAttribute
        {
            get
            {
                return EnumCreatureAttribute.ARTHROPOD;
            }
        }

        public override bool isPotionApplicable(PotionEffect potionEffect1)
        {
            return potionEffect1.PotionID == Potion.poison.id ? false : base.isPotionApplicable(potionEffect1);
        }

        public virtual bool func_40149_l_()
        {
            return (dataWatcher.getWatchableObjectByte(16) & 1) != 0;
        }

        public virtual void func_40148_a(bool z1)
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
    }

}