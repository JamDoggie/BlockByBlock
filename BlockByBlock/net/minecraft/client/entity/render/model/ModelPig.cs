namespace net.minecraft.client.entity.render.model
{
    public class ModelPig : ModelQuadruped
    {
        public ModelPig() : this(0.0F)
        {
        }

        public ModelPig(float f1) : base(6, f1)
        {
            head.setTextureOffset(16, 16).addBox(-2.0F, 0.0F, -9.0F, 4, 3, 1, f1);
            field_40331_g = 4.0F;
        }
    }

}