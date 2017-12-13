using System;
using NAudio.Dsp;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// ADSR sample provider allowing you to specify attack, decay, sustain and release values
	/// </summary>
	// Token: 0x02000070 RID: 112
	public class AdsrSampleProvider : ISampleProvider
	{
		/// <summary>
		/// Creates a new AdsrSampleProvider with default values
		/// </summary>
		// Token: 0x06000256 RID: 598 RVA: 0x00007A18 File Offset: 0x00005C18
		public AdsrSampleProvider(ISampleProvider source)
		{
			if (source.WaveFormat.Channels > 1)
			{
				throw new ArgumentException("Currently only supports mono inputs");
			}
			this.source = source;
			this.adsr = new EnvelopeGenerator();
			this.AttackSeconds = 0.01f;
			this.adsr.SustainLevel = 1f;
			this.adsr.DecayRate = 0f * (float)this.WaveFormat.SampleRate;
			this.ReleaseSeconds = 0.3f;
			this.adsr.Gate(true);
		}

		/// <summary>
		/// Attack time in seconds
		/// </summary>
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00007AA5 File Offset: 0x00005CA5
		// (set) Token: 0x06000258 RID: 600 RVA: 0x00007AAD File Offset: 0x00005CAD
		public float AttackSeconds
		{
			get
			{
				return this.attackSeconds;
			}
			set
			{
				this.attackSeconds = value;
				this.adsr.AttackRate = this.attackSeconds * (float)this.WaveFormat.SampleRate;
			}
		}

		/// <summary>
		/// Release time in seconds
		/// </summary>
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00007AD4 File Offset: 0x00005CD4
		// (set) Token: 0x0600025A RID: 602 RVA: 0x00007ADC File Offset: 0x00005CDC
		public float ReleaseSeconds
		{
			get
			{
				return this.releaseSeconds;
			}
			set
			{
				this.releaseSeconds = value;
				this.adsr.ReleaseRate = this.releaseSeconds * (float)this.WaveFormat.SampleRate;
			}
		}

		/// <summary>
		/// Reads audio from this sample provider
		/// </summary>
		// Token: 0x0600025B RID: 603 RVA: 0x00007B04 File Offset: 0x00005D04
		public int Read(float[] buffer, int offset, int count)
		{
			if (this.adsr.State == EnvelopeGenerator.EnvelopeState.Idle)
			{
				return 0;
			}
			int num = this.source.Read(buffer, offset, count);
			for (int i = 0; i < num; i++)
			{
				buffer[offset++] *= this.adsr.Process();
			}
			return num;
		}

		/// <summary>
		/// Enters the Release phase
		/// </summary>
		// Token: 0x0600025C RID: 604 RVA: 0x00007B5F File Offset: 0x00005D5F
		public void Stop()
		{
			this.adsr.Gate(false);
		}

		/// <summary>
		/// The output WaveFormat
		/// </summary>
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00007B6D File Offset: 0x00005D6D
		public WaveFormat WaveFormat
		{
			get
			{
				return this.source.WaveFormat;
			}
		}

		// Token: 0x04000373 RID: 883
		private readonly ISampleProvider source;

		// Token: 0x04000374 RID: 884
		private readonly EnvelopeGenerator adsr;

		// Token: 0x04000375 RID: 885
		private float attackSeconds;

		// Token: 0x04000376 RID: 886
		private float releaseSeconds;
	}
}
