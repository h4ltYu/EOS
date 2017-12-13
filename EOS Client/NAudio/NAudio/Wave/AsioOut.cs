using System;
using System.Threading;
using NAudio.Wave.Asio;

namespace NAudio.Wave
{
	/// <summary>
	/// ASIO Out Player. New implementation using an internal C# binding.
	///
	/// This implementation is only supporting Short16Bit and Float32Bit formats and is optimized 
	/// for 2 outputs channels .
	/// SampleRate is supported only if ASIODriver is supporting it
	///
	/// This implementation is probably the first ASIODriver binding fully implemented in C#!
	///
	/// Original Contributor: Mark Heath 
	/// New Contributor to C# binding : Alexandre Mutel - email: alexandre_mutel at yahoo.fr
	/// </summary>
	// Token: 0x020001B2 RID: 434
	public class AsioOut : IWavePlayer, IDisposable
	{
		/// <summary>
		/// Playback Stopped
		/// </summary>
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060008F2 RID: 2290 RVA: 0x00019D0C File Offset: 0x00017F0C
		// (remove) Token: 0x060008F3 RID: 2291 RVA: 0x00019D44 File Offset: 0x00017F44
		public event EventHandler<StoppedEventArgs> PlaybackStopped;

		/// <summary>
		/// When recording, fires whenever recorded audio is available
		/// </summary>
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060008F4 RID: 2292 RVA: 0x00019D7C File Offset: 0x00017F7C
		// (remove) Token: 0x060008F5 RID: 2293 RVA: 0x00019DB4 File Offset: 0x00017FB4
		public event EventHandler<AsioAudioAvailableEventArgs> AudioAvailable;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NAudio.Wave.AsioOut" /> class with the first 
		/// available ASIO Driver.
		/// </summary>
		// Token: 0x060008F6 RID: 2294 RVA: 0x00019DE9 File Offset: 0x00017FE9
		public AsioOut() : this(0)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NAudio.Wave.AsioOut" /> class with the driver name.
		/// </summary>
		/// <param name="driverName">Name of the device.</param>
		// Token: 0x060008F7 RID: 2295 RVA: 0x00019DF2 File Offset: 0x00017FF2
		public AsioOut(string driverName)
		{
			this.syncContext = SynchronizationContext.Current;
			this.InitFromName(driverName);
		}

		/// <summary>
		/// Opens an ASIO output device
		/// </summary>
		/// <param name="driverIndex">Device number (zero based)</param>
		// Token: 0x060008F8 RID: 2296 RVA: 0x00019E0C File Offset: 0x0001800C
		public AsioOut(int driverIndex)
		{
			this.syncContext = SynchronizationContext.Current;
			string[] driverNames = AsioOut.GetDriverNames();
			if (driverNames.Length == 0)
			{
				throw new ArgumentException("There is no ASIO Driver installed on your system");
			}
			if (driverIndex < 0 || driverIndex > driverNames.Length)
			{
				throw new ArgumentException(string.Format("Invalid device number. Must be in the range [0,{0}]", driverNames.Length));
			}
			this.driverName = driverNames[driverIndex];
			this.InitFromName(this.driverName);
		}

		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the
		/// <see cref="T:NAudio.Wave.AsioOut" /> is reclaimed by garbage collection.
		/// </summary>
		// Token: 0x060008F9 RID: 2297 RVA: 0x00019E78 File Offset: 0x00018078
		~AsioOut()
		{
			this.Dispose();
		}

		/// <summary>
		/// Dispose
		/// </summary>
		// Token: 0x060008FA RID: 2298 RVA: 0x00019EA4 File Offset: 0x000180A4
		public void Dispose()
		{
			if (this.driver != null)
			{
				if (this.playbackState != PlaybackState.Stopped)
				{
					this.driver.Stop();
				}
				this.driver.ReleaseDriver();
				this.driver = null;
			}
		}

