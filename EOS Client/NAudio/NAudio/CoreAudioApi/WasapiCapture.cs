using System;
using System.Runtime.InteropServices;
using System.Threading;
using NAudio.Wave;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Audio Capture using Wasapi
	/// See http://msdn.microsoft.com/en-us/library/dd370800%28VS.85%29.aspx
	/// </summary>
	// Token: 0x02000082 RID: 130
	public class WasapiCapture : IWaveIn, IDisposable
	{
		/// <summary>
		/// Indicates recorded data is available 
		/// </summary>
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060002D1 RID: 721 RVA: 0x00009668 File Offset: 0x00007868
		// (remove) Token: 0x060002D2 RID: 722 RVA: 0x000096A0 File Offset: 0x000078A0
		public event EventHandler<WaveInEventArgs> DataAvailable;

		/// <summary>
		/// Indicates that all recorded data has now been received.
		/// </summary>
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060002D3 RID: 723 RVA: 0x000096D8 File Offset: 0x000078D8
		// (remove) Token: 0x060002D4 RID: 724 RVA: 0x00009710 File Offset: 0x00007910
		public event EventHandler<StoppedEventArgs> RecordingStopped;

		/// <summary>
		/// Initialises a new instance of the WASAPI capture class
		/// </summary>
		// Token: 0x060002D5 RID: 725 RVA: 0x00009745 File Offset: 0x00007945
		public WasapiCapture() : this(WasapiCapture.GetDefaultCaptureDevice())
		{
		}

		/// <summary>
		/// Initialises a new instance of the WASAPI capture class
		/// </summary>
		/// <param name="captureDevice">Capture device to use</param>
		// Token: 0x060002D6 RID: 726 RVA: 0x00009752 File Offset: 0x00007952
		public WasapiCapture(MMDevice captureDevice) : this(captureDevice, false)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NAudio.CoreAudioApi.WasapiCapture" /> class.
		/// </summary>
		/// <param name="captureDevice">The capture device.</param>
		/// <param name="useEventSync">true if sync is done with event. false use sleep.</param>
		// Token: 0x060002D7 RID: 727 RVA: 0x0000975C File Offset: 0x0000795C
		public WasapiCapture(MMDevice captureDevice, bool useEventSync)
		{
			this.syncContext = SynchronizationContext.Current;
			this.audioClient = captureDevice.AudioClient;
			this.ShareMode = AudioClientShareMode.Shared;
			this.isUsingEventSync = useEventSync;
			this.waveFormat = this.audioClient.MixFormat;
		}

		/// <summary>
		/// Share Mode - set before calling StartRecording
		/// </summary>
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000979A File Offset: 0x0000799A
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x000097A2 File Offset: 0x000079A2
		public AudioClientShareMode ShareMode { get; set; }

		/// <summary>
		/// Recording wave format
		/// </summary>
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002DA RID: 730 RVA: 0x000097AC File Offset: 0x000079AC
		// (set) Token: 0x060002DB RID: 731 RVA: 0x000097EC File Offset: 0x000079EC
		public virtual WaveFormat WaveFormat
		{
			get
			{
				WaveFormatExtensible waveFormatExtensible = this.waveFormat as WaveFormatExtensible;
				if (waveFormatExtensible != null)
				{
					try
					{
						return waveFormatExtensible.ToStandardWaveFormat();
					}
					catch (InvalidOperationException)
					{
					}
				}
				return this.waveFormat;
			}
			set
			{
				this.waveFormat = value;
			}
		}

		/// <summary>
		/// Gets the default audio capture device
		/// </summary>
		/// <returns>The default audio capture device</returns>
		// Token: 0x060002DC RID: 732 RVA: 0x000097F8 File Offset: 0x000079F8
		public static MMDevice GetDefaultCaptureDevice()
		{
			MMDeviceEnumerator mmdeviceEnumerator = new MMDeviceEnumerator();
			return mmdeviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00009814 File Offset: 0x00007A14
		private void InitializeCaptureDevice()
		{
			if (this.initialized)
			{
				return;
			}
			long num = 1000000L;
			if (!this.audioClient.IsFormatSupported(this.ShareMode, this.waveFormat))
			{
				throw new ArgumentException("Unsupported Wave Format");
			}
			AudioClientStreamFlags audioClientStreamFlags = this.GetAudioClientStreamFlags();
			if (this.isUsingEventSync)
			{
				if (this.ShareMode == AudioClientShareMode.Shared)
				{
					this.audioClient.Initialize(this.ShareMode, AudioClientStreamFlags.EventCallback, num, 0L, this.waveFormat, Guid.Empty);
				}
				else
				{
					this.audioClient.Initialize(this.ShareMode, AudioClientStreamFlags.EventCallback, num, num, this.waveFormat, Guid.Empty);
				}
				this.frameEventWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
				this.audioClient.SetEventHandle(this.frameEventWaitHandle.SafeWaitHandle.DangerousGetHandle());
			}
			else
			{
				this.audioClient.Initialize(this.ShareMode, audioClientStreamFlags, num, 0L, this.waveFormat, Guid.Empty);
			}
			int bufferSize = this.audioClient.BufferSize;
			this.bytesPerFrame = this.waveFormat.Channels * this.waveFormat.BitsPerSample / 8;
			this.recordBuffer = new byte[bufferSize * this.bytesPerFrame];
			this.initialized = true;
		}

		/// <summary>
		/// To allow overrides to specify different flags (e.g. loopback)
		/// </summary>
		// Token: 0x060002DE RID: 734 RVA: 0x00009944 File Offset: 0x00007B44
		protected virtual AudioClientStreamFlags GetAudioClientStreamFlags()
		{
			return AudioClientStreamFlags.None;
		}

		/// <summary>
		/// Start Recording
		/// </summary>
		// Token: 0x060002DF RID: 735 RVA: 0x00009958 File Offset: 0x00007B58
		public void StartRecording()
		{
			if (this.captureThread != null)
			{
				throw new InvalidOperationException("Previous recording still in progress");
			}
			this.InitializeCaptureDevice();
			ThreadStart start = delegate
			{
				this.CaptureThread(this.audioClient);
			};
			this.captureThread = new Thread(start);
			this.requestStop = false;
			this.captureThread.Start();
		}

		/// <summary>
		/// Stop Recording (requests a stop, wait for RecordingStopped event to know it has finished)
		/// </summary>
		// Token: 0x060002E0 RID: 736 RVA: 0x000099AB File Offset: 0x00007BAB
		public void StopRecording()
		{
			this.requestStop = true;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000099B8 File Offset: 0x00007BB8
		private void CaptureThread(AudioClient client)
		{
			Exception e = null;
			try
			{
				this.DoRecording(client);
			}
			catch (Exception ex)
			{
				e = ex;
			}
			finally
			{
				client.Stop();
			}
			this.captureThread = null;
			this.RaiseRecordingStopped(e);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00009A08 File Offset: 0x00007C08
		private void DoRecording(AudioClient client)
		{
			int bufferSize = client.BufferSize;
			long num = (long)(10000000.0 * (double)bufferSize / (double)this.waveFormat.SampleRate);
			int millisecondsTimeout = (int)(num / 10000L / 2L);
			int millisecondsTimeout2 = (int)(3L * num / 10000L);
			AudioCaptureClient audioCaptureClient = client.AudioCaptureClient;
			client.Start();
			if (this.isUsingEventSync)
			{
			}
			while (!this.requestStop)
			{
				bool flag = true;
				if (this.isUsingEventSync)
				{
					flag = this.frameEventWaitHandle.WaitOne(millisecondsTimeout2, false);
				}
				else
				{
					Thread.Sleep(millisecondsTimeout);
				}
				if (!this.requestStop && flag)
				{
					this.ReadNextPacket(audioCaptureClient);
				}
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00009AD4 File Offset: 0x00007CD4
		private void RaiseRecordingStopped(Exception e)
		{
			EventHandler<StoppedEventArgs> handler = this.RecordingStopped;
			if (handler == null)
			{
				return;
			}
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

		// Token: 0x060002E4 RID: 740 RVA: 0x00009B4C File Offset: 0x00007D4C
		private void ReadNextPacket(AudioCaptureClient capture)
		{
			int nextPacketSize = capture.GetNextPacketSize();
			int num = 0;
			while (nextPacketSize != 0)
			{
				int num2;
				AudioClientBufferFlags audioClientBufferFlags;
				IntPtr buffer = capture.GetBuffer(out num2, out audioClientBufferFlags);
				int num3 = num2 * this.bytesPerFrame;
				int num4 = Math.Max(0, this.recordBuffer.Length - num);
				if (num4 < num3 && num > 0)
				{
					if (this.DataAvailable != null)
					{
						this.DataAvailable(this, new WaveInEventArgs(this.recordBuffer, num));
					}
					num = 0;
				}
				if ((audioClientBufferFlags & AudioClientBufferFlags.Silent) != AudioClientBufferFlags.Silent)
				{
					Marshal.Copy(buffer, this.recordBuffer, num, num3);
				}
				else
				{
					Array.Clear(this.recordBuffer, num, num3);
				}
				num += num3;
				capture.ReleaseBuffer(num2);
				nextPacketSize = capture.GetNextPacketSize();
			}
			if (this.DataAvailable != null)
			{
				this.DataAvailable(this, new WaveInEventArgs(this.recordBuffer, num));
			}
		}

		/// <summary>
		/// Dispose
		/// </summary>
		// Token: 0x060002E5 RID: 741 RVA: 0x00009C1A File Offset: 0x00007E1A
		public void Dispose()
		{
			this.StopRecording();
			if (this.captureThread != null)
			{
				this.captureThread.Join();
				this.captureThread = null;
			}
			if (this.audioClient != null)
			{
				this.audioClient.Dispose();
				this.audioClient = null;
			}
		}

		// Token: 0x040003BC RID: 956
		private const long REFTIMES_PER_SEC = 10000000L;

		// Token: 0x040003BD RID: 957
		private const long REFTIMES_PER_MILLISEC = 10000L;

		// Token: 0x040003BE RID: 958
		private volatile bool requestStop;

		// Token: 0x040003BF RID: 959
		private byte[] recordBuffer;

		// Token: 0x040003C0 RID: 960
		private Thread captureThread;

		// Token: 0x040003C1 RID: 961
		private AudioClient audioClient;

		// Token: 0x040003C2 RID: 962
		private int bytesPerFrame;

		// Token: 0x040003C3 RID: 963
		private WaveFormat waveFormat;

		// Token: 0x040003C4 RID: 964
		private bool initialized;

		// Token: 0x040003C5 RID: 965
		private readonly SynchronizationContext syncContext;

		// Token: 0x040003C6 RID: 966
		private readonly bool isUsingEventSync;

		// Token: 0x040003C7 RID: 967
		private EventWaitHandle frameEventWaitHandle;
	}
}
