using net.minecraft.client;

namespace net.minecraft.src
{

	public class OpenGlCapsChecker
	{
		private static bool tryCheckOcclusionCapable = true;

		public static bool checkARBOcclusion()
		{
			return tryCheckOcclusionCapable && MinecraftApplet.OpenGLExtensions.Contains("GL_ARB_occlusion_query");
		}
	}

}