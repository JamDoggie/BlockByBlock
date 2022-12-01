using net.minecraft.client;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{

	public class RenderHelper
	{
		private static float[] colorBuffer = new float[16];

		public static void disableStandardItemLighting()
		{
			GL.Disable(EnableCap.Lighting);
			GL.Disable(EnableCap.Light0);
			GL.Disable(EnableCap.Light1);
			GL.Disable(EnableCap.ColorMaterial);
		}

		public static void enableStandardItemLighting()
		{
			GL.Enable(EnableCap.Lighting);
			GL.Enable(EnableCap.Light0);
			GL.Enable(EnableCap.Light1);
			GL.Enable(EnableCap.ColorMaterial);
			GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.AmbientAndDiffuse);
			float f0 = 0.4F;
			float f1 = 0.6F;
			float f2 = 0.0F;
			Vec3D vec3D3 = Vec3D.createVector((double)0.2F, 1.0D, -0.699999988079071D).normalize();
			GL.Light(LightName.Light0, LightParameter.Position, setColorBuffer(vec3D3.xCoord, vec3D3.yCoord, vec3D3.zCoord, 0.0D));
			GL.Light(LightName.Light0, LightParameter.Diffuse, setColorBuffer(f1, f1, f1, 1.0F));
			GL.Light(LightName.Light0, LightParameter.Ambient, setColorBuffer(0.0F, 0.0F, 0.0F, 1.0F));
			GL.Light(LightName.Light0, LightParameter.Specular, setColorBuffer(f2, f2, f2, 1.0F));
			vec3D3 = Vec3D.createVector(-0.20000000298023224D, 1.0D, (double)0.7F).normalize();
			GL.Light(LightName.Light1, LightParameter.Position, setColorBuffer(vec3D3.xCoord, vec3D3.yCoord, vec3D3.zCoord, 0.0D));
			GL.Light(LightName.Light1, LightParameter.Diffuse, setColorBuffer(f1, f1, f1, 1.0F));
			GL.Light(LightName.Light1, LightParameter.Ambient, setColorBuffer(0.0F, 0.0F, 0.0F, 1.0F));
			GL.Light(LightName.Light1, LightParameter.Specular, setColorBuffer(f2, f2, f2, 1.0F));
			GL.ShadeModel(ShadingModel.Flat);
			GL.LightModel(LightModelParameter.LightModelAmbient, setColorBuffer(f0, f0, f0, 1.0F));
		}

		private static float[] setColorBuffer(double d0, double d2, double d4, double d6)
		{
			return setColorBuffer((float)d0, (float)d2, (float)d4, (float)d6);
		}

		private static float[] setColorBuffer(float f0, float f1, float f2, float f3)
		{
			colorBuffer[0] = f0;
			colorBuffer[1] = f1;
			colorBuffer[2] = f2;
			colorBuffer[3] = f3;
			return colorBuffer;
		}

		public static void enableGUIStandardItemLighting()
		{
            Minecraft.newRenderer.ModelMatrix.PushMatrix();
			Minecraft.newRenderer.ModelMatrix.Rotate(-30.0F, 0.0F, 1.0F, 0.0F);
            Minecraft.newRenderer.ModelMatrix.Rotate(165.0F, 1.0F, 0.0F, 0.0F);
			enableStandardItemLighting();
            Minecraft.newRenderer.ModelMatrix.PopMatrix();
		}
	}

}