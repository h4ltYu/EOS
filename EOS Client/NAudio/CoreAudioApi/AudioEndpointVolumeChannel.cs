using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
    public class AudioEndpointVolumeChannel
    {
        internal AudioEndpointVolumeChannel(IAudioEndpointVolume parent, int channel)
        {
            this.channel = (uint)channel;
            this.audioEndpointVolume = parent;
        }

        public float VolumeLevel
        {
            get
            {
                float result;
                Marshal.ThrowExceptionForHR(this.audioEndpointVolume.GetChannelVolumeLevel(this.channel, out result));
                return result;
            }
            set
            {
                Marshal.ThrowExceptionForHR(this.audioEndpointVolume.SetChannelVolumeLevel(this.channel, value, Guid.Empty));
            }
        }

        public float VolumeLevelScalar
        {
            get
            {
                float result;
                Marshal.ThrowExceptionForHR(this.audioEndpointVolume.GetChannelVolumeLevelScalar(this.channel, out result));
                return result;
            }
            set
            {
                Marshal.ThrowExceptionForHR(this.audioEndpointVolume.SetChannelVolumeLevelScalar(this.channel, value, Guid.Empty));
            }
        }

        private readonly uint channel;

        private readonly IAudioEndpointVolume audioEndpointVolume;
    }
}
