using System;
using System.Runtime.InteropServices;
using System.Threading;
using NAudio.Mixer;

namespace NAudio.Wave
{
	/// <summary>
	/// Allows recording using the Windows waveIn APIs
	/// Events are raised as recorded buffers are made available
	/// </summary>
	// Token: 0x02000183 RID: 387
	public class WaveIn : IWaveIn, IDisposable
	{
		/// <summary>
		/// Indicates recorded data is available 
		/// </summary>
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060007F0 RID: 2032 RVA: 0x000172C0 File Offset: 0x000154C0
		// (remove) Token: 0x060007F1 RID: 2033 RVA: 0x000172F8 File Offset: 0x000154F8
		public event EventHandler<WaveInEventArgs> DataAvailable;

		/// <summary>
		/// Indicates that all recorded data has now been received.
		/// </summary>
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060007F2 RID: 2034 RVA: 0x00017330 File Offset: 0x00015530
		// (remove) Token: 0x060007F3 RID: 2035 RVA: 0x00017368 File Offset: 0x00015568
		public event EventHandler<StoppedEventArgs> RecordingStopped;

		/// <summary>
		/// Prepares a Wave input device for recording
		/// </summary>
		// Token: 0x060007F4 RID: 2036 RVA: 0x0001739D File Offset: 0x0001559D
		public WaveIn() : this(WaveCallbackInfo.NewWindow())
		{
		}

		/// <summary>
		/// Creates a WaveIn device using the specified window handle for callbacks
		/// </summary>
		/// <param name="windowHandle">A valid window handle</param>
		// Token: 0x060007F5 RID: 2037 RVA: 0x000173AA File Offset: 0x000155AA
		public WaveIn(IntPtr windowHandle) : this(WaveCallbackInfo.ExistingWindow(windowHandle))
		{
		}

		/// <summary>
		/// Prepares a Wave input device for recording
		/// </summary>
		// Token: 0x060007F6 RID: 2038 RVA: 0x000173B8 File Offset: 0x000155B8
		public WaveIn(WaveCallbackInfo callbackInfo)
		{
			this.syncContext = SynchronizationContext.Current;
			if ((callbackInfo.Strategy == WaveCallbackStrategy.NewWindow || callbackInfo.Strategy == WaveCallbackStrategy.ExistingWindow) && this.syncContext == null)
			{
				throw new InvalidOperationException("Use WaveInEvent to record on a background thread");
			}
			this.DeviceNumber = 0;
			this.WaveFormat = new WaveFormat(8000, 16, 1);
			this.BufferMilliseconds = 100;
			this.NumberOfBuffers = 3;
			this.callback = new WaveInterop.WaveCallback(this.Callback);
			this.callbackInfo = callbackInfo;
			callbackInfo.Connect(this.callback);
		}

		/// <summary>
		/// Returns the number of Wave In devices available in the system
		/// </summary>
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x00017449 File Offset: 0x00015649
		public static int DeviceCount
		{
			get
			{
				return WaveInterop.waveInGetNumDevs();
			}
		}

		/// <summary>
		/// Retrieves the capabilities of a waveIn device
		/// </summary>
		/// <param name="devNumber">Device to test</param>
		/// <returns>The WaveIn device capabilities</returns>
		// Token: 0x060007F8 RID: 2040 RVA: 0x00017450 File Offset: 0x00015650
		public static WaveInCapabilities GetCapabilities(int devNumber)
		{
			WaveInCapabilities waveInCapabilities = default(WaveInCapabilities);
			int waveInCapsSize = Marshal.SizeOf(waveInCapabilities);
			MmException.Try(WaveInterop.waveInGetDevCaps((IntPtr)devNumber, out waveInCapabilities, waveInCapsSize), "waveInGetDevCaps");
			return waveInCapabilities;
		}

		/// <summary>
		/// Milliseconds for the buffer. Recommended value is 100ms
		/// </summary>
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x0001748A File Offset: 0x0001568A
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x00017492 File Offset: 0x00015692
		public int BufferMilliseconds { get; set; }

		/// <summary>
		/// Number of Buffers to use (usually 2 or 3)
		/// </summary>
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x0001749B File Offset: 0x0001569B
		// (set) Token: 0x060007FC RID: 2044 RVA: 0x000174A3 File Offset: 0x000156A3
		public int NumberOfBuffers { get; set; }

		/// <summary>
		/// The device number to use
		/// </summary>
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x000174AC File Offset: 0x000156AC
		// (set) Token: 0x060007FE RID: 2046 RVA: 0x000174B4 File Offset: 0x000156B4
		public int DeviceNumber { get; set; }

		// Token: 0x060007FF RID: 2047 RVA: 0x000174C0 File Offset: 0x000156C0
		private void CreateBuffers()
		{
			int num = this.BufferMilliseconds * this.WaveFormat.AverageBytesPerSecond / 1000;
			if (num % this.WaveFormat.BlockAlign != 0)
			{
				num -= num % this.WaveFormat.BlockAlign;
			}
			this.buffers = new WaveInBuffer[this.NumberOfBuffers];
			for (int i = 0; i < this.buffers.Length; i++)
			{
				this.buffers[i] = new WaveInBuffer(this.waveInHandle, num);
			}
		}

