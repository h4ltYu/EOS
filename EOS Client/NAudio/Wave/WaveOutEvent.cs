using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace NAudio.Wave
{
    public class WaveOutEvent : IWavePlayer, IDisposable, IWavePosition
    {
        public event EventHandler<StoppedEventArgs> PlaybackStopped;

        public int DesiredLatency { get; set; }

        public int NumberOfBuffers { get; set; }

        public int DeviceNumber { get; set; }

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
                ThreadPool.QueueUserWorkItem(delegate (object state)
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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            this.Stop();
            if (disposing)
            {
                this.DisposeBuffers();
            }
            this.CloseWaveOut();
        }

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

        ~WaveOutEvent()
        {
            this.Dispose(false);
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

        private readonly object waveOutLock;

        private readonly SynchronizationContext syncContext;

        private IntPtr hWaveOut;

        private WaveOutBuffer[] buffers;

        private IWaveProvider waveStream;

        private volatile PlaybackState playbackState;

        private AutoResetEvent callbackEvent;

        private float volume = 1f;
    }
}