		/// <summary>
		/// Gets the names of the installed ASIO Driver.
		/// </summary>
		/// <returns>an array of driver names</returns>
		// Token: 0x060008FB RID: 2299 RVA: 0x00019ED3 File Offset: 0x000180D3
		public static string[] GetDriverNames()
		{
			return ASIODriver.GetASIODriverNames();
		}

		/// <summary>
		/// Determines whether ASIO is supported.
		/// </summary>
		/// <returns>
		///     <c>true</c> if ASIO is supported; otherwise, <c>false</c>.
		/// </returns>
		// Token: 0x060008FC RID: 2300 RVA: 0x00019EDA File Offset: 0x000180DA
		public static bool isSupported()
		{
			return AsioOut.GetDriverNames().Length > 0;
		}

		/// <summary>
		/// Inits the driver from the asio driver name.
		/// </summary>
		/// <param name="driverName">Name of the driver.</param>
		// Token: 0x060008FD RID: 2301 RVA: 0x00019EE8 File Offset: 0x000180E8
		private void InitFromName(string driverName)
		{
			ASIODriver asiodriverByName = ASIODriver.GetASIODriverByName(driverName);
			this.driver = new ASIODriverExt(asiodriverByName);
			this.ChannelOffset = 0;
		}

		/// <summary>
		/// Shows the control panel
		/// </summary>
		// Token: 0x060008FE RID: 2302 RVA: 0x00019F0F File Offset: 0x0001810F
		public void ShowControlPanel()
		{
			this.driver.ShowControlPanel();
		}

		/// <summary>
		/// Starts playback
		/// </summary>
		// Token: 0x060008FF RID: 2303 RVA: 0x00019F1C File Offset: 0x0001811C
		public void Play()
		{
			if (this.playbackState != PlaybackState.Playing)
			{
				this.playbackState = PlaybackState.Playing;
				this.driver.Start();
			}
		}

		/// <summary>
		/// Stops playback
		/// </summary>
		// Token: 0x06000900 RID: 2304 RVA: 0x00019F39 File Offset: 0x00018139
		public void Stop()
		{
			this.playbackState = PlaybackState.Stopped;
			this.driver.Stop();
			this.RaisePlaybackStopped(null);
		}

		/// <summary>
		/// Pauses playback
		/// </summary>
		// Token: 0x06000901 RID: 2305 RVA: 0x00019F54 File Offset: 0x00018154
		public void Pause()
		{
			this.playbackState = PlaybackState.Paused;
			this.driver.Stop();
		}

		/// <summary>
		/// Initialises to play
		/// </summary>
		/// <param name="waveProvider">Source wave provider</param>
		// Token: 0x06000902 RID: 2306 RVA: 0x00019F68 File Offset: 0x00018168
		public void Init(IWaveProvider waveProvider)
		{
			this.InitRecordAndPlayback(waveProvider, 0, -1);
		}

		/// <summary>
		/// Initialises to play, with optional recording
		/// </summary>
		/// <param name="waveProvider">Source wave provider - set to null for record only</param>
		/// <param name="recordChannels">Number of channels to record</param>
		/// <param name="recordOnlySampleRate">Specify sample rate here if only recording, ignored otherwise</param>
		// Token: 0x06000903 RID: 2307 RVA: 0x00019F74 File Offset: 0x00018174
		public void InitRecordAndPlayback(IWaveProvider waveProvider, int recordChannels, int recordOnlySampleRate)
		{
			if (this.sourceStream != null)
			{
				throw new InvalidOperationException("Already initialised this instance of AsioOut - dispose and create a new one");
			}
			int num = (waveProvider != null) ? waveProvider.WaveFormat.SampleRate : recordOnlySampleRate;
			if (waveProvider != null)
			{
				this.sourceStream = waveProvider;
				this.NumberOfOutputChannels = waveProvider.WaveFormat.Channels;
				this.convertor = ASIOSampleConvertor.SelectSampleConvertor(waveProvider.WaveFormat, this.driver.Capabilities.OutputChannelInfos[0].type);
			}
			else
			{
				this.NumberOfOutputChannels = 0;
			}
			if (!this.driver.IsSampleRateSupported((double)num))
			{
				throw new ArgumentException("SampleRate is not supported");
			}
			if (this.driver.Capabilities.SampleRate != (double)num)
			{
				this.driver.SetSampleRate((double)num);
			}
			this.driver.FillBufferCallback = new ASIOFillBufferCallback(this.driver_BufferUpdate);
			this.NumberOfInputChannels = recordChannels;
			this.nbSamples = this.driver.CreateBuffers(this.NumberOfOutputChannels, this.NumberOfInputChannels, false);
			this.driver.SetChannelOffset(this.ChannelOffset, this.InputChannelOffset);
			if (waveProvider != null)
			{
				this.waveBuffer = new byte[this.nbSamples * this.NumberOfOutputChannels * waveProvider.WaveFormat.BitsPerSample / 8];
			}
		}

