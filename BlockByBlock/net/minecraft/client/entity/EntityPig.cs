using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntityPig : EntityAnimal
    {
        public EntityPig(World world1) : base(world1)
        {
            texture = "/mob/pig.png";
            SetSize(0.9F, 0.9F);
            Navigator.func_48664_a(true);
            float f2 = 0.25F;
            tasks.addTask(0, new EntityAISwimming(this));
            tasks.addTask(1, new EntityAIPanic(this, 0.38F));
            tasks.addTask(2, new EntityAIMate(this, f2));
            tasks.addTask(3, new EntityAITempt(this, 0.25F, Item.wheat.shiftedIndex, false));
            tasks.addTask(4, new EntityAIFollowParent(this, 0.28F));
            tasks.addTask(5, new EntityAIWander(this, f2));
            tasks.addTask(6, new EntityAIWatchClosest(this, typeof(EntityPlayer), 6.0F));
            tasks.addTask(7, new EntityAILookIdle(this));
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
                return 10;
            }
        }

        protected internal override void entityInit()
        {
            base.entityInit();
            dataWatcher.addObject(16, (sbyte)0);
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            base.writeEntityToNBT(nBTTagCompound1);
            nBTTagCompound1.setBoolean("Saddle", Saddled);
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            base.readEntityFromNBT(nBTTagCompound1);
            Saddled = nBTTagCompound1.getBoolean("Saddle");
        }

        protected internal override string LivingSound
        {
            get
            {
                return "mob.pig";
            }
        }

        protected internal override string HurtSound
        {
            get
            {
                return "mob.pig";
            }
        }

        protected internal override string DeathSound
        {
            get
            {
                return "mob.pigdeath";
            }
        }

        public override bool interact(EntityPlayer entityPlayer1)
        {
            if (base.interact(entityPlayer1))
            {
                return true;
            }
            else if (!Saddled || worldObj.isRemote || riddenByEntity != null && riddenByEntity != entityPlayer1)
            {
                return false;
            }
            else
            {
                entityPlayer1.mountEntity(this);
                return true;
            }
        }

        protected internal override int DropItemId
        {
            get
            {
                return Burning ? Item.porkCooked.shiftedIndex : Item.porkRaw.shiftedIndex;
            }
        }

        public virtual bool Saddled
        {
            get
            {
                return (dataWatcher.getWatchableObjectByte(16) & 1) != 0;
            }
            set
            {
                if (value)
                {
                    dataWatcher.updateObject(16, (sbyte)1);
                }
                else
                {
                    dataWatcher.updateObject(16, (sbyte)0);
                }

            }
        }


        public override void onStruckByLightning(EntityLightningBolt entityLightningBolt1)
        {
            if (!worldObj.isRemote)
            {
                EntityPigZombie entityPigZombie2 = new EntityPigZombie(worldObj);
                entityPigZombie2.setLocationAndAngles(posX, posY, posZ, rotationYaw, rotationPitch);
                worldObj.spawnEntityInWorld(entityPigZombie2);
                setDead();
            }
        }

        protected internal override void fall(float f1)
        {
            base.fall(f1);
            if (f1 > 5.0F && riddenByEntity is EntityPlayer)
            {
                ((EntityPlayer)riddenByEntity).triggerAchievement(AchievementList.flyPig);
            }

        }

        public override EntityAnimal spawnBabyAnimal(EntityAnimal entityAnimal1)
        {
            return new EntityPig(worldObj);
        }
    }

}