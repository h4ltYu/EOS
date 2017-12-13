using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace NAudio.Wave
{
	/// <summary>
	/// Represents a wave out device
	/// </summary>
	// Token: 0x020001CD RID: 461
	public class WaveOut : IWavePlayer, IDisposable, IWavePosition
	{
		/// <summary>
		/// Indicates playback has stopped automatically
		/// </summary>
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060009E9 RID: 2537 RVA: 0x0001C908 File Offset: 0x0001AB08
		// (remove) Token: 0x060009EA RID: 2538 RVA: 0x0001C940 File Offset: 0x0001AB40
		public event EventHandler<StoppedEventArgs> PlaybackStopped;

		/// <summary>
		/// Retrieves the capabilities of a waveOut device
		/// </summary>
		/// <param name="devNumber">Device to test</param>
		/// <returns>The WaveOut device capabilities</returns>
		// Token: 0x060009EB RID: 2539 RVA: 0x0001C978 File Offset: 0x0001AB78
		public static WaveOutCapabilities GetCapabilities(int devNumber)
		{
			WaveOutCapabilities waveOutCapabilities = default(WaveOutCapabilities);
			int waveOutCapsSize = Marshal.SizeOf(waveOutCapabilities);
			MmException.Try(WaveInterop.waveOutGetDevCaps((IntPtr)devNumber, out waveOutCapabilities, waveOutCapsSize), "waveOutGetDevCaps");
			return waveOutCapabilities;
		}

		/// <summary>
		/// Returns the number of Wave Out devices available in the system
		/// </summary>
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x0001C9B2 File Offset: 0x0001ABB2
		public static int DeviceCount
		{
			get
			{
				return WaveInterop.waveOutGetNumDevs();
			}
		}

		/// <summary>
		/// Gets or sets the desired latency in milliseconds
		/// Should be set before a call to Init
		/// </summary>
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x0001C9B9 File Offset: 0x0001ABB9
		// (set) Token: 0x060009EE RID: 2542 RVA: 0x0001C9C1 File Offset: 0x0001ABC1
		public int DesiredLatency { get; set; }

		/// <summary>
		/// Gets or sets the number of buffers used
		/// Should be set before a call to Init
		/// </summary>
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x0001C9CA File Offset: 0x0001ABCA
		// (set) Token: 0x060009F0 RID: 2544 RVA: 0x0001C9D2 File Offset: 0x0001ABD2
		public int NumberOfBuffers { get; set; }

		/// <summary>
		/// Gets or sets the device number
		/// Should be set before a call to Init
		/// This must be between 0 and <see>DeviceCount</see> - 1.
		/// </summary>
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x0001C9DB File Offset: 0x0001ABDB
		// (set) Token: 0x060009F2 RID: 2546 RVA: 0x0001C9E3 File Offset: 0x0001ABE3
		public int DeviceNumber { get; set; }

		/// <summary>
		/// Creates a default WaveOut device
		/// Will use window callbacks if called from a GUI thread, otherwise function
		/// callbacks
		/// </summary>
		// Token: 0x060009F3 RID: 2547 RVA: 0x0001C9EC File Offset: 0x0001ABEC
		public WaveOut() : this((SynchronizationContext.Current == null) ? WaveCallbackInfo.FunctionCallback() : WaveCallbackInfo.NewWindow())
		{
		}

		/// <summary>
		/// Creates a WaveOut device using the specified window handle for callbacks
		/// </summary>
		/// <param name="windowHandle">A valid window handle</param>
		// Token: 0x060009F4 RID: 2548 RVA: 0x0001CA07 File Offset: 0x0001AC07
		public WaveOut(IntPtr windowHandle) : this(WaveCallbackInfo.ExistingWindow(windowHandle))
		{
		}

		/// <summary>
		/// Opens a WaveOut device
		/// </summary>
		// Token: 0x060009F5 RID: 2549 RVA: 0x0001CA18 File Offset: 0x0001AC18
		public WaveOut(WaveCallbackInfo callbackInfo)
		{
			this.syncContext = SynchronizationContext.Current;
			this.DeviceNumber = 0;
			this.DesiredLatency = 300;
			this.NumberOfBuffers = 2;
			this.callback = new WaveInterop.WaveCallback(this.Callback);
			this.waveOutLock = new object();
			this.callbackInfo = callbackInfo;
			callbackInfo.Connect(this.callback);
		}

		/// <summary>
		/// Initialises the WaveOut device
		/// </summary>
		/// <param name="waveProvider">WaveProvider to play</param>
		// Token: 0x060009F6 RID: 2550 RVA: 0x0001CA8C File Offset: 0x0001AC8C
		public void Init(IWaveProvider waveProvider)
		{
			this.waveStream = waveProvider;
			int bufferSize = waveProvider.WaveFormat.ConvertLatencyToByteSize((this.DesiredLatency + this.NumberOfBuffers - 1) / this.NumberOfBuffers);
			MmResult result;
			lock (this.waveOutLock)
			{
				result = this.callbackInfo.WaveOutOpen(out this.hWaveOut, this.DeviceNumber, this.waveStream.WaveFormat, this.callback);
			}
			MmException.Try(result, "waveOutOpen");
			this.buffers = new WaveOutBuffer[this.NumberOfBuffers];
			this.playbackState = PlaybackState.Stopped;
			for (int i = 0; i < this.NumberOfBuffers; i++)
			{
				this.buffers[i] = new WaveOutBuffer(this.hWaveOut, bufferSize, this.waveStream, this.waveOutLock);
			}
		}

		/// <summary>
		/// Start playing the audio from the WaveStream
		/// </summary>
		// Token: 0x060009F7 RID: 2551 RVA: 0x0001CB68 File Offset: 0x0001AD68
		public void Play()
		{
			if (this.playbackState == PlaybackState.Stopped)
			{
				this.playbackState = PlaybackState.Playing;
				this.EnqueueBuffers();
				return;
			}
			if (this.playbackState == PlaybackState.Paused)
			{
				this.EnqueueBuffers();
				this.Resume();
				this.playbackState = PlaybackState.Playing;
			}
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0001CBA4 File Offset: 0x0001ADA4
		private void EnqueueBuffers()
		{
			for (int i = 0; i < this.NumberOfBuffers; i++)
			{
				if (!this.buffers[i].InQueue)
				{
					if (!this.buffers[i].OnDone())
					{
						this.playbackState = PlaybackState.Stopped;
						return;
					}
					Interlocked.Increment(ref this.queuedBuffers);
				}
			}
		}

		/// <summary>
		/// Pause the audio
		/// </summary>
		// Token: 0x060009F9 RID: 2553 RVA: 0x0001CBF8 File Offset: 0x0001ADF8
		public void Pause()
		{
			if (this.playbackState == PlaybackState.Playing)
			{
				MmResult mmResult;
				lock (this.waveOutLock)
				{
					mmResult = WaveInterop.waveOutPause(this.hWaveOut);
				}
				if (mmResult != MmResult.NoError)
				{
					throw new MmException(mmResult, "waveOutPause");
				}
				this.playbackState = PlaybackState.Paused;
			}
		}

		/// <summary>
		/// Resume playing after a pause from the same position
		/// </summary>
		// Token: 0x060009FA RID: 2554 RVA: 0x0001CC5C File Offset: 0x0001AE5C
		public void Resume()
		{
			if (this.playbackState == PlaybackState.Paused)
			{
				MmResult mmResult;
				lock (this.waveOutLock)
				{
					mmResult = WaveInterop.waveOutRestart(this.hWaveOut);
				}
				if (mmResult != MmResult.NoError)
				{
					throw new MmException(mmResult, "waveOutRestart");
				}
				this.playbackState = PlaybackState.Playing;
			}
		}

		/// <summary>
		/// Stop and reset the WaveOut device
		/// </summary>
		// Token: 0x060009FB RID: 2555 RVA: 0x0001CCC0 File Offset: 0x0001AEC0
		public void Stop()
		{
			if (this.playbackState != PlaybackState.Stopped)
			{
				this.playbackState = PlaybackState.Stopped;
				MmResult mmResult;
				lock (this.waveOutLock)
				{
					mmResult = WaveInterop.waveOutReset(this.hWaveOut);
				}
				if (mmResult != MmResult.NoError)
				{
					throw new MmException(mmResult, "waveOutReset");
				}
				if (this.callbackInfo.Strategy == WaveCallbackStrategy.FunctionCallback)
				{
					this.RaisePlaybackStoppedEvent(null);
				}
			}
		}

		/// <summary>
		/// Gets the current position in bytes from the wave output device.
		/// (n.b. this is not the same thing as the position within your reader
		/// stream - it calls directly into waveOutGetPosition)
		/// </summary>
		/// <returns>Position in bytes</returns>
		// Token: 0x060009FC RID: 2556 RVA: 0x0001CD38 File Offset: 0x0001AF38
		public long GetPosition()
		{
			long result;
			lock (this.waveOutLock)
			{
				MmTime mmTime = default(MmTime);
				mmTime.wType = 4u;
				MmException.Try(WaveInterop.waveOutGetPosition(this.hWaveOut, out mmTime, Marshal.SizeOf(mmTime)), "waveOutGetPosition");
				if (mmTime.wType != 4u)
				{
					throw new Exception(string.Format("waveOutGetPosition: wType -> Expected {0}, Received {1}", 4, mmTime.wType));
				}
				result = (long)((ulong)mmTime.cb);
			}
			return result;
		}

		/// <summary>
		/// Gets a <see cref="T:NAudio.Wave.WaveFormat" /> instance indicating the format the hardware is using.
		/// </summary>
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x0001CDD4 File Offset: 0x0001AFD4
		public WaveFormat OutputWaveFormat
		{
			get
			{
				return this.waveStream.WaveFormat;
			}
		}

		/// <summary>
		/// Playback State
		/// </summary>
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x0001CDE1 File Offset: 0x0001AFE1
		public PlaybackState PlaybackState
		{
			get
			{
				return this.playbackState;
			}
		}

		/// <summary>
		/// Volume for this device 1.0 is full scale
		/// </summary>
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x0001CDEB File Offset: 0x0001AFEB
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x0001CDF3 File Offset: 0x0001AFF3
		public float Volume
		{
			get
			{
				return this.volume;
			}
			set
			{
				WaveOut.SetWaveOutVolume(value, this.hWaveOut, this.waveOutLock);
				this.volume = value;
			}
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0001CE10 File Offset: 0x0001B010
		internal static void SetWaveOutVolume(float value, IntPtr hWaveOut, object lockObject)
		{
			if (value < 0f)
			{
				throw new ArgumentOutOfRangeException("value", "Volume must be between 0.0 and 1.0");
			}
			if (value > 1f)
			{
				throw new ArgumentOutOfRangeException("value", "Volume must be between 0.0 and 1.0");
			}
			int dwVolume = (int)(value * 65535f) + ((int)(value * 65535f) << 16);
			MmResult result;
			lock (lockObject)
			{
				result = WaveInterop.waveOutSetVolume(hWaveOut, dwVolume);
			}
			MmException.Try(result, "waveOutSetVolume");
		}

		/// <summary>
		/// Closes this WaveOut device
		/// </summary>
		// Token: 0x06000A02 RID: 2562 RVA: 0x0001CE9C File Offset: 0x0001B09C
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			this.Dispose(true);
		}

		/// <summary>
		/// Closes the WaveOut device and disposes of buffers
		/// </summary>
		/// <param name="disposing">True if called from <see>Dispose</see></param>
		// Token: 0x06000A03 RID: 2563 RVA: 0x0001CEAC File Offset: 0x0001B0AC
		protected void Dispose(bool disposing)
		{
			this.Stop();
			if (disposing && this.buffers != null)
			{
				for (int i = 0; i < this.buffers.Length; i++)
				{
					if (this.buffers[i] != null)
					{
						this.buffers[i].Dispose();
					}
				}
				this.buffers = null;
			}
			lock (this.waveOutLock)
			{
				WaveInterop.waveOutClose(this.hWaveOut);
			}
			if (disposing)
			{
				this.callbackInfo.Disconnect();
			}
		}

		/// <summary>
		/// Finalizer. Only called when user forgets to call <see>Dispose</see>
		/// </summary>
		// Token: 0x06000A04 RID: 2564 RVA: 0x0001CF3C File Offset: 0x0001B13C
		~WaveOut()
		{
			this.Dispose(false);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0001CF6C File Offset: 0x0001B16C
		private void Callback(IntPtr hWaveOut, WaveInterop.WaveMessage uMsg, IntPtr dwInstance, WaveHeader wavhdr, IntPtr dwReserved)
		{
			if (uMsg == WaveInterop.WaveMessage.WaveOutDone)
			{
				WaveOutBuffer waveOutBuffer = (WaveOutBuffer)((GCHandle)wavhdr.userData).Target;
				Interlocked.Decrement(ref this.queuedBuffers);
				Exception e = null;
				if (this.PlaybackState == PlaybackState.Playing)
				{
					lock (this.waveOutLock)
					{
						try
						{
							if (waveOutBuffer.OnDone())
							{
								Interlocked.Increment(ref this.queuedBuffers);
							}
						}
						catch (Exception ex)
						{
							e = ex;
						}
					}
				}
				if (this.queuedBuffers == 0)
				{
					if (this.callbackInfo.Strategy == WaveCallbackStrategy.FunctionCallback && this.playbackState == PlaybackState.Stopped)
					{
						return;
					}
					this.playbackState = PlaybackState.Stopped;
					this.RaisePlaybackStoppedEvent(e);
				}
			}
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0001D05C File Offset: 0x0001B25C
		private void RaisePlaybackStoppedEvent(Exception e)
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

		// Token: 0x04000B1D RID: 2845
		private IntPtr hWaveOut;

		// Token: 0x04000B1E RID: 2846
		private WaveOutBuffer[] buffers;

		// Token: 0x04000B1F RID: 2847
		private IWaveProvider waveStream;

		// Token: 0x04000B20 RID: 2848
		private volatile PlaybackState playbackState;

		// Token: 0x04000B21 RID: 2849
		private WaveInterop.WaveCallback callback;

		// Token: 0x04000B22 RID: 2850
		private float volume = 1f;

		// Token: 0x04000B23 RID: 2851
		private WaveCallbackInfo callbackInfo;

		// Token: 0x04000B24 RID: 2852
		private object waveOutLock;

		// Token: 0x04000B25 RID: 2853
		private int queuedBuffers;

		// Token: 0x04000B26 RID: 2854
		private SynchronizationContext syncContext;
	}
}
