// WWM_ActivatorDlg.h : header file
//

#pragma once

// CWWM_ActivatorDlg dialog
class CWWM_ActivatorDlg : public CDialog
{
// Construction
public:
	CWWM_ActivatorDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_WWM_ACTIVATOR_DIALOG };

#ifdef WIN32_PLATFORM_WFSP
protected:  // control bar embedded members
	CCommandBar m_dlgCommandBar;
#endif // WIN32_PLATFORM_WFSP

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
	afx_msg void OnSize(UINT /*nType*/, int /*cx*/, int /*cy*/);
#endif
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedButton1();
	afx_msg void OnBnClickedButton2();
	afx_msg void OnBnClickedButton3();
	afx_msg void OnBnClickedButton4();
};
