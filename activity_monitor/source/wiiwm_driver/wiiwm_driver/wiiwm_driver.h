#include "stdafx.h"

#define DLL_EXPORT __declspec(dllexport)

extern "C"
{
BOOL DLL_EXPORT WWM_Close(DWORD hOpenContext);
DWORD DLL_EXPORT WWM_Open(DWORD hDeviceContext, DWORD AccessCode, DWORD ShareMode);
DWORD DLL_EXPORT WWM_Init(LPCTSTR pContext, DWORD dwBusContext);
DWORD DLL_EXPORT WWM_Write(DWORD hOpenContext, LPCVOID pBuffer, DWORD Count);
DWORD DLL_EXPORT WWM_Seek(DWORD hOpenContext, long Amount, WORD Type);
DWORD DLL_EXPORT WWM_Read(DWORD hOpenContext, LPVOID pBuffer, DWORD Count);
BOOL DLL_EXPORT WWM_Deinit(DWORD hDeviceContext);
BOOL DLL_EXPORT WWM_IOControl(DWORD hOpenContext, DWORD dwCode, PBYTE pBufIn, DWORD dwLenIn, PBYTE pBufOut, DWORD dwLenOut, PDWORD pdwActualOut);
void DLL_EXPORT WWM_PowerDown(DWORD hDeviceContext);
void DLL_EXPORT WWM_PowerUp(DWORD hDeviceContext);
}
