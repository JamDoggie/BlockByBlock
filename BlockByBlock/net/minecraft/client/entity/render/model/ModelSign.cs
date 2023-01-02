namespace net.minecraft.client.entity.render.model
{
    public class ModelSign : ModelBase
    {
        private bool InstanceFieldsInitialized = false;

        private void InitializeInstanceFields()
        {
            signBoard = new ModelRenderer(this, 0, 0);
        }

        public ModelRenderer signBoard;
        public ModelRenderer signStick;

        public ModelSign()
        {
            if (!InstanceFieldsInitialized)
            {
                InitializeInstanceFields();
                InstanceFieldsInitialized = true;
            }
            signBoard.addBox(-12.0F, -14.0F, -1.0F, 24, 12, 2, 0.0F);
            signStick = new ModelRenderer(this, 0, 14);
            signStick.addBox(-1.0F, -2.0F, -1.0F, 2, 14, 2, 0.0F);
        }

        public virtual void renderSign()
        {
            signBoard.render(0.0625F);
            signStick.render(0.0625F);
        }
    }

}