using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Arqeta.Window;

namespace Arqeta
{
    public class Game
    {
        bool close = false;

        Window window;
        Scene scene;

        public Game()
        {
            window = new(800, 600, "Arqeta", () => close = true);

            scene = new([new Mesh(this, new())]);
            while (!close)
            {
                if (GetKey((int)Keys.KEY_ESCAPE))
                {
                    close = true;
                }
                scene.Update();
                window.Update();
            }
        }

        public void Render(RenderObject obj)
        {
            window.Render(obj);
        }
    }
}
