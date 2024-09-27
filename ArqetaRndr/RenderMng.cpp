#include "RenderMng.h"

RenderMng::RenderMng(VLS* _vls)
{
	vls = _vls;
	vls->rendermng = this;
}

RenderMng::~RenderMng()
{

}