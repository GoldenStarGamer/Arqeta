using OpenTK.Graphics.OpenGL4;
using StbImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arqeta
{
    public class Texture
    {
        public ImageResult image;
        public Texture(Game game, string path)
        {
            StbImage.stbi_set_flip_vertically_on_load(1);
            image = ImageResult.FromMemory(game.assets.Get<byte[]>(path), ColorComponents.RedGreenBlueAlpha);   
        }

    }
}
