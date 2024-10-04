using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arqeta
{
    public struct Vert
    {
        public float[] pos;
        public float[] texpos;
        public float[] usable()
        {
            return pos.Concat(texpos).ToArray();
        }
    }
    public struct RenderObject
    {
        public Vert[] verts;
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
