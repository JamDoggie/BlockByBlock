using net.minecraft.client;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{
	public class RenderEntity : Render
	{
		public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
		{
            Minecraft.newRenderer.ModelMatrix.PushMatrix();
			renderOffsetAABB(entity1.boundingBox, d2 - entity1.lastTickPosX, d4 - entity1.lastTickPosY, d6 - entity1.lastTickPosZ);
            Minecraft.newRenderer.ModelMatrix.PopMatrix();
		}
	}

}