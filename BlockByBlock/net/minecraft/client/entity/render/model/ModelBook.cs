using net.minecraft.client.entity;
using net.minecraft.src;

namespace net.minecraft.client.entity.render.model
{
    public class ModelBook : ModelBase
    {
        private bool InstanceFieldsInitialized = false;

        private void InitializeInstanceFields()
        {
            coverRight = new ModelRenderer(this).setTextureOffset(0, 0).addBox(-6.0F, -5.0F, 0.0F, 6, 10, 0);
            coverLeft = new ModelRenderer(this).setTextureOffset(16, 0).addBox(0.0F, -5.0F, 0.0F, 6, 10, 0);
            pagesRight = new ModelRenderer(this).setTextureOffset(0, 10).addBox(0.0F, -4.0F, -0.99F, 5, 8, 1);
            pagesLeft = new ModelRenderer(this).setTextureOffset(12, 10).addBox(0.0F, -4.0F, -0.01F, 5, 8, 1);
            flippingPageRight = new ModelRenderer(this).setTextureOffset(24, 10).addBox(0.0F, -4.0F, 0.0F, 5, 8, 0);
            flippingPageLeft = new ModelRenderer(this).setTextureOffset(24, 10).addBox(0.0F, -4.0F, 0.0F, 5, 8, 0);
            bookSpine = new ModelRenderer(this).setTextureOffset(12, 0).addBox(-1.0F, -5.0F, 0.0F, 2, 10, 0);
        }

        public ModelRenderer coverRight;
        public ModelRenderer coverLeft;
        public ModelRenderer pagesRight;
        public ModelRenderer pagesLeft;
        public ModelRenderer flippingPageRight;
        public ModelRenderer flippingPageLeft;
        public ModelRenderer bookSpine;

        public ModelBook()
        {
            if (!InstanceFieldsInitialized)
            {
                InitializeInstanceFields();
                InstanceFieldsInitialized = true;
            }
            coverRight.setRotationPoint(0.0F, 0.0F, -1.0F);
            coverLeft.setRotationPoint(0.0F, 0.0F, 1.0F);
            bookSpine.rotateAngleY = (float)Math.PI / 2F;
        }

        public override void render(Entity entity1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
            setRotationAngles(f2, f3, f4, f5, f6, f7);
            coverRight.render(f7);
            coverLeft.render(f7);
            bookSpine.render(f7);
            pagesRight.render(f7);
            pagesLeft.render(f7);
            flippingPageRight.render(f7);
            flippingPageLeft.render(f7);
        }

        public override void setRotationAngles(float f1, float f2, float f3, float f4, float f5, float f6)
        {
            float f7 = (MathHelper.sin(f1 * 0.02F) * 0.1F + 1.25F) * f4;
            coverRight.rotateAngleY = (float)Math.PI + f7;
            coverLeft.rotateAngleY = -f7;
            pagesRight.rotateAngleY = f7;
            pagesLeft.rotateAngleY = -f7;
            flippingPageRight.rotateAngleY = f7 - f7 * 2.0F * f2;
            flippingPageLeft.rotateAngleY = f7 - f7 * 2.0F * f3;
            pagesRight.rotationPointX = MathHelper.sin(f7);
            pagesLeft.rotationPointX = MathHelper.sin(f7);
            flippingPageRight.rotationPointX = MathHelper.sin(f7);
            flippingPageLeft.rotationPointX = MathHelper.sin(f7);
        }
    }

}