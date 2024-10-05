using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Arqeta
{
    public class Game : GameWindow
    {
        RenderMng mng;   
        Scene scene;
        ContentMng assets;
        public Game(int width, int height, string title) : base(GameWindowSettings.Default, new()
        { 
            ClientSize = (width, height),
            Title = title, API = ContextAPI.OpenGL,
            Profile = ContextProfile.Core,
            APIVersion = new System.Version(4, 6)
        })
        {
            mng = new();
            scene = new([new Mesh(this, new())]);
            assets = new();
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            mng.Init();
            scene.Init();
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
        }

        /// <summary>
        /// Add RenderObject to render batch
        /// </summary>
        /// <param name="obj">object to render</param>
        public void Render(RenderObject obj)
        {
            mng.AddRender(obj);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            scene.Update();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            mng.Render();
            SwapBuffers();
        }

        public T GetAsset<T>(string path)
        {
            return assets.Get<T>(path);
        }
    }
}
