using net.minecraft.render;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using sun.java2d.pipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockByBlock.net.minecraft.render
{
    public class MatrixStack
    {
        public Matrix4[] MatrixStackArray { get; set; }
        public int MatrixStackIndex { get; set; }
        public string Name { get; set; }

        private readonly RenderPipeline renderer;

        /// <summary>
        /// This constructor just sets up the underlying buffer and an identity matrix. It can be called before OpenGL is initialized.
        /// </summary>
        /// <param name="renderer"></param>
        /// <param name="stackName"></param>
        public MatrixStack(RenderPipeline renderer, string stackName)
        {
            MatrixStackArray = new Matrix4[64];
            MatrixStackIndex = 0;
            MatrixStackArray[MatrixStackIndex] = Matrix4.Identity;
            this.renderer = renderer;
            Name = stackName;
        }

        public void InitStack()
        {
            UpdateUniform();
        }

        public void UpdateUniform()
        {
            int uniform = renderer.GetUniform(Name);
            GL.UniformMatrix4(uniform, false, ref MatrixStackArray[MatrixStackIndex]);
        }

        public void PushMatrix()
        {
            MatrixStackIndex++;
            MatrixStackArray[MatrixStackIndex] = MatrixStackArray[MatrixStackIndex - 1];
        }

        public void PopMatrix()
        {
            MatrixStackIndex--;

            if (MatrixStackIndex < 0)
                throw new IndexOutOfRangeException("Matrix stack underflow. The stack was popped more than it was pushed!");
        }

        public void Translate(float x, float y, float z)
        {
            // Multiply the current matrix in a way that is identical to GL.Translate()
            MatrixStackArray[MatrixStackIndex] = Matrix4.CreateTranslation(x, y, z) * MatrixStackArray[MatrixStackIndex];
        }

        public void Scale(float x, float y, float z)
        {
            // Multiply the current matrix in a way that is identical to GL.Scale()
            MatrixStackArray[MatrixStackIndex] = Matrix4.CreateScale(x, y, z) * MatrixStackArray[MatrixStackIndex];
        }

        public void Scale(float s)
        {
            Scale(s, s, s);
        }

        public void Rotate(float angle, float x, float y, float z)
        {
            // Multiply the current matrix in a way that is identical to GL.Rotate() in function
            MatrixStackArray[MatrixStackIndex] = Matrix4.CreateFromAxisAngle(new Vector3(x, y, z), (angle * (float)Math.PI / 180f)) * MatrixStackArray[MatrixStackIndex];
        }

        public void LoadIdentity()
        {
            MatrixStackArray[MatrixStackIndex] = Matrix4.Identity;
        }

        public void MultMatrix(Matrix4 matrix)
        {
            MatrixStackArray[MatrixStackIndex] = matrix * MatrixStackArray[MatrixStackIndex];
        }

        public void LoadMatrix(Matrix4 matrix)
        {
            MatrixStackArray[MatrixStackIndex] = matrix;
        }

        public void Ortho(double left, double right, double bottom, double top, double zNear, double zFar)
        {
            MatrixStackArray[MatrixStackIndex] = Matrix4.CreateOrthographicOffCenter((float)left, (float)right, (float)bottom, (float)top, (float)zNear, (float)zFar) * MatrixStackArray[MatrixStackIndex];

            // Do this in a way that is identical to GL.Ortho
            //MatrixStackArray[MatrixStackIndex] *= Matrix4.CreateTranslation((float)(-left - right) / 2f, (float)(-bottom - top) / 2f, (float)(-zNear - zFar) / 2f);
        }
            

        public Matrix4 GetMatrix()
        {
            return MatrixStackArray[MatrixStackIndex];
        }
    }
}
