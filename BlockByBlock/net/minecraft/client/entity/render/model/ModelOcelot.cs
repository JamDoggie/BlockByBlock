using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render.model
{

    public class ModelOcelot : ModelBase
    {
        internal ModelRenderer field_48225_a;
        internal ModelRenderer field_48223_b;
        internal ModelRenderer field_48224_c;
        internal ModelRenderer field_48221_d;
        internal ModelRenderer field_48222_e;
        internal ModelRenderer field_48219_f;
        internal ModelRenderer field_48220_g;
        internal ModelRenderer field_48226_n;
        internal int field_48227_o = 1;

        public ModelOcelot()
        {
            setTextureOffset("head.main", 0, 0);
            setTextureOffset("head.nose", 0, 24);
            setTextureOffset("head.ear1", 0, 10);
            setTextureOffset("head.ear2", 6, 10);
            field_48220_g = new ModelRenderer(this, "head");
            field_48220_g.addBox("main", -2.5F, -2.0F, -3.0F, 5, 4, 5);
            field_48220_g.addBox("nose", -1.5F, 0.0F, -4.0F, 3, 2, 2);
            field_48220_g.addBox("ear1", -2.0F, -3.0F, 0.0F, 1, 1, 2);
            field_48220_g.addBox("ear2", 1.0F, -3.0F, 0.0F, 1, 1, 2);
            field_48220_g.setRotationPoint(0.0F, 15.0F, -9.0F);
            field_48226_n = new ModelRenderer(this, 20, 0);
            field_48226_n.addBox(-2.0F, 3.0F, -8.0F, 4, 16, 6, 0.0F);
            field_48226_n.setRotationPoint(0.0F, 12.0F, -10.0F);
            field_48222_e = new ModelRenderer(this, 0, 15);
            field_48222_e.addBox(-0.5F, 0.0F, 0.0F, 1, 8, 1);
            field_48222_e.rotateAngleX = 0.9F;
            field_48222_e.setRotationPoint(0.0F, 15.0F, 8.0F);
            field_48219_f = new ModelRenderer(this, 4, 15);
            field_48219_f.addBox(-0.5F, 0.0F, 0.0F, 1, 8, 1);
            field_48219_f.setRotationPoint(0.0F, 20.0F, 14.0F);
            field_48225_a = new ModelRenderer(this, 8, 13);
            field_48225_a.addBox(-1.0F, 0.0F, 1.0F, 2, 6, 2);
            field_48225_a.setRotationPoint(1.1F, 18.0F, 5.0F);
            field_48223_b = new ModelRenderer(this, 8, 13);
            field_48223_b.addBox(-1.0F, 0.0F, 1.0F, 2, 6, 2);
            field_48223_b.setRotationPoint(-1.1F, 18.0F, 5.0F);
            field_48224_c = new ModelRenderer(this, 40, 0);
            field_48224_c.addBox(-1.0F, 0.0F, 0.0F, 2, 10, 2);
            field_48224_c.setRotationPoint(1.2F, 13.8F, -5.0F);
            field_48221_d = new ModelRenderer(this, 40, 0);
            field_48221_d.addBox(-1.0F, 0.0F, 0.0F, 2, 10, 2);
            field_48221_d.setRotationPoint(-1.2F, 13.8F, -5.0F);
        }

        public override void render(Entity entity1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
            setRotationAngles(f2, f3, f4, f5, f6, f7);
            if (isChild)
            {
                float f8 = 2.0F;
                Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                Minecraft.renderPipeline.ModelMatrix.Scale(1.5F / f8, 1.5F / f8, 1.5F / f8);
                Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 10.0F * f7, 4.0F * f7);
                field_48220_g.render(f7);
                Minecraft.renderPipeline.ModelMatrix.PopMatrix();
                Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                Minecraft.renderPipeline.ModelMatrix.Scale(1.0F / f8, 1.0F / f8, 1.0F / f8);
                Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 24.0F * f7, 0.0F);
                field_48226_n.render(f7);
                field_48225_a.render(f7);
                field_48223_b.render(f7);
                field_48224_c.render(f7);
                field_48221_d.render(f7);
                field_48222_e.render(f7);
                field_48219_f.render(f7);
                Minecraft.renderPipeline.ModelMatrix.PopMatrix();
            }
            else
            {
                field_48220_g.render(f7);
                field_48226_n.render(f7);
                field_48222_e.render(f7);
                field_48219_f.render(f7);
                field_48225_a.render(f7);
                field_48223_b.render(f7);
                field_48224_c.render(f7);
                field_48221_d.render(f7);
            }

        }

        public override void setRotationAngles(float f1, float f2, float f3, float f4, float f5, float f6)
        {
            field_48220_g.rotateAngleX = f5 / 57.295776F;
            field_48220_g.rotateAngleY = f4 / 57.295776F;
            if (field_48227_o != 3)
            {
                field_48226_n.rotateAngleX = (float)Math.PI / 2F;
                if (field_48227_o == 2)
                {
                    field_48225_a.rotateAngleX = MathHelper.cos(f1 * 0.6662F) * 1.0F * f2;
                    field_48223_b.rotateAngleX = MathHelper.cos(f1 * 0.6662F + 0.3F) * 1.0F * f2;
                    field_48224_c.rotateAngleX = MathHelper.cos(f1 * 0.6662F + (float)Math.PI + 0.3F) * 1.0F * f2;
                    field_48221_d.rotateAngleX = MathHelper.cos(f1 * 0.6662F + (float)Math.PI) * 1.0F * f2;
                    field_48219_f.rotateAngleX = 1.7278761F + 0.31415927F * MathHelper.cos(f1) * f2;
                }
                else
                {
                    field_48225_a.rotateAngleX = MathHelper.cos(f1 * 0.6662F) * 1.0F * f2;
                    field_48223_b.rotateAngleX = MathHelper.cos(f1 * 0.6662F + (float)Math.PI) * 1.0F * f2;
                    field_48224_c.rotateAngleX = MathHelper.cos(f1 * 0.6662F + (float)Math.PI) * 1.0F * f2;
                    field_48221_d.rotateAngleX = MathHelper.cos(f1 * 0.6662F) * 1.0F * f2;
                    if (field_48227_o == 1)
                    {
                        field_48219_f.rotateAngleX = 1.7278761F + 0.7853982F * MathHelper.cos(f1) * f2;
                    }
                    else
                    {
                        field_48219_f.rotateAngleX = 1.7278761F + 0.47123894F * MathHelper.cos(f1) * f2;
                    }
                }
            }

        }

        public override void setLivingAnimations(EntityLiving entityLiving1, float f2, float f3, float f4)
        {
            EntityOcelot entityOcelot5 = (EntityOcelot)entityLiving1;
            field_48226_n.rotationPointY = 12.0F;
            field_48226_n.rotationPointZ = -10.0F;
            field_48220_g.rotationPointY = 15.0F;
            field_48220_g.rotationPointZ = -9.0F;
            field_48222_e.rotationPointY = 15.0F;
            field_48222_e.rotationPointZ = 8.0F;
            field_48219_f.rotationPointY = 20.0F;
            field_48219_f.rotationPointZ = 14.0F;
            field_48224_c.rotationPointY = field_48221_d.rotationPointY = 13.8F;
            field_48224_c.rotationPointZ = field_48221_d.rotationPointZ = -5.0F;
            field_48225_a.rotationPointY = field_48223_b.rotationPointY = 18.0F;
            field_48225_a.rotationPointZ = field_48223_b.rotationPointZ = 5.0F;
            field_48222_e.rotateAngleX = 0.9F;
            if (entityOcelot5.Sneaking)
            {
                ++field_48226_n.rotationPointY;
                field_48220_g.rotationPointY += 2.0F;
                ++field_48222_e.rotationPointY;
                field_48219_f.rotationPointY += -4.0F;
                field_48219_f.rotationPointZ += 2.0F;
                field_48222_e.rotateAngleX = (float)Math.PI / 2F;
                field_48219_f.rotateAngleX = (float)Math.PI / 2F;
                field_48227_o = 0;
            }
            else if (entityOcelot5.Sprinting)
            {
                field_48219_f.rotationPointY = field_48222_e.rotationPointY;
                field_48219_f.rotationPointZ += 2.0F;
                field_48222_e.rotateAngleX = (float)Math.PI / 2F;
                field_48219_f.rotateAngleX = (float)Math.PI / 2F;
                field_48227_o = 2;
            }
            else if (entityOcelot5.Sitting)
            {
                field_48226_n.rotateAngleX = 0.7853982F;
                field_48226_n.rotationPointY += -4.0F;
                field_48226_n.rotationPointZ += 5.0F;
                field_48220_g.rotationPointY += -3.3F;
                ++field_48220_g.rotationPointZ;
                field_48222_e.rotationPointY += 8.0F;
                field_48222_e.rotationPointZ += -2.0F;
                field_48219_f.rotationPointY += 2.0F;
                field_48219_f.rotationPointZ += -0.8F;
                field_48222_e.rotateAngleX = 1.7278761F;
                field_48219_f.rotateAngleX = 2.670354F;
                field_48224_c.rotateAngleX = field_48221_d.rotateAngleX = -0.15707964F;
                field_48224_c.rotationPointY = field_48221_d.rotationPointY = 15.8F;
                field_48224_c.rotationPointZ = field_48221_d.rotationPointZ = -7.0F;
                field_48225_a.rotateAngleX = field_48223_b.rotateAngleX = -1.5707964F;
                field_48225_a.rotationPointY = field_48223_b.rotationPointY = 21.0F;
                field_48225_a.rotationPointZ = field_48223_b.rotationPointZ = 1.0F;
                field_48227_o = 3;
            }
            else
            {
                field_48227_o = 1;
            }

        }
    }

}