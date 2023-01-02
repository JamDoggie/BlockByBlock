using net.minecraft.client;
using net.minecraft.client.entity;
using OpenTK.Graphics.OpenGL;
using System;

namespace net.minecraft.client.entity.render.model
{

    public class ModelDragon : ModelBase
    {
        private ModelRenderer head;
        private ModelRenderer neck;
        private ModelRenderer jaw;
        private ModelRenderer body;
        private ModelRenderer rearLeg;
        private ModelRenderer frontLeg;
        private ModelRenderer rearLegTip;
        private ModelRenderer frontLegTip;
        private ModelRenderer rearFoot;
        private ModelRenderer frontFoot;
        private ModelRenderer wing;
        private ModelRenderer wingTip;
        private float field_40317_s;

        public ModelDragon(float f1)
        {
            textureWidth = 256;
            textureHeight = 256;
            setTextureOffset("body.body", 0, 0);
            setTextureOffset("wing.skin", -56, 88);
            setTextureOffset("wingtip.skin", -56, 144);
            setTextureOffset("rearleg.main", 0, 0);
            setTextureOffset("rearfoot.main", 112, 0);
            setTextureOffset("rearlegtip.main", 196, 0);
            setTextureOffset("head.upperhead", 112, 30);
            setTextureOffset("wing.bone", 112, 88);
            setTextureOffset("head.upperlip", 176, 44);
            setTextureOffset("jaw.jaw", 176, 65);
            setTextureOffset("frontleg.main", 112, 104);
            setTextureOffset("wingtip.bone", 112, 136);
            setTextureOffset("frontfoot.main", 144, 104);
            setTextureOffset("neck.box", 192, 104);
            setTextureOffset("frontlegtip.main", 226, 138);
            setTextureOffset("body.scale", 220, 53);
            setTextureOffset("head.scale", 0, 0);
            setTextureOffset("neck.scale", 48, 0);
            setTextureOffset("head.nostril", 112, 0);
            float f2 = -16.0F;
            head = new ModelRenderer(this, "head");
            head.addBox("upperlip", -6.0F, -1.0F, -8.0F + f2, 12, 5, 16);
            head.addBox("upperhead", -8.0F, -8.0F, 6.0F + f2, 16, 16, 16);
            head.mirror = true;
            head.addBox("scale", -5.0F, -12.0F, 12.0F + f2, 2, 4, 6);
            head.addBox("nostril", -5.0F, -3.0F, -6.0F + f2, 2, 2, 4);
            head.mirror = false;
            head.addBox("scale", 3.0F, -12.0F, 12.0F + f2, 2, 4, 6);
            head.addBox("nostril", 3.0F, -3.0F, -6.0F + f2, 2, 2, 4);
            jaw = new ModelRenderer(this, "jaw");
            jaw.setRotationPoint(0.0F, 4.0F, 8.0F + f2);
            jaw.addBox("jaw", -6.0F, 0.0F, -16.0F, 12, 4, 16);
            head.addChild(jaw);
            neck = new ModelRenderer(this, "neck");
            neck.addBox("box", -5.0F, -5.0F, -5.0F, 10, 10, 10);
            neck.addBox("scale", -1.0F, -9.0F, -3.0F, 2, 4, 6);
            body = new ModelRenderer(this, "body");
            body.setRotationPoint(0.0F, 4.0F, 8.0F);
            body.addBox("body", -12.0F, 0.0F, -16.0F, 24, 24, 64);
            body.addBox("scale", -1.0F, -6.0F, -10.0F, 2, 6, 12);
            body.addBox("scale", -1.0F, -6.0F, 10.0F, 2, 6, 12);
            body.addBox("scale", -1.0F, -6.0F, 30.0F, 2, 6, 12);
            wing = new ModelRenderer(this, "wing");
            wing.setRotationPoint(-12.0F, 5.0F, 2.0F);
            wing.addBox("bone", -56.0F, -4.0F, -4.0F, 56, 8, 8);
            wing.addBox("skin", -56.0F, 0.0F, 2.0F, 56, 0, 56);
            wingTip = new ModelRenderer(this, "wingtip");
            wingTip.setRotationPoint(-56.0F, 0.0F, 0.0F);
            wingTip.addBox("bone", -56.0F, -2.0F, -2.0F, 56, 4, 4);
            wingTip.addBox("skin", -56.0F, 0.0F, 2.0F, 56, 0, 56);
            wing.addChild(wingTip);
            frontLeg = new ModelRenderer(this, "frontleg");
            frontLeg.setRotationPoint(-12.0F, 20.0F, 2.0F);
            frontLeg.addBox("main", -4.0F, -4.0F, -4.0F, 8, 24, 8);
            frontLegTip = new ModelRenderer(this, "frontlegtip");
            frontLegTip.setRotationPoint(0.0F, 20.0F, -1.0F);
            frontLegTip.addBox("main", -3.0F, -1.0F, -3.0F, 6, 24, 6);
            frontLeg.addChild(frontLegTip);
            frontFoot = new ModelRenderer(this, "frontfoot");
            frontFoot.setRotationPoint(0.0F, 23.0F, 0.0F);
            frontFoot.addBox("main", -4.0F, 0.0F, -12.0F, 8, 4, 16);
            frontLegTip.addChild(frontFoot);
            rearLeg = new ModelRenderer(this, "rearleg");
            rearLeg.setRotationPoint(-16.0F, 16.0F, 42.0F);
            rearLeg.addBox("main", -8.0F, -4.0F, -8.0F, 16, 32, 16);
            rearLegTip = new ModelRenderer(this, "rearlegtip");
            rearLegTip.setRotationPoint(0.0F, 32.0F, -4.0F);
            rearLegTip.addBox("main", -6.0F, -2.0F, 0.0F, 12, 32, 12);
            rearLeg.addChild(rearLegTip);
            rearFoot = new ModelRenderer(this, "rearfoot");
            rearFoot.setRotationPoint(0.0F, 31.0F, 4.0F);
            rearFoot.addBox("main", -9.0F, 0.0F, -20.0F, 18, 6, 24);
            rearLegTip.addChild(rearFoot);
        }

