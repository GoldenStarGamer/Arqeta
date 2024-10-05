#version 460
in vec2 texpos;
out vec4 FragColor;

uniform sampler2D tex;

void main()
{
    FragColor = vec4(1, 1, 1, 1);
}