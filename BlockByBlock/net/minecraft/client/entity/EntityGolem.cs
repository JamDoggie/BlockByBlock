using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public abstract class EntityGolem : EntityCreature
    {
        public EntityGolem(World world1) : base(world1)
        {
        }

        protected internal override void fall(float f1)
        {
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            base.writeEntityToNBT(nBTTagCompound1);
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            base.readEntityFromNBT(nBTTagCompound1);
        }

        protected internal override string LivingSound
        {
            get
            {
                return "none";
            }
        }

        protected internal override string HurtSound
        {
            get
            {
                return "none";
            }
        }

        protected internal override string DeathSound
        {
            get
            {
                return "none";
            }
        }

        public override int TalkInterval
        {
            get
            {
                return 120;
            }
        }

        protected internal override bool canDespawn()
        {
            return false;
        }
    }

}