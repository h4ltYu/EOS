using System;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Converts an IWaveProvider containing 32 bit PCM to an
	/// ISampleProvider
	/// </summary>
	// Token: 0x02000076 RID: 118
	public class Pcm32BitToSampleProvider : SampleProviderConverterBase
	{
		/// <summary>
		/// Initialises a new instance of Pcm32BitToSampleProvider
		/// </summary>
		/// <param name="source">Source Wave Provider</param>
		// Token: 0x06000285 RID: 645 RVA: 0x0000857B File Offset: 0x0000677B
		public Pcm32BitToSampleProvider(IWaveProvider source) : base(source)
		{
		}

		/// <summary>
		/// Reads floating point samples from this sample provider
		/// </summary>
		/// <param name="buffer">sample buffer</param>
		/// <param name="offset">offset within sample buffer to write to</param>
		/// <param name="count">number of samples required</param>
		/// <returns>number of samples provided</returns>
		// Token: 0x06000286 RID: 646 RVA: 0x00008584 File Offset: 0x00006784
		public override int Read(float[] buffer, int offset, int count)
		{
			int num = count * 4;
			base.EnsureSourceBuffer(num);
			int num2 = this.source.Read(this.sourceBuffer, 0, num);
			int num3 = offset;
			for (int i = 0; i < num2; i += 4)
			{
				buffer[num3++] = (float)((int)((sbyte)this.sourceBuffer[i + 3]) << 24 | (int)this.sourceBuffer[i + 2] << 16 | (int)this.sourceBuffer[i + 1] << 8 | (int)this.sourceBuffer[i]) / 2.14748365E+09f;
			}
			return num2 / 4;
		}
	}
}
