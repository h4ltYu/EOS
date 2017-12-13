using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
    public class SimpleAudioVolume : IDisposable
    {
        internal SimpleAudioVolume(ISimpleAudioVolume realSimpleVolume)
        {
            this.simpleAudioVolume = realSimpleVolume;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        ~SimpleAudioVolume()
        {
            this.Dispose();
        }

        public float Volume
        {
            get
            {
                float result;
                Marshal.ThrowExceptionForHR(this.simpleAudioVolume.GetMasterVolume(out result));
                return result;
            }
            set
            {
                if ((double)value >= 0.0 && (double)value <= 1.0)
                {
                    Marshal.ThrowExceptionForHR(this.simpleAudioVolume.SetMasterVolume(value, Guid.Empty));
                }
            }
        }

        public bool Mute
        {
            get
            {
                bool result;
                Marshal.ThrowExceptionForHR(this.simpleAudioVolume.GetMute(out result));
                return result;
            }
            set
            {
                Marshal.ThrowExceptionForHR(this.simpleAudioVolume.SetMute(value, Guid.Empty));
            }
        }

        private readonly ISimpleAudioVolume simpleAudioVolume;
    }
}
