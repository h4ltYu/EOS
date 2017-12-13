using System;

namespace NAudio.Wave.Asio
{
	/// <summary>
	/// ASIODriverExt is a simplified version of the ASIODriver. It provides an easier
	/// way to access the capabilities of the Driver and implement the callbacks necessary 
	/// for feeding the driver.
	/// Implementation inspired from Rob Philpot's with a managed C++ ASIO wrapper BlueWave.Interop.Asio
	/// http://www.codeproject.com/KB/mcpp/Asio.Net.aspx
	///
	/// Contributor: Alexandre Mutel - email: alexandre_mutel at yahoo.fr
	/// </summary>
	// Token: 0x0200014F RID: 335
	internal class ASIODriverExt
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NAudio.Wave.Asio.ASIODriverExt" /> class based on an already
		/// instantiated ASIODriver instance.
		/// </summary>
		/// <param name="driver">A ASIODriver already instantiated.</param>
		// Token: 0x06000749 RID: 1865 RVA: 0x00015758 File Offset: 0x00013958
		public ASIODriverExt(ASIODriver driver)
		{
			this.driver = driver;
			if (!driver.init(IntPtr.Zero))
			{
				throw new InvalidOperationException(driver.getErrorMessage());
			}
			this.callbacks = default(ASIOCallbacks);
			this.callbacks.pasioMessage = new ASIOCallbacks.ASIOAsioMessageCallBack(this.AsioMessageCallBack);
			this.callbacks.pbufferSwitch = new ASIOCallbacks.ASIOBufferSwitchCallBack(this.BufferSwitchCallBack);
			this.callbacks.pbufferSwitchTimeInfo = new ASIOCallbacks.ASIOBufferSwitchTimeInfoCallBack(this.BufferSwitchTimeInfoCallBack);
			this.callbacks.psampleRateDidChange = new ASIOCallbacks.ASIOSampleRateDidChangeCallBack(this.SampleRateDidChangeCallBack);
			this.BuildCapabilities();
		}

		/// <summary>
		/// Allows adjustment of which is the first output channel we write to
		/// </summary>
		/// <param name="outputChannelOffset">Output Channel offset</param>
		/// <param name="inputChannelOffset">Input Channel offset</param>
		// Token: 0x0600074A RID: 1866 RVA: 0x000157FC File Offset: 0x000139FC
		public void SetChannelOffset(int outputChannelOffset, int inputChannelOffset)
		{
			if (outputChannelOffset + this.numberOfOutputChannels > this.Capabilities.NbOutputChannels)
			{
				throw new ArgumentException("Invalid channel offset");
			}
			this.outputChannelOffset = outputChannelOffset;
			if (inputChannelOffset + this.numberOfInputChannels <= this.Capabilities.NbInputChannels)
			{
				this.inputChannelOffset = inputChannelOffset;
				return;
			}
			throw new ArgumentException("Invalid channel offset");
		}

		/// <summary>
		/// Gets the driver used.
		/// </summary>
		/// <value>The ASIOdriver.</value>
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x00015859 File Offset: 0x00013A59
		public ASIODriver Driver
		{
			get
			{
				return this.driver;
			}
		}

		/// <summary>
		/// Starts playing the buffers.
		/// </summary>
		// Token: 0x0600074C RID: 1868 RVA: 0x00015861 File Offset: 0x00013A61
		public void Start()
		{
			this.driver.start();
		}

		/// <summary>
		/// Stops playing the buffers.
		/// </summary>
		// Token: 0x0600074D RID: 1869 RVA: 0x0001586E File Offset: 0x00013A6E
		public void Stop()
		{
			this.driver.stop();
		}

		/// <summary>
		/// Shows the control panel.
		/// </summary>
		// Token: 0x0600074E RID: 1870 RVA: 0x0001587C File Offset: 0x00013A7C
		public void ShowControlPanel()
		{
			this.driver.controlPanel();
		}

