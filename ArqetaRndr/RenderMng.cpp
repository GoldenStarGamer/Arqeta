#include "RenderMng.h"
#include <iostream>

RenderMng::RenderMng(Window* _window)
{
	window = _window;
}

RenderMng::~RenderMng()
{

}

void RenderMng::Render(float* verts, int size)
{
	for (int i = 0; i < size; i++)
	{
		std::cout << verts[i] << ", ";
	}
	std::cout << "render" << std::endl;
}