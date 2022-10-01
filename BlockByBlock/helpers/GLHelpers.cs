
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
        /// <summary>
        /// Loads a perspective matrix with the given inputs.
        /// PORTING TODO: this is used in EntityRenderer. Make sure it has parity with real GLU.
        /// </summary>
        /// <param name="fovy"></param>
        /// <param name="aspect"></param>
        /// <param name="zNear"></param>
        /// <param name="zFar"></param>
        public static void Perspective(float fovy, float aspect, float zNear, float zFar)
        {
            Matrix4.CreatePerspectiveFieldOfView(fovy, aspect, zNear, zFar, out Matrix4 result);
            
            GL.LoadMatrix(ref result);
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
    }
}
