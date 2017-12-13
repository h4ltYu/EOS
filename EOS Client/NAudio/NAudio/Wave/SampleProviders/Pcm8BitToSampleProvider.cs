using System;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Converts an IWaveProvider containing 8 bit PCM to an
	/// ISampleProvider
	/// </summary>
	// Token: 0x020001F6 RID: 502
	public class Pcm8BitToSampleProvider : SampleProviderConverterBase
	{
		/// <summary>
		/// Initialises a new instance of Pcm8BitToSampleProvider
		/// </summary>
		/// <param name="source">Source wave provider</param>
		// Token: 0x06000B3E RID: 2878 RVA: 0x0002177F File Offset: 0x0001F97F
		public Pcm8BitToSampleProvider(IWaveProvider source) : base(source)
		{
		}

		/// <summary>
		/// Reads samples from this sample provider
		/// </summary>
		/// <param name="buffer">Sample buffer</param>
		/// <param name="offset">Offset into sample buffer</param>
		/// <param name="count">Number of samples to read</param>
		/// <returns>Number of samples read</returns>
		// Token: 0x06000B3F RID: 2879 RVA: 0x00021788 File Offset: 0x0001F988
		public override int Read(float[] buffer, int offset, int count)
		{
			base.EnsureSourceBuffer(count);
			int num = this.source.Read(this.sourceBuffer, 0, count);
			int num2 = offset;
			for (int i = 0; i < num; i++)
			{
				buffer[num2++] = (float)this.sourceBuffer[i] / 128f - 1f;
			}
			return num;
		}
	}
}
