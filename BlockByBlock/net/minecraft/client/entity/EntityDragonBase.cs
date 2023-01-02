using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntityDragonBase : EntityLiving
    {
        protected internal int maxHealth = 100;

        public EntityDragonBase(World world1) : base(world1)
        {
        }

        public override int MaxHealth
        {
            get
            {
                return maxHealth;
            }
        }

        public virtual bool attackEntityFromPart(EntityDragonPart entityDragonPart1, DamageSource damageSource2, int i3)
        {
            return attackEntityFrom(damageSource2, i3);
        }

        public override bool attackEntityFrom(DamageSource damageSource1, int i2)
        {
            return false;
        }

        protected internal virtual bool superAttackFrom(DamageSource damageSource1, int i2)
        {
            return base.attackEntityFrom(damageSource1, i2);
        }
    }

}