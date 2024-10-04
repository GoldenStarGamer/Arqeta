using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
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
        List<RenderObject> batch = [];
        public void Init()
        {
            GL.ClearColor(0f, 0f, 1f, 1f);
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

        public void Render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            foreach (var item in batch)
            {
                RndrBuffers buffrs = new();
                GL.BindBuffer(BufferTarget.ArrayBuffer, buffrs.VBO);
                GL.BufferData(BufferTarget.ArrayBuffer, item.usable().Length * sizeof(float), item.usable(), BufferUsageHint.StreamDraw);
                GL.BindVertexArray(buffrs.VAO);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
                Free(buffrs);
            }
            batch.Clear();
        }
    }
}
