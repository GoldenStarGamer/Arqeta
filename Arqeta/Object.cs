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
        protected Game game;
        protected Object(Game _game) { game = _game; }
        public abstract void Init();
        public abstract void Update();
        public abstract void LateUpdate();
        public abstract void Delete();
    }
}
