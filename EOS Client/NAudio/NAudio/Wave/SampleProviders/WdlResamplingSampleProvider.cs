using System;
using NAudio.Dsp;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Fully managed resampling sample provider, based on the WDL Resampler
	/// </summary>
	// Token: 0x0200007E RID: 126
	public class WdlResamplingSampleProvider : ISampleProvider
	{
		/// <summary>
		/// Constructs a new resampler
		/// </summary>
		/// <param name="source">Source to resample</param>
		/// <param name="newSampleRate">Desired output sample rate</param>
		// Token: 0x060002A9 RID: 681 RVA: 0x00008F94 File Offset: 0x00007194
		public WdlResamplingSampleProvider(ISampleProvider source, int newSampleRate)
		{
			this.channels = source.WaveFormat.Channels;
			this.outFormat = WaveFormat.CreateIeeeFloatWaveFormat(newSampleRate, this.channels);
			this.source = source;
			this.resampler = new WdlResampler();
			this.resampler.SetMode(true, 2, false, 64, 32);
			this.resampler.SetFilterParms(0.693f, 0.707f);
			this.resampler.SetFeedMode(false);
			this.resampler.SetRates((double)source.WaveFormat.SampleRate, (double)newSampleRate);
		}

		/// <summary>
		/// Reads from this sample provider
		/// </summary>
		// Token: 0x060002AA RID: 682 RVA: 0x00009028 File Offset: 0x00007228
		public int Read(float[] buffer, int offset, int count)
		{
			int num = count / this.channels;
			float[] buffer2;
			int offset2;
			int num2 = this.resampler.ResamplePrepare(num, this.outFormat.Channels, out buffer2, out offset2);
			int nsamples_in = this.source.Read(buffer2, offset2, num2 * this.channels) / this.channels;
			int num3 = this.resampler.ResampleOut(buffer, offset, nsamples_in, num, this.channels);
			return num3 * this.channels;
		}

		/// <summary>
		/// Output WaveFormat
		/// </summary>
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00009099 File Offset: 0x00007299
		public WaveFormat WaveFormat
		{
			get
			{
				return this.outFormat;
			}
		}

		// Token: 0x040003AD RID: 941
		private readonly WdlResampler resampler;

		// Token: 0x040003AE RID: 942
		private readonly WaveFormat outFormat;

		// Token: 0x040003AF RID: 943
		private readonly ISampleProvider source;

		// Token: 0x040003B0 RID: 944
		private readonly int channels;
	}
}
