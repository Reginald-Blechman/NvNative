using System;
using System.Runtime.InteropServices;

namespace NvidiaX.NVIDIA.Native.NVAPI
{
	// Token: 0x02000068 RID: 104
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 8)]
	public struct NVDRS_PROFILE
	{
		// Token: 0x040003C3 RID: 963
		public uint version;

		// Token: 0x040003C4 RID: 964
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
		public string profileName;

		// Token: 0x040003C5 RID: 965
		public NVDRS_GPU_SUPPORT gpuSupport;

		// Token: 0x040003C6 RID: 966
		public uint isPredefined;

		// Token: 0x040003C7 RID: 967
		public uint numOfApps;

		// Token: 0x040003C8 RID: 968
		public uint numOfSettings;
	}
}