		/// <summary>
		/// Releases this instance.
		/// </summary>
		// Token: 0x0600074F RID: 1871 RVA: 0x0001588C File Offset: 0x00013A8C
		public void ReleaseDriver()
		{
			try
			{
				this.driver.disposeBuffers();
			}
			catch (Exception ex)
			{
				Console.Out.WriteLine(ex.ToString());
			}
			this.driver.ReleaseComASIODriver();
		}

		/// <summary>
		/// Determines whether the specified sample rate is supported.
		/// </summary>
		/// <param name="sampleRate">The sample rate.</param>
		/// <returns>
		/// 	<c>true</c> if [is sample rate supported]; otherwise, <c>false</c>.
		/// </returns>
		// Token: 0x06000750 RID: 1872 RVA: 0x000158D8 File Offset: 0x00013AD8
		public bool IsSampleRateSupported(double sampleRate)
		{
			return this.driver.canSampleRate(sampleRate);
		}

		/// <summary>
		/// Sets the sample rate.
		/// </summary>
		/// <param name="sampleRate">The sample rate.</param>
		// Token: 0x06000751 RID: 1873 RVA: 0x000158E6 File Offset: 0x00013AE6
		public void SetSampleRate(double sampleRate)
		{
			this.driver.setSampleRate(sampleRate);
			this.BuildCapabilities();
		}

		/// <summary>
		/// Gets or sets the fill buffer callback.
		/// </summary>
		/// <value>The fill buffer callback.</value>
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x000158FA File Offset: 0x00013AFA
		// (set) Token: 0x06000753 RID: 1875 RVA: 0x00015902 File Offset: 0x00013B02
		public ASIOFillBufferCallback FillBufferCallback
		{
			get
			{
				return this.fillBufferCallback;
			}
			set
			{
				this.fillBufferCallback = value;
			}
		}

		/// <summary>
		/// Gets the capabilities of the ASIODriver.
		/// </summary>
		/// <value>The capabilities.</value>
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x0001590B File Offset: 0x00013B0B
		public AsioDriverCapability Capabilities
		{
			get
			{
				return this.capability;
			}
		}

		/// <summary>
		/// Creates the buffers for playing.
		/// </summary>
		/// <param name="numberOfOutputChannels">The number of outputs channels.</param>
		/// <param name="numberOfInputChannels">The number of input channel.</param>
		/// <param name="useMaxBufferSize">if set to <c>true</c> [use max buffer size] else use Prefered size</param>
		// Token: 0x06000755 RID: 1877 RVA: 0x00015914 File Offset: 0x00013B14
		public unsafe int CreateBuffers(int numberOfOutputChannels, int numberOfInputChannels, bool useMaxBufferSize)
		{
			if (numberOfOutputChannels < 0 || numberOfOutputChannels > this.capability.NbOutputChannels)
			{
				throw new ArgumentException(string.Format("Invalid number of channels {0}, must be in the range [0,{1}]", numberOfOutputChannels, this.capability.NbOutputChannels));
			}
			if (numberOfInputChannels < 0 || numberOfInputChannels > this.capability.NbInputChannels)
			{
				throw new ArgumentException("numberOfInputChannels", string.Format("Invalid number of input channels {0}, must be in the range [0,{1}]", numberOfInputChannels, this.capability.NbInputChannels));
			}
			this.numberOfOutputChannels = numberOfOutputChannels;
			this.numberOfInputChannels = numberOfInputChannels;
			int num = this.capability.NbInputChannels + this.capability.NbOutputChannels;
			this.bufferInfos = new ASIOBufferInfo[num];
			this.currentOutputBuffers = new IntPtr[numberOfOutputChannels];
			this.currentInputBuffers = new IntPtr[numberOfInputChannels];
			int num2 = 0;
			int i = 0;
			while (i < this.capability.NbInputChannels)
			{
				this.bufferInfos[num2].isInput = true;
				this.bufferInfos[num2].channelNum = i;
				this.bufferInfos[num2].pBuffer0 = IntPtr.Zero;
				this.bufferInfos[num2].pBuffer1 = IntPtr.Zero;
				i++;
				num2++;
			}
			int j = 0;
			while (j < this.capability.NbOutputChannels)
			{
				this.bufferInfos[num2].isInput = false;
				this.bufferInfos[num2].channelNum = j;
				this.bufferInfos[num2].pBuffer0 = IntPtr.Zero;
				this.bufferInfos[num2].pBuffer1 = IntPtr.Zero;
				j++;
				num2++;
			}
			if (useMaxBufferSize)
			{
				this.bufferSize = this.capability.BufferMaxSize;
			}
			else
			{
				this.bufferSize = this.capability.BufferPreferredSize;
			}
			fixed (ASIOBufferInfo* ptr = &this.bufferInfos[0])
			{
				IntPtr intPtr = new IntPtr((void*)ptr);
				this.driver.createBuffers(intPtr, num, this.bufferSize, ref this.callbacks);
			}
			this.isOutputReadySupported = (this.driver.outputReady() == ASIOError.ASE_OK);
			return this.bufferSize;
		}

