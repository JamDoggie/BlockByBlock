using net.minecraft.client;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{
	public class OpenGlHelper
	{
		public static int defaultTexUnit;
		public static int lightmapTexUnit;
		private static bool useMultitextureARB = false;

		public static void initializeTextures()
		{
            //useMultitextureARB = MinecraftApplet.OpenGLExtensions.Contains("GL_ARB_multitexture") && !OpenGl13; // PORTING TODO: perform this check correctly, maybe. Or maybe not. It's not 2004.
            useMultitextureARB = false;
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
					//ARBMultitexture.glActiveTextureARB(value);
				}
				else
				{
					GL.ActiveTexture((TextureUnit)value);
				}
    
			}
		}

		public static int ClientActiveTexture
		{
			set
			{
				if (useMultitextureARB)
				{
					//ARBMultitexture.glClientActiveTextureARB(value);
				}
				else
				{
					GL.ClientActiveTexture((TextureUnit)value);
				}
    
			}
		}

		public static void setLightmapTextureCoords(int i0, float f1, float f2)
		{
			if (useMultitextureARB)
			{
				//ARBMultitexture.glMultiTexCoord2fARB(i0, f1, f2);
			}
			else
			{
				GL.MultiTexCoord2((TextureUnit)i0, f1, f2);
			}

		}
	}

}