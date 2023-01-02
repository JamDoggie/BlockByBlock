using BlockByBlock.net.minecraft.render;
using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render
{

    public class RenderTNTPrimed : Renderer
    {
        private RenderBlocks blockRenderer = new RenderBlocks();

        public RenderTNTPrimed()
        {
            shadowSize = 0.5F;
        }

        public virtual void func_153_a(EntityTNTPrimed entityTNTPrimed1, double d2, double d4, double d6, float f8, float f9)
        {
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
            Minecraft.renderPipeline.ModelMatrix.Translate((float)d2, (float)d4, (float)d6);
            float f10;
            if (entityTNTPrimed1.fuse - f9 + 1.0F < 10.0F)
            {
                f10 = 1.0F - (entityTNTPrimed1.fuse - f9 + 1.0F) / 10.0F;
                if (f10 < 0.0F)
                {
                    f10 = 0.0F;
                }

                if (f10 > 1.0F)
                {
                    f10 = 1.0F;
                }

                f10 *= f10;
                f10 *= f10;
                float f11 = 1.0F + f10 * 0.3F;
                Minecraft.renderPipeline.ModelMatrix.Scale(f11, f11, f11);
            }

            f10 = (1.0F - (entityTNTPrimed1.fuse - f9 + 1.0F) / 100.0F) * 0.8F;
            loadTexture("/terrain.png");
            blockRenderer.renderBlockAsItem(Block.tnt, 0, entityTNTPrimed1.getBrightness(f9));
            if (entityTNTPrimed1.fuse / 5 % 2 == 0)
            {
                Minecraft.renderPipeline.SetState(RenderState.TextureState, false);
                Minecraft.renderPipeline.SetState(RenderState.LightingState, false);
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.DstAlpha);
                Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, f10);
                blockRenderer.renderBlockAsItem(Block.tnt, 0, 1.0F);
                Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
                GL.Disable(EnableCap.Blend);
                Minecraft.renderPipeline.SetState(RenderState.LightingState, true);
                Minecraft.renderPipeline.SetState(RenderState.TextureState, true);
            }

            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
        }

        public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
        {
            func_153_a((EntityTNTPrimed)entity1, d2, d4, d6, f8, f9);
        }
    }

}