using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
struct RndrBuffers
{   
    public int VBO;
    public int VAO;
    public RndrBuffers()
    {
        VBO = GL.GenBuffer();
        VAO = GL.GenVertexArray();
    }
}

namespace Arqeta
{
    public class RenderMng
    {
        Matrix4 projection;
        List<RenderObject> batch = [];
        Shader shader;
        public RenderMng(Vector2 size)
        {
            shader = new("Shaders\\vertex.vert", "Shaders\\fragment.frag");
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90f), size.X / size.Y, 0.1f, 100.0f);
        }

        public void ResizeProject(Vector2 size)
        {
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90f), size.X / size.Y, 0.1f, 100.0f);
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
        }

        public void Render(Camera cam)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            foreach (var item in batch)
            {
                RndrBuffers buffrs = new();
                shader.Use();
                GL.BindBuffer(BufferTarget.ArrayBuffer, buffrs.VBO);
                GL.BufferData(BufferTarget.ArrayBuffer, item.usable().Length * sizeof(float), item.usable(), BufferUsageHint.StreamDraw);
                GL.BindVertexArray(buffrs.VAO);
                GL.VertexAttribPointer(shader.GetAttribLocation("pos"), 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
                GL.EnableVertexAttribArray(shader.GetAttribLocation("pos"));
                GL.VertexAttribPointer(shader.GetAttribLocation("texcoord"), 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
                GL.EnableVertexAttribArray(shader.GetAttribLocation("tex"));
                shader.SetUniform("model", item.model);
                shader.SetUniform("view", Matrix4.CreateTranslation(cam.transform.position) * Matrix4.CreateRotationX(cam.transform.rotation.X) * Matrix4.CreateRotationY(cam.transform.rotation.Y) * Matrix4.CreateRotationZ(cam.transform.rotation.Z));
                shader.SetUniform("project", projection);
                GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
                Free(buffrs);
            }
            batch.Clear();
        }
    }
}
