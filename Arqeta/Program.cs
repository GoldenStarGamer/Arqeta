using Arqeta;
using System;
using System.Runtime.InteropServices;

class Program
{

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ShouldCloseCallback();

    [DllImport("ArqetaRndr.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void OnShouldClose(ShouldCloseCallback callback);

    // Import the Add function from the C++ DLL
    [DllImport("ArqetaRndr.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void initwindow(int width, int height, string title);

    [DllImport("ArqetaRndr.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void update();

    [DllImport("ArqetaRndr.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool GetKey(int key);

    static bool close = false;

    public static void OnClose()
    {
        close = true;
    }

    static void Main()
    {
        ShouldCloseCallback callback = new ShouldCloseCallback(OnClose);

        OnShouldClose(callback);

        initwindow(800, 600, "ArqetaEngine");
        while (!close)
        {
            if(GetKey(((int)Keys.KEY_ESCAPE)))
            {
                close = true;
            }
            update();
        }
    }
}