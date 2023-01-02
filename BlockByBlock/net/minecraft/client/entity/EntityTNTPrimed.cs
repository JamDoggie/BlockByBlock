using System;
using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntityTNTPrimed : Entity
    {
        public int fuse;

        public EntityTNTPrimed(World world1) : base(world1)
        {
            fuse = 0;
            preventEntitySpawning = true;
            SetSize(0.98F, 0.98F);
            yOffset = height / 2.0F;
        }

        public EntityTNTPrimed(World world1, double d2, double d4, double d6) : this(world1)
        {
            SetPosition(d2, d4, d6);
            float f8 = (float)(portinghelpers.MathHelper.NextDouble * (double)(float)Math.PI * 2.0D);
            motionX = -(float)Math.Sin((double)f8) * 0.02F;
            motionY = 0.2F;
            motionZ = -(float)Math.Cos((double)f8) * 0.02F;
            fuse = 80;
            prevPosX = d2;
            prevPosY = d4;
            prevPosZ = d6;
        }

        protected internal override void entityInit()
        {
        }

        protected internal override bool canTriggerWalking()
        {
            return false;
        }

        public override bool canBeCollidedWith()
        {
            return !isDead;
        }

        public override void onUpdate()
        {
            prevPosX = posX;
            prevPosY = posY;
            prevPosZ = posZ;
            motionY -= 0.04F;
            moveEntity(motionX, motionY, motionZ);
            motionX *= 0.98F;
            motionY *= 0.98F;
            motionZ *= 0.98F;
            if (onGround)
            {
                motionX *= 0.7F;
                motionZ *= 0.7F;
                motionY *= -0.5D;
            }

            if (fuse-- <= 0)
            {
                if (!worldObj.isRemote)
                {
                    setDead();
                    explode();
                }
                else
                {
                    setDead();
                }
            }
            else
            {
                worldObj.spawnParticle("smoke", posX, posY + 0.5D, posZ, 0.0D, 0.0D, 0.0D);
            }

        }

        private void explode()
        {
            float f1 = 4.0F;
            worldObj.createExplosion(null, posX, posY, posZ, f1);
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            nBTTagCompound1.setByte("Fuse", (sbyte)fuse);
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            fuse = nBTTagCompound1.getByte("Fuse");
        }

        public override float ShadowSize
        {
            get
            {
                return 0.0F;
            }
        }
    }

}