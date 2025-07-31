using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NvidiaX.NVIDIA.Native
{
	// Token: 0x02000002 RID: 2
	public sealed class NativeArrayHelper
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static T GetArrayItemData<T>(IntPtr sourcePointer)
		{
			return (T)((object)Marshal.PtrToStructure(sourcePointer, typeof(T)));
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002068 File Offset: 0x00000268
		public static T[] GetArrayData<T>(IntPtr sourcePointer, int itemCount)
		{
			List<T> list = new List<T>();
			if (sourcePointer != IntPtr.Zero && itemCount > 0)
			{
				int num = Marshal.SizeOf(typeof(T));
				for (int i = 0; i < itemCount; i++)
				{
					list.Add(NativeArrayHelper.GetArrayItemData<T>(sourcePointer + num * i));
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020C4 File Offset: 0x000002C4
		public static void SetArrayData<T>(T[] items, out IntPtr targetPointer)
		{
			if (items != null && items.Length != 0)
			{
				int num = Marshal.SizeOf(typeof(T));
				targetPointer = Marshal.AllocHGlobal(num * items.Length);
				for (int i = 0; i < items.Length; i++)
				{
					Marshal.StructureToPtr<T>(items[i], targetPointer + num * i, true);
				}
				return;
			}
			targetPointer = IntPtr.Zero;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002124 File Offset: 0x00000324
		public static void SetArrayItemData<T>(T item, out IntPtr targetPointer)
		{
			int cb = Marshal.SizeOf(typeof(T));
			targetPointer = Marshal.AllocHGlobal(cb);
			Marshal.StructureToPtr<T>(item, targetPointer, true);
		}
	}
}
