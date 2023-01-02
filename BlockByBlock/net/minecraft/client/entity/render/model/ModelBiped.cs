using net.minecraft.client.entity;
using net.minecraft.src;

namespace net.minecraft.client.entity.render.model
{
    public class ModelBiped : ModelBase
    {
        public ModelRenderer bipedHead;
        public ModelRenderer bipedHeadwear;
        public ModelRenderer bipedBody;
        public ModelRenderer bipedRightArm;
        public ModelRenderer bipedLeftArm;
        public ModelRenderer bipedRightLeg;
        public ModelRenderer bipedLeftLeg;
        public ModelRenderer bipedEars;
        public ModelRenderer bipedCloak;
        public int heldItemLeft;
        public int heldItemRight;
        public bool isSneak;
        public bool aimedBow;

        public ModelBiped() : this(0.0F)
        {
        }

        public ModelBiped(float f1) : this(f1, 0.0F)
        {
        }

        public ModelBiped(float f1, float f2)
        {
            heldItemLeft = 0;
            heldItemRight = 0;
            isSneak = false;
            aimedBow = false;
            bipedCloak = new ModelRenderer(this, 0, 0);
            bipedCloak.addBox(-5.0F, 0.0F, -1.0F, 10, 16, 1, f1);
            bipedEars = new ModelRenderer(this, 24, 0);
            bipedEars.addBox(-3.0F, -6.0F, -1.0F, 6, 6, 1, f1);
            bipedHead = new ModelRenderer(this, 0, 0);
            bipedHead.addBox(-4.0F, -8.0F, -4.0F, 8, 8, 8, f1);
            bipedHead.setRotationPoint(0.0F, 0.0F + f2, 0.0F);
            bipedHeadwear = new ModelRenderer(this, 32, 0);
            bipedHeadwear.addBox(-4.0F, -8.0F, -4.0F, 8, 8, 8, f1 + 0.5F);
            bipedHeadwear.setRotationPoint(0.0F, 0.0F + f2, 0.0F);
            bipedBody = new ModelRenderer(this, 16, 16);
            bipedBody.addBox(-4.0F, 0.0F, -2.0F, 8, 12, 4, f1);
            bipedBody.setRotationPoint(0.0F, 0.0F + f2, 0.0F);
            bipedRightArm = new ModelRenderer(this, 40, 16);
            bipedRightArm.addBox(-3.0F, -2.0F, -2.0F, 4, 12, 4, f1);
            bipedRightArm.setRotationPoint(-5.0F, 2.0F + f2, 0.0F);
            bipedLeftArm = new ModelRenderer(this, 40, 16);
            bipedLeftArm.mirror = true;
            bipedLeftArm.addBox(-1.0F, -2.0F, -2.0F, 4, 12, 4, f1);
            bipedLeftArm.setRotationPoint(5.0F, 2.0F + f2, 0.0F);
            bipedRightLeg = new ModelRenderer(this, 0, 16);
            bipedRightLeg.addBox(-2.0F, 0.0F, -2.0F, 4, 12, 4, f1);
            bipedRightLeg.setRotationPoint(-2.0F, 12.0F + f2, 0.0F);
            bipedLeftLeg = new ModelRenderer(this, 0, 16);
            bipedLeftLeg.mirror = true;
            bipedLeftLeg.addBox(-2.0F, 0.0F, -2.0F, 4, 12, 4, f1);
            bipedLeftLeg.setRotationPoint(2.0F, 12.0F + f2, 0.0F);
        }

        public override void render(Entity entity1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
            setRotationAngles(f2, f3, f4, f5, f6, f7);
            bipedHead.render(f7);
            bipedBody.render(f7);
            bipedRightArm.render(f7);
            bipedLeftArm.render(f7);
            bipedRightLeg.render(f7);
            bipedLeftLeg.render(f7);
            bipedHeadwear.render(f7);
        }