		/// <summary>
		/// driver buffer update callback to fill the wave buffer.
		/// </summary>
		/// <param name="inputChannels">The input channels.</param>
		/// <param name="outputChannels">The output channels.</param>
		// Token: 0x06000904 RID: 2308 RVA: 0x0001A0B0 File Offset: 0x000182B0
		private unsafe void driver_BufferUpdate(IntPtr[] inputChannels, IntPtr[] outputChannels)
		{
			if (this.NumberOfInputChannels > 0)
			{
				EventHandler<AsioAudioAvailableEventArgs> audioAvailable = this.AudioAvailable;
				if (audioAvailable != null)
				{
					AsioAudioAvailableEventArgs asioAudioAvailableEventArgs = new AsioAudioAvailableEventArgs(inputChannels, outputChannels, this.nbSamples, this.driver.Capabilities.InputChannelInfos[0].type);
					audioAvailable(this, asioAudioAvailableEventArgs);
					if (asioAudioAvailableEventArgs.WrittenToOutputBuffers)
					{
						return;
					}
				}
			}
			if (this.NumberOfOutputChannels > 0)
			{
				int num = this.sourceStream.Read(this.waveBuffer, 0, this.waveBuffer.Length);
				int num2 = this.waveBuffer.Length;
				fixed (IntPtr* ptr = (IntPtr*)(&this.waveBuffer[0]))
				{
					this.convertor(new IntPtr((void*)ptr), outputChannels, this.NumberOfOutputChannels, this.nbSamples);
				}
				if (num == 0)
				{
					this.Stop();
				}
			}
		}

		/// <summary>
		/// Gets the latency (in ms) of the playback driver
		/// </summary>
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x0001A174 File Offset: 0x00018374
		public int PlaybackLatency
		{
			get
			{
				int num;
				int result;
				this.driver.Driver.GetLatencies(out num, out result);
				return result;
			}
		}

		/// <summary>
		/// Playback State
		/// </summary>
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x0001A197 File Offset: 0x00018397
		public PlaybackState PlaybackState
		{
			get
			{
				return this.playbackState;
			}
		}

		/// <summary>
		/// Driver Name
		/// </summary>
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x0001A19F File Offset: 0x0001839F
		public string DriverName
		{
			get
			{
				return this.driverName;
			}
		}

		/// <summary>
		/// The number of output channels we are currently using for playback
		/// (Must be less than or equal to DriverOutputChannelCount)
		/// </summary>
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x0001A1A7 File Offset: 0x000183A7
		// (set) Token: 0x06000909 RID: 2313 RVA: 0x0001A1AF File Offset: 0x000183AF
		public int NumberOfOutputChannels { get; private set; }

