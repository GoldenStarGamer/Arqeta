using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arqeta
{
    public class Scene
    {
        private List<Entity> entities;
        public Scene(List<Entity> ents)
        {
            entities = new(ents);
        }



        public void Update()
        {

        }
    }
}
