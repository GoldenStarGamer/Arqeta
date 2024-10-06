using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arqeta
{
    public class BaseCube : Mesh
    {
        public BaseCube(Game game, Transform tran) : base(game, tran)
        {
            renderObject.verts =
            [
                new() { pos = (-1f, -1f,  1f), texpos = (-1f, -1f) },
                new() { pos = ( 1f, -1f,  1f), texpos = ( 1f, -1f) },
                new() { pos = (-1f,  1f,  1f), texpos = (-1f,  1f) },
                new() { pos = ( 1f,  1f,  1f), texpos = ( 1f,  1f) },
                new() { pos = (-1f, -1f, -1f), texpos = (-1f, -1f) },
                new() { pos = ( 1f, -1f, -1f), texpos = ( 1f, -1f) },
                new() { pos = (-1f,  1f, -1f), texpos = (-1f,  1f) },
                new() { pos = ( 1f,  1f, -1f), texpos = ( 1f,  1f) }
            ];
            renderObject.index =
            [
                0, 1, 2,
                1, 2, 3,
                4, 5, 6,
                5, 6, 7,
                0, 2, 4,
                2, 4, 6,
                1, 3, 5,
                3, 5, 7,
                2, 3, 6,
                3, 6, 7,
                0, 1, 4,
                1, 4, 5
            ];
        }
        public async override Task Update()
        {
            await base.Update();
            transform.rotation += new Quaternion(1, 1, 1, 1).ToEulerAngles() * (float)game.DeltaT;
        }
    }
}
