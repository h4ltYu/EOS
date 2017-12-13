using System;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Allows you to:
	/// 1. insert a pre-delay of silence before the source begins
	/// 2. skip over a certain amount of the beginning of the source
	/// 3. only play a set amount from the source
	/// 4. insert silence at the end after the source is complete
	/// </summary>
	// Token: 0x02000074 RID: 116
	public class OffsetSampleProvider : ISampleProvider
	{
		// Token: 0x0600026C RID: 620 RVA: 0x00008160 File Offset: 0x00006360
		private int TimeSpanToSamples(TimeSpan time)
		{
			return (int)(time.TotalSeconds * (double)this.WaveFormat.SampleRate) * this.WaveFormat.Channels;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00008190 File Offset: 0x00006390
		private TimeSpan SamplesToTimeSpan(int samples)
		{
			return TimeSpan.FromSeconds((double)(samples / this.WaveFormat.Channels) / (double)this.WaveFormat.SampleRate);
		}

		/// <summary>
		/// Number of samples of silence to insert before playing source
		/// </summary>
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600026E RID: 622 RVA: 0x000081B2 File Offset: 0x000063B2
		// (set) Token: 0x0600026F RID: 623 RVA: 0x000081BA File Offset: 0x000063BA
		public int DelayBySamples
		{
			get
			{
				return this.delayBySamples;
			}
			set
			{
				if (this.phase != 0)
				{
					throw new InvalidOperationException("Can't set DelayBySamples after calling Read");
				}
				if (value % this.WaveFormat.Channels != 0)
				{
					throw new ArgumentException("DelayBySamples must be a multiple of WaveFormat.Channels");
				}
				this.delayBySamples = value;
			}
		}

		/// <summary>
		/// Amount of silence to insert before playing
		/// </summary>
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000270 RID: 624 RVA: 0x000081F0 File Offset: 0x000063F0
		// (set) Token: 0x06000271 RID: 625 RVA: 0x000081FE File Offset: 0x000063FE
		public TimeSpan DelayBy
		{
			get
			{
				return this.SamplesToTimeSpan(this.delayBySamples);
			}
			set
			{
				this.delayBySamples = this.TimeSpanToSamples(value);
			}
		}

		/// <summary>
		/// Number of samples in source to discard
		/// </summary>
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000820D File Offset: 0x0000640D
		// (set) Token: 0x06000273 RID: 627 RVA: 0x00008215 File Offset: 0x00006415
		public int SkipOverSamples
		{
			get
			{
				return this.skipOverSamples;
			}
			set
			{
				if (this.phase != 0)
				{
					throw new InvalidOperationException("Can't set SkipOverSamples after calling Read");
				}
				if (value % this.WaveFormat.Channels != 0)
				{
					throw new ArgumentException("SkipOverSamples must be a multiple of WaveFormat.Channels");
				}
				this.skipOverSamples = value;
			}
		}

		/// <summary>
		/// Amount of audio to skip over from the source before beginning playback
		/// </summary>
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000824B File Offset: 0x0000644B
		// (set) Token: 0x06000275 RID: 629 RVA: 0x00008259 File Offset: 0x00006459
		public TimeSpan SkipOver
		{
			get
			{
				return this.SamplesToTimeSpan(this.skipOverSamples);
			}
			set
			{
				this.skipOverSamples = this.TimeSpanToSamples(value);
			}
		}

		/// <summary>
		/// Number of samples to read from source (if 0, then read it all)
		/// </summary>
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00008268 File Offset: 0x00006468
		// (set) Token: 0x06000277 RID: 631 RVA: 0x00008270 File Offset: 0x00006470
		public int TakeSamples
		{
			get
			{
				return this.takeSamples;
			}
			set
			{
				if (this.phase != 0)
				{
					throw new InvalidOperationException("Can't set TakeSamples after calling Read");
				}
				if (value % this.WaveFormat.Channels != 0)
				{
					throw new ArgumentException("TakeSamples must be a multiple of WaveFormat.Channels");
				}
				this.takeSamples = value;
			}
		}

		/// <summary>
		/// Amount of audio to take from the source (TimeSpan.Zero means play to end)
		/// </summary>
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000278 RID: 632 RVA: 0x000082A6 File Offset: 0x000064A6
		// (set) Token: 0x06000279 RID: 633 RVA: 0x000082B4 File Offset: 0x000064B4
		public TimeSpan Take
		{
			get
			{
				return this.SamplesToTimeSpan(this.takeSamples);
			}
			set
			{
				this.takeSamples = this.TimeSpanToSamples(value);
			}
		}

		/// <summary>
		/// Number of samples of silence to insert after playing source
		/// </summary>
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600027A RID: 634 RVA: 0x000082C3 File Offset: 0x000064C3
		// (set) Token: 0x0600027B RID: 635 RVA: 0x000082CB File Offset: 0x000064CB
		public int LeadOutSamples
		{
			get
			{
				return this.leadOutSamples;
			}
			set
			{
				if (this.phase != 0)
				{
					throw new InvalidOperationException("Can't set LeadOutSamples after calling Read");
				}
				if (value % this.WaveFormat.Channels != 0)
				{
					throw new ArgumentException("LeadOutSamples must be a multiple of WaveFormat.Channels");
				}
				this.leadOutSamples = value;
			}
		}

		/// <summary>
		/// Amount of silence to insert after playing source
		/// </summary>
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00008301 File Offset: 0x00006501
		// (set) Token: 0x0600027D RID: 637 RVA: 0x0000830F File Offset: 0x0000650F
		public TimeSpan LeadOut
		{
			get
			{
				return this.SamplesToTimeSpan(this.leadOutSamples);
			}
			set
			{
				this.leadOutSamples = this.TimeSpanToSamples(value);
			}
		}

		/// <summary>
		/// Creates a new instance of offsetSampleProvider
		/// </summary>
		/// <param name="sourceProvider">The Source Sample Provider to read from</param>
		// Token: 0x0600027E RID: 638 RVA: 0x0000831E File Offset: 0x0000651E
		public OffsetSampleProvider(ISampleProvider sourceProvider)
		{
			this.sourceProvider = sourceProvider;
		}

		/// <summary>
		/// The WaveFormat of this SampleProvider
		/// </summary>
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000832D File Offset: 0x0000652D
		public WaveFormat WaveFormat
		{
			get
			{
				return this.sourceProvider.WaveFormat;
			}
		}

		/// <summary>
		/// Reads from this sample provider
		/// </summary>
		/// <param name="buffer">Sample buffer</param>
		/// <param name="offset">Offset within sample buffer to read to</param>
		/// <param name="count">Number of samples required</param>
		/// <returns>Number of samples read</returns>
		// Token: 0x06000280 RID: 640 RVA: 0x0000833C File Offset: 0x0000653C
		public int Read(float[] buffer, int offset, int count)
		{
			int num = 0;
			if (this.phase == 0)
			{
				this.phase++;
			}
			if (this.phase == 1)
			{
				int num2 = Math.Min(count, this.DelayBySamples - this.phasePos);
				for (int i = 0; i < num2; i++)
				{
					buffer[offset + i] = 0f;
				}
				this.phasePos += num2;
				num += num2;
				if (this.phasePos >= this.DelayBySamples)
				{
					this.phase++;
					this.phasePos = 0;
				}
			}
			if (this.phase == 2)
			{
				if (this.SkipOverSamples > 0)
				{
					float[] array = new float[this.WaveFormat.SampleRate * this.WaveFormat.Channels];
					int num3;
					for (int j = 0; j < this.SkipOverSamples; j += num3)
					{
						int count2 = Math.Min(this.SkipOverSamples - j, array.Length);
						num3 = this.sourceProvider.Read(array, 0, count2);
						if (num3 == 0)
						{
							break;
						}
					}
				}
				this.phase++;
				this.phasePos = 0;
			}
			if (this.phase == 3)
			{
				int num4 = count - num;
				if (this.TakeSamples != 0)
				{
					num4 = Math.Min(num4, this.TakeSamples - this.phasePos);
				}
				int num5 = this.sourceProvider.Read(buffer, offset + num, num4);
				this.phasePos += num5;
				num += num5;
				if (num5 < num4)
				{
					this.phase++;
					this.phasePos = 0;
				}
			}
			if (this.phase == 4)
			{
				int num6 = Math.Min(count - num, this.LeadOutSamples - this.phasePos);
				for (int k = 0; k < num6; k++)
				{
					buffer[offset + num + k] = 0f;
				}
				this.phasePos += num6;
				num += num6;
				if (this.phasePos >= this.LeadOutSamples)
				{
					this.phase++;
					this.phasePos = 0;
				}
			}
			return num;
		}

		// Token: 0x04000387 RID: 903
		private readonly ISampleProvider sourceProvider;

		// Token: 0x04000388 RID: 904
		private int phase;

		// Token: 0x04000389 RID: 905
		private int phasePos;

		// Token: 0x0400038A RID: 906
		private int delayBySamples;

		// Token: 0x0400038B RID: 907
		private int skipOverSamples;

		// Token: 0x0400038C RID: 908
		private int takeSamples;

		// Token: 0x0400038D RID: 909
		private int leadOutSamples;
	}
}
