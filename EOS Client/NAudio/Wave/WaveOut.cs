using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace NAudio.Wave
{
    public class WaveOut : IWavePlayer, IDisposable, IWavePosition
    {
        public event EventHandler<StoppedEventArgs> PlaybackStopped;

        public static WaveOutCapabilities GetCapabilities(int devNumber)
        {
            WaveOutCapabilities waveOutCapabilities = default(WaveOutCapabilities);
            int waveOutCapsSize = Marshal.SizeOf(waveOutCapabilities);
            MmException.Try(WaveInterop.waveOutGetDevCaps((IntPtr)devNumber, out waveOutCapabilities, waveOutCapsSize), "waveOutGetDevCaps");
            return waveOutCapabilities;
        }

        public static int DeviceCount
        {
            get
            {
                return WaveInterop.waveOutGetNumDevs();
            }
        }

        public int DesiredLatency { get; set; }

        public int NumberOfBuffers { get; set; }

        public int DeviceNumber { get; set; }

        public WaveOut() : this((SynchronizationContext.Current == null) ? WaveCallbackInfo.FunctionCallback() : WaveCallbackInfo.NewWindow())
        {
        }

        public WaveOut(IntPtr windowHandle) : this(WaveCallbackInfo.ExistingWindow(windowHandle))
        {
        }

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

        public WaveFormat OutputWaveFormat
        {
            get
            {
                return this.waveStream.WaveFormat;
            }
        }

        public PlaybackState PlaybackState
        {
            get
            {
                return this.playbackState;
            }
        }

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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }

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

        ~WaveOut()
        {
            this.Dispose(false);
        }

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
                this.syncContext.Post(delegate (object state)
                {
                    handler(this, new StoppedEventArgs(e));
                }, null);
            }
        }

        private IntPtr hWaveOut;

        private WaveOutBuffer[] buffers;

        private IWaveProvider waveStream;

        private volatile PlaybackState playbackState;

        private WaveInterop.WaveCallback callback;

        private float volume = 1f;

        private WaveCallbackInfo callbackInfo;

        private object waveOutLock;

        private int queuedBuffers;

        private SynchronizationContext syncContext;
    }
}
