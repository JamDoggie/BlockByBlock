namespace net.minecraft.src
{
	using GLContext = org.lwjgl.opengl.GLContext;

	// PORTING TODO: OpenGL code

	public class OpenGlCapsChecker
	{
		private static bool tryCheckOcclusionCapable = true;

		public static bool checkARBOcclusion()
		{
			return tryCheckOcclusionCapable && GLContext.getCapabilities().GL_ARB_occlusion_query;
		}
	}

}