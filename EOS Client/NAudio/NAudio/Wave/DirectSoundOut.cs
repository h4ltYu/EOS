using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace NAudio.Wave
{
	/// <summary>
	/// NativeDirectSoundOut using DirectSound COM interop.
	/// Contact author: Alexandre Mutel - alexandre_mutel at yahoo.fr
	/// Modified by: Graham "Gee" Plumb
	/// </summary>
	// Token: 0x020001BA RID: 442
	public class DirectSoundOut : IWavePlayer, IDisposable
	{
		/// <summary>
		/// Playback Stopped
		/// </summary>
		// Token: 0x14000013 RID: 19
		// (add) Token: 0x0600096C RID: 2412 RVA: 0x0001B33C File Offset: 0x0001953C
		// (remove) Token: 0x0600096D RID: 2413 RVA: 0x0001B374 File Offset: 0x00019574
		public event EventHandler<StoppedEventArgs> PlaybackStopped;

		/// <summary>
		/// Gets the DirectSound output devices in the system
		/// </summary>
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x0001B3A9 File Offset: 0x000195A9
		public static IEnumerable<DirectSoundDeviceInfo> Devices
		{
			get
			{
				DirectSoundOut.devices = new List<DirectSoundDeviceInfo>();
				DirectSoundOut.DirectSoundEnumerate(new DirectSoundOut.DSEnumCallback(DirectSoundOut.EnumCallback), IntPtr.Zero);
				return DirectSoundOut.devices;
			}
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0001B3D0 File Offset: 0x000195D0
		private static bool EnumCallback(IntPtr lpGuid, IntPtr lpcstrDescription, IntPtr lpcstrModule, IntPtr lpContext)
		{
			DirectSoundDeviceInfo directSoundDeviceInfo = new DirectSoundDeviceInfo();
			if (lpGuid == IntPtr.Zero)
			{
				directSoundDeviceInfo.Guid = Guid.Empty;
			}
			else
			{
				byte[] array = new byte[16];
				Marshal.Copy(lpGuid, array, 0, 16);
				directSoundDeviceInfo.Guid = new Guid(array);
			}
			directSoundDeviceInfo.Description = Marshal.PtrToStringAnsi(lpcstrDescription);
			directSoundDeviceInfo.ModuleName = Marshal.PtrToStringAnsi(lpcstrModule);
			DirectSoundOut.devices.Add(directSoundDeviceInfo);
			return true;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NAudio.Wave.DirectSoundOut" /> class.
		/// </summary>
		// Token: 0x06000970 RID: 2416 RVA: 0x0001B43F File Offset: 0x0001963F
		public DirectSoundOut() : this(DirectSoundOut.DSDEVID_DefaultPlayback)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NAudio.Wave.DirectSoundOut" /> class.
		/// </summary>
		// Token: 0x06000971 RID: 2417 RVA: 0x0001B44C File Offset: 0x0001964C
		public DirectSoundOut(Guid device) : this(device, 40)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NAudio.Wave.DirectSoundOut" /> class.
		/// </summary>
		// Token: 0x06000972 RID: 2418 RVA: 0x0001B457 File Offset: 0x00019657
		public DirectSoundOut(int latency) : this(DirectSoundOut.DSDEVID_DefaultPlayback, latency)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NAudio.Wave.DirectSoundOut" /> class.
		/// (40ms seems to work under Vista).
		/// </summary>
		/// <param name="latency">The latency.</param>
		/// <param name="device">Selected device</param>
		// Token: 0x06000973 RID: 2419 RVA: 0x0001B465 File Offset: 0x00019665
		public DirectSoundOut(Guid device, int latency)
		{
			if (device == Guid.Empty)
			{
				device = DirectSoundOut.DSDEVID_DefaultPlayback;
			}
			this.device = device;
			this.desiredLatency = latency;
			this.syncContext = SynchronizationContext.Current;
		}

		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the
		/// <see cref="T:NAudio.Wave.DirectSoundOut" /> is reclaimed by garbage collection.
		/// </summary>
		// Token: 0x06000974 RID: 2420 RVA: 0x0001B4A8 File Offset: 0x000196A8
		~DirectSoundOut()
		{
			this.Dispose();
		}

		/// <summary>
		/// Begin playback
		/// </summary>
		// Token: 0x06000975 RID: 2421 RVA: 0x0001B4D4 File Offset: 0x000196D4
		public void Play()
		{
			if (this.playbackState == PlaybackState.Stopped)
			{
				this.notifyThread = new Thread(new ThreadStart(this.PlaybackThreadFunc));
				this.notifyThread.Priority = ThreadPriority.Normal;
				this.notifyThread.IsBackground = true;
				this.notifyThread.Start();
			}
			lock (this.m_LockObject)
			{
				this.playbackState = PlaybackState.Playing;
			}
		}

		/// <summary>
		/// Stop playback
		/// </summary>
		// Token: 0x06000976 RID: 2422 RVA: 0x0001B550 File Offset: 0x00019750
		public void Stop()
		{
			if (Monitor.TryEnter(this.m_LockObject, 50))
			{
				this.playbackState = PlaybackState.Stopped;
				Monitor.Exit(this.m_LockObject);
				return;
			}
			if (this.notifyThread != null)
			{
				this.notifyThread.Abort();
				this.notifyThread = null;
			}
		}

		/// <summary>
		/// Pause Playback
		/// </summary>
		// Token: 0x06000977 RID: 2423 RVA: 0x0001B590 File Offset: 0x00019790
		public void Pause()
		{
			lock (this.m_LockObject)
			{
				this.playbackState = PlaybackState.Paused;
			}
		}

		/// <summary>
		/// Gets the current position in bytes from the wave output device.
		/// (n.b. this is not the same thing as the position within your reader
		/// stream)
		/// </summary>
		/// <returns>Position in bytes</returns>
		// Token: 0x06000978 RID: 2424 RVA: 0x0001B5CC File Offset: 0x000197CC
		public long GetPosition()
		{
			if (this.playbackState != PlaybackState.Stopped)
			{
				DirectSoundOut.IDirectSoundBuffer directSoundBuffer = this.secondaryBuffer;
				if (directSoundBuffer != null)
				{
					uint num;
					uint num2;
					directSoundBuffer.GetCurrentPosition(out num, out num2);
					return (long)((ulong)num + (ulong)this.bytesPlayed);
				}
			}
			return 0L;
		}

		/// <summary>
		/// Gets the current position from the wave output device.
		/// </summary>
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x0001B604 File Offset: 0x00019804
		public TimeSpan PlaybackPosition
		{
			get
			{
				long num = this.GetPosition();
				num /= (long)(this.waveFormat.Channels * this.waveFormat.BitsPerSample / 8);
				return TimeSpan.FromMilliseconds((double)num * 1000.0 / (double)this.waveFormat.SampleRate);
			}
		}

		/// <summary>
		/// Initialise playback
		/// </summary>
		/// <param name="waveProvider">The waveprovider to be played</param>
		// Token: 0x0600097A RID: 2426 RVA: 0x0001B653 File Offset: 0x00019853
		public void Init(IWaveProvider waveProvider)
		{
			this.waveStream = waveProvider;
			this.waveFormat = waveProvider.WaveFormat;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0001B668 File Offset: 0x00019868
		private void InitializeDirectSound()
		{
			lock (this.m_LockObject)
			{
				this.directSound = null;
				DirectSoundOut.DirectSoundCreate(ref this.device, out this.directSound, IntPtr.Zero);
				if (this.directSound != null)
				{
					this.directSound.SetCooperativeLevel(DirectSoundOut.GetDesktopWindow(), DirectSoundOut.DirectSoundCooperativeLevel.DSSCL_PRIORITY);
					DirectSoundOut.BufferDescription bufferDescription = new DirectSoundOut.BufferDescription();
					bufferDescription.dwSize = Marshal.SizeOf(bufferDescription);
					bufferDescription.dwBufferBytes = 0u;
					bufferDescription.dwFlags = DirectSoundOut.DirectSoundBufferCaps.DSBCAPS_PRIMARYBUFFER;
					bufferDescription.dwReserved = 0;
					bufferDescription.lpwfxFormat = IntPtr.Zero;
					bufferDescription.guidAlgo = Guid.Empty;
					object obj;
					this.directSound.CreateSoundBuffer(bufferDescription, out obj, IntPtr.Zero);
					this.primarySoundBuffer = (DirectSoundOut.IDirectSoundBuffer)obj;
					this.primarySoundBuffer.Play(0u, 0u, DirectSoundOut.DirectSoundPlayFlags.DSBPLAY_LOOPING);
					this.samplesFrameSize = this.MsToBytes(this.desiredLatency);
					DirectSoundOut.BufferDescription bufferDescription2 = new DirectSoundOut.BufferDescription();
					bufferDescription2.dwSize = Marshal.SizeOf(bufferDescription2);
					bufferDescription2.dwBufferBytes = (uint)(this.samplesFrameSize * 2);
					bufferDescription2.dwFlags = (DirectSoundOut.DirectSoundBufferCaps.DSBCAPS_CTRLVOLUME | DirectSoundOut.DirectSoundBufferCaps.DSBCAPS_CTRLPOSITIONNOTIFY | DirectSoundOut.DirectSoundBufferCaps.DSBCAPS_STICKYFOCUS | DirectSoundOut.DirectSoundBufferCaps.DSBCAPS_GLOBALFOCUS | DirectSoundOut.DirectSoundBufferCaps.DSBCAPS_GETCURRENTPOSITION2);
					bufferDescription2.dwReserved = 0;
					GCHandle gchandle = GCHandle.Alloc(this.waveFormat, GCHandleType.Pinned);
					bufferDescription2.lpwfxFormat = gchandle.AddrOfPinnedObject();
					bufferDescription2.guidAlgo = Guid.Empty;
					this.directSound.CreateSoundBuffer(bufferDescription2, out obj, IntPtr.Zero);
					this.secondaryBuffer = (DirectSoundOut.IDirectSoundBuffer)obj;
					gchandle.Free();
					DirectSoundOut.BufferCaps bufferCaps = new DirectSoundOut.BufferCaps();
					bufferCaps.dwSize = Marshal.SizeOf(bufferCaps);
					this.secondaryBuffer.GetCaps(bufferCaps);
					this.nextSamplesWriteIndex = 0;
					this.samplesTotalSize = bufferCaps.dwBufferBytes;
					this.samples = new byte[this.samplesTotalSize];
					DirectSoundOut.IDirectSoundNotify directSoundNotify = (DirectSoundOut.IDirectSoundNotify)obj;
					this.frameEventWaitHandle1 = new EventWaitHandle(false, EventResetMode.AutoReset);
					this.frameEventWaitHandle2 = new EventWaitHandle(false, EventResetMode.AutoReset);
					this.endEventWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
					DirectSoundOut.DirectSoundBufferPositionNotify[] array = new DirectSoundOut.DirectSoundBufferPositionNotify[3];
					array[0] = default(DirectSoundOut.DirectSoundBufferPositionNotify);
					array[0].dwOffset = 0u;
					array[0].hEventNotify = this.frameEventWaitHandle1.SafeWaitHandle.DangerousGetHandle();
					array[1] = default(DirectSoundOut.DirectSoundBufferPositionNotify);
					array[1].dwOffset = (uint)this.samplesFrameSize;
					array[1].hEventNotify = this.frameEventWaitHandle2.SafeWaitHandle.DangerousGetHandle();
					array[2] = default(DirectSoundOut.DirectSoundBufferPositionNotify);
					array[2].dwOffset = uint.MaxValue;
					array[2].hEventNotify = this.endEventWaitHandle.SafeWaitHandle.DangerousGetHandle();
					directSoundNotify.SetNotificationPositions(3u, array);
				}
			}
		}

		/// <summary>
		/// Current playback state
		/// </summary>
		/// <value></value>
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x0001B914 File Offset: 0x00019B14
		public PlaybackState PlaybackState
		{
			get
			{
				return this.playbackState;
			}
		}

		/// <summary>
		/// The volume 1.0 is full scale
		/// </summary>
		/// <value></value>
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x0001B91C File Offset: 0x00019B1C
		// (set) Token: 0x0600097E RID: 2430 RVA: 0x0001B923 File Offset: 0x00019B23
		public float Volume
		{
			get
			{
				return 1f;
			}
			set
			{
				if (value != 1f)
				{
					throw new InvalidOperationException("Setting volume not supported on DirectSoundOut, adjust the volume on your WaveProvider instead");
				}
			}
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		// Token: 0x0600097F RID: 2431 RVA: 0x0001B938 File Offset: 0x00019B38
		public void Dispose()
		{
			this.Stop();
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Determines whether the SecondaryBuffer is lost.
		/// </summary>
		/// <returns>
		/// 	<c>true</c> if [is buffer lost]; otherwise, <c>false</c>.
		/// </returns>
		// Token: 0x06000980 RID: 2432 RVA: 0x0001B946 File Offset: 0x00019B46
		private bool IsBufferLost()
		{
			return (this.secondaryBuffer.GetStatus() & DirectSoundOut.DirectSoundBufferStatus.DSBSTATUS_BUFFERLOST) != (DirectSoundOut.DirectSoundBufferStatus)0u;
		}

		/// <summary>
		/// Convert ms to bytes size according to WaveFormat
		/// </summary>
		/// <param name="ms">The ms</param>
		/// <returns>number of byttes</returns>
		// Token: 0x06000981 RID: 2433 RVA: 0x0001B95C File Offset: 0x00019B5C
		private int MsToBytes(int ms)
		{
			int num = ms * (this.waveFormat.AverageBytesPerSecond / 1000);
			return num - num % this.waveFormat.BlockAlign;
		}

		/// <summary>
		/// Processes the samples in a separate thread.
		/// </summary>
		// Token: 0x06000982 RID: 2434 RVA: 0x0001B990 File Offset: 0x00019B90
		private void PlaybackThreadFunc()
		{
			bool flag = false;
			bool flag2 = false;
			this.bytesPlayed = 0L;
			Exception ex = null;
			try
			{
				this.InitializeDirectSound();
				int num = 1;
				if (this.PlaybackState == PlaybackState.Stopped)
				{
					this.secondaryBuffer.SetCurrentPosition(0u);
					this.nextSamplesWriteIndex = 0;
					num = this.Feed(this.samplesTotalSize);
				}
				if (num > 0)
				{
					lock (this.m_LockObject)
					{
						this.playbackState = PlaybackState.Playing;
					}
					this.secondaryBuffer.Play(0u, 0u, DirectSoundOut.DirectSoundPlayFlags.DSBPLAY_LOOPING);
					WaitHandle[] waitHandles = new WaitHandle[]
					{
						this.frameEventWaitHandle1,
						this.frameEventWaitHandle2,
						this.endEventWaitHandle
					};
					bool flag3 = true;
					while (this.PlaybackState != PlaybackState.Stopped && flag3)
					{
						int num2 = WaitHandle.WaitAny(waitHandles, 3 * this.desiredLatency, false);
						if (num2 == 258)
						{
							this.StopPlayback();
							flag = true;
							throw new Exception("DirectSound buffer timeout");
						}
						if (num2 == 2)
						{
							this.StopPlayback();
							flag = true;
							flag3 = false;
						}
						else
						{
							if (num2 == 0)
							{
								if (flag2)
								{
									this.bytesPlayed += (long)(this.samplesFrameSize * 2);
								}
							}
							else
							{
								flag2 = true;
							}
							num2 = ((num2 == 0) ? 1 : 0);
							this.nextSamplesWriteIndex = num2 * this.samplesFrameSize;
							if (this.Feed(this.samplesFrameSize) == 0)
							{
								this.StopPlayback();
								flag = true;
								flag3 = false;
							}
						}
					}
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			finally
			{
				if (!flag)
				{
					try
					{
						this.StopPlayback();
					}
					catch (Exception ex3)
					{
						if (ex == null)
						{
							ex = ex3;
						}
					}
				}
				lock (this.m_LockObject)
				{
					this.playbackState = PlaybackState.Stopped;
				}
				this.bytesPlayed = 0L;
				this.RaisePlaybackStopped(ex);
			}
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0001BBDC File Offset: 0x00019DDC
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

		/// <summary>
		/// Stop playback
		/// </summary>
		// Token: 0x06000984 RID: 2436 RVA: 0x0001BC50 File Offset: 0x00019E50
		private void StopPlayback()
		{
			lock (this.m_LockObject)
			{
				if (this.secondaryBuffer != null)
				{
					this.secondaryBuffer.Stop();
					this.secondaryBuffer = null;
				}
				if (this.primarySoundBuffer != null)
				{
					this.primarySoundBuffer.Stop();
					this.primarySoundBuffer = null;
				}
			}
		}

		/// <summary>
		/// Feeds the SecondaryBuffer with the WaveStream
		/// </summary>
		/// <param name="bytesToCopy">number of bytes to feed</param>
		// Token: 0x06000985 RID: 2437 RVA: 0x0001BCB8 File Offset: 0x00019EB8
		private int Feed(int bytesToCopy)
		{
			int num = bytesToCopy;
			if (this.IsBufferLost())
			{
				this.secondaryBuffer.Restore();
			}
			if (this.playbackState == PlaybackState.Paused)
			{
				Array.Clear(this.samples, 0, this.samples.Length);
			}
			else
			{
				num = this.waveStream.Read(this.samples, 0, bytesToCopy);
				if (num == 0)
				{
					Array.Clear(this.samples, 0, this.samples.Length);
					return 0;
				}
			}
			IntPtr intPtr;
			int num2;
			IntPtr intPtr2;
			int dwAudioBytes;
			this.secondaryBuffer.Lock(this.nextSamplesWriteIndex, (uint)num, out intPtr, out num2, out intPtr2, out dwAudioBytes, DirectSoundOut.DirectSoundBufferLockFlag.None);
			if (intPtr != IntPtr.Zero)
			{
				Marshal.Copy(this.samples, 0, intPtr, num2);
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.Copy(this.samples, 0, intPtr, num2);
				}
			}
			this.secondaryBuffer.Unlock(intPtr, num2, intPtr2, dwAudioBytes);
			return num;
		}

		/// <summary>
		/// Instanciate DirectSound from the DLL
		/// </summary>
		/// <param name="GUID">The GUID.</param>
		/// <param name="directSound">The direct sound.</param>
		/// <param name="pUnkOuter">The p unk outer.</param>
		// Token: 0x06000986 RID: 2438
		[DllImport("dsound.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		private static extern void DirectSoundCreate(ref Guid GUID, [MarshalAs(UnmanagedType.Interface)] out DirectSoundOut.IDirectSound directSound, IntPtr pUnkOuter);

		/// <summary>
		/// The DirectSoundEnumerate function enumerates the DirectSound drivers installed in the system.
		/// </summary>
		/// <param name="lpDSEnumCallback">callback function</param>
		/// <param name="lpContext">User context</param>
		// Token: 0x06000987 RID: 2439
		[DllImport("dsound.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "DirectSoundEnumerateA", ExactSpelling = true, SetLastError = true)]
		private static extern void DirectSoundEnumerate(DirectSoundOut.DSEnumCallback lpDSEnumCallback, IntPtr lpContext);

		/// <summary>
		/// Gets the HANDLE of the desktop window.
		/// </summary>
		/// <returns>HANDLE of the Desktop window</returns>
		// Token: 0x06000988 RID: 2440
		[DllImport("user32.dll")]
		private static extern IntPtr GetDesktopWindow();

		// Token: 0x04000AAB RID: 2731
		private PlaybackState playbackState;

		// Token: 0x04000AAC RID: 2732
		private WaveFormat waveFormat;

		// Token: 0x04000AAD RID: 2733
		private int samplesTotalSize;

		// Token: 0x04000AAE RID: 2734
		private int samplesFrameSize;

		// Token: 0x04000AAF RID: 2735
		private int nextSamplesWriteIndex;

		// Token: 0x04000AB0 RID: 2736
		private int desiredLatency;

		// Token: 0x04000AB1 RID: 2737
		private Guid device;

		// Token: 0x04000AB2 RID: 2738
		private byte[] samples;

		// Token: 0x04000AB3 RID: 2739
		private IWaveProvider waveStream;

		// Token: 0x04000AB4 RID: 2740
		private DirectSoundOut.IDirectSound directSound;

		// Token: 0x04000AB5 RID: 2741
		private DirectSoundOut.IDirectSoundBuffer primarySoundBuffer;

		// Token: 0x04000AB6 RID: 2742
		private DirectSoundOut.IDirectSoundBuffer secondaryBuffer;

		// Token: 0x04000AB7 RID: 2743
		private EventWaitHandle frameEventWaitHandle1;

		// Token: 0x04000AB8 RID: 2744
		private EventWaitHandle frameEventWaitHandle2;

		// Token: 0x04000AB9 RID: 2745
		private EventWaitHandle endEventWaitHandle;

		// Token: 0x04000ABA RID: 2746
		private Thread notifyThread;

		// Token: 0x04000ABB RID: 2747
		private SynchronizationContext syncContext;

		// Token: 0x04000ABC RID: 2748
		private long bytesPlayed;

		// Token: 0x04000ABD RID: 2749
		private object m_LockObject = new object();

		// Token: 0x04000ABE RID: 2750
		private static List<DirectSoundDeviceInfo> devices;

		/// <summary>
		/// DirectSound default playback device GUID 
		/// </summary>
		// Token: 0x04000ABF RID: 2751
		public static readonly Guid DSDEVID_DefaultPlayback = new Guid("DEF00000-9C6D-47ED-AAF1-4DDA8F2B5C03");

		/// <summary>
		/// DirectSound default capture device GUID
		/// </summary>
		// Token: 0x04000AC0 RID: 2752
		public static readonly Guid DSDEVID_DefaultCapture = new Guid("DEF00001-9C6D-47ED-AAF1-4DDA8F2B5C03");

		/// <summary>
		/// DirectSound default device for voice playback
		/// </summary>
		// Token: 0x04000AC1 RID: 2753
		public static readonly Guid DSDEVID_DefaultVoicePlayback = new Guid("DEF00002-9C6D-47ED-AAF1-4DDA8F2B5C03");

		/// <summary>
		/// DirectSound default device for voice capture
		/// </summary>
		// Token: 0x04000AC2 RID: 2754
		public static readonly Guid DSDEVID_DefaultVoiceCapture = new Guid("DEF00003-9C6D-47ED-AAF1-4DDA8F2B5C03");

		// Token: 0x020001BB RID: 443
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		internal class BufferDescription
		{
			// Token: 0x04000AC3 RID: 2755
			public int dwSize;

			// Token: 0x04000AC4 RID: 2756
			[MarshalAs(UnmanagedType.U4)]
			public DirectSoundOut.DirectSoundBufferCaps dwFlags;

			// Token: 0x04000AC5 RID: 2757
			public uint dwBufferBytes;

			// Token: 0x04000AC6 RID: 2758
			public int dwReserved;

			// Token: 0x04000AC7 RID: 2759
			public IntPtr lpwfxFormat;

			// Token: 0x04000AC8 RID: 2760
			public Guid guidAlgo;
		}

		// Token: 0x020001BC RID: 444
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		internal class BufferCaps
		{
			// Token: 0x04000AC9 RID: 2761
			public int dwSize;

			// Token: 0x04000ACA RID: 2762
			public int dwFlags;

			// Token: 0x04000ACB RID: 2763
			public int dwBufferBytes;

			// Token: 0x04000ACC RID: 2764
			public int dwUnlockTransferRate;

			// Token: 0x04000ACD RID: 2765
			public int dwPlayCpuOverhead;
		}

		// Token: 0x020001BD RID: 445
		internal enum DirectSoundCooperativeLevel : uint
		{
			// Token: 0x04000ACF RID: 2767
			DSSCL_NORMAL = 1u,
			// Token: 0x04000AD0 RID: 2768
			DSSCL_PRIORITY,
			// Token: 0x04000AD1 RID: 2769
			DSSCL_EXCLUSIVE,
			// Token: 0x04000AD2 RID: 2770
			DSSCL_WRITEPRIMARY
		}

		// Token: 0x020001BE RID: 446
		[Flags]
		internal enum DirectSoundPlayFlags : uint
		{
			// Token: 0x04000AD4 RID: 2772
			DSBPLAY_LOOPING = 1u,
			// Token: 0x04000AD5 RID: 2773
			DSBPLAY_LOCHARDWARE = 2u,
			// Token: 0x04000AD6 RID: 2774
			DSBPLAY_LOCSOFTWARE = 4u,
			// Token: 0x04000AD7 RID: 2775
			DSBPLAY_TERMINATEBY_TIME = 8u,
			// Token: 0x04000AD8 RID: 2776
			DSBPLAY_TERMINATEBY_DISTANCE = 16u,
			// Token: 0x04000AD9 RID: 2777
			DSBPLAY_TERMINATEBY_PRIORITY = 32u
		}

		// Token: 0x020001BF RID: 447
		internal enum DirectSoundBufferLockFlag : uint
		{
			// Token: 0x04000ADB RID: 2779
			None,
			// Token: 0x04000ADC RID: 2780
			FromWriteCursor,
			// Token: 0x04000ADD RID: 2781
			EntireBuffer
		}

		// Token: 0x020001C0 RID: 448
		[Flags]
		internal enum DirectSoundBufferStatus : uint
		{
			// Token: 0x04000ADF RID: 2783
			DSBSTATUS_PLAYING = 1u,
			// Token: 0x04000AE0 RID: 2784
			DSBSTATUS_BUFFERLOST = 2u,
			// Token: 0x04000AE1 RID: 2785
			DSBSTATUS_LOOPING = 4u,
			// Token: 0x04000AE2 RID: 2786
			DSBSTATUS_LOCHARDWARE = 8u,
			// Token: 0x04000AE3 RID: 2787
			DSBSTATUS_LOCSOFTWARE = 16u,
			// Token: 0x04000AE4 RID: 2788
			DSBSTATUS_TERMINATED = 32u
		}

		// Token: 0x020001C1 RID: 449
		[Flags]
		internal enum DirectSoundBufferCaps : uint
		{
			// Token: 0x04000AE6 RID: 2790
			DSBCAPS_PRIMARYBUFFER = 1u,
			// Token: 0x04000AE7 RID: 2791
			DSBCAPS_STATIC = 2u,
			// Token: 0x04000AE8 RID: 2792
			DSBCAPS_LOCHARDWARE = 4u,
			// Token: 0x04000AE9 RID: 2793
			DSBCAPS_LOCSOFTWARE = 8u,
			// Token: 0x04000AEA RID: 2794
			DSBCAPS_CTRL3D = 16u,
			// Token: 0x04000AEB RID: 2795
			DSBCAPS_CTRLFREQUENCY = 32u,
			// Token: 0x04000AEC RID: 2796
			DSBCAPS_CTRLPAN = 64u,
			// Token: 0x04000AED RID: 2797
			DSBCAPS_CTRLVOLUME = 128u,
			// Token: 0x04000AEE RID: 2798
			DSBCAPS_CTRLPOSITIONNOTIFY = 256u,
			// Token: 0x04000AEF RID: 2799
			DSBCAPS_CTRLFX = 512u,
			// Token: 0x04000AF0 RID: 2800
			DSBCAPS_STICKYFOCUS = 16384u,
			// Token: 0x04000AF1 RID: 2801
			DSBCAPS_GLOBALFOCUS = 32768u,
			// Token: 0x04000AF2 RID: 2802
			DSBCAPS_GETCURRENTPOSITION2 = 65536u,
			// Token: 0x04000AF3 RID: 2803
			DSBCAPS_MUTE3DATMAXDISTANCE = 131072u,
			// Token: 0x04000AF4 RID: 2804
			DSBCAPS_LOCDEFER = 262144u
		}

		// Token: 0x020001C2 RID: 450
		internal struct DirectSoundBufferPositionNotify
		{
			// Token: 0x04000AF5 RID: 2805
			public uint dwOffset;

			// Token: 0x04000AF6 RID: 2806
			public IntPtr hEventNotify;
		}

		/// <summary>
		/// IDirectSound interface
		/// </summary>
		// Token: 0x020001C3 RID: 451
		[SuppressUnmanagedCodeSecurity]
		[Guid("279AFA83-4981-11CE-A521-0020AF0BE560")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IDirectSound
		{
			// Token: 0x0600098C RID: 2444
			void CreateSoundBuffer([In] DirectSoundOut.BufferDescription desc, [MarshalAs(UnmanagedType.Interface)] out object dsDSoundBuffer, IntPtr pUnkOuter);

			// Token: 0x0600098D RID: 2445
			void GetCaps(IntPtr caps);

			// Token: 0x0600098E RID: 2446
			void DuplicateSoundBuffer([MarshalAs(UnmanagedType.Interface)] [In] DirectSoundOut.IDirectSoundBuffer bufferOriginal, [MarshalAs(UnmanagedType.Interface)] [In] DirectSoundOut.IDirectSoundBuffer bufferDuplicate);

			// Token: 0x0600098F RID: 2447
			void SetCooperativeLevel(IntPtr HWND, [MarshalAs(UnmanagedType.U4)] [In] DirectSoundOut.DirectSoundCooperativeLevel dwLevel);

			// Token: 0x06000990 RID: 2448
			void Compact();

			// Token: 0x06000991 RID: 2449
			void GetSpeakerConfig(IntPtr pdwSpeakerConfig);

			// Token: 0x06000992 RID: 2450
			void SetSpeakerConfig(uint pdwSpeakerConfig);

			// Token: 0x06000993 RID: 2451
			void Initialize([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guid);
		}

		/// <summary>
		/// IDirectSoundBuffer interface
		/// </summary>
		// Token: 0x020001C4 RID: 452
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("279AFA85-4981-11CE-A521-0020AF0BE560")]
		[SuppressUnmanagedCodeSecurity]
		[ComImport]
		internal interface IDirectSoundBuffer
		{
			// Token: 0x06000994 RID: 2452
			void GetCaps([MarshalAs(UnmanagedType.LPStruct)] DirectSoundOut.BufferCaps pBufferCaps);

			// Token: 0x06000995 RID: 2453
			void GetCurrentPosition(out uint currentPlayCursor, out uint currentWriteCursor);

			// Token: 0x06000996 RID: 2454
			void GetFormat();

			// Token: 0x06000997 RID: 2455
			[return: MarshalAs(UnmanagedType.I4)]
			int GetVolume();

			// Token: 0x06000998 RID: 2456
			void GetPan(out uint pan);

			// Token: 0x06000999 RID: 2457
			[return: MarshalAs(UnmanagedType.I4)]
			int GetFrequency();

			// Token: 0x0600099A RID: 2458
			[return: MarshalAs(UnmanagedType.U4)]
			DirectSoundOut.DirectSoundBufferStatus GetStatus();

			// Token: 0x0600099B RID: 2459
			void Initialize([MarshalAs(UnmanagedType.Interface)] [In] DirectSoundOut.IDirectSound directSound, [In] DirectSoundOut.BufferDescription desc);

			// Token: 0x0600099C RID: 2460
			void Lock(int dwOffset, uint dwBytes, out IntPtr audioPtr1, out int audioBytes1, out IntPtr audioPtr2, out int audioBytes2, [MarshalAs(UnmanagedType.U4)] DirectSoundOut.DirectSoundBufferLockFlag dwFlags);

			// Token: 0x0600099D RID: 2461
			void Play(uint dwReserved1, uint dwPriority, [MarshalAs(UnmanagedType.U4)] [In] DirectSoundOut.DirectSoundPlayFlags dwFlags);

			// Token: 0x0600099E RID: 2462
			void SetCurrentPosition(uint dwNewPosition);

			// Token: 0x0600099F RID: 2463
			void SetFormat([In] WaveFormat pcfxFormat);

			// Token: 0x060009A0 RID: 2464
			void SetVolume(int volume);

			// Token: 0x060009A1 RID: 2465
			void SetPan(uint pan);

			// Token: 0x060009A2 RID: 2466
			void SetFrequency(uint frequency);

			// Token: 0x060009A3 RID: 2467
			void Stop();

			// Token: 0x060009A4 RID: 2468
			void Unlock(IntPtr pvAudioPtr1, int dwAudioBytes1, IntPtr pvAudioPtr2, int dwAudioBytes2);

			// Token: 0x060009A5 RID: 2469
			void Restore();
		}

		/// <summary>
		/// IDirectSoundNotify interface
		/// </summary>
		// Token: 0x020001C5 RID: 453
		[SuppressUnmanagedCodeSecurity]
		[Guid("b0210783-89cd-11d0-af08-00a0c925cd16")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IDirectSoundNotify
		{
			// Token: 0x060009A6 RID: 2470
			void SetNotificationPositions(uint dwPositionNotifies, [MarshalAs(UnmanagedType.LPArray)] [In] DirectSoundOut.DirectSoundBufferPositionNotify[] pcPositionNotifies);
		}

		/// <summary>
		/// The DSEnumCallback function is an application-defined callback function that enumerates the DirectSound drivers. 
		/// The system calls this function in response to the application's call to the DirectSoundEnumerate or DirectSoundCaptureEnumerate function.
		/// </summary>
		/// <param name="lpGuid">Address of the GUID that identifies the device being enumerated, or NULL for the primary device. This value can be passed to the DirectSoundCreate8 or DirectSoundCaptureCreate8 function to create a device object for that driver. </param>
		/// <param name="lpcstrDescription">Address of a null-terminated string that provides a textual description of the DirectSound device. </param>
		/// <param name="lpcstrModule">Address of a null-terminated string that specifies the module name of the DirectSound driver corresponding to this device. </param>
		/// <param name="lpContext">Address of application-defined data. This is the pointer passed to DirectSoundEnumerate or DirectSoundCaptureEnumerate as the lpContext parameter. </param>
		/// <returns>Returns TRUE to continue enumerating drivers, or FALSE to stop.</returns>
		// Token: 0x020001C6 RID: 454
		// (Invoke) Token: 0x060009A8 RID: 2472
		private delegate bool DSEnumCallback(IntPtr lpGuid, IntPtr lpcstrDescription, IntPtr lpcstrModule, IntPtr lpContext);
	}
}
