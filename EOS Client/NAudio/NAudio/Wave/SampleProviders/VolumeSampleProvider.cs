using System;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Very simple sample provider supporting adjustable gain
	/// </summary>
	// Token: 0x020001D3 RID: 467
	public class VolumeSampleProvider : ISampleProvider
	{
		/// <summary>
		/// Initializes a new instance of VolumeSampleProvider
		/// </summary>
		/// <param name="source">Source Sample Provider</param>
		// Token: 0x06000A35 RID: 2613 RVA: 0x0001DB45 File Offset: 0x0001BD45
		public VolumeSampleProvider(ISampleProvider source)
		{
			this.source = source;
			this.volume = 1f;
		}

		/// <summary>
		/// WaveFormat
		/// </summary>
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x0001DB5F File Offset: 0x0001BD5F
		public WaveFormat WaveFormat
		{
			get
			{
				return this.source.WaveFormat;
			}
		}

		/// <summary>
		/// Reads samples from this sample provider
		/// </summary>
		/// <param name="buffer">Sample buffer</param>
		/// <param name="offset">Offset into sample buffer</param>
		/// <param name="sampleCount">Number of samples desired</param>
		/// <returns>Number of samples read</returns>
		// Token: 0x06000A37 RID: 2615 RVA: 0x0001DB6C File Offset: 0x0001BD6C
		public int Read(float[] buffer, int offset, int sampleCount)
		{
			int result = this.source.Read(buffer, offset, sampleCount);
			if (this.volume != 1f)
			{
				for (int i = 0; i < sampleCount; i++)
				{
					buffer[offset + i] *= this.volume;
				}
			}
			return result;
		}

		/// <summary>
		/// Allows adjusting the volume, 1.0f = full volume
		/// </summary>
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x0001DBBD File Offset: 0x0001BDBD
		// (set) Token: 0x06000A39 RID: 2617 RVA: 0x0001DBC5 File Offset: 0x0001BDC5
		public float Volume
		{
			get
			{
				return this.volume;
			}
			set
			{
				this.volume = value;
			}
		}

		// Token: 0x04000B43 RID: 2883
		private readonly ISampleProvider source;

		// Token: 0x04000B44 RID: 2884
		private float volume;
	}
}
