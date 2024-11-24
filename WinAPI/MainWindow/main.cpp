#define _CRT_SECURE_NO_WARNINGS
#include<windows.h>
#include<stdio.h>
#pragma comment(lib,"user32")
#include "resource.h"

#define IDC_STATIC	1000
#define IDC_EDIT	1001
#define IDC_BUTTON	1002


CONST CHAR g_sz_WINDOW_CLASS[] = "My Main Window";

INT CALLBACK WndProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
int WindowSizeX();
int WindowSizeY();
void CenterWindow(HWND hwnd); // изменение окна по коррекции положения(x, y), ширине, высоте, 




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
	//wClass.hIcon = (HICON)LoadImage(hInstance, "ICO\\hIcon32.ico", IMAGE_BITMAP, LR_DEFAULTSIZE, LR_DEFAULTSIZE, LR_LOADFROMFILE);
	// Используется для отображения значка окна в заголовке окна и на панелях задач
	wClass.hIconSm = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_ICON2));
	//wClass.hIconSm = (HICON)LoadImage(hInstance, "ICO\\hIconSm16.ico", IMAGE_BITMAP, LR_DEFAULTSIZE, LR_DEFAULTSIZE, LR_LOADFROMFILE);
	// предназначен для использования в заголовках окон и в списке задач
	wClass.hCursor = (HCURSOR)LoadImage(hInstance, "Cursor\\starcraft-original\\Working In Background.ani", IMAGE_CURSOR, LR_DEFAULTSIZE, LR_DEFAULTSIZE, LR_LOADFROMFILE);
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
		0, 0, // size - размер создоваемого окнаокна (px) 
		NULL,
		NULL, // hMenu - для главного окна это ResourceID главного меню,
		// для дочернего окна (элимента какого-то окна)
		// это ResourseID соответствующего этимента 
		// поэтому ResourseID всегда можно получить при помощи функции GetdlgItem()
		// кроме того этот resourseID будет "прилетать" в параметре LOWORD(wParam)
		// при воздействии пользователя 
		hInstance,
		NULL
	);

	if (hwnd == NULL)
	{
		MessageBox(NULL, "Window creation failed", "", MB_OK);
		return 0;
	}
	
	//HCURSOR hCursor = (HCURSOR)LoadImage(hInstance, "amogus.ani", IMAGE_CURSOR, 0, 0, LR_LOADFROMFILE);
	//if (hCursor) {
	//	// Установка курсора для окна
	//	SetClassLongPtr(hwnd, GCLP_HCURSOR, (LONG_PTR)hCursor);
	//}

	CenterWindow(hwnd);
	ShowWindow(hwnd, nCmdShow); // задаёт режим отображения окна (развёрнуто на весь экран, свёрнутое )
	UpdateWindow(hwnd); // прорисовывает окно
	

	MSG msg;
	while (GetMessage(&msg, NULL, 0, 0) > 0)
	{
		//TranslateMessage(&msg);
		//DispatchMessage(&msg);
		IsDialogMessage(hwnd, &msg);
	}

	return msg.wParam;
}

INT CALLBACK WndProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{

	HCURSOR hcHand, hcArrow;
	switch (uMsg)
	{
	case WM_CREATE:
	{
		HWND hStatic = CreateWindowEx
		(
			NULL, 
			"Static",
			"",
			WS_CHILD | WS_VISIBLE | WS_TABSTOP,
			10, 10, 
			500, 22, 
			hwnd, 
			(HMENU)IDC_STATIC,
			GetModuleHandle(NULL), 
			NULL
		);

		HWND hEdit = CreateWindowEx
		(
			NULL, "Edit", "",
			WS_CHILD |WS_VISIBLE | WS_BORDER | ES_CENTER | WS_TABSTOP,
			10, 32, 500, 22,
			hwnd, (HMENU)IDC_EDIT, GetModuleHandle(NULL), NULL
			);

		HWND hButton = CreateWindowEx
		(
			NULL, "Button", "применить",
			WS_CHILD | WS_VISIBLE | BS_PUSHBUTTON | WS_TABSTOP,
			410, 58, 100, 22, 
			hwnd, (HMENU)IDC_BUTTON, GetModuleHandle(NULL), NULL
		);

	} break;
	

	case WM_MOVE: 
	{
		RECT rect;
		GetWindowRect(hwnd, &rect);  // Получаем положение окна

		int width = rect.right - rect.left;
		int height = rect.bottom - rect.top;

		// Форматируем строку для заголовка
		char title[100];
		sprintf(title, "%s, X: %i, Y: %i, Width: %i, Height: %i", g_sz_WINDOW_CLASS, rect.left, rect.top, width, height);
		SendMessage(hwnd, WM_SETTEXT, 0, (LPARAM)title);
		//SetWindowText(hwnd, title);  // Устанавливаем заголовок окна

	} break;

	case WM_COMMAND:
	{
		switch (LOWORD(wParam))
		{
		case IDC_BUTTON:
		{
			HWND hEdit = GetDlgItem(hwnd, IDC_EDIT);
			HWND hStatic = GetDlgItem(hwnd, IDC_STATIC);
			CONST INT SIZE = 256;
			CHAR sz_buffer[SIZE]{};
			SendMessage(hEdit, WM_GETTEXT, SIZE, (LPARAM)sz_buffer);
			SendMessage(hStatic, WM_SETTEXT, 0, (LPARAM)sz_buffer);
			break;
		} 

		} break;
	} break;

	
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

	// Рассчитываем размеры окна (75% ширины)
	int windowX = (screenWidth / 4) * 3;
	return windowX;
}
int WindowSizeY()
{
	// Получение размера экрана
	int screenHeight = GetSystemMetrics(SM_CYSCREEN);

	// Рассчитываем размеры окна (75% высоты)
	int windowY = (screenHeight / 4) * 3;
	return windowY;
}

void CenterWindow(HWND hwnd)
{
	// Получаем размеры экрана
	RECT rect;
	SystemParametersInfo(SPI_GETWORKAREA, 0, &rect, 0);

	// Вычисляем координаты для центрирования окна
	int x = (rect.right - rect.left) / 2 - (WindowSizeX() / 2);
	int y = (rect.bottom - rect.top) / 2 - (WindowSizeY() / 2);

	// Устанавливаем положение окна
	MoveWindow(hwnd, x, y, WindowSizeX(), WindowSizeY(), FALSE);
	//SetWindowPos(hwnd, NULL, x, y, 0, 0, SWP_NOSIZE | SWP_NOZORDER);

}