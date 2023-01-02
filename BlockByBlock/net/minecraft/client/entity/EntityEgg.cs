using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntityEgg : EntityThrowable
    {
        public EntityEgg(World world1) : base(world1)
        {
        }

        public EntityEgg(World world1, EntityLiving entityLiving2) : base(world1, entityLiving2)
        {
        }

        public EntityEgg(World world1, double d2, double d4, double d6) : base(world1, d2, d4, d6)
        {
        }

        protected internal override void onImpact(MovingObjectPosition movingObjectPosition1)
        {
            if (movingObjectPosition1.entityHit != null && movingObjectPosition1.entityHit.attackEntityFrom(DamageSource.causeThrownDamage(this, thrower), 0))
            {
                ;
            }

            if (!worldObj.isRemote && rand.Next(8) == 0)
            {
                sbyte b2 = 1;
                if (rand.Next(32) == 0)
                {
                    b2 = 4;
                }

                for (int i3 = 0; i3 < b2; ++i3)
                {
                    EntityChicken entityChicken4 = new EntityChicken(worldObj);
                    entityChicken4.GrowingAge = -24000;
                    entityChicken4.setLocationAndAngles(posX, posY, posZ, rotationYaw, 0.0F);
                    worldObj.spawnEntityInWorld(entityChicken4);
                }
            }

            for (int i5 = 0; i5 < 8; ++i5)
            {
                worldObj.spawnParticle("snowballpoof", posX, posY, posZ, 0.0D, 0.0D, 0.0D);
            }

            if (!worldObj.isRemote)
            {
                setDead();
            }

        }
    }

}