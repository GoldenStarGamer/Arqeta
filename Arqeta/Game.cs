using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
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
        Camera camera;
        public Game(int width, int height, string title) : base(GameWindowSettings.Default, new()
        { 
            ClientSize = (width, height),
            Title = title, API = ContextAPI.OpenGL,
            Profile = ContextProfile.Core,
            APIVersion = new System.Version(4, 6)
        })
        {
            mng = new(Size);
            scene = new([new BaseCube(this, new()), new BaseCube(this, new() { position = (1f, -1f, -1f)})]);
            assets = new();
            camera = new(new() { position = (0f, 0f, -3f)});
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
            mng.ResizeProject(Size);
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
            var input = KeyboardState;

            Vector3 movement = Vector3.Zero;

            movement += Vector3.UnitZ * (input.IsKeyDown(Keys.W) ? 1 : 0);
            movement += Vector3.UnitZ * -1 * (input.IsKeyDown(Keys.S) ? 1 : 0);
            movement += Vector3.UnitX * (input.IsKeyDown(Keys.A) ? 1 : 0);
            movement += Vector3.UnitX * -1 * (input.IsKeyDown(Keys.D) ? 1 : 0);
            movement += Vector3.UnitY * (input.IsKeyDown(Keys.LeftControl) ? 1 : 0);
            movement += Vector3.UnitY * -1 * (input.IsKeyDown(Keys.Space) ? 1 : 0);

            if (movement != Vector3.Zero) camera.transform.Move(movement * (float)args.Time);
            scene.Update();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            mng.Render(camera);
            SwapBuffers();
        }

        public T GetAsset<T>(string path)
        {
            return assets.Get<T>(path);
        }
    }
}
