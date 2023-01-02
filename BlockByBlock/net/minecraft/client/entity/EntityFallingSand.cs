using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntityFallingSand : Entity
    {
        public int blockID;
        public int fallTime = 0;

        public EntityFallingSand(World world1) : base(world1)
        {
        }

        public EntityFallingSand(World world1, double d2, double d4, double d6, int i8) : base(world1)
        {
            blockID = i8;
            preventEntitySpawning = true;
            SetSize(0.98F, 0.98F);
            yOffset = height / 2.0F;
            SetPosition(d2, d4, d6);
            motionX = 0.0D;
            motionY = 0.0D;
            motionZ = 0.0D;
            prevPosX = d2;
            prevPosY = d4;
            prevPosZ = d6;
        }

        protected internal override bool canTriggerWalking()
        {
            return false;
        }

        protected internal override void entityInit()
        {
        }

        public override bool canBeCollidedWith()
        {
            return !isDead;
        }

        public override void onUpdate()
        {
            if (blockID == 0)
            {
                setDead();
            }
            else
            {
                prevPosX = posX;
                prevPosY = posY;
                prevPosZ = posZ;
                ++fallTime;
                motionY -= 0.04F;
                moveEntity(motionX, motionY, motionZ);
                motionX *= 0.98F;
                motionY *= 0.98F;
                motionZ *= 0.98F;
                int i1 = MathHelper.floor_double(posX);
                int i2 = MathHelper.floor_double(posY);
                int i3 = MathHelper.floor_double(posZ);
                if (fallTime == 1 && worldObj.getBlockId(i1, i2, i3) == blockID)
                {
                    worldObj.setBlockWithNotify(i1, i2, i3, 0);
                }
                else if (!worldObj.isRemote && fallTime == 1)
                {
                    setDead();
                }

                if (onGround)
                {
                    motionX *= 0.7F;
                    motionZ *= 0.7F;
                    motionY *= -0.5D;
                    if (worldObj.getBlockId(i1, i2, i3) != Block.pistonMoving.blockID)
                    {
                        setDead();
                        if ((!worldObj.canBlockBePlacedAt(blockID, i1, i2, i3, true, 1) || BlockSand.canFallBelow(worldObj, i1, i2 - 1, i3) || !worldObj.setBlockWithNotify(i1, i2, i3, blockID)) && !worldObj.isRemote)
                        {
                            dropItem(blockID, 1);
                        }
                    }
                }
                else if (fallTime > 100 && !worldObj.isRemote && (i2 < 1 || i2 > 256) || fallTime > 600)
                {
                    dropItem(blockID, 1);
                    setDead();
                }

            }
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            nBTTagCompound1.setByte("Tile", (sbyte)blockID);
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            blockID = nBTTagCompound1.getByte("Tile") & 255;
        }

        public override float ShadowSize
        {
            get
            {
                return 0.0F;
            }
        }

        public virtual World World
        {
            get
            {
                return worldObj;
            }
        }
    }

}