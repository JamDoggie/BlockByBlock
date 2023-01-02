using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public abstract class EntityWaterMob : EntityCreature
    {
        public EntityWaterMob(World world1) : base(world1)
        {
        }

        public override bool canBreatheUnderwater()
        {
            return true;
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            base.writeEntityToNBT(nBTTagCompound1);
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            base.readEntityFromNBT(nBTTagCompound1);
        }

        public override bool CanSpawnHere
        {
            get
            {
                return worldObj.checkIfAABBIsClear(boundingBox);
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
            return true;
        }

        protected internal override int getExperiencePoints(EntityPlayer entityPlayer1)
        {
            return 1 + worldObj.rand.Next(3);
        }
    }

}