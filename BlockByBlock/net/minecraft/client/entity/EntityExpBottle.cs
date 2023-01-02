using System;
using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntityExpBottle : EntityThrowable
    {
        public EntityExpBottle(World world1) : base(world1)
        {
        }

        public EntityExpBottle(World world1, EntityLiving entityLiving2) : base(world1, entityLiving2)
        {
        }

        public EntityExpBottle(World world1, double d2, double d4, double d6) : base(world1, d2, d4, d6)
        {
        }

        protected internal override float func_40075_e()
        {
            return 0.07F;
        }

        protected internal override float func_40077_c()
        {
            return 0.7F;
        }

        protected internal override float func_40074_d()
        {
            return -20.0F;
        }

        protected internal override void onImpact(MovingObjectPosition movingObjectPosition1)
        {
            if (!worldObj.isRemote)
            {
                worldObj.playAuxSFX(2002, (int)(long)Math.Round(posX, MidpointRounding.AwayFromZero), (int)(long)Math.Round(posY, MidpointRounding.AwayFromZero), (int)(long)Math.Round(posZ, MidpointRounding.AwayFromZero), 0);
                int i2 = 3 + worldObj.rand.Next(5) + worldObj.rand.Next(5);

                while (i2 > 0)
                {
                    int i3 = EntityXPOrb.getXPSplit(i2);
                    i2 -= i3;
                    worldObj.spawnEntityInWorld(new EntityXPOrb(worldObj, posX, posY, posZ, i3));
                }

                setDead();
            }

        }
    }

}