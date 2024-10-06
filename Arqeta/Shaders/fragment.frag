#version 460
in vec4 texpos;
out vec4 FragColor;

uniform sampler2D tex;

void main()
{
    FragColor = vec4((texpos + 1)/2);
}