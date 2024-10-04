#include "RenderMng.h"
#include <iostream>

RenderMng::RenderMng(Window* _window)
{
	window = _window;
}

RenderMng::~RenderMng()
{

}

void RenderMng::AddRender(std::vector<float> verts)
{
	batch.push_back(verts);
}

void RenderMng::Render()
{
	for(std::vector<float> var1 : batch)
	{
		for (auto var2 : var1)
		{
			std::cout << var2 << ", ";
		}
		std::cout << std::endl;
		var1;
	}
	batch.clear();
	//batch.shrink_to_fit();
}