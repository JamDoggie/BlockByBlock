
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace BlockByBlock.helpers
{
    public static class Glu
    {
        private static float[] IDENTITY_MATRIX = new float[] { 1.0F, 0.0F, 0.0F, 0.0F, 0.0F, 1.0F, 0.0F, 0.0F, 0.0F, 0.0F, 1.0F, 0.0F, 0.0F, 0.0F, 0.0F, 1.0F };
        private static float[] currentMatrix = new float[16];
        
        /// <summary>
        /// Creates a perpsective matrix with the given arguments.
        /// </summary>
        /// <param name="fovy"></param>
        /// <param name="aspect"></param>
        /// <param name="zNear"></param>
        /// <param name="zFar"></param>
        /// <returns></returns>
        public static Matrix4 Perspective(float fovy, float aspect, float zNear, float zFar)
        {
            float radians = fovy / 2.0F * (float)Math.PI / 180.0F;
            float deltaZ = zFar - zNear;
            float sine = (float)Math.Sin((double)radians);
            if (deltaZ != 0.0F && sine != 0.0F && aspect != 0.0F)
            {
                float cotangent = (float)Math.Cos((double)radians) / sine;

                Matrix4 matrix = Matrix4.Identity;
                
                matrix.M11 = cotangent / aspect;
                matrix.M22 = cotangent;
                matrix.M33 = -(zFar + zNear) / deltaZ;
                matrix.M34 = -1.0F;
                matrix.M43 = -2.0F * zNear * zFar / deltaZ;
                matrix.M44 = 0.0F;
                
                return matrix;
            }

            return Matrix4.Identity;
        }

        /// <summary>
        /// Unprojects the given model vertices from screenspace to worldspace with the given inputs. objPos is the output buffer.
        /// Uses OpenTK's Vector3.Unproject()
        /// </summary>
        /// <param name="winx"></param>
        /// <param name="winy"></param>
        /// <param name="winz"></param>
        /// <param name="modelMatrix"></param>
        /// <param name="projMatrix"></param>
        /// <param name="viewport"></param>
        /// <param name="objPos"></param>
        public static void UnProject(float winx, float winy, float winz, Matrix4 modelMatrix, Matrix4 projMatrix, int[] viewport, float[] objPos)
        {
            Vector3 result = Vector3.Unproject(new Vector3(winx, winy, winz), viewport[0], viewport[1], viewport[2], viewport[3], projMatrix[0, 0], projMatrix[1, 1], (modelMatrix * projMatrix).Inverted());
            objPos[0] = result.X;
            objPos[1] = result.Y;
            objPos[2] = result.Z;
        }

        private static void CreatePerspectiveFieldOfView(float fovy, float aspect, float depthNear, float depthFar, out Matrix4 result)
        {
            if (aspect <= 0f)
            {
                throw new ArgumentOutOfRangeException("aspect");
            }

            if (depthNear <= 0f)
            {
                throw new ArgumentOutOfRangeException("depthNear");
            }

            if (depthFar <= 0f)
            {
                throw new ArgumentOutOfRangeException("depthFar");
            }

            float num = depthNear * MathF.Tan(0.5f * fovy);
            float num2 = 0f - num;
            float left = num2 * aspect;
            float right = num * aspect;
            Matrix4.CreatePerspectiveOffCenter(left, right, num2, num, depthNear, depthFar, out result);
        }
    }
}
