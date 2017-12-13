using System;
using System.Threading;
using NAudio.Wave.Asio;

namespace NAudio.Wave
{
    public class AsioOut : IWavePlayer, IDisposable
    {
        public event EventHandler<StoppedEventArgs> PlaybackStopped;

        public event EventHandler<AsioAudioAvailableEventArgs> AudioAvailable;

        public AsioOut() : this(0)
        {
        }

        public AsioOut(string driverName)
        {
            this.syncContext = SynchronizationContext.Current;
            this.InitFromName(driverName);
        }

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

        ~AsioOut()
        {
            this.Dispose();
        }

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

        public static string[] GetDriverNames()
        {
            return ASIODriver.GetASIODriverNames();
        }

        public static bool isSupported()
        {
            return AsioOut.GetDriverNames().Length > 0;
        }

        private void InitFromName(string driverName)
        {
            ASIODriver asiodriverByName = ASIODriver.GetASIODriverByName(driverName);
            this.driver = new ASIODriverExt(asiodriverByName);
            this.ChannelOffset = 0;
        }

        public void ShowControlPanel()
        {
            this.driver.ShowControlPanel();
        }

        public void Play()
        {
            if (this.playbackState != PlaybackState.Playing)
            {
                this.playbackState = PlaybackState.Playing;
                this.driver.Start();
            }
        }

        public void Stop()
        {
            this.playbackState = PlaybackState.Stopped;
            this.driver.Stop();
            this.RaisePlaybackStopped(null);
        }

        public void Pause()
        {
            this.playbackState = PlaybackState.Paused;
            this.driver.Stop();
        }

        public void Init(IWaveProvider waveProvider)
        {
            this.InitRecordAndPlayback(waveProvider, 0, -1);
        }

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
                fixed (byte* ptr = this.waveBuffer)
                {
                    this.convertor(new IntPtr((void*)ptr), outputChannels, this.NumberOfOutputChannels, this.nbSamples);
                }
                if (num == 0)
                {
                    this.Stop();
                }
            }
        }

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

        public PlaybackState PlaybackState
        {
            get
            {
                return this.playbackState;
            }
        }

        public string DriverName
        {
            get
            {
                return this.driverName;
            }
        }

        public int NumberOfOutputChannels { get; private set; }

        public int NumberOfInputChannels { get; private set; }

        public int DriverInputChannelCount
        {
            get
            {
                return this.driver.Capabilities.NbInputChannels;
            }
        }

        public int DriverOutputChannelCount
        {
            get
            {
                return this.driver.Capabilities.NbOutputChannels;
            }
        }

        public int ChannelOffset { get; set; }

        public int InputChannelOffset { get; set; }

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
                this.syncContext.Post(delegate (object state)
                {
                    handler(this, new StoppedEventArgs(e));
                }, null);
            }
        }

        public string AsioInputChannelName(int channel)
        {
            if (channel <= this.DriverInputChannelCount)
            {
                return this.driver.Capabilities.InputChannelInfos[channel].name;
            }
            return "";
        }

        public string AsioOutputChannelName(int channel)
        {
            if (channel <= this.DriverOutputChannelCount)
            {
                return this.driver.Capabilities.OutputChannelInfos[channel].name;
            }
            return "";
        }

        private ASIODriverExt driver;

        private IWaveProvider sourceStream;

        private PlaybackState playbackState;

        private int nbSamples;

        private byte[] waveBuffer;

        private ASIOSampleConvertor.SampleConvertor convertor;

        private readonly string driverName;

        private readonly SynchronizationContext syncContext;
    }
}
