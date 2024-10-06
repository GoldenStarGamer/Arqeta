using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        public RenderMng(Vector2 size)
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
            foreach (var item in batch)
            {
                RndrBuffers buffrs = new();
                shader.Use();
                GL.BindVertexArray(buffrs.VAO);
                
                GL.BindBuffer(BufferTarget.ArrayBuffer, buffrs.VBO);
                GL.BufferData(BufferTarget.ArrayBuffer, item.usable().Length * sizeof(float), item.usable(), BufferUsageHint.StreamDraw);

                GL.BindBuffer(BufferTarget.ElementArrayBuffer, buffrs.EBO);
                GL.BufferData(BufferTarget.ElementArrayBuffer, item.index.Length * sizeof(uint), item.index, BufferUsageHint.StaticDraw);

                GL.VertexAttribPointer(shader.GetAttribLocation("pos"), 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
                GL.EnableVertexAttribArray(shader.GetAttribLocation("pos"));
                
                GL.VertexAttribPointer(shader.GetAttribLocation("texcoord"), 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
                GL.EnableVertexAttribArray(shader.GetAttribLocation("tex"));
                
                shader.SetUniform("model", item.model);
                shader.SetUniform("view", cam.GetViewMatrix());
                shader.SetUniform("project", cam.GetProjectionMatrix());

                GL.DrawElements(PrimitiveType.Triangles, item.index.Length, DrawElementsType.UnsignedInt, 0);

                Free(buffrs);
            }
            batch.Clear();
        }
    }
}
