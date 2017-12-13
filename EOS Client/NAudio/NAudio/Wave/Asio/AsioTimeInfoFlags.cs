using System;

namespace NAudio.Wave.Asio
{
	// Token: 0x0200015B RID: 347
	[Flags]
	internal enum AsioTimeInfoFlags
	{
		// Token: 0x040007A9 RID: 1961
		kSystemTimeValid = 1,
		// Token: 0x040007AA RID: 1962
		kSamplePositionValid = 2,
		// Token: 0x040007AB RID: 1963
		kSampleRateValid = 4,
		// Token: 0x040007AC RID: 1964
		kSpeedValid = 8,
		// Token: 0x040007AD RID: 1965
		kSampleRateChanged = 16,
		// Token: 0x040007AE RID: 1966
		kClockSourceChanged = 32
	}
}
