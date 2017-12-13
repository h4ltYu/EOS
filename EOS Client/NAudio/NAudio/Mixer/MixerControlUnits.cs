using System;

namespace NAudio.Mixer
{
	// Token: 0x02000100 RID: 256
	[Flags]
	internal enum MixerControlUnits
	{
		// Token: 0x04000623 RID: 1571
		Custom = 0,
		// Token: 0x04000624 RID: 1572
		Boolean = 65536,
		// Token: 0x04000625 RID: 1573
		Signed = 131072,
		// Token: 0x04000626 RID: 1574
		Unsigned = 196608,
		// Token: 0x04000627 RID: 1575
		Decibels = 262144,
		// Token: 0x04000628 RID: 1576
		Percent = 327680,
		// Token: 0x04000629 RID: 1577
		Mask = 16711680
	}
}
