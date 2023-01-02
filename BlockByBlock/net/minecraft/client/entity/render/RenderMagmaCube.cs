using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.client.entity.render.model;
using OpenTK.Graphics.OpenGL;
using System;

namespace net.minecraft.client.entity.render
{

    public class RenderMagmaCube : RenderLiving
    {
        private bool InstanceFieldsInitialized = false;

        private void InitializeInstanceFields()
        {
            field_40276_c = ((ModelMagmaCube)mainModel).func_40343_a();
        }

        private int field_40276_c;

        public RenderMagmaCube() : base(new ModelMagmaCube(), 0.25F)
        {
            if (!InstanceFieldsInitialized)
            {
                InitializeInstanceFields();
                InstanceFieldsInitialized = true;
            }
        }

        public virtual void renderMagmaCube(EntityMagmaCube entityMagmaCube1, double d2, double d4, double d6, float f8, float f9)
        {
            int i10 = ((ModelMagmaCube)mainModel).func_40343_a();
            if (i10 != field_40276_c)
            {
                field_40276_c = i10;
                mainModel = new ModelMagmaCube();
                Console.WriteLine("new lava slime model");
            }

            base.doRenderLiving(entityMagmaCube1, d2, d4, d6, f8, f9);
        }

        protected internal virtual void scaleMagmaCube(EntityMagmaCube entityMagmaCube1, float f2)
        {
            int i3 = entityMagmaCube1.SlimeSize;
            float f4 = (entityMagmaCube1.field_767_b + (entityMagmaCube1.field_768_a - entityMagmaCube1.field_767_b) * f2) / (i3 * 0.5F + 1.0F);
            float f5 = 1.0F / (f4 + 1.0F);
            float f6 = i3;
            Minecraft.renderPipeline.ModelMatrix.Scale(f5 * f6, 1.0F / f5 * f6, f5 * f6);
        }

        protected internal override void preRenderCallback(EntityLiving entityLiving1, float f2)
        {
            scaleMagmaCube((EntityMagmaCube)entityLiving1, f2);
        }

        public override void doRenderLiving(EntityLiving entityLiving1, double d2, double d4, double d6, float f8, float f9)
        {
            renderMagmaCube((EntityMagmaCube)entityLiving1, d2, d4, d6, f8, f9);
        }

        public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
        {
            renderMagmaCube((EntityMagmaCube)entity1, d2, d4, d6, f8, f9);
        }
    }

}