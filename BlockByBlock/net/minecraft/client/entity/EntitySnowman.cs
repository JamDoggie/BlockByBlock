using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntitySnowman : EntityGolem
    {
        public EntitySnowman(World world1) : base(world1)
        {
            texture = "/mob/snowman.png";
            SetSize(0.4F, 1.8F);
            Navigator.func_48664_a(true);
            tasks.addTask(1, new EntityAIArrowAttack(this, 0.25F, 2, 20));
            tasks.addTask(2, new EntityAIWander(this, 0.2F));
            tasks.addTask(3, new EntityAIWatchClosest(this, typeof(EntityPlayer), 6.0F));
            tasks.addTask(4, new EntityAILookIdle(this));
            targetTasks.addTask(1, new EntityAINearestAttackableTarget(this, typeof(EntityMob), 16.0F, 0, true));
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
            if (Wet)
            {
                attackEntityFrom(DamageSource.drown, 1);
            }

            int i1 = MathHelper.floor_double(posX);
            int i2 = MathHelper.floor_double(posZ);
            if (worldObj.getBiomeGenForCoords(i1, i2).FloatTemperature > 1.0F)
            {
                attackEntityFrom(DamageSource.onFire, 1);
            }

            for (i1 = 0; i1 < 4; ++i1)
            {
                i2 = MathHelper.floor_double(posX + (double)((i1 % 2 * 2 - 1) * 0.25F));
                int i3 = MathHelper.floor_double(posY);
                int i4 = MathHelper.floor_double(posZ + (double)((i1 / 2 % 2 * 2 - 1) * 0.25F));
                if (worldObj.getBlockId(i2, i3, i4) == 0 && worldObj.getBiomeGenForCoords(i2, i4).FloatTemperature < 0.8F && Block.snow.canPlaceBlockAt(worldObj, i2, i3, i4))
                {
                    worldObj.setBlockWithNotify(i2, i3, i4, Block.snow.blockID);
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
                return Item.snowball.shiftedIndex;
            }
        }

        protected internal override void dropFewItems(bool z1, int i2)
        {
            int i3 = rand.Next(16);

            for (int i4 = 0; i4 < i3; ++i4)
            {
                dropItem(Item.snowball.shiftedIndex, 1);
            }

        }
    }

}