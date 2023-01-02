using BlockByBlock.net.minecraft.client.entity.render.model;
using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.client.entity.render.model;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render
{

    public class RenderBoat : Renderer
    {
        protected internal ModelBase modelBoat;

        public RenderBoat()
        {
            shadowSize = 0.5F;
            modelBoat = new ModelBoat();
        }

        public virtual void renderBoat(EntityBoat entityBoat1, double d2, double d4, double d6, float f8, float f9)
        {
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
            Minecraft.renderPipeline.ModelMatrix.Translate((float)d2, (float)d4, (float)d6);
            Minecraft.renderPipeline.ModelMatrix.Rotate(180.0F - f8, 0.0F, 1.0F, 0.0F);
            float f10 = entityBoat1.TimeSinceHit - f9;
            float f11 = entityBoat1.DamageTaken - f9;
            if (f11 < 0.0F)
            {
                f11 = 0.0F;
            }

            if (f10 > 0.0F)
            {
                Minecraft.renderPipeline.ModelMatrix.Rotate(MathHelper.sin(f10) * f10 * f11 / 10.0F * entityBoat1.ForwardDirection, 1.0F, 0.0F, 0.0F);
            }

            loadTexture("/terrain.png");
            float f12 = 0.75F;
            Minecraft.renderPipeline.ModelMatrix.Scale(f12, f12, f12);
            Minecraft.renderPipeline.ModelMatrix.Scale(1.0F / f12, 1.0F / f12, 1.0F / f12);
            loadTexture("/item/boat.png");
            Minecraft.renderPipeline.ModelMatrix.Scale(-1.0F, -1.0F, 1.0F);
            modelBoat.render(entityBoat1, 0.0F, 0.0F, -0.1F, 0.0F, 0.0F, 0.0625F);
            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
        }

        public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
        {
            renderBoat((EntityBoat)entity1, d2, d4, d6, f8, f9);
        }
    }

}