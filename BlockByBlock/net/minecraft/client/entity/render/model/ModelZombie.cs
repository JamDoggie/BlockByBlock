using net.minecraft.src;

namespace net.minecraft.client.entity.render.model
{
    public class ModelZombie : ModelBiped
    {
        public override void setRotationAngles(float f1, float f2, float f3, float f4, float f5, float f6)
        {
            base.setRotationAngles(f1, f2, f3, f4, f5, f6);
            float f7 = MathHelper.sin(onGround * (float)Math.PI);
            float f8 = MathHelper.sin((1.0F - (1.0F - onGround) * (1.0F - onGround)) * (float)Math.PI);
            bipedRightArm.rotateAngleZ = 0.0F;
            bipedLeftArm.rotateAngleZ = 0.0F;
            bipedRightArm.rotateAngleY = -(0.1F - f7 * 0.6F);
            bipedLeftArm.rotateAngleY = 0.1F - f7 * 0.6F;
            bipedRightArm.rotateAngleX = -1.5707964F;
            bipedLeftArm.rotateAngleX = -1.5707964F;
            bipedRightArm.rotateAngleX -= f7 * 1.2F - f8 * 0.4F;
            bipedLeftArm.rotateAngleX -= f7 * 1.2F - f8 * 0.4F;
            bipedRightArm.rotateAngleZ += MathHelper.cos(f3 * 0.09F) * 0.05F + 0.05F;
            bipedLeftArm.rotateAngleZ -= MathHelper.cos(f3 * 0.09F) * 0.05F + 0.05F;
            bipedRightArm.rotateAngleX += MathHelper.sin(f3 * 0.067F) * 0.05F;
            bipedLeftArm.rotateAngleX -= MathHelper.sin(f3 * 0.067F) * 0.05F;
        }
    }

}