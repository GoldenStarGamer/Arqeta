using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arqeta
{
    public struct Vert
    {
        public Vector3 pos;
        public Vector2 texpos;
        public float[] usable()
        {

            float[] list = new float[5];

            pos.Deconstruct(out float x, out float y, out float z);
            list[0] = x;
            list[1] = y;
            list[2] = z;

            texpos.Deconstruct(out x, out y);
            list[3] = x;
            list[4] = y;

            return list;
        }
    }
    public struct RenderObject
    {
        public Vert[] verts;
        public Texture texture;
        public Matrix4 model;
        public float[] usable()
        {
            List<float> list = new();
            foreach (var item in verts)
            {
                list.AddRange(item.usable());
            }
            return list.ToArray();
        }
    }
}
