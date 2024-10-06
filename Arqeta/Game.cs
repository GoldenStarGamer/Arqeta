using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Common.Input;
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
            CursorState = CursorState.Grabbed;
            mng = new(Size);
            scene = new([new BaseCube(this, new()), new BaseCube(this, new() { position = (1f, -1f, -1f)})]);
            assets = new();
            camera = new(Vector3.UnitZ * 3, Size.X / (float)Size.Y);
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
            camera.AspectRatio = Size.X / (float)Size.Y;
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

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            const float cameraSpeed = 1.5f;
            const float sensitivity = 0.2f;

            if (input.IsKeyDown(Keys.W))
            {
                camera.Position += camera.Front * cameraSpeed * (float)args.Time; // Forward
            }

            if (input.IsKeyDown(Keys.S))
            {
                camera.Position -= camera.Front * cameraSpeed * (float)args.Time; // Backwards
            }
            if (input.IsKeyDown(Keys.A))
            {
                camera.Position -= camera.Right * cameraSpeed * (float)args.Time; // Left
            }
            if (input.IsKeyDown(Keys.D))
            {
                camera.Position += camera.Right * cameraSpeed * (float)args.Time; // Right
            }
            if (input.IsKeyDown(Keys.Space))
            {
                camera.Position += camera.Up * cameraSpeed * (float)args.Time; // Up
            }
            if (input.IsKeyDown(Keys.LeftShift))
            {
                camera.Position -= camera.Up * cameraSpeed * (float)args.Time; // Down
            }

            var mouse = MouseState;

            // Apply the camera pitch and yaw (we clamp the pitch in the camera class)
            camera.Yaw += mouse.Delta.X * sensitivity;
            camera.Pitch -= mouse.Delta.Y * sensitivity; // Reversed since y-coordinates range from bottom to top

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
