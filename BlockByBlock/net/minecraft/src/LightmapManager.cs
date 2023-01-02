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
			defaultTexUnit = 33984;
			lightmapTexUnit = 33985;

			int uniform = Minecraft.renderPipeline.GetUniform(lightTexUniform);
			GL.Uniform1(uniform, 1);
		}

		public static int ActiveTexture
		{
			set
			{
				GL.ActiveTexture((TextureUnit)value);
				Minecraft.renderPipeline.SetActiveTexture(value);
			}
		}

		public static void setLightmapTextureCoords(int texUnit, float x, float y) // TODO: remove texUnit parameter.
		{
			float normalizedX = x / 16f;
            float normalizedY = y / 16f;

			Minecraft.renderPipeline.SetLightmapCoords((normalizedX / (17F)) + 0.0625f, (normalizedY / (17F)) + 0.0625f);
		}
	}

}