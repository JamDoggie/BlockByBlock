using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.minecraft.render
{
    public class FogRenderer
    {
        private RenderPipeline renderer;

        private string fogModeName = "FogMode";
        private string fogDensityName = "FogDensity";
        private string fogColorName = "FogColor";
        private string fogStartName = "FogStart";
        private string fogEndName = "FogEnd";

        public FogRenderer(RenderPipeline renderer)
        {
            this.renderer = renderer;
        }

        /// <summary>
        /// Sets the fog mode to use. Default value is linear (0).
        /// </summary>
        public FogType FogMode
        {
            set
            {
                int uniform = renderer.GetUniform(fogModeName);
                GL.Uniform1(uniform, (int)value);
            }
        }

        /// <summary>
        /// Sets the fog density to use if the fog mode is set to exponential (1).
        /// </summary>
        public float FogDensity
        {
            set
            {
                int uniform = renderer.GetUniform(fogDensityName);
                GL.Uniform1(uniform, value);
            }
        }

        /// <summary>
        /// Sets the fog color to use.
        /// </summary>
        public Vector4 FogColor
        {
            set
            {
                int uniform = renderer.GetUniform(fogColorName);
                GL.Uniform4(uniform, value);
            }
        }

        /// <summary>
        /// Sets the start distance of the fog.
        /// </summary>
        public float FogStart
        {
            set
            {
                int uniform = renderer.GetUniform(fogStartName);
                GL.Uniform1(uniform, value);
            }
        }

        /// <summary>
        /// Sets the end distance of the fog.
        /// </summary>
        public float FogEnd
        {
            set
            {
                int uniform = renderer.GetUniform(fogEndName);
                GL.Uniform1(uniform, value);
            }
        }
    }
}
