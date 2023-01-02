using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render.model
{

    public class ModelQuadruped : ModelBase
    {
        private bool InstanceFieldsInitialized = false;

        private void InitializeInstanceFields()
        {
            head = new ModelRenderer(this, 0, 0);
        }

        public ModelRenderer head;
        public ModelRenderer body;
        public ModelRenderer leg1;
        public ModelRenderer leg2;
        public ModelRenderer leg3;
        public ModelRenderer leg4;
        protected internal float field_40331_g = 8.0F;
        protected internal float field_40332_n = 4.0F;

        public ModelQuadruped(int i1, float f2)
        {
            if (!InstanceFieldsInitialized)
            {
                InitializeInstanceFields();
                InstanceFieldsInitialized = true;
            }
            head.addBox(-4.0F, -4.0F, -8.0F, 8, 8, 8, f2);
            head.setRotationPoint(0.0F, 18 - i1, -6.0F);
            body = new ModelRenderer(this, 28, 8);
            body.addBox(-5.0F, -10.0F, -7.0F, 10, 16, 8, f2);
            body.setRotationPoint(0.0F, 17 - i1, 2.0F);
            leg1 = new ModelRenderer(this, 0, 16);
            leg1.addBox(-2.0F, 0.0F, -2.0F, 4, i1, 4, f2);
            leg1.setRotationPoint(-3.0F, 24 - i1, 7.0F);
            leg2 = new ModelRenderer(this, 0, 16);
            leg2.addBox(-2.0F, 0.0F, -2.0F, 4, i1, 4, f2);
            leg2.setRotationPoint(3.0F, 24 - i1, 7.0F);
            leg3 = new ModelRenderer(this, 0, 16);
            leg3.addBox(-2.0F, 0.0F, -2.0F, 4, i1, 4, f2);
            leg3.setRotationPoint(-3.0F, 24 - i1, -5.0F);
            leg4 = new ModelRenderer(this, 0, 16);
            leg4.addBox(-2.0F, 0.0F, -2.0F, 4, i1, 4, f2);
            leg4.setRotationPoint(3.0F, 24 - i1, -5.0F);
        }

        public override void render(Entity entity1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
            setRotationAngles(f2, f3, f4, f5, f6, f7);
            if (isChild)
            {
                float f8 = 2.0F;
                Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, field_40331_g * f7, field_40332_n * f7);
                head.render(f7);
                Minecraft.renderPipeline.ModelMatrix.PopMatrix();
                Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                Minecraft.renderPipeline.ModelMatrix.Scale(1.0F / f8, 1.0F / f8, 1.0F / f8);
                Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 24.0F * f7, 0.0F);
                body.render(f7);
                leg1.render(f7);
                leg2.render(f7);
                leg3.render(f7);
                leg4.render(f7);
                Minecraft.renderPipeline.ModelMatrix.PopMatrix();
            }
            else
            {
                head.render(f7);
                body.render(f7);
                leg1.render(f7);
                leg2.render(f7);
                leg3.render(f7);
                leg4.render(f7);
            }

        }

        public override void setRotationAngles(float f1, float f2, float f3, float f4, float f5, float f6)
        {
            head.rotateAngleX = f5 / 57.295776F;
            head.rotateAngleY = f4 / 57.295776F;
            body.rotateAngleX = (float)Math.PI / 2F;
            leg1.rotateAngleX = MathHelper.cos(f1 * 0.6662F) * 1.4F * f2;
            leg2.rotateAngleX = MathHelper.cos(f1 * 0.6662F + (float)Math.PI) * 1.4F * f2;
            leg3.rotateAngleX = MathHelper.cos(f1 * 0.6662F + (float)Math.PI) * 1.4F * f2;
            leg4.rotateAngleX = MathHelper.cos(f1 * 0.6662F) * 1.4F * f2;
        }
    }

}