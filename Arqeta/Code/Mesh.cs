using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arqeta
{
    public class Mesh : Entity
    {
        public async override Task Init()
        {
            await base.Init();
        }
        public async override Task Update()
        {
            await base.Update();
            game.Render(new() { verts = [new() { pos = [0f, 0f, 1f], texpos = [0f, 1f] }, new() { pos = [0.5f, 1f, 1f], texpos = [0f, 1f] }, new() { pos = [1f, 0f, 1f], texpos = [0f, 1f] }] });
        }
        public Mesh(Game game, Transform tran) : base(game, tran)
        {
            
        }
    }
}
