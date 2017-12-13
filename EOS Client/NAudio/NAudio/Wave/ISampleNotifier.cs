using System;

namespace NAudio.Wave
{
	/// <summary>
	/// An interface for WaveStreams which can report notification of individual samples
	/// </summary>
	// Token: 0x020001D1 RID: 465
	public interface ISampleNotifier
	{
		/// <summary>
		/// A sample has been detected
		/// </summary>
		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000A2E RID: 2606
		// (remove) Token: 0x06000A2F RID: 2607
		event EventHandler<SampleEventArgs> Sample;
	}
}
