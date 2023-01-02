using BlockByBlock.net.minecraft.render;
using net.minecraft.client;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render
{
    public class RenderFallingSand : Renderer
    {
        private new RenderBlocks renderBlocks = new RenderBlocks();

        public RenderFallingSand()
        {
            this.shadowSize = 0.5F;
        }

        public virtual void doRenderFallingSand(EntityFallingSand entityFallingSand1, double d2, double d4, double d6, float f8, float f9)
        {
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
            Minecraft.renderPipeline.ModelMatrix.Translate((float)d2, (float)d4, (float)d6);
            this.loadTexture("/terrain.png");
            Block block10 = Block.blocksList[entityFallingSand1.blockID];
            World world11 = entityFallingSand1.World;
            Minecraft.renderPipeline.SetState(RenderState.LightingState, false);
            if (block10 == Block.dragonEgg)
            {
                renderBlocks.blockAccess = world11;
                Tessellator tessellator12 = Tessellator.instance;
                tessellator12.startDrawingQuads();
                tessellator12.setTranslation((double)(-MathHelper.floor_double(entityFallingSand1.posX) - 0.5F), (double)(-MathHelper.floor_double(entityFallingSand1.posY) - 0.5F), (double)(-MathHelper.floor_double(entityFallingSand1.posZ) - 0.5F));
                renderBlocks.renderBlockByRenderType(block10, MathHelper.floor_double(entityFallingSand1.posX), MathHelper.floor_double(entityFallingSand1.posY), MathHelper.floor_double(entityFallingSand1.posZ));
                tessellator12.setTranslation(0.0D, 0.0D, 0.0D);
                tessellator12.DrawImmediate();
            }
            else
            {
                renderBlocks.renderBlockFallingSand(block10, world11, MathHelper.floor_double(entityFallingSand1.posX), MathHelper.floor_double(entityFallingSand1.posY), MathHelper.floor_double(entityFallingSand1.posZ));
            }

            Minecraft.renderPipeline.SetState(RenderState.LightingState, true);
            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
        }

        public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
        {
            doRenderFallingSand((EntityFallingSand)entity1, d2, d4, d6, f8, f9);
        }
    }

}