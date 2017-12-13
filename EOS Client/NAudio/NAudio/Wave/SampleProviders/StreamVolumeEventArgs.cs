using System;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Event args for aggregated stream volume
	/// </summary>
	// Token: 0x020001D0 RID: 464
	public class StreamVolumeEventArgs : EventArgs
	{
		/// <summary>
		/// Max sample values array (one for each channel)
		/// </summary>
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x0001D9FA File Offset: 0x0001BBFA
		// (set) Token: 0x06000A2C RID: 2604 RVA: 0x0001DA02 File Offset: 0x0001BC02
		public float[] MaxSampleValues { get; set; }
	}
}
