using System;
using NAudio.CoreAudioApi;

namespace NAudio.Wave
{
    public class WasapiLoopbackCapture : WasapiCapture
    {
        public WasapiLoopbackCapture() : this(WasapiLoopbackCapture.GetDefaultLoopbackCaptureDevice())
        {
        }

        public WasapiLoopbackCapture(MMDevice captureDevice) : base(captureDevice)
        {
        }

        public static MMDevice GetDefaultLoopbackCaptureDevice()
        {
            MMDeviceEnumerator mmdeviceEnumerator = new MMDeviceEnumerator();
            return mmdeviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
        }

        public override WaveFormat WaveFormat
        {
            get
            {
                return base.WaveFormat;
            }
            set
            {
                throw new InvalidOperationException("WaveFormat cannot be set for WASAPI Loopback Capture");
            }
        }

        protected override AudioClientStreamFlags GetAudioClientStreamFlags()
        {
            return AudioClientStreamFlags.Loopback;
        }
    }
}
