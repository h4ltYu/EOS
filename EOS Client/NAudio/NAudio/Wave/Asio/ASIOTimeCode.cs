using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Asio
{
	// Token: 0x02000157 RID: 343
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct ASIOTimeCode
	{
		// Token: 0x04000795 RID: 1941
		public double speed;

		// Token: 0x04000796 RID: 1942
		public ASIO64Bit timeCodeSamples;

		// Token: 0x04000797 RID: 1943
		public ASIOTimeCodeFlags flags;

		// Token: 0x04000798 RID: 1944
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string future;
	}
}
