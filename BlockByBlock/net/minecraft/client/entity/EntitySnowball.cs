using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntitySnowball : EntityThrowable
    {
        public EntitySnowball(World world1) : base(world1)
        {
        }

        public EntitySnowball(World world1, EntityLiving entityLiving2) : base(world1, entityLiving2)
        {
        }

        public EntitySnowball(World world1, double d2, double d4, double d6) : base(world1, d2, d4, d6)
        {
        }

        protected internal override void onImpact(MovingObjectPosition movingObjectPosition1)
        {
            if (movingObjectPosition1.entityHit != null)
            {
                sbyte b2 = 0;
                if (movingObjectPosition1.entityHit is EntityBlaze)
                {
                    b2 = 3;
                }

                if (movingObjectPosition1.entityHit.attackEntityFrom(DamageSource.causeThrownDamage(this, thrower), b2))
                {
                    ; // Incredible
                }
            }

            for (int i3 = 0; i3 < 8; ++i3)
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