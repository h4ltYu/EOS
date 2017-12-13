using System;
using NAudio.Wave.SampleProviders;

namespace NAudio.Wave
{
    public static class WaveExtensionMethods
    {
        public static ISampleProvider ToSampleProvider(this IWaveProvider waveProvider)
        {
            return SampleProviderConverters.ConvertWaveProviderIntoSampleProvider(waveProvider);
        }

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
