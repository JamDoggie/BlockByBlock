using BlockByBlock.net.minecraft.render;
using net.minecraft.client;
using net.minecraft.render;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
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

        public MatrixStack ProjectionMatrix { get; set; }
        public MatrixStack ModelMatrix { get; set; }
        public MatrixStack TextureMatrix { get; set; }

        public LightRenderer LightRenderer { get; set; }
        public FogRenderer FogRenderer { get; set; }

        public Vector2 LightmapCoords { get; set; } = new Vector2(0, 0);

        public Vector3 CurrentNormal { get; set; } = new(0); // Default normal of 0,0,0

        #region DEBUG STUFF
        private static DebugProc _debugProcCallback = DebugCallback;
        private static GCHandle _debugProcCallbackHandle;
        #endregion

        private Dictionary<string, int> _uniformLocations = new();
        private Dictionary<RenderState, int> _renderStateUniformLocations = new();

        public RenderPipeline()
        {
            ProjectionMatrix = new(this, "projectionMatrix");
            ModelMatrix = new(this, "modelViewMatrix");
            TextureMatrix = new(this, "textureMatrix");

            LightRenderer = new(this);
            FogRenderer = new(this);
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

            ProjectionMatrix.InitStack();
            ModelMatrix.InitStack();
            TextureMatrix.InitStack();

            SetState(RenderState.TextureState, true);
            SetState(RenderState.ColorState, true);

            Tessellator.instance.Init();
            GameLighting.InitializeLighting();
        }

        public int GetUniform(string uniform)
        {
            if (_uniformLocations.ContainsKey(uniform))
            {
                return _uniformLocations[uniform];
            }

            int location = GL.GetUniformLocation(GLProgram, uniform);
            _uniformLocations.Add(uniform, location);

            return location;
        }

        public int GetRenderStateUniform(RenderState state)
        {
            if (_renderStateUniformLocations.ContainsKey(state))
            {
                return _renderStateUniformLocations[state];
            }

            int location = GetUniform(state.ToString());
            _renderStateUniformLocations.Add(state, location);

            return location;
        }

        public void SetState(RenderState state, bool active)
        {
            int uniform = GetRenderStateUniform(state);
            GL.ProgramUniform1(GLProgram, uniform, active ? 1 : 0);
        }

        public void SetColor(float r, float g, float b, float a)
        {
            int uniform = GetUniform("GlobalColor");
            GL.ProgramUniform4(GLProgram, uniform, r, g, b, a);
        }


        public void SetColor(float f)
        {
            SetColor(f, f, f, f);
        }

        public void SetColor(float r, float g, float b)
        {
            SetColor(r, g, b, 1.0f);
        }

        public void SetNormal(float x, float y, float z, bool normalize = false)
        {
            Vector4 normalVec = new(x, y, z, 1.0f);
            CurrentNormal = normalVec.Xyz;

            if (normalize)
                CurrentNormal.Normalize();
        }

        public void SetLightmapCoords(float x, float y)
        {
            LightmapCoords = new(x, y);
        }

        private string brightnessOverride = "BrightnessOverride";

        internal void SetBrightnessOverrideCoords(float x, float y)
        {
            int uniform = GetUniform(brightnessOverride);

            GL.ProgramUniform2(GLProgram, uniform, x, y);
        }

        public void AlphaTestThreshold(float threshold)
        {
            int uniform = GetUniform("AlphaTestThreshold");
            GL.ProgramUniform1(GLProgram, uniform, threshold);
        }

        public void SetActiveTexture(int texture)
        {
            int uniform = GetUniform("activeTexture");
            GL.ProgramUniform1(GLProgram, uniform, texture);
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

    public struct ShaderInclude
    {
        public string Name;
        public string Source;
    }
}
