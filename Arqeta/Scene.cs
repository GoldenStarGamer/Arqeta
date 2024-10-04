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
            entities = ents;
            Task[] tasks = [];
            foreach (var ent in entities) tasks.Append(Task.Run(() => ent.Init()));
            Task.WaitAll(tasks);
            entities = new(ents);
        }

        public void Update()
        {
            Task[] tasks = [];
            foreach (var ent in entities) tasks.Append(Task.Run(() => ent.Update()));
            Task.WaitAll(tasks);
            tasks = [];
            foreach (var ent in entities) tasks.Append(Task.Run(() => ent.LateUpdate()));
            Task.WaitAll(tasks);
        }

        ~Scene()
        {
            Task[] tasks = [];
            foreach (var ent in entities) tasks.Append(Task.Run(() => ent.Delete()));
            Task.WaitAll(tasks);
        }
    }
}
