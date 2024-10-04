#pragma once
#include "Window.h"
#include <vector>

class RenderMng
{
private:
	Window* window;
	std::vector<std::vector<float>> batch;
public:
	RenderMng(Window* _window);
	void AddRender(std::vector<float> verts);
	void Render();
	~RenderMng();
};