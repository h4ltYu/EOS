using System;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Converts an IWaveProvider containing 24 bit PCM to an
	/// ISampleProvider
	/// </summary>
	// Token: 0x020001F5 RID: 501
	public class Pcm24BitToSampleProvider : SampleProviderConverterBase
	{
		/// <summary>
		/// Initialises a new instance of Pcm24BitToSampleProvider
		/// </summary>
		/// <param name="source">Source Wave Provider</param>
		// Token: 0x06000B3C RID: 2876 RVA: 0x00021707 File Offset: 0x0001F907
		public Pcm24BitToSampleProvider(IWaveProvider source) : base(source)
		{
		}

		/// <summary>
		/// Reads floating point samples from this sample provider
		/// </summary>
		/// <param name="buffer">sample buffer</param>
		/// <param name="offset">offset within sample buffer to write to</param>
		/// <param name="count">number of samples required</param>
		/// <returns>number of samples provided</returns>
		// Token: 0x06000B3D RID: 2877 RVA: 0x00021710 File Offset: 0x0001F910
		public override int Read(float[] buffer, int offset, int count)
		{
			int num = count * 3;
			base.EnsureSourceBuffer(num);
			int num2 = this.source.Read(this.sourceBuffer, 0, num);
			int num3 = offset;
			for (int i = 0; i < num2; i += 3)
			{
				buffer[num3++] = (float)((int)((sbyte)this.sourceBuffer[i + 2]) << 16 | (int)this.sourceBuffer[i + 1] << 8 | (int)this.sourceBuffer[i]) / 8388608f;
			}
			return num2 / 3;
		}
	}
}
