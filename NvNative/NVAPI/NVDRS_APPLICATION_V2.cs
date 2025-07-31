using System;
using System.Runtime.InteropServices;

namespace NvidiaX.NVIDIA.Native.NVAPI
{
	// Token: 0x02000066 RID: 102
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 8)]
	public struct NVDRS_APPLICATION_V2
	{
		// Token: 0x040003B6 RID: 950
		public uint version;

		// Token: 0x040003B7 RID: 951
		public uint isPredefined;

		// Token: 0x040003B8 RID: 952
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
		public string appName;

		// Token: 0x040003B9 RID: 953
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
		public string userFriendlyName;

		// Token: 0x040003BA RID: 954
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
		public string launcher;

		// Token: 0x040003BB RID: 955
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
		public string fileInFolder;
	}
}
