using System;
using NAudio.Wave;

namespace NAudio.Utils
{
	/// <summary>
	/// WavePosition extension methods
	/// </summary>
	// Token: 0x02000069 RID: 105
	public static class WavePositionExtensions
	{
		/// <summary>
		/// Get Position as timespan
		/// </summary>
		// Token: 0x06000248 RID: 584 RVA: 0x00007724 File Offset: 0x00005924
		public static TimeSpan GetPositionTimeSpan(this IWavePosition @this)
		{
			long num = @this.GetPosition() / (long)(@this.OutputWaveFormat.Channels * @this.OutputWaveFormat.BitsPerSample / 8);
			return TimeSpan.FromMilliseconds((double)num * 1000.0 / (double)@this.OutputWaveFormat.SampleRate);
		}
	}
}