		/// <summary>
		/// Called when we get a new buffer of recorded data
		/// </summary>
		// Token: 0x06000800 RID: 2048 RVA: 0x00017540 File Offset: 0x00015740
		private void Callback(IntPtr waveInHandle, WaveInterop.WaveMessage message, IntPtr userData, WaveHeader waveHeader, IntPtr reserved)
		{
			if (message == WaveInterop.WaveMessage.WaveInData && this.recording)
			{
				WaveInBuffer waveInBuffer = (WaveInBuffer)((GCHandle)waveHeader.userData).Target;
				if (waveInBuffer == null)
				{
					return;
				}
				this.lastReturnedBufferIndex = Array.IndexOf<WaveInBuffer>(this.buffers, waveInBuffer);
				this.RaiseDataAvailable(waveInBuffer);
				try
				{
					waveInBuffer.Reuse();
				}
				catch (Exception e)
				{
					this.recording = false;
					this.RaiseRecordingStopped(e);
				}
			}
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x000175C4 File Offset: 0x000157C4
		private void RaiseDataAvailable(WaveInBuffer buffer)
		{
			EventHandler<WaveInEventArgs> dataAvailable = this.DataAvailable;
			if (dataAvailable != null)
			{
				dataAvailable(this, new WaveInEventArgs(buffer.Data, buffer.BytesRecorded));
			}
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001761C File Offset: 0x0001581C
		private void RaiseRecordingStopped(Exception e)
		{
			EventHandler<StoppedEventArgs> handler = this.RecordingStopped;
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

		// Token: 0x06000803 RID: 2051 RVA: 0x00017690 File Offset: 0x00015890
		private void OpenWaveInDevice()
		{
			this.CloseWaveInDevice();
			MmResult result = this.callbackInfo.WaveInOpen(out this.waveInHandle, this.DeviceNumber, this.WaveFormat, this.callback);
			MmException.Try(result, "waveInOpen");
			this.CreateBuffers();
		}

		/// <summary>
		/// Start recording
		/// </summary>
		// Token: 0x06000804 RID: 2052 RVA: 0x000176D8 File Offset: 0x000158D8
		public void StartRecording()
		{
			if (this.recording)
			{
				throw new InvalidOperationException("Already recording");
			}
			this.OpenWaveInDevice();
			this.EnqueueBuffers();
			MmException.Try(WaveInterop.waveInStart(this.waveInHandle), "waveInStart");
			this.recording = true;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00017724 File Offset: 0x00015924
		private void EnqueueBuffers()
		{
			foreach (WaveInBuffer waveInBuffer in this.buffers)
			{
				if (!waveInBuffer.InQueue)
				{
					waveInBuffer.Reuse();
				}
			}
		}

		/// <summary>
		/// Stop recording
		/// </summary>
		// Token: 0x06000806 RID: 2054 RVA: 0x00017758 File Offset: 0x00015958
		public void StopRecording()
		{
			if (this.recording)
			{
				this.recording = false;
				MmException.Try(WaveInterop.waveInStop(this.waveInHandle), "waveInStop");
				for (int i = 0; i < this.buffers.Length; i++)
				{
					int num = (i + this.lastReturnedBufferIndex + 1) % this.buffers.Length;
					WaveInBuffer waveInBuffer = this.buffers[num];
					if (waveInBuffer.Done)
					{
						this.RaiseDataAvailable(waveInBuffer);
					}
				}
				this.RaiseRecordingStopped(null);
			}
		}

		/// <summary>
		/// WaveFormat we are recording in
		/// </summary>
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x000177D3 File Offset: 0x000159D3
		// (set) Token: 0x06000808 RID: 2056 RVA: 0x000177DB File Offset: 0x000159DB
		public WaveFormat WaveFormat { get; set; }

		/// <summary>
		/// Dispose pattern
		/// </summary>
		// Token: 0x06000809 RID: 2057 RVA: 0x000177E4 File Offset: 0x000159E4
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.recording)
				{
					this.StopRecording();
				}
				this.CloseWaveInDevice();
				if (this.callbackInfo != null)
				{
					this.callbackInfo.Disconnect();
					this.callbackInfo = null;
				}
			}
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0001781C File Offset: 0x00015A1C
		private void CloseWaveInDevice()
		{
			if (this.waveInHandle == IntPtr.Zero)
			{
				return;
			}
			WaveInterop.waveInReset(this.waveInHandle);
			if (this.buffers != null)
			{
				for (int i = 0; i < this.buffers.Length; i++)
				{
					this.buffers[i].Dispose();
				}
				this.buffers = null;
			}
			WaveInterop.waveInClose(this.waveInHandle);
			this.waveInHandle = IntPtr.Zero;
		}

		/// <summary>
		/// Microphone Level
		/// </summary>
		// Token: 0x0600080B RID: 2059 RVA: 0x00017890 File Offset: 0x00015A90
		public MixerLine GetMixerLine()
		{
			MixerLine result;
			if (this.waveInHandle != IntPtr.Zero)
			{
				result = new MixerLine(this.waveInHandle, 0, MixerFlags.WaveInHandle);
			}
			else
			{
				result = new MixerLine((IntPtr)this.DeviceNumber, 0, MixerFlags.WaveIn);
			}
			return result;
		}

		/// <summary>
		/// Dispose method
		/// </summary>
		// Token: 0x0600080C RID: 2060 RVA: 0x000178DB File Offset: 0x00015ADB
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000932 RID: 2354
		private IntPtr waveInHandle;

		// Token: 0x04000933 RID: 2355
		private volatile bool recording;

		// Token: 0x04000934 RID: 2356
		private WaveInBuffer[] buffers;

		// Token: 0x04000935 RID: 2357
		private readonly WaveInterop.WaveCallback callback;

		// Token: 0x04000936 RID: 2358
		private WaveCallbackInfo callbackInfo;

		// Token: 0x04000937 RID: 2359
		private readonly SynchronizationContext syncContext;

		// Token: 0x04000938 RID: 2360
		private int lastReturnedBufferIndex;
	}
}
