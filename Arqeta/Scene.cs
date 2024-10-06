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
            List<Task> tasks = [];
            foreach (var ent in entities) tasks.Add(ent.Init());
            Task.WaitAll(tasks.ToArray());
        }

        public void Update()
        {
            List<Task> tasks = [];
            foreach (var ent in entities) tasks.Add(ent.Update());
            Task.WaitAll(tasks.ToArray());
            tasks = [];
            foreach (var ent in entities) tasks.Add(ent.LateUpdate());
            Task.WaitAll(tasks.ToArray());
        }

        ~Scene()
        {
            List<Task> tasks = [];
            foreach (var ent in entities) tasks.Add(ent.Delete());
            Task.WaitAll(tasks.ToArray());
        }
    }
}
