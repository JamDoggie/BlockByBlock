namespace net.minecraft.src
{
	using GL11 = org.lwjgl.opengl.GL11;

	public class RenderEntity : Render
	{
		public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
		{
			GL11.glPushMatrix();
			renderOffsetAABB(entity1.boundingBox, d2 - entity1.lastTickPosX, d4 - entity1.lastTickPosY, d6 - entity1.lastTickPosZ);
			GL11.glPopMatrix();
		}
	}

}