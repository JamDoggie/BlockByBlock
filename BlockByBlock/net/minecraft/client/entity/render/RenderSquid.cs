using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.client.entity.render.model;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render
{

    public class RenderSquid : RenderLiving
    {
        public RenderSquid(ModelBase modelBase1, float f2) : base(modelBase1, f2)
        {
        }

        public virtual void func_21008_a(EntitySquid entitySquid1, double d2, double d4, double d6, float f8, float f9)
        {
            base.doRenderLiving(entitySquid1, d2, d4, d6, f8, f9);
        }

        protected internal virtual void func_21007_a(EntitySquid entitySquid1, float f2, float f3, float f4)
        {
            float f5 = entitySquid1.field_21088_b + (entitySquid1.field_21089_a - entitySquid1.field_21088_b) * f4;
            float f6 = entitySquid1.field_21086_f + (entitySquid1.field_21087_c - entitySquid1.field_21086_f) * f4;
            Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 0.5F, 0.0F);
            Minecraft.renderPipeline.ModelMatrix.Rotate(180.0F - f3, 0.0F, 1.0F, 0.0F);
            Minecraft.renderPipeline.ModelMatrix.Rotate(f5, 1.0F, 0.0F, 0.0F);
            Minecraft.renderPipeline.ModelMatrix.Rotate(f6, 0.0F, 1.0F, 0.0F);
            Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, -1.2F, 0.0F);
        }

        protected internal virtual void func_21005_a(EntitySquid entitySquid1, float f2)
        {
        }

        protected internal virtual float handleRotationFloat(EntitySquid entitySquid1, float f2)
        {
            float f3 = entitySquid1.lastTentacleAngle + (entitySquid1.tentacleAngle - entitySquid1.lastTentacleAngle) * f2;
            return f3;
        }

        protected internal override void preRenderCallback(EntityLiving entityLiving1, float f2)
        {
            func_21005_a((EntitySquid)entityLiving1, f2);
        }

        protected internal override float handleRotationFloat(EntityLiving entityLiving1, float f2)
        {
            return handleRotationFloat((EntitySquid)entityLiving1, f2);
        }

        protected internal override void rotateCorpse(EntityLiving entityLiving1, float f2, float f3, float f4)
        {
            func_21007_a((EntitySquid)entityLiving1, f2, f3, f4);
        }

        public override void doRenderLiving(EntityLiving entityLiving1, double d2, double d4, double d6, float f8, float f9)
        {
            func_21008_a((EntitySquid)entityLiving1, d2, d4, d6, f8, f9);
        }

        public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
        {
            func_21008_a((EntitySquid)entity1, d2, d4, d6, f8, f9);
        }
    }

}