#pragma once
#include <glad/glad.h>
#include <glfw3.h>

class Window
{
private:
	GLFWwindow* window;
public:
	Window(int width, int height, const char* title);
	GLFWwindow* GetWindow() { return window; }
	void Update();
	~Window();
};