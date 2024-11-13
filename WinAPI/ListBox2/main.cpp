#define _CRT_SECURE_NO_WARNINGS
#include <windows.h>
#include <cstdio>
#include <commctrl.h>
#include <string.h>
#include "resource.h"


CONST CHAR* g_VALUES[] = { "This", "is", "my", "first", "List", "Box"};

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
	} break;

	case WM_KEYDOWN:
	{
		switch (wParam)
		{
			
		case VK_DELETE:
			SendMessage(GetDlgItem(hwnd, IDC_BUTTON_ADD), BM_CLICK, 0, 0); // лик по кнопке
			break;

		}break;

	} break;

	case WM_LBUTTONDBLCLK:
	{
		HWND hListBox = GetDlgItem(hwnd, IDC_LIST);
		INT I = SendMessage(hListBox, LB_GETCURSEL, 0, 0);
		CHAR sz_buffer[256];
		SendMessage(hListBox, LB_GETTEXT, I, (LPARAM)sz_buffer);
		MessageBox(hwnd, sz_buffer, "info", MB_OK);

	} break;
	

	case WM_COMMAND:
		switch (LOWORD(wParam))
		{
		case IDC_BUTTON_ADD:
			DialogBox(GetModuleHandle(NULL), MAKEINTRESOURCE(IDD_DIALOG_ADD_ITEM), hwnd, DlgProcAddItem, 0);
			break;

		case IDC_BUTTON_REMUVE:
		{
			HWND hListBox = GetDlgItem(hwnd, IDC_LIST);
			INT i = SendMessage(hListBox, LB_GETCURSEL, 0, 0);
			SendMessage(hListBox, LB_DELETESTRING, i, 0);
		} break;

		case IDOK: 
		{
			HWND hListBox = GetDlgItem(hwnd, IDC_LIST); // дескриптор нашего основного окна
			CONST INT SIZE = 256;
			CHAR sz_buffer[SIZE]{}; // массив под значение  
			INT i = SendMessage(hListBox, LB_GETCURSEL, 0, 0); // выдел€ем значение мышкой 
			SendMessage(hListBox, LB_GETTEXT, i, (LPARAM)sz_buffer);

			CHAR sz_message[SIZE]{}; // массив под вывод сообщени€ 
			sprintf(sz_message, "вы выбрали элимент є%i co значением \"%s\".", i, sz_buffer);
			MessageBox(hwnd, sz_message, "Info", MB_OK | MB_ICONINFORMATION);
					
		} break;


		case IDCANCEL: EndDialog(hwnd, 0); break;
		
		} break;

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
		SetFocus(GetDlgItem(hwnd, IDC_EDIT_ADD_ITEM));
			break;
	case WM_COMMAND:
		switch (LOWORD(wParam))
		{
		case IDOK:
		{
			CONST INT SIZE = 256;
			CHAR OS_buffer[SIZE]{};
			HWND hEdit = GetDlgItem(hwnd, IDC_EDIT_ADD_ITEM); // дескриптор edit control
			SendMessage(hEdit, WM_GETTEXT, SIZE, (LPARAM)OS_buffer); // получение текста 1
			HWND hParent = GetParent(hwnd); // получение родительского окна
			HWND hListBox = GetDlgItem(hParent, IDC_LIST); // дескриптор бокса

			int count = SendMessage(hListBox, LB_GETCOUNT, 0, 0); // получение кол-во элиментов 
			BOOL flag = true; // флаг

			for (int i = 0; i < count; i++)
			{
				CHAR sz_buffer[SIZE]{};
				SendMessage(hListBox, LB_GETTEXT, i, (LPARAM)sz_buffer); // получение элимента из бокса
				if (strcmp(sz_buffer,OS_buffer) == 0)
				{
					flag = false;
					break;
				}
			}	
			if (flag && strlen(OS_buffer) && strlen(OS_buffer + 1)) // проверка на флаг, длину\пустоту
				SendMessage(hListBox, LB_ADDSTRING, 0, (LPARAM)OS_buffer); // добавление текста
			else
				MessageBox(hwnd, "Ёлемент уже существует или €вл€етс€ пустым", "Error", MB_OK | MB_ICONERROR); // ошибка

		} 
		case IDCANCEL: EndDialog(hwnd, 0); break;
		} break;

	case WM_CLOSE: EndDialog(hwnd, 0); break;
	}
	return false;
}