		/// <summary>
		/// Builds the capabilities internally.
		/// </summary>
		// Token: 0x06000756 RID: 1878 RVA: 0x00015B30 File Offset: 0x00013D30
		private void BuildCapabilities()
		{
			this.capability = new AsioDriverCapability();
			this.capability.DriverName = this.driver.getDriverName();
			this.driver.getChannels(out this.capability.NbInputChannels, out this.capability.NbOutputChannels);
			this.capability.InputChannelInfos = new ASIOChannelInfo[this.capability.NbInputChannels];
			this.capability.OutputChannelInfos = new ASIOChannelInfo[this.capability.NbOutputChannels];
			for (int i = 0; i < this.capability.NbInputChannels; i++)
			{
				this.capability.InputChannelInfos[i] = this.driver.getChannelInfo(i, true);
			}
			for (int j = 0; j < this.capability.NbOutputChannels; j++)
			{
				this.capability.OutputChannelInfos[j] = this.driver.getChannelInfo(j, false);
			}
			this.capability.SampleRate = this.driver.getSampleRate();
			ASIOError latencies = this.driver.GetLatencies(out this.capability.InputLatency, out this.capability.OutputLatency);
			if (latencies != ASIOError.ASE_OK && latencies != ASIOError.ASE_NotPresent)
			{
				throw new ASIOException("ASIOgetLatencies")
				{
					Error = latencies
				};
			}
			this.driver.getBufferSize(out this.capability.BufferMinSize, out this.capability.BufferMaxSize, out this.capability.BufferPreferredSize, out this.capability.BufferGranularity);
		}

		/// <summary>
		/// Callback called by the ASIODriver on fill buffer demand. Redirect call to external callback.
		/// </summary>
		/// <param name="doubleBufferIndex">Index of the double buffer.</param>
		/// <param name="directProcess">if set to <c>true</c> [direct process].</param>
		// Token: 0x06000757 RID: 1879 RVA: 0x00015CB8 File Offset: 0x00013EB8
		private void BufferSwitchCallBack(int doubleBufferIndex, bool directProcess)
		{
			for (int i = 0; i < this.numberOfInputChannels; i++)
			{
				this.currentInputBuffers[i] = this.bufferInfos[i + this.inputChannelOffset].Buffer(doubleBufferIndex);
			}
			for (int j = 0; j < this.numberOfOutputChannels; j++)
			{
				this.currentOutputBuffers[j] = this.bufferInfos[j + this.outputChannelOffset + this.capability.NbInputChannels].Buffer(doubleBufferIndex);
			}
			if (this.fillBufferCallback != null)
			{
				this.fillBufferCallback(this.currentInputBuffers, this.currentOutputBuffers);
			}
			if (this.isOutputReadySupported)
			{
				this.driver.outputReady();
			}
		}

