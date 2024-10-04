// dllmain.cpp : Defines the entry point for the DLL application.

#include "Window.h"
#include "RenderMng.h"
#include <iostream>

#define export __declspec(dllexport)

Window* window;
RenderMng* rendermng;

extern "C"
{

	export void initwindow(int width, int height, const char* title)
	{
		window = new Window(width, height, title);
		rendermng = new RenderMng(window);
	}
	
	typedef void (*ShouldCloseCallback)();
	ShouldCloseCallback close_callback = nullptr;

	export void OnShouldClose(ShouldCloseCallback callback)
	{
		close_callback = callback;
	}

	export void update()
	{
		window->Update();
		if (glfwWindowShouldClose(window->GetWindow()))
		{
			if (close_callback)
			{
				close_callback();
			}
			
			delete rendermng;
			delete window;
			rendermng = nullptr;
			window = nullptr;
		}
	}

	export bool GetKey(int key)
	{
		return glfwGetKey(window->GetWindow(), key);
	}

	export void render(float* verts, int size)
	{
		rendermng->Render(verts, size);
	}
}