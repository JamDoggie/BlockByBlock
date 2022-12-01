using BlockByBlock.net.minecraft.render;
using net.minecraft.client;
using net.minecraft.render;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace net.minecraft.render
{
    public class RenderPipeline
    {
        public int GLProgram { get; set; }

        public MatrixStack CameraMatrix { get; set; }
        public MatrixStack ModelMatrix { get; set; }
        public MatrixStack TextureMatrix { get; set; }

        #region DEBUG STUFF
        private static DebugProc _debugProcCallback = DebugCallback;
        private static GCHandle _debugProcCallbackHandle;
        #endregion

        public RenderPipeline()
        {
            CameraMatrix = new(this, "projectionMatrix");
            ModelMatrix = new(this, "modelMatrix");
            TextureMatrix = new(this, "textureMatrix");
        }

        public void InitRenderer()
        {
            #region DEBUG PRINTING
            _debugProcCallbackHandle = GCHandle.Alloc(_debugProcCallback);

            GL.DebugMessageCallback(_debugProcCallback, IntPtr.Zero);
            GL.Enable(EnableCap.DebugOutput);
            GL.Enable(EnableCap.DebugOutputSynchronous);
            #endregion

            GLProgram = GL.CreateProgram();

            LoadAndCompileShaders();
            
            GL.LinkProgram(GLProgram);

            GL.UseProgram(GLProgram);

            GL.GetProgram(GLProgram, GetProgramParameterName.LinkStatus, out int success);
            if (success == 0)
            {
                string infoLog = GL.GetProgramInfoLog(GLProgram);
                Console.WriteLine(infoLog);
            }

            CameraMatrix.InitStack();
            ModelMatrix.InitStack();
            TextureMatrix.InitStack();
            
            

            SetState(RenderState.TextureState, true);
            SetState(RenderState.ColorState, false);

            
        }

        public int GetUniform(string uniform)
        {
            return GL.GetUniformLocation(GLProgram, uniform);
        }
        
        public void SetState(RenderState state, bool active)
        {
            int uniform = GetUniform(state.ToString());
            GL.ProgramUniform1(GLProgram, uniform, active ? 1 : 0);
        }

        public void LoadAndCompileShaders()
        {
            if (!Directory.Exists("shaders/fragment") || !Directory.Exists("shaders/vertex"))
            {
                return;
            }
            
            FileInfo[] vertexShaderFiles, fragmentShaderFiles;

            try
            {
                DirectoryInfo vertexDir = new("shaders/vertex");
                DirectoryInfo fragmentDir = new("shaders/fragment");

                vertexShaderFiles = vertexDir.GetFiles();
                fragmentShaderFiles = fragmentDir.GetFiles();

                // Vertex shaders
                foreach (FileInfo vertexShaderFile in vertexShaderFiles)
                {
                    if (vertexShaderFile.Extension != ".glsl")
                    {
                        continue;
                    }

                    int shaderHandle = LoadShader(vertexShaderFile.FullName, ShaderType.VertexShader);

                    GL.AttachShader(GLProgram, shaderHandle);
                }

                // Fragment shaders
                foreach (FileInfo fragShaderFile in fragmentShaderFiles)
                {
                    if (fragShaderFile.Extension != ".glsl")
                    {
                        continue;
                    }

                    int shaderHandle = LoadShader(fragShaderFile.FullName, ShaderType.FragmentShader);

                    GL.AttachShader(GLProgram, shaderHandle);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e);
                throw new ShaderLoadException();
            }
        }

        private static int LoadShader(string path, ShaderType type)
        {
            int shader = GL.CreateShader(type);
            GL.ShaderSource(shader, File.ReadAllText(path));
            GL.CompileShader(shader);

            string infoLog = GL.GetShaderInfoLog(shader);
            if (infoLog != string.Empty)
            {
                Console.WriteLine(infoLog);
            }

            return shader;
        }

        private static void DebugCallback(DebugSource source, DebugType type, int id, DebugSeverity severity, int length, IntPtr message, IntPtr userParam)
        {
            string messageString = Marshal.PtrToStringAnsi(message, length);
            if (severity != DebugSeverity.DebugSeverityNotification)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{severity} [{type}]: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{messageString}");
            }
            
        }
    }

    public class ShaderLoadException : Exception
    {
        public ShaderLoadException(string s) : base(s)
        {
            
        }

        public ShaderLoadException()
        {
            
        }
    }

    public class ShaderCompileException : Exception
    {
        public ShaderCompileException(string s) : base(s)
        {

        }

        public ShaderCompileException()
        {

        }
    }
}
