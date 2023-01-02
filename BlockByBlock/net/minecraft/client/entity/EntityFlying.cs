using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public abstract class EntityFlying : EntityLiving
    {
        public EntityFlying(World world1) : base(world1)
        {
        }

        protected internal override void fall(float f1)
        {
        }

        public override void moveEntityWithHeading(float f1, float f2)
        {
            if (InWater)
            {
                moveFlying(f1, f2, 0.02F);
                moveEntity(motionX, motionY, motionZ);
                motionX *= 0.8F;
                motionY *= 0.8F;
                motionZ *= 0.8F;
            }
            else if (handleLavaMovement())
            {
                moveFlying(f1, f2, 0.02F);
                moveEntity(motionX, motionY, motionZ);
                motionX *= 0.5D;
                motionY *= 0.5D;
                motionZ *= 0.5D;
            }
            else
            {
                float f3 = 0.91F;
                if (onGround)
                {
                    f3 = 0.54600006F;
                    int i4 = worldObj.getBlockId(MathHelper.floor_double(posX), MathHelper.floor_double(boundingBox.minY) - 1, MathHelper.floor_double(posZ));
                    if (i4 > 0)
                    {
                        f3 = Block.blocksList[i4].slipperiness * 0.91F;
                    }
                }

                float f8 = 0.16277136F / (f3 * f3 * f3);
                moveFlying(f1, f2, onGround ? 0.1F * f8 : 0.02F);
                f3 = 0.91F;
                if (onGround)
                {
                    f3 = 0.54600006F;
                    int i5 = worldObj.getBlockId(MathHelper.floor_double(posX), MathHelper.floor_double(boundingBox.minY) - 1, MathHelper.floor_double(posZ));
                    if (i5 > 0)
                    {
                        f3 = Block.blocksList[i5].slipperiness * 0.91F;
                    }
                }

                moveEntity(motionX, motionY, motionZ);
                motionX *= f3;
                motionY *= f3;
                motionZ *= f3;
            }

            field_705_Q = field_704_R;
            double d10 = posX - prevPosX;
            double d9 = posZ - prevPosZ;
            float f7 = MathHelper.sqrt_double(d10 * d10 + d9 * d9) * 4.0F;
            if (f7 > 1.0F)
            {
                f7 = 1.0F;
            }

            field_704_R += (f7 - field_704_R) * 0.4F;
            field_703_S += field_704_R;
        }

        public override bool OnLadder
        {
            get
            {
                return false;
            }
        }
    }

}