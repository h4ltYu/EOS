using System;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Converts an IWaveProvider containing 16 bit PCM to an
	/// ISampleProvider
	/// </summary>
	// Token: 0x020001F4 RID: 500
	public class Pcm16BitToSampleProvider : SampleProviderConverterBase
	{
		/// <summary>
		/// Initialises a new instance of Pcm16BitToSampleProvider
		/// </summary>
		/// <param name="source">Source wave provider</param>
		// Token: 0x06000B3A RID: 2874 RVA: 0x000216A7 File Offset: 0x0001F8A7
		public Pcm16BitToSampleProvider(IWaveProvider source) : base(source)
		{
		}

		/// <summary>
		/// Reads samples from this sample provider
		/// </summary>
		/// <param name="buffer">Sample buffer</param>
		/// <param name="offset">Offset into sample buffer</param>
		/// <param name="count">Samples required</param>
		/// <returns>Number of samples read</returns>
		// Token: 0x06000B3B RID: 2875 RVA: 0x000216B0 File Offset: 0x0001F8B0
		public override int Read(float[] buffer, int offset, int count)
		{
			int num = count * 2;
			base.EnsureSourceBuffer(num);
			int num2 = this.source.Read(this.sourceBuffer, 0, num);
			int num3 = offset;
			for (int i = 0; i < num2; i += 2)
			{
				buffer[num3++] = (float)BitConverter.ToInt16(this.sourceBuffer, i) / 32768f;
			}
			return num2 / 2;
		}
	}
}
