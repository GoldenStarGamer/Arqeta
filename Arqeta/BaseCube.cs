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
            renderObject.verts = [new() { pos = (-0.5f, -0.5f, 0f), texpos = (0f, 0f) }, new() { pos = (0f, 0.5f, 0f), texpos = (0.5f, 1f) }, new() { pos = (0.5f, -0.5f, 0f), texpos = (1f, 0f) }];
        }
        public async override Task Update()
        {
            await base.Update();
        }
    }
}
