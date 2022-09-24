namespace net.minecraft.src
{
	using ARBMultitexture = org.lwjgl.opengl.ARBMultitexture;
	using GL13 = org.lwjgl.opengl.GL13;
	using GLContext = org.lwjgl.opengl.GLContext;

	public class OpenGlHelper
	{
		public static int defaultTexUnit;
		public static int lightmapTexUnit;
		private static bool useMultitextureARB = false;

		public static void initializeTextures()
		{
			useMultitextureARB = GLContext.getCapabilities().GL_ARB_multitexture && !GLContext.getCapabilities().OpenGL13;
			if (useMultitextureARB)
			{
				defaultTexUnit = 33984;
				lightmapTexUnit = 33985;
			}
			else
			{
				defaultTexUnit = 33984;
				lightmapTexUnit = 33985;
			}

		}

		public static int ActiveTexture
		{
			set
			{
				if (useMultitextureARB)
				{
					ARBMultitexture.glActiveTextureARB(value);
				}
				else
				{
					GL13.glActiveTexture(value);
				}
    
			}
		}

		public static int ClientActiveTexture
		{
			set
			{
				if (useMultitextureARB)
				{
					ARBMultitexture.glClientActiveTextureARB(value);
				}
				else
				{
					GL13.glClientActiveTexture(value);
				}
    
			}
		}

		public static void setLightmapTextureCoords(int i0, float f1, float f2)
		{
			if (useMultitextureARB)
			{
				ARBMultitexture.glMultiTexCoord2fARB(i0, f1, f2);
			}
			else
			{
				GL13.glMultiTexCoord2f(i0, f1, f2);
			}

		}
	}

}