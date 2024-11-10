#define _CRT_SECURE_NO_WARNINGS
#include <windows.h>
#include <cstdio>
#include "resource.h"

CONST CHAR* g_VALUES[] = { "This", "is", "my", "first", "List", "Box" };

BOOL CALLBACK DlgProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
BOOL CALLBACK DlgProcAddItem(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

INT WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInst, LPSTR lpCmdLine, INT nCmdShow)
{
	
	DialogBoxParam(hInstance, MAKEINTRESOURCE(IDD_DIALOG1), NULL, DlgProc, 0);

	return 0;
}

BOOL CALLBACK DlgProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	switch (uMsg)
	{
	case WM_INITDIALOG:
	{
		HICON hIcon = LoadIcon(GetModuleHandle(NULL), MAKEINTRESOURCE(IDI_ICON1));
		SendMessage(hwnd, WM_SETICON, 0, (LPARAM)hIcon);

		HWND hListBox = GetDlgItem(hwnd, IDC_LIST);
		for (int i = 0; i < sizeof(g_VALUES) / sizeof(g_VALUES[0]); i++)
			SendMessage(hListBox, LB_ADDSTRING, 0, (LPARAM)g_VALUES[i]);
	}
		break;
	case WM_COMMAND:
		switch (LOWORD(wParam))
		{
		case IDC_BUTTON_ADD:
			DialogBox(GetModuleHandle(NULL), MAKEINTRESOURCE(IDD_DIALOG_ADD_ITEM), hwnd, DlgProcAddItem, 0);
			break;

		case IDOK: 
		{
			HWND hListBox = GetDlgItem(hwnd, IDC_LIST);
			CONST INT SIZE = 256;
			CHAR sz_buffer[SIZE]{};
			INT i = SendMessage(hListBox, LB_GETCURSEL, 0, 0);
			SendMessage(hListBox, LB_GETTEXT, i, (LPARAM)sz_buffer);
			//MessageBox(hwnd, sz_buffer, "Info", MB_OK | MB_ICONINFORMATION);

			CHAR sz_message[SIZE]{};
			sprintf(sz_message, "вы выбрали элимент №%i co значением \"%s\".", i, sz_buffer);
			MessageBox(hwnd, sz_message, "Info", MB_OK | MB_ICONINFORMATION);
					
		} break;
		case IDCANCEL: EndDialog(hwnd, 0); break;
		
		}
		break;
	case WM_CLOSE:
		EndDialog(hwnd, 0);
		break;
	}
	return false;
}

BOOL CALLBACK DlgProcAddItem(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	switch (uMsg)
	{
	case WM_INITDIALOG:
		break;
	case WM_COMMAND:
		switch (LOWORD(wParam))
		{
		case IDOK:
		{
			CONST INT SIZE = 256;
			CHAR sz_buffer[SIZE]{};
			HWND hEdit = GetDlgItem(hwnd, IDC_EDIT_ADD_ITEM);
			SendMessage(hEdit, WM_GETTEXT, SIZE, (LPARAM)sz_buffer);

			HWND hParent = GetParent(hwnd);
			HWND hListBox = GetDlgItem(hParent, IDC_LIST);

			SendMessage(hListBox, LB_ADDSTRING, 0, (LPARAM)sz_buffer);
		} 
		case IDCANCEL: EndDialog(hwnd, 0);
		}
		break;
	case WM_CLOSE: EndDialog(hwnd, 0); break;
	}
	return false;
}