        public override void setLivingAnimations(EntityLiving entityLiving1, float f2, float f3, float f4)
        {
            field_40317_s = f4;
        }

        public override void render(Entity entity1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
            EntityDragon entityDragon8 = (EntityDragon)entity1;
            float f9 = entityDragon8.field_40173_aw + (entityDragon8.field_40172_ax - entityDragon8.field_40173_aw) * field_40317_s;
            jaw.rotateAngleX = (float)(Math.Sin((double)(f9 * (float)Math.PI * 2.0F)) + 1.0D) * 0.2F;
            float f10 = (float)(Math.Sin((double)(f9 * (float)Math.PI * 2.0F - 1.0F)) + 1.0D);
            f10 = (f10 * f10 * 1.0F + f10 * 2.0F) * 0.05F;
            Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, f10 - 2.0F, -3.0F);
            Minecraft.renderPipeline.ModelMatrix.Rotate(f10 * 2.0F, 1.0F, 0.0F, 0.0F);
            float f11 = -30.0F;
            float f13 = 0.0F;
            float f14 = 1.5F;
            double[] d15 = entityDragon8.func_40160_a(6, field_40317_s);
            float f16 = updateRotations(entityDragon8.func_40160_a(5, field_40317_s)[0] - entityDragon8.func_40160_a(10, field_40317_s)[0]);
            float f17 = updateRotations(entityDragon8.func_40160_a(5, field_40317_s)[0] + (double)(f16 / 2.0F));
            f11 += 2.0F;
            float f18 = f9 * (float)Math.PI * 2.0F;
            f11 = 20.0F;
            float f12 = -12.0F;

            float f21;
            for (int i19 = 0; i19 < 5; ++i19)
            {
                double[] d20 = entityDragon8.func_40160_a(5 - i19, field_40317_s);
                f21 = (float)Math.Cos((double)(i19 * 0.45F + f18)) * 0.15F;
                neck.rotateAngleY = updateRotations(d20[0] - d15[0]) * (float)Math.PI / 180.0F * f14;
                neck.rotateAngleX = f21 + (float)(d20[1] - d15[1]) * (float)Math.PI / 180.0F * f14 * 5.0F;
                neck.rotateAngleZ = -updateRotations(d20[0] - (double)f17) * (float)Math.PI / 180.0F * f14;
                neck.rotationPointY = f11;
                neck.rotationPointZ = f12;
                neck.rotationPointX = f13;
                f11 = (float)((double)f11 + Math.Sin(neck.rotateAngleX) * 10.0D);
                f12 = (float)((double)f12 - Math.Cos(neck.rotateAngleY) * Math.Cos(neck.rotateAngleX) * 10.0D);
                f13 = (float)((double)f13 - Math.Sin(neck.rotateAngleY) * Math.Cos(neck.rotateAngleX) * 10.0D);
                neck.render(f7);
            }

