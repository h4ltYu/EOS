using System;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Utility class for converting to SampleProvider
	/// </summary>
	// Token: 0x02000077 RID: 119
	internal static class SampleProviderConverters
	{
		/// <summary>
		/// Helper function to go from IWaveProvider to a SampleProvider
		/// Must already be PCM or IEEE float
		/// </summary>
		/// <param name="waveProvider">The WaveProvider to convert</param>
		/// <returns>A sample provider</returns>
		// Token: 0x06000287 RID: 647 RVA: 0x00008604 File Offset: 0x00006804
		public static ISampleProvider ConvertWaveProviderIntoSampleProvider(IWaveProvider waveProvider)
		{
			ISampleProvider result;
			if (waveProvider.WaveFormat.Encoding == WaveFormatEncoding.Pcm)
			{
				if (waveProvider.WaveFormat.BitsPerSample == 8)
				{
					result = new Pcm8BitToSampleProvider(waveProvider);
				}
				else if (waveProvider.WaveFormat.BitsPerSample == 16)
				{
					result = new Pcm16BitToSampleProvider(waveProvider);
				}
				else if (waveProvider.WaveFormat.BitsPerSample == 24)
				{
					result = new Pcm24BitToSampleProvider(waveProvider);
				}
				else
				{
					if (waveProvider.WaveFormat.BitsPerSample != 32)
					{
						throw new InvalidOperationException("Unsupported bit depth");
					}
					result = new Pcm32BitToSampleProvider(waveProvider);
				}
			}
			else
			{
				if (waveProvider.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
				{
					throw new ArgumentException("Unsupported source encoding");
				}
				if (waveProvider.WaveFormat.BitsPerSample == 64)
				{
					result = new WaveToSampleProvider64(waveProvider);
				}
				else
				{
					result = new WaveToSampleProvider(waveProvider);
				}
			}
			return result;
		}
	}
}
