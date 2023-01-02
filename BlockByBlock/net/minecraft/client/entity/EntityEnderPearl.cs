using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntityEnderPearl : EntityThrowable
    {
        public EntityEnderPearl(World world1) : base(world1)
        {
        }

        public EntityEnderPearl(World world1, EntityLiving entityLiving2) : base(world1, entityLiving2)
        {
        }

        public EntityEnderPearl(World world1, double d2, double d4, double d6) : base(world1, d2, d4, d6)
        {
        }

        protected internal override void onImpact(MovingObjectPosition movingObjectPosition1)
        {
            if (movingObjectPosition1.entityHit != null && movingObjectPosition1.entityHit.attackEntityFrom(DamageSource.causeThrownDamage(this, thrower), 0))
            {
                ;
            }

            for (int i2 = 0; i2 < 32; ++i2)
            {
                worldObj.spawnParticle("portal", posX, posY + rand.NextDouble() * 2.0D, posZ, rand.NextGaussian(), 0.0D, rand.NextGaussian());
            }

            if (!worldObj.isRemote)
            {
                if (thrower != null)
                {
                    thrower.setPositionAndUpdate(posX, posY, posZ);
                    thrower.fallDistance = 0.0F;
                    thrower.attackEntityFrom(DamageSource.fall, 5);
                }

                setDead();
            }

        }
    }

}