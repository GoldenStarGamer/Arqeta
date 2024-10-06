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
                if (!File.Exists("Resources\\" + key)) throw new FileNotFoundException($"FILE {key} NON-EXISTENT");

                if (typeof(T) == typeof(string))
                {
                    Assets[key] = File.ReadAllText("Resources\\" + key);
                }
                else Assets[key] = File.ReadAllBytes("Resources\\" + key);
            }
            
            return (T)Assets[key];
        }

        public bool Exists(string path)
        {
            return File.Exists("Resources\\"+path);
        }
    }
}
