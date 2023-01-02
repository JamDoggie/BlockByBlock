using System.Collections;
using net.minecraft.client.entity;
using net.minecraft.src;

namespace net.minecraft.client.entity.render.model
{

    public abstract class ModelBase
    {
        public float onGround;
        public bool isRiding = false;
        public IList boxList = new ArrayList();
        public bool isChild = true;
        private IDictionary modelTextureMap = new Hashtable();
        public int textureWidth = 64;
        public int textureHeight = 32;

        public virtual void render(Entity entity1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
        }

        public virtual void setRotationAngles(float f1, float f2, float f3, float f4, float f5, float f6)
        {
        }

        public virtual void setLivingAnimations(EntityLiving entityLiving1, float f2, float f3, float f4)
        {
        }

        protected internal virtual void setTextureOffset(string string1, int i2, int i3)
        {
            modelTextureMap[string1] = new TextureOffset(i2, i3);
        }

        public virtual TextureOffset getTextureOffset(string string1)
        {
            return (TextureOffset)modelTextureMap[string1];
        }
    }

}