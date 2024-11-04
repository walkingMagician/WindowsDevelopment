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
	{
	}
	break;

	case WM_COMMAND:
	{
		if (LOWORD(wParam) == IDCOPY) // ���������, ���� �� ������ ������ "Copy"
		{ 
			TCHAR buffer[256]; // ����� ��� ������
			HWND hEdit1 = GetDlgItem(hwnd, IDC_EDIT1); // �������� ���������� Edit Control 1
			HWND hEdit2 = GetDlgItem(hwnd, IDC_EDIT2); // �������� ���������� Edit Control 2
			SendMessage(hEdit1, WM_GETTEXT, sizeof(buffer) / sizeof(TCHAR), (LPARAM)buffer); // �������� ����� �� Edit Control
			SendMessage(hEdit2, WM_SETTEXT, 0, (LPARAM)buffer); // ������������� ����� �� ������ Edit Control
		}

		if (LOWORD(wParam) == IDC_BUTTON3) // ������ "ADD"
		{
			HWND hListBox4 = GetDlgItem(hwnd, IDC_LIST4); // ���������� list box
			HWND hEdit3List = GetDlgItem(hwnd, IDC_EDIT3); // ���������� ������� "ADD"
			const char buffer[256]{};
			SendMessage(hEdit3List, WM_GETTEXT, sizeof(buffer) / sizeof(TCHAR), (LPARAM)buffer);

			AddStringToListBox(hListBox4, buffer);
			SetWindowText(hEdit3List, ""); // ������� ��������������� � "hEdit3List" (Edit Control)  ������ ������ 
		}

		if (LOWORD(wParam) == IDC_BUTTON4) // button "DELETE"
		{
			HWND hListBox4 = GetDlgItem(hwnd, IDC_LIST4); // ���������� list box
			SendMessage(hListBox4, LB_DELETESTRING, 0, 0); // �������� �������� �� ListBox 
		}

		switch (LOWORD(wParam))
		{
		case IDOK: MessageBox(hwnd, "���� ������ ������ ��", "Info", MB_OK | MB_ICONINFORMATION); break;
		//case IDCOPY: MessageBox(hwnd, "���� ������ ������ Copy", "Info", MB_OK | MB_ICONINFORMATION); break;
		case IDCANCEL: EndDialog(hwnd, 0); break;

		}
	}
	break;
	
	case WM_DESTROY: // ������������ ������� �������� ����
	{
		PostQuitMessage(0); // ������� ��������� �� ������� � ��������� ����������
		break;
	}

	case WM_CLOSE:
		EndDialog(hwnd, 0); break;
	}
	return FALSE;
}

