using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arqeta
{
    public abstract class Mesh : Entity
    {
        protected RenderObject renderObject = new();
        public async override Task Init()
        {
            await base.Init();
        }
        public async override Task LateUpdate()
        {
            await base.LateUpdate();
            renderObject.model = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(transform.rotation.X)) * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(transform.rotation.Y)) * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(transform.rotation.Z)) * Matrix4.CreateTranslation(transform.position);
            game.Render(renderObject);
        }
        public Mesh(Game game, Transform tran) : base(game, tran)
        {
            
        }
    }
}
