using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.client.entity.render.model;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render
{

    public class RenderVillager : RenderLiving
    {
        private bool InstanceFieldsInitialized = false;

        private void InitializeInstanceFields()
        {
            field_40295_c = (ModelVillager)mainModel;
        }

        protected internal ModelVillager field_40295_c;

        public RenderVillager() : base(new ModelVillager(0.0F), 0.5F)
        {
            if (!InstanceFieldsInitialized)
            {
                InitializeInstanceFields();
                InstanceFieldsInitialized = true;
            }
        }

        protected internal virtual int func_40293_a(EntityVillager entityVillager1, int i2, float f3)
        {
            return -1;
        }

        public virtual void renderVillager(EntityVillager entityVillager1, double d2, double d4, double d6, float f8, float f9)
        {
            base.doRenderLiving(entityVillager1, d2, d4, d6, f8, f9);
        }

        protected internal virtual void func_40290_a(EntityVillager entityVillager1, double d2, double d4, double d6)
        {
        }

        protected internal virtual void func_40291_a(EntityVillager entityVillager1, float f2)
        {
            base.renderEquippedItems(entityVillager1, f2);
        }

        protected internal virtual void func_40292_b(EntityVillager entityVillager1, float f2)
        {
            float f3 = 0.9375F;
            if (entityVillager1.GrowingAge < 0)
            {
                f3 = (float)((double)f3 * 0.5D);
                shadowSize = 0.25F;
            }
            else
            {
                shadowSize = 0.5F;
            }

            Minecraft.renderPipeline.ModelMatrix.Scale(f3, f3, f3);
        }

        protected internal override void passSpecialRender(EntityLiving entityLiving1, double d2, double d4, double d6)
        {
            func_40290_a((EntityVillager)entityLiving1, d2, d4, d6);
        }

        protected internal override void preRenderCallback(EntityLiving entityLiving1, float f2)
        {
            func_40292_b((EntityVillager)entityLiving1, f2);
        }

        protected internal override int shouldRenderPass(EntityLiving entityLiving1, int i2, float f3)
        {
            return func_40293_a((EntityVillager)entityLiving1, i2, f3);
        }

        protected internal override void renderEquippedItems(EntityLiving entityLiving1, float f2)
        {
            func_40291_a((EntityVillager)entityLiving1, f2);
        }

        public override void doRenderLiving(EntityLiving entityLiving1, double d2, double d4, double d6, float f8, float f9)
        {
            renderVillager((EntityVillager)entityLiving1, d2, d4, d6, f8, f9);
        }

        public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
        {
            renderVillager((EntityVillager)entity1, d2, d4, d6, f8, f9);
        }
    }

}