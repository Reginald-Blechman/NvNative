using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NvidiaX.NVIDIA.Native.NVAPI
{
	// Token: 0x02000069 RID: 105
	public sealed class NVAPIDrsWrapper
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000022D0 File Offset: 0x000004D0
		private NVAPIDrsWrapper()
		{
		}

		// Token: 0x06000011 RID: 17
		[DllImport("kernel32.dll")]
		private static extern IntPtr LoadLibrary(string dllname);

		// Token: 0x06000012 RID: 18
		[DllImport("kernel32.dll")]
		private static extern IntPtr GetProcAddress(IntPtr hModule, string procname);

		// Token: 0x06000013 RID: 19 RVA: 0x000022D8 File Offset: 0x000004D8
		private static uint MAKE_NVAPI_VERSION<T>(int version)
		{
			return (uint)(Marshal.SizeOf(typeof(T)) | version << 16);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022EE File Offset: 0x000004EE
		private static string GetDllName()
		{
			if (IntPtr.Size == 4)
			{
				return "nvapi.dll";
			}
			return "nvapi64.dll";
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002304 File Offset: 0x00000504
		private static void GetDelegate<T>(uint id, out T newDelegate, uint? fallbackId = null) where T : class
		{
			IntPtr intPtr = NVAPIDrsWrapper.nvapi_QueryInterface(id);
			if (intPtr != IntPtr.Zero)
			{
				newDelegate = (Marshal.GetDelegateForFunctionPointer(intPtr, typeof(T)) as T);
				return;
			}
			if (fallbackId != null)
			{
				NVAPIDrsWrapper.GetDelegate<T>(fallbackId.Value, out newDelegate, null);
				return;
			}
			newDelegate = default(T);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002374 File Offset: 0x00000574
		private static T GetDelegateOfFunction<T>(IntPtr pLib, string signature)
		{
			T result = default(T);
			IntPtr procAddress = NVAPIDrsWrapper.GetProcAddress(pLib, signature);
			if (procAddress != IntPtr.Zero)
			{
				result = (T)((object)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(T)));
			}
			return result;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023B8 File Offset: 0x000005B8
		public static NVAPI_STATUS DRS_EnumApplications<TDrsAppVersion>(IntPtr hSession, IntPtr hProfile, uint startIndex, ref uint appCount, ref TDrsAppVersion[] apps)
		{
			IntPtr intPtr;
			NativeArrayHelper.SetArrayData<TDrsAppVersion>(apps, out intPtr);
			NVAPI_STATUS result;
			try
			{
				result = NVAPIDrsWrapper.DRS_EnumApplicationsInternal(hSession, hProfile, startIndex, ref appCount, intPtr);
				apps = NativeArrayHelper.GetArrayData<TDrsAppVersion>(intPtr, (int)appCount);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002404 File Offset: 0x00000604
		public static NVAPI_STATUS DRS_SetSetting(IntPtr hSession, IntPtr hProfile, ref NVDRS_SETTING pSetting)
		{
			return NVAPIDrsWrapper._DRS_SetSetting(hSession, hProfile, ref pSetting, 0U, 0U);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002418 File Offset: 0x00000618
		public static NVAPI_STATUS DRS_GetSetting(IntPtr hSession, IntPtr hProfile, uint settingId, ref NVDRS_SETTING pSetting)
		{
			uint num = 0U;
			return NVAPIDrsWrapper._DRS_GetSetting(hSession, hProfile, settingId, ref pSetting, ref num);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002438 File Offset: 0x00000638
		public static NVAPI_STATUS DRS_EnumSettings(IntPtr hSession, IntPtr hProfile, uint startIndex, ref uint settingsCount, ref NVDRS_SETTING[] settings)
		{
			IntPtr intPtr;
			NativeArrayHelper.SetArrayData<NVDRS_SETTING>(settings, out intPtr);
			NVAPI_STATUS result;
			try
			{
				result = NVAPIDrsWrapper.DRS_EnumSettingsInternal(hSession, hProfile, startIndex, ref settingsCount, intPtr);
				settings = NativeArrayHelper.GetArrayData<NVDRS_SETTING>(intPtr, (int)settingsCount);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002484 File Offset: 0x00000684
		public static NVAPI_STATUS DRS_EnumAvailableSettingIds(out List<uint> settingIds, uint maxCount)
		{
			uint[] array = new uint[maxCount];
			IntPtr zero = IntPtr.Zero;
			NativeArrayHelper.SetArrayData<uint>(array, out zero);
			NVAPI_STATUS result;
			try
			{
				result = NVAPIDrsWrapper.DRS_EnumAvailableSettingIdsInternal(zero, ref maxCount);
				array = NativeArrayHelper.GetArrayData<uint>(zero, (int)maxCount);
				settingIds = array.ToList<uint>();
			}
			finally
			{
				Marshal.FreeHGlobal(zero);
			}
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024E0 File Offset: 0x000006E0
		public static NVAPI_STATUS DRS_EnumAvailableSettingValues(uint settingId, ref uint pMaxNumValues, ref NVDRS_SETTING_VALUES settingValues)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NVDRS_SETTING_VALUES)));
			NVAPI_STATUS result;
			try
			{
				settingValues.settingValues = new NVDRS_SETTING_UNION[100];
				Marshal.StructureToPtr<NVDRS_SETTING_VALUES>(settingValues, intPtr, true);
				result = NVAPIDrsWrapper.DRS_EnumAvailableSettingValuesInternal(settingId, ref pMaxNumValues, intPtr);
				settingValues = (NVDRS_SETTING_VALUES)Marshal.PtrToStructure(intPtr, typeof(NVDRS_SETTING_VALUES));
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002560 File Offset: 0x00000760
		static NVAPIDrsWrapper()
		{
			IntPtr intPtr = NVAPIDrsWrapper.LoadLibrary(NVAPIDrsWrapper.GetDllName());
			if (intPtr != IntPtr.Zero)
			{
				NVAPIDrsWrapper.nvapi_QueryInterface = NVAPIDrsWrapper.GetDelegateOfFunction<NVAPIDrsWrapper.nvapi_QueryInterfaceDelegate>(intPtr, "nvapi_QueryInterface");
				if (NVAPIDrsWrapper.nvapi_QueryInterface != null)
				{
					NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.NvAPI_InitializeDelegate>(22079528U, out NVAPIDrsWrapper.NvAPI_Initialize, null);
					if (NVAPIDrsWrapper.NvAPI_Initialize() == NVAPI_STATUS.NVAPI_OK)
					{
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.InitializeDelegate>(22079528U, out NVAPIDrsWrapper.Initialize, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.UnloadDelegate>(3526090110U, out NVAPIDrsWrapper.Unload, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.GetErrorMessageDelegate>(1814889612U, out NVAPIDrsWrapper.GetErrorMessage, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.GetInterfaceVersionStringDelegate>(17121189U, out NVAPIDrsWrapper.GetInterfaceVersionString, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.SYS_GetDriverAndBranchVersionDelegate>(690399917U, out NVAPIDrsWrapper.SYS_GetDriverAndBranchVersion, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_CreateSessionDelegate>(110417198U, out NVAPIDrsWrapper.DRS_CreateSession, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_DestroySessionDelegate>(3671707640U, out NVAPIDrsWrapper.DRS_DestroySession, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_LoadSettingsDelegate>(928890219U, out NVAPIDrsWrapper.DRS_LoadSettings, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_SaveSettingsDelegate>(4240211476U, out NVAPIDrsWrapper.DRS_SaveSettings, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_LoadSettingsFromFileDelegate>(3555584137U, out NVAPIDrsWrapper.DRS_LoadSettingsFromFile, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_SaveSettingsToFileDelegate>(736255480U, out NVAPIDrsWrapper.DRS_SaveSettingsToFile, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_LoadSettingsFromFileExDelegate>(3325822043U, out NVAPIDrsWrapper.DRS_LoadSettingsFromFileEx, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_SaveSettingsToFileExDelegate>(308773262U, out NVAPIDrsWrapper.DRS_SaveSettingsToFileEx, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_CreateProfileDelegate>(3424084072U, out NVAPIDrsWrapper.DRS_CreateProfile, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_DeleteProfileDelegate>(386478598U, out NVAPIDrsWrapper.DRS_DeleteProfile, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_SetCurrentGlobalProfileDelegate>(478791135U, out NVAPIDrsWrapper.DRS_SetCurrentGlobalProfile, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_GetCurrentGlobalProfileDelegate>(1635516319U, out NVAPIDrsWrapper.DRS_GetCurrentGlobalProfile, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_GetProfileInfoDelegate>(1640853462U, out NVAPIDrsWrapper.DRS_GetProfileInfo, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_SetProfileInfoDelegate>(380359593U, out NVAPIDrsWrapper.DRS_SetProfileInfo, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_FindProfileByNameDelegate>(2118818315U, out NVAPIDrsWrapper.DRS_FindProfileByName, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_EnumProfilesDelegate>(3157728992U, out NVAPIDrsWrapper.DRS_EnumProfiles, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_GetNumProfilesDelegate>(497962940U, out NVAPIDrsWrapper.DRS_GetNumProfiles, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_CreateApplicationDelegate>(1128770014U, out NVAPIDrsWrapper.DRS_CreateApplication, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_DeleteApplicationExDelegate>(3320481185U, out NVAPIDrsWrapper.DRS_DeleteApplicationEx, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_DeleteApplicationDelegate>(745098182U, out NVAPIDrsWrapper.DRS_DeleteApplication, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_GetApplicationInfoDelegate>(3978267753U, out NVAPIDrsWrapper.DRS_GetApplicationInfo, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_EnumApplicationsDelegate>(2141329210U, out NVAPIDrsWrapper.DRS_EnumApplicationsInternal, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_FindApplicationByNameDelegate>(4008011442U, out NVAPIDrsWrapper.DRS_FindApplicationByName, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_SetSettingDelegate>(2318202357U, out NVAPIDrsWrapper._DRS_SetSetting, new uint?(1467863554U));
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_GetSettingDelegate>(3935914381U, out NVAPIDrsWrapper._DRS_GetSetting, new uint?(1941930808U));
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_EnumSettingsDelegate>(3486947390U, out NVAPIDrsWrapper.DRS_EnumSettingsInternal, new uint?(2922396122U));
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_EnumAvailableSettingIdsDelegate>(3856550117U, out NVAPIDrsWrapper.DRS_EnumAvailableSettingIdsInternal, new uint?(4028653898U));
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_EnumAvailableSettingValuesDelegate>(784572304U, out NVAPIDrsWrapper.DRS_EnumAvailableSettingValuesInternal, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_GetSettingIdFromNameDelegate>(3413313997U, out NVAPIDrsWrapper.DRS_GetSettingIdFromName, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_GetSettingNameFromIdDelegate>(514930577U, out NVAPIDrsWrapper.DRS_GetSettingNameFromId, new uint?(3592207982U));
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_DeleteProfileSettingDelegate>(3524078047U, out NVAPIDrsWrapper.DRS_DeleteProfileSetting, new uint?(3835847522U));
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_RestoreAllDefaultsDelegate>(1495773332U, out NVAPIDrsWrapper.DRS_RestoreAllDefaults, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_RestoreProfileDefaultDelegate>(4200554804U, out NVAPIDrsWrapper.DRS_RestoreProfileDefault, null);
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_RestoreProfileDefaultSettingDelegate>(2111156833U, out NVAPIDrsWrapper.DRS_RestoreProfileDefaultSetting, new uint?(1408251934U));
						NVAPIDrsWrapper.GetDelegate<NVAPIDrsWrapper.DRS_GetBaseProfileDelegate>(3666110112U, out NVAPIDrsWrapper.DRS_GetBaseProfile, null);
					}
				}
			}
		}

		// Token: 0x040003C9 RID: 969
		public const uint NVAPI_GENERIC_STRING_MAX = 4096U;

		// Token: 0x040003CA RID: 970
		public const uint NVAPI_LONG_STRING_MAX = 256U;

		// Token: 0x040003CB RID: 971
		public const uint NVAPI_SHORT_STRING_MAX = 64U;

		// Token: 0x040003CC RID: 972
		public const uint NVAPI_MAX_PHYSICAL_GPUS = 64U;

		// Token: 0x040003CD RID: 973
		public const uint NVAPI_UNICODE_STRING_MAX = 2048U;

		// Token: 0x040003CE RID: 974
		public const uint NVAPI_BINARY_DATA_MAX = 4096U;

		// Token: 0x040003CF RID: 975
		public const uint NVAPI_SETTING_MAX_VALUES = 100U;

		// Token: 0x040003D0 RID: 976
		public static uint NVDRS_SETTING_VALUES_VER = NVAPIDrsWrapper.MAKE_NVAPI_VERSION<NVDRS_SETTING_VALUES>(1);

		// Token: 0x040003D1 RID: 977
		public static uint NVDRS_SETTING_VER = NVAPIDrsWrapper.MAKE_NVAPI_VERSION<NVDRS_SETTING>(1);

		// Token: 0x040003D2 RID: 978
		public static uint NVDRS_APPLICATION_VER_V1 = NVAPIDrsWrapper.MAKE_NVAPI_VERSION<NVDRS_APPLICATION_V1>(1);

		// Token: 0x040003D3 RID: 979
		public static uint NVDRS_APPLICATION_VER_V2 = NVAPIDrsWrapper.MAKE_NVAPI_VERSION<NVDRS_APPLICATION_V2>(2);

		// Token: 0x040003D4 RID: 980
		public static uint NVDRS_APPLICATION_VER_V3 = NVAPIDrsWrapper.MAKE_NVAPI_VERSION<NVDRS_APPLICATION_V3>(3);

		// Token: 0x040003D5 RID: 981
		public static uint NVDRS_APPLICATION_VER = NVAPIDrsWrapper.NVDRS_APPLICATION_VER_V3;

		// Token: 0x040003D6 RID: 982
		public static uint NVDRS_PROFILE_VER = NVAPIDrsWrapper.MAKE_NVAPI_VERSION<NVDRS_PROFILE>(1);

		// Token: 0x040003D7 RID: 983
		public const uint OGL_IMPLICIT_GPU_AFFINITY_NUM_VALUES = 1U;

		// Token: 0x040003D8 RID: 984
		public const uint CUDA_EXCLUDED_GPUS_NUM_VALUES = 1U;

		// Token: 0x040003D9 RID: 985
		public const string D3DOGL_GPU_MAX_POWER_DEFAULTPOWER = "0";

		// Token: 0x040003DA RID: 986
		public const uint D3DOGL_GPU_MAX_POWER_NUM_VALUES = 1U;

		// Token: 0x040003DB RID: 987
		public const string D3DOGL_GPU_MAX_POWER_DEFAULT = "0";

		// Token: 0x040003DC RID: 988
		private static readonly NVAPIDrsWrapper.nvapi_QueryInterfaceDelegate nvapi_QueryInterface;

		// Token: 0x040003DD RID: 989
		public static readonly NVAPIDrsWrapper.NvAPI_InitializeDelegate NvAPI_Initialize;

		// Token: 0x040003DE RID: 990
		public static readonly NVAPIDrsWrapper.InitializeDelegate Initialize;

		// Token: 0x040003DF RID: 991
		public static readonly NVAPIDrsWrapper.UnloadDelegate Unload;

		// Token: 0x040003E0 RID: 992
		public static readonly NVAPIDrsWrapper.GetErrorMessageDelegate GetErrorMessage;

		// Token: 0x040003E1 RID: 993
		public static readonly NVAPIDrsWrapper.GetInterfaceVersionStringDelegate GetInterfaceVersionString;

		// Token: 0x040003E2 RID: 994
		public static readonly NVAPIDrsWrapper.SYS_GetDriverAndBranchVersionDelegate SYS_GetDriverAndBranchVersion;

		// Token: 0x040003E3 RID: 995
		public static readonly NVAPIDrsWrapper.DRS_CreateSessionDelegate DRS_CreateSession;

		// Token: 0x040003E4 RID: 996
		public static readonly NVAPIDrsWrapper.DRS_DestroySessionDelegate DRS_DestroySession;

		// Token: 0x040003E5 RID: 997
		public static readonly NVAPIDrsWrapper.DRS_LoadSettingsDelegate DRS_LoadSettings;

		// Token: 0x040003E6 RID: 998
		public static readonly NVAPIDrsWrapper.DRS_SaveSettingsDelegate DRS_SaveSettings;

		// Token: 0x040003E7 RID: 999
		public static readonly NVAPIDrsWrapper.DRS_LoadSettingsFromFileDelegate DRS_LoadSettingsFromFile;

		// Token: 0x040003E8 RID: 1000
		public static readonly NVAPIDrsWrapper.DRS_SaveSettingsToFileDelegate DRS_SaveSettingsToFile;

		// Token: 0x040003E9 RID: 1001
		public static readonly NVAPIDrsWrapper.DRS_LoadSettingsFromFileExDelegate DRS_LoadSettingsFromFileEx;

		// Token: 0x040003EA RID: 1002
		public static readonly NVAPIDrsWrapper.DRS_SaveSettingsToFileExDelegate DRS_SaveSettingsToFileEx;

		// Token: 0x040003EB RID: 1003
		public static readonly NVAPIDrsWrapper.DRS_CreateProfileDelegate DRS_CreateProfile;

		// Token: 0x040003EC RID: 1004
		public static readonly NVAPIDrsWrapper.DRS_DeleteProfileDelegate DRS_DeleteProfile;

		// Token: 0x040003ED RID: 1005
		public static readonly NVAPIDrsWrapper.DRS_SetCurrentGlobalProfileDelegate DRS_SetCurrentGlobalProfile;

		// Token: 0x040003EE RID: 1006
		public static readonly NVAPIDrsWrapper.DRS_GetCurrentGlobalProfileDelegate DRS_GetCurrentGlobalProfile;

		// Token: 0x040003EF RID: 1007
		public static readonly NVAPIDrsWrapper.DRS_GetProfileInfoDelegate DRS_GetProfileInfo;

		// Token: 0x040003F0 RID: 1008
		public static readonly NVAPIDrsWrapper.DRS_SetProfileInfoDelegate DRS_SetProfileInfo;

		// Token: 0x040003F1 RID: 1009
		public static readonly NVAPIDrsWrapper.DRS_FindProfileByNameDelegate DRS_FindProfileByName;

		// Token: 0x040003F2 RID: 1010
		public static readonly NVAPIDrsWrapper.DRS_EnumProfilesDelegate DRS_EnumProfiles;

		// Token: 0x040003F3 RID: 1011
		public static readonly NVAPIDrsWrapper.DRS_GetNumProfilesDelegate DRS_GetNumProfiles;

		// Token: 0x040003F4 RID: 1012
		public static readonly NVAPIDrsWrapper.DRS_CreateApplicationDelegate DRS_CreateApplication;

		// Token: 0x040003F5 RID: 1013
		public static readonly NVAPIDrsWrapper.DRS_DeleteApplicationExDelegate DRS_DeleteApplicationEx;

		// Token: 0x040003F6 RID: 1014
		public static readonly NVAPIDrsWrapper.DRS_DeleteApplicationDelegate DRS_DeleteApplication;

		// Token: 0x040003F7 RID: 1015
		public static readonly NVAPIDrsWrapper.DRS_GetApplicationInfoDelegate DRS_GetApplicationInfo;

		// Token: 0x040003F8 RID: 1016
		private static readonly NVAPIDrsWrapper.DRS_EnumApplicationsDelegate DRS_EnumApplicationsInternal;

		// Token: 0x040003F9 RID: 1017
		public static readonly NVAPIDrsWrapper.DRS_FindApplicationByNameDelegate DRS_FindApplicationByName;

		// Token: 0x040003FA RID: 1018
		private static readonly NVAPIDrsWrapper.DRS_SetSettingDelegate _DRS_SetSetting;

		// Token: 0x040003FB RID: 1019
		private static readonly NVAPIDrsWrapper.DRS_GetSettingDelegate _DRS_GetSetting;

		// Token: 0x040003FC RID: 1020
		private static readonly NVAPIDrsWrapper.DRS_EnumSettingsDelegate DRS_EnumSettingsInternal;

		// Token: 0x040003FD RID: 1021
		public static readonly NVAPIDrsWrapper.DRS_EnumAvailableSettingIdsDelegate DRS_EnumAvailableSettingIdsInternal;

		// Token: 0x040003FE RID: 1022
		private static readonly NVAPIDrsWrapper.DRS_EnumAvailableSettingValuesDelegate DRS_EnumAvailableSettingValuesInternal;

		// Token: 0x040003FF RID: 1023
		public static readonly NVAPIDrsWrapper.DRS_GetSettingIdFromNameDelegate DRS_GetSettingIdFromName;

		// Token: 0x04000400 RID: 1024
		public static readonly NVAPIDrsWrapper.DRS_GetSettingNameFromIdDelegate DRS_GetSettingNameFromId;

		// Token: 0x04000401 RID: 1025
		public static readonly NVAPIDrsWrapper.DRS_DeleteProfileSettingDelegate DRS_DeleteProfileSetting;

		// Token: 0x04000402 RID: 1026
		public static readonly NVAPIDrsWrapper.DRS_RestoreAllDefaultsDelegate DRS_RestoreAllDefaults;

		// Token: 0x04000403 RID: 1027
		public static readonly NVAPIDrsWrapper.DRS_RestoreProfileDefaultDelegate DRS_RestoreProfileDefault;

		// Token: 0x04000404 RID: 1028
		public static readonly NVAPIDrsWrapper.DRS_RestoreProfileDefaultSettingDelegate DRS_RestoreProfileDefaultSetting;

		// Token: 0x04000405 RID: 1029
		public static readonly NVAPIDrsWrapper.DRS_GetBaseProfileDelegate DRS_GetBaseProfile;

		// Token: 0x0200006A RID: 106
		// (Invoke) Token: 0x0600001F RID: 31
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate IntPtr nvapi_QueryInterfaceDelegate(uint id);

		// Token: 0x0200006B RID: 107
		// (Invoke) Token: 0x06000023 RID: 35
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS NvAPI_InitializeDelegate();

		// Token: 0x0200006C RID: 108
		// (Invoke) Token: 0x06000027 RID: 39
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS InitializeDelegate();

		// Token: 0x0200006D RID: 109
		// (Invoke) Token: 0x0600002B RID: 43
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS UnloadDelegate();

		// Token: 0x0200006E RID: 110
		// (Invoke) Token: 0x0600002F RID: 47
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS GetErrorMessageDelegate(NVAPI_STATUS nr, [MarshalAs(UnmanagedType.LPStr)] StringBuilder szDesc);

		// Token: 0x0200006F RID: 111
		// (Invoke) Token: 0x06000033 RID: 51
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS GetInterfaceVersionStringDelegate([MarshalAs(UnmanagedType.LPStr)] StringBuilder szDesc);

		// Token: 0x02000070 RID: 112
		// (Invoke) Token: 0x06000037 RID: 55
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS SYS_GetDriverAndBranchVersionDelegate(ref uint pDriverVersion, [MarshalAs(UnmanagedType.LPStr)] StringBuilder szBuildBranchString);

		// Token: 0x02000071 RID: 113
		// (Invoke) Token: 0x0600003B RID: 59
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_CreateSessionDelegate(ref IntPtr phSession);

		// Token: 0x02000072 RID: 114
		// (Invoke) Token: 0x0600003F RID: 63
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_DestroySessionDelegate(IntPtr hSession);

		// Token: 0x02000073 RID: 115
		// (Invoke) Token: 0x06000043 RID: 67
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_LoadSettingsDelegate(IntPtr hSession);

		// Token: 0x02000074 RID: 116
		// (Invoke) Token: 0x06000047 RID: 71
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_SaveSettingsDelegate(IntPtr hSession);

		// Token: 0x02000075 RID: 117
		// (Invoke) Token: 0x0600004B RID: 75
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_LoadSettingsFromFileDelegate(IntPtr hSession, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder fileName);

		// Token: 0x02000076 RID: 118
		// (Invoke) Token: 0x0600004F RID: 79
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_SaveSettingsToFileDelegate(IntPtr hSession, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder fileName);

		// Token: 0x02000077 RID: 119
		// (Invoke) Token: 0x06000053 RID: 83
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_LoadSettingsFromFileExDelegate(IntPtr hSession, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder fileName);

		// Token: 0x02000078 RID: 120
		// (Invoke) Token: 0x06000057 RID: 87
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_SaveSettingsToFileExDelegate(IntPtr hSession, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder fileName);

		// Token: 0x02000079 RID: 121
		// (Invoke) Token: 0x0600005B RID: 91
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_CreateProfileDelegate(IntPtr hSession, ref NVDRS_PROFILE pProfileInfo, ref IntPtr phProfile);

		// Token: 0x0200007A RID: 122
		// (Invoke) Token: 0x0600005F RID: 95
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_DeleteProfileDelegate(IntPtr hSession, IntPtr hProfile);

		// Token: 0x0200007B RID: 123
		// (Invoke) Token: 0x06000063 RID: 99
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_SetCurrentGlobalProfileDelegate(IntPtr hSession, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder wszGlobalProfileName);

		// Token: 0x0200007C RID: 124
		// (Invoke) Token: 0x06000067 RID: 103
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_GetCurrentGlobalProfileDelegate(IntPtr hSession, ref IntPtr phProfile);

		// Token: 0x0200007D RID: 125
		// (Invoke) Token: 0x0600006B RID: 107
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_GetProfileInfoDelegate(IntPtr hSession, IntPtr hProfile, ref NVDRS_PROFILE pProfileInfo);

		// Token: 0x0200007E RID: 126
		// (Invoke) Token: 0x0600006F RID: 111
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_SetProfileInfoDelegate(IntPtr hSession, IntPtr hProfile, ref NVDRS_PROFILE pProfileInfo);

		// Token: 0x0200007F RID: 127
		// (Invoke) Token: 0x06000073 RID: 115
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_FindProfileByNameDelegate(IntPtr hSession, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder profileName, ref IntPtr phProfile);

		// Token: 0x02000080 RID: 128
		// (Invoke) Token: 0x06000077 RID: 119
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_EnumProfilesDelegate(IntPtr hSession, uint index, ref IntPtr phProfile);

		// Token: 0x02000081 RID: 129
		// (Invoke) Token: 0x0600007B RID: 123
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_GetNumProfilesDelegate(IntPtr hSession, ref uint numProfiles);

		// Token: 0x02000082 RID: 130
		// (Invoke) Token: 0x0600007F RID: 127
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_CreateApplicationDelegate(IntPtr hSession, IntPtr hProfile, ref NVDRS_APPLICATION_V3 pApplication);

		// Token: 0x02000083 RID: 131
		// (Invoke) Token: 0x06000083 RID: 131
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_DeleteApplicationExDelegate(IntPtr hSession, IntPtr hProfile, ref NVDRS_APPLICATION_V3 pApp);

		// Token: 0x02000084 RID: 132
		// (Invoke) Token: 0x06000087 RID: 135
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_DeleteApplicationDelegate(IntPtr hSession, IntPtr hProfile, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder appName);

		// Token: 0x02000085 RID: 133
		// (Invoke) Token: 0x0600008B RID: 139
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_GetApplicationInfoDelegate(IntPtr hSession, IntPtr hProfile, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder appName, ref NVDRS_APPLICATION_V3 pApplication);

		// Token: 0x02000086 RID: 134
		// (Invoke) Token: 0x0600008F RID: 143
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate NVAPI_STATUS DRS_EnumApplicationsDelegate(IntPtr hSession, IntPtr hProfile, uint startIndex, ref uint appCount, IntPtr pApplication);

		// Token: 0x02000087 RID: 135
		// (Invoke) Token: 0x06000093 RID: 147
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_FindApplicationByNameDelegate(IntPtr hSession, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder appName, ref IntPtr phProfile, ref NVDRS_APPLICATION_V3 pApplication);

		// Token: 0x02000088 RID: 136
		// (Invoke) Token: 0x06000097 RID: 151
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_SetSettingDelegate(IntPtr hSession, IntPtr hProfile, ref NVDRS_SETTING pSetting, uint x, uint y);

		// Token: 0x02000089 RID: 137
		// (Invoke) Token: 0x0600009B RID: 155
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_GetSettingDelegate(IntPtr hSession, IntPtr hProfile, uint settingId, ref NVDRS_SETTING pSetting, ref uint x);

		// Token: 0x0200008A RID: 138
		// (Invoke) Token: 0x0600009F RID: 159
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate NVAPI_STATUS DRS_EnumSettingsDelegate(IntPtr hSession, IntPtr hProfile, uint startIndex, ref uint settingsCount, IntPtr pSetting);

		// Token: 0x0200008B RID: 139
		// (Invoke) Token: 0x060000A3 RID: 163
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_EnumAvailableSettingIdsDelegate(IntPtr pSettingIds, ref uint pMaxCount);

		// Token: 0x0200008C RID: 140
		// (Invoke) Token: 0x060000A7 RID: 167
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate NVAPI_STATUS DRS_EnumAvailableSettingValuesDelegate(uint settingId, ref uint pMaxNumValues, IntPtr pSettingValues);

		// Token: 0x0200008D RID: 141
		// (Invoke) Token: 0x060000AB RID: 171
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_GetSettingIdFromNameDelegate([MarshalAs(UnmanagedType.LPWStr)] StringBuilder settingName, ref uint pSettingId);

		// Token: 0x0200008E RID: 142
		// (Invoke) Token: 0x060000AF RID: 175
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_GetSettingNameFromIdDelegate(uint settingId, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pSettingName);

		// Token: 0x0200008F RID: 143
		// (Invoke) Token: 0x060000B3 RID: 179
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_DeleteProfileSettingDelegate(IntPtr hSession, IntPtr hProfile, uint settingId);

		// Token: 0x02000090 RID: 144
		// (Invoke) Token: 0x060000B7 RID: 183
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_RestoreAllDefaultsDelegate(IntPtr hSession);

		// Token: 0x02000091 RID: 145
		// (Invoke) Token: 0x060000BB RID: 187
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_RestoreProfileDefaultDelegate(IntPtr hSession, IntPtr hProfile);

		// Token: 0x02000092 RID: 146
		// (Invoke) Token: 0x060000BF RID: 191
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_RestoreProfileDefaultSettingDelegate(IntPtr hSession, IntPtr hProfile, uint settingId);

		// Token: 0x02000093 RID: 147
		// (Invoke) Token: 0x060000C3 RID: 195
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate NVAPI_STATUS DRS_GetBaseProfileDelegate(IntPtr hSession, ref IntPtr phProfile);
	}
}
