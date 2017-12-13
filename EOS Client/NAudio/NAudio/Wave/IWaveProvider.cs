using System;

namespace NAudio.Wave
{
	/// <summary>
	/// Generic interface for all WaveProviders.
	/// </summary>
	// Token: 0x02000078 RID: 120
	public interface IWaveProvider
	{
		/// <summary>
		/// Gets the WaveFormat of this WaveProvider.
		/// </summary>
		/// <value>The wave format.</value>
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000288 RID: 648
		WaveFormat WaveFormat { get; }

		/// <summary>
		/// Fill the specified buffer with wave data.
		/// </summary>
		/// <param name="buffer">The buffer to fill of wave data.</param>
		/// <param name="offset">Offset into buffer</param>
		/// <param name="count">The number of bytes to read</param>
		/// <returns>the number of bytes written to the buffer.</returns>
		// Token: 0x06000289 RID: 649
		int Read(byte[] buffer, int offset, int count);
	}
}
