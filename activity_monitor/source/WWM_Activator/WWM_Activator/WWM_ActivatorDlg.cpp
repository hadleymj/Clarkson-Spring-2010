// WWM_ActivatorDlg.cpp : implementation file
//

#include "stdafx.h"
#include "WWM_Activator.h"
#include "WWM_ActivatorDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

// CWWM_ActivatorDlg dialog

CWWM_ActivatorDlg::CWWM_ActivatorDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CWWM_ActivatorDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CWWM_ActivatorDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CWWM_ActivatorDlg, CDialog)
#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
	ON_WM_SIZE()
#endif
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(IDC_BUTTON1, &CWWM_ActivatorDlg::OnBnClickedButton1)
	ON_BN_CLICKED(IDC_BUTTON2, &CWWM_ActivatorDlg::OnBnClickedButton2)
	ON_BN_CLICKED(IDC_BUTTON3, &CWWM_ActivatorDlg::OnBnClickedButton3)
	ON_BN_CLICKED(IDC_BUTTON4, &CWWM_ActivatorDlg::OnBnClickedButton4)
END_MESSAGE_MAP()


// CWWM_ActivatorDlg message handlers

BOOL CWWM_ActivatorDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

#ifdef WIN32_PLATFORM_WFSP
	if (!m_dlgCommandBar.Create(this) ||
	    !m_dlgCommandBar.InsertMenuBar(IDR_MAINFRAME))
	{
		TRACE0("Failed to create CommandBar\n");
		return FALSE;      // fail to create
	}
#endif // WIN32_PLATFORM_WFSP
	// TODO: Add extra initialization here
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
void CWWM_ActivatorDlg::OnSize(UINT /*nType*/, int /*cx*/, int /*cy*/)
{
	if (AfxIsDRAEnabled())
	{
		DRA::RelayoutDialog(
			AfxGetResourceHandle(), 
			this->m_hWnd, 
			DRA::GetDisplayMode() != DRA::Portrait ? 
			MAKEINTRESOURCE(IDD_WWM_ACTIVATOR_DIALOG_WIDE) : 
			MAKEINTRESOURCE(IDD_WWM_ACTIVATOR_DIALOG));
	}
}
#endif


void CWWM_ActivatorDlg::OnBnClickedButton1()
{
	
	ActivateDeviceEx(L"Drivers\\BuiltIn\\wiitest", NULL, 0, (LPVOID)0);
	int e = GetLastError();

	if ( e == 2404 )
		this->MessageBox(L"Driver was already activated.");

	else if ( e == 0 )
		this->MessageBox(L"Driver Activated!");

	else
		this->MessageBox(L"Error Activating Driver");

}

void CWWM_ActivatorDlg::OnBnClickedButton2()
{

	//Need a file handle to get the ActivateDeviceEx handle.
	HANDLE h = CreateFile(L"WWM0:", GENERIC_READ | GENERIC_WRITE, 0, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
	int error = GetLastError();

	DEVMGR_DEVICE_INFORMATION DeviceInfo;
    DeviceInfo.dwSize = sizeof( DEVMGR_DEVICE_INFORMATION );

	//Get ActivateDeviceEx handle using the file handle.
	GetDeviceInformationByFileHandle(h, &DeviceInfo);

	CloseHandle(h);

	//Deactivate the device.
	bool success = DeactivateDevice(DeviceInfo.hDevice);
	
	if ( success )
		this->MessageBox(L"Driver Deactivated");

	else
		this->MessageBox(L"Unable to Deactivate Device");

}

void CWWM_ActivatorDlg::OnBnClickedButton3()
{
	
	HANDLE h = CreateFile(L"WWM0:", GENERIC_READ | GENERIC_WRITE, 0, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
	int error = GetLastError();

	DWORD written;
	char rbuff[] = "Hello World!";
	bool success = WriteFile(h, rbuff, 12, &written, NULL);
	
	if ( !success )
		this->MessageBox(L"Unable to Write");


	byte inbuff[7] = {0, 0, 0, 0, 0, 0, 0 };
	success = ReadFile(h, inbuff, 6, &written, NULL);
	
	if ( !success )
		this->MessageBox(L"Unable to read 6 bytes.");

	CloseHandle(h);

}

void CWWM_ActivatorDlg::OnBnClickedButton4()
{
	
	// TODO: Add your control notification handler code here
	HANDLE h = CreateFile(L"WWM0:", GENERIC_READ | GENERIC_WRITE, 0, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
	int error = GetLastError();
	
	DWORD written;

	BOOL success;
	do
	{
		byte inbuff[22];
		success = ReadFile(h, inbuff, 22, &written, NULL);
	}while(written == 22);
	
	if (success && (written == 22))
	{
		this->MessageBox(L"Flush Successful, Read 22 Bytes, FTW!");
	}

	if(success && (written != 22))
	{
		this->MessageBox(L"Flush Successful, Failed to Read all 22 bytes");
	}

	if ( !success )
		this->MessageBox(L"Failed to Flush COM Port, die");

	CloseHandle(h);

}