		/// <summary>
		/// The number of input channels we are currently recording from
		/// (Must be less than or equal to DriverInputChannelCount)
		/// </summary>
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x0001A1B8 File Offset: 0x000183B8
		// (set) Token: 0x0600090B RID: 2315 RVA: 0x0001A1C0 File Offset: 0x000183C0
		public int NumberOfInputChannels { get; private set; }

		/// <summary>
		/// The maximum number of input channels this ASIO driver supports
		/// </summary>
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x0001A1C9 File Offset: 0x000183C9
		public int DriverInputChannelCount
		{
			get
			{
				return this.driver.Capabilities.NbInputChannels;
			}
		}

		/// <summary>
		/// The maximum number of output channels this ASIO driver supports
		/// </summary>
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x0001A1DB File Offset: 0x000183DB
		public int DriverOutputChannelCount
		{
			get
			{
				return this.driver.Capabilities.NbOutputChannels;
			}
		}

		/// <summary>
		/// By default the first channel on the input WaveProvider is sent to the first ASIO output.
		/// This option sends it to the specified channel number.
		/// Warning: make sure you don't set it higher than the number of available output channels - 
		/// the number of source channels.
		/// n.b. Future NAudio may modify this
		/// </summary>
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x0001A1ED File Offset: 0x000183ED
		// (set) Token: 0x0600090F RID: 2319 RVA: 0x0001A1F5 File Offset: 0x000183F5
		public int ChannelOffset { get; set; }

		/// <summary>
		/// Input channel offset (used when recording), allowing you to choose to record from just one
		/// specific input rather than them all
		/// </summary>
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x0001A1FE File Offset: 0x000183FE
		// (set) Token: 0x06000911 RID: 2321 RVA: 0x0001A206 File Offset: 0x00018406
		public int InputChannelOffset { get; set; }

		/// <summary>
		/// Sets the volume (1.0 is unity gain)
		/// Not supported for ASIO Out. Set the volume on the input stream instead
		/// </summary>
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x0001A20F File Offset: 0x0001840F
		// (set) Token: 0x06000913 RID: 2323 RVA: 0x0001A216 File Offset: 0x00018416
		[Obsolete("this function will be removed in a future NAudio as ASIO does not support setting the volume on the device")]
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
					throw new InvalidOperationException("AsioOut does not support setting the device volume");
				}
			}
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0001A254 File Offset: 0x00018454
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
		/// Get the input channel name
		/// </summary>
		/// <param name="channel">channel index (zero based)</param>
		/// <returns>channel name</returns>
		// Token: 0x06000915 RID: 2325 RVA: 0x0001A2C8 File Offset: 0x000184C8
		public string AsioInputChannelName(int channel)
		{
			if (channel <= this.DriverInputChannelCount)
			{
				return this.driver.Capabilities.InputChannelInfos[channel].name;
			}
			return "";
		}

		/// <summary>
		/// Get the output channel name
		/// </summary>
		/// <param name="channel">channel index (zero based)</param>
		/// <returns>channel name</returns>
		// Token: 0x06000916 RID: 2326 RVA: 0x0001A2F4 File Offset: 0x000184F4
		public string AsioOutputChannelName(int channel)
		{
			if (channel <= this.DriverOutputChannelCount)
			{
				return this.driver.Capabilities.OutputChannelInfos[channel].name;
			}
			return "";
		}

		// Token: 0x04000A8B RID: 2699
		private ASIODriverExt driver;

		// Token: 0x04000A8C RID: 2700
		private IWaveProvider sourceStream;

		// Token: 0x04000A8D RID: 2701
		private PlaybackState playbackState;

		// Token: 0x04000A8E RID: 2702
		private int nbSamples;

		// Token: 0x04000A8F RID: 2703
		private byte[] waveBuffer;

		// Token: 0x04000A90 RID: 2704
		private ASIOSampleConvertor.SampleConvertor convertor;

		// Token: 0x04000A91 RID: 2705
		private readonly string driverName;

		// Token: 0x04000A92 RID: 2706
		private readonly SynchronizationContext syncContext;
	}
}
