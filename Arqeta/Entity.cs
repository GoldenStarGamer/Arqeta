using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Arqeta
{
    public abstract class Entity : Object
    {
        protected List<Object> Children = [];
        protected Transform transform;
        protected Entity(Game game, Transform tran) : base(game)
        {
            transform = tran;
        }
        public override async Task Update()
        {
            List<Task> tasks = new();
            foreach (var child in Children)
            {
                tasks.Add(child.Update());
                await Task.WhenAll(tasks);
            }
        }

        public override async Task LateUpdate()
        {
            List<Task> tasks = new();
            foreach (var child in Children)
            {
                tasks.Add(child.LateUpdate());
                await Task.WhenAll(tasks);
            }
        }
        public override async Task Init()
        {
            List<Task> tasks = new();
            foreach (var child in Children)
            {
                tasks.Add(child.Init());
                await Task.WhenAll(tasks);
            }
        }
        public override async Task Delete()
        {
            List<Task> tasks = new();
            foreach (var child in Children)
            {
                tasks.Add(child.Delete());
                await Task.WhenAll(tasks);
            }
        }
    }
}
