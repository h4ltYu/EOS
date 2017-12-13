using System;
using NAudio.Wave;

namespace NAudio.Utils
{
    public static class WavePositionExtensions
    {
        public static TimeSpan GetPositionTimeSpan(this IWavePosition @this)
        {
            long num = @this.GetPosition() / (long)(@this.OutputWaveFormat.Channels * @this.OutputWaveFormat.BitsPerSample / 8);
            return TimeSpan.FromMilliseconds((double)num * 1000.0 / (double)@this.OutputWaveFormat.SampleRate);
        }
    }
}
