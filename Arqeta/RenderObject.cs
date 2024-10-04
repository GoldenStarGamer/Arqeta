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
    }
    public struct RenderObject
    {
        public Vert[] verts;
    }
}
