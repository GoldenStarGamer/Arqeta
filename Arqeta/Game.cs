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

        public Game()
        {
            window = new(600, 600, "Arqeta", () => close = true);
            while (!close)
            {
                if (GetKey((int)Keys.KEY_ESCAPE))
                {
                    close = true;
                }

                window.Update();
            }
        }
    }
}