		/// <summary>
		/// Callback called by the ASIODriver on event "Samples rate changed".
		/// </summary>
		/// <param name="sRate">The sample rate.</param>
		// Token: 0x06000758 RID: 1880 RVA: 0x00015D7A File Offset: 0x00013F7A
		private void SampleRateDidChangeCallBack(double sRate)
		{
			this.capability.SampleRate = sRate;
		}

		/// <summary>
		/// Asio message call back.
		/// </summary>
		/// <param name="selector">The selector.</param>
		/// <param name="value">The value.</param>
		/// <param name="message">The message.</param>
		/// <param name="opt">The opt.</param>
		/// <returns></returns>
		// Token: 0x06000759 RID: 1881 RVA: 0x00015D88 File Offset: 0x00013F88
		private int AsioMessageCallBack(ASIOMessageSelector selector, int value, IntPtr message, IntPtr opt)
		{
			switch (selector)
			{
			case ASIOMessageSelector.kAsioSelectorSupported:
				switch ((ASIOMessageSelector)Enum.ToObject(typeof(ASIOMessageSelector), value))
				{
				case ASIOMessageSelector.kAsioEngineVersion:
					return 1;
				case ASIOMessageSelector.kAsioResetRequest:
					return 0;
				case ASIOMessageSelector.kAsioBufferSizeChange:
					return 0;
				case ASIOMessageSelector.kAsioResyncRequest:
					return 0;
				case ASIOMessageSelector.kAsioLatenciesChanged:
					return 0;
				case ASIOMessageSelector.kAsioSupportsTimeInfo:
					return 0;
				case ASIOMessageSelector.kAsioSupportsTimeCode:
					return 0;
				}
				break;
			case ASIOMessageSelector.kAsioEngineVersion:
				return 2;
			case ASIOMessageSelector.kAsioResetRequest:
				return 1;
			case ASIOMessageSelector.kAsioBufferSizeChange:
				return 0;
			case ASIOMessageSelector.kAsioResyncRequest:
				return 0;
			case ASIOMessageSelector.kAsioLatenciesChanged:
				return 0;
			case ASIOMessageSelector.kAsioSupportsTimeInfo:
				return 0;
			case ASIOMessageSelector.kAsioSupportsTimeCode:
				return 0;
			}
			return 0;
		}

		/// <summary>
		/// Buffers switch time info call back.
		/// </summary>
		/// <param name="asioTimeParam">The asio time param.</param>
		/// <param name="doubleBufferIndex">Index of the double buffer.</param>
		/// <param name="directProcess">if set to <c>true</c> [direct process].</param>
		/// <returns></returns>
		// Token: 0x0600075A RID: 1882 RVA: 0x00015E1C File Offset: 0x0001401C
		private IntPtr BufferSwitchTimeInfoCallBack(IntPtr asioTimeParam, int doubleBufferIndex, bool directProcess)
		{
			return IntPtr.Zero;
		}

		// Token: 0x04000763 RID: 1891
		private ASIODriver driver;

		// Token: 0x04000764 RID: 1892
		private ASIOCallbacks callbacks;

		// Token: 0x04000765 RID: 1893
		private AsioDriverCapability capability;

		// Token: 0x04000766 RID: 1894
		private ASIOBufferInfo[] bufferInfos;

		// Token: 0x04000767 RID: 1895
		private bool isOutputReadySupported;

		// Token: 0x04000768 RID: 1896
		private IntPtr[] currentOutputBuffers;

		// Token: 0x04000769 RID: 1897
		private IntPtr[] currentInputBuffers;

		// Token: 0x0400076A RID: 1898
		private int numberOfOutputChannels;

		// Token: 0x0400076B RID: 1899
		private int numberOfInputChannels;

		// Token: 0x0400076C RID: 1900
		private ASIOFillBufferCallback fillBufferCallback;

		// Token: 0x0400076D RID: 1901
		private int bufferSize;

		// Token: 0x0400076E RID: 1902
		private int outputChannelOffset;

		// Token: 0x0400076F RID: 1903
		private int inputChannelOffset;
	}
}
