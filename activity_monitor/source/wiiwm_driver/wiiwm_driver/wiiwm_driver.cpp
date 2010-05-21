// WWMwm_driver.cpp : Defines the entry point for the DLL application.
//

#include "stdafx.h"

#include "wiiwm_driver.h"

char stuff[512];
int start=0, end=0, len=0;

HANDLE com;
wchar_t com_id[6];

BOOL DLL_EXPORT WWM_Close(DWORD hOpenContext)
{
	DWORD written;
	char pBuffer[22] = {255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255};
	bool success = WriteFile(com, pBuffer, 22, &written, NULL);
	CloseHandle(com);
	return true;
}

DWORD DLL_EXPORT WWM_Open(DWORD hDeviceContext, DWORD AccessCode, DWORD ShareMode)
{
    if ( hDeviceContext != 1024 ) return 0;

	com = CreateFile(com_id, GENERIC_READ | GENERIC_WRITE, 0, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);

	if ( !com ) return 0;

	return 2048;
}


/*
 * pContext is the path to the registry key of the active device. To see what this looks like
 * try: MessageBox(NULL, pContext, "Registry Key", MB_OK);
 * 
 * dwBusContext is the data passed into the ActivateDeviceEx function. For this driver this
 * will be the number 0->9 of the com port that should be used.
 *
 * Return if the device is already open the value will be 0 else it will be 1024.
 */
DWORD DLL_EXPORT WWM_Init(LPCTSTR pContext, DWORD dwBusContext)
{

	wcscpy(com_id, L"COM*:");
	com_id[3] = (dwBusContext & 0xFF) + L'0';
	
    return 1024;
}

DWORD DLL_EXPORT WWM_Write(DWORD hOpenContext, LPCVOID pBuffer, DWORD Count)
{
	DWORD written;
	bool success = WriteFile(com, pBuffer, Count, &written, NULL);
	if ( success )
		return written;

	else
		return -1;


	/*MessageBox(NULL, L"Inside DLL", L"Really?", MB_OK);

    if ( hOpenContext == 34 && 512-len > Count)
    {
        for (int i = 0; i < Count; i++)
        {
            stuff[end++] = ((char*)pBuffer)[i];
            if ( end == 512 ) end = 0;
            len++;
        }
        return Count;
    }
    else
        return -1;*/
}

DWORD DLL_EXPORT WWM_Seek(DWORD hOpenContext, long Amount, WORD Type)
{
    return 0;
}

DWORD DLL_EXPORT WWM_Read(DWORD hOpenContext, LPVOID pBuffer, DWORD Count)
{

	DWORD written;
	bool success = ReadFile(com, pBuffer, Count, &written, NULL);
	if ( success )
		return written;

	else
		return -1;



    /*if ( hOpenContext == 34 && len >= Count)
    {
        for ( int i =0; i < Count; i++)
        {
            ((char*)pBuffer)[i] = stuff[start++];
            if ( start == 512 ) start = 0;
            len--;
        }
        return Count;
    }
    else
        return -1;*/
}


BOOL DLL_EXPORT WWM_Deinit(DWORD hDeviceContext)
{
    return true;
}

BOOL DLL_EXPORT WWM_IOControl(DWORD hOpenContext, DWORD dwCode, PBYTE pBufIn, DWORD dwLenIn, PBYTE pBufOut, DWORD dwLenOut, PDWORD pdwActualOut)
{
    return false;
}

void DLL_EXPORT WWM_PowerDown(DWORD hDeviceContext)
{

}

void DLL_EXPORT WWM_PowerUp(DWORD hDeviceContext)
{

}



BOOL APIENTRY DllMain( HANDLE hModule, 
                       DWORD  ul_reason_for_call, 
                       LPVOID lpReserved
					 )
{
    return TRUE;
}

