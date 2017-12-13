using System;

namespace NAudio.Wave
{
	/// <summary>
	/// Represents the interface to a device that can play a WaveFile
	/// </summary>
	// Token: 0x020001B1 RID: 433
	public interface IWavePlayer : IDisposable
	{
		/// <summary>
		/// Begin playback
		/// </summary>
		// Token: 0x060008E9 RID: 2281
		void Play();

		/// <summary>
		/// Stop playback
		/// </summary>
		// Token: 0x060008EA RID: 2282
		void Stop();

		/// <summary>
		/// Pause Playback
		/// </summary>
		// Token: 0x060008EB RID: 2283
		void Pause();

		/// <summary>
		/// Initialise playback
		/// </summary>
		/// <param name="waveProvider">The waveprovider to be played</param>
		// Token: 0x060008EC RID: 2284
		void Init(IWaveProvider waveProvider);

		/// <summary>
		/// Current playback state
		/// </summary>
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060008ED RID: 2285
		PlaybackState PlaybackState { get; }

		/// <summary>
		/// The volume 1.0 is full scale
		/// </summary>
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060008EE RID: 2286
		// (set) Token: 0x060008EF RID: 2287
		[Obsolete("Not intending to keep supporting this going forward: set the volume on your input WaveProvider instead")]
		float Volume { get; set; }

		/// <summary>
		/// Indicates that playback has gone into a stopped state due to 
		/// reaching the end of the input stream or an error has been encountered during playback
		/// </summary>
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060008F0 RID: 2288
		// (remove) Token: 0x060008F1 RID: 2289
		event EventHandler<StoppedEventArgs> PlaybackStopped;
	}
}
