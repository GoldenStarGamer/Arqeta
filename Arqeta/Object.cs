using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arqeta
{
    public abstract class Object
    {
        Game Game;
        protected Object(Game game) { Game = game; }
        public abstract void Init();
        public abstract void Update();
        public abstract void LateUpdate();
        public abstract void Delete();
    }
}
