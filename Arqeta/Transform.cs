using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Mathematics;
using System.Text;
using System.Threading.Tasks;

namespace Arqeta
{
    public struct Transform
    {
        public Vector3 position;
        public Vector3 rotation;
        public void Move(Vector3 direction)
        {
            Vector3 rotatedDirection = Vector3.Transform(direction, Quaternion.FromEulerAngles(rotation));

            float lengh = rotatedDirection.Length;

            rotatedDirection.Normalize();

            Console.WriteLine(rotatedDirection);

            position += rotatedDirection * lengh;
        }
    }
    
}
