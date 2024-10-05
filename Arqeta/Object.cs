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

        /// <summary>
        /// Runs when the scene initiates
        /// </summary>
        public abstract Task Init();

        /// <summary>
        /// Runs every frame
        /// </summary>
        public abstract Task Update();

        /// <summary>
        /// Runs every frame after Update.
        /// Internaly it's used to render so place any render object modifying logic on Update().
        /// </summary>
        public abstract Task LateUpdate();
        public abstract Task Delete();
    }
}
