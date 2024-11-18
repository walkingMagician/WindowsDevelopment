#include<windows.h>
#pragma comment(lib,"user32")
#include "resource.h"


CONST CHAR g_sz_WINDOW_CLASS[] = "My Main Window";

INT CALLBACK WndProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
int WindowSizeX();
int WindowSizeY();

INT WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInst, LPSTR lpCmdLine, INT nCmdShow)
{

	WNDCLASSEX wClass;
	ZeroMemory(&wClass, sizeof(wClass));


	wClass.style = 0;
	wClass.cbSize = sizeof(wClass);
	wClass.cbClsExtra = 0;
	wClass.cbWndExtra = 0;
	// cb...  - CountBytes

	wClass.hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_ICON1)); 
	// Используется для отображения значка окна в заголовке окна и на панелях задач
	wClass.hIconSm = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_ICON2));
	// предназначен для использования в заголовках окон и в списке задач
	wClass.hCursor = LoadCursor(hInstance, IDC_ARROW);
	wClass.hbrBackground = (HBRUSH)COLOR_WINDOW;

	wClass.hInstance = hInstance;
	wClass.lpszMenuName = NULL;
	wClass.lpszClassName = g_sz_WINDOW_CLASS;
	wClass.lpfnWndProc = (WNDPROC)WndProc;

	if (!RegisterClassEx(&wClass))
	{
		MessageBox(NULL, "Class registration failed", "", MB_OK);
		return 0;
	}

	HWND hwnd = CreateWindowEx
	(
		NULL, // Ex-Styles
		g_sz_WINDOW_CLASS, // Class name
		g_sz_WINDOW_CLASS, // Window Title
		WS_OVERLAPPEDWINDOW, // Window style. такой стиль всегда задаётся для главного окна. 
		CW_USEDEFAULT, CW_USEDEFAULT, // position - положение окна при запуске от левого верхнего угла (px)
		WindowSizeX(), WindowSizeY(), // size - размер создоваемого окнаокна (px) 
		NULL,
		NULL, // hMenu - для главного окна это ResourceID главного меню,
		// для дочернего окна (элимента какого-то окна)
		// это ResourseID соответствующего этимента 
		// поэтому ResourseID всегда можно получить при помощи функции GetdlgItem()
		hInstance,
		NULL
	);

	if (hwnd == NULL)
	{
		MessageBox(NULL, "Window creation failed", "", MB_OK);
		return 0;
	}
	ShowWindow(hwnd, nCmdShow); // задаёт режим отображения окна (развёрнуто на весь экран, свёрнутое )
	UpdateWindow(hwnd); // прорисовывает окно

	MSG msg;
	while (GetMessage(&msg, NULL, 0, 0) > 0)
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}

	return msg.wParam;
}

INT CALLBACK WndProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	switch (uMsg)
	{
	case WM_CREATE:
	{} break;

	case WM_COMMAND:
	{} break;

	case WM_DESTROY:
	{
		PostQuitMessage(0);
	} break;

	case WM_CLOSE:
	{
		DestroyWindow(hwnd);
	} break;

	default: return DefWindowProc(hwnd, uMsg, wParam, lParam);
	}
	return 0;
}

int WindowSizeX()
{
	// Получение размера экрана
	int screenWidth = GetSystemMetrics(SM_CXSCREEN);

	// Рассчитываем размеры окна (50% высоты и ширины)
	int windowWidth = screenWidth / 2;
	return windowWidth;
}
int WindowSizeY()
{
	// Получение размера экрана
	int screenHeight = GetSystemMetrics(SM_CYSCREEN);

	// Рассчитываем размеры окна (50% высоты и ширины)
	int windowHeight = screenHeight / 2;
	return windowHeight;
}