using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntityCreeper : EntityMob
    {
        internal int timeSinceIgnited;
        internal int lastActiveTime;

        public EntityCreeper(World world1) : base(world1)
        {
            texture = "/mob/creeper.png";
            tasks.addTask(1, new EntityAISwimming(this));
            tasks.addTask(2, new EntityAICreeperSwell(this));
            tasks.addTask(3, new EntityAIAvoidEntity(this, typeof(EntityOcelot), 6.0F, 0.25F, 0.3F));
            tasks.addTask(4, new EntityAIAttackOnCollide(this, 0.25F, false));
            tasks.addTask(5, new EntityAIWander(this, 0.2F));
            tasks.addTask(6, new EntityAIWatchClosest(this, typeof(EntityPlayer), 8.0F));
            tasks.addTask(6, new EntityAILookIdle(this));
            targetTasks.addTask(1, new EntityAINearestAttackableTarget(this, typeof(EntityPlayer), 16.0F, 0, true));
            targetTasks.addTask(2, new EntityAIHurtByTarget(this, false));
        }

        public override bool AIEnabled
        {
            get
            {
                return true;
            }
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
            dataWatcher.addObject(16, (sbyte)-1);
            dataWatcher.addObject(17, (sbyte)0);
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            base.writeEntityToNBT(nBTTagCompound1);
            if (dataWatcher.getWatchableObjectByte(17) == 1)
            {
                nBTTagCompound1.setBoolean("powered", true);
            }

        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            base.readEntityFromNBT(nBTTagCompound1);
            dataWatcher.updateObject(17, (sbyte)(nBTTagCompound1.getBoolean("powered") ? 1 : 0));
        }

        public override void onUpdate()
        {
            if (EntityAlive)
            {
                lastActiveTime = timeSinceIgnited;
                int i1 = CreeperState;
                if (i1 > 0 && timeSinceIgnited == 0)
                {
                    worldObj.playSoundAtEntity(this, "random.fuse", 1.0F, 0.5F);
                }

                timeSinceIgnited += i1;
                if (timeSinceIgnited < 0)
                {
                    timeSinceIgnited = 0;
                }

                if (timeSinceIgnited >= 30)
                {
                    timeSinceIgnited = 30;
                    if (!worldObj.isRemote)
                    {
                        if (Powered)
                        {
                            worldObj.createExplosion(this, posX, posY, posZ, 6.0F);
                        }
                        else
                        {
                            worldObj.createExplosion(this, posX, posY, posZ, 3.0F);
                        }

                        setDead();
                    }
                }
            }

            base.onUpdate();
        }

        protected internal override string HurtSound
        {
            get
            {
                return "mob.creeper";
            }
        }

        protected internal override string DeathSound
        {
            get
            {
                return "mob.creeperdeath";
            }
        }

        public override void onDeath(DamageSource damageSource1)
        {
            base.onDeath(damageSource1);
            if (damageSource1.Entity is EntitySkeleton)
            {
                dropItem(Item.record13.shiftedIndex + rand.Next(10), 1);
            }

        }

        public override bool attackEntityAsMob(Entity entity1)
        {
            return true;
        }

        public virtual bool Powered
        {
            get
            {
                return dataWatcher.getWatchableObjectByte(17) == 1;
            }
        }

        public virtual float setCreeperFlashTime(float f1)
        {
            return (lastActiveTime + (timeSinceIgnited - lastActiveTime) * f1) / 28.0F;
        }

        protected internal override int DropItemId
        {
            get
            {
                return Item.gunpowder.shiftedIndex;
            }
        }

        public virtual int CreeperState
        {
            get
            {
                return dataWatcher.getWatchableObjectByte(16);
            }
            set
            {
                dataWatcher.updateObject(16, (sbyte)value);
            }
        }


        public override void onStruckByLightning(EntityLightningBolt entityLightningBolt1)
        {
            base.onStruckByLightning(entityLightningBolt1);
            dataWatcher.updateObject(17, (sbyte)1);
        }
    }

}