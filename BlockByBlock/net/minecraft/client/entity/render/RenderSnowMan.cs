using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.client.entity.render.model;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render
{

    public class RenderSnowMan : RenderLiving
    {
        private bool InstanceFieldsInitialized = false;

        private void InitializeInstanceFields()
        {
            snowmanModel = (ModelSnowMan)mainModel;
        }

        private ModelSnowMan snowmanModel;

        public RenderSnowMan() : base(new ModelSnowMan(), 0.5F)
        {
            if (!InstanceFieldsInitialized)
            {
                InitializeInstanceFields();
                InstanceFieldsInitialized = true;
            }
            RenderPassModel = snowmanModel;
        }

        protected internal virtual void func_40288_a(EntitySnowman entitySnowman1, float f2)
        {
            base.renderEquippedItems(entitySnowman1, f2);
            ItemStack itemStack3 = new ItemStack(Block.pumpkin, 1);
            if (itemStack3 != null && itemStack3.Item.shiftedIndex < 256)
            {
                Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                snowmanModel.field_40305_c.postRender(0.0625F);
                if (RenderBlocks.renderItemIn3d(Block.blocksList[itemStack3.itemID].RenderType))
                {
                    float f4 = 0.625F;
                    Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, -0.34375F, 0.0F);
                    Minecraft.renderPipeline.ModelMatrix.Rotate(180.0F, 0.0F, 1.0F, 0.0F);
                    Minecraft.renderPipeline.ModelMatrix.Scale(f4, -f4, f4);
                }

                renderManager.itemRenderer.renderItem(entitySnowman1, itemStack3, 0);
                Minecraft.renderPipeline.ModelMatrix.PopMatrix();
            }

        }

        protected internal override void renderEquippedItems(EntityLiving entityLiving1, float f2)
        {
            func_40288_a((EntitySnowman)entityLiving1, f2);
        }
    }

}