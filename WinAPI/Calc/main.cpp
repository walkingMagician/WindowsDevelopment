#include<Windows.h>

#define IDC_EDIT_DISPLAY	999
#define IDC_BUTTON_0		1000
#define IDC_BUTTON_1		1001
#define IDC_BUTTON_2		1002
#define IDC_BUTTON_3		1003
#define IDC_BUTTON_4		1004
#define IDC_BUTTON_5		1005
#define IDC_BUTTON_6		1006
#define IDC_BUTTON_7		1007
#define IDC_BUTTON_8		1008
#define IDC_BUTTON_9		1009
#define IDC_BUTTON_POINT	1010

#define IDC_BUTTON_PLUS		1011
#define IDC_BUTTON_MINUS	1012
#define IDC_BUTTON_ASTER	1013 // '*'
#define IDC_BUTTON_SLESH	1014 // '/'

#define IDC_BUTTON_BSP		1015 // BACKSPACE
#define IDC_BUTTON_CLR		1016 // Claear
#define IDC_BUTTON_EQUAL	1017 // '='

CONST CHAR g_sz_WINDOW_CLASS[] = "Calc_VPD_311";

CONST INT g_i_SCREEN_WIDTH = 400;
CONST INT g_i_SCREEN_HEIGHT = 22;

CONST INT g_i_BUTTON_SIZE = 50; // размер кнопки в пикселях
CONST INT g_i_INTERVAL = 5; // растояние между кнопками

CONST INT g_i_START_X = 10; // 
CONST INT g_i_START_Y = 10; // 
CONST INT g_i_BUTTON_START_X = g_i_START_X; // 
CONST INT g_i_BUTTON_START_Y = g_i_START_Y + g_i_SCREEN_HEIGHT + g_i_INTERVAL; // 


INT CALLBACK WndProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

INT WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInst, LPSTR lpCmdLine, INT nCmdShow)
{

	WNDCLASSEX wClass;
	ZeroMemory(&wClass, sizeof(wClass));

	wClass.style = 0;
	wClass.cbSize = sizeof(wClass);
	wClass.cbClsExtra = 0;
	wClass.cbWndExtra = 0;

	wClass.hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_APPLICATION));
	wClass.hIconSm = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_APPLICATION));
	wClass.hCursor = LoadCursor(hInstance, IDC_ARROW);
	wClass.hbrBackground = (HBRUSH)COLOR_WINDOW;
	
	wClass.hInstance = hInstance;
	wClass.lpszClassName = g_sz_WINDOW_CLASS;
	wClass.lpszMenuName = NULL;
	wClass.lpfnWndProc = (WNDPROC)WndProc;

	if (!RegisterClassEx(&wClass))
	{
		MessageBoxA(NULL, "Class registrration", NULL, MB_OK | MB_ICONERROR);
		return 0;
	}

	HWND hwnd = CreateWindowEx
	(
		NULL, g_sz_WINDOW_CLASS,
		g_sz_WINDOW_CLASS, WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, CW_USEDEFAULT,
		CW_USEDEFAULT, CW_USEDEFAULT,
		NULL, NULL, hInstance, NULL
	);

	ShowWindow(hwnd, nCmdShow);
	UpdateWindow(hwnd);
	MSG msg;
	while (GetMessage(&msg, NULL, 0, 0) > 0)
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
		//IsDialogMessage(hwnd, &msg);
	}

	return msg.wParam;
}

INT CALLBACK WndProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	switch (uMsg)
	{
	case WM_CREATE:
	{
		HWND hEdit = CreateWindowEx
		(
			NULL, "Edit", "0",
			WS_CHILD | WS_VISIBLE | WS_BORDER | WS_TABSTOP | ES_RIGHT,
			10, 10, 500, 22,
			hwnd, (HMENU)1000, GetModuleHandle(NULL), NULL
		);
	} break;
	case WM_COMMAND:
		break;
	case WM_DESTROY:
		PostQuitMessage(0); break;
	case WM_CLOSE:
		DestroyWindow(hwnd); break;

	default: return DefWindowProc(hwnd, uMsg, wParam, lParam);
	} 
	return FALSE;
}