using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Asio
{
	// Token: 0x0200015C RID: 348
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct ASIOTime
	{
		// Token: 0x040007AF RID: 1967
		public int reserved1;

		// Token: 0x040007B0 RID: 1968
		public int reserved2;

		// Token: 0x040007B1 RID: 1969
		public int reserved3;

		// Token: 0x040007B2 RID: 1970
		public int reserved4;

		// Token: 0x040007B3 RID: 1971
		public AsioTimeInfo timeInfo;

		// Token: 0x040007B4 RID: 1972
		public ASIOTimeCode timeCode;
	}
}
