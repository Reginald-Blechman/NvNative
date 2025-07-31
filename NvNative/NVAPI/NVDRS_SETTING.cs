using System;
using System.Runtime.InteropServices;

namespace NvidiaX.NVIDIA.Native.NVAPI
{
	// Token: 0x02000064 RID: 100
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 8)]
	public struct NVDRS_SETTING
	{
		// Token: 0x040003A8 RID: 936
		public uint version;

		// Token: 0x040003A9 RID: 937
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
		public string settingName;

		// Token: 0x040003AA RID: 938
		public uint settingId;

		// Token: 0x040003AB RID: 939
		public NVDRS_SETTING_TYPE settingType;

		// Token: 0x040003AC RID: 940
		public NVDRS_SETTING_LOCATION settingLocation;

		// Token: 0x040003AD RID: 941
		public uint isCurrentPredefined;

		// Token: 0x040003AE RID: 942
		public uint isPredefinedValid;

		// Token: 0x040003AF RID: 943
		public NVDRS_SETTING_UNION predefinedValue;

		// Token: 0x040003B0 RID: 944
		public NVDRS_SETTING_UNION currentValue;
	}
}
