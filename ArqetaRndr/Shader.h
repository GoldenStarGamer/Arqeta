#pragma once
#include "RndrVLS.h"

class Shader
{
public:
	Shader(VLS* vls);

	~Shader();

private:
	VLS* vls;
};
