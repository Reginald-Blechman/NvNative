﻿using System;

namespace NvidiaX.NVIDIA.Native.NVAPI
{
	// Token: 0x02000033 RID: 51
	public enum EValues_SHIM_RENDERING_OPTIONS : uint
	{
		// Token: 0x040001E5 RID: 485
		SHIM_RENDERING_OPTIONS_DEFAULT_RENDERING_MODE,
		// Token: 0x040001E6 RID: 486
		SHIM_RENDERING_OPTIONS_DISABLE_ASYNC_PRESENT,
		// Token: 0x040001E7 RID: 487
		SHIM_RENDERING_OPTIONS_EHSHELL_DETECT,
		// Token: 0x040001E8 RID: 488
		SHIM_RENDERING_OPTIONS_FLASHPLAYER_HOST_DETECT = 4U,
		// Token: 0x040001E9 RID: 489
		SHIM_RENDERING_OPTIONS_VIDEO_DRM_APP_DETECT = 8U,
		// Token: 0x040001EA RID: 490
		SHIM_RENDERING_OPTIONS_IGNORE_OVERRIDES = 16U,
		// Token: 0x040001EB RID: 491
		SHIM_RENDERING_OPTIONS_RESERVED1 = 32U,
		// Token: 0x040001EC RID: 492
		SHIM_RENDERING_OPTIONS_ENABLE_DWM_ASYNC_PRESENT = 64U,
		// Token: 0x040001ED RID: 493
		SHIM_RENDERING_OPTIONS_RESERVED2 = 128U,
		// Token: 0x040001EE RID: 494
		SHIM_RENDERING_OPTIONS_ALLOW_INHERITANCE = 256U,
		// Token: 0x040001EF RID: 495
		SHIM_RENDERING_OPTIONS_DISABLE_WRAPPERS = 512U,
		// Token: 0x040001F0 RID: 496
		SHIM_RENDERING_OPTIONS_DISABLE_DXGI_WRAPPERS = 1024U,
		// Token: 0x040001F1 RID: 497
		SHIM_RENDERING_OPTIONS_PRUNE_UNSUPPORTED_FORMATS = 2048U,
		// Token: 0x040001F2 RID: 498
		SHIM_RENDERING_OPTIONS_ENABLE_ALPHA_FORMAT = 4096U,
		// Token: 0x040001F3 RID: 499
		SHIM_RENDERING_OPTIONS_IGPU_TRANSCODING = 8192U,
		// Token: 0x040001F4 RID: 500
		SHIM_RENDERING_OPTIONS_DISABLE_CUDA = 16384U,
		// Token: 0x040001F5 RID: 501
		SHIM_RENDERING_OPTIONS_ALLOW_CP_CAPS_FOR_VIDEO = 32768U,
		// Token: 0x040001F6 RID: 502
		SHIM_RENDERING_OPTIONS_IGPU_TRANSCODING_FWD_OPTIMUS = 65536U,
		// Token: 0x040001F7 RID: 503
		SHIM_RENDERING_OPTIONS_DISABLE_DURING_SECURE_BOOT = 131072U,
		// Token: 0x040001F8 RID: 504
		SHIM_RENDERING_OPTIONS_INVERT_FOR_QUADRO = 262144U,
		// Token: 0x040001F9 RID: 505
		SHIM_RENDERING_OPTIONS_INVERT_FOR_MSHYBRID = 524288U,
		// Token: 0x040001FA RID: 506
		SHIM_RENDERING_OPTIONS_REGISTER_PROCESS_ENABLE_GOLD = 1048576U,
		// Token: 0x040001FB RID: 507
		SHIM_RENDERING_OPTIONS_HANDLE_WINDOWED_MODE_PERF_OPT = 2097152U,
		// Token: 0x040001FC RID: 508
		SHIM_RENDERING_OPTIONS_HANDLE_WIN7_ASYNC_RUNTIME_BUG = 4194304U,
		// Token: 0x040001FD RID: 509
		SHIM_RENDERING_OPTIONS_EXPLICIT_ADAPTER_OPTED_BY_APP = 8388608U,
		// Token: 0x040001FE RID: 510
		SHIM_RENDERING_OPTIONS_ALLOW_DYNAMIC_DISPLAY_MUX_SWITCH = 16777216U,
		// Token: 0x040001FF RID: 511
		SHIM_RENDERING_OPTIONS_DISALLOW_DYNAMIC_DISPLAY_MUX_SWITCH = 33554432U,
		// Token: 0x04000200 RID: 512
		SHIM_RENDERING_OPTIONS_DISABLE_TURING_POWER_POLICY = 67108864U,
		// Token: 0x04000201 RID: 513
		SHIM_RENDERING_OPTIONS_NUM_VALUES = 28U,
		// Token: 0x04000202 RID: 514
		SHIM_RENDERING_OPTIONS_DEFAULT = 0U
	}
}
