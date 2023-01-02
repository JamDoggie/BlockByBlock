using net.minecraft.client;
using net.minecraft.client.entity;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render;

public class RenderEntity : Renderer
{
    public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
    {
        Minecraft.renderPipeline.ModelMatrix.PushMatrix();
        renderOffsetAABB(entity1.boundingBox, d2 - entity1.lastTickPosX, d4 - entity1.lastTickPosY, d6 - entity1.lastTickPosZ);
        Minecraft.renderPipeline.ModelMatrix.PopMatrix();
    }
}