using System;

namespace NAudio.Wave
{
	/// <summary>
	/// Like IWaveProvider, but makes it much simpler to put together a 32 bit floating
	/// point mixing engine
	/// </summary>
	// Token: 0x0200006F RID: 111
	public interface ISampleProvider
	{
		/// <summary>
		/// Gets the WaveFormat of this Sample Provider.
		/// </summary>
		/// <value>The wave format.</value>
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000254 RID: 596
		WaveFormat WaveFormat { get; }

		/// <summary>
		/// Fill the specified buffer with 32 bit floating point samples
		/// </summary>
		/// <param name="buffer">The buffer to fill with samples.</param>
		/// <param name="offset">Offset into buffer</param>
		/// <param name="count">The number of samples to read</param>
		/// <returns>the number of samples written to the buffer.</returns>
		// Token: 0x06000255 RID: 597
		int Read(float[] buffer, int offset, int count);
	}
}
