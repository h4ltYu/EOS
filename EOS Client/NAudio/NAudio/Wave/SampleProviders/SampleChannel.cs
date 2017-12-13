using System;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Utility class that takes an IWaveProvider input at any bit depth
	/// and exposes it as an ISampleProvider. Can turn mono inputs into stereo,
	/// and allows adjusting of volume
	/// (The eventual successor to WaveChannel32)
	/// This class also serves as an example of how you can link together several simple 
	/// Sample Providers to form a more useful class.
	/// </summary>
	// Token: 0x020001FD RID: 509
	public class SampleChannel : ISampleProvider
	{
		/// <summary>
		/// Initialises a new instance of SampleChannel
		/// </summary>
		/// <param name="waveProvider">Source wave provider, must be PCM or IEEE</param>
		// Token: 0x06000B90 RID: 2960 RVA: 0x000228C5 File Offset: 0x00020AC5
		public SampleChannel(IWaveProvider waveProvider) : this(waveProvider, false)
		{
		}

		/// <summary>
		/// Initialises a new instance of SampleChannel
		/// </summary>
		/// <param name="waveProvider">Source wave provider, must be PCM or IEEE</param>
		/// <param name="forceStereo">force mono inputs to become stereo</param>
		// Token: 0x06000B91 RID: 2961 RVA: 0x000228D0 File Offset: 0x00020AD0
		public SampleChannel(IWaveProvider waveProvider, bool forceStereo)
		{
			ISampleProvider sampleProvider = SampleProviderConverters.ConvertWaveProviderIntoSampleProvider(waveProvider);
			if (sampleProvider.WaveFormat.Channels == 1 && forceStereo)
			{
				sampleProvider = new MonoToStereoSampleProvider(sampleProvider);
			}
			this.waveFormat = sampleProvider.WaveFormat;
			this.preVolumeMeter = new MeteringSampleProvider(sampleProvider);
			this.volumeProvider = new VolumeSampleProvider(this.preVolumeMeter);
		}

		/// <summary>
		/// Reads samples from this sample provider
		/// </summary>
		/// <param name="buffer">Sample buffer</param>
		/// <param name="offset">Offset into sample buffer</param>
		/// <param name="sampleCount">Number of samples desired</param>
		/// <returns>Number of samples read</returns>
		// Token: 0x06000B92 RID: 2962 RVA: 0x0002292B File Offset: 0x00020B2B
		public int Read(float[] buffer, int offset, int sampleCount)
		{
			return this.volumeProvider.Read(buffer, offset, sampleCount);
		}

		/// <summary>
		/// The WaveFormat of this Sample Provider
		/// </summary>
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x0002293B File Offset: 0x00020B3B
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		/// <summary>
		/// Allows adjusting the volume, 1.0f = full volume
		/// </summary>
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x00022943 File Offset: 0x00020B43
		// (set) Token: 0x06000B95 RID: 2965 RVA: 0x00022950 File Offset: 0x00020B50
		public float Volume
		{
			get
			{
				return this.volumeProvider.Volume;
			}
			set
			{
				this.volumeProvider.Volume = value;
			}
		}

		/// <summary>
		/// Raised periodically to inform the user of the max volume
		/// (before the volume meter)
		/// </summary>
		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000B96 RID: 2966 RVA: 0x0002295E File Offset: 0x00020B5E
		// (remove) Token: 0x06000B97 RID: 2967 RVA: 0x0002296C File Offset: 0x00020B6C
		public event EventHandler<StreamVolumeEventArgs> PreVolumeMeter
		{
			add
			{
				this.preVolumeMeter.StreamVolume += value;
			}
			remove
			{
				this.preVolumeMeter.StreamVolume -= value;
			}
		}

		// Token: 0x04000BF0 RID: 3056
		private readonly VolumeSampleProvider volumeProvider;

		// Token: 0x04000BF1 RID: 3057
		private readonly MeteringSampleProvider preVolumeMeter;

		// Token: 0x04000BF2 RID: 3058
		private readonly WaveFormat waveFormat;
	}
}
