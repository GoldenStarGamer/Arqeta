using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arqeta
{
    public class ContentMng
    {
        Dictionary<string, object> Assets = [];
        public T Get<T>(string key)
        {
            if (!Assets.ContainsKey(key))
            {
                if (typeof(T) == typeof(string))
                {
                    Assets[key] = File.ReadAllText(key);
                }
                else Assets[key] = File.ReadAllBytes(key);
            }
            
            return (T)Assets[key];
        }
    }
}
