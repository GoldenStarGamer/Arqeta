#pragma once
#include "RndrVLS.h"

class RenderMng
{
public:
	RenderMng(VLS* _vls);
	Render()
	~RenderMng();

private:
	VLS* vls;
};