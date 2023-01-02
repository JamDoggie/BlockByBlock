using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.client.entity.render.model;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render
{

    public class RenderMooshroom : RenderLiving
    {
        public RenderMooshroom(ModelBase modelBase1, float f2) : base(modelBase1, f2)
        {
        }

        public virtual void func_40273_a(EntityMooshroom entityMooshroom1, double d2, double d4, double d6, float f8, float f9)
        {
            base.doRenderLiving(entityMooshroom1, d2, d4, d6, f8, f9);
        }

        protected internal virtual void func_40272_a(EntityMooshroom entityMooshroom1, float f2)
        {
            base.renderEquippedItems(entityMooshroom1, f2);
            if (!entityMooshroom1.Child)
            {
                loadTexture("/terrain.png");
                GL.Enable(EnableCap.CullFace);
                Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                Minecraft.renderPipeline.ModelMatrix.Scale(1.0F, -1.0F, 1.0F);
                Minecraft.renderPipeline.ModelMatrix.Translate(0.2F, 0.4F, 0.5F);
                Minecraft.renderPipeline.ModelMatrix.Rotate(42.0F, 0.0F, 1.0F, 0.0F);
                renderBlocks.renderBlockAsItem(Block.mushroomRed, 0, 1.0F);
                Minecraft.renderPipeline.ModelMatrix.Translate(0.1F, 0.0F, -0.6F);
                Minecraft.renderPipeline.ModelMatrix.Rotate(42.0F, 0.0F, 1.0F, 0.0F);
                renderBlocks.renderBlockAsItem(Block.mushroomRed, 0, 1.0F);
                Minecraft.renderPipeline.ModelMatrix.PopMatrix();
                Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                ((ModelQuadruped)mainModel).head.postRender(0.0625F);
                Minecraft.renderPipeline.ModelMatrix.Scale(1.0F, -1.0F, 1.0F);
                Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 0.75F, -0.2F);
                Minecraft.renderPipeline.ModelMatrix.Rotate(12.0F, 0.0F, 1.0F, 0.0F);
                renderBlocks.renderBlockAsItem(Block.mushroomRed, 0, 1.0F);
                Minecraft.renderPipeline.ModelMatrix.PopMatrix();
                GL.Disable(EnableCap.CullFace);
            }
        }

        protected internal override void renderEquippedItems(EntityLiving entityLiving1, float f2)
        {
            func_40272_a((EntityMooshroom)entityLiving1, f2);
        }

        public override void doRenderLiving(EntityLiving entityLiving1, double d2, double d4, double d6, float f8, float f9)
        {
            func_40273_a((EntityMooshroom)entityLiving1, d2, d4, d6, f8, f9);
        }

        public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
        {
            func_40273_a((EntityMooshroom)entity1, d2, d4, d6, f8, f9);
        }
    }

}