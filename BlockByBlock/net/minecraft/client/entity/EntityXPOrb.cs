using System;
using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntityXPOrb : Entity
    {
        public int xpColor;
        public int xpOrbAge = 0;
        public int field_35126_c;
        private int xpOrbHealth = 5;
        private int xpValue;

        public EntityXPOrb(World world1, double d2, double d4, double d6, int i8) : base(world1)
        {
            SetSize(0.5F, 0.5F);
            yOffset = height / 2.0F;
            SetPosition(d2, d4, d6);
            rotationYaw = (float)(portinghelpers.MathHelper.NextDouble * 360.0D);
            motionX = (float)(portinghelpers.MathHelper.NextDouble * (double)0.2F - (double)0.1F) * 2.0F;
            motionY = (float)(portinghelpers.MathHelper.NextDouble * 0.2D) * 2.0F;
            motionZ = (float)(portinghelpers.MathHelper.NextDouble * (double)0.2F - (double)0.1F) * 2.0F;
            xpValue = i8;
        }

        protected internal override bool canTriggerWalking()
        {
            return false;
        }

        public EntityXPOrb(World world1) : base(world1)
        {
            SetSize(0.25F, 0.25F);
            yOffset = height / 2.0F;
        }

        protected internal override void entityInit()
        {
        }

        public override int getBrightnessForRender(float f1)
        {
            float f2 = 0.5F;
            if (f2 < 0.0F)
            {
                f2 = 0.0F;
            }

            if (f2 > 1.0F)
            {
                f2 = 1.0F;
            }

            int i3 = base.getBrightnessForRender(f1);
            int i4 = i3 & 255;
            int i5 = i3 >> 16 & 255;
            i4 += (int)(f2 * 15.0F * 16.0F);
            if (i4 > 240)
            {
                i4 = 240;
            }

            return i4 | i5 << 16;
        }

        public override void onUpdate()
        {
            base.onUpdate();
            if (field_35126_c > 0)
            {
                --field_35126_c;
            }

            prevPosX = posX;
            prevPosY = posY;
            prevPosZ = posZ;
            motionY -= 0.03F;
            if (worldObj.getBlockMaterial(MathHelper.floor_double(posX), MathHelper.floor_double(posY), MathHelper.floor_double(posZ)) == Material.lava)
            {
                motionY = 0.2F;
                motionX = (rand.NextSingle() - rand.NextSingle()) * 0.2F;
                motionZ = (rand.NextSingle() - rand.NextSingle()) * 0.2F;
                worldObj.playSoundAtEntity(this, "random.fizz", 0.4F, 2.0F + rand.NextSingle() * 0.4F);
            }

            pushOutOfBlocks(posX, (boundingBox.minY + boundingBox.maxY) / 2.0D, posZ);
            double d1 = 8.0D;
            EntityPlayer entityPlayer3 = worldObj.getClosestPlayerToEntity(this, d1);
            if (entityPlayer3 != null)
            {
                double d4 = (entityPlayer3.posX - posX) / d1;
                double d6 = (entityPlayer3.posY + (double)entityPlayer3.EyeHeight - posY) / d1;
                double d8 = (entityPlayer3.posZ - posZ) / d1;
                double d10 = Math.Sqrt(d4 * d4 + d6 * d6 + d8 * d8);
                double d12 = 1.0D - d10;
                if (d12 > 0.0D)
                {
                    d12 *= d12;
                    motionX += d4 / d10 * d12 * 0.1D;
                    motionY += d6 / d10 * d12 * 0.1D;
                    motionZ += d8 / d10 * d12 * 0.1D;
                }
            }

            moveEntity(motionX, motionY, motionZ);
            float f14 = 0.98F;
            if (onGround)
            {
                f14 = 0.58800006F;
                int i5 = worldObj.getBlockId(MathHelper.floor_double(posX), MathHelper.floor_double(boundingBox.minY) - 1, MathHelper.floor_double(posZ));
                if (i5 > 0)
                {
                    f14 = Block.blocksList[i5].slipperiness * 0.98F;
                }
            }

            motionX *= f14;
            motionY *= 0.98F;
            motionZ *= f14;
            if (onGround)
            {
                motionY *= -0.8999999761581421D;
            }

            ++xpColor;
            ++xpOrbAge;
            if (xpOrbAge >= 6000)
            {
                setDead();
            }

        }

        public override bool handleWaterMovement()
        {
            return worldObj.handleMaterialAcceleration(boundingBox, Material.water, this);
        }

        protected internal override void dealFireDamage(int i1)
        {
            attackEntityFrom(DamageSource.inFire, i1);
        }

        public override bool attackEntityFrom(DamageSource damageSource1, int i2)
        {
            setBeenAttacked();
            xpOrbHealth -= i2;
            if (xpOrbHealth <= 0)
            {
                setDead();
            }

            return false;
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            nBTTagCompound1.setShort("Health", (sbyte)xpOrbHealth);
            nBTTagCompound1.setShort("Age", (short)xpOrbAge);
            nBTTagCompound1.setShort("Value", (short)xpValue);
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            xpOrbHealth = nBTTagCompound1.getShort("Health") & 255;
            xpOrbAge = nBTTagCompound1.getShort("Age");
            xpValue = nBTTagCompound1.getShort("Value");
        }

        public override void onCollideWithPlayer(EntityPlayer entityPlayer1)
        {
            if (!worldObj.isRemote)
            {
                if (field_35126_c == 0 && entityPlayer1.xpCooldown == 0)
                {
                    entityPlayer1.xpCooldown = 2;
                    worldObj.playSoundAtEntity(this, "random.orb", 0.1F, 0.5F * ((rand.NextSingle() - rand.NextSingle()) * 0.7F + 1.8F));
                    entityPlayer1.onItemPickup(this, 1);
                    entityPlayer1.addExperience(xpValue);
                    setDead();
                }

            }
        }

        public virtual int XpValue
        {
            get
            {
                return xpValue;
            }
        }

        public virtual int TextureByXP
        {
            get
            {
                return xpValue >= 2477 ? 10 : xpValue >= 1237 ? 9 : xpValue >= 617 ? 8 : xpValue >= 307 ? 7 : xpValue >= 149 ? 6 : xpValue >= 73 ? 5 : xpValue >= 37 ? 4 : xpValue >= 17 ? 3 : xpValue >= 7 ? 2 : xpValue >= 3 ? 1 : 0;
            }
        }

        public static int getXPSplit(int i0)
        {
            return i0 >= 2477 ? 2477 : i0 >= 1237 ? 1237 : i0 >= 617 ? 617 : i0 >= 307 ? 307 : i0 >= 149 ? 149 : i0 >= 73 ? 73 : i0 >= 37 ? 37 : i0 >= 17 ? 17 : i0 >= 7 ? 7 : i0 >= 3 ? 3 : 1;
        }

        public override bool canAttackWithItem()
        {
            return false;
        }
    }

}