using BlockByBlock.net.minecraft.render;
using net.minecraft.client;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace net.minecraft.src
{

	public class RenderHelper
	{
		private static float[] vectorBuffer = new float[16];
        private static readonly string lightMatrixName = "lightMatrix";

        public static void disableStandardItemLighting()
		{
            Minecraft.renderPipeline.SetState(RenderState.LightingState, false);
		}
        
		public static void enableStandardItemLighting()
		{
			Minecraft.renderPipeline.SetState(RenderState.LightingState, true);

            LightRenderer lights = Minecraft.renderPipeline.LightRenderer;

            float ambient = 0.4F;
            float diffuse = 0.6F;
            float specular = 0.0F;

            Matrix4 modelView = Minecraft.renderPipeline.ModelMatrix.GetMatrix();
            modelView = new Matrix4(modelView.Row0, modelView.Row1, modelView.Row2, modelView.Row3);

            Vector4 lightPos1 = new(0.2F, 1.0F, -0.7F, 0.0f);
            lightPos1.Normalize();

            Vector4 lightPos2 = new(-0.2F, 1.0F, 0.7F, 0.0f);
            lightPos2.Normalize();

            lightPos1 *= modelView;
            lightPos2 *= modelView;

            // Send the current modelview matrix as a uniform called "lightMatrix"
            int lightMatUniform = Minecraft.renderPipeline.GetUniform(lightMatrixName);
            GL.UniformMatrix4(lightMatUniform, true, ref modelView);

            // Light 0
            lights.SetLightPos(0, lightPos1);
            lights.SetLightDiffuse(0, new Vector4(diffuse, diffuse, diffuse, 1.0F));
            lights.SetLightAmbient(0, new Vector4(0.0f, 0.0f, 0.0f, 1.0F));
            lights.SetLightSpecular(0, new Vector4(specular, specular, specular, 1.0F));

            // Light 1
            lights.SetLightPos(1, lightPos2);
            lights.SetLightDiffuse(1, new Vector4(diffuse, diffuse, diffuse, 1.0F));
            lights.SetLightAmbient(1, new Vector4(0.0f, 0.0f, 0.0f, 1.0F));
            lights.SetLightSpecular(1, new Vector4(specular, specular, specular, 1.0F));

            // Misc parameters.
            lights.SetNumLights(2);

            lights.SetMaterialAmbient(new Vector4(0.2f, 0.2f, 0.2f, 1.0F));
            lights.SetMaterialShininess(0f);
            lights.SetMaterialDiffuse(new Vector4(0.8F, 0.8F, 0.8F, 1.0F));
            lights.SetMaterialSpecular(new Vector4(0.0F, 0.0F, 0.0F, 1.0F));

            lights.SetLightModelAmbient(new Vector4(ambient, ambient, ambient, 1.0f));
            
            //GL.Enable(EnableCap.Lighting);
            //GL.Enable(EnableCap.Light0);
            //GL.Enable(EnableCap.Light1);
            //GL.Enable(EnableCap.ColorMaterial);
            //GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.AmbientAndDiffuse);
            //float f0 = 0.4F;
            //float f1 = 0.6F;
            //float f2 = 0.0F;
            //Vec3D vec3D3 = Vec3D.createVector((double)0.2F, 1.0D, -0.699999988079071D).normalize();
            //GL.Light(LightName.Light0, LightParameter.Position, setColorBuffer(vec3D3.xCoord, vec3D3.yCoord, vec3D3.zCoord, 0.0D));
            //GL.Light(LightName.Light0, LightParameter.Diffuse, setColorBuffer(f1, f1, f1, 1.0F));
            //GL.Light(LightName.Light0, LightParameter.Ambient, setColorBuffer(0.0F, 0.0F, 0.0F, 1.0F));
            //GL.Light(LightName.Light0, LightParameter.Specular, setColorBuffer(f2, f2, f2, 1.0F));
            //vec3D3 = Vec3D.createVector(-0.20000000298023224D, 1.0D, (double)0.7F).normalize();
            //GL.Light(LightName.Light1, LightParameter.Position, setColorBuffer(vec3D3.xCoord, vec3D3.yCoord, vec3D3.zCoord, 0.0D));
            //GL.Light(LightName.Light1, LightParameter.Diffuse, setColorBuffer(f1, f1, f1, 1.0F));
            //GL.Light(LightName.Light1, LightParameter.Ambient, setColorBuffer(0.0F, 0.0F, 0.0F, 1.0F));
            //GL.Light(LightName.Light1, LightParameter.Specular, setColorBuffer(f2, f2, f2, 1.0F));
            //GL.ShadeModel(ShadingModel.Flat);
            //GL.LightModel(LightModelParameter.LightModelAmbient, setColorBuffer(f0, f0, f0, 1.0F));
        }

        private static float[] setVectorBuffer(double d0, double d2, double d4, double d6)
		{
			return setVectorBuffer((float)d0, (float)d2, (float)d4, (float)d6);
		}

		private static float[] setVectorBuffer(float f0, float f1, float f2, float f3)
		{
			vectorBuffer[0] = f0;
			vectorBuffer[1] = f1;
			vectorBuffer[2] = f2;
			vectorBuffer[3] = f3;
			return vectorBuffer;
		}

		public static void enableGUIStandardItemLighting()
		{
            Minecraft.renderPipeline.ModelMatrix.PushMatrix(); 
            Minecraft.renderPipeline.ModelMatrix.Rotate(-30.0F, 0.0F, 1.0F, 0.0F);
            Minecraft.renderPipeline.ModelMatrix.Rotate(165.0F, 1.0F, 0.0F, 0.0F);
			enableStandardItemLighting();
            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
		}
	}
}