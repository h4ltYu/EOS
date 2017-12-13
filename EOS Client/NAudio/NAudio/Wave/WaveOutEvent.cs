using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace NAudio.Wave
{
	/// <summary>
	/// Alternative WaveOut class, making use of the Event callback
	/// </summary>
	// Token: 0x020001CE RID: 462
	public class WaveOutEvent : IWavePlayer, IDisposable, IWavePosition
	{
		/// <summary>
		/// Indicates playback has stopped automatically
		/// </summary>
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000A07 RID: 2567 RVA: 0x0001D0D0 File Offset: 0x0001B2D0
		// (remove) Token: 0x06000A08 RID: 2568 RVA: 0x0001D108 File Offset: 0x0001B308
		public event EventHandler<StoppedEventArgs> PlaybackStopped;

		/// <summary>
		/// Gets or sets the desired latency in milliseconds
		/// Should be set before a call to Init
		/// </summary>
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0001D13D File Offset: 0x0001B33D
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x0001D145 File Offset: 0x0001B345
		public int DesiredLatency { get; set; }

		/// <summary>
		/// Gets or sets the number of buffers used
		/// Should be set before a call to Init
		/// </summary>
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0001D14E File Offset: 0x0001B34E
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x0001D156 File Offset: 0x0001B356
		public int NumberOfBuffers { get; set; }

		/// <summary>
		/// Gets or sets the device number
		/// Should be set before a call to Init
		/// This must be between 0 and <see>DeviceCount</see> - 1.
		/// </summary>
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0001D15F File Offset: 0x0001B35F
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x0001D167 File Offset: 0x0001B367
		public int DeviceNumber { get; set; }

		/// <summary>
		/// Opens a WaveOut device
		/// </summary>
		// Token: 0x06000A0F RID: 2575 RVA: 0x0001D170 File Offset: 0x0001B370
		public WaveOutEvent()
		{
			this.syncContext = SynchronizationContext.Current;
			if (this.syncContext != null && (this.syncContext.GetType().Name == "LegacyAspNetSynchronizationContext" || this.syncContext.GetType().Name == "AspNetSynchronizationContext"))
			{
				this.syncContext = null;
			}
			this.DeviceNumber = 0;
			this.DesiredLatency = 300;
			this.NumberOfBuffers = 2;
			this.waveOutLock = new object();
		}

		/// <summary>
		/// Initialises the WaveOut device
		/// </summary>
		/// <param name="waveProvider">WaveProvider to play</param>
		// Token: 0x06000A10 RID: 2576 RVA: 0x0001D204 File Offset: 0x0001B404
		public void Init(IWaveProvider waveProvider)
		{
			if (this.playbackState != PlaybackState.Stopped)
			{
				throw new InvalidOperationException("Can't re-initialize during playback");
			}
			if (this.hWaveOut != IntPtr.Zero)
			{
				this.DisposeBuffers();
				this.CloseWaveOut();
			}
			this.callbackEvent = new AutoResetEvent(false);
			this.waveStream = waveProvider;
			int bufferSize = waveProvider.WaveFormat.ConvertLatencyToByteSize((this.DesiredLatency + this.NumberOfBuffers - 1) / this.NumberOfBuffers);
			MmResult result;
			lock (this.waveOutLock)
			{
				result = WaveInterop.waveOutOpenWindow(out this.hWaveOut, (IntPtr)this.DeviceNumber, this.waveStream.WaveFormat, this.callbackEvent.SafeWaitHandle.DangerousGetHandle(), IntPtr.Zero, WaveInterop.WaveInOutOpenFlags.CallbackEvent);
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
		// Token: 0x06000A11 RID: 2577 RVA: 0x0001D33C File Offset: 0x0001B53C
		public void Play()
		{
			if (this.buffers == null || this.waveStream == null)
			{
				throw new InvalidOperationException("Must call Init first");
			}
			if (this.playbackState == PlaybackState.Stopped)
			{
				this.playbackState = PlaybackState.Playing;
				this.callbackEvent.Set();
				ThreadPool.QueueUserWorkItem(delegate(object state)
				{
					this.PlaybackThread();
				}, null);
				return;
			}
			if (this.playbackState == PlaybackState.Paused)
			{
				this.Resume();
				this.callbackEvent.Set();
			}
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0001D3BC File Offset: 0x0001B5BC
		private void PlaybackThread()
		{
			Exception e = null;
			try
			{
				this.DoPlayback();
			}
			catch (Exception ex)
			{
				e = ex;
			}
			finally
			{
				this.playbackState = PlaybackState.Stopped;
				this.RaisePlaybackStoppedEvent(e);
			}
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0001D408 File Offset: 0x0001B608
		private void DoPlayback()
		{
			while (this.playbackState != PlaybackState.Stopped)
			{
				this.callbackEvent.WaitOne(this.DesiredLatency);
				if (this.playbackState == PlaybackState.Playing)
				{
					int num = 0;
					foreach (WaveOutBuffer waveOutBuffer in this.buffers)
					{
						if (waveOutBuffer.InQueue || waveOutBuffer.OnDone())
						{
							num++;
						}
					}
					if (num == 0)
					{
						this.playbackState = PlaybackState.Stopped;
						this.callbackEvent.Set();
					}
				}
			}
		}

		/// <summary>
		/// Pause the audio
		/// </summary>
		// Token: 0x06000A14 RID: 2580 RVA: 0x0001D488 File Offset: 0x0001B688
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
		// Token: 0x06000A15 RID: 2581 RVA: 0x0001D4EC File Offset: 0x0001B6EC
		private void Resume()
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
		// Token: 0x06000A16 RID: 2582 RVA: 0x0001D550 File Offset: 0x0001B750
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
				this.callbackEvent.Set();
			}
		}

		/// <summary>
		/// Gets the current position in bytes from the wave output device.
		/// (n.b. this is not the same thing as the position within your reader
		/// stream - it calls directly into waveOutGetPosition)
		/// </summary>
		/// <returns>Position in bytes</returns>
		// Token: 0x06000A17 RID: 2583 RVA: 0x0001D5C0 File Offset: 0x0001B7C0
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
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0001D65C File Offset: 0x0001B85C
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
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x0001D669 File Offset: 0x0001B869
		public PlaybackState PlaybackState
		{
			get
			{
				return this.playbackState;
			}
		}

		/// <summary>
		/// Obsolete property
		/// </summary>
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0001D673 File Offset: 0x0001B873
		// (set) Token: 0x06000A1B RID: 2587 RVA: 0x0001D67B File Offset: 0x0001B87B
		[Obsolete]
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

		/// <summary>
		/// Closes this WaveOut device
		/// </summary>
		// Token: 0x06000A1C RID: 2588 RVA: 0x0001D696 File Offset: 0x0001B896
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			this.Dispose(true);
		}

		/// <summary>
		/// Closes the WaveOut device and disposes of buffers
		/// </summary>
		/// <param name="disposing">True if called from <see>Dispose</see></param>
		// Token: 0x06000A1D RID: 2589 RVA: 0x0001D6A5 File Offset: 0x0001B8A5
		protected void Dispose(bool disposing)
		{
			this.Stop();
			if (disposing)
			{
				this.DisposeBuffers();
			}
			this.CloseWaveOut();
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0001D6BC File Offset: 0x0001B8BC
		private void CloseWaveOut()
		{
			if (this.callbackEvent != null)
			{
				this.callbackEvent.Close();
				this.callbackEvent = null;
			}
			lock (this.waveOutLock)
			{
				if (this.hWaveOut != IntPtr.Zero)
				{
					WaveInterop.waveOutClose(this.hWaveOut);
					this.hWaveOut = IntPtr.Zero;
				}
			}
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0001D734 File Offset: 0x0001B934
		private void DisposeBuffers()
		{
			if (this.buffers != null)
			{
				foreach (WaveOutBuffer waveOutBuffer in this.buffers)
				{
					waveOutBuffer.Dispose();
				}
				this.buffers = null;
			}
		}

		/// <summary>
		/// Finalizer. Only called when user forgets to call <see>Dispose</see>
		/// </summary>
		// Token: 0x06000A20 RID: 2592 RVA: 0x0001D770 File Offset: 0x0001B970
		~WaveOutEvent()
		{
			this.Dispose(false);
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0001D7C8 File Offset: 0x0001B9C8
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

		// Token: 0x04000B2B RID: 2859
		private readonly object waveOutLock;

		// Token: 0x04000B2C RID: 2860
		private readonly SynchronizationContext syncContext;

		// Token: 0x04000B2D RID: 2861
		private IntPtr hWaveOut;

		// Token: 0x04000B2E RID: 2862
		private WaveOutBuffer[] buffers;

		// Token: 0x04000B2F RID: 2863
		private IWaveProvider waveStream;

		// Token: 0x04000B30 RID: 2864
		private volatile PlaybackState playbackState;

		// Token: 0x04000B31 RID: 2865
		private AutoResetEvent callbackEvent;

		// Token: 0x04000B32 RID: 2866
		private float volume = 1f;
	}
}
