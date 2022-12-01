
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
        /// Loads a perspective matrix with the given inputs.
        /// PORTING TODO: this is used in EntityRenderer. Make sure it has parity with real GLU.
        /// </summary>
        /// <param name="fovy"></param>
        /// <param name="aspect"></param>
        /// <param name="zNear"></param>
        /// <param name="zFar"></param>
        [Obsolete("This method uses the legacy OpenGL fixed pipeline. Please use the Perspective() method with the programmable pipeline instead.")]
        public static void LegacyPipelinePerspective(float fovy, float aspect, float zNear, float zFar)
        {
            float radians = fovy / 2.0F * (float)Math.PI / 180.0F;
            float deltaZ = zFar - zNear;
            float sine = (float)Math.Sin((double)radians);
            if (deltaZ != 0.0F && sine != 0.0F && aspect != 0.0F)
            {
                float cotangent = (float)Math.Cos((double)radians) / sine;

                Array.Copy(IDENTITY_MATRIX, currentMatrix, 16);

                float[] matrix = currentMatrix;
                
                matrix[0] = cotangent / aspect;
                matrix[5] = cotangent;
                matrix[10] = -(zFar + zNear) / deltaZ;
                matrix[11] = -1.0F;
                matrix[14] = -2.0F * zNear * zFar / deltaZ;
                matrix[15] = 0.0F;
                GL.MultMatrix(matrix);
            }
        }

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
        public static void UnProject(float winx, float winy, float winz, float[] modelMatrix, float[] projMatrix, int[] viewport, float[] objPos)
        {
            // Get a Matrix4 from modelMatrix
            Matrix4 modelMatrix4 = new Matrix4(
                modelMatrix[0], modelMatrix[1], modelMatrix[2], modelMatrix[3],
                modelMatrix[4], modelMatrix[5], modelMatrix[6], modelMatrix[7],
                modelMatrix[8], modelMatrix[9], modelMatrix[10], modelMatrix[11],
                modelMatrix[12], modelMatrix[13], modelMatrix[14], modelMatrix[15]
            );

            Vector3 result = Vector3.Unproject(new Vector3(winx, winy, winz), viewport[0], viewport[1], viewport[2], viewport[3], projMatrix[0], projMatrix[5], modelMatrix4);
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
