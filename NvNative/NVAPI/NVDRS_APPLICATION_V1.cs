using System;
using System.Runtime.InteropServices;

namespace NvidiaX.NVIDIA.Native.NVAPI
{
	// Token: 0x02000065 RID: 101
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 8)]
	public struct NVDRS_APPLICATION_V1
	{
		// Token: 0x040003B1 RID: 945
		public uint version;

		// Token: 0x040003B2 RID: 946
		public uint isPredefined;

		// Token: 0x040003B3 RID: 947
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
		public string appName;

		// Token: 0x040003B4 RID: 948
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
		public string userFriendlyName;

		// Token: 0x040003B5 RID: 949
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
		public string launcher;
	}
}
