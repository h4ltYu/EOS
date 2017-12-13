using System;

namespace NAudio.Wave
{
	// Token: 0x020001F1 RID: 497
	internal class Mp3Index
	{
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x00020EED File Offset: 0x0001F0ED
		// (set) Token: 0x06000B19 RID: 2841 RVA: 0x00020EF5 File Offset: 0x0001F0F5
		public long FilePosition { get; set; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x00020EFE File Offset: 0x0001F0FE
		// (set) Token: 0x06000B1B RID: 2843 RVA: 0x00020F06 File Offset: 0x0001F106
		public long SamplePosition { get; set; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x00020F0F File Offset: 0x0001F10F
		// (set) Token: 0x06000B1D RID: 2845 RVA: 0x00020F17 File Offset: 0x0001F117
		public int SampleCount { get; set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x00020F20 File Offset: 0x0001F120
		// (set) Token: 0x06000B1F RID: 2847 RVA: 0x00020F28 File Offset: 0x0001F128
		public int ByteCount { get; set; }
	}
}
