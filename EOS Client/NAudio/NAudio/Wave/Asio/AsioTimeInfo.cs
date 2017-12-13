using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Asio
{
	// Token: 0x0200015A RID: 346
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct AsioTimeInfo
	{
		// Token: 0x040007A2 RID: 1954
		public double speed;

		// Token: 0x040007A3 RID: 1955
		public ASIO64Bit systemTime;

		// Token: 0x040007A4 RID: 1956
		public ASIO64Bit samplePosition;

		// Token: 0x040007A5 RID: 1957
		public double sampleRate;

		// Token: 0x040007A6 RID: 1958
		public AsioTimeInfoFlags flags;

		// Token: 0x040007A7 RID: 1959
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
		public string reserved;
	}
}