            head.rotationPointY = f11;
            head.rotationPointZ = f12;
            head.rotationPointX = f13;
            double[] d22 = entityDragon8.func_40160_a(0, field_40317_s);
            head.rotateAngleY = updateRotations(d22[0] - d15[0]) * (float)Math.PI / 180.0F * 1.0F;
            head.rotateAngleZ = -updateRotations(d22[0] - (double)f17) * (float)Math.PI / 180.0F * 1.0F;
            head.render(f7);
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
            Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 1.0F, 0.0F);
            Minecraft.renderPipeline.ModelMatrix.Rotate(-f16 * f14 * 1.0F, 0.0F, 0.0F, 1.0F);
            Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, -1.0F, 0.0F);
            body.rotateAngleZ = 0.0F;
            body.render(f7);

            for (int i23 = 0; i23 < 2; ++i23)
            {
                GL.Enable(EnableCap.CullFace);
                f21 = f9 * (float)Math.PI * 2.0F;
                wing.rotateAngleX = 0.125F - (float)Math.Cos((double)f21) * 0.2F;
                wing.rotateAngleY = 0.25F;
                wing.rotateAngleZ = (float)(Math.Sin((double)f21) + 0.125D) * 0.8F;
                wingTip.rotateAngleZ = -(float)(Math.Sin((double)(f21 + 2.0F)) + 0.5D) * 0.75F;
                rearLeg.rotateAngleX = 1.0F + f10 * 0.1F;
                rearLegTip.rotateAngleX = 0.5F + f10 * 0.1F;
                rearFoot.rotateAngleX = 0.75F + f10 * 0.1F;
                frontLeg.rotateAngleX = 1.3F + f10 * 0.1F;
                frontLegTip.rotateAngleX = -0.5F - f10 * 0.1F;
                frontFoot.rotateAngleX = 0.75F + f10 * 0.1F;
                wing.render(f7);
                frontLeg.render(f7);
                rearLeg.render(f7);
                Minecraft.renderPipeline.ModelMatrix.Scale(-1.0F, 1.0F, 1.0F);
                if (i23 == 0)
                {
                    GL.CullFace(CullFaceMode.Front);
                }
            }

            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
            GL.CullFace(CullFaceMode.Back);
            GL.Disable(EnableCap.CullFace);
            float f24 = -(float)Math.Sin((double)(f9 * (float)Math.PI * 2.0F)) * 0.0F;
            f18 = f9 * (float)Math.PI * 2.0F;
            f11 = 10.0F;
            f12 = 60.0F;
            f13 = 0.0F;
            d15 = entityDragon8.func_40160_a(11, field_40317_s);

            for (int i25 = 0; i25 < 12; ++i25)
            {
                d22 = entityDragon8.func_40160_a(12 + i25, field_40317_s);
                f24 = (float)((double)f24 + Math.Sin((double)(i25 * 0.45F + f18)) * (double)0.05F);
                neck.rotateAngleY = (updateRotations(d22[0] - d15[0]) * f14 + 180.0F) * (float)Math.PI / 180.0F;
                neck.rotateAngleX = f24 + (float)(d22[1] - d15[1]) * (float)Math.PI / 180.0F * f14 * 5.0F;
                neck.rotateAngleZ = updateRotations(d22[0] - (double)f17) * (float)Math.PI / 180.0F * f14;
                neck.rotationPointY = f11;
                neck.rotationPointZ = f12;
                neck.rotationPointX = f13;
                f11 = (float)((double)f11 + Math.Sin(neck.rotateAngleX) * 10.0D);
                f12 = (float)((double)f12 - Math.Cos(neck.rotateAngleY) * Math.Cos(neck.rotateAngleX) * 10.0D);
                f13 = (float)((double)f13 - Math.Sin(neck.rotateAngleY) * Math.Cos(neck.rotateAngleX) * 10.0D);
                neck.render(f7);
            }

            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
        }

        public override void setRotationAngles(float f1, float f2, float f3, float f4, float f5, float f6)
        {
            base.setRotationAngles(f1, f2, f3, f4, f5, f6);
        }

        private float updateRotations(double d1)
        {
            while (d1 >= 180.0D)
            {
                d1 -= 360.0D;
            }

            while (d1 < -180.0D)
            {
                d1 += 360.0D;
            }

            return (float)d1;
        }
    }

}