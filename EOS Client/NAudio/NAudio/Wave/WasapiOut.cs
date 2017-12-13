using System;
using System.Runtime.InteropServices;
using System.Threading;
using NAudio.CoreAudioApi;

namespace NAudio.Wave
{
	/// <summary>
	/// Support for playback using Wasapi
	/// </summary>
	// Token: 0x020001C9 RID: 457
	public class WasapiOut : IWavePlayer, IDisposable, IWavePosition
	{
		/// <summary>
		/// Playback Stopped
		/// </summary>
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060009B2 RID: 2482 RVA: 0x0001BE14 File Offset: 0x0001A014
		// (remove) Token: 0x060009B3 RID: 2483 RVA: 0x0001BE4C File Offset: 0x0001A04C
		public event EventHandler<StoppedEventArgs> PlaybackStopped;

		/// <summary>
		/// WASAPI Out using default audio endpoint
		/// </summary>
		/// <param name="shareMode">ShareMode - shared or exclusive</param>
		/// <param name="latency">Desired latency in milliseconds</param>
		// Token: 0x060009B4 RID: 2484 RVA: 0x0001BE81 File Offset: 0x0001A081
		public WasapiOut(AudioClientShareMode shareMode, int latency) : this(WasapiOut.GetDefaultAudioEndpoint(), shareMode, true, latency)
		{
		}

		/// <summary>
		/// WASAPI Out using default audio endpoint
		/// </summary>
		/// <param name="shareMode">ShareMode - shared or exclusive</param>
		/// <param name="useEventSync">true if sync is done with event. false use sleep.</param>
		/// <param name="latency">Desired latency in milliseconds</param>
		// Token: 0x060009B5 RID: 2485 RVA: 0x0001BE91 File Offset: 0x0001A091
		public WasapiOut(AudioClientShareMode shareMode, bool useEventSync, int latency) : this(WasapiOut.GetDefaultAudioEndpoint(), shareMode, useEventSync, latency)
		{
		}

