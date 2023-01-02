using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render.model
{

    public class ModelWolf : ModelBase
    {
        public ModelRenderer wolfHeadMain;
        public ModelRenderer wolfBody;
        public ModelRenderer wolfLeg1;
        public ModelRenderer wolfLeg2;
        public ModelRenderer wolfLeg3;
        public ModelRenderer wolfLeg4;
        internal ModelRenderer wolfTail;
        internal ModelRenderer wolfMane;

        public ModelWolf()
        {
            float f1 = 0.0F;
            float f2 = 13.5F;
            wolfHeadMain = new ModelRenderer(this, 0, 0);
            wolfHeadMain.addBox(-3.0F, -3.0F, -2.0F, 6, 6, 4, f1);
            wolfHeadMain.setRotationPoint(-1.0F, f2, -7.0F);
            wolfBody = new ModelRenderer(this, 18, 14);
            wolfBody.addBox(-4.0F, -2.0F, -3.0F, 6, 9, 6, f1);
            wolfBody.setRotationPoint(0.0F, 14.0F, 2.0F);
            wolfMane = new ModelRenderer(this, 21, 0);
            wolfMane.addBox(-4.0F, -3.0F, -3.0F, 8, 6, 7, f1);
            wolfMane.setRotationPoint(-1.0F, 14.0F, 2.0F);
            wolfLeg1 = new ModelRenderer(this, 0, 18);
            wolfLeg1.addBox(-1.0F, 0.0F, -1.0F, 2, 8, 2, f1);
            wolfLeg1.setRotationPoint(-2.5F, 16.0F, 7.0F);
            wolfLeg2 = new ModelRenderer(this, 0, 18);
            wolfLeg2.addBox(-1.0F, 0.0F, -1.0F, 2, 8, 2, f1);
            wolfLeg2.setRotationPoint(0.5F, 16.0F, 7.0F);
            wolfLeg3 = new ModelRenderer(this, 0, 18);
            wolfLeg3.addBox(-1.0F, 0.0F, -1.0F, 2, 8, 2, f1);
            wolfLeg3.setRotationPoint(-2.5F, 16.0F, -4.0F);
            wolfLeg4 = new ModelRenderer(this, 0, 18);
            wolfLeg4.addBox(-1.0F, 0.0F, -1.0F, 2, 8, 2, f1);
            wolfLeg4.setRotationPoint(0.5F, 16.0F, -4.0F);
            wolfTail = new ModelRenderer(this, 9, 18);
            wolfTail.addBox(-1.0F, 0.0F, -1.0F, 2, 8, 2, f1);
            wolfTail.setRotationPoint(-1.0F, 12.0F, 8.0F);
            wolfHeadMain.setTextureOffset(16, 14).addBox(-3.0F, -5.0F, 0.0F, 2, 2, 1, f1);
            wolfHeadMain.setTextureOffset(16, 14).addBox(1.0F, -5.0F, 0.0F, 2, 2, 1, f1);
            wolfHeadMain.setTextureOffset(0, 10).addBox(-1.5F, 0.0F, -5.0F, 3, 3, 4, f1);
        }

        public override void render(Entity entity1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
            base.render(entity1, f2, f3, f4, f5, f6, f7);
            setRotationAngles(f2, f3, f4, f5, f6, f7);
            if (isChild)
            {
                float f8 = 2.0F;
                Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 5.0F * f7, 2.0F * f7);
                wolfHeadMain.renderWithRotation(f7);
                Minecraft.renderPipeline.ModelMatrix.PopMatrix();
                Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                Minecraft.renderPipeline.ModelMatrix.Scale(1.0F / f8, 1.0F / f8, 1.0F / f8);
                Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 24.0F * f7, 0.0F);
                wolfBody.render(f7);
                wolfLeg1.render(f7);
                wolfLeg2.render(f7);
                wolfLeg3.render(f7);
                wolfLeg4.render(f7);
                wolfTail.renderWithRotation(f7);
                wolfMane.render(f7);
                Minecraft.renderPipeline.ModelMatrix.PopMatrix();
            }
            else
            {
                wolfHeadMain.renderWithRotation(f7);
                wolfBody.render(f7);
                wolfLeg1.render(f7);
                wolfLeg2.render(f7);
                wolfLeg3.render(f7);
                wolfLeg4.render(f7);
                wolfTail.renderWithRotation(f7);
                wolfMane.render(f7);
            }

        }

        public override void setLivingAnimations(EntityLiving entityLiving1, float f2, float f3, float f4)
        {
            EntityWolf entityWolf5 = (EntityWolf)entityLiving1;
            if (entityWolf5.Angry)
            {
                wolfTail.rotateAngleY = 0.0F;
            }
            else
            {
                wolfTail.rotateAngleY = MathHelper.cos(f2 * 0.6662F) * 1.4F * f3;
            }

            if (entityWolf5.Sitting)
            {
                wolfMane.setRotationPoint(-1.0F, 16.0F, -3.0F);
                wolfMane.rotateAngleX = 1.2566371F;
                wolfMane.rotateAngleY = 0.0F;
                wolfBody.setRotationPoint(0.0F, 18.0F, 0.0F);
                wolfBody.rotateAngleX = 0.7853982F;
                wolfTail.setRotationPoint(-1.0F, 21.0F, 6.0F);
                wolfLeg1.setRotationPoint(-2.5F, 22.0F, 2.0F);
                wolfLeg1.rotateAngleX = 4.712389F;
                wolfLeg2.setRotationPoint(0.5F, 22.0F, 2.0F);
                wolfLeg2.rotateAngleX = 4.712389F;
                wolfLeg3.rotateAngleX = 5.811947F;
                wolfLeg3.setRotationPoint(-2.49F, 17.0F, -4.0F);
                wolfLeg4.rotateAngleX = 5.811947F;
                wolfLeg4.setRotationPoint(0.51F, 17.0F, -4.0F);
            }
            else
            {
                wolfBody.setRotationPoint(0.0F, 14.0F, 2.0F);
                wolfBody.rotateAngleX = (float)Math.PI / 2F;
                wolfMane.setRotationPoint(-1.0F, 14.0F, -3.0F);
                wolfMane.rotateAngleX = wolfBody.rotateAngleX;
                wolfTail.setRotationPoint(-1.0F, 12.0F, 8.0F);
                wolfLeg1.setRotationPoint(-2.5F, 16.0F, 7.0F);
                wolfLeg2.setRotationPoint(0.5F, 16.0F, 7.0F);
                wolfLeg3.setRotationPoint(-2.5F, 16.0F, -4.0F);
                wolfLeg4.setRotationPoint(0.5F, 16.0F, -4.0F);
                wolfLeg1.rotateAngleX = MathHelper.cos(f2 * 0.6662F) * 1.4F * f3;
                wolfLeg2.rotateAngleX = MathHelper.cos(f2 * 0.6662F + (float)Math.PI) * 1.4F * f3;
                wolfLeg3.rotateAngleX = MathHelper.cos(f2 * 0.6662F + (float)Math.PI) * 1.4F * f3;
                wolfLeg4.rotateAngleX = MathHelper.cos(f2 * 0.6662F) * 1.4F * f3;
            }

            wolfHeadMain.rotateAngleZ = entityWolf5.getInterestedAngle(f4) + entityWolf5.getShakeAngle(f4, 0.0F);
            wolfMane.rotateAngleZ = entityWolf5.getShakeAngle(f4, -0.08F);
            wolfBody.rotateAngleZ = entityWolf5.getShakeAngle(f4, -0.16F);
            wolfTail.rotateAngleZ = entityWolf5.getShakeAngle(f4, -0.2F);
            if (entityWolf5.WolfShaking)
            {
                float f6 = entityWolf5.getBrightness(f4) * entityWolf5.getShadingWhileShaking(f4);
                Minecraft.renderPipeline.SetColor(f6, f6, f6);
            }

        }

        public override void setRotationAngles(float f1, float f2, float f3, float f4, float f5, float f6)
        {
            base.setRotationAngles(f1, f2, f3, f4, f5, f6);
            wolfHeadMain.rotateAngleX = f5 / 57.295776F;
            wolfHeadMain.rotateAngleY = f4 / 57.295776F;
            wolfTail.rotateAngleX = f3;
        }
    }

}