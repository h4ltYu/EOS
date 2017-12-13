using System;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Simple SampleProvider that passes through audio unchanged and raises
	/// an event every n samples with the maximum sample value from the period
	/// for metering purposes
	/// </summary>
	// Token: 0x020001CF RID: 463
	public class MeteringSampleProvider : ISampleProvider
	{
		/// <summary>
		/// Number of Samples per notification
		/// </summary>
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x0001D83C File Offset: 0x0001BA3C
		// (set) Token: 0x06000A24 RID: 2596 RVA: 0x0001D844 File Offset: 0x0001BA44
		public int SamplesPerNotification { get; set; }

		/// <summary>
		/// Raised periodically to inform the user of the max volume
		/// </summary>
		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000A25 RID: 2597 RVA: 0x0001D850 File Offset: 0x0001BA50
		// (remove) Token: 0x06000A26 RID: 2598 RVA: 0x0001D888 File Offset: 0x0001BA88
		public event EventHandler<StreamVolumeEventArgs> StreamVolume;

		/// <summary>
		/// Initialises a new instance of MeteringSampleProvider that raises 10 stream volume
		/// events per second
		/// </summary>
		/// <param name="source">Source sample provider</param>
		// Token: 0x06000A27 RID: 2599 RVA: 0x0001D8BD File Offset: 0x0001BABD
		public MeteringSampleProvider(ISampleProvider source) : this(source, source.WaveFormat.SampleRate / 10)
		{
		}

		/// <summary>
		/// Initialises a new instance of MeteringSampleProvider 
		/// </summary>
		/// <param name="source">source sampler provider</param>
		/// <param name="samplesPerNotification">Number of samples between notifications</param>
		// Token: 0x06000A28 RID: 2600 RVA: 0x0001D8D4 File Offset: 0x0001BAD4
		public MeteringSampleProvider(ISampleProvider source, int samplesPerNotification)
		{
			this.source = source;
			this.channels = source.WaveFormat.Channels;
			this.maxSamples = new float[this.channels];
			this.SamplesPerNotification = samplesPerNotification;
			this.args = new StreamVolumeEventArgs
			{
				MaxSampleValues = this.maxSamples
			};
		}

		/// <summary>
		/// The WaveFormat of this sample provider
		/// </summary>
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x0001D930 File Offset: 0x0001BB30
		public WaveFormat WaveFormat
		{
			get
			{
				return this.source.WaveFormat;
			}
		}

		/// <summary>
		/// Reads samples from this Sample Provider
		/// </summary>
		/// <param name="buffer">Sample buffer</param>
		/// <param name="offset">Offset into sample buffer</param>
		/// <param name="count">Number of samples required</param>
		/// <returns>Number of samples read</returns>
		// Token: 0x06000A2A RID: 2602 RVA: 0x0001D940 File Offset: 0x0001BB40
		public int Read(float[] buffer, int offset, int count)
		{
			int num = this.source.Read(buffer, offset, count);
			if (this.StreamVolume != null)
			{
				for (int i = 0; i < num; i += this.channels)
				{
					for (int j = 0; j < this.channels; j++)
					{
						float val = Math.Abs(buffer[offset + i + j]);
						this.maxSamples[j] = Math.Max(this.maxSamples[j], val);
					}
					this.sampleCount++;
					if (this.sampleCount >= this.SamplesPerNotification)
					{
						this.StreamVolume(this, this.args);
						this.sampleCount = 0;
						Array.Clear(this.maxSamples, 0, this.channels);
					}
				}
			}
			return num;
		}

		// Token: 0x04000B37 RID: 2871
		private readonly ISampleProvider source;

		// Token: 0x04000B38 RID: 2872
		private readonly float[] maxSamples;

		// Token: 0x04000B39 RID: 2873
		private int sampleCount;

		// Token: 0x04000B3A RID: 2874
		private readonly int channels;

		// Token: 0x04000B3B RID: 2875
		private readonly StreamVolumeEventArgs args;
	}
}
