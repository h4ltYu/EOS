using System;

namespace NAudio.Wave.Asio
{
	// Token: 0x02000159 RID: 345
	[Flags]
	internal enum ASIOTimeCodeFlags
	{
		// Token: 0x0400079C RID: 1948
		kTcValid = 1,
		// Token: 0x0400079D RID: 1949
		kTcRunning = 2,
		// Token: 0x0400079E RID: 1950
		kTcReverse = 4,
		// Token: 0x0400079F RID: 1951
		kTcOnspeed = 8,
		// Token: 0x040007A0 RID: 1952
		kTcStill = 16,
		// Token: 0x040007A1 RID: 1953
		kTcSpeedValid = 256
	}
}
