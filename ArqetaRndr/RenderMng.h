#pragma once
#include "RndrVLS.h"

class RenderMng
{
public:
	RenderMng(VLS* _vls);
	~RenderMng();

private:
	VLS* vls;
};