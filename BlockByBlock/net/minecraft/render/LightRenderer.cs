using net.minecraft.render;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockByBlock.net.minecraft.render
{
    public class LightRenderer
    {
        private RenderPipeline renderer;

        // Precached strings
        private string materialAmbientName = "material_ambient";
        private string materialDiffuseName = "material_diffuse";
        private string materialSpecularName = "material_specular";
        private string materialShininessName = "material_shininess";

        private string lightModelAmbientName = "LightModelAmbient";

        public LightRenderer(RenderPipeline renderer)
        {
            this.renderer = renderer;
        }

        public void SetNumLights(int numLights)
        {
            //int uniform = GL.GetUniformLocation(renderer.GLProgram, numLightsName);
            //GL.Uniform1(uniform, numLights);
        }

        public void SetLightModelAmbient(Vector4 ambient)
        {
            int uniform = GL.GetUniformLocation(renderer.GLProgram, lightModelAmbientName);
            GL.Uniform4(uniform, ambient);
        }

        public void SetLightPos(int index, Vector3 pos)
        {
            int uniform = renderer.GetUniform($"LightSources[{index}].position");
            GL.Uniform4(uniform, pos.X, pos.Y, pos.Z, 0f);
        }

        public void SetLightPos(int index, Vector4 pos)
        {
            int uniform = renderer.GetUniform($"LightSources[{index}].position");
            GL.Uniform4(uniform, pos.X, pos.Y, pos.Z, pos.W);
        }

        public void SetLightAmbient(int index, Vector4 ambient)
        {
            int uniform = renderer.GetUniform($"LightSources[{index}].ambient");
            GL.Uniform4(uniform, ambient);
        }

        public void SetLightDiffuse(int index, Vector4 diffuse)
        {
            int uniform = renderer.GetUniform($"LightSources[{index}].diffuse");
            GL.Uniform4(uniform, diffuse);
        }

        public void SetLightSpecular(int index, Vector4 specular)
        {
            int uniform = renderer.GetUniform($"LightSources[{index}].specular");
            GL.Uniform4(uniform, specular);
        }

        public void SetMaterialAmbient(Vector4 ambient)
        {
            int uniform = renderer.GetUniform(materialAmbientName);
            GL.Uniform4(uniform, ambient);
        }

        public void SetMaterialDiffuse(Vector4 diffuse)
        {
            int uniform = renderer.GetUniform(materialDiffuseName);
            GL.Uniform4(uniform, diffuse);
        }

        public void SetMaterialSpecular(Vector4 specular)
        {
            int uniform = renderer.GetUniform(materialSpecularName);
            GL.Uniform4(uniform, specular);
        }

        public void SetMaterialShininess(float shininess)
        {
            int uniform = renderer.GetUniform(materialShininessName);
            GL.Uniform1(uniform, shininess);
        }
    }
}
