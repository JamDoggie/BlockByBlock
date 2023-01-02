using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntityGiantZombie : EntityMob
    {
        public EntityGiantZombie(World world1) : base(world1)
        {
            texture = "/mob/zombie.png";
            moveSpeed = 0.5F;
            attackStrength = 50;
            yOffset *= 6.0F;
            SetSize(width * 6.0F, height * 6.0F);
        }

        public override int MaxHealth
        {
            get
            {
                return 100;
            }
        }

        public override float getBlockPathWeight(int i1, int i2, int i3)
        {
            return worldObj.getLightBrightness(i1, i2, i3) - 0.5F;
        }
    }

}