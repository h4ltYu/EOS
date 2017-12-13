using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Asio
{
	// Token: 0x02000158 RID: 344
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct ASIO64Bit
	{
		// Token: 0x04000799 RID: 1945
		public uint hi;

		// Token: 0x0400079A RID: 1946
		public uint lo;
	}
}
