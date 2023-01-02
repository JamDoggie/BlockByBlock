using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntityEnderCrystal : Entity
    {
        public int innerRotation;
        public int health;

        public EntityEnderCrystal(World world1) : base(world1)
        {
            innerRotation = 0;
            preventEntitySpawning = true;
            SetSize(2.0F, 2.0F);
            yOffset = height / 2.0F;
            health = 5;
            innerRotation = rand.Next(100000);
        }

        public EntityEnderCrystal(World world1, double d2, double d4, double d6) : this(world1)
        {
            SetPosition(d2, d4, d6);
        }

        protected internal override bool canTriggerWalking()
        {
            return false;
        }

        protected internal override void entityInit()
        {
            dataWatcher.addObject(8, health);
        }

        public override void onUpdate()
        {
            prevPosX = posX;
            prevPosY = posY;
            prevPosZ = posZ;
            ++innerRotation;
            dataWatcher.updateObject(8, health);
            int i1 = MathHelper.floor_double(posX);
            int i2 = MathHelper.floor_double(posY);
            int i3 = MathHelper.floor_double(posZ);
            if (worldObj.getBlockId(i1, i2, i3) != Block.fire.blockID)
            {
                worldObj.setBlockWithNotify(i1, i2, i3, Block.fire.blockID);
            }

        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
        }

        public override float ShadowSize
        {
            get
            {
                return 0.0F;
            }
        }

        public override bool canBeCollidedWith()
        {
            return true;
        }

        public override bool attackEntityFrom(DamageSource damageSource1, int i2)
        {
            if (!isDead && !worldObj.isRemote)
            {
                health = 0;
                if (health <= 0)
                {
                    if (!worldObj.isRemote)
                    {
                        setDead();
                        worldObj.createExplosion(null, posX, posY, posZ, 6.0F);
                    }
                    else
                    {
                        setDead();
                    }
                }
            }

            return true;
        }
    }

}