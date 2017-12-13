using System;
using NAudio.Wave.SampleProviders;

namespace NAudio.Wave
{
	/// <summary>
	/// Useful extension methods to make switching between WaveAndSampleProvider easier
	/// </summary>
	// Token: 0x0200007F RID: 127
	public static class WaveExtensionMethods
	{
		/// <summary>
		/// Converts a WaveProvider into a SampleProvider (only works for PCM)
		/// </summary>
		/// <param name="waveProvider">WaveProvider to convert</param>
		/// <returns></returns>
		// Token: 0x060002AC RID: 684 RVA: 0x000090A1 File Offset: 0x000072A1
		public static ISampleProvider ToSampleProvider(this IWaveProvider waveProvider)
		{
			return SampleProviderConverters.ConvertWaveProviderIntoSampleProvider(waveProvider);
		}

		/// <summary>
		/// Allows sending a SampleProvider directly to an IWavePlayer without needing to convert
		/// back to an IWaveProvider
		/// </summary>
		/// <param name="wavePlayer">The WavePlayer</param>
		/// <param name="sampleProvider"></param>
		/// <param name="convertTo16Bit"></param>
		// Token: 0x060002AD RID: 685 RVA: 0x000090AC File Offset: 0x000072AC
		public static void Init(this IWavePlayer wavePlayer, ISampleProvider sampleProvider, bool convertTo16Bit = false)
		{
			IWaveProvider waveProvider2;
			if (!convertTo16Bit)
			{
				IWaveProvider waveProvider = new SampleToWaveProvider(sampleProvider);
				waveProvider2 = waveProvider;
			}
			else
			{
				waveProvider2 = new SampleToWaveProvider16(sampleProvider);
			}
			IWaveProvider waveProvider3 = waveProvider2;
			wavePlayer.Init(waveProvider3);
		}
	}
}
