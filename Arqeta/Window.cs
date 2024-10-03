using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Arqeta
{
    public class Window
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ShouldCloseCallback();

        [DllImport("ArqetaRndr.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void OnShouldClose(ShouldCloseCallback callback);

        // Import the Add function from the C++ DLL
        [DllImport("ArqetaRndr.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void initwindow(int width, int height, string title);

        [DllImport("ArqetaRndr.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void update();

        [DllImport("ArqetaRndr.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetKey(int key);

        public Window(int width, int height, string title, ShouldCloseCallback onClose)
        {
            OnShouldClose(onClose);
            initwindow(width, height, title);
        }
        public void Update()
        {
            update();
        }
    }
}
