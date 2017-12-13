using System;

namespace NAudio.Mixer
{
	// Token: 0x020000FE RID: 254
	[Flags]
	internal enum MixerControlClass
	{
		// Token: 0x04000610 RID: 1552
		Custom = 0,
		// Token: 0x04000611 RID: 1553
		Meter = 268435456,
		// Token: 0x04000612 RID: 1554
		Switch = 536870912,
		// Token: 0x04000613 RID: 1555
		Number = 805306368,
		// Token: 0x04000614 RID: 1556
		Slider = 1073741824,
		// Token: 0x04000615 RID: 1557
		Fader = 1342177280,
		// Token: 0x04000616 RID: 1558
		Time = 1610612736,
		// Token: 0x04000617 RID: 1559
		List = 1879048192,
		// Token: 0x04000618 RID: 1560
		Mask = 1879048192
	}
}
