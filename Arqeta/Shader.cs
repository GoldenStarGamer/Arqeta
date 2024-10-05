using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arqeta
{
    public class Shader : IDisposable
    {
        int Handle;
        bool disposed;
        public Shader( string vertPath, string fragPath)
        {
            var vertcode = File.ReadAllText(vertPath);
            var fragcode = File.ReadAllText(fragPath);

            var vertshader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertshader, vertcode);

            var fragshader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragshader, fragcode);

            GL.CompileShader(vertshader);

            GL.GetShader(vertshader, ShaderParameter.CompileStatus, out int success);

            if (success == 0)
            {
                throw new Exception($"VERTEX SHADER COMPILE ERROR: {GL.GetShaderInfoLog(vertshader)}");
            }

            GL.CompileShader(fragshader);

            GL.GetShader(fragshader, ShaderParameter.CompileStatus, out success);

            if (success == 0)
            {
                throw new Exception($"FRAGMENT SHADER COMPILE ERROR: {GL.GetShaderInfoLog(fragshader)}");
            }

            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, vertshader);
            GL.AttachShader(Handle, fragshader);

            GL.LinkProgram(Handle);

            GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out success);

            if(success == 0)
            {
                throw new Exception($"PROGRAM LINK ERROR: {GL.GetProgramInfoLog(Handle)}");
            }

            GL.DetachShader(Handle, vertshader);
            GL.DetachShader(Handle, fragshader);
            GL.DeleteShader(vertshader);
            GL.DeleteShader(fragshader);
        }

        public void Use()
        {
            GL.UseProgram(Handle);
        }

        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(Handle, attribName);
        }

        public void Dispose()
        {
            if (disposed) return;
            GL.DeleteProgram(Handle);
            disposed = true;
            GC.SuppressFinalize(this);
        }

        ~Shader() { if (!disposed) throw new Exception("SHADER LEAK"); }
    }
}
