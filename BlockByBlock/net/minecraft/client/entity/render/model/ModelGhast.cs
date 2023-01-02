using System;
using BlockByBlock.java_extensions;
using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render.model
{

    public class ModelGhast : ModelBase
    {
        internal ModelRenderer body;
        internal ModelRenderer[] tentacles = new ModelRenderer[9];

        public ModelGhast()
        {
            sbyte b1 = -16;
            body = new ModelRenderer(this, 0, 0);
            body.addBox(-8.0F, -8.0F, -8.0F, 16, 16, 16);
            body.rotationPointY += 24 + b1;
            RandomExtended random2 = new RandomExtended(1660L);

            for (int i3 = 0; i3 < tentacles.Length; ++i3)
            {
                tentacles[i3] = new ModelRenderer(this, 0, 0);
                float f4 = ((i3 % 3 - i3 / 3 % 2 * 0.5F + 0.25F) / 2.0F * 2.0F - 1.0F) * 5.0F;
                float f5 = (i3 / 3 / 2.0F * 2.0F - 1.0F) * 5.0F;
                int i6 = random2.Next(7) + 8;
                tentacles[i3].addBox(-1.0F, 0.0F, -1.0F, 2, i6, 2);
                tentacles[i3].rotationPointX = f4;
                tentacles[i3].rotationPointZ = f5;
                tentacles[i3].rotationPointY = 31 + b1;
            }

        }

        public override void setRotationAngles(float f1, float f2, float f3, float f4, float f5, float f6)
        {
            for (int i7 = 0; i7 < tentacles.Length; ++i7)
            {
                tentacles[i7].rotateAngleX = 0.2F * MathHelper.sin(f3 * 0.3F + i7) + 0.4F;
            }

        }

        public override void render(Entity entity1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
            setRotationAngles(f2, f3, f4, f5, f6, f7);
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
            Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 0.6F, 0.0F);
            body.render(f7);
            ModelRenderer[] modelRenderer8 = tentacles;
            int i9 = modelRenderer8.Length;

            for (int i10 = 0; i10 < i9; ++i10)
            {
                ModelRenderer modelRenderer11 = modelRenderer8[i10];
                modelRenderer11.render(f7);
            }

            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
        }
    }

}