// WWM_Activator.h : main header file for the PROJECT_NAME application
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#ifdef POCKETPC2003_UI_MODEL
#include "resourceppc.h"
#endif 
#ifdef SMARTPHONE2003_UI_MODEL
#include "resourcesp.h"
#endif

// CWWM_ActivatorApp:
// See WWM_Activator.cpp for the implementation of this class
//

class CWWM_ActivatorApp : public CWinApp
{
public:
	CWWM_ActivatorApp();
	
// Overrides
public:
	virtual BOOL InitInstance();

// Implementation

	DECLARE_MESSAGE_MAP()
};

extern CWWM_ActivatorApp theApp;
