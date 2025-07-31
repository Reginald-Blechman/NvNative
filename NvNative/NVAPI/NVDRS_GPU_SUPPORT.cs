using System;

namespace NvidiaX.NVIDIA.Native.NVAPI
{
	// Token: 0x02000061 RID: 97
	[Flags]
	public enum NVDRS_GPU_SUPPORT : uint
	{
		// Token: 0x0400039E RID: 926
		None = 0U,
		// Token: 0x0400039F RID: 927
		Geforce = 1U,
		// Token: 0x040003A0 RID: 928
		Quadro = 2U,
		// Token: 0x040003A1 RID: 929
		Nvs = 3U
	}
}
