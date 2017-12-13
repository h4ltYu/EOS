using System;
using System.Runtime.InteropServices;
using System.Threading;
using NAudio.Wave;

namespace NAudio.CoreAudioApi
{
    public class WasapiCapture : IWaveIn, IDisposable
    {
        public event EventHandler<WaveInEventArgs> DataAvailable;

        public event EventHandler<StoppedEventArgs> RecordingStopped;

        public WasapiCapture() : this(WasapiCapture.GetDefaultCaptureDevice())
        {
        }

        public WasapiCapture(MMDevice captureDevice) : this(captureDevice, false)
        {
        }

        public WasapiCapture(MMDevice captureDevice, bool useEventSync)
        {
            this.syncContext = SynchronizationContext.Current;
            this.audioClient = captureDevice.AudioClient;
            this.ShareMode = AudioClientShareMode.Shared;
            this.isUsingEventSync = useEventSync;
            this.waveFormat = this.audioClient.MixFormat;
        }

        public AudioClientShareMode ShareMode { get; set; }

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

        public static MMDevice GetDefaultCaptureDevice()
        {
            MMDeviceEnumerator mmdeviceEnumerator = new MMDeviceEnumerator();
            return mmdeviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
        }

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

        protected virtual AudioClientStreamFlags GetAudioClientStreamFlags()
        {
            return AudioClientStreamFlags.None;
        }

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

        public void StopRecording()
        {
            this.requestStop = true;
        }

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
            this.syncContext.Post(delegate (object state)
            {
                handler(this, new StoppedEventArgs(e));
            }, null);
        }

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

        private const long REFTIMES_PER_SEC = 10000000L;

        private const long REFTIMES_PER_MILLISEC = 10000L;

        private volatile bool requestStop;

        private byte[] recordBuffer;

        private Thread captureThread;

        private AudioClient audioClient;

        private int bytesPerFrame;

        private WaveFormat waveFormat;

        private bool initialized;

        private readonly SynchronizationContext syncContext;

        private readonly bool isUsingEventSync;

        private EventWaitHandle frameEventWaitHandle;
    }
}
