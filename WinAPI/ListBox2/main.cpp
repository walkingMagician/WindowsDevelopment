#define _CRT_SECURE_NO_WARNINGS
#include <windows.h>
#include <cstdio>
#include <commctrl.h>
#include <string.h>
#include "resource.h"


CONST CHAR* g_VALUES[] = { "This", "is", "my", "first", "List", "Box"};

BOOL CALLBACK DlgProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
BOOL CALLBACK DlgProcAddItem(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
BOOL CALLBACK DlgProcAlterItem(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

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
	
	case WM_COMMAND:
		switch (LOWORD(wParam))
		{
		case IDC_LIST: // сообщение от листа
		{
			if (HIWORD(wParam) == LBN_DBLCLK) // двойной клик
				DialogBoxParam(GetModuleHandle(NULL), MAKEINTRESOURCE(IDD_DIALOG_ADD_ITEM), hwnd, DlgProcAlterItem, 0);

		} break;
		
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
			INT i = SendMessage(hListBox, LB_GETCURSEL, 0, 0); // выделяем значение мышкой 
			SendMessage(hListBox, LB_GETTEXT, i, (LPARAM)sz_buffer);

			CHAR sz_message[SIZE]{}; // массив под вывод сообщения 
			sprintf(sz_message, "вы выбрали элимент №%i co значением \"%s\".", i, sz_buffer);
			MessageBox(hwnd, sz_message, "Info", MB_OK | MB_ICONINFORMATION);
					
		} break;



		case IDCANCEL: EndDialog(hwnd, 0); break;
		
		} break;

	case WM_VKEYTOITEM:
	{
		switch (LOWORD(wParam))
		{
		case VK_DELETE: SendMessage(hwnd, WM_COMMAND, LOWORD(IDC_BUTTON_REMUVE), 0); break;
		case VK_RETURN: SendMessage(hwnd, WM_COMMAND, MAKEWPARAM(IDC_LIST, LBN_DBLCLK), (LPARAM)GetDlgItem(hwnd, IDC_LIST)); break;
		} break;
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
	{
		SetFocus(GetDlgItem(hwnd, IDC_EDIT_ADD_ITEM));
	case IDC_LIST:
	{
		HWND hParent = GetParent(hwnd); // получение родительского окна
		HWND hListBox = GetDlgItem(hParent, IDC_LIST);
		INT I = SendMessage(hListBox, LB_GETCURSEL, 0, 0);
		CHAR sz_buffer[256]{};
		SendMessage(hListBox, LB_GETTEXT, I, (LPARAM)sz_buffer);
		SetDlgItemText(hwnd, IDC_EDIT_ADD_ITEM, sz_buffer);
		
	} break;

	} break;
	
	case WM_COMMAND:
		switch (LOWORD(wParam))
		{		
		case IDOK:
		{


			CONST INT SIZE = 256;
			CHAR OS_buffer[SIZE]{};
			HWND hEdit = GetDlgItem(hwnd, IDC_EDIT_ADD_ITEM); // дескриптор edit control
			SendMessage(hEdit, WM_GETTEXT, SIZE, (LPARAM)OS_buffer); // получение вводимого текста

			HWND hParent = GetParent(hwnd); // получение родительского окна
			HWND hListBox = GetDlgItem(hParent, IDC_LIST); // дескриптор бокса

			if (SendMessage(hListBox, LB_FINDSTRINGEXACT, -1, (LPARAM)OS_buffer) == LB_ERR)
				SendMessage(hListBox, LB_ADDSTRING, 0, (LPARAM)OS_buffer);
			else
			{
				INT answer = MessageBox(hwnd, "такое уже есть уверены что хотите добавить?", "info", MB_YESNO | MB_ICONQUESTION);
				if (answer == IDYES)break;
			}
			/*
			INT I = SendMessage(hListBox, LB_GETCURSEL, 0, 0); // получение индекса при выборе мышки 

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
			if (flag && strlen(OS_buffer)) // проверка на флаг, длину/пустоту
			{
				if (I >= 0) SendMessage(hListBox, LB_DELETESTRING, I, 0); // проверка на то брали из листа или нет 
				SendMessage(hListBox, LB_ADDSTRING, 0, (LPARAM)OS_buffer); // добавление текста
			}
			else
				MessageBox(hwnd, "Элемент уже существует или является пустым", "Error", MB_OK | MB_ICONERROR); // ошибка
			*/

		} 
		case IDCANCEL: EndDialog(hwnd, 0); break;
		} break;

	case WM_CLOSE: EndDialog(hwnd, 0); break;
	}
	return false;
}

BOOL CALLBACK DlgProcAlterItem(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	switch (uMsg)
	{
	case WM_INITDIALOG:
	{
		SendMessage(hwnd, WM_SETTEXT, 0, (LPARAM)"изменить вхождение");
		HWND hEdit = GetDlgItem(hwnd, IDC_EDIT_ADD_ITEM);
		HWND hParent = GetParent(hwnd);
		HWND hListBox = GetDlgItem(hParent, IDC_LIST);
		INT i = SendMessage(hListBox, LB_GETCURSEL, 0, 0);
		CONST INT SIZE = 256;
		CHAR sz_buffer[SIZE]{};
		SendMessage(hListBox, LB_GETTEXT, i, (LPARAM)sz_buffer);
		SendMessage(hEdit, WM_SETTEXT, 0, (LPARAM)sz_buffer);
		SetFocus(hEdit);
		INT length = SendMessage(hEdit, WM_GETTEXTLENGTH, 0, 0);
		SendMessage(hEdit, EM_SETSEL, length, length); // выделяем весь текст 
		//SendMessage(hEdit, EM_REPLACESEL, 0, length);

	} break;
	case WM_COMMAND:
	{
		switch (LOWORD(wParam))
		{
		case IDOK:
		{
			HWND hEdit = GetDlgItem(hwnd, IDC_EDIT_ADD_ITEM);
			HWND hParent = GetParent(hwnd);
			HWND hListBox = GetDlgItem(hParent, IDC_LIST);
			CONST INT SIZE = 256;
			CHAR sz_buffer[256]{};
			SendMessage(hEdit, WM_GETTEXT, SIZE, (LPARAM)sz_buffer);
			INT i = SendMessage(hListBox, LB_GETCURSEL, 0, 0);
			SendMessage(hListBox, LB_DELETESTRING, i, 0);
			SendMessage(hListBox, LB_INSERTSTRING, i, (LPARAM)sz_buffer);
		}
		case IDCANCEL: EndDialog(hwnd, 0); break;
		} break;

	} break;
	case WM_CLOSE: EndDialog(hwnd, 0); break;
	}
	return FALSE;
}