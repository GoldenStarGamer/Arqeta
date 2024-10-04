#pragma once
#include "Window.h"

class RenderMng
{
private:
	Window* window;
public:
	RenderMng(Window* _window);
	void Render(float* verts, int size);
	~RenderMng();
};