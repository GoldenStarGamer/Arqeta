// dllmain.cpp : Defines the entry point for the DLL application.

#include "Window.h"
#include "RndrVLS.h"

#define export __declspec(dllexport)

VLS vls;


extern "C"
{

	export void initwindow(int width, int height, const char* title)
	{
		WindowInit(&vls, width, height, title);
	}
	
	typedef void (*ShouldCloseCallback)();
	ShouldCloseCallback close_callback = nullptr;

	export void OnShouldClose(ShouldCloseCallback callback)
	{
		close_callback = callback;
	}

	export void update()
	{
		Update(&vls);
		if (glfwWindowShouldClose(vls.window))
		{
			if (close_callback)
			{
				close_callback();
			}
		}
	}
}