        public override void setRotationAngles(float f1, float f2, float f3, float f4, float f5, float f6)
        {
            bipedHead.rotateAngleY = f4 / 57.295776F;
            bipedHead.rotateAngleX = f5 / 57.295776F;
            bipedHeadwear.rotateAngleY = bipedHead.rotateAngleY;
            bipedHeadwear.rotateAngleX = bipedHead.rotateAngleX;
            bipedRightArm.rotateAngleX = MathHelper.cos(f1 * 0.6662F + (float)Math.PI) * 2.0F * f2 * 0.5F;
            bipedLeftArm.rotateAngleX = MathHelper.cos(f1 * 0.6662F) * 2.0F * f2 * 0.5F;
            bipedRightArm.rotateAngleZ = 0.0F;
            bipedLeftArm.rotateAngleZ = 0.0F;
            bipedRightLeg.rotateAngleX = MathHelper.cos(f1 * 0.6662F) * 1.4F * f2;
            bipedLeftLeg.rotateAngleX = MathHelper.cos(f1 * 0.6662F + (float)Math.PI) * 1.4F * f2;
            bipedRightLeg.rotateAngleY = 0.0F;
            bipedLeftLeg.rotateAngleY = 0.0F;
            if (isRiding)
            {
                bipedRightArm.rotateAngleX += -0.62831855F;
                bipedLeftArm.rotateAngleX += -0.62831855F;
                bipedRightLeg.rotateAngleX = -1.2566371F;
                bipedLeftLeg.rotateAngleX = -1.2566371F;
                bipedRightLeg.rotateAngleY = 0.31415927F;
                bipedLeftLeg.rotateAngleY = -0.31415927F;
            }

            if (heldItemLeft != 0)
            {
                bipedLeftArm.rotateAngleX = bipedLeftArm.rotateAngleX * 0.5F - 0.31415927F * heldItemLeft;
            }

            if (heldItemRight != 0)
            {
                bipedRightArm.rotateAngleX = bipedRightArm.rotateAngleX * 0.5F - 0.31415927F * heldItemRight;
            }

            bipedRightArm.rotateAngleY = 0.0F;
            bipedLeftArm.rotateAngleY = 0.0F;
            float f7;
            float f8;
            if (onGround > -9990.0F)
            {
                f7 = onGround;
                bipedBody.rotateAngleY = MathHelper.sin(MathHelper.sqrt_float(f7) * (float)Math.PI * 2.0F) * 0.2F;
                bipedRightArm.rotationPointZ = MathHelper.sin(bipedBody.rotateAngleY) * 5.0F;
                bipedRightArm.rotationPointX = -MathHelper.cos(bipedBody.rotateAngleY) * 5.0F;
                bipedLeftArm.rotationPointZ = -MathHelper.sin(bipedBody.rotateAngleY) * 5.0F;
                bipedLeftArm.rotationPointX = MathHelper.cos(bipedBody.rotateAngleY) * 5.0F;
                bipedRightArm.rotateAngleY += bipedBody.rotateAngleY;
                bipedLeftArm.rotateAngleY += bipedBody.rotateAngleY;
                bipedLeftArm.rotateAngleX += bipedBody.rotateAngleY;
                f7 = 1.0F - onGround;
                f7 *= f7;
                f7 *= f7;
                f7 = 1.0F - f7;
                f8 = MathHelper.sin(f7 * (float)Math.PI);
                float f9 = MathHelper.sin(onGround * (float)Math.PI) * -(bipedHead.rotateAngleX - 0.7F) * 0.75F;
                bipedRightArm.rotateAngleX = (float)(bipedRightArm.rotateAngleX - ((double)f8 * 1.2D + (double)f9));
                bipedRightArm.rotateAngleY += bipedBody.rotateAngleY * 2.0F;
                bipedRightArm.rotateAngleZ = MathHelper.sin(onGround * (float)Math.PI) * -0.4F;
            }

            if (isSneak)
            {
                bipedBody.rotateAngleX = 0.5F;
                bipedRightArm.rotateAngleX += 0.4F;
                bipedLeftArm.rotateAngleX += 0.4F;
                bipedRightLeg.rotationPointZ = 4.0F;
                bipedLeftLeg.rotationPointZ = 4.0F;
                bipedRightLeg.rotationPointY = 9.0F;
                bipedLeftLeg.rotationPointY = 9.0F;
                bipedHead.rotationPointY = 1.0F;
            }
            else
            {
                bipedBody.rotateAngleX = 0.0F;
                bipedRightLeg.rotationPointZ = 0.0F;
                bipedLeftLeg.rotationPointZ = 0.0F;
                bipedRightLeg.rotationPointY = 12.0F;
                bipedLeftLeg.rotationPointY = 12.0F;
                bipedHead.rotationPointY = 0.0F;
            }

            bipedRightArm.rotateAngleZ += MathHelper.cos(f3 * 0.09F) * 0.05F + 0.05F;
            bipedLeftArm.rotateAngleZ -= MathHelper.cos(f3 * 0.09F) * 0.05F + 0.05F;
            bipedRightArm.rotateAngleX += MathHelper.sin(f3 * 0.067F) * 0.05F;
            bipedLeftArm.rotateAngleX -= MathHelper.sin(f3 * 0.067F) * 0.05F;
            if (aimedBow)
            {
                f7 = 0.0F;
                f8 = 0.0F;
                bipedRightArm.rotateAngleZ = 0.0F;
                bipedLeftArm.rotateAngleZ = 0.0F;
                bipedRightArm.rotateAngleY = -(0.1F - f7 * 0.6F) + bipedHead.rotateAngleY;
                bipedLeftArm.rotateAngleY = 0.1F - f7 * 0.6F + bipedHead.rotateAngleY + 0.4F;
                bipedRightArm.rotateAngleX = -1.5707964F + bipedHead.rotateAngleX;
                bipedLeftArm.rotateAngleX = -1.5707964F + bipedHead.rotateAngleX;
                bipedRightArm.rotateAngleX -= f7 * 1.2F - f8 * 0.4F;
                bipedLeftArm.rotateAngleX -= f7 * 1.2F - f8 * 0.4F;
                bipedRightArm.rotateAngleZ += MathHelper.cos(f3 * 0.09F) * 0.05F + 0.05F;
                bipedLeftArm.rotateAngleZ -= MathHelper.cos(f3 * 0.09F) * 0.05F + 0.05F;
                bipedRightArm.rotateAngleX += MathHelper.sin(f3 * 0.067F) * 0.05F;
                bipedLeftArm.rotateAngleX -= MathHelper.sin(f3 * 0.067F) * 0.05F;
            }

        }

        public virtual void renderEars(float f1)
        {
            bipedEars.rotateAngleY = bipedHead.rotateAngleY;
            bipedEars.rotateAngleX = bipedHead.rotateAngleX;
            bipedEars.rotationPointX = 0.0F;
            bipedEars.rotationPointY = 0.0F;
            bipedEars.render(f1);
        }

        public virtual void renderCloak(float f1)
        {
            bipedCloak.render(f1);
        }
    }

}