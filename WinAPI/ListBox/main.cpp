#define _CRT_SECURE_NO_WARNINGS
#include <Windows.h>
#include <cstdio>
#include "resource.h"
#pragma comment(lib,"user32")




BOOL CALLBACK DlgProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

void AddStringToListBox(HWND hwndListBox, LPCSTR text) {
	SendMessage(hwndListBox, LB_ADDSTRING, 0, (LPARAM)text);
}


INT WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInst, LPSTR lpCmdLine, INT nCmdShow)
{

	DialogBox(hInstance, MAKEINTRESOURCE(IDD_DIALOG1), NULL, DlgProc, 0);

	return 0;
}

BOOL CALLBACK DlgProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	
	switch (uMsg)
	{
	case WM_INITDIALOG:
	{}
	break;

	case WM_COMMAND:
	{
		switch (LOWORD(wParam))
		{
		case IDOK: MessageBox(hwnd, "Была нажата кнопка ОК", "Info", MB_OK | MB_ICONINFORMATION); break;
		//case IDCOPY: MessageBox(hwnd, "Была нажата кнопка Copy", "Info", MB_OK | MB_ICONINFORMATION); break;
		case IDCANCEL: EndDialog(hwnd, 0); break;
		
		case IDCOPY: // броверяем, была ли нажата кнопка "Copy"
		{
			
			TCHAR buffer[256]; // буфер для текста
			HWND hEdit1 = GetDlgItem(hwnd, IDC_EDIT1); // получаем дескриптор Edit Control 1
			HWND hEdit2 = GetDlgItem(hwnd, IDC_EDIT2); // получаем дескриптор Edit Control 2
			SendMessage(hEdit1, WM_GETTEXT, sizeof(buffer) / sizeof(TCHAR), (LPARAM)buffer); // получаем текст из Edit Control
			SendMessage(hEdit2, WM_SETTEXT, 0, (LPARAM)buffer); // устанавливаем текст во второй Edit Control
			break;
		}

		case IDC_BUTTON3:  // кнопка "ADD"
		{

			HWND hListBox4 = GetDlgItem(hwnd, IDC_LIST4); // дескриптор list box
			HWND hEdit3List = GetDlgItem(hwnd, IDC_EDIT3); // дескриптор кнопкни "ADD"
			const char buffer[256]{};
			SendMessage(hEdit3List, WM_GETTEXT, sizeof(buffer) / sizeof(TCHAR), (LPARAM)buffer);

			AddStringToListBox(hListBox4, buffer);
			SetWindowText(hEdit3List, ""); // команда устанавливающая в "hEdit3List" (Edit Control)  пустую строку 
			break;
		}

		case IDC_BUTTON4: // button "DELETE"
		{

			HWND hListBox4 = GetDlgItem(hwnd, IDC_LIST4); // дескриптор list box
			SendMessage(hListBox4, LB_DELETESTRING, 0, 0); // удаление элимента из ListBox 
			break;
		}
		}
	}
	break;
	
	case WM_DESTROY: // обрабатываем событие закрытия окна
	{
		PostQuitMessage(0); // удаляем сообщение из очереди и завершаем приложение
		break;
	}

	case WM_CLOSE:
		EndDialog(hwnd, 0); break;
	}
	return FALSE;
}

