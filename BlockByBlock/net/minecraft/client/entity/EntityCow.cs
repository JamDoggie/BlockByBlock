using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntityCow : EntityAnimal
    {
        public EntityCow(World world1) : base(world1)
        {
            texture = "/mob/cow.png";
            SetSize(0.9F, 1.3F);
            Navigator.func_48664_a(true);
            tasks.addTask(0, new EntityAISwimming(this));
            tasks.addTask(1, new EntityAIPanic(this, 0.38F));
            tasks.addTask(2, new EntityAIMate(this, 0.2F));
            tasks.addTask(3, new EntityAITempt(this, 0.25F, Item.wheat.shiftedIndex, false));
            tasks.addTask(4, new EntityAIFollowParent(this, 0.25F));
            tasks.addTask(5, new EntityAIWander(this, 0.2F));
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

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            base.writeEntityToNBT(nBTTagCompound1);
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            base.readEntityFromNBT(nBTTagCompound1);
        }

        protected internal override string LivingSound
        {
            get
            {
                return "mob.cow";
            }
        }

        protected internal override string HurtSound
        {
            get
            {
                return "mob.cowhurt";
            }
        }

        protected internal override string DeathSound
        {
            get
            {
                return "mob.cowhurt";
            }
        }

        protected internal override float SoundVolume
        {
            get
            {
                return 0.4F;
            }
        }

        protected internal override int DropItemId
        {
            get
            {
                return Item.leather.shiftedIndex;
            }
        }

        protected internal override void dropFewItems(bool z1, int i2)
        {
            int i3 = rand.Next(3) + rand.Next(1 + i2);

            int i4;
            for (i4 = 0; i4 < i3; ++i4)
            {
                dropItem(Item.leather.shiftedIndex, 1);
            }

            i3 = rand.Next(3) + 1 + rand.Next(1 + i2);

            for (i4 = 0; i4 < i3; ++i4)
            {
                if (Burning)
                {
                    dropItem(Item.beefCooked.shiftedIndex, 1);
                }
                else
                {
                    dropItem(Item.beefRaw.shiftedIndex, 1);
                }
            }

        }

        public override bool interact(EntityPlayer entityPlayer1)
        {
            ItemStack itemStack2 = entityPlayer1.inventory.CurrentItem;
            if (itemStack2 != null && itemStack2.itemID == Item.bucketEmpty.shiftedIndex)
            {
                entityPlayer1.inventory.setInventorySlotContents(entityPlayer1.inventory.currentItem, new ItemStack(Item.bucketMilk));
                return true;
            }
            else
            {
                return base.interact(entityPlayer1);
            }
        }

        public override EntityAnimal spawnBabyAnimal(EntityAnimal entityAnimal1)
        {
            return new EntityCow(worldObj);
        }
    }

}