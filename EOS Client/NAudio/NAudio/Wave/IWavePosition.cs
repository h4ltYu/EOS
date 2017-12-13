using System;

namespace NAudio.Wave
{
	/// <summary>
	/// Interface for IWavePlayers that can report position
	/// </summary>
	// Token: 0x020001B9 RID: 441
	public interface IWavePosition
	{
		/// <summary>
		/// Position (in terms of bytes played - does not necessarily)
		/// </summary>
		/// <returns>Position in bytes</returns>
		// Token: 0x0600096A RID: 2410
		long GetPosition();

		/// <summary>
		/// Gets a <see cref="T:NAudio.Wave.WaveFormat" /> instance indicating the format the hardware is using.
		/// </summary>
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600096B RID: 2411
		WaveFormat OutputWaveFormat { get; }
	}
}
