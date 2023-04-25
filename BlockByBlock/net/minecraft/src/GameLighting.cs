using BlockByBlock.net.minecraft.render;
using net.minecraft.client;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace net.minecraft.src
{

	public class GameLighting
	{
		private static float[] vectorBuffer = new float[16];

        private const float ambient = 0.4F;
        private const float diffuse = 0.6F;
        private const float specular = 0.0F;

        public static void InitializeLighting()
        {
            LightRenderer lights = Minecraft.renderPipeline.LightRenderer;

            // Light 0
            lights.SetLightDiffuse(0, new Vector4(diffuse, diffuse, diffuse, 1.0F));
            lights.SetLightAmbient(0, new Vector4(0.0f, 0.0f, 0.0f, 1.0F));
            lights.SetLightSpecular(0, new Vector4(specular, specular, specular, 1.0F));

            // Light 1
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
        }

        public static void DisableMeshLighting()
		{
            Minecraft.renderPipeline.SetState(RenderState.LightingState, false);
		}
        
		public static void EnableMeshLighting()
		{
			Minecraft.renderPipeline.SetState(RenderState.LightingState, true);

            LightRenderer lights = Minecraft.renderPipeline.LightRenderer;
            
            Matrix4 modelView = Minecraft.renderPipeline.ModelMatrix.GetMatrix();
            modelView = new Matrix4(modelView.Row0, modelView.Row1, modelView.Row2, modelView.Row3);

            Vector4 lightPos1 = new(0.2F, 1.0F, -0.7F, 0.0f);
            lightPos1.Normalize();

            Vector4 lightPos2 = new(-0.2F, 1.0F, 0.7F, 0.0f);
            lightPos2.Normalize();

            lightPos1 *= modelView;
            lightPos2 *= modelView;

            // Light 0
            lights.SetLightPos(0, lightPos1);
            
            // Light 1
            lights.SetLightPos(1, lightPos2);
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

		public static void EnableGUIStandardItemLighting()
		{
            Minecraft.renderPipeline.ModelMatrix.PushMatrix(); 
            Minecraft.renderPipeline.ModelMatrix.Rotate(-30.0F, 0.0F, 1.0F, 0.0F);
            Minecraft.renderPipeline.ModelMatrix.Rotate(165.0F, 1.0F, 0.0F, 0.0F);
			EnableMeshLighting();
            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
		}
	}
}