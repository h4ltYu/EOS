using System;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Helper base class for classes converting to ISampleProvider
	/// </summary>
	// Token: 0x02000075 RID: 117
	public abstract class SampleProviderConverterBase : ISampleProvider
	{
		/// <summary>
		/// Initialises a new instance of SampleProviderConverterBase
		/// </summary>
		/// <param name="source">Source Wave provider</param>
		// Token: 0x06000281 RID: 641 RVA: 0x0000852F File Offset: 0x0000672F
		public SampleProviderConverterBase(IWaveProvider source)
		{
			this.source = source;
			this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(source.WaveFormat.SampleRate, source.WaveFormat.Channels);
		}

		/// <summary>
		/// Wave format of this wave provider
		/// </summary>
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000855F File Offset: 0x0000675F
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		/// <summary>
		/// Reads samples from the source wave provider
		/// </summary>
		/// <param name="buffer">Sample buffer</param>
		/// <param name="offset">Offset into sample buffer</param>
		/// <param name="count">Number of samples required</param>
		/// <returns>Number of samples read</returns>
		// Token: 0x06000283 RID: 643
		public abstract int Read(float[] buffer, int offset, int count);

		/// <summary>
		/// Ensure the source buffer exists and is big enough
		/// </summary>
		/// <param name="sourceBytesRequired">Bytes required</param>
		// Token: 0x06000284 RID: 644 RVA: 0x00008567 File Offset: 0x00006767
		protected void EnsureSourceBuffer(int sourceBytesRequired)
		{
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, sourceBytesRequired);
		}

		/// <summary>
		/// Source Wave Provider
		/// </summary>
		// Token: 0x0400038E RID: 910
		protected IWaveProvider source;

		// Token: 0x0400038F RID: 911
		private WaveFormat waveFormat;

		/// <summary>
		/// Source buffer (to avoid constantly creating small buffers during playback)
		/// </summary>
		// Token: 0x04000390 RID: 912
		protected byte[] sourceBuffer;
	}
}
