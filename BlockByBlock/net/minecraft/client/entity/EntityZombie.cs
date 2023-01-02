using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntityZombie : EntityMob
    {
        public EntityZombie(World world1) : base(world1)
        {
            texture = "/mob/zombie.png";
            moveSpeed = 0.23F;
            attackStrength = 4;
            Navigator.BreakDoors = true;
            tasks.addTask(0, new EntityAISwimming(this));
            tasks.addTask(1, new EntityAIBreakDoor(this));
            tasks.addTask(2, new EntityAIAttackOnCollide(this, typeof(EntityPlayer), moveSpeed, false));
            tasks.addTask(3, new EntityAIAttackOnCollide(this, typeof(EntityVillager), moveSpeed, true));
            tasks.addTask(4, new EntityAIMoveTwardsRestriction(this, moveSpeed));
            tasks.addTask(5, new EntityAIMoveThroughVillage(this, moveSpeed, false));
            tasks.addTask(6, new EntityAIWander(this, moveSpeed));
            tasks.addTask(7, new EntityAIWatchClosest(this, typeof(EntityPlayer), 8.0F));
            tasks.addTask(7, new EntityAILookIdle(this));
            targetTasks.addTask(1, new EntityAIHurtByTarget(this, false));
            targetTasks.addTask(2, new EntityAINearestAttackableTarget(this, typeof(EntityPlayer), 16.0F, 0, true));
            targetTasks.addTask(2, new EntityAINearestAttackableTarget(this, typeof(EntityVillager), 16.0F, 0, false));
        }

        public override int MaxHealth
        {
            get
            {
                return 20;
            }
        }

        public override int TotalArmorValue
        {
            get
            {
                return 2;
            }
        }

        public override bool AIEnabled
        {
            get
            {
                return true;
            }
        }

        public override void onLivingUpdate()
        {
            if (worldObj.Daytime && !worldObj.isRemote)
            {
                float f1 = getBrightness(1.0F);
                if (f1 > 0.5F && worldObj.canBlockSeeTheSky(MathHelper.floor_double(posX), MathHelper.floor_double(posY), MathHelper.floor_double(posZ)) && rand.NextSingle() * 30.0F < (f1 - 0.4F) * 2.0F)
                {
                    Fire = 8;
                }
            }

            base.onLivingUpdate();
        }

        protected internal override string LivingSound
        {
            get
            {
                return "mob.zombie";
            }
        }

        protected internal override string HurtSound
        {
            get
            {
                return "mob.zombiehurt";
            }
        }

        protected internal override string DeathSound
        {
            get
            {
                return "mob.zombiedeath";
            }
        }

        protected internal override int DropItemId
        {
            get
            {
                return Item.rottenFlesh.shiftedIndex;
            }
        }

        public override EnumCreatureAttribute CreatureAttribute
        {
            get
            {
                return EnumCreatureAttribute.UNDEAD;
            }
        }

        protected internal override void dropRareDrop(int i1)
        {
            switch (rand.Next(4))
            {
                case 0:
                    dropItem(Item.swordSteel.shiftedIndex, 1);
                    break;
                case 1:
                    dropItem(Item.helmetSteel.shiftedIndex, 1);
                    break;
                case 2:
                    dropItem(Item.ingotIron.shiftedIndex, 1);
                    break;
                case 3:
                    dropItem(Item.shovelSteel.shiftedIndex, 1);
                    break;
            }

        }
    }

}