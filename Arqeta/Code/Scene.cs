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

        public void Init()
        {
            Task[] tasks = [];
            foreach (var ent in entities) tasks.Append(ent.Init());
            Task.WaitAll(tasks);
        }

        public void Update()
        {
            Task[] tasks = [];
            foreach (var ent in entities) tasks.Append(ent.Update());
            Task.WaitAll(tasks);
            tasks = [];
            foreach (var ent in entities) tasks.Append(ent.LateUpdate());
            Task.WaitAll(tasks);
        }

        ~Scene()
        {
            Task[] tasks = [];
            foreach (var ent in entities) tasks.Append(ent.Delete());
            Task.WaitAll(tasks);
        }
    }
}
