using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntityChicken : EntityAnimal
    {
        public bool field_753_a = false;
        public float field_752_b = 0.0F;
        public float destPos = 0.0F;
        public float field_757_d;
        public float field_756_e;
        public float field_755_h = 1.0F;
        public int timeUntilNextEgg;

        public EntityChicken(World world1) : base(world1)
        {
            texture = "/mob/chicken.png";
            SetSize(0.3F, 0.7F);
            timeUntilNextEgg = rand.Next(6000) + 6000;
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
                return 4;
            }
        }

        public override void onLivingUpdate()
        {
            base.onLivingUpdate();
            field_756_e = field_752_b;
            field_757_d = destPos;
            destPos = (float)(destPos + (onGround ? -1 : 4) * 0.3D);
            if (destPos < 0.0F)
            {
                destPos = 0.0F;
            }

            if (destPos > 1.0F)
            {
                destPos = 1.0F;
            }

            if (!onGround && field_755_h < 1.0F)
            {
                field_755_h = 1.0F;
            }

            field_755_h = (float)(field_755_h * 0.9D);
            if (!onGround && motionY < 0.0D)
            {
                motionY *= 0.6D;
            }

            field_752_b += field_755_h * 2.0F;
            if (!Child && !worldObj.isRemote && --timeUntilNextEgg <= 0)
            {
                worldObj.playSoundAtEntity(this, "mob.chickenplop", 1.0F, (rand.NextSingle() - rand.NextSingle()) * 0.2F + 1.0F);
                dropItem(Item.egg.shiftedIndex, 1);
                timeUntilNextEgg = rand.Next(6000) + 6000;
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

        protected internal override string LivingSound
        {
            get
            {
                return "mob.chicken";
            }
        }

        protected internal override string HurtSound
        {
            get
            {
                return "mob.chickenhurt";
            }
        }

        protected internal override string DeathSound
        {
            get
            {
                return "mob.chickenhurt";
            }
        }

        protected internal override int DropItemId
        {
            get
            {
                return Item.feather.shiftedIndex;
            }
        }

        protected internal override void dropFewItems(bool z1, int i2)
        {
            int i3 = rand.Next(3) + rand.Next(1 + i2);

            for (int i4 = 0; i4 < i3; ++i4)
            {
                dropItem(Item.feather.shiftedIndex, 1);
            }

            if (Burning)
            {
                dropItem(Item.chickenCooked.shiftedIndex, 1);
            }
            else
            {
                dropItem(Item.chickenRaw.shiftedIndex, 1);
            }

        }

        public override EntityAnimal spawnBabyAnimal(EntityAnimal entityAnimal1)
        {
            return new EntityChicken(worldObj);
        }
    }

}