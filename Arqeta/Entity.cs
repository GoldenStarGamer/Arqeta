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
        public override async void Update()
        {
            List<Task> tasks = new();
            foreach (var child in Children)
            {
                tasks.Add(Task.Run(() => child.Update()));
                await Task.WhenAll(tasks);
            }
        }
        public override async void LateUpdate()
        {
            List<Task> tasks = new();
            foreach (var child in Children)
            {
                tasks.Add(Task.Run(() => child.LateUpdate()));
                await Task.WhenAll(tasks);
            }
        }
        public override async void Init()
        {
            List<Task> tasks = new();
            foreach (var child in Children)
            {
                tasks.Add(Task.Run(() => child.Init()));
                await Task.WhenAll(tasks);
            }
        }
        public override async void Delete()
        {
            List<Task> tasks = new();
            foreach (var child in Children)
            {
                tasks.Add(Task.Run(() => child.Delete()));
                await Task.WhenAll(tasks);
            }
        }
    }
}
