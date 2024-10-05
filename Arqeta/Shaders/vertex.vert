#version 460
in vec3 pos;
in vec2 texcoord;
out vec2 texpos;

void main()
{
	gl_Position = vec4(pos, 1);
	texpos = texcoord;
}