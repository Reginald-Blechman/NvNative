using System;
using System.Runtime.InteropServices;

namespace NvidiaX.NVIDIA.Native.NVAPI
{
	// Token: 0x02000062 RID: 98
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct NVDRS_SETTING_VALUES
	{
		// Token: 0x040003A2 RID: 930
		public uint version;

		// Token: 0x040003A3 RID: 931
		public uint numSettingValues;

		// Token: 0x040003A4 RID: 932
		public NVDRS_SETTING_TYPE settingType;

		// Token: 0x040003A5 RID: 933
		public NVDRS_SETTING_UNION defaultValue;

		// Token: 0x040003A6 RID: 934
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
		public NVDRS_SETTING_UNION[] settingValues;
	}
}
