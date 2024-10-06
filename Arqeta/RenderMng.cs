using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
struct RndrBuffers
{   
    public int VBO;
    public int VAO;
    public int EBO;
    public RndrBuffers()
    {
        VBO = GL.GenBuffer();
        VAO = GL.GenVertexArray();
        EBO = GL.GenBuffer();
    }
}

namespace Arqeta
{
    public class RenderMng
    {
        List<RenderObject> batch = [];
        Shader shader;
        public RenderMng()
        {
            shader = new("Shaders\\vertex.vert", "Shaders\\fragment.frag");
        }

        public void Init()
        {
            GL.ClearColor(0f, 1f, 0f, 1f);
            GL.Enable(EnableCap.DepthTest);
        }
        public void AddRender(RenderObject obj)
        {
            batch.Add(obj);
        }

        static void Free(RndrBuffers buffrs)
        {
            GL.BindVertexArray(buffrs.VAO);
            GL.DeleteVertexArray(buffrs.VAO);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(buffrs.VBO);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.DeleteBuffer(buffrs.EBO);
        }

        public void Render(Camera cam)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            RndrBuffers buffrs = new();
            shader.Use();
            GL.BindVertexArray(buffrs.VAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffrs.VBO);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, buffrs.EBO);

            GL.VertexAttribPointer(shader.GetAttribLocation("pos"), 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(shader.GetAttribLocation("pos"));
            GL.VertexAttribPointer(shader.GetAttribLocation("texcoord"), 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(shader.GetAttribLocation("texcoord"));

            shader.SetUniform("view", cam.GetViewMatrix());
            shader.SetUniform("project", cam.GetProjectionMatrix());

            foreach (var item in batch)
            {
                int texhandle;

                int LayerCount = item.textures.Length;
                texhandle = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2DArray, texhandle);

                GL.TexImage3D(TextureTarget.Texture2DArray, 0, PixelInternalFormat.Rgba, item.textures[0].image.Width, item.textures[0].image.Height, LayerCount, 0, PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);

                for (int i = 0; i < LayerCount; i++)
                {
                    GL.TexSubImage3D(TextureTarget.Texture2DArray, 0, 0, 0, i, item.textures[i].image.Width, item.textures[i].image.Height, 1, PixelFormat.Rgba, PixelType.UnsignedByte, item.textures[i].image.Data);
                }

                GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

                GL.GenerateMipmap(GenerateMipmapTarget.Texture2DArray);

                GL.BindTexture(TextureTarget.Texture2DArray, texhandle); // Bind the texture array before drawing
                shader.SetUniform("model", item.model);

                GL.BufferData(BufferTarget.ArrayBuffer, item.usable().Length * sizeof(float), item.usable(), BufferUsageHint.StreamDraw);
                GL.BufferData(BufferTarget.ElementArrayBuffer, item.index.Length * sizeof(uint), item.index, BufferUsageHint.StaticDraw);

                GL.DrawElements(PrimitiveType.Triangles, item.index.Length, DrawElementsType.UnsignedInt, 0);
            }

            Free(buffrs);
            batch.Clear();
        }
    }
}
