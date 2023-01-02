using System;
using net.minecraft.client.entity;
using net.minecraft.src;

namespace net.minecraft.client.entity.render.model
{
    public class ModelSpider : ModelBase
    {
        public ModelRenderer spiderHead;
        public ModelRenderer spiderNeck;
        public ModelRenderer spiderBody;
        public ModelRenderer spiderLeg1;
        public ModelRenderer spiderLeg2;
        public ModelRenderer spiderLeg3;
        public ModelRenderer spiderLeg4;
        public ModelRenderer spiderLeg5;
        public ModelRenderer spiderLeg6;
        public ModelRenderer spiderLeg7;
        public ModelRenderer spiderLeg8;

        public ModelSpider()
        {
            float f1 = 0.0F;
            sbyte b2 = 15;
            spiderHead = new ModelRenderer(this, 32, 4);
            spiderHead.addBox(-4.0F, -4.0F, -8.0F, 8, 8, 8, f1);
            spiderHead.setRotationPoint(0.0F, b2, -3.0F);
            spiderNeck = new ModelRenderer(this, 0, 0);
            spiderNeck.addBox(-3.0F, -3.0F, -3.0F, 6, 6, 6, f1);
            spiderNeck.setRotationPoint(0.0F, b2, 0.0F);
            spiderBody = new ModelRenderer(this, 0, 12);
            spiderBody.addBox(-5.0F, -4.0F, -6.0F, 10, 8, 12, f1);
            spiderBody.setRotationPoint(0.0F, b2, 9.0F);
            spiderLeg1 = new ModelRenderer(this, 18, 0);
            spiderLeg1.addBox(-15.0F, -1.0F, -1.0F, 16, 2, 2, f1);
            spiderLeg1.setRotationPoint(-4.0F, b2, 2.0F);
            spiderLeg2 = new ModelRenderer(this, 18, 0);
            spiderLeg2.addBox(-1.0F, -1.0F, -1.0F, 16, 2, 2, f1);
            spiderLeg2.setRotationPoint(4.0F, b2, 2.0F);
            spiderLeg3 = new ModelRenderer(this, 18, 0);
            spiderLeg3.addBox(-15.0F, -1.0F, -1.0F, 16, 2, 2, f1);
            spiderLeg3.setRotationPoint(-4.0F, b2, 1.0F);
            spiderLeg4 = new ModelRenderer(this, 18, 0);
            spiderLeg4.addBox(-1.0F, -1.0F, -1.0F, 16, 2, 2, f1);
            spiderLeg4.setRotationPoint(4.0F, b2, 1.0F);
            spiderLeg5 = new ModelRenderer(this, 18, 0);
            spiderLeg5.addBox(-15.0F, -1.0F, -1.0F, 16, 2, 2, f1);
            spiderLeg5.setRotationPoint(-4.0F, b2, 0.0F);
            spiderLeg6 = new ModelRenderer(this, 18, 0);
            spiderLeg6.addBox(-1.0F, -1.0F, -1.0F, 16, 2, 2, f1);
            spiderLeg6.setRotationPoint(4.0F, b2, 0.0F);
            spiderLeg7 = new ModelRenderer(this, 18, 0);
            spiderLeg7.addBox(-15.0F, -1.0F, -1.0F, 16, 2, 2, f1);
            spiderLeg7.setRotationPoint(-4.0F, b2, -1.0F);
            spiderLeg8 = new ModelRenderer(this, 18, 0);
            spiderLeg8.addBox(-1.0F, -1.0F, -1.0F, 16, 2, 2, f1);
            spiderLeg8.setRotationPoint(4.0F, b2, -1.0F);
        }

        public override void render(Entity entity1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
            setRotationAngles(f2, f3, f4, f5, f6, f7);
            spiderHead.render(f7);
            spiderNeck.render(f7);
            spiderBody.render(f7);
            spiderLeg1.render(f7);
            spiderLeg2.render(f7);
            spiderLeg3.render(f7);
            spiderLeg4.render(f7);
            spiderLeg5.render(f7);
            spiderLeg6.render(f7);
            spiderLeg7.render(f7);
            spiderLeg8.render(f7);
        }

        public override void setRotationAngles(float f1, float f2, float f3, float f4, float f5, float f6)
        {
            spiderHead.rotateAngleY = f4 / 57.295776F;
            spiderHead.rotateAngleX = f5 / 57.295776F;
            float f7 = 0.7853982F;
            spiderLeg1.rotateAngleZ = -f7;
            spiderLeg2.rotateAngleZ = f7;
            spiderLeg3.rotateAngleZ = -f7 * 0.74F;
            spiderLeg4.rotateAngleZ = f7 * 0.74F;
            spiderLeg5.rotateAngleZ = -f7 * 0.74F;
            spiderLeg6.rotateAngleZ = f7 * 0.74F;
            spiderLeg7.rotateAngleZ = -f7;
            spiderLeg8.rotateAngleZ = f7;
            float f8 = -0.0F;
            float f9 = 0.3926991F;
            spiderLeg1.rotateAngleY = f9 * 2.0F + f8;
            spiderLeg2.rotateAngleY = -f9 * 2.0F - f8;
            spiderLeg3.rotateAngleY = f9 * 1.0F + f8;
            spiderLeg4.rotateAngleY = -f9 * 1.0F - f8;
            spiderLeg5.rotateAngleY = -f9 * 1.0F + f8;
            spiderLeg6.rotateAngleY = f9 * 1.0F - f8;
            spiderLeg7.rotateAngleY = -f9 * 2.0F + f8;
            spiderLeg8.rotateAngleY = f9 * 2.0F - f8;
            float f10 = -(MathHelper.cos(f1 * 0.6662F * 2.0F + 0.0F) * 0.4F) * f2;
            float f11 = -(MathHelper.cos(f1 * 0.6662F * 2.0F + (float)Math.PI) * 0.4F) * f2;
            float f12 = -(MathHelper.cos(f1 * 0.6662F * 2.0F + (float)Math.PI / 2F) * 0.4F) * f2;
            float f13 = -(MathHelper.cos(f1 * 0.6662F * 2.0F + 4.712389F) * 0.4F) * f2;
            float f14 = Math.Abs(MathHelper.sin(f1 * 0.6662F + 0.0F) * 0.4F) * f2;
            float f15 = Math.Abs(MathHelper.sin(f1 * 0.6662F + (float)Math.PI) * 0.4F) * f2;
            float f16 = Math.Abs(MathHelper.sin(f1 * 0.6662F + (float)Math.PI / 2F) * 0.4F) * f2;
            float f17 = Math.Abs(MathHelper.sin(f1 * 0.6662F + 4.712389F) * 0.4F) * f2;
            spiderLeg1.rotateAngleY += f10;
            spiderLeg2.rotateAngleY += -f10;
            spiderLeg3.rotateAngleY += f11;
            spiderLeg4.rotateAngleY += -f11;
            spiderLeg5.rotateAngleY += f12;
            spiderLeg6.rotateAngleY += -f12;
            spiderLeg7.rotateAngleY += f13;
            spiderLeg8.rotateAngleY += -f13;
            spiderLeg1.rotateAngleZ += f14;
            spiderLeg2.rotateAngleZ += -f14;
            spiderLeg3.rotateAngleZ += f15;
            spiderLeg4.rotateAngleZ += -f15;
            spiderLeg5.rotateAngleZ += f16;
            spiderLeg6.rotateAngleZ += -f16;
            spiderLeg7.rotateAngleZ += f17;
            spiderLeg8.rotateAngleZ += -f17;
        }
    }

}