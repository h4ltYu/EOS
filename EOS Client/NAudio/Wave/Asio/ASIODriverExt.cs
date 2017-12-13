using System;

namespace NAudio.Wave.Asio
{
    internal class ASIODriverExt
    {
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

        public ASIODriver Driver
        {
            get
            {
                return this.driver;
            }
        }

        public void Start()
        {
            this.driver.start();
        }

        public void Stop()
        {
            this.driver.stop();
        }

        public void ShowControlPanel()
        {
            this.driver.controlPanel();
        }

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

        public bool IsSampleRateSupported(double sampleRate)
        {
            return this.driver.canSampleRate(sampleRate);
        }

        public void SetSampleRate(double sampleRate)
        {
            this.driver.setSampleRate(sampleRate);
            this.BuildCapabilities();
        }

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

        public AsioDriverCapability Capabilities
        {
            get
            {
                return this.capability;
            }
        }

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

        private void SampleRateDidChangeCallBack(double sRate)
        {
            this.capability.SampleRate = sRate;
        }

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

        private IntPtr BufferSwitchTimeInfoCallBack(IntPtr asioTimeParam, int doubleBufferIndex, bool directProcess)
        {
            return IntPtr.Zero;
        }

        private ASIODriver driver;

        private ASIOCallbacks callbacks;

        private AsioDriverCapability capability;

        private ASIOBufferInfo[] bufferInfos;

        private bool isOutputReadySupported;

        private IntPtr[] currentOutputBuffers;

        private IntPtr[] currentInputBuffers;

        private int numberOfOutputChannels;

        private int numberOfInputChannels;

        private ASIOFillBufferCallback fillBufferCallback;

        private int bufferSize;

        private int outputChannelOffset;

        private int inputChannelOffset;
    }
}
