using BlockByBlock.net.minecraft.client.entity.render.model;
using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.client.entity.render.model;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render
{
    public class RenderEnderCrystal : Renderer
    {
        private int field_41037_a = -1;
        private ModelBase field_41036_b;

        public RenderEnderCrystal()
        {
            shadowSize = 0.5F;
        }

        public virtual void func_41035_a(EntityEnderCrystal entityEnderCrystal1, double d2, double d4, double d6, float f8, float f9)
        {
            if (field_41037_a != 1)
            {
                field_41036_b = new ModelEnderCrystal(0.0F);
                field_41037_a = 1;
            }

            float f10 = entityEnderCrystal1.innerRotation + f9;
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
            Minecraft.renderPipeline.ModelMatrix.Translate((float)d2, (float)d4, (float)d6);
            loadTexture("/mob/enderdragon/crystal.png");
            float f11 = MathHelper.sin(f10 * 0.2F) / 2.0F + 0.5F;
            f11 += f11 * f11;
            field_41036_b.render(entityEnderCrystal1, 0.0F, f10 * 3.0F, f11 * 0.2F, 0.0F, 0.0F, 0.0625F);
            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
        }

        public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
        {
            func_41035_a((EntityEnderCrystal)entity1, d2, d4, d6, f8, f9);
        }
    }

}