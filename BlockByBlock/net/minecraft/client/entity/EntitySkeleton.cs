using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntitySkeleton : EntityMob
    {
        private static readonly ItemStack defaultHeldItem = new ItemStack(Item.bow, 1);

        public EntitySkeleton(World world1) : base(world1)
        {
            texture = "/mob/skeleton.png";
            moveSpeed = 0.25F;
            tasks.addTask(1, new EntityAISwimming(this));
            tasks.addTask(2, new EntityAIRestrictSun(this));
            tasks.addTask(3, new EntityAIFleeSun(this, moveSpeed));
            tasks.addTask(4, new EntityAIArrowAttack(this, moveSpeed, 1, 60));
            tasks.addTask(5, new EntityAIWander(this, moveSpeed));
            tasks.addTask(6, new EntityAIWatchClosest(this, typeof(EntityPlayer), 8.0F));
            tasks.addTask(6, new EntityAILookIdle(this));
            targetTasks.addTask(1, new EntityAIHurtByTarget(this, false));
            targetTasks.addTask(2, new EntityAINearestAttackableTarget(this, typeof(EntityPlayer), 16.0F, 0, true));
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

        protected internal override string LivingSound
        {
            get
            {
                return "mob.skeleton";
            }
        }

        protected internal override string HurtSound
        {
            get
            {
                return "mob.skeletonhurt";
            }
        }

        protected internal override string DeathSound
        {
            get
            {
                return "mob.skeletonhurt";
            }
        }

        public override ItemStack HeldItem
        {
            get
            {
                return defaultHeldItem;
            }
        }

        public override EnumCreatureAttribute CreatureAttribute
        {
            get
            {
                return EnumCreatureAttribute.UNDEAD;
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

        public override void onDeath(DamageSource damageSource1)
        {
            base.onDeath(damageSource1);
            if (damageSource1.SourceOfDamage is EntityArrow && damageSource1.Entity is EntityPlayer)
            {
                EntityPlayer entityPlayer2 = (EntityPlayer)damageSource1.Entity;
                double d3 = entityPlayer2.posX - posX;
                double d5 = entityPlayer2.posZ - posZ;
                if (d3 * d3 + d5 * d5 >= 2500.0D)
                {
                    entityPlayer2.triggerAchievement(AchievementList.snipeSkeleton);
                }
            }

        }

        protected internal override int DropItemId
        {
            get
            {
                return Item.arrow.shiftedIndex;
            }
        }

        protected internal override void dropFewItems(bool z1, int i2)
        {
            int i3 = rand.Next(3 + i2);

            int i4;
            for (i4 = 0; i4 < i3; ++i4)
            {
                dropItem(Item.arrow.shiftedIndex, 1);
            }

            i3 = rand.Next(3 + i2);

            for (i4 = 0; i4 < i3; ++i4)
            {
                dropItem(Item.bone.shiftedIndex, 1);
            }

        }

        protected internal override void dropRareDrop(int i1)
        {
            if (i1 > 0)
            {
                ItemStack itemStack2 = new ItemStack(Item.bow);
                EnchantmentHelper.func_48441_a(rand, itemStack2, 5);
                entityDropItem(itemStack2, 0.0F);
            }
            else
            {
                dropItem(Item.bow.shiftedIndex, 1);
            }

        }
    }

}