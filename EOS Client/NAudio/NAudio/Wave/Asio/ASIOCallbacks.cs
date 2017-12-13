using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Asio
{
	// Token: 0x0200015D RID: 349
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct ASIOCallbacks
	{
		// Token: 0x040007B5 RID: 1973
		public ASIOCallbacks.ASIOBufferSwitchCallBack pbufferSwitch;

		// Token: 0x040007B6 RID: 1974
		public ASIOCallbacks.ASIOSampleRateDidChangeCallBack psampleRateDidChange;

		// Token: 0x040007B7 RID: 1975
		public ASIOCallbacks.ASIOAsioMessageCallBack pasioMessage;

		// Token: 0x040007B8 RID: 1976
		public ASIOCallbacks.ASIOBufferSwitchTimeInfoCallBack pbufferSwitchTimeInfo;

		// Token: 0x0200015E RID: 350
		// (Invoke) Token: 0x06000776 RID: 1910
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal delegate void ASIOBufferSwitchCallBack(int doubleBufferIndex, bool directProcess);

		// Token: 0x0200015F RID: 351
		// (Invoke) Token: 0x0600077A RID: 1914
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal delegate void ASIOSampleRateDidChangeCallBack(double sRate);

		// Token: 0x02000160 RID: 352
		// (Invoke) Token: 0x0600077E RID: 1918
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal delegate int ASIOAsioMessageCallBack(ASIOMessageSelector selector, int value, IntPtr message, IntPtr opt);

		// Token: 0x02000161 RID: 353
		// (Invoke) Token: 0x06000782 RID: 1922
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal delegate IntPtr ASIOBufferSwitchTimeInfoCallBack(IntPtr asioTimeParam, int doubleBufferIndex, bool directProcess);
	}
}
