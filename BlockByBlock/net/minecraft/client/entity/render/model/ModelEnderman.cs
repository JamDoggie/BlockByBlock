using BlockByBlock.net.minecraft.client.entity.render.model;
using net.minecraft.src;

namespace net.minecraft.client.entity.render.model
{
    public class ModelEnderman : ModelBiped
    {
        public bool isCarrying = false;
        public bool isAttacking = false;

        public ModelEnderman() : base(0.0F, -14.0F)
        {
            float f1 = -14.0F;
            float f2 = 0.0F;
            bipedHeadwear = new ModelRenderer(this, 0, 16);
            bipedHeadwear.addBox(-4.0F, -8.0F, -4.0F, 8, 8, 8, f2 - 0.5F);
            bipedHeadwear.setRotationPoint(0.0F, 0.0F + f1, 0.0F);
            bipedBody = new ModelRenderer(this, 32, 16);
            bipedBody.addBox(-4.0F, 0.0F, -2.0F, 8, 12, 4, f2);
            bipedBody.setRotationPoint(0.0F, 0.0F + f1, 0.0F);
            bipedRightArm = new ModelRenderer(this, 56, 0);
            bipedRightArm.addBox(-1.0F, -2.0F, -1.0F, 2, 30, 2, f2);
            bipedRightArm.setRotationPoint(-3.0F, 2.0F + f1, 0.0F);
            bipedLeftArm = new ModelRenderer(this, 56, 0);
            bipedLeftArm.mirror = true;
            bipedLeftArm.addBox(-1.0F, -2.0F, -1.0F, 2, 30, 2, f2);
            bipedLeftArm.setRotationPoint(5.0F, 2.0F + f1, 0.0F);
            bipedRightLeg = new ModelRenderer(this, 56, 0);
            bipedRightLeg.addBox(-1.0F, 0.0F, -1.0F, 2, 30, 2, f2);
            bipedRightLeg.setRotationPoint(-2.0F, 12.0F + f1, 0.0F);
            bipedLeftLeg = new ModelRenderer(this, 56, 0);
            bipedLeftLeg.mirror = true;
            bipedLeftLeg.addBox(-1.0F, 0.0F, -1.0F, 2, 30, 2, f2);
            bipedLeftLeg.setRotationPoint(2.0F, 12.0F + f1, 0.0F);
        }

        public override void setRotationAngles(float f1, float f2, float f3, float f4, float f5, float f6)
        {
            base.setRotationAngles(f1, f2, f3, f4, f5, f6);
            bipedHead.showModel = true;
            float f7 = -14.0F;
            bipedBody.rotateAngleX = 0.0F;
            bipedBody.rotationPointY = f7;
            bipedBody.rotationPointZ = -0.0F;
            bipedRightLeg.rotateAngleX -= 0.0F;
            bipedLeftLeg.rotateAngleX -= 0.0F;
            bipedRightArm.rotateAngleX = (float)(bipedRightArm.rotateAngleX * 0.5D);
            bipedLeftArm.rotateAngleX = (float)(bipedLeftArm.rotateAngleX * 0.5D);
            bipedRightLeg.rotateAngleX = (float)(bipedRightLeg.rotateAngleX * 0.5D);
            bipedLeftLeg.rotateAngleX = (float)(bipedLeftLeg.rotateAngleX * 0.5D);
            float f8 = 0.4F;
            if (bipedRightArm.rotateAngleX > f8)
            {
                bipedRightArm.rotateAngleX = f8;
            }

            if (bipedLeftArm.rotateAngleX > f8)
            {
                bipedLeftArm.rotateAngleX = f8;
            }

            if (bipedRightArm.rotateAngleX < -f8)
            {
                bipedRightArm.rotateAngleX = -f8;
            }

            if (bipedLeftArm.rotateAngleX < -f8)
            {
                bipedLeftArm.rotateAngleX = -f8;
            }

            if (bipedRightLeg.rotateAngleX > f8)
            {
                bipedRightLeg.rotateAngleX = f8;
            }

            if (bipedLeftLeg.rotateAngleX > f8)
            {
                bipedLeftLeg.rotateAngleX = f8;
            }

            if (bipedRightLeg.rotateAngleX < -f8)
            {
                bipedRightLeg.rotateAngleX = -f8;
            }

            if (bipedLeftLeg.rotateAngleX < -f8)
            {
                bipedLeftLeg.rotateAngleX = -f8;
            }

            if (isCarrying)
            {
                bipedRightArm.rotateAngleX = -0.5F;
                bipedLeftArm.rotateAngleX = -0.5F;
                bipedRightArm.rotateAngleZ = 0.05F;
                bipedLeftArm.rotateAngleZ = -0.05F;
            }

            bipedRightArm.rotationPointZ = 0.0F;
            bipedLeftArm.rotationPointZ = 0.0F;
            bipedRightLeg.rotationPointZ = 0.0F;
            bipedLeftLeg.rotationPointZ = 0.0F;
            bipedRightLeg.rotationPointY = 9.0F + f7;
            bipedLeftLeg.rotationPointY = 9.0F + f7;
            bipedHead.rotationPointZ = -0.0F;
            bipedHead.rotationPointY = f7 + 1.0F;
            bipedHeadwear.rotationPointX = bipedHead.rotationPointX;
            bipedHeadwear.rotationPointY = bipedHead.rotationPointY;
            bipedHeadwear.rotationPointZ = bipedHead.rotationPointZ;
            bipedHeadwear.rotateAngleX = bipedHead.rotateAngleX;
            bipedHeadwear.rotateAngleY = bipedHead.rotateAngleY;
            bipedHeadwear.rotateAngleZ = bipedHead.rotateAngleZ;
            if (isAttacking)
            {
                float f9 = 1.0F;
                bipedHead.rotationPointY -= f9 * 5.0F;
            }

        }
    }

}