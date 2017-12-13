using System;
using System.Runtime.InteropServices;
using System.Threading;
using NAudio.Mixer;

namespace NAudio.Wave
{
	/// <summary>
	/// Recording using waveIn api with event callbacks.
	/// Use this for recording in non-gui applications
	/// Events are raised as recorded buffers are made available
	/// </summary>
	// Token: 0x02000081 RID: 129
	public class WaveInEvent : IWaveIn, IDisposable
	{
		/// <summary>
		/// Indicates recorded data is available 
		/// </summary>
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060002B6 RID: 694 RVA: 0x000090D4 File Offset: 0x000072D4
		// (remove) Token: 0x060002B7 RID: 695 RVA: 0x0000910C File Offset: 0x0000730C
		public event EventHandler<WaveInEventArgs> DataAvailable;

		/// <summary>
		/// Indicates that all recorded data has now been received.
		/// </summary>
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060002B8 RID: 696 RVA: 0x00009144 File Offset: 0x00007344
		// (remove) Token: 0x060002B9 RID: 697 RVA: 0x0000917C File Offset: 0x0000737C
		public event EventHandler<StoppedEventArgs> RecordingStopped;

		/// <summary>
		/// Prepares a Wave input device for recording
		/// </summary>
		// Token: 0x060002BA RID: 698 RVA: 0x000091B4 File Offset: 0x000073B4
		public WaveInEvent()
		{
			this.callbackEvent = new AutoResetEvent(false);
			this.syncContext = SynchronizationContext.Current;
			this.DeviceNumber = 0;
			this.WaveFormat = new WaveFormat(8000, 16, 1);
			this.BufferMilliseconds = 100;
			this.NumberOfBuffers = 3;
		}

		/// <summary>
		/// Returns the number of Wave In devices available in the system
		/// </summary>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00009207 File Offset: 0x00007407
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
		// Token: 0x060002BC RID: 700 RVA: 0x00009210 File Offset: 0x00007410
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
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000924A File Offset: 0x0000744A
		// (set) Token: 0x060002BE RID: 702 RVA: 0x00009252 File Offset: 0x00007452
		public int BufferMilliseconds { get; set; }

		/// <summary>
		/// Number of Buffers to use (usually 2 or 3)
		/// </summary>
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000925B File Offset: 0x0000745B
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x00009263 File Offset: 0x00007463
		public int NumberOfBuffers { get; set; }

		/// <summary>
		/// The device number to use
		/// </summary>
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000926C File Offset: 0x0000746C
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x00009274 File Offset: 0x00007474
		public int DeviceNumber { get; set; }

		// Token: 0x060002C3 RID: 707 RVA: 0x00009280 File Offset: 0x00007480
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

		// Token: 0x060002C4 RID: 708 RVA: 0x00009300 File Offset: 0x00007500
		private void OpenWaveInDevice()
		{
			this.CloseWaveInDevice();
			MmResult result = WaveInterop.waveInOpenWindow(out this.waveInHandle, (IntPtr)this.DeviceNumber, this.WaveFormat, this.callbackEvent.SafeWaitHandle.DangerousGetHandle(), IntPtr.Zero, WaveInterop.WaveInOutOpenFlags.CallbackEvent);
			MmException.Try(result, "waveInOpen");
			this.CreateBuffers();
		}

		/// <summary>
		/// Start recording
		/// </summary>
		// Token: 0x060002C5 RID: 709 RVA: 0x00009364 File Offset: 0x00007564
		public void StartRecording()
		{
			if (this.recording)
			{
				throw new InvalidOperationException("Already recording");
			}
			this.OpenWaveInDevice();
			MmException.Try(WaveInterop.waveInStart(this.waveInHandle), "waveInStart");
			this.recording = true;
			ThreadPool.QueueUserWorkItem(delegate(object state)
			{
				this.RecordThread();
			}, null);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000093C0 File Offset: 0x000075C0
		private void RecordThread()
		{
			Exception e = null;
			try
			{
				this.DoRecording();
			}
			catch (Exception ex)
			{
				e = ex;
			}
			finally
			{
				this.recording = false;
				this.RaiseRecordingStoppedEvent(e);
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000940C File Offset: 0x0000760C
		private void DoRecording()
		{
			foreach (WaveInBuffer waveInBuffer in this.buffers)
			{
				if (!waveInBuffer.InQueue)
				{
					waveInBuffer.Reuse();
				}
			}
			while (this.recording)
			{
				if (this.callbackEvent.WaitOne() && this.recording)
				{
					foreach (WaveInBuffer waveInBuffer2 in this.buffers)
					{
						if (waveInBuffer2.Done)
						{
							if (this.DataAvailable != null)
							{
								this.DataAvailable(this, new WaveInEventArgs(waveInBuffer2.Data, waveInBuffer2.BytesRecorded));
							}
							waveInBuffer2.Reuse();
						}
					}
				}
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000094E0 File Offset: 0x000076E0
		private void RaiseRecordingStoppedEvent(Exception e)
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

		/// <summary>
		/// Stop recording
		/// </summary>
		// Token: 0x060002C9 RID: 713 RVA: 0x00009554 File Offset: 0x00007754
		public void StopRecording()
		{
			this.recording = false;
			this.callbackEvent.Set();
			MmException.Try(WaveInterop.waveInStop(this.waveInHandle), "waveInStop");
		}

		/// <summary>
		/// WaveFormat we are recording in
		/// </summary>
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00009580 File Offset: 0x00007780
		// (set) Token: 0x060002CB RID: 715 RVA: 0x00009588 File Offset: 0x00007788
		public WaveFormat WaveFormat { get; set; }

		/// <summary>
		/// Dispose pattern
		/// </summary>
		// Token: 0x060002CC RID: 716 RVA: 0x00009591 File Offset: 0x00007791
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.recording)
				{
					this.StopRecording();
				}
				this.CloseWaveInDevice();
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x000095AC File Offset: 0x000077AC
		private void CloseWaveInDevice()
		{
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
		// Token: 0x060002CE RID: 718 RVA: 0x0000960C File Offset: 0x0000780C
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
		// Token: 0x060002CF RID: 719 RVA: 0x00009657 File Offset: 0x00007857
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x040003B1 RID: 945
		private readonly AutoResetEvent callbackEvent;

		// Token: 0x040003B2 RID: 946
		private readonly SynchronizationContext syncContext;

		// Token: 0x040003B3 RID: 947
		private IntPtr waveInHandle;

		// Token: 0x040003B4 RID: 948
		private volatile bool recording;

		// Token: 0x040003B5 RID: 949
		private WaveInBuffer[] buffers;
	}
}
