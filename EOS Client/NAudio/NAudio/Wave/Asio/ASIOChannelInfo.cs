using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Asio
{
	// Token: 0x02000154 RID: 340
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct ASIOChannelInfo
	{
		// Token: 0x0400077B RID: 1915
		public int channel;

		// Token: 0x0400077C RID: 1916
		public bool isInput;

		// Token: 0x0400077D RID: 1917
		public bool isActive;

		// Token: 0x0400077E RID: 1918
		public int channelGroup;

		// Token: 0x0400077F RID: 1919
		[MarshalAs(UnmanagedType.U4)]
		public AsioSampleType type;

		// Token: 0x04000780 RID: 1920
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string name;
	}
}
