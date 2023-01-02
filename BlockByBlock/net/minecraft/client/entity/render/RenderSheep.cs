using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.client.entity.render.model;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render
{
    public class RenderSheep : RenderLiving
    {
        public RenderSheep(ModelBase modelBase1, ModelBase modelBase2, float f3) : base(modelBase1, f3)
        {
            RenderPassModel = modelBase2;
        }

        protected internal virtual int setWoolColorAndRender(EntitySheep entitySheep1, int i2, float f3)
        {
            if (i2 == 0 && !entitySheep1.Sheared)
            {
                loadTexture("/mob/sheep_fur.png");
                float f4 = 1.0F;
                int i5 = entitySheep1.FleeceColor;
                Minecraft.renderPipeline.SetColor(f4 * EntitySheep.fleeceColorTable[i5][0], f4 * EntitySheep.fleeceColorTable[i5][1], f4 * EntitySheep.fleeceColorTable[i5][2]);
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public virtual void doRenderSheep(EntitySheep entitySheep1, double d2, double d4, double d6, float f8, float f9)
        {
            base.doRenderLiving(entitySheep1, d2, d4, d6, f8, f9);
        }

        protected internal override int shouldRenderPass(EntityLiving entityLiving1, int i2, float f3)
        {
            return setWoolColorAndRender((EntitySheep)entityLiving1, i2, f3);
        }

        public override void doRenderLiving(EntityLiving entityLiving1, double d2, double d4, double d6, float f8, float f9)
        {
            doRenderSheep((EntitySheep)entityLiving1, d2, d4, d6, f8, f9);
        }

        public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
        {
            doRenderSheep((EntitySheep)entity1, d2, d4, d6, f8, f9);
        }
    }

}