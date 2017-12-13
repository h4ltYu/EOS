using System;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Pair of floating point values, representing samples or multipliers
	/// </summary>
	// Token: 0x0200019B RID: 411
	public struct StereoSamplePair
	{
		/// <summary>
		/// Left value
		/// </summary>
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x000186F0 File Offset: 0x000168F0
		// (set) Token: 0x06000878 RID: 2168 RVA: 0x000186F8 File Offset: 0x000168F8
		public float Left { get; set; }

		/// <summary>
		/// Right value
		/// </summary>
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x00018701 File Offset: 0x00016901
		// (set) Token: 0x0600087A RID: 2170 RVA: 0x00018709 File Offset: 0x00016909
		public float Right { get; set; }
	}
}
