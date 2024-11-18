#include<windows.h>

CONST CHAR g_sz_WINDOW_CLASS[] = "My Main Window";

INT CALLBACK WndProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

INT WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInst, LPSTR lpCmdLine, INT nCmdShow)
{
	WNDCLASSEX wClass;
	ZeroMemory(&wClass, sizeof(wClass));


	wClass.style = 0;
	wClass.cbSize = sizeof(wClass);
	wClass.cbClsExtra = 0;
	wClass.cbWndExtra = 0;
	// cb...  - CountBytes

	wClass.hIcon = LoadIcon(NULL, IDI_APPLICATION);
	wClass.hIconSm = LoadIcon(NULL, IDI_APPLICATION);
	wClass.hCursor = LoadCursor(NULL, IDC_ARROW);
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
		WS_OVERLAPPEDWINDOW, // Window style. ����� ����� ������ ������� ��� �������� ����. 
		CW_USEDEFAULT, CW_USEDEFAULT, // position - ��������� ���� ��� �������
		CW_USEDEFAULT, CW_USEDEFAULT, // size - ������ ������������ �������� 
		NULL,
		NULL, // hMenu - ��� �������� ���� ��� ResourceID �������� ����,
		// ��� ��������� ���� (�������� ������-�� ����)
		// ��� ResourseID ���������������� �������� 
		// ������� ResourseID ������ ����� �������� ��� ������ ������� GetdlgItem()
		hInstance,
		NULL
	);

	if (hwnd == NULL)
	{
		MessageBox(NULL, "Window creation failed", "", MB_OK);
		return 0;
	}
	ShowWindow(hwnd, nCmdShow); // ����� ����� ����������� ���� (��������� �� ���� �����, �������� )
	UpdateWindow(hwnd); // ������������� ����

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