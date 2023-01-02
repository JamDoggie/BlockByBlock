using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render.model
{

    public class ModelChicken : ModelBase
    {
        public ModelRenderer head;
        public ModelRenderer body;
        public ModelRenderer rightLeg;
        public ModelRenderer leftLeg;
        public ModelRenderer rightWing;
        public ModelRenderer leftWing;
        public ModelRenderer bill;
        public ModelRenderer chin;

        public ModelChicken()
        {
            sbyte b1 = 16;
            head = new ModelRenderer(this, 0, 0);
            head.addBox(-2.0F, -6.0F, -2.0F, 4, 6, 3, 0.0F);
            head.setRotationPoint(0.0F, -1 + b1, -4.0F);
            bill = new ModelRenderer(this, 14, 0);
            bill.addBox(-2.0F, -4.0F, -4.0F, 4, 2, 2, 0.0F);
            bill.setRotationPoint(0.0F, -1 + b1, -4.0F);
            chin = new ModelRenderer(this, 14, 4);
            chin.addBox(-1.0F, -2.0F, -3.0F, 2, 2, 2, 0.0F);
            chin.setRotationPoint(0.0F, -1 + b1, -4.0F);
            body = new ModelRenderer(this, 0, 9);
            body.addBox(-3.0F, -4.0F, -3.0F, 6, 8, 6, 0.0F);
            body.setRotationPoint(0.0F, b1, 0.0F);
            rightLeg = new ModelRenderer(this, 26, 0);
            rightLeg.addBox(-1.0F, 0.0F, -3.0F, 3, 5, 3);
            rightLeg.setRotationPoint(-2.0F, 3 + b1, 1.0F);
            leftLeg = new ModelRenderer(this, 26, 0);
            leftLeg.addBox(-1.0F, 0.0F, -3.0F, 3, 5, 3);
            leftLeg.setRotationPoint(1.0F, 3 + b1, 1.0F);
            rightWing = new ModelRenderer(this, 24, 13);
            rightWing.addBox(0.0F, 0.0F, -3.0F, 1, 4, 6);
            rightWing.setRotationPoint(-4.0F, -3 + b1, 0.0F);
            leftWing = new ModelRenderer(this, 24, 13);
            leftWing.addBox(-1.0F, 0.0F, -3.0F, 1, 4, 6);
            leftWing.setRotationPoint(4.0F, -3 + b1, 0.0F);
        }

        public override void render(Entity entity1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
            setRotationAngles(f2, f3, f4, f5, f6, f7);
            if (isChild)
            {
                float f8 = 2.0F;
                Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 5.0F * f7, 2.0F * f7);
                head.render(f7);
                bill.render(f7);
                chin.render(f7);
                Minecraft.renderPipeline.ModelMatrix.PopMatrix();
                Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                Minecraft.renderPipeline.ModelMatrix.Scale(1.0F / f8, 1.0F / f8, 1.0F / f8);
                Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 24.0F * f7, 0.0F);
                body.render(f7);
                rightLeg.render(f7);
                leftLeg.render(f7);
                rightWing.render(f7);
                leftWing.render(f7);
                Minecraft.renderPipeline.ModelMatrix.PopMatrix();
            }
            else
            {
                head.render(f7);
                bill.render(f7);
                chin.render(f7);
                body.render(f7);
                rightLeg.render(f7);
                leftLeg.render(f7);
                rightWing.render(f7);
                leftWing.render(f7);
            }

        }

        public override void setRotationAngles(float f1, float f2, float f3, float f4, float f5, float f6)
        {
            head.rotateAngleX = -(f5 / 57.295776F);
            head.rotateAngleY = f4 / 57.295776F;
            bill.rotateAngleX = head.rotateAngleX;
            bill.rotateAngleY = head.rotateAngleY;
            chin.rotateAngleX = head.rotateAngleX;
            chin.rotateAngleY = head.rotateAngleY;
            body.rotateAngleX = (float)Math.PI / 2F;
            rightLeg.rotateAngleX = MathHelper.cos(f1 * 0.6662F) * 1.4F * f2;
            leftLeg.rotateAngleX = MathHelper.cos(f1 * 0.6662F + (float)Math.PI) * 1.4F * f2;
            rightWing.rotateAngleZ = f3;
            leftWing.rotateAngleZ = -f3;
        }
    }

}