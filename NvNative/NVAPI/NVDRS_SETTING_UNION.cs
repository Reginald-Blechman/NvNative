using System;
using System.Runtime.InteropServices;
using System.Text;

namespace NvidiaX.NVIDIA.Native.NVAPI
{
	// Token: 0x02000063 RID: 99
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 8, Size = 4100)]
	public struct NVDRS_SETTING_UNION
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000215C File Offset: 0x0000035C
		// (set) Token: 0x06000007 RID: 7 RVA: 0x0000218D File Offset: 0x0000038D
		public byte[] binaryValue
		{
			get
			{
				uint num = BitConverter.ToUInt32(this.rawData, 0);
				byte[] array = new byte[num];
				Buffer.BlockCopy(this.rawData, 4, array, 0, (int)num);
				return array;
			}
			set
			{
				this.rawData = new byte[4100];
				if (value != null)
				{
					Buffer.BlockCopy(BitConverter.GetBytes(value.Length), 0, this.rawData, 0, 4);
					Buffer.BlockCopy(value, 0, this.rawData, 4, value.Length);
				}
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000021C9 File Offset: 0x000003C9
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000021D7 File Offset: 0x000003D7
		public uint dwordValue
		{
			get
			{
				return BitConverter.ToUInt32(this.rawData, 0);
			}
			set
			{
				this.rawData = new byte[4100];
				Buffer.BlockCopy(BitConverter.GetBytes(value), 0, this.rawData, 0, 4);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021FD File Offset: 0x000003FD
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002220 File Offset: 0x00000420
		public string stringValue
		{
			get
			{
				return Encoding.Unicode.GetString(this.rawData).Split(new char[1], 2)[0];
			}
			set
			{
				this.rawData = new byte[4100];
				byte[] bytes = Encoding.Unicode.GetBytes(value);
				Buffer.BlockCopy(bytes, 0, this.rawData, 0, bytes.Length);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000225A File Offset: 0x0000045A
		// (set) Token: 0x0600000D RID: 13 RVA: 0x0000227C File Offset: 0x0000047C
		public string ansiStringValue
		{
			get
			{
				return Encoding.Default.GetString(this.rawData).Split(new char[1], 2)[0];
			}
			set
			{
				this.rawData = new byte[4100];
				byte[] bytes = Encoding.Default.GetBytes(value);
				Buffer.BlockCopy(bytes, 0, this.rawData, 0, bytes.Length);
			}
		}

		// Token: 0x040003A7 RID: 935
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4100)]
		public byte[] rawData;
	}
}
