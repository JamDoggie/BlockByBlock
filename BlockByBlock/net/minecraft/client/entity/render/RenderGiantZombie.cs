using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.client.entity.render.model;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render
{

    public class RenderGiantZombie : RenderLiving
    {
        private float scale;

        public RenderGiantZombie(ModelBase modelBase1, float f2, float f3) : base(modelBase1, f2 * f3)
        {
            scale = f3;
        }

        protected internal virtual void preRenderScale(EntityGiantZombie entityGiantZombie1, float f2)
        {
            Minecraft.renderPipeline.ModelMatrix.Scale(scale, scale, scale);
        }

        protected internal override void preRenderCallback(EntityLiving entityLiving1, float f2)
        {
            preRenderScale((EntityGiantZombie)entityLiving1, f2);
        }
    }

}