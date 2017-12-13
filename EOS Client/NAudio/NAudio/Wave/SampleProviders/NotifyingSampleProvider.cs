using System;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Simple class that raises an event on every sample
	/// </summary>
	// Token: 0x020001D2 RID: 466
	public class NotifyingSampleProvider : ISampleProvider, ISampleNotifier
	{
		/// <summary>
		/// Initializes a new instance of NotifyingSampleProvider
		/// </summary>
		/// <param name="source">Source Sample Provider</param>
		// Token: 0x06000A30 RID: 2608 RVA: 0x0001DA13 File Offset: 0x0001BC13
		public NotifyingSampleProvider(ISampleProvider source)
		{
			this.source = source;
			this.channels = this.WaveFormat.Channels;
		}

		/// <summary>
		/// WaveFormat
		/// </summary>
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x0001DA48 File Offset: 0x0001BC48
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
		// Token: 0x06000A32 RID: 2610 RVA: 0x0001DA58 File Offset: 0x0001BC58
		public int Read(float[] buffer, int offset, int sampleCount)
		{
			int num = this.source.Read(buffer, offset, sampleCount);
			if (this.Sample != null)
			{
				for (int i = 0; i < num; i += this.channels)
				{
					this.sampleArgs.Left = buffer[offset + i];
					this.sampleArgs.Right = ((this.channels > 1) ? buffer[offset + i + 1] : this.sampleArgs.Left);
					this.Sample(this, this.sampleArgs);
				}
			}
			return num;
		}

		/// <summary>
		/// Sample notifier
		/// </summary>
		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000A33 RID: 2611 RVA: 0x0001DAD8 File Offset: 0x0001BCD8
		// (remove) Token: 0x06000A34 RID: 2612 RVA: 0x0001DB10 File Offset: 0x0001BD10
		public event EventHandler<SampleEventArgs> Sample;

		// Token: 0x04000B3F RID: 2879
		private ISampleProvider source;

		// Token: 0x04000B40 RID: 2880
		private SampleEventArgs sampleArgs = new SampleEventArgs(0f, 0f);

		// Token: 0x04000B41 RID: 2881
		private int channels;
	}
}
