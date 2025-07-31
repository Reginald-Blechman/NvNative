using System;
using System.Runtime.InteropServices;

namespace NvidiaX.NVIDIA.Native.NVAPI
{
	// Token: 0x02000067 RID: 103
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 8)]
	public struct NVDRS_APPLICATION_V3
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000022B6 File Offset: 0x000004B6
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000022C0 File Offset: 0x000004C0
		public uint isMetro
		{
			get
			{
				return this.bitvector1 & 1U;
			}
			set
			{
				this.bitvector1 = (value | this.bitvector1);
			}
		}

		// Token: 0x040003BC RID: 956
		public uint version;

		// Token: 0x040003BD RID: 957
		public uint isPredefined;

		// Token: 0x040003BE RID: 958
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
		public string appName;

		// Token: 0x040003BF RID: 959
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
		public string userFriendlyName;

		// Token: 0x040003C0 RID: 960
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
		public string launcher;

		// Token: 0x040003C1 RID: 961
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
		public string fileInFolder;

		// Token: 0x040003C2 RID: 962
		private uint bitvector1;
	}
}
