using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntityVillager : EntityAgeable
    {
        private int randomTickDivider;
        private bool isMatingFlag;
        private bool isPlayingFlag;
        internal Village villageObj;

        public EntityVillager(World world1) : this(world1, 0)
        {
        }

        public EntityVillager(World world1, int i2) : base(world1)
        {
            randomTickDivider = 0;
            isMatingFlag = false;
            isPlayingFlag = false;
            villageObj = null;
            Profession = i2;
            texture = "/mob/villager/villager.png";
            moveSpeed = 0.5F;
            Navigator.BreakDoors = true;
            Navigator.func_48664_a(true);
            tasks.addTask(0, new EntityAISwimming(this));
            tasks.addTask(1, new EntityAIAvoidEntity(this, typeof(EntityZombie), 8.0F, 0.3F, 0.35F));
            tasks.addTask(2, new EntityAIMoveIndoors(this));
            tasks.addTask(3, new EntityAIRestrictOpenDoor(this));
            tasks.addTask(4, new EntityAIOpenDoor(this, true));
            tasks.addTask(5, new EntityAIMoveTwardsRestriction(this, 0.3F));
            tasks.addTask(6, new EntityAIVillagerMate(this));
            tasks.addTask(7, new EntityAIFollowGolem(this));
            tasks.addTask(8, new EntityAIPlay(this, 0.32F));
            tasks.addTask(9, new EntityAIWatchClosest2(this, typeof(EntityPlayer), 3.0F, 1.0F));
            tasks.addTask(9, new EntityAIWatchClosest2(this, typeof(EntityVillager), 5.0F, 0.02F));
            tasks.addTask(9, new EntityAIWander(this, 0.3F));
            tasks.addTask(10, new EntityAIWatchClosest(this, typeof(EntityLiving), 8.0F));
        }

        public override bool AIEnabled
        {
            get
            {
                return true;
            }
        }

        protected internal override void updateAITick()
        {
            if (--randomTickDivider <= 0)
            {
                worldObj.villageCollectionObj.addVillagerPosition(MathHelper.floor_double(posX), MathHelper.floor_double(posY), MathHelper.floor_double(posZ));
                randomTickDivider = 70 + rand.Next(50);
                villageObj = worldObj.villageCollectionObj.findNearestVillage(MathHelper.floor_double(posX), MathHelper.floor_double(posY), MathHelper.floor_double(posZ), 32);
                if (villageObj == null)
                {
                    detachHome();
                }
                else
                {
                    ChunkCoordinates chunkCoordinates1 = villageObj.Center;
                    setHomeArea(chunkCoordinates1.posX, chunkCoordinates1.posY, chunkCoordinates1.posZ, villageObj.VillageRadius);
                }
            }

            base.updateAITick();
        }

        protected internal override void entityInit()
        {
            base.entityInit();
            dataWatcher.addObject(16, 0);
        }

        public override int MaxHealth
        {
            get
            {
                return 20;
            }
        }

        public override void onLivingUpdate()
        {
            base.onLivingUpdate();
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            base.writeEntityToNBT(nBTTagCompound1);
            nBTTagCompound1.setInteger("Profession", Profession);
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            base.readEntityFromNBT(nBTTagCompound1);
            Profession = nBTTagCompound1.getInteger("Profession");
        }

        public override string Texture
        {
            get
            {
                switch (Profession)
                {
                    case 0:
                        return "/mob/villager/farmer.png";
                    case 1:
                        return "/mob/villager/librarian.png";
                    case 2:
                        return "/mob/villager/priest.png";
                    case 3:
                        return "/mob/villager/smith.png";
                    case 4:
                        return "/mob/villager/butcher.png";
                    default:
                        return base.Texture;
                }
            }
        }

        protected internal override bool canDespawn()
        {
            return false;
        }

        protected internal override string LivingSound
        {
            get
            {
                return "mob.villager.default";
            }
        }

        protected internal override string HurtSound
        {
            get
            {
                return "mob.villager.defaulthurt";
            }
        }

        protected internal override string DeathSound
        {
            get
            {
                return "mob.villager.defaultdeath";
            }
        }

        public virtual int Profession
        {
            set
            {
                dataWatcher.updateObject(16, value);
            }
            get
            {
                return dataWatcher.getWatchableObjectInt(16);
            }
        }


        public virtual bool IsMatingFlag
        {
            get
            {
                return isMatingFlag;
            }
            set
            {
                isMatingFlag = value;
            }
        }


        public virtual bool IsPlayingFlag
        {
            set
            {
                isPlayingFlag = value;
            }
            get
            {
                return isPlayingFlag;
            }
        }


        public override EntityLiving RevengeTarget
        {
            set
            {
                base.RevengeTarget = value;
                if (villageObj != null && value != null)
                {
                    villageObj.addOrRenewAgressor(value);
                }

            }
        }
    }

}