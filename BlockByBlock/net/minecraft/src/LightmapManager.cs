using net.minecraft.client;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{
	public class LightmapManager
	{
		public static int defaultTexUnit;
		public static int lightmapTexUnit { get; set; }
		private static string lightTexUniform = "lightTexture";

		public static void initializeTextures()
		{
			defaultTexUnit = 0;
			lightmapTexUnit = 1;

			int uniform = Minecraft.renderPipeline.GetUniform(lightTexUniform);
			GL.Uniform1(uniform, 1);
		}

		public static int ActiveTexture
		{
			set
			{
				GL.ActiveTexture((TextureUnit)(value + 33984));
				Minecraft.renderPipeline.SetActiveTexture(value);
			}
		}

		public static void setLightmapTextureCoords(int texUnit, float x, float y) // TODO: remove texUnit parameter.
		{
			Minecraft.renderPipeline.SetLightmapCoords(x, y);
		}
	}
}