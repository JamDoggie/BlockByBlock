using BlockByBlock.net.minecraft.client.entity.render.model;
using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.client.entity.render.model;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render
{
    public class RenderGhast : RenderLiving
    {
        public RenderGhast() : base(new ModelGhast(), 0.5F)
        {
        }

        protected internal virtual void func_4014_a(EntityGhast entityGhast1, float f2)
        {
            float f4 = (entityGhast1.prevAttackCounter + (entityGhast1.attackCounter - entityGhast1.prevAttackCounter) * f2) / 20.0F;
            if (f4 < 0.0F)
            {
                f4 = 0.0F;
            }

            f4 = 1.0F / (f4 * f4 * f4 * f4 * f4 * 2.0F + 1.0F);
            float f5 = (8.0F + f4) / 2.0F;
            float f6 = (8.0F + 1.0F / f4) / 2.0F;
            Minecraft.renderPipeline.ModelMatrix.Scale(f6, f5, f6);
            Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
        }

        protected internal override void preRenderCallback(EntityLiving entityLiving1, float f2)
        {
            func_4014_a((EntityGhast)entityLiving1, f2);
        }
    }

}