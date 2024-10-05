using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Mathematics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Arqeta
{
    public struct Transform
    {
        public Vector3 position;
        public Vector3 rotation;
        public void Move(Vector3 direction)
        {
            position += direction;
        }

        public void Rotate(Vector2 mouseDelta, float sensitivity)
        {
            rotation += new Vector3() { Yx = mouseDelta * sensitivity };
        }
    }
}

