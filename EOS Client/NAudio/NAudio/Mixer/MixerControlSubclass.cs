using System;

namespace NAudio.Mixer
{
	// Token: 0x020000FF RID: 255
	[Flags]
	internal enum MixerControlSubclass
	{
		// Token: 0x0400061A RID: 1562
		SwitchBoolean = 0,
		// Token: 0x0400061B RID: 1563
		SwitchButton = 16777216,
		// Token: 0x0400061C RID: 1564
		MeterPolled = 0,
		// Token: 0x0400061D RID: 1565
		TimeMicrosecs = 0,
		// Token: 0x0400061E RID: 1566
		TimeMillisecs = 16777216,
		// Token: 0x0400061F RID: 1567
		ListSingle = 0,
		// Token: 0x04000620 RID: 1568
		ListMultiple = 16777216,
		// Token: 0x04000621 RID: 1569
		Mask = 251658240
	}
}