		/// <summary>
		/// Creates a new WASAPI Output
		/// </summary>
		/// <param name="device">Device to use</param>
		/// <param name="shareMode"></param>
		/// <param name="useEventSync">true if sync is done with event. false use sleep.</param>
		/// <param name="latency"></param>
		// Token: 0x060009B6 RID: 2486 RVA: 0x0001BEA1 File Offset: 0x0001A0A1
		public WasapiOut(MMDevice device, AudioClientShareMode shareMode, bool useEventSync, int latency)
		{
			this.audioClient = device.AudioClient;
			this.mmDevice = device;
			this.shareMode = shareMode;
			this.isUsingEventSync = useEventSync;
			this.latencyMilliseconds = latency;
			this.syncContext = SynchronizationContext.Current;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0001BEE0 File Offset: 0x0001A0E0
		private static MMDevice GetDefaultAudioEndpoint()
		{
			if (Environment.OSVersion.Version.Major < 6)
			{
				throw new NotSupportedException("WASAPI supported only on Windows Vista and above");
			}
			MMDeviceEnumerator mmdeviceEnumerator = new MMDeviceEnumerator();
			return mmdeviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0001BF18 File Offset: 0x0001A118
		private void PlayThread()
		{
			ResamplerDmoStream resamplerDmoStream = null;
			IWaveProvider playbackProvider = this.sourceProvider;
			Exception e = null;
			try
			{
				if (this.dmoResamplerNeeded)
				{
					resamplerDmoStream = new ResamplerDmoStream(this.sourceProvider, this.outputFormat);
					playbackProvider = resamplerDmoStream;
				}
				this.bufferFrameCount = this.audioClient.BufferSize;
				this.bytesPerFrame = this.outputFormat.Channels * this.outputFormat.BitsPerSample / 8;
				this.readBuffer = new byte[this.bufferFrameCount * this.bytesPerFrame];
				this.FillBuffer(playbackProvider, this.bufferFrameCount);
				WaitHandle[] waitHandles = new WaitHandle[]
				{
					this.frameEventWaitHandle
				};
				this.audioClient.Start();
				while (this.playbackState != PlaybackState.Stopped)
				{
					int num = 0;
					if (this.isUsingEventSync)
					{
						num = WaitHandle.WaitAny(waitHandles, 3 * this.latencyMilliseconds, false);
					}
					else
					{
						Thread.Sleep(this.latencyMilliseconds / 2);
					}
					if (this.playbackState == PlaybackState.Playing && num != 258)
					{
						int num2;
						if (this.isUsingEventSync)
						{
							num2 = ((this.shareMode == AudioClientShareMode.Shared) ? this.audioClient.CurrentPadding : 0);
						}
						else
						{
							num2 = this.audioClient.CurrentPadding;
						}
						int num3 = this.bufferFrameCount - num2;
						if (num3 > 10)
						{
							this.FillBuffer(playbackProvider, num3);
						}
					}
				}
				Thread.Sleep(this.latencyMilliseconds / 2);
				this.audioClient.Stop();
				if (this.playbackState == PlaybackState.Stopped)
				{
					this.audioClient.Reset();
				}
			}
			catch (Exception ex)
			{
				e = ex;
			}
			finally
			{
				if (resamplerDmoStream != null)
				{
					resamplerDmoStream.Dispose();
				}
				this.RaisePlaybackStopped(e);
			}
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0001C104 File Offset: 0x0001A304
		private void RaisePlaybackStopped(Exception e)
		{
			EventHandler<StoppedEventArgs> handler = this.PlaybackStopped;
			if (handler != null)
			{
				if (this.syncContext == null)
				{
					handler(this, new StoppedEventArgs(e));
					return;
				}
				this.syncContext.Post(delegate(object state)
				{
					handler(this, new StoppedEventArgs(e));
				}, null);
			}
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0001C178 File Offset: 0x0001A378
		private void FillBuffer(IWaveProvider playbackProvider, int frameCount)
		{
			IntPtr buffer = this.renderClient.GetBuffer(frameCount);
			int count = frameCount * this.bytesPerFrame;
			int num = playbackProvider.Read(this.readBuffer, 0, count);
			if (num == 0)
			{
				this.playbackState = PlaybackState.Stopped;
			}
			Marshal.Copy(this.readBuffer, 0, buffer, num);
			int numFramesWritten = num / this.bytesPerFrame;
			this.renderClient.ReleaseBuffer(numFramesWritten, AudioClientBufferFlags.None);
		}

		/// <summary>
		/// Gets the current position in bytes from the wave output device.
		/// (n.b. this is not the same thing as the position within your reader
		/// stream)
		/// </summary>
		/// <returns>Position in bytes</returns>
		// Token: 0x060009BB RID: 2491 RVA: 0x0001C1DA File Offset: 0x0001A3DA
		public long GetPosition()
		{
			if (this.playbackState == PlaybackState.Stopped)
			{
				return 0L;
			}
			return (long)this.audioClient.AudioClockClient.AdjustedPosition;
		}

		/// <summary>
		/// Gets a <see cref="T:NAudio.Wave.WaveFormat" /> instance indicating the format the hardware is using.
		/// </summary>
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x0001C1F9 File Offset: 0x0001A3F9
		public WaveFormat OutputWaveFormat
		{
			get
			{
				return this.outputFormat;
			}
		}

		/// <summary>
		/// Begin Playback
		/// </summary>
		// Token: 0x060009BD RID: 2493 RVA: 0x0001C204 File Offset: 0x0001A404
		public void Play()
		{
			if (this.playbackState != PlaybackState.Playing)
			{
				if (this.playbackState == PlaybackState.Stopped)
				{
					this.playThread = new Thread(new ThreadStart(this.PlayThread));
					this.playbackState = PlaybackState.Playing;
					this.playThread.Start();
					return;
				}
				this.playbackState = PlaybackState.Playing;
			}
		}

		/// <summary>
		/// Stop playback and flush buffers
		/// </summary>
		// Token: 0x060009BE RID: 2494 RVA: 0x0001C25B File Offset: 0x0001A45B
		public void Stop()
		{
			if (this.playbackState != PlaybackState.Stopped)
			{
				this.playbackState = PlaybackState.Stopped;
				this.playThread.Join();
				this.playThread = null;
			}
		}

		/// <summary>
		/// Stop playback without flushing buffers
		/// </summary>
		// Token: 0x060009BF RID: 2495 RVA: 0x0001C282 File Offset: 0x0001A482
		public void Pause()
		{
			if (this.playbackState == PlaybackState.Playing)
			{
				this.playbackState = PlaybackState.Paused;
			}
		}

		/// <summary>
		/// Initialize for playing the specified wave stream
		/// </summary>
		/// <param name="waveProvider">IWaveProvider to play</param>
		// Token: 0x060009C0 RID: 2496 RVA: 0x0001C298 File Offset: 0x0001A498
		public void Init(IWaveProvider waveProvider)
		{
			long num = (long)(this.latencyMilliseconds * 10000);
			this.outputFormat = waveProvider.WaveFormat;
			WaveFormatExtensible waveFormatExtensible;
			if (!this.audioClient.IsFormatSupported(this.shareMode, this.outputFormat, out waveFormatExtensible))
			{
				if (waveFormatExtensible == null)
				{
					WaveFormat waveFormat = this.audioClient.MixFormat;
					if (!this.audioClient.IsFormatSupported(this.shareMode, waveFormat))
					{
						foreach (WaveFormatExtensible waveFormat in new WaveFormatExtensible[]
						{
							new WaveFormatExtensible(this.outputFormat.SampleRate, 32, this.outputFormat.Channels),
							new WaveFormatExtensible(this.outputFormat.SampleRate, 24, this.outputFormat.Channels),
							new WaveFormatExtensible(this.outputFormat.SampleRate, 16, this.outputFormat.Channels)
						})
						{
							if (this.audioClient.IsFormatSupported(this.shareMode, waveFormat))
							{
								break;
							}
							waveFormat = null;
						}
						if (waveFormat == null)
						{
							waveFormat = new WaveFormatExtensible(this.outputFormat.SampleRate, 16, 2);
							if (!this.audioClient.IsFormatSupported(this.shareMode, waveFormat))
							{
								throw new NotSupportedException("Can't find a supported format to use");
							}
						}
					}
					this.outputFormat = waveFormat;
				}
				else
				{
					this.outputFormat = waveFormatExtensible;
				}
				using (new ResamplerDmoStream(waveProvider, this.outputFormat))
				{
				}
				this.dmoResamplerNeeded = true;
			}
			else
			{
				this.dmoResamplerNeeded = false;
			}
			this.sourceProvider = waveProvider;
			if (this.isUsingEventSync)
			{
				if (this.shareMode == AudioClientShareMode.Shared)
				{
					this.audioClient.Initialize(this.shareMode, AudioClientStreamFlags.EventCallback, 0L, 0L, this.outputFormat, Guid.Empty);
					this.latencyMilliseconds = (int)(this.audioClient.StreamLatency / 10000L);
				}
				else
				{
					this.audioClient.Initialize(this.shareMode, AudioClientStreamFlags.EventCallback, num, num, this.outputFormat, Guid.Empty);
				}
				this.frameEventWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
				this.audioClient.SetEventHandle(this.frameEventWaitHandle.SafeWaitHandle.DangerousGetHandle());
			}
			else
			{
				this.audioClient.Initialize(this.shareMode, AudioClientStreamFlags.None, num, 0L, this.outputFormat, Guid.Empty);
			}
			this.renderClient = this.audioClient.AudioRenderClient;
		}

		/// <summary>
		/// Playback State
		/// </summary>
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x0001C4FC File Offset: 0x0001A6FC
		public PlaybackState PlaybackState
		{
			get
			{
				return this.playbackState;
			}
		}

		/// <summary>
		/// Volume
		/// </summary>
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x0001C506 File Offset: 0x0001A706
		// (set) Token: 0x060009C3 RID: 2499 RVA: 0x0001C518 File Offset: 0x0001A718
		public float Volume
		{
			get
			{
				return this.mmDevice.AudioEndpointVolume.MasterVolumeLevelScalar;
			}
			set
			{
				if (value < 0f)
				{
					throw new ArgumentOutOfRangeException("value", "Volume must be between 0.0 and 1.0");
				}
				if (value > 1f)
				{
					throw new ArgumentOutOfRangeException("value", "Volume must be between 0.0 and 1.0");
				}
				this.mmDevice.AudioEndpointVolume.MasterVolumeLevelScalar = value;
			}
		}

		/// <summary>
		/// Retrieve the AudioStreamVolume object for this audio stream
		/// </summary>
		/// <remarks>
		/// This returns the AudioStreamVolume object ONLY for shared audio streams.
		/// </remarks>
		/// <exception cref="T:System.InvalidOperationException">
		/// This is thrown when an exclusive audio stream is being used.
		/// </exception>
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x0001C566 File Offset: 0x0001A766
		public AudioStreamVolume AudioStreamVolume
		{
			get
			{
				if (this.shareMode == AudioClientShareMode.Exclusive)
				{
					throw new InvalidOperationException("AudioStreamVolume is ONLY supported for shared audio streams.");
				}
				return this.audioClient.AudioStreamVolume;
			}
		}

		/// <summary>
		/// Dispose
		/// </summary>
		// Token: 0x060009C5 RID: 2501 RVA: 0x0001C587 File Offset: 0x0001A787
		public void Dispose()
		{
			if (this.audioClient != null)
			{
				this.Stop();
				this.audioClient.Dispose();
				this.audioClient = null;
				this.renderClient = null;
			}
		}

		// Token: 0x04000AFE RID: 2814
		private AudioClient audioClient;

		// Token: 0x04000AFF RID: 2815
		private readonly MMDevice mmDevice;

		// Token: 0x04000B00 RID: 2816
		private readonly AudioClientShareMode shareMode;

		// Token: 0x04000B01 RID: 2817
		private AudioRenderClient renderClient;

		// Token: 0x04000B02 RID: 2818
		private IWaveProvider sourceProvider;

		// Token: 0x04000B03 RID: 2819
		private int latencyMilliseconds;

		// Token: 0x04000B04 RID: 2820
		private int bufferFrameCount;

		// Token: 0x04000B05 RID: 2821
		private int bytesPerFrame;

		// Token: 0x04000B06 RID: 2822
		private readonly bool isUsingEventSync;

		// Token: 0x04000B07 RID: 2823
		private EventWaitHandle frameEventWaitHandle;

		// Token: 0x04000B08 RID: 2824
		private byte[] readBuffer;

		// Token: 0x04000B09 RID: 2825
		private volatile PlaybackState playbackState;

		// Token: 0x04000B0A RID: 2826
		private Thread playThread;

		// Token: 0x04000B0B RID: 2827
		private WaveFormat outputFormat;

		// Token: 0x04000B0C RID: 2828
		private bool dmoResamplerNeeded;

		// Token: 0x04000B0D RID: 2829
		private readonly SynchronizationContext syncContext;
	}